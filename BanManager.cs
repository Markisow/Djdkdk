using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Token: 0x0200011B RID: 283
public class BanManager : MonoBehaviourSingleton<BanManager>
{
	// Token: 0x060007D2 RID: 2002 RVA: 0x000340D0 File Offset: 0x000322D0
	public override void Awake()
	{
		base.Awake();
		this.bannedSteamIdsFilePath = (Utils.GetCommandLineArgument("--bannedSteamIdsPath", null) ?? "./banned_steam_ids.json");
		this.bannedSteamIdsFilePath = Path.GetFullPath(this.bannedSteamIdsFilePath);
		this.bannedIpAddressesFilePath = (Utils.GetCommandLineArgument("--bannedIpAddressesPath", null) ?? "./banned_ip_addresses.json");
		this.bannedIpAddressesFilePath = Path.GetFullPath(this.bannedIpAddressesFilePath);
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0003413C File Offset: 0x0003233C
	public void Dispose()
	{
		this.bannedSteamIds.Clear();
		if (this.bannedSteamIdsWatcher != null)
		{
			this.bannedSteamIdsWatcher.Dispose();
			this.bannedSteamIdsWatcher = null;
		}
		this.bannedIpAddresses.Clear();
		if (this.bannedIpAddressesWatcher != null)
		{
			this.bannedIpAddressesWatcher.Dispose();
			this.bannedIpAddressesWatcher = null;
		}
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00034194 File Offset: 0x00032394
	public void LoadBannedSteamIds()
	{
		if (!File.Exists(this.bannedSteamIdsFilePath))
		{
			BanManager.Logger.Warning("Banned steam ids file not found at " + this.bannedSteamIdsFilePath + ", creating default...");
			File.AppendAllText(this.bannedSteamIdsFilePath, JsonSerializer.Serialize<List<string>>(new List<string>(), new JsonSerializerOptions
			{
				WriteIndented = true
			}));
		}
		this.ReadBannedSteamIds();
		this.WatchBannedSteamIds(this.bannedSteamIdsFilePath);
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x0000E130 File Offset: 0x0000C330
	public void SaveBannedSteamIds()
	{
		File.WriteAllText(this.bannedSteamIdsFilePath, JsonSerializer.Serialize<List<string>>(this.bannedSteamIds, new JsonSerializerOptions
		{
			WriteIndented = true
		}));
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00034200 File Offset: 0x00032400
	public void ReadBannedSteamIds()
	{
		string json = File.ReadAllText(this.bannedSteamIdsFilePath);
		this.bannedSteamIds = JsonSerializer.Deserialize<List<string>>(json, null);
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00034228 File Offset: 0x00032428
	public void WatchBannedSteamIds(string bannedSteamIdsFilePath)
	{
		if (this.bannedSteamIdsWatcher != null)
		{
			return;
		}
		string fileName = Path.GetFileName(bannedSteamIdsFilePath);
		string path = bannedSteamIdsFilePath.Replace(fileName, string.Empty);
		this.bannedSteamIdsWatcher = new FileSystemWatcher(path);
		this.bannedSteamIdsWatcher.NotifyFilter = NotifyFilters.LastWrite;
		this.bannedSteamIdsWatcher.Filter = fileName;
		this.bannedSteamIdsWatcher.EnableRaisingEvents = true;
		this.bannedSteamIdsWatcher.Changed += this.OnBannedSteamIdsFileChanged;
		BanManager.Logger.Info("Watching banned Steam IDs file " + fileName);
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0000E154 File Offset: 0x0000C354
	public void AddBannedSteamId(string steamId)
	{
		if (this.bannedSteamIds.Contains(steamId))
		{
			return;
		}
		this.bannedSteamIds.Add(steamId);
		this.SaveBannedSteamIds();
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x0000E177 File Offset: 0x0000C377
	public void RemoveBannedSteamId(string steamId)
	{
		if (!this.bannedSteamIds.Contains(steamId))
		{
			return;
		}
		this.bannedSteamIds.Remove(steamId);
		this.SaveBannedSteamIds();
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0000E19B File Offset: 0x0000C39B
	public bool IsSteamIdBanned(string steamId)
	{
		return this.bannedSteamIds.Contains(steamId);
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x000342B0 File Offset: 0x000324B0
	public void LoadBannedIpAddresses()
	{
		if (!File.Exists(this.bannedIpAddressesFilePath))
		{
			BanManager.Logger.Warning("Banned IP addresses file not found at " + this.bannedIpAddressesFilePath + ", creating default...");
			File.AppendAllText(this.bannedIpAddressesFilePath, JsonSerializer.Serialize<List<string>>(new List<string>(), new JsonSerializerOptions
			{
				WriteIndented = true
			}));
		}
		this.ReadBannedIpAddresses();
		this.WatchBannedIpAddresses(this.bannedIpAddressesFilePath);
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x0000E1A9 File Offset: 0x0000C3A9
	public void SaveBannedIpAddresses()
	{
		File.WriteAllText(this.bannedIpAddressesFilePath, JsonSerializer.Serialize<List<string>>(this.bannedIpAddresses, new JsonSerializerOptions
		{
			WriteIndented = true
		}));
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x0003431C File Offset: 0x0003251C
	public void ReadBannedIpAddresses()
	{
		string json = File.ReadAllText(this.bannedIpAddressesFilePath);
		this.bannedIpAddresses = JsonSerializer.Deserialize<List<string>>(json, null);
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00034344 File Offset: 0x00032544
	public void WatchBannedIpAddresses(string bannedIpAddressesFilePath)
	{
		if (this.bannedIpAddressesWatcher != null)
		{
			return;
		}
		string fileName = Path.GetFileName(bannedIpAddressesFilePath);
		string path = bannedIpAddressesFilePath.Replace(fileName, string.Empty);
		this.bannedIpAddressesWatcher = new FileSystemWatcher(path);
		this.bannedIpAddressesWatcher.NotifyFilter = NotifyFilters.LastWrite;
		this.bannedIpAddressesWatcher.Filter = fileName;
		this.bannedIpAddressesWatcher.Changed += this.OnBannedIpAddressesFileChanged;
		this.bannedIpAddressesWatcher.EnableRaisingEvents = true;
		BanManager.Logger.Info("Watching banned IP addresses file " + fileName);
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0000E1CD File Offset: 0x0000C3CD
	public void AddBannedIpAddress(string ipAddress)
	{
		if (this.bannedIpAddresses.Contains(ipAddress))
		{
			return;
		}
		this.bannedIpAddresses.Add(ipAddress);
		this.SaveBannedIpAddresses();
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
	public void RemoveBannedIpAddress(string ipAddress)
	{
		if (!this.bannedIpAddresses.Contains(ipAddress))
		{
			return;
		}
		this.bannedIpAddresses.Remove(ipAddress);
		this.SaveBannedIpAddresses();
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x0000E214 File Offset: 0x0000C414
	public bool IsIpAddressBanned(string ipAddress)
	{
		return this.bannedIpAddresses.Contains(ipAddress);
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x000343CC File Offset: 0x000325CC
	private void OnBannedSteamIdsFileChanged(object sender, FileSystemEventArgs e)
	{
		BanManager.Logger.Info("Banned Steam IDs file changed: " + e.FullPath);
		string json = File.ReadAllText(e.FullPath);
		this.bannedSteamIds = JsonSerializer.Deserialize<List<string>>(json, null);
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0003440C File Offset: 0x0003260C
	private void OnBannedIpAddressesFileChanged(object sender, FileSystemEventArgs e)
	{
		BanManager.Logger.Info("Banned IP addresses file changed: " + e.FullPath);
		string json = File.ReadAllText(e.FullPath);
		this.bannedIpAddresses = JsonSerializer.Deserialize<List<string>>(json, null);
	}

	// Token: 0x040004C1 RID: 1217
	private static readonly Logger Logger = new Logger("BanManager");

	// Token: 0x040004C2 RID: 1218
	private List<string> bannedSteamIds = new List<string>();

	// Token: 0x040004C3 RID: 1219
	private string bannedSteamIdsFilePath;

	// Token: 0x040004C4 RID: 1220
	private FileSystemWatcher bannedSteamIdsWatcher;

	// Token: 0x040004C5 RID: 1221
	private List<string> bannedIpAddresses = new List<string>();

	// Token: 0x040004C6 RID: 1222
	private string bannedIpAddressesFilePath;

	// Token: 0x040004C7 RID: 1223
	private FileSystemWatcher bannedIpAddressesWatcher;
}
