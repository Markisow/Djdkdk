using System;
using System.Collections.Generic;

// Token: 0x02000118 RID: 280
public static class SceneManagerController
{
	// Token: 0x060007BB RID: 1979 RVA: 0x00033EC4 File Offset: 0x000320C4
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(SceneManagerController.Event_Server_OnServerStarted));
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(SceneManagerController.Event_Server_OnServerStopped));
		EventManager.AddEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(SceneManagerController.Event_OnClientStopped));
		if (!ApplicationManager.IsDedicatedGameServer)
		{
			SceneManager.LoadScene("locker_room");
		}
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00033F24 File Offset: 0x00032124
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(SceneManagerController.Event_Server_OnServerStarted));
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(SceneManagerController.Event_Server_OnServerStopped));
		EventManager.RemoveEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(SceneManagerController.Event_OnClientStopped));
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0000DF72 File Offset: 0x0000C172
	private static void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		ServerConfig serverConfig = (ServerConfig)message["serverConfig"];
		SceneManager.InitializeServer();
		if (serverConfig.level == "default")
		{
			SceneManager.LoadScene("level_default");
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
	private static void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		SceneManager.DisposeServer();
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0000DFAB File Offset: 0x0000C1AB
	private static void Event_OnClientStopped(Dictionary<string, object> message)
	{
		SceneManager.LoadScene("locker_room");
	}
}
