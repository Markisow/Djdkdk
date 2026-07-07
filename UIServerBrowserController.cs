using System;
using System.Collections.Generic;

// Token: 0x020001CD RID: 461
public class UIServerBrowserController : UIViewController<UIServerBrowser>
{
	// Token: 0x06000D75 RID: 3445 RVA: 0x0004850C File Offset: 0x0004670C
	public override void Awake()
	{
		base.Awake();
		this.uiServerBrowser = base.GetComponent<UIServerBrowser>();
		EventManager.AddEventListener("Event_OnServerBrowserShow", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserShow));
		EventManager.AddEventListener("Event_OnServerBrowserClickRefresh", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickRefresh));
		WebSocketManager.AddMessageListener("playerGetServerBrowserEndPointsResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerGetServerBrowserEndPointsResponse));
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00048570 File Offset: 0x00046770
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnServerBrowserShow", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserShow));
		EventManager.RemoveEventListener("Event_OnServerBrowserClickRefresh", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickRefresh));
		WebSocketManager.RemoveMessageListener("playerGetServerBrowserEndPointsResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerGetServerBrowserEndPointsResponse));
		base.OnDestroy();
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x00012FBA File Offset: 0x000111BA
	private void Event_OnServerBrowserShow(Dictionary<string, object> message)
	{
		this.uiServerBrowser.HideFilters();
		if (this.uiServerBrowser.ServerCount == 0)
		{
			this.uiServerBrowser.Refresh();
		}
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00012FDF File Offset: 0x000111DF
	private void Event_OnServerBrowserClickRefresh(Dictionary<string, object> message)
	{
		this.uiServerBrowser.Refresh();
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x000485C8 File Offset: 0x000467C8
	private void WebSocket_Event_OnPlayerGetServerBrowserEndPointsResponse(Dictionary<string, object> message)
	{
		ServerBrowserEndPointsResponse data = ((InMessage)message["inMessage"]).GetData<ServerBrowserEndPointsResponse>();
		List<EndPoint> list = new List<EndPoint>(data.data.endPoints);
		if (data.success)
		{
			this.uiServerBrowser.UpdateEndPoints(list.ToArray());
		}
	}

	// Token: 0x04000821 RID: 2081
	private UIServerBrowser uiServerBrowser;
}
