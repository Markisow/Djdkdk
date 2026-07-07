using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class GameModeManager : MonoBehaviourSingleton<GameModeManager>
{
	// Token: 0x06000573 RID: 1395 RVA: 0x0000C541 File Offset: 0x0000A741
	public override void Awake()
	{
		base.Awake();
		this.RegisterGameModes();
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0002D65C File Offset: 0x0002B85C
	private void RegisterGameModes()
	{
		PublicGameMode<PublicGameModeConfig> value = new PublicGameMode<PublicGameModeConfig>("./public_game_mode_config.json", "--publicGameModeConfigPath", "--publicGameModeConfig", "PUCK_PUBLIC_GAME_MODE_CONFIG");
		CompetitiveGameMode<CompetitiveGameModeConfig> value2 = new CompetitiveGameMode<CompetitiveGameModeConfig>("./competitive_game_mode_config.json", "--competitiveGameModeConfigPath", "--competitiveGameModeConfig", "PUCK_COMPETITIVE_GAME_MODE_CONFIG");
		this.gameModeMap.Add("public", value);
		this.gameModeMap.Add("competitive", value2);
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0002D6C0 File Offset: 0x0002B8C0
	public void SelectGameMode(string name)
	{
		if (!this.gameModeMap.ContainsKey(name))
		{
			return;
		}
		this.selectedGameMode = this.gameModeMap[name];
		GameModeManager.Logger.Info("Selected game mode " + this.selectedGameMode.GetType().Name);
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0000C54F File Offset: 0x0000A74F
	public void DeselectGameMode()
	{
		this.selectedGameMode = null;
		GameModeManager.Logger.Info("Deselected game mode");
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0002D714 File Offset: 0x0002B914
	public void EnableSelectedGameMode()
	{
		if (this.selectedGameMode == null)
		{
			return;
		}
		if (this.selectedGameMode.IsInitialized)
		{
			return;
		}
		GameModeManager.Logger.Info("Enabling game mode " + this.selectedGameMode.GetType().Name);
		this.selectedGameMode.Initialize(this.Level, this.serverManager, this.gameManager, this.playerManager, this.puckManager, this.chatManager, this.replayManager, this.voteManager);
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0002D798 File Offset: 0x0002B998
	public void DisableSelectedGameMode()
	{
		if (this.selectedGameMode == null)
		{
			return;
		}
		if (!this.selectedGameMode.IsInitialized)
		{
			return;
		}
		GameModeManager.Logger.Info("Disabling game mode " + this.selectedGameMode.GetType().Name);
		this.selectedGameMode.Dispose();
	}

	// Token: 0x04000358 RID: 856
	private static readonly global::Logger Logger = new global::Logger("GameModeManager");

	// Token: 0x04000359 RID: 857
	[Header("References")]
	[SerializeField]
	private ServerManager serverManager;

	// Token: 0x0400035A RID: 858
	[SerializeField]
	private GameManager gameManager;

	// Token: 0x0400035B RID: 859
	[SerializeField]
	private PlayerManager playerManager;

	// Token: 0x0400035C RID: 860
	[SerializeField]
	private PuckManager puckManager;

	// Token: 0x0400035D RID: 861
	[SerializeField]
	private ChatManager chatManager;

	// Token: 0x0400035E RID: 862
	[SerializeField]
	private ReplayManager replayManager;

	// Token: 0x0400035F RID: 863
	[SerializeField]
	private VoteManager voteManager;

	// Token: 0x04000360 RID: 864
	[HideInInspector]
	public Level Level;

	// Token: 0x04000361 RID: 865
	private Dictionary<string, IGameMode> gameModeMap = new Dictionary<string, IGameMode>();

	// Token: 0x04000362 RID: 866
	private IGameMode selectedGameMode;
}
