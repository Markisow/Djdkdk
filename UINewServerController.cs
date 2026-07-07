using System;
using System.Collections.Generic;

// Token: 0x020001A4 RID: 420
public class UINewServerController : UIViewController<UINewServer>
{
	// Token: 0x06000C6B RID: 3179 RVA: 0x00012406 File Offset: 0x00010606
	public override void Awake()
	{
		base.Awake();
		this.uiNewServer = base.GetComponent<UINewServer>();
		EventManager.AddEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		WebSocketManager.AddMessageListener("playerGetLocationsResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerGetLocationsResponse));
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x00012446 File Offset: 0x00010646
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		WebSocketManager.RemoveMessageListener("playerGetLocationsResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerGetLocationsResponse));
		base.OnDestroy();
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x00044740 File Offset: 0x00042940
	private void Event_OnPlayerDataChanged(Dictionary<string, object> message)
	{
		PlayerData playerData = (PlayerData)message["newPlayerData"];
		if (playerData == null)
		{
			return;
		}
		if (playerData.patreonLevel >= 1)
		{
			this.uiNewServer.HidePatreonOverlay();
			return;
		}
		this.uiNewServer.ShowPatreonOverlay();
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00044784 File Offset: 0x00042984
	private void WebSocket_Event_OnPlayerGetLocationsResponse(Dictionary<string, object> message)
	{
		PlayerGetLocationsResponse data = ((InMessage)message["inMessage"]).GetData<PlayerGetLocationsResponse>();
		this.uiNewServer.SetDedicatedLocations(data.data.locations);
	}

	// Token: 0x0400076F RID: 1903
	private UINewServer uiNewServer;
}
