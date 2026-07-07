using System;
using System.Collections.Generic;

// Token: 0x02000165 RID: 357
public static class WebSocketManagerController
{
	// Token: 0x06000AAD RID: 2733 RVA: 0x00010E2B File Offset: 0x0000F02B
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(WebSocketManagerController.Event_OnSteamConnected));
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x00010E43 File Offset: 0x0000F043
	public static void Dispose()
	{
		WebSocketManager.Disconnect();
		EventManager.RemoveEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(WebSocketManagerController.Event_OnSteamConnected));
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x00010E61 File Offset: 0x0000F061
	private static void Event_OnSteamConnected(Dictionary<string, object> message)
	{
		if (WebSocketManager.IsConnected || WebSocketManager.IsConnectionInProgress || WebSocketManager.IsReconnecting)
		{
			return;
		}
		WebSocketManager.Connect("wss://puck1.nasejevs.com");
	}
}
