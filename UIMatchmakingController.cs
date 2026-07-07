using System;
using System.Collections.Generic;

// Token: 0x0200019A RID: 410
public class UIMatchmakingController : UIViewController<UIMatchmaking>
{
	// Token: 0x06000BF3 RID: 3059 RVA: 0x000425D4 File Offset: 0x000407D4
	public override void Awake()
	{
		base.Awake();
		this.uiMatchmaking = base.GetComponent<UIMatchmaking>();
		EventManager.AddEventListener("Event_OnPlayerGroupDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerGroupDataChanged));
		EventManager.AddEventListener("Event_OnPlayerMatchDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerMatchDataChanged));
		EventManager.AddEventListener("Event_OnConnectionStateChanged", new Action<Dictionary<string, object>>(this.Event_OnConnectionStateChanged));
		EventManager.AddEventListener("Event_OnMatchingTickerStarted", new Action<Dictionary<string, object>>(this.Event_OnMatchingTickerStarted));
		EventManager.AddEventListener("Event_OnMatchingTickerTick", new Action<Dictionary<string, object>>(this.Event_OnMatchingTickerTick));
		EventManager.AddEventListener("Event_OnMatchingTickerStopped", new Action<Dictionary<string, object>>(this.Event_OnMatchingTickerStopped));
		EventManager.AddEventListener("Event_OnMatchJoinTimeoutTickerStarted", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerStarted));
		EventManager.AddEventListener("Event_OnMatchJoinTimeoutTickerTick", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerTick));
		EventManager.AddEventListener("Event_OnMatchJoinTimeoutTickerStopped", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerStopped));
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x000426BC File Offset: 0x000408BC
	private void Start()
	{
		this.uiMatchmaking.SetMatchingVisibility(false);
		this.uiMatchmaking.SetMatchingPhaseText(string.Empty);
		this.uiMatchmaking.SetMatchingTimeVisibility(false);
		this.uiMatchmaking.SetMatchingTimeText(0);
		this.uiMatchmaking.SetMatchingConnectButtonVisibility(false);
		this.uiMatchmaking.SetMatchingCloseButtonVisibility(false);
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x00042718 File Offset: 0x00040918
	public override void OnDestroy()
	{
		base.OnDestroy();
		EventManager.RemoveEventListener("Event_OnPlayerGroupDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerGroupDataChanged));
		EventManager.RemoveEventListener("Event_OnPlayerMatchDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerMatchDataChanged));
		EventManager.RemoveEventListener("Event_OnConnectionStateChanged", new Action<Dictionary<string, object>>(this.Event_OnConnectionStateChanged));
		EventManager.RemoveEventListener("Event_OnMatchingTickerStarted", new Action<Dictionary<string, object>>(this.Event_OnMatchingTickerStarted));
		EventManager.RemoveEventListener("Event_OnMatchingTickerTick", new Action<Dictionary<string, object>>(this.Event_OnMatchingTickerTick));
		EventManager.RemoveEventListener("Event_OnMatchingTickerStopped", new Action<Dictionary<string, object>>(this.Event_OnMatchingTickerStopped));
		EventManager.RemoveEventListener("Event_OnMatchJoinTimeoutTickerStarted", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerStarted));
		EventManager.RemoveEventListener("Event_OnMatchJoinTimeoutTickerTick", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerTick));
		EventManager.RemoveEventListener("Event_OnMatchJoinTimeoutTickerStopped", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerStopped));
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x000427F4 File Offset: 0x000409F4
	private void UpdateMatching()
	{
		bool groupData = BackendManager.PlayerState.GroupData != null;
		PlayerMatchData matchData = BackendManager.PlayerState.MatchData;
		if (groupData)
		{
			this.uiMatchmaking.SetMatchingVisibility(true);
			this.uiMatchmaking.SetMatchingPhaseText("LOOKING FOR A MATCH...");
			this.uiMatchmaking.SetMatchingConnectButtonVisibility(false);
			this.uiMatchmaking.SetMatchingCloseButtonVisibility(true);
			return;
		}
		if (matchData == null || BackendUtils.IsConnectedToMatchEndPoint())
		{
			this.uiMatchmaking.SetMatchingVisibility(false);
			this.uiMatchmaking.SetMatchingPhaseText(string.Empty);
			this.uiMatchmaking.SetMatchingConnectButtonVisibility(false);
			this.uiMatchmaking.SetMatchingCloseButtonVisibility(false);
			return;
		}
		this.uiMatchmaking.SetMatchingVisibility(true);
		this.uiMatchmaking.SetMatchingCloseButtonVisibility(false);
		if (matchData.endPoint == null)
		{
			this.uiMatchmaking.SetMatchingPhaseText("MATCH FOUND! DEPLOYING MATCH SERVER...");
			this.uiMatchmaking.SetMatchingConnectButtonVisibility(false);
			return;
		}
		this.uiMatchmaking.SetMatchingPhaseText("MATCH READY!");
		this.uiMatchmaking.SetMatchingConnectButtonVisibility(true);
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00011DD8 File Offset: 0x0000FFD8
	private void Event_OnPlayerGroupDataChanged(Dictionary<string, object> message)
	{
		this.UpdateMatching();
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00011DD8 File Offset: 0x0000FFD8
	private void Event_OnPlayerMatchDataChanged(Dictionary<string, object> message)
	{
		this.UpdateMatching();
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x00011DD8 File Offset: 0x0000FFD8
	private void Event_OnConnectionStateChanged(Dictionary<string, object> message)
	{
		this.UpdateMatching();
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x000428EC File Offset: 0x00040AEC
	private void Event_OnMatchingTickerStarted(Dictionary<string, object> message)
	{
		int matchingTimeText = (int)message["startingTick"];
		this.uiMatchmaking.SetMatchingTimeVisibility(true);
		this.uiMatchmaking.SetMatchingTimeText(matchingTimeText);
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00042924 File Offset: 0x00040B24
	private void Event_OnMatchingTickerTick(Dictionary<string, object> message)
	{
		int matchingTimeText = (int)message["tick"];
		this.uiMatchmaking.SetMatchingTimeText(matchingTimeText);
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00011DE0 File Offset: 0x0000FFE0
	private void Event_OnMatchingTickerStopped(Dictionary<string, object> message)
	{
		this.uiMatchmaking.SetMatchingTimeVisibility(false);
		this.uiMatchmaking.SetMatchingTimeText(0);
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x000428EC File Offset: 0x00040AEC
	private void Event_OnMatchJoinTimeoutTickerStarted(Dictionary<string, object> message)
	{
		int matchingTimeText = (int)message["startingTick"];
		this.uiMatchmaking.SetMatchingTimeVisibility(true);
		this.uiMatchmaking.SetMatchingTimeText(matchingTimeText);
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x00042924 File Offset: 0x00040B24
	private void Event_OnMatchJoinTimeoutTickerTick(Dictionary<string, object> message)
	{
		int matchingTimeText = (int)message["tick"];
		this.uiMatchmaking.SetMatchingTimeText(matchingTimeText);
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x00011DE0 File Offset: 0x0000FFE0
	private void Event_OnMatchJoinTimeoutTickerStopped(Dictionary<string, object> message)
	{
		this.uiMatchmaking.SetMatchingTimeVisibility(false);
		this.uiMatchmaking.SetMatchingTimeText(0);
	}

	// Token: 0x0400072D RID: 1837
	private UIMatchmaking uiMatchmaking;
}
