using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x02000085 RID: 133
public static class ApplicationManagerController
{
	// Token: 0x06000497 RID: 1175 RVA: 0x00029664 File Offset: 0x00027864
	public static Task Initialize()
	{
		ApplicationManagerController.<Initialize>d__0 <Initialize>d__;
		<Initialize>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<Initialize>d__.<>1__state = -1;
		<Initialize>d__.<>t__builder.Start<ApplicationManagerController.<Initialize>d__0>(ref <Initialize>d__);
		return <Initialize>d__.<>t__builder.Task;
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x000296A0 File Offset: 0x000278A0
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_OnFullScreenModeChanged", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnFullScreenModeChanged));
		EventManager.RemoveEventListener("Event_OnDisplayIndexChanged", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnDisplayIndexChanged));
		EventManager.RemoveEventListener("Event_OnResolutionIndexChanged", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnResolutionIndexChanged));
		EventManager.RemoveEventListener("Event_OnVSyncChanged", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnVSyncChanged));
		EventManager.RemoveEventListener("Event_OnFpsLimitChanged", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnFpsLimitChanged));
		EventManager.RemoveEventListener("Event_OnQualityChanged", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnQualityChanged));
		EventManager.RemoveEventListener("Event_OnUIStateChanged", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnUIStateChanged));
		EventManager.RemoveEventListener("Event_OnSocialClickDiscord", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnSocialClickDiscord));
		EventManager.RemoveEventListener("Event_OnSocialClickPatreon", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnSocialClickPatreon));
		EventManager.RemoveEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_OnPopupClickOk));
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(ApplicationManagerController.Event_Server_OnServerStarted));
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x000297A0 File Offset: 0x000279A0
	private static void Event_OnFullScreenModeChanged(Dictionary<string, object> message)
	{
		FullScreenMode fullScreenMode = (FullScreenMode)message["value"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		ApplicationManager.SetFullScreenMode(fullScreenMode);
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x000297CC File Offset: 0x000279CC
	private static void Event_OnDisplayIndexChanged(Dictionary<string, object> message)
	{
		ApplicationManagerController.<Event_OnDisplayIndexChanged>d__3 <Event_OnDisplayIndexChanged>d__;
		<Event_OnDisplayIndexChanged>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<Event_OnDisplayIndexChanged>d__.message = message;
		<Event_OnDisplayIndexChanged>d__.<>1__state = -1;
		<Event_OnDisplayIndexChanged>d__.<>t__builder.Start<ApplicationManagerController.<Event_OnDisplayIndexChanged>d__3>(ref <Event_OnDisplayIndexChanged>d__);
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00029804 File Offset: 0x00027A04
	private static void Event_OnResolutionIndexChanged(Dictionary<string, object> message)
	{
		int resolution = (int)message["value"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		ApplicationManager.SetResolution(resolution);
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00029830 File Offset: 0x00027A30
	private static void Event_OnVSyncChanged(Dictionary<string, object> message)
	{
		bool vsync = (bool)message["value"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		ApplicationManager.SetVSync(vsync);
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0002985C File Offset: 0x00027A5C
	private static void Event_OnFpsLimitChanged(Dictionary<string, object> message)
	{
		int targetFrameRate = (int)message["value"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		ApplicationManager.SetTargetFrameRate(targetFrameRate);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00029888 File Offset: 0x00027A88
	private static void Event_OnQualityChanged(Dictionary<string, object> message)
	{
		ApplicationQuality quality = (ApplicationQuality)message["value"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		ApplicationManager.SetQuality(quality);
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x000298B4 File Offset: 0x00027AB4
	private static void Event_OnUIStateChanged(Dictionary<string, object> message)
	{
		UIState uistate = (UIState)message["oldUIState"];
		UIState uistate2 = (UIState)message["newUIState"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		if (uistate.IsMouseRequired == uistate2.IsMouseRequired)
		{
			return;
		}
		ApplicationManager.SetMouseVisibility(uistate2.IsMouseRequired);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0000BC6D File Offset: 0x00009E6D
	private static void Event_OnSocialClickDiscord(Dictionary<string, object> message)
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		Application.OpenURL("https://discord.gg/AZDBj6XsGg");
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0000BC81 File Offset: 0x00009E81
	private static void Event_OnSocialClickPatreon(Dictionary<string, object> message)
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		Application.OpenURL("https://www.patreon.com/c/PuckGame");
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00029908 File Offset: 0x00027B08
	private static void Event_OnPopupClickOk(Dictionary<string, object> message)
	{
		string name = ((Popup)message["popup"]).Name;
		if (name == "mainMenuExitGame")
		{
			Application.Quit();
			return;
		}
		if (!(name == "pauseMenuExitGame"))
		{
			return;
		}
		Application.Quit();
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00029954 File Offset: 0x00027B54
	private static void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		ServerConfig serverConfig = (ServerConfig)message["serverConfig"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			ApplicationManager.SetTargetFrameRate(serverConfig.tickRate);
		}
	}
}
