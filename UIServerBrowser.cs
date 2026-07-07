using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001C5 RID: 453
public class UIServerBrowser : UIView
{
	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00012DBA File Offset: 0x00010FBA
	public int ServerCount
	{
		get
		{
			return this.endPointVisualElementMap.Count;
		}
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x000472F8 File Offset: 0x000454F8
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("ServerBrowserView", null);
		this.serverBrowser = base.View.Query("ServerBrowser", null);
		this.serverList = this.serverBrowser.Query("ServerList", null);
		this.filters = base.View.Query("Filters", null);
		this.closeIconButton = this.serverBrowser.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnServerBrowserClickClose;
		this.nameButton = this.serverBrowser.Query("NameButton", null);
		this.nameButton.clicked += this.OnClickNameSort;
		this.playersButton = this.serverBrowser.Query("PlayersButton", null);
		this.playersButton.clicked += this.OnClickPlayersSort;
		this.pingButton = this.serverBrowser.Query("PingButton", null);
		this.pingButton.clicked += this.OnClickPingSort;
		this.refreshButton = this.serverBrowser.Query("RefreshButton", null);
		this.refreshButton.clicked += this.OnClickRefresh;
		this.newServerButton = this.serverBrowser.Query("NewServerButton", null);
		this.newServerButton.clicked += this.OnClickNewServer;
		this.filtersButton = this.serverBrowser.Query("FiltersButton", null);
		this.filtersButton.clicked += this.OnClickFilters;
		this.filtersCloseIconButton = this.filters.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.filtersCloseIconButton.clicked += this.OnFiltersClickClose;
		this.searchTextField = this.filters.Query("SearchTextField", null).First().Query(null, null);
		this.searchTextField.value = string.Empty;
		this.searchTextField.RegisterCallback<ChangeEvent<string>>(new EventCallback<ChangeEvent<string>>(this.OnChangeSearchTextField), TrickleDown.NoTrickleDown);
		this.maxPingTextField = this.filters.Query("MaxPingIntegerField", null).First().Query(null, null);
		this.maxPingTextField.value = 100;
		this.maxPingTextField.RegisterCallback<ChangeEvent<int>>(new EventCallback<ChangeEvent<int>>(this.OnChangeMaxPingTextField), TrickleDown.NoTrickleDown);
		this.showFullToggle = this.filters.Query("ShowFullToggle", null).First().Query(null, null);
		this.showFullToggle.value = true;
		this.showFullToggle.RegisterCallback<ChangeEvent<bool>>(new EventCallback<ChangeEvent<bool>>(this.OnChangeShowFullToggle), TrickleDown.NoTrickleDown);
		this.showEmptyToggle = this.filters.Query("ShowEmptyToggle", null).First().Query(null, null);
		this.showEmptyToggle.value = true;
		this.showEmptyToggle.RegisterCallback<ChangeEvent<bool>>(new EventCallback<ChangeEvent<bool>>(this.OnChangeShowEmptyToggle), TrickleDown.NoTrickleDown);
		this.showPasswordProtectedToggle = this.filters.Query("ShowPasswordProtectedToggle", null).First().Query(null, null);
		this.showPasswordProtectedToggle.value = true;
		this.showPasswordProtectedToggle.RegisterCallback<ChangeEvent<bool>>(new EventCallback<ChangeEvent<bool>>(this.OnChangeShowPasswordProtectedToggle), TrickleDown.NoTrickleDown);
		this.showModdedToggle = this.filters.Query("ShowModdedToggle", null).First().Query(null, null);
		this.showModdedToggle.value = true;
		this.showModdedToggle.RegisterCallback<ChangeEvent<bool>>(new EventCallback<ChangeEvent<bool>>(this.OnChangeShowModdedToggle), TrickleDown.NoTrickleDown);
		this.showUnreachableToggle = this.filters.Query("ShowUnreachableToggle", null).First().Query(null, null);
		this.showUnreachableToggle.value = false;
		this.showUnreachableToggle.RegisterCallback<ChangeEvent<bool>>(new EventCallback<ChangeEvent<bool>>(this.OnChangeShowUnreachableToggle), TrickleDown.NoTrickleDown);
		this.serverList.Clear();
		this.StyleSortButtons();
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x00012DC7 File Offset: 0x00010FC7
	public override bool Show()
	{
		bool flag = base.Show();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnServerBrowserShow", null);
		}
		return flag;
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x00012DDD File Offset: 0x00010FDD
	public void Refresh()
	{
		WebSocketManager.Emit("playerGetServerBrowserEndPointsRequest", null, "playerGetServerBrowserEndPointsResponse");
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x00047768 File Offset: 0x00045968
	public void UpdateEndPoints(EndPoint[] endPoints)
	{
		UIServerBrowser.<>c__DisplayClass28_0 CS$<>8__locals1 = new UIServerBrowser.<>c__DisplayClass28_0();
		CS$<>8__locals1.endPoints = endPoints;
		CS$<>8__locals1.<>4__this = this;
		this.RemoveAllServers();
		this.refreshButton.SetEnabled(false);
		foreach (EndPoint endPoint in CS$<>8__locals1.endPoints)
		{
			this.AddServer(endPoint);
		}
		this.FilterServers();
		this.SortServers();
		Task.Run(delegate()
		{
			UIServerBrowser.<>c__DisplayClass28_0.<<UpdateEndPoints>b__0>d <<UpdateEndPoints>b__0>d;
			<<UpdateEndPoints>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<UpdateEndPoints>b__0>d.<>4__this = CS$<>8__locals1;
			<<UpdateEndPoints>b__0>d.<>1__state = -1;
			<<UpdateEndPoints>b__0>d.<>t__builder.Start<UIServerBrowser.<>c__DisplayClass28_0.<<UpdateEndPoints>b__0>d>(ref <<UpdateEndPoints>b__0>d);
			return <<UpdateEndPoints>b__0>d.<>t__builder.Task;
		});
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x000477DC File Offset: 0x000459DC
	private void AddServer(EndPoint endPoint)
	{
		if (this.endPointVisualElementMap.ContainsKey(endPoint))
		{
			return;
		}
		VisualElement visualElement = this.serverAsset.Instantiate();
		visualElement.userData = new Dictionary<string, object>();
		VisualElement visualElement2 = visualElement.Query("Server", null);
		visualElement2.userData = new Dictionary<string, object>();
		visualElement2.Query(null, null).RegisterCallback<ClickEvent, EndPoint>(new EventCallback<ClickEvent, EndPoint>(this.OnClickServer), endPoint, TrickleDown.NoTrickleDown);
		this.endPointVisualElementMap.Add(endPoint, visualElement);
		this.serverList.Add(visualElement);
		this.StyleServer(endPoint);
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0004786C File Offset: 0x00045A6C
	private EndPoint GetServerEndPoint(VisualElement visualElement)
	{
		if (!this.endPointVisualElementMap.ContainsValue(visualElement))
		{
			return null;
		}
		return this.endPointVisualElementMap.FirstOrDefault((KeyValuePair<EndPoint, VisualElement> x) => x.Value == visualElement).Key;
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x000478BC File Offset: 0x00045ABC
	private void SetServerPreviewData(EndPoint endPoint, ServerPreviewData previewData)
	{
		if (!this.endPointVisualElementMap.ContainsKey(endPoint))
		{
			return;
		}
		(this.endPointVisualElementMap[endPoint].Query("Server", null).userData as Dictionary<string, object>)["previewData"] = previewData;
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0004790C File Offset: 0x00045B0C
	private ServerPreviewData GetServerPreviewData(EndPoint endPoint)
	{
		if (!this.endPointVisualElementMap.ContainsKey(endPoint))
		{
			return null;
		}
		return (this.endPointVisualElementMap[endPoint].Query("Server", null).userData as Dictionary<string, object>).GetValueOrDefault("previewData", null) as ServerPreviewData;
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x00047960 File Offset: 0x00045B60
	private void StyleServer(EndPoint endPoint)
	{
		if (!this.endPointVisualElementMap.ContainsKey(endPoint))
		{
			return;
		}
		VisualElement visualElement = this.endPointVisualElementMap[endPoint].Query("Server", null);
		ServerPreviewData serverPreviewData = this.GetServerPreviewData(endPoint);
		Label label = visualElement.Query("NameLabel", null);
		Label label2 = visualElement.Query("PlayersLabel", null);
		Label label3 = visualElement.Query("PingLabel", null);
		if (serverPreviewData == null)
		{
			visualElement.EnableInClassList("passwordProtected", false);
			visualElement.EnableInClassList("modded", false);
			visualElement.EnableInClassList("unreachable", true);
			label.text = endPoint.ToString();
			label2.text = "?";
			label3.text = "?";
			return;
		}
		visualElement.EnableInClassList("passwordProtected", serverPreviewData.isPasswordProtected);
		visualElement.EnableInClassList("modded", serverPreviewData.clientRequiredModIds.Length != 0);
		visualElement.EnableInClassList("unreachable", false);
		label.text = serverPreviewData.name;
		label2.text = string.Format("{0}/{1}", serverPreviewData.players, serverPreviewData.maxPlayers);
		label3.text = string.Format("{0}ms", serverPreviewData.ping);
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x00047AA4 File Offset: 0x00045CA4
	private void RemoveServer(EndPoint endPoint)
	{
		if (!this.endPointVisualElementMap.ContainsKey(endPoint))
		{
			return;
		}
		this.endPointVisualElementMap[endPoint].Query("Server", null).Query(null, null).UnregisterCallback<ClickEvent, EndPoint>(new EventCallback<ClickEvent, EndPoint>(this.OnClickServer), TrickleDown.NoTrickleDown);
		this.serverList.Remove(this.endPointVisualElementMap[endPoint]);
		this.endPointVisualElementMap.Remove(endPoint);
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x00047B20 File Offset: 0x00045D20
	private void RemoveAllServers()
	{
		foreach (EndPoint endPoint in this.endPointVisualElementMap.Keys.ToList<EndPoint>())
		{
			this.RemoveServer(endPoint);
		}
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x00047B80 File Offset: 0x00045D80
	private void StyleSortButtons()
	{
		this.nameButton.text = ((this.sortType == ServerSortType.Name) ? ((this.sortDirection == ServerSortDirection.Ascending) ? "▼ NAME" : "▲ NAME") : "NAME");
		this.playersButton.text = ((this.sortType == ServerSortType.Players) ? ((this.sortDirection == ServerSortDirection.Ascending) ? "▼ PLAYERS" : "▲ PLAYERS") : "PLAYERS");
		this.pingButton.text = ((this.sortType == ServerSortType.Ping) ? ((this.sortDirection == ServerSortDirection.Ascending) ? "▼ PING" : "▲ PING") : "PING");
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x00047C1C File Offset: 0x00045E1C
	private void SortServers()
	{
		this.serverList.hierarchy.Sort(delegate(VisualElement a, VisualElement b)
		{
			EndPoint serverEndPoint = this.GetServerEndPoint(a);
			EndPoint serverEndPoint2 = this.GetServerEndPoint(b);
			ServerPreviewData serverPreviewData = this.GetServerPreviewData(serverEndPoint);
			ServerPreviewData serverPreviewData2 = this.GetServerPreviewData(serverEndPoint2);
			string text = (serverPreviewData != null) ? serverPreviewData.name : serverEndPoint.ToString();
			string strB = (serverPreviewData2 != null) ? serverPreviewData2.name : serverEndPoint2.ToString();
			int num = (serverPreviewData != null) ? serverPreviewData.players : 0;
			int value = (serverPreviewData2 != null) ? serverPreviewData2.players : 0;
			int num2 = (serverPreviewData != null) ? serverPreviewData.ping : int.MaxValue;
			int value2 = (serverPreviewData2 != null) ? serverPreviewData2.ping : int.MaxValue;
			int num3 = 0;
			switch (this.sortType)
			{
			case ServerSortType.Name:
				num3 = text.CompareTo(strB) * ((this.sortDirection == ServerSortDirection.Ascending) ? 1 : -1);
				break;
			case ServerSortType.Players:
				num3 = num.CompareTo(value) * ((this.sortDirection == ServerSortDirection.Ascending) ? 1 : -1);
				if (num3 == 0)
				{
					num3 = text.CompareTo(strB);
				}
				break;
			case ServerSortType.Ping:
				num3 = num2.CompareTo(value2) * ((this.sortDirection == ServerSortDirection.Ascending) ? 1 : -1);
				if (num3 == 0)
				{
					num3 = text.CompareTo(strB);
				}
				break;
			}
			return num3;
		});
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00047C48 File Offset: 0x00045E48
	private void FilterServers()
	{
		foreach (EndPoint endPoint in this.endPointVisualElementMap.Keys)
		{
			this.FilterServer(endPoint);
		}
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x00047CA0 File Offset: 0x00045EA0
	private void FilterServer(EndPoint endPoint)
	{
		if (!this.endPointVisualElementMap.ContainsKey(endPoint))
		{
			return;
		}
		VisualElement visualElement = this.endPointVisualElementMap[endPoint];
		ServerPreviewData serverPreviewData = this.GetServerPreviewData(endPoint);
		bool flag;
		if (serverPreviewData == null)
		{
			string text = endPoint.ipAddress.ToLower();
			string value = string.IsNullOrEmpty(this.searchTextField.value) ? null : this.searchTextField.value.ToLower();
			flag = ((string.IsNullOrEmpty(value) || text.Contains(value)) && this.showUnreachableToggle.value);
		}
		else
		{
			string text2 = serverPreviewData.name.ToLower();
			string value2 = string.IsNullOrEmpty(this.searchTextField.value) ? null : this.searchTextField.value.ToLower();
			flag = ((string.IsNullOrEmpty(value2) || text2.Contains(value2)) && serverPreviewData.ping <= this.maxPingTextField.value && (serverPreviewData.players > 0 || this.showEmptyToggle.value) && (serverPreviewData.players < serverPreviewData.maxPlayers || this.showFullToggle.value) && (!serverPreviewData.isPasswordProtected || this.showPasswordProtectedToggle.value) && (serverPreviewData.clientRequiredModIds.Length == 0 || this.showModdedToggle.value));
		}
		visualElement.style.display = (flag ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x00012DEF File Offset: 0x00010FEF
	public void ShowFilters()
	{
		this.filters.style.display = DisplayStyle.Flex;
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x00012E07 File Offset: 0x00011007
	public void HideFilters()
	{
		this.filters.style.display = DisplayStyle.None;
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x00047DFC File Offset: 0x00045FFC
	private ServerPreviewData PingServer(EndPoint endPoint, int connectTimeout, int responseTimeout)
	{
		TCPClient tcpClient = new TCPClient(endPoint, connectTimeout, 1000);
		double pingTimestamp = 0.0;
		ServerPreviewData previewData = null;
		ManualResetEventSlim responseEvent = new ManualResetEventSlim(false);
		tcpClient.OnConnected += delegate()
		{
			string message = JsonSerializer.Serialize<TCPServerPreviewRequest>(new TCPServerPreviewRequest(), null);
			tcpClient.SendMessage(message);
		};
		tcpClient.OnMessageSent += delegate(string message)
		{
			try
			{
				if (JsonSerializer.Deserialize<TCPServerMessage>(message, null).type == TCPServerMessageType.PreviewRequest)
				{
					pingTimestamp = Utils.GetTimestamp();
				}
			}
			catch (Exception ex)
			{
				UIServerBrowser.Logger.Error(string.Format("Error parsing message sent to {0}: {1}", endPoint, ex.Message));
			}
		};
		tcpClient.OnMessageReceived += delegate(string message)
		{
			try
			{
				if (JsonSerializer.Deserialize<TCPServerMessage>(message, null).type == TCPServerMessageType.PreviewResponse)
				{
					TCPServerPreviewResponse tcpserverPreviewResponse = JsonSerializer.Deserialize<TCPServerPreviewResponse>(message, null);
					int ping = (int)(Utils.GetTimestamp() - pingTimestamp);
					previewData = new ServerPreviewData
					{
						name = tcpserverPreviewResponse.name,
						players = tcpserverPreviewResponse.players,
						maxPlayers = tcpserverPreviewResponse.maxPlayers,
						isPasswordProtected = tcpserverPreviewResponse.isPasswordProtected,
						clientRequiredModIds = tcpserverPreviewResponse.clientRequiredModIds,
						ping = ping
					};
					responseEvent.Set();
				}
			}
			catch (Exception ex)
			{
				UIServerBrowser.Logger.Error(string.Format("Error parsing message from {0}: {1}", endPoint, ex.Message));
			}
		};
		tcpClient.Connect();
		if (tcpClient.IsConnected)
		{
			responseEvent.Wait(responseTimeout);
			tcpClient.Disconnect();
		}
		return previewData;
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x00012E1F File Offset: 0x0001101F
	private void OnClickNameSort()
	{
		if (this.sortType == ServerSortType.Name)
		{
			this.sortDirection = ((this.sortDirection == ServerSortDirection.Ascending) ? ServerSortDirection.Descending : ServerSortDirection.Ascending);
		}
		else
		{
			this.sortType = ServerSortType.Name;
			this.sortDirection = ServerSortDirection.Ascending;
		}
		this.StyleSortButtons();
		this.SortServers();
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x00012E57 File Offset: 0x00011057
	private void OnClickPlayersSort()
	{
		if (this.sortType == ServerSortType.Players)
		{
			this.sortDirection = ((this.sortDirection == ServerSortDirection.Ascending) ? ServerSortDirection.Descending : ServerSortDirection.Ascending);
		}
		else
		{
			this.sortType = ServerSortType.Players;
			this.sortDirection = ServerSortDirection.Descending;
		}
		this.StyleSortButtons();
		this.SortServers();
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x00012E90 File Offset: 0x00011090
	private void OnClickPingSort()
	{
		if (this.sortType == ServerSortType.Ping)
		{
			this.sortDirection = ((this.sortDirection == ServerSortDirection.Ascending) ? ServerSortDirection.Descending : ServerSortDirection.Ascending);
		}
		else
		{
			this.sortType = ServerSortType.Ping;
			this.sortDirection = ServerSortDirection.Ascending;
		}
		this.StyleSortButtons();
		this.SortServers();
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x00012EC9 File Offset: 0x000110C9
	private void OnServerBrowserClickClose()
	{
		EventManager.TriggerEvent("Event_OnServerBrowserClickClose", null);
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x00012ED6 File Offset: 0x000110D6
	private void OnClickRefresh()
	{
		EventManager.TriggerEvent("Event_OnServerBrowserClickRefresh", null);
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x00012EE3 File Offset: 0x000110E3
	private void OnClickNewServer()
	{
		EventManager.TriggerEvent("Event_OnServerBrowserClickNewServer", null);
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x00012EF0 File Offset: 0x000110F0
	private void OnClickFilters()
	{
		EventManager.TriggerEvent("Event_OnServerBrowserClickFilters", null);
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x00012EFD File Offset: 0x000110FD
	private void OnClickServer(ClickEvent e, EndPoint endPoint)
	{
		EventManager.TriggerEvent("Event_OnServerBrowserClickEndPoint", new Dictionary<string, object>
		{
			{
				"endPoint",
				endPoint
			}
		});
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x00012F1A File Offset: 0x0001111A
	private void OnFiltersClickClose()
	{
		EventManager.TriggerEvent("Event_OnServerBrowserFiltersClickClose", null);
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00012F27 File Offset: 0x00011127
	private void OnChangeSearchTextField(ChangeEvent<string> e)
	{
		this.FilterServers();
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00012F27 File Offset: 0x00011127
	private void OnChangeMaxPingTextField(ChangeEvent<int> e)
	{
		this.FilterServers();
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00012F27 File Offset: 0x00011127
	private void OnChangeShowFullToggle(ChangeEvent<bool> e)
	{
		this.FilterServers();
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00012F27 File Offset: 0x00011127
	private void OnChangeShowEmptyToggle(ChangeEvent<bool> e)
	{
		this.FilterServers();
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x00012F27 File Offset: 0x00011127
	private void OnChangeShowPasswordProtectedToggle(ChangeEvent<bool> e)
	{
		this.FilterServers();
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00012F27 File Offset: 0x00011127
	private void OnChangeShowModdedToggle(ChangeEvent<bool> e)
	{
		this.FilterServers();
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x00012F27 File Offset: 0x00011127
	private void OnChangeShowUnreachableToggle(ChangeEvent<bool> e)
	{
		this.FilterServers();
	}

	// Token: 0x040007F1 RID: 2033
	private static readonly global::Logger Logger = new global::Logger("UIServerBrowser");

	// Token: 0x040007F2 RID: 2034
	[Header("References")]
	[SerializeField]
	private VisualTreeAsset serverAsset;

	// Token: 0x040007F3 RID: 2035
	private VisualElement serverBrowser;

	// Token: 0x040007F4 RID: 2036
	private VisualElement filters;

	// Token: 0x040007F5 RID: 2037
	private IconButton closeIconButton;

	// Token: 0x040007F6 RID: 2038
	private VisualElement serverList;

	// Token: 0x040007F7 RID: 2039
	private Button nameButton;

	// Token: 0x040007F8 RID: 2040
	private Button playersButton;

	// Token: 0x040007F9 RID: 2041
	private Button pingButton;

	// Token: 0x040007FA RID: 2042
	private Button refreshButton;

	// Token: 0x040007FB RID: 2043
	private Button newServerButton;

	// Token: 0x040007FC RID: 2044
	private Button filtersButton;

	// Token: 0x040007FD RID: 2045
	private IconButton filtersCloseIconButton;

	// Token: 0x040007FE RID: 2046
	private TextField searchTextField;

	// Token: 0x040007FF RID: 2047
	private IntegerField maxPingTextField;

	// Token: 0x04000800 RID: 2048
	private Toggle showFullToggle;

	// Token: 0x04000801 RID: 2049
	private Toggle showEmptyToggle;

	// Token: 0x04000802 RID: 2050
	private Toggle showPasswordProtectedToggle;

	// Token: 0x04000803 RID: 2051
	private Toggle showModdedToggle;

	// Token: 0x04000804 RID: 2052
	private Toggle showUnreachableToggle;

	// Token: 0x04000805 RID: 2053
	private Dictionary<EndPoint, VisualElement> endPointVisualElementMap = new Dictionary<EndPoint, VisualElement>();

	// Token: 0x04000806 RID: 2054
	private ServerSortType sortType;

	// Token: 0x04000807 RID: 2055
	private ServerSortDirection sortDirection;
}
