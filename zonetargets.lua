function unitscan_zone_targets()
	local zone = GetZoneText()
	if not zone then return end

    unitscan_zonetargets = {} -- reset zonetargets
    local t = unitscan_zonetargets
    
	if zone == "Alterac Mountains" then
        -- t["Araga"] = true

    elseif zone == "Arathi Highlands" then
        -- t["Boulderfist Lord"] = true

    elseif zone == "Ashenvale" then
        t["Taerar"] = true
        -- t["Apothecary Falthis"] = true

    elseif zone == "Azshara" then
        -- t["Antilos"] = true
        t["Azuregos"] = true

    elseif zone == "Badlands" then
        -- t["7:XT"] = true

    elseif zone == "Blackrock Depths" then
        -- t["Panzor the Invincible"] = true

    elseif zone == "Blackrock Spire" then
        -- t["Bannok Grimaxe"] = true

    elseif zone == "Blasted Lands" then
        -- t["Akubar the Seer"] = true

    elseif zone == "Burning Steppes" then
        -- t["Black Dragonspawn"] = true

    elseif zone == "Darkshore" then
        -- t["Carnivous the Breaker"] = true

    elseif zone == "Desolace" then
        -- t["Accursed Slitherblade"] = true

    elseif zone == "Dun Morogh" then
        -- t["Ben"] = true

    elseif zone == "Durotar" then
        -- t["Captain Flat Tusk"] = true

    elseif zone == "Duskwood" then
        t["Emeriss"] = true
        -- t["Eliza"] = true

    elseif zone == "Dustwallow Marsh" then
        -- t["Brimgore"] = true
        
    elseif zone == "Eastern Plaguelands" then
        -- t["Bloodletter"] = true

    elseif zone == "Elwynn Forest" then
        -- t["Fedfennel"] = true

    elseif zone == "Felwood" then
        -- t["Alshirr Banebreath"] = true
        
    elseif zone == "Feralas" then
        t["Lethon"] = true
        -- t["Arash-ethis"] = true

    elseif zone == "Gnomeregan" then
        -- t["Dark Iron Ambassador"] = true

    elseif zone == "Hillsbrad Foothills" then
        -- t["Big Samras"] = true

    elseif zone == "Lapidis Isle" then
        -- t["Twisted Water Elemental"] = true

    elseif zone == "Ironforge" then
        -- t["Spectral Stalker"] = true

    elseif zone == "Loch Modan" then
        --     t["Boss Galgosh"] = true

    elseif zone == "Maraudon" then
		--     t["Meshlok the Harvester"] = true

    elseif zone == "Mulgore" then
		--     t["Enforcer Emilgund"] = true

    elseif zone == "Northwind" then
		--     t["Dul Wolfsnarl"] = true

    elseif zone == "Orgrimmar" then
		--     t["Spectral Stalker"] = true        

    elseif zone == "Razorfen Kraul" then
		--     t["Blind Hunter"] = true

    elseif zone == "Redridge Mountains" then
		--     t["Blackrock Gladiator"] = true

    elseif zone == "Scarlet Monastery" then
		--     t["Azshir the Sleepless"] = true

    elseif zone == "Searing Gorge" then
		--     t["Clunk"] = true


    elseif zone == "Shadowfang Keep" then        
		--     t["Deathsworn Captain"] = true

    elseif zone == "Silithus" then
		--     t["Aluntir"] = true

    elseif zone == "Silverpine Forest" then
		--     t["Apothecary Berard"] = true

    elseif zone == "Stonetalon Mountains" then
		--     t["Besseleth"] = true

    elseif zone == "Stormwind City" then 
		--     t["Onyxia's Elite Guard"] = true

    elseif zone == "Stranglethorn Vale" then
		--     t["Bhag'thera"] = true

    elseif zone == "Stratholme" then
		--     t["Hearthsinger Forresten"] = true

    elseif zone == "Swamp of Sorrows" then
		--     t["Dark Rider"] = true

    elseif zone == "Tanaris" then
		--     t["Bone Witch"] = true

    elseif zone == "Teldrassil" then
		--     t["Blackmoss the Fetid"] = true

    elseif zone == "Temple of Atal'Hakkar" then
		--     t["Veyzhak the Cannibal"] = true

    elseif zone == "The Barrens" then
		--     t["Aean Swiftriver"] = true

    elseif zone == "The Deadmines" then
		--     t["Miner Johnson"] = true

    elseif zone == "The Hinterlands" then
		t["Ysondre"] = true
		--     t["Emerald Ooze"] = true

    elseif zone == "The Stockade" then
    --     t["Bruegal Ironknuckle"] = true

    elseif zone == "Thousand Needles" then
    --     t["Achellios the Banished"] = true

    elseif zone == "Thunder Bluff" then
    --     t["Ghost Howl"] = true

    elseif zone == "Tirisfal Glades" then
    --     t["Bayne"] = true

    elseif zone == "Un'Goro Crater" then
    --     t["Baron Charr"] = true

    elseif zone == "Wailing Caverns" then
    --     t["Deviate Faerie Dragon"] = true


    elseif zone == "Western Plaguelands" then
    --     t["Araj the Summoner"] = true


    elseif zone == "Westfall" then
    --     t["Benny Blaanco"] = true

    elseif zone == "Wetlands" then
    --     t["Axtroz"] = true

    elseif zone == "Winterspring" then
    --     t["Azurous"] = true

    elseif zone == "Zul'Farrak" then
    --     t["Dustwraith"] = true
				
	elseif zone == "Gilneas" then
    --     t["Baron Perenolde"] = true

	elseif zone == "Tel'Abim" then
    --     t["Grimeclaw"] = true

    end
end
