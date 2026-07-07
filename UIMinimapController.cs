using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200019C RID: 412
internal class UIMinimapController : UIViewController<UIMinimap>
{
	// Token: 0x06000C10 RID: 3088 RVA: 0x00042FE8 File Offset: 0x000411E8
	public override void Awake()
	{
		base.Awake();
		this.uiMinimap = base.GetComponent<UIMinimap>();
		EventManager.AddEventListener("Event_Everyone_OnLevelSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnLevelSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerNumberChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerNumberChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodyDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodyDespawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckDespawned));
		EventManager.AddEventListener("Event_OnShowMinimapChanged", new Action<Dictionary<string, object>>(this.Event_OnShowMinimapChanged));
		EventManager.AddEventListener("Event_OnMinimapOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapOpacityChanged));
		EventManager.AddEventListener("Event_OnShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(this.Event_OnShowGameUserInterfaceChanged));
		EventManager.AddEventListener("Event_OnMinimapHorizontalPositionChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapHorizontalPositionChanged));
		EventManager.AddEventListener("Event_OnMinimapVerticalPositionChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapVerticalPositionChanged));
		EventManager.AddEventListener("Event_OnMinimapBackgroundOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapBackgroundOpacityChanged));
		EventManager.AddEventListener("Event_OnMinimapScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapScaleChanged));
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00043154 File Offset: 0x00041354
	private void Start()
	{
		this.uiMinimap.SetOpacity(SettingsManager.MinimapOpacity);
		this.uiMinimap.SetBackgroundOpacity(SettingsManager.MinimapBackgroundOpacity);
		this.uiMinimap.SetPosition(new Vector2(SettingsManager.MinimapHorizontalPosition, SettingsManager.MinimapVerticalPosition));
		this.uiMinimap.SetScale(SettingsManager.MinimapScale);
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x000431AC File Offset: 0x000413AC
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnLevelSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnLevelSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerNumberChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerNumberChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodyDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodyDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckDespawned));
		EventManager.RemoveEventListener("Event_OnShowMinimapChanged", new Action<Dictionary<string, object>>(this.Event_OnShowMinimapChanged));
		EventManager.RemoveEventListener("Event_OnMinimapOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapOpacityChanged));
		EventManager.RemoveEventListener("Event_OnShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(this.Event_OnShowGameUserInterfaceChanged));
		EventManager.RemoveEventListener("Event_OnMinimapHorizontalPositionChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapHorizontalPositionChanged));
		EventManager.RemoveEventListener("Event_OnMinimapVerticalPositionChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapVerticalPositionChanged));
		EventManager.RemoveEventListener("Event_OnMinimapBackgroundOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapBackgroundOpacityChanged));
		EventManager.RemoveEventListener("Event_OnMinimapScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapScaleChanged));
		base.OnDestroy();
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x0004330C File Offset: 0x0004150C
	private void HandlePlayerGameState(Player player)
	{
		PlayerGameState value = player.GameState.Value;
		this.uiMinimap.Team = value.Team;
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00043338 File Offset: 0x00041538
	private void Event_Everyone_OnLevelSpawned(Dictionary<string, object> message)
	{
		Level level = (Level)message["level"];
		this.uiMinimap.Bounds = level.Bounds;
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00043368 File Offset: 0x00041568
	private void Event_Everyone_OnPlayerBodySpawned(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		this.uiMinimap.AddPlayerBody(playerBody);
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00043394 File Offset: 0x00041594
	private void Event_Everyone_OnPlayerSpawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (!player.IsLocalPlayer)
		{
			return;
		}
		this.HandlePlayerGameState(player);
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x000433C4 File Offset: 0x000415C4
	private void Event_Everyone_OnPlayerGameStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsLocalPlayer)
		{
			this.HandlePlayerGameState(player);
		}
		this.uiMinimap.StylePlayer(player.PlayerBody);
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00043404 File Offset: 0x00041604
	private void Event_Everyone_OnPlayerNumberChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.uiMinimap.StylePlayer(player.PlayerBody);
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00043434 File Offset: 0x00041634
	private void Event_Everyone_OnPlayerBodyDespawned(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		this.uiMinimap.RemovePlayerBody(playerBody);
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00043460 File Offset: 0x00041660
	private void Event_Everyone_OnPuckSpawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		this.uiMinimap.AddPuck(puck);
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0004348C File Offset: 0x0004168C
	private void Event_Everyone_OnPuckDespawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		this.uiMinimap.RemovePuck(puck);
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x000434B8 File Offset: 0x000416B8
	private void Event_OnMinimapOpacityChanged(Dictionary<string, object> message)
	{
		float opacity = (float)message["value"];
		this.uiMinimap.SetOpacity(opacity);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00011F0D File Offset: 0x0001010D
	private void Event_OnShowMinimapChanged(Dictionary<string, object> message)
	{
		if (GlobalStateManager.UIState.Phase == UIPhase.LockerRoom)
		{
			return;
		}
		if ((bool)message["value"])
		{
			this.uiMinimap.Show();
			return;
		}
		this.uiMinimap.Hide();
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00011F0D File Offset: 0x0001010D
	private void Event_OnShowGameUserInterfaceChanged(Dictionary<string, object> message)
	{
		if (GlobalStateManager.UIState.Phase == UIPhase.LockerRoom)
		{
			return;
		}
		if ((bool)message["value"])
		{
			this.uiMinimap.Show();
			return;
		}
		this.uiMinimap.Hide();
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x000434E4 File Offset: 0x000416E4
	private void Event_OnMinimapHorizontalPositionChanged(Dictionary<string, object> message)
	{
		float x = (float)message["value"];
		this.uiMinimap.SetPosition(new Vector2(x, this.uiMinimap.Position.y));
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x00043524 File Offset: 0x00041724
	private void Event_OnMinimapVerticalPositionChanged(Dictionary<string, object> message)
	{
		float y = (float)message["value"];
		this.uiMinimap.SetPosition(new Vector2(this.uiMinimap.Position.x, y));
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00043564 File Offset: 0x00041764
	private void Event_OnMinimapBackgroundOpacityChanged(Dictionary<string, object> message)
	{
		float backgroundOpacity = (float)message["value"];
		this.uiMinimap.SetBackgroundOpacity(backgroundOpacity);
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00043590 File Offset: 0x00041790
	private void Event_OnMinimapScaleChanged(Dictionary<string, object> message)
	{
		float scale = (float)message["value"];
		this.uiMinimap.SetScale(scale);
	}

	// Token: 0x0400073A RID: 1850
	private UIMinimap uiMinimap;
}
