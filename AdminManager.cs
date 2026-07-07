using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Token: 0x02000119 RID: 281
public class AdminManager : MonoBehaviourSingleton<AdminManager>
{
	// Token: 0x060007C0 RID: 1984 RVA: 0x0000DFB7 File Offset: 0x0000C1B7
	public override void Awake()
	{
		base.Awake();
		this.adminSteamIdsFilePath = (Utils.GetCommandLineArgument("--adminSteamIdsPath", null) ?? "./admin_steam_ids.json");
		this.adminSteamIdsFilePath = Path.GetFullPath(this.adminSteamIdsFilePath);
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0000DFEA File Offset: 0x0000C1EA
	public void Dispose()
	{
		this.adminSteamIds.Clear();
		if (this.adminSteamIdsWatcher != null)
		{
			this.adminSteamIdsWatcher.Dispose();
			this.adminSteamIdsWatcher = null;
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00033F74 File Offset: 0x00032174
	public void LoadAdminSteamIds()
	{
		if (!File.Exists(this.adminSteamIdsFilePath))
		{
			AdminManager.Logger.Warning("Admin steam ids file not found at " + this.adminSteamIdsFilePath + ", creating default...");
			File.AppendAllText(this.adminSteamIdsFilePath, JsonSerializer.Serialize<List<string>>(new List<string>(), new JsonSerializerOptions
			{
				WriteIndented = true
			}));
		}
		this.ReadAdminSteamIds();
		this.WatchAdminSteamIds(this.adminSteamIdsFilePath);
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0000E011 File Offset: 0x0000C211
	public void SaveAdminSteamIds()
	{
		File.WriteAllText(this.adminSteamIdsFilePath, JsonSerializer.Serialize<List<string>>(this.adminSteamIds, new JsonSerializerOptions
		{
			WriteIndented = true
		}));
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00033FE0 File Offset: 0x000321E0
	public void ReadAdminSteamIds()
	{
		string json = File.ReadAllText(this.adminSteamIdsFilePath);
		this.adminSteamIds = JsonSerializer.Deserialize<List<string>>(json, null);
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00034008 File Offset: 0x00032208
	public void WatchAdminSteamIds(string adminSteamIdsFilePath)
	{
		if (this.adminSteamIdsWatcher != null)
		{
			return;
		}
		string fileName = Path.GetFileName(adminSteamIdsFilePath);
		string path = adminSteamIdsFilePath.Replace(fileName, string.Empty);
		this.adminSteamIdsWatcher = new FileSystemWatcher(path);
		this.adminSteamIdsWatcher.NotifyFilter = NotifyFilters.LastWrite;
		this.adminSteamIdsWatcher.Filter = fileName;
		this.adminSteamIdsWatcher.EnableRaisingEvents = true;
		this.adminSteamIdsWatcher.Changed += this.OnAdminSteamIdsFileChanged;
		AdminManager.Logger.Info("Watching admin Steam IDs file " + fileName);
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00034090 File Offset: 0x00032290
	private void OnAdminSteamIdsFileChanged(object sender, FileSystemEventArgs e)
	{
		AdminManager.Logger.Info("Admin Steam IDs file changed: " + e.FullPath);
		string json = File.ReadAllText(e.FullPath);
		this.adminSteamIds = JsonSerializer.Deserialize<List<string>>(json, null);
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0000E035 File Offset: 0x0000C235
	public void AddAdminSteamId(string steamId)
	{
		if (this.adminSteamIds.Contains(steamId))
		{
			return;
		}
		this.adminSteamIds.Add(steamId);
		this.SaveAdminSteamIds();
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x0000E058 File Offset: 0x0000C258
	public void RemoveAdminSteamId(string steamId)
	{
		if (!this.adminSteamIds.Contains(steamId))
		{
			return;
		}
		this.adminSteamIds.Remove(steamId);
		this.SaveAdminSteamIds();
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0000E07C File Offset: 0x0000C27C
	public bool IsSteamIdAdmin(string steamId)
	{
		return this.adminSteamIds.Contains(steamId);
	}

	// Token: 0x040004BC RID: 1212
	private static readonly Logger Logger = new Logger("AdminManager");

	// Token: 0x040004BD RID: 1213
	private List<string> adminSteamIds = new List<string>();

	// Token: 0x040004BE RID: 1214
	private string adminSteamIdsFilePath;

	// Token: 0x040004BF RID: 1215
	private FileSystemWatcher adminSteamIdsWatcher;
}
