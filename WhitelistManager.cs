using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Token: 0x02000131 RID: 305
public class WhitelistManager : MonoBehaviourSingleton<WhitelistManager>
{
	// Token: 0x0600089E RID: 2206 RVA: 0x0000EC1B File Offset: 0x0000CE1B
	public override void Awake()
	{
		base.Awake();
		this.whitelistedSteamIdsFilePath = (Utils.GetCommandLineArgument("--whitelistedSteamIdsPath", null) ?? "./whitelisted_steam_ids.json");
		this.whitelistedSteamIdsFilePath = Path.GetFullPath(this.whitelistedSteamIdsFilePath);
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0000EC4E File Offset: 0x0000CE4E
	public void Dispose()
	{
		this.whitelistedSteamIds.Clear();
		if (this.whitelistedSteamIdsWatcher != null)
		{
			this.whitelistedSteamIdsWatcher.Dispose();
			this.whitelistedSteamIdsWatcher = null;
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x000362E4 File Offset: 0x000344E4
	public void LoadWhitelistedSteamIds()
	{
		if (!File.Exists(this.whitelistedSteamIdsFilePath))
		{
			WhitelistManager.Logger.Warning("Whitelisted Steam IDs file not found at " + this.whitelistedSteamIdsFilePath + ", creating default...");
			File.AppendAllText(this.whitelistedSteamIdsFilePath, JsonSerializer.Serialize<List<string>>(new List<string>(), new JsonSerializerOptions
			{
				WriteIndented = true
			}));
		}
		this.ReadWhitelistedSteamIds();
		this.WatchWhitelistedSteamIds(this.whitelistedSteamIdsFilePath);
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0000EC75 File Offset: 0x0000CE75
	public void SaveWhitelistedSteamIds()
	{
		File.WriteAllText(this.whitelistedSteamIdsFilePath, JsonSerializer.Serialize<List<string>>(this.whitelistedSteamIds, new JsonSerializerOptions
		{
			WriteIndented = true
		}));
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00036350 File Offset: 0x00034550
	public void ReadWhitelistedSteamIds()
	{
		string json = File.ReadAllText(this.whitelistedSteamIdsFilePath);
		this.whitelistedSteamIds = JsonSerializer.Deserialize<List<string>>(json, null);
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00036378 File Offset: 0x00034578
	public void WatchWhitelistedSteamIds(string whitelistedSteamIdsFilePath)
	{
		if (this.whitelistedSteamIdsWatcher != null)
		{
			return;
		}
		string fileName = Path.GetFileName(whitelistedSteamIdsFilePath);
		string path = whitelistedSteamIdsFilePath.Replace(fileName, string.Empty);
		this.whitelistedSteamIdsWatcher = new FileSystemWatcher(path);
		this.whitelistedSteamIdsWatcher.NotifyFilter = NotifyFilters.LastWrite;
		this.whitelistedSteamIdsWatcher.Filter = fileName;
		this.whitelistedSteamIdsWatcher.EnableRaisingEvents = true;
		this.whitelistedSteamIdsWatcher.Changed += this.OnWhitelistedSteamIdsFileChanged;
		WhitelistManager.Logger.Info("Watching whitelisted Steam IDs file " + fileName);
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0000EC99 File Offset: 0x0000CE99
	public void AddWhitelistedSteamId(string steamId)
	{
		if (this.whitelistedSteamIds.Contains(steamId))
		{
			return;
		}
		this.whitelistedSteamIds.Add(steamId);
		this.SaveWhitelistedSteamIds();
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00036400 File Offset: 0x00034600
	public void AddWhitelistedSteamIds(params string[] steamIds)
	{
		bool flag = false;
		foreach (string item in steamIds)
		{
			if (!this.whitelistedSteamIds.Contains(item))
			{
				this.whitelistedSteamIds.Add(item);
				flag = true;
			}
		}
		if (flag)
		{
			this.SaveWhitelistedSteamIds();
		}
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0000ECBC File Offset: 0x0000CEBC
	public void RemoveWhitelistedSteamId(string steamId)
	{
		if (!this.whitelistedSteamIds.Contains(steamId))
		{
			return;
		}
		this.whitelistedSteamIds.Remove(steamId);
		this.SaveWhitelistedSteamIds();
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00036448 File Offset: 0x00034648
	public void RemoveWhitelistedSteamIds(params string[] steamIds)
	{
		bool flag = false;
		foreach (string item in steamIds)
		{
			if (this.whitelistedSteamIds.Contains(item))
			{
				this.whitelistedSteamIds.Remove(item);
				flag = true;
			}
		}
		if (flag)
		{
			this.SaveWhitelistedSteamIds();
		}
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
	public bool IsSteamIdWhitelisted(string steamId)
	{
		return this.whitelistedSteamIds.Contains(steamId);
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00036494 File Offset: 0x00034694
	private void OnWhitelistedSteamIdsFileChanged(object sender, FileSystemEventArgs e)
	{
		WhitelistManager.Logger.Info("Whitelisted Steam IDs file changed: " + e.FullPath);
		string json = File.ReadAllText(e.FullPath);
		this.whitelistedSteamIds = JsonSerializer.Deserialize<List<string>>(json, null);
	}

	// Token: 0x0400051F RID: 1311
	private static readonly Logger Logger = new Logger("WhitelistManager");

	// Token: 0x04000520 RID: 1312
	private List<string> whitelistedSteamIds = new List<string>();

	// Token: 0x04000521 RID: 1313
	private string whitelistedSteamIdsFilePath;

	// Token: 0x04000522 RID: 1314
	private FileSystemWatcher whitelistedSteamIdsWatcher;
}
