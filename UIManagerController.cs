using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class UIManagerController : MonoBehaviour
{
	// Token: 0x06000A33 RID: 2611 RVA: 0x0003C490 File Offset: 0x0003A690
	public void Awake()
	{
		this.uiManager = base.GetComponent<UIManager>();
		EventManager.AddEventListener("Event_OnUserInterfaceScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnUserInterfaceScaleChanged));
		EventManager.AddEventListener("Event_OnUIStateChanged", new Action<Dictionary<string, object>>(this.Event_OnUIStateChanged));
		EventManager.AddEventListener("Event_OnMainMenuClickPlay", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickPlay));
		EventManager.AddEventListener("Event_OnMainMenuClickPlayer", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickPlayer));
		EventManager.AddEventListener("Event_OnMainMenuClickSettings", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickSettings));
		EventManager.AddEventListener("Event_OnMainMenuClickMods", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickMods));
		EventManager.AddEventListener("Event_OnPlayerMenuClickBack", new Action<Dictionary<string, object>>(this.Event_OnPlayerMenuClickBack));
		EventManager.AddEventListener("Event_OnPlayerMenuClickIdentity", new Action<Dictionary<string, object>>(this.Event_OnPlayerMenuClickIdentity));
		EventManager.AddEventListener("Event_OnPlayerMenuClickAppearance", new Action<Dictionary<string, object>>(this.Event_OnPlayerMenuClickAppearance));
		EventManager.AddEventListener("Event_OnIdentityClickClose", new Action<Dictionary<string, object>>(this.Event_OnIdentityClickClose));
		EventManager.AddEventListener("Event_OnAppearanceClickClose", new Action<Dictionary<string, object>>(this.Event_OnAppearanceClickClose));
		EventManager.AddEventListener("Event_OnPauseMenuClickSettings", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSettings));
		EventManager.AddEventListener("Event_OnPauseMenuClickSelectTeam", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSelectTeam));
		EventManager.AddEventListener("Event_OnPauseMenuClickSelectPosition", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSelectPosition));
		EventManager.AddEventListener("Event_OnPauseMenuClickServerBrowser", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickServerBrowser));
		EventManager.AddEventListener("Event_OnServerBrowserClickClose", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickClose));
		EventManager.AddEventListener("Event_OnServerBrowserClickEndPoint", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickEndPoint));
		EventManager.AddEventListener("Event_OnServerBrowserClickNewServer", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickNewServer));
		EventManager.AddEventListener("Event_OnServerBrowserClickFilters", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickFilters));
		EventManager.AddEventListener("Event_OnServerBrowserFiltersClickClose", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserFiltersClickClose));
		EventManager.AddEventListener("Event_OnSettingsClickClose", new Action<Dictionary<string, object>>(this.Event_OnSettingsClickClose));
		EventManager.AddEventListener("Event_OnNewServerClickStart", new Action<Dictionary<string, object>>(this.Event_OnNewServerClickStart));
		EventManager.AddEventListener("Event_OnNewServerClickClose", new Action<Dictionary<string, object>>(this.Event_OnNewServerClickClose));
		EventManager.AddEventListener("Event_OnModsClickClose", new Action<Dictionary<string, object>>(this.Event_OnModsClickClose));
		EventManager.AddEventListener("Event_OnFooterClickInvite", new Action<Dictionary<string, object>>(this.Event_OnFooterClickInvite));
		EventManager.AddEventListener("Event_OnFriendsClickClose", new Action<Dictionary<string, object>>(this.Event_OnFriendsClickClose));
		EventManager.AddEventListener("Event_OnPlayClickServerBrowser", new Action<Dictionary<string, object>>(this.Event_OnPlayClickServerBrowser));
		EventManager.AddEventListener("Event_OnPlayClickClose", new Action<Dictionary<string, object>>(this.Event_OnPlayClickClose));
		EventManager.AddEventListener("Event_OnChatMessageAdded", new Action<Dictionary<string, object>>(this.Event_OnChatMessageAdded));
		EventManager.AddEventListener("Event_OnMatchJoinTimeoutTickerStarted", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerStarted));
		EventManager.AddEventListener("Event_OnMatchJoinTimeoutTickerTick", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerTick));
		EventManager.AddEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0001071C File Offset: 0x0000E91C
	private void Start()
	{
		this.uiManager.SetUIScale(SettingsManager.UserInterfaceScale);
		this.uiManager.ShowPhaseViews(GlobalStateManager.UIState.Phase);
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0003C780 File Offset: 0x0003A980
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnUserInterfaceScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnUserInterfaceScaleChanged));
		EventManager.RemoveEventListener("Event_OnUIStateChanged", new Action<Dictionary<string, object>>(this.Event_OnUIStateChanged));
		EventManager.RemoveEventListener("Event_OnMainMenuClickPlay", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickPlay));
		EventManager.RemoveEventListener("Event_OnMainMenuClickPlayer", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickPlayer));
		EventManager.RemoveEventListener("Event_OnMainMenuClickSettings", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickSettings));
		EventManager.RemoveEventListener("Event_OnMainMenuClickMods", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickMods));
		EventManager.RemoveEventListener("Event_OnPlayerMenuClickBack", new Action<Dictionary<string, object>>(this.Event_OnPlayerMenuClickBack));
		EventManager.RemoveEventListener("Event_OnPlayerMenuClickIdentity", new Action<Dictionary<string, object>>(this.Event_OnPlayerMenuClickIdentity));
		EventManager.RemoveEventListener("Event_OnPlayerMenuClickAppearance", new Action<Dictionary<string, object>>(this.Event_OnPlayerMenuClickAppearance));
		EventManager.RemoveEventListener("Event_OnIdentityClickClose", new Action<Dictionary<string, object>>(this.Event_OnIdentityClickClose));
		EventManager.RemoveEventListener("Event_OnAppearanceClickClose", new Action<Dictionary<string, object>>(this.Event_OnAppearanceClickClose));
		EventManager.RemoveEventListener("Event_OnPauseMenuClickSettings", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSettings));
		EventManager.RemoveEventListener("Event_OnPauseMenuClickSelectTeam", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSelectTeam));
		EventManager.RemoveEventListener("Event_OnPauseMenuClickSelectPosition", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSelectPosition));
		EventManager.RemoveEventListener("Event_OnPauseMenuClickServerBrowser", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickServerBrowser));
		EventManager.RemoveEventListener("Event_OnServerBrowserClickClose", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickClose));
		EventManager.RemoveEventListener("Event_OnServerBrowserClickEndPoint", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickEndPoint));
		EventManager.RemoveEventListener("Event_OnServerBrowserClickNewServer", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickNewServer));
		EventManager.RemoveEventListener("Event_OnServerBrowserClickFilters", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickFilters));
		EventManager.RemoveEventListener("Event_OnServerBrowserFiltersClickClose", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserFiltersClickClose));
		EventManager.RemoveEventListener("Event_OnSettingsClickClose", new Action<Dictionary<string, object>>(this.Event_OnSettingsClickClose));
		EventManager.RemoveEventListener("Event_OnNewServerClickStart", new Action<Dictionary<string, object>>(this.Event_OnNewServerClickStart));
		EventManager.RemoveEventListener("Event_OnNewServerClickClose", new Action<Dictionary<string, object>>(this.Event_OnNewServerClickClose));
		EventManager.RemoveEventListener("Event_OnModsClickClose", new Action<Dictionary<string, object>>(this.Event_OnModsClickClose));
		EventManager.RemoveEventListener("Event_OnFooterClickInvite", new Action<Dictionary<string, object>>(this.Event_OnFooterClickInvite));
		EventManager.RemoveEventListener("Event_OnFriendsClickClose", new Action<Dictionary<string, object>>(this.Event_OnFriendsClickClose));
		EventManager.RemoveEventListener("Event_OnPlayClickServerBrowser", new Action<Dictionary<string, object>>(this.Event_OnPlayClickServerBrowser));
		EventManager.RemoveEventListener("Event_OnPlayClickClose", new Action<Dictionary<string, object>>(this.Event_OnPlayClickClose));
		EventManager.RemoveEventListener("Event_OnChatMessageAdded", new Action<Dictionary<string, object>>(this.Event_OnChatMessageAdded));
		EventManager.RemoveEventListener("Event_OnMatchJoinTimeoutTickerStarted", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerStarted));
		EventManager.RemoveEventListener("Event_OnMatchJoinTimeoutTickerTick", new Action<Dictionary<string, object>>(this.Event_OnMatchJoinTimeoutTickerTick));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0003CA64 File Offset: 0x0003AC64
	private void HandlePlayerGameState(Player player)
	{
		ref PlayerGameState value = player.GameState.Value;
		this.uiManager.ShowPhaseViews(GlobalStateManager.UIState.Phase);
		switch (value.Phase)
		{
		case PlayerPhase.TeamSelect:
			this.uiManager.TeamSelect.Show();
			return;
		case PlayerPhase.PositionSelect:
			this.uiManager.PositionSelect.Show();
			return;
		case PlayerPhase.Play:
			this.uiManager.Hud.Show();
			this.uiManager.Minimap.Show();
			return;
		default:
			return;
		}
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0003CAF4 File Offset: 0x0003ACF4
	private void Event_OnUserInterfaceScaleChanged(Dictionary<string, object> message)
	{
		float uiscale = (float)message["value"];
		this.uiManager.SetUIScale(uiscale);
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0003CB20 File Offset: 0x0003AD20
	private void Event_OnUIStateChanged(Dictionary<string, object> message)
	{
		ref UIState ptr = (UIState)message["oldUIState"];
		UIState uistate = (UIState)message["newUIState"];
		if (ptr.Phase == uistate.Phase)
		{
			return;
		}
		this.uiManager.ShowPhaseViews(uistate.Phase);
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00010743 File Offset: 0x0000E943
	private void Event_OnMainMenuClickPlay(Dictionary<string, object> message)
	{
		this.uiManager.Play.Show();
		this.uiManager.MainMenu.Hide();
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00010767 File Offset: 0x0000E967
	private void Event_OnMainMenuClickPlayer(Dictionary<string, object> message)
	{
		this.uiManager.PlayerMenu.Show();
		this.uiManager.MainMenu.Hide();
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x0001078B File Offset: 0x0000E98B
	private void Event_OnMainMenuClickSettings(Dictionary<string, object> message)
	{
		this.uiManager.Settings.Show();
		this.uiManager.MainMenu.Hide();
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x000107AF File Offset: 0x0000E9AF
	private void Event_OnMainMenuClickMods(Dictionary<string, object> message)
	{
		this.uiManager.Mods.Show();
		this.uiManager.MainMenu.Hide();
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x000107D3 File Offset: 0x0000E9D3
	private void Event_OnPlayerMenuClickBack(Dictionary<string, object> message)
	{
		this.uiManager.MainMenu.Show();
		this.uiManager.PlayerMenu.Hide();
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x000107F7 File Offset: 0x0000E9F7
	private void Event_OnPlayerMenuClickIdentity(Dictionary<string, object> message)
	{
		this.uiManager.Identity.Show();
		this.uiManager.PlayerMenu.Hide();
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0001081B File Offset: 0x0000EA1B
	private void Event_OnPlayerMenuClickAppearance(Dictionary<string, object> message)
	{
		this.uiManager.Appearance.Show();
		this.uiManager.PlayerMenu.Hide();
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x0001083F File Offset: 0x0000EA3F
	private void Event_OnIdentityClickClose(Dictionary<string, object> message)
	{
		this.uiManager.PlayerMenu.Show();
		this.uiManager.Identity.Hide();
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x00010863 File Offset: 0x0000EA63
	private void Event_OnAppearanceClickClose(Dictionary<string, object> message)
	{
		this.uiManager.PlayerMenu.Show();
		this.uiManager.Appearance.Hide();
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x00010887 File Offset: 0x0000EA87
	private void Event_OnPauseMenuClickSettings(Dictionary<string, object> message)
	{
		this.uiManager.Settings.Show();
		this.uiManager.PauseMenu.Hide();
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x000108AB File Offset: 0x0000EAAB
	private void Event_OnPauseMenuClickSelectTeam(Dictionary<string, object> message)
	{
		this.uiManager.PauseMenu.Hide();
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x000108AB File Offset: 0x0000EAAB
	private void Event_OnPauseMenuClickSelectPosition(Dictionary<string, object> message)
	{
		this.uiManager.PauseMenu.Hide();
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x000108BE File Offset: 0x0000EABE
	private void Event_OnPauseMenuClickServerBrowser(Dictionary<string, object> message)
	{
		this.uiManager.ServerBrowser.Show();
		this.uiManager.PauseMenu.Hide();
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0003CB70 File Offset: 0x0003AD70
	private void Event_OnServerBrowserClickClose(Dictionary<string, object> message)
	{
		UIPhase phase = GlobalStateManager.UIState.Phase;
		if (phase != UIPhase.LockerRoom)
		{
			if (phase == UIPhase.Playing)
			{
				this.uiManager.PauseMenu.Show();
			}
		}
		else
		{
			this.uiManager.Play.Show();
		}
		this.uiManager.ServerBrowser.Hide();
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Event_OnServerBrowserClickEndPoint(Dictionary<string, object> message)
	{
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x000108E2 File Offset: 0x0000EAE2
	private void Event_OnServerBrowserClickNewServer(Dictionary<string, object> message)
	{
		this.uiManager.NewServer.Show();
		this.uiManager.ServerBrowser.Hide();
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00010906 File Offset: 0x0000EB06
	private void Event_OnServerBrowserClickFilters(Dictionary<string, object> message)
	{
		this.uiManager.ServerBrowser.ShowFilters();
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00010918 File Offset: 0x0000EB18
	private void Event_OnServerBrowserFiltersClickClose(Dictionary<string, object> message)
	{
		this.uiManager.ServerBrowser.HideFilters();
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0003CBC8 File Offset: 0x0003ADC8
	private void Event_OnSettingsClickClose(Dictionary<string, object> message)
	{
		UIPhase phase = GlobalStateManager.UIState.Phase;
		if (phase != UIPhase.LockerRoom)
		{
			if (phase == UIPhase.Playing)
			{
				this.uiManager.PauseMenu.Show();
			}
		}
		else
		{
			this.uiManager.MainMenu.Show();
		}
		this.uiManager.Settings.Hide();
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0001092A File Offset: 0x0000EB2A
	private void Event_OnNewServerClickStart(Dictionary<string, object> message)
	{
		this.uiManager.ServerBrowser.Show();
		this.uiManager.NewServer.Hide();
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0001092A File Offset: 0x0000EB2A
	private void Event_OnNewServerClickClose(Dictionary<string, object> message)
	{
		this.uiManager.ServerBrowser.Show();
		this.uiManager.NewServer.Hide();
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0001094E File Offset: 0x0000EB4E
	private void Event_OnModsClickClose(Dictionary<string, object> message)
	{
		this.uiManager.MainMenu.Show();
		this.uiManager.Mods.Hide();
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00010972 File Offset: 0x0000EB72
	private void Event_OnFooterClickInvite(Dictionary<string, object> message)
	{
		this.uiManager.Friends.Show();
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00010985 File Offset: 0x0000EB85
	private void Event_OnFriendsClickClose(Dictionary<string, object> message)
	{
		this.uiManager.Friends.Hide();
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00010998 File Offset: 0x0000EB98
	private void Event_OnPlayClickServerBrowser(Dictionary<string, object> message)
	{
		this.uiManager.ServerBrowser.Show();
		this.uiManager.Play.Hide();
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x000109BC File Offset: 0x0000EBBC
	private void Event_OnPlayClickClose(Dictionary<string, object> message)
	{
		this.uiManager.MainMenu.Show();
		this.uiManager.Play.Hide();
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x000109E0 File Offset: 0x0000EBE0
	private void Event_OnChatMessageAdded(Dictionary<string, object> message)
	{
		this.uiManager.PlayNotificationSound();
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x000109ED File Offset: 0x0000EBED
	private void Event_OnMatchJoinTimeoutTickerStarted(Dictionary<string, object> message)
	{
		if (!BackendUtils.IsConnectedToMatchEndPoint())
		{
			this.uiManager.PlayWhooshSound();
		}
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00010A01 File Offset: 0x0000EC01
	private void Event_OnMatchJoinTimeoutTickerTick(Dictionary<string, object> message)
	{
		if (!BackendUtils.IsConnectedToMatchEndPoint())
		{
			this.uiManager.PlayTickSound();
		}
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0003CC20 File Offset: 0x0003AE20
	private void Event_Everyone_OnPlayerSpawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (!player.IsLocalPlayer)
		{
			return;
		}
		this.HandlePlayerGameState(player);
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0003CC50 File Offset: 0x0003AE50
	private void Event_Everyone_OnPlayerGameStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		PlayerGameState playerGameState = (PlayerGameState)message["oldGameState"];
		PlayerGameState playerGameState2 = (PlayerGameState)message["newGameState"];
		if (!player.IsLocalPlayer)
		{
			return;
		}
		if (playerGameState.Phase == playerGameState2.Phase)
		{
			return;
		}
		this.HandlePlayerGameState(player);
	}

	// Token: 0x04000604 RID: 1540
	private UIManager uiManager;
}
