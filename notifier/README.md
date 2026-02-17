# Unitscan Notifier

A .NET 10 console application that monitors WoW SavedVariables and posts world-boss alerts to Discord.

## Setup

### 1. Configure Discord Webhook

1. In your Discord server, create or select a channel (e.g., `#world-bosses`)
2. Go to **Channel Settings** → **Integrations** → **Webhooks** → **New Webhook**
3. Copy the webhook URL (should look like `https://discord.com/api/webhooks/...`)

### 2. Update appsettings.json

Open `appsettings.json` and replace the placeholder:

```json
{
  "Discord": {
    "WebhookUrl": "https://discord.com/api/webhooks/YOUR_WEBHOOK_ID/YOUR_WEBHOOK_TOKEN"
  },
  "SavedVariablesPath": "C:\\Games\\TurtleWoW\\Interface\\AddOns\\unitscan-turtle-hc"
}
```

**SavedVariablesPath notes:**
- On Windows, this is typically: `C:\Users\<YourUsername>\AppData\Local\VirtualStore\Games\TurtleWoW\Interface\AddOns\unitscan-turtle-hc`
- Or if running without virtualization: `C:\Games\TurtleWoW\Interface\AddOns\unitscan-turtle-hc`
- The app watches for `unitscan-turtle-hc.lua` (the WoW SavedVariables file) in this directory

### 3. Build and Run

```bash
dotnet build
dotnet run
```

Or run the built executable:

```bash
bin\Debug\net10.0\UnitscanNotifier.exe
```

## How It Works

1. The notifier watches the `unitscan-turtle-hc.lua` SavedVariables file for changes
2. When a world-boss alert is recorded by the addon, the file is updated
3. The notifier detects the change, parses the new alert data (name, zone, timestamp)
4. An embedded Discord message is sent immediately to your configured webhook

## Message Format

The Discord message includes:
- **Title:** "🐉 World Boss Detected!"
- **Boss Name & Zone**
- **Timestamp** (local time)
- **@here mention** to alert the channel
- Red embed color for visibility

## Troubleshooting

- **"SavedVariables directory not found"**: Check the path in `appsettings.json`
- **"Discord webhook URL not configured"**: Ensure you've replaced the placeholder in `appsettings.json`
- **No alerts detected**: Verify the addon is loaded and at least one world-boss target is enabled in `/unitscan` chat command

## Notes

- The notifier must be running while WoW is open and the addon is active
- Run the `.exe` in the background (e.g., in a separate terminal or task scheduler)
- The app uses a small debounce (100ms) to avoid race conditions with file writes
