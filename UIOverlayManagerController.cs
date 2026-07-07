using System;
using System.Collections.Generic;
using Unity.Netcode;

// Token: 0x020001A8 RID: 424
internal class UIOverlayManagerController : UIViewController<UIOverlayManager>
{
	// Token: 0x06000C81 RID: 3201 RVA: 0x00044BDC File Offset: 0x00042DDC
	public override void Awake()
	{
		base.Awake();
		this.uiOverlay = base.GetComponent<UIOverlayManager>();
		EventManager.AddEventListener("Event_Everyone_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientConnected));
		EventManager.AddEventListener("Event_OnBaseCameraEnabled", new Action<Dictionary<string, object>>(this.Event_OnBaseCameraEnabled));
		EventManager.AddEventListener("Event_OnBaseCameraDisabled", new Action<Dictionary<string, object>>(this.Event_OnBaseCameraDisabled));
		EventManager.AddEventListener("Event_OnClientStarted", new Action<Dictionary<string, object>>(this.Event_OnClientStarted));
		EventManager.AddEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.AddEventListener("Event_OnPopupShow", new Action<Dictionary<string, object>>(this.Event_OnPopupShow));
		EventManager.AddEventListener("Event_OnPopupHide", new Action<Dictionary<string, object>>(this.Event_OnPopupHide));
		WebSocketManager.AddMessageListener("playerData", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerData));
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x000125DD File Offset: 0x000107DD
	private void Start()
	{
		this.uiOverlay.ShowOverlay("loading", true, false, true, 0.25f, false, 0.25f);
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x00044CAC File Offset: 0x00042EAC
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientConnected));
		EventManager.RemoveEventListener("Event_OnBaseCameraEnabled", new Action<Dictionary<string, object>>(this.Event_OnBaseCameraEnabled));
		EventManager.RemoveEventListener("Event_OnBaseCameraDisabled", new Action<Dictionary<string, object>>(this.Event_OnBaseCameraDisabled));
		EventManager.RemoveEventListener("Event_OnClientStarted", new Action<Dictionary<string, object>>(this.Event_OnClientStarted));
		EventManager.RemoveEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.RemoveEventListener("Event_OnPopupShow", new Action<Dictionary<string, object>>(this.Event_OnPopupShow));
		EventManager.RemoveEventListener("Event_OnPopupHide", new Action<Dictionary<string, object>>(this.Event_OnPopupHide));
		WebSocketManager.RemoveMessageListener("playerData", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerData));
		base.OnDestroy();
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x00044D70 File Offset: 0x00042F70
	private void Event_Everyone_OnClientConnected(Dictionary<string, object> message)
	{
		ulong num = (ulong)message["clientId"];
		if (NetworkManager.Singleton.LocalClientId != num)
		{
			return;
		}
		this.uiOverlay.HideOverlay("connecting");
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x000125FD File Offset: 0x000107FD
	private void Event_OnBaseCameraEnabled(Dictionary<string, object> message)
	{
		this.uiOverlay.HideOverlay("camera");
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0001260F File Offset: 0x0001080F
	private void Event_OnBaseCameraDisabled(Dictionary<string, object> message)
	{
		this.uiOverlay.ShowOverlay("camera", false, false, true, 0.25f, false, 0.25f);
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0001262F File Offset: 0x0001082F
	private void Event_OnClientStarted(Dictionary<string, object> message)
	{
		this.uiOverlay.ShowOverlay("connecting", true, false, true, 0.25f, false, 0.25f);
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0001264F File Offset: 0x0001084F
	private void Event_OnClientStopped(Dictionary<string, object> message)
	{
		this.uiOverlay.HideOverlay("connecting");
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x00044DAC File Offset: 0x00042FAC
	private void Event_OnPopupShow(Dictionary<string, object> message)
	{
		string a = (string)message["name"];
		if (a == "missingPassword")
		{
			this.uiOverlay.ShowOverlay("missingPassword", false, false, true, 0.25f, false, 0.25f);
			return;
		}
		if (a == "missingMods")
		{
			this.uiOverlay.ShowOverlay("missingMods", false, false, true, 0.25f, false, 0.25f);
			return;
		}
		if (!(a == "downloadingMods"))
		{
			return;
		}
		this.uiOverlay.ShowOverlay("downloadingMods", true, false, true, 0.25f, false, 0.25f);
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x00044E50 File Offset: 0x00043050
	private void Event_OnPopupHide(Dictionary<string, object> message)
	{
		string a = (string)message["name"];
		if (a == "missingPassword")
		{
			this.uiOverlay.HideOverlay("missingPassword");
			return;
		}
		if (a == "missingMods")
		{
			this.uiOverlay.HideOverlay("missingMods");
			return;
		}
		if (!(a == "downloadingMods"))
		{
			return;
		}
		this.uiOverlay.HideOverlay("downloadingMods");
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00012661 File Offset: 0x00010861
	private void WebSocket_Event_OnPlayerData(Dictionary<string, object> message)
	{
		this.uiOverlay.HideOverlay("loading");
	}

	// Token: 0x04000786 RID: 1926
	private UIOverlayManager uiOverlay;
}
