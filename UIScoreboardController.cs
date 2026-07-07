using System;
using System.Collections.Generic;

// Token: 0x020001C1 RID: 449
internal class UIScoreboardController : UIViewController<UIScoreboard>
{
	// Token: 0x06000D2D RID: 3373 RVA: 0x00046FC0 File Offset: 0x000451C0
	public override void Awake()
	{
		base.Awake();
		this.uiScoreboard = base.GetComponent<UIScoreboard>();
		EventManager.AddEventListener("Event_Everyone_OnPlayerAdded", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAdded));
		EventManager.AddEventListener("Event_Everyone_OnPlayerRemoved", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerRemoved));
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerUsernameChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerUsernameChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerGoalsChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGoalsChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerAssistsChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAssistsChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerPingChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPingChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerPositionChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerPatreonLevelChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPatreonLevelChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerAdminLevelChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAdminLevelChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerSteamIdChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSteamIdChanged));
		EventManager.AddEventListener("Event_Everyone_OnServerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnServerChanged));
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x000470E8 File Offset: 0x000452E8
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerAdded", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAdded));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerRemoved", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerRemoved));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerUsernameChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerUsernameChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGoalsChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGoalsChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerAssistsChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAssistsChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerPingChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPingChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerPositionChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerPatreonLevelChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPatreonLevelChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerAdminLevelChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAdminLevelChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerSteamIdChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSteamIdChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnServerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnServerChanged));
		base.OnDestroy();
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x00047204 File Offset: 0x00045404
	private void Event_Everyone_OnPlayerAdded(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.uiScoreboard.AddPlayer(player);
		this.uiScoreboard.StyleServer(NetworkBehaviourSingleton<ServerManager>.Instance.Server.Value, MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayers(false).Count);
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x00047268 File Offset: 0x00045468
	private void Event_Everyone_OnPlayerRemoved(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.uiScoreboard.RemovePlayer(player);
		this.uiScoreboard.StyleServer(NetworkBehaviourSingleton<ServerManager>.Instance.Server.Value, MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayers(false).Count);
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerGameStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerUsernameChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerGoalsChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerAssistsChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerPingChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerPositionChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerPatreonLevelChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerAdminLevelChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x000472CC File Offset: 0x000454CC
	private void Event_Everyone_OnPlayerSteamIdChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiScoreboard.StylePlayer(player);
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x00012D86 File Offset: 0x00010F86
	private void Event_Everyone_OnServerChanged(Dictionary<string, object> message)
	{
		this.uiScoreboard.StyleServer(NetworkBehaviourSingleton<ServerManager>.Instance.Server.Value, MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayers(false).Count);
	}

	// Token: 0x040007E3 RID: 2019
	private UIScoreboard uiScoreboard;
}
