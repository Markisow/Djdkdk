using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class PlayerManagerController : MonoBehaviour
{
	// Token: 0x06000737 RID: 1847 RVA: 0x00031B7C File Offset: 0x0002FD7C
	private void Awake()
	{
		this.playerManager = base.GetComponent<PlayerManager>();
		EventManager.AddEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerDespawned));
		EventManager.AddEventListener("Event_Server_OnApprovedClientConnected", new Action<Dictionary<string, object>>(this.Event_Server_OnApprovedClientConnected));
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00031BD8 File Offset: 0x0002FDD8
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerDespawned));
		EventManager.RemoveEventListener("Event_Server_OnApprovedClientConnected", new Action<Dictionary<string, object>>(this.Event_Server_OnApprovedClientConnected));
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00031C28 File Offset: 0x0002FE28
	private void Event_Everyone_OnPlayerSpawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.playerManager.AddPlayer(player);
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00031C54 File Offset: 0x0002FE54
	private void Event_Everyone_OnPlayerDespawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		this.playerManager.RemovePlayer(player);
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00031C80 File Offset: 0x0002FE80
	private void Event_Server_OnApprovedClientConnected(Dictionary<string, object> message)
	{
		ulong clientId = (ulong)message["clientId"];
		ConnectionApproval connectionApproval = (ConnectionApproval)message["connectionApproval"];
		ConnectionData connectionData = connectionApproval.ConnectionData;
		PlayerData playerData = connectionApproval.PlayerData;
		PlayerGameState gameState = new PlayerGameState
		{
			Phase = PlayerPhase.TeamSelect,
			Team = PlayerTeam.None,
			Role = PlayerRole.None
		};
		PlayerCustomizationState customizationState = new PlayerCustomizationState
		{
			FlagID = connectionData.FlagID,
			HeadgearIDBlueAttacker = connectionData.HeadgearIDBlueAttacker,
			HeadgearIDRedAttacker = connectionData.HeadgearIDRedAttacker,
			HeadgearIDBlueGoalie = connectionData.HeadgearIDBlueGoalie,
			HeadgearIDRedGoalie = connectionData.HeadgearIDRedGoalie,
			MustacheID = connectionData.MustacheID,
			BeardID = connectionData.BeardID,
			JerseyIDBlueAttacker = connectionData.JerseyIDBlueAttacker,
			JerseyIDRedAttacker = connectionData.JerseyIDRedAttacker,
			JerseyIDBlueGoalie = connectionData.JerseyIDBlueGoalie,
			JerseyIDRedGoalie = connectionData.JerseyIDRedGoalie,
			StickSkinIDBlueAttacker = connectionData.StickSkinIDBlueAttacker,
			StickSkinIDRedAttacker = connectionData.StickSkinIDRedAttacker,
			StickSkinIDBlueGoalie = connectionData.StickSkinIDBlueGoalie,
			StickSkinIDRedGoalie = connectionData.StickSkinIDRedGoalie,
			StickShaftTapeIDBlueAttacker = connectionData.StickShaftTapeIDBlueAttacker,
			StickShaftTapeIDRedAttacker = connectionData.StickShaftTapeIDRedAttacker,
			StickShaftTapeIDBlueGoalie = connectionData.StickShaftTapeIDBlueGoalie,
			StickShaftTapeIDRedGoalie = connectionData.StickShaftTapeIDRedGoalie,
			StickBladeTapeIDBlueAttacker = connectionData.StickBladeTapeIDBlueAttacker,
			StickBladeTapeIDRedAttacker = connectionData.StickBladeTapeIDRedAttacker,
			StickBladeTapeIDBlueGoalie = connectionData.StickBladeTapeIDBlueGoalie,
			StickBladeTapeIDRedGoalie = connectionData.StickBladeTapeIDRedGoalie
		};
		bool isMuted = BackendUtils.GetActivePlayerDataMute(playerData) != null;
		this.playerManager.Server_SpawnPlayer(clientId, gameState, customizationState, connectionData.Handedness, playerData.steamId, playerData.username, playerData.number, playerData.patreonLevel, playerData.adminLevel, isMuted, false);
	}

	// Token: 0x0400045C RID: 1116
	private static readonly global::Logger Logger = new global::Logger("PlayerManagerController");

	// Token: 0x0400045D RID: 1117
	private PlayerManager playerManager;
}
