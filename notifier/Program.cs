using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var webhookUrl = config["Discord:WebhookUrl"];
var savedVariablesDir = config["SavedVariablesPath"];
var savedVariablesFile = Path.Combine(savedVariablesDir, "unitscan-turtle-hc.lua");

if (string.IsNullOrWhiteSpace(webhookUrl) || webhookUrl.Contains("YOUR_WEBHOOK"))
{
    Console.WriteLine("ERROR: Discord webhook URL not configured in appsettings.json");
    Environment.Exit(1);
}

if (!Directory.Exists(savedVariablesDir))
{
    Console.WriteLine($"ERROR: SavedVariables directory not found: {savedVariablesDir}");
    Environment.Exit(1);
}

Console.WriteLine($"Watching for alerts in: {savedVariablesFile}");
Console.WriteLine("Press Ctrl+C to stop.\n");

string lastAlertHash = "";

// Initial read to establish baseline
if (File.Exists(savedVariablesFile))
{
    lastAlertHash = GetAlertHash(savedVariablesFile);
}

using (var watcher = new FileSystemWatcher(savedVariablesDir))
{
    watcher.Filter = "unitscan-turtle-hc.lua";
    watcher.NotifyFilter = NotifyFilters.LastWrite;

    watcher.Changed += (sender, e) =>
    {
        Thread.Sleep(100); // Brief delay to ensure file is written
        var currentHash = GetAlertHash(e.FullPath);
        
        if (currentHash != lastAlertHash && !string.IsNullOrEmpty(currentHash))
        {
            lastAlertHash = currentHash;
            ProcessAlert(e.FullPath, webhookUrl).GetAwaiter().GetResult();
        }
    };

    watcher.EnableRaisingEvents = true;

    // Keep running
    Console.CancelKeyPress += (sender, e) =>
    {
        e.Cancel = true;
        Console.WriteLine("\nShutting down...");
        Environment.Exit(0);
    };

    while (true)
    {
        Thread.Sleep(1000);
    }
}

string GetAlertHash(string filePath)
{
    try
    {
        var content = File.ReadAllText(filePath);
        var match = Regex.Match(content, @"unitscan_alerts\s*=\s*{.*?last\s*=\s*{\s*name\s*=\s*[""']([^""']+)[""'],\s*zone\s*=\s*[""']([^""']+)[""'],\s*ts\s*=\s*(\d+)", RegexOptions.Singleline);
        
        if (match.Success)
        {
            var name = match.Groups[1].Value;
            var zone = match.Groups[2].Value;
            var ts = match.Groups[3].Value;
            return $"{name}|{zone}|{ts}";
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading alert: {ex.Message}");
    }
    
    return "";
}

async Task ProcessAlert(string filePath, string webhookUrl)
{
    try
    {
        var content = File.ReadAllText(filePath);
        var match = Regex.Match(content, @"unitscan_alerts\s*=\s*{.*?last\s*=\s*{\s*name\s*=\s*[""']([^""']+)[""'],\s*zone\s*=\s*[""']([^""']+)[""'],\s*ts\s*=\s*(\d+)", RegexOptions.Singleline);
        
        if (match.Success)
        {
            var name = match.Groups[1].Value;
            var zone = match.Groups[2].Value;
            var ts = long.Parse(match.Groups[3].Value);
            var timestamp = UnixTimeStampToDateTime(ts);

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Alert: {name} in {zone}");

            await SendDiscordMessage(webhookUrl, name, zone, timestamp);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing alert: {ex.Message}");
    }
}

async Task SendDiscordMessage(string webhookUrl, string bossName, string zone, DateTime timestamp)
{
    try
    {
        using (var client = new HttpClient())
        {
            var payload = new
            {
                embeds = new[]
                {
                    new
                    {
                        title = "🐉 World Boss Detected!",
                        description = $"**{bossName}** has been spotted!",
                        fields = new[]
                        {
                            new { name = "Zone", value = zone, inline = true },
                            new { name = "Time", value = timestamp.ToString("yyyy-MM-dd HH:mm:ss"), inline = true }
                        },
                        color = 16711680, // Red
                        thumbnail = new { url = "https://wow.zamimg.com/images/wow/icons/large/inv_misc_head_dragon_red.jpg" }
                    }
                },
                content = "@here World boss alert!"
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(webhookUrl, content);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("✓ Discord message sent successfully");
            }
            else
            {
                Console.WriteLine($"✗ Discord API error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending Discord message: {ex.Message}");
    }
}

DateTime UnixTimeStampToDateTime(long unixTimeStamp)
{
    var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
    return dateTime;
}
