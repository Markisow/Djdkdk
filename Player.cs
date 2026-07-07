using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class Player : NetworkBehaviour
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x0600021C RID: 540 RVA: 0x00009F9C File Offset: 0x0000819C
	// (set) Token: 0x0600021D RID: 541 RVA: 0x00009FA4 File Offset: 0x000081A4
	[HideInInspector]
	public float ChatTickets { get; private set; }

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x0600021E RID: 542 RVA: 0x00009FAD File Offset: 0x000081AD
	[HideInInspector]
	public bool IsChatAvailable
	{
		get
		{
			return this.ChatTickets >= 1f;
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x0600021F RID: 543 RVA: 0x00009FBF File Offset: 0x000081BF
	[HideInInspector]
	public PlayerPhase Phase
	{
		get
		{
			return this.GameState.Value.Phase;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000220 RID: 544 RVA: 0x00009FD1 File Offset: 0x000081D1
	[HideInInspector]
	public PlayerTeam Team
	{
		get
		{
			return this.GameState.Value.Team;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000221 RID: 545 RVA: 0x00009FE3 File Offset: 0x000081E3
	[HideInInspector]
	public PlayerRole Role
	{
		get
		{
			return this.GameState.Value.Role;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000222 RID: 546 RVA: 0x00009FF5 File Offset: 0x000081F5
	[HideInInspector]
	public int FlagID
	{
		get
		{
			return this.CustomizationState.Value.FlagID;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000223 RID: 547 RVA: 0x0000A007 File Offset: 0x00008207
	[HideInInspector]
	public int HeadgearIDBlueAttacker
	{
		get
		{
			return this.CustomizationState.Value.HeadgearIDBlueAttacker;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A019 File Offset: 0x00008219
	[HideInInspector]
	public int HeadgearIDRedAttacker
	{
		get
		{
			return this.CustomizationState.Value.HeadgearIDRedAttacker;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000225 RID: 549 RVA: 0x0000A02B File Offset: 0x0000822B
	[HideInInspector]
	public int HeadgearIDBlueGoalie
	{
		get
		{
			return this.CustomizationState.Value.HeadgearIDBlueGoalie;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000226 RID: 550 RVA: 0x0000A03D File Offset: 0x0000823D
	[HideInInspector]
	public int HeadgearIDRedGoalie
	{
		get
		{
			return this.CustomizationState.Value.HeadgearIDRedGoalie;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A04F File Offset: 0x0000824F
	[HideInInspector]
	public int MustacheID
	{
		get
		{
			return this.CustomizationState.Value.MustacheID;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A061 File Offset: 0x00008261
	[HideInInspector]
	public int BeardID
	{
		get
		{
			return this.CustomizationState.Value.BeardID;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A073 File Offset: 0x00008273
	[HideInInspector]
	public int JerseyIDBlueAttacker
	{
		get
		{
			return this.CustomizationState.Value.JerseyIDBlueAttacker;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A085 File Offset: 0x00008285
	[HideInInspector]
	public int JerseyIDRedAttacker
	{
		get
		{
			return this.CustomizationState.Value.JerseyIDRedAttacker;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A097 File Offset: 0x00008297
	[HideInInspector]
	public int JerseyIDBlueGoalie
	{
		get
		{
			return this.CustomizationState.Value.JerseyIDBlueGoalie;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A0A9 File Offset: 0x000082A9
	[HideInInspector]
	public int JerseyIDRedGoalie
	{
		get
		{
			return this.CustomizationState.Value.JerseyIDRedGoalie;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A0BB File Offset: 0x000082BB
	[HideInInspector]
	public int StickSkinIDBlueAttacker
	{
		get
		{
			return this.CustomizationState.Value.StickSkinIDBlueAttacker;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600022E RID: 558 RVA: 0x0000A0CD File Offset: 0x000082CD
	[HideInInspector]
	public int StickSkinIDRedAttacker
	{
		get
		{
			return this.CustomizationState.Value.StickSkinIDRedAttacker;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A0DF File Offset: 0x000082DF
	[HideInInspector]
	public int StickSkinIDBlueGoalie
	{
		get
		{
			return this.CustomizationState.Value.StickSkinIDBlueGoalie;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000230 RID: 560 RVA: 0x0000A0F1 File Offset: 0x000082F1
	[HideInInspector]
	public int StickSkinIDRedGoalie
	{
		get
		{
			return this.CustomizationState.Value.StickSkinIDRedGoalie;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A103 File Offset: 0x00008303
	[HideInInspector]
	public int StickShaftTapeIDBlueAttacker
	{
		get
		{
			return this.CustomizationState.Value.StickShaftTapeIDBlueAttacker;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A115 File Offset: 0x00008315
	[HideInInspector]
	public int StickShaftTapeIDRedAttacker
	{
		get
		{
			return this.CustomizationState.Value.StickShaftTapeIDRedAttacker;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A127 File Offset: 0x00008327
	[HideInInspector]
	public int StickShaftTapeIDBlueGoalie
	{
		get
		{
			return this.CustomizationState.Value.StickShaftTapeIDBlueGoalie;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000234 RID: 564 RVA: 0x0000A139 File Offset: 0x00008339
	[HideInInspector]
	public int StickShaftTapeIDRedGoalie
	{
		get
		{
			return this.CustomizationState.Value.StickShaftTapeIDRedGoalie;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A14B File Offset: 0x0000834B
	[HideInInspector]
	public int StickBladeTapeIDBlueAttacker
	{
		get
		{
			return this.CustomizationState.Value.StickBladeTapeIDBlueAttacker;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000236 RID: 566 RVA: 0x0000A15D File Offset: 0x0000835D
	[HideInInspector]
	public int StickBladeTapeIDRedAttacker
	{
		get
		{
			return this.CustomizationState.Value.StickBladeTapeIDRedAttacker;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A16F File Offset: 0x0000836F
	[HideInInspector]
	public int StickBladeTapeIDBlueGoalie
	{
		get
		{
			return this.CustomizationState.Value.StickBladeTapeIDBlueGoalie;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000238 RID: 568 RVA: 0x0000A181 File Offset: 0x00008381
	[HideInInspector]
	public int StickBladeTapeIDRedGoalie
	{
		get
		{
			return this.CustomizationState.Value.StickBladeTapeIDRedGoalie;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A193 File Offset: 0x00008393
	[HideInInspector]
	public bool IsCharacterSpawned
	{
		get
		{
			return this.PlayerCamera && this.PlayerBody && this.StickPositioner && this.Stick;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600023A RID: 570 RVA: 0x0000A1C9 File Offset: 0x000083C9
	[HideInInspector]
	public bool IsSpectatorCameraSpawned
	{
		get
		{
			return this.SpectatorCamera != null;
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000A1D7 File Offset: 0x000083D7
	private void Awake()
	{
		this.PlayerInput = base.GetComponent<PlayerInput>();
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00020170 File Offset: 0x0001E370
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(default(PlayerGameState), default(PlayerCustomizationState), PlayerHandedness.None, default(FixedString32Bytes), default(FixedString32Bytes), 0, 0, 0, 0, 0, 0UL, default(NetworkObjectReference), false, false);
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x0600023D RID: 573 RVA: 0x000201C4 File Offset: 0x0001E3C4
	public override void OnNetworkSpawn()
	{
		NetworkVariable<PlayerGameState> gameState = this.GameState;
		gameState.OnValueChanged = (NetworkVariable<PlayerGameState>.OnValueChangedDelegate)Delegate.Combine(gameState.OnValueChanged, new NetworkVariable<PlayerGameState>.OnValueChangedDelegate(this.OnPlayerGameStateChanged));
		NetworkVariable<PlayerCustomizationState> customizationState = this.CustomizationState;
		customizationState.OnValueChanged = (NetworkVariable<PlayerCustomizationState>.OnValueChangedDelegate)Delegate.Combine(customizationState.OnValueChanged, new NetworkVariable<PlayerCustomizationState>.OnValueChangedDelegate(this.OnPlayerCustomizationStateChanged));
		NetworkVariable<PlayerHandedness> handedness = this.Handedness;
		handedness.OnValueChanged = (NetworkVariable<PlayerHandedness>.OnValueChangedDelegate)Delegate.Combine(handedness.OnValueChanged, new NetworkVariable<PlayerHandedness>.OnValueChangedDelegate(this.OnPlayerHandednessChanged));
		NetworkVariable<FixedString32Bytes> steamId = this.SteamId;
		steamId.OnValueChanged = (NetworkVariable<FixedString32Bytes>.OnValueChangedDelegate)Delegate.Combine(steamId.OnValueChanged, new NetworkVariable<FixedString32Bytes>.OnValueChangedDelegate(this.OnPlayerSteamIdChanged));
		NetworkVariable<FixedString32Bytes> username = this.Username;
		username.OnValueChanged = (NetworkVariable<FixedString32Bytes>.OnValueChangedDelegate)Delegate.Combine(username.OnValueChanged, new NetworkVariable<FixedString32Bytes>.OnValueChangedDelegate(this.OnPlayerUsernameChanged));
		NetworkVariable<int> number = this.Number;
		number.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Combine(number.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerNumberChanged));
		NetworkVariable<int> patreonLevel = this.PatreonLevel;
		patreonLevel.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Combine(patreonLevel.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerPatreonLevelChanged));
		NetworkVariable<int> adminLevel = this.AdminLevel;
		adminLevel.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Combine(adminLevel.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerAdminLevelChanged));
		NetworkVariable<int> goals = this.Goals;
		goals.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Combine(goals.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerGoalsChanged));
		NetworkVariable<int> assists = this.Assists;
		assists.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Combine(assists.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerAssistsChanged));
		NetworkVariable<ulong> ping = this.Ping;
		ping.OnValueChanged = (NetworkVariable<ulong>.OnValueChangedDelegate)Delegate.Combine(ping.OnValueChanged, new NetworkVariable<ulong>.OnValueChangedDelegate(this.OnPlayerPingChanged));
		NetworkVariable<NetworkObjectReference> playerPositionReference = this.PlayerPositionReference;
		playerPositionReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Combine(playerPositionReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerPlayerPositionReferenceChanged));
		NetworkVariable<bool> isMuted = this.IsMuted;
		isMuted.OnValueChanged = (NetworkVariable<bool>.OnValueChangedDelegate)Delegate.Combine(isMuted.OnValueChanged, new NetworkVariable<bool>.OnValueChangedDelegate(this.OnPlayerIsMutedChanged));
		NetworkVariable<bool> isReplay = this.IsReplay;
		isReplay.OnValueChanged = (NetworkVariable<bool>.OnValueChangedDelegate)Delegate.Combine(isReplay.OnValueChanged, new NetworkVariable<bool>.OnValueChangedDelegate(this.OnPlayerIsReplayChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000A1E5 File Offset: 0x000083E5
	protected override void OnNetworkPostSpawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerSpawned", new Dictionary<string, object>
		{
			{
				"player",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x0600023F RID: 575 RVA: 0x000203FC File Offset: 0x0001E5FC
	private void Update()
	{
		if (NetworkManager.Singleton.IsServer && this.ChatTickets < (float)this.maxChatTickets)
		{
			this.ChatTickets += this.ChatTicketsPerSecond * Time.deltaTime;
			if (this.ChatTickets > (float)this.maxChatTickets)
			{
				this.ChatTickets = (float)this.maxChatTickets;
			}
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0002045C File Offset: 0x0001E65C
	public override void OnNetworkDespawn()
	{
		if (NetworkManager.Singleton.IsServer)
		{
			this.Server_CancelDelayedGameState();
			if (this.IsCharacterSpawned)
			{
				this.Server_DespawnCharacter();
			}
			if (this.IsSpectatorCameraSpawned)
			{
				this.Server_DespawnSpectatorCamera();
			}
		}
		Player.Logger.Info(string.Format("Despawning Player ({0})", base.OwnerClientId));
		EventManager.TriggerEvent("Event_Everyone_OnPlayerDespawned", new Dictionary<string, object>
		{
			{
				"player",
				this
			}
		});
		NetworkVariable<PlayerGameState> gameState = this.GameState;
		gameState.OnValueChanged = (NetworkVariable<PlayerGameState>.OnValueChangedDelegate)Delegate.Remove(gameState.OnValueChanged, new NetworkVariable<PlayerGameState>.OnValueChangedDelegate(this.OnPlayerGameStateChanged));
		NetworkVariable<PlayerCustomizationState> customizationState = this.CustomizationState;
		customizationState.OnValueChanged = (NetworkVariable<PlayerCustomizationState>.OnValueChangedDelegate)Delegate.Remove(customizationState.OnValueChanged, new NetworkVariable<PlayerCustomizationState>.OnValueChangedDelegate(this.OnPlayerCustomizationStateChanged));
		NetworkVariable<PlayerHandedness> handedness = this.Handedness;
		handedness.OnValueChanged = (NetworkVariable<PlayerHandedness>.OnValueChangedDelegate)Delegate.Remove(handedness.OnValueChanged, new NetworkVariable<PlayerHandedness>.OnValueChangedDelegate(this.OnPlayerHandednessChanged));
		NetworkVariable<FixedString32Bytes> steamId = this.SteamId;
		steamId.OnValueChanged = (NetworkVariable<FixedString32Bytes>.OnValueChangedDelegate)Delegate.Remove(steamId.OnValueChanged, new NetworkVariable<FixedString32Bytes>.OnValueChangedDelegate(this.OnPlayerSteamIdChanged));
		NetworkVariable<FixedString32Bytes> username = this.Username;
		username.OnValueChanged = (NetworkVariable<FixedString32Bytes>.OnValueChangedDelegate)Delegate.Remove(username.OnValueChanged, new NetworkVariable<FixedString32Bytes>.OnValueChangedDelegate(this.OnPlayerUsernameChanged));
		NetworkVariable<int> number = this.Number;
		number.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Remove(number.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerNumberChanged));
		NetworkVariable<int> patreonLevel = this.PatreonLevel;
		patreonLevel.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Remove(patreonLevel.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerPatreonLevelChanged));
		NetworkVariable<int> adminLevel = this.AdminLevel;
		adminLevel.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Remove(adminLevel.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerAdminLevelChanged));
		NetworkVariable<int> goals = this.Goals;
		goals.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Remove(goals.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerGoalsChanged));
		NetworkVariable<int> assists = this.Assists;
		assists.OnValueChanged = (NetworkVariable<int>.OnValueChangedDelegate)Delegate.Remove(assists.OnValueChanged, new NetworkVariable<int>.OnValueChangedDelegate(this.OnPlayerAssistsChanged));
		NetworkVariable<ulong> ping = this.Ping;
		ping.OnValueChanged = (NetworkVariable<ulong>.OnValueChangedDelegate)Delegate.Remove(ping.OnValueChanged, new NetworkVariable<ulong>.OnValueChangedDelegate(this.OnPlayerPingChanged));
		NetworkVariable<NetworkObjectReference> playerPositionReference = this.PlayerPositionReference;
		playerPositionReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Remove(playerPositionReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerPlayerPositionReferenceChanged));
		NetworkVariable<bool> isMuted = this.IsMuted;
		isMuted.OnValueChanged = (NetworkVariable<bool>.OnValueChangedDelegate)Delegate.Remove(isMuted.OnValueChanged, new NetworkVariable<bool>.OnValueChangedDelegate(this.OnPlayerIsMutedChanged));
		NetworkVariable<bool> isReplay = this.IsReplay;
		isReplay.OnValueChanged = (NetworkVariable<bool>.OnValueChangedDelegate)Delegate.Remove(isReplay.OnValueChanged, new NetworkVariable<bool>.OnValueChangedDelegate(this.OnPlayerIsReplayChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x06000241 RID: 577 RVA: 0x000206FC File Offset: 0x0001E8FC
	public void InitializeNetworkVariables(PlayerGameState gameState = default(PlayerGameState), PlayerCustomizationState customizationState = default(PlayerCustomizationState), PlayerHandedness handedness = PlayerHandedness.None, FixedString32Bytes steamID = default(FixedString32Bytes), FixedString32Bytes username = default(FixedString32Bytes), int number = 0, int patreonLevel = 0, int adminLevel = 0, int goals = 0, int assists = 0, ulong ping = 0UL, NetworkObjectReference playerPositionReference = default(NetworkObjectReference), bool isMuted = false, bool isReplay = false)
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.GameState = new NetworkVariable<PlayerGameState>(gameState, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.CustomizationState = new NetworkVariable<PlayerCustomizationState>(customizationState, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.Handedness = new NetworkVariable<PlayerHandedness>(handedness, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.SteamId = new NetworkVariable<FixedString32Bytes>(steamID, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.Username = new NetworkVariable<FixedString32Bytes>(username, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.Number = new NetworkVariable<int>(number, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.PatreonLevel = new NetworkVariable<int>(patreonLevel, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.AdminLevel = new NetworkVariable<int>(adminLevel, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.Goals = new NetworkVariable<int>(goals, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.Assists = new NetworkVariable<int>(assists, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.Ping = new NetworkVariable<ulong>(ping, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.PlayerPositionReference = new NetworkVariable<NetworkObjectReference>(playerPositionReference, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.IsMuted = new NetworkVariable<bool>(isMuted, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		this.IsReplay = new NetworkVariable<bool>(isReplay, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x000207E8 File Offset: 0x0001E9E8
	public int GetPlayerJerseyID()
	{
		PlayerTeam team = this.Team;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				return 0;
			}
			if (this.Role != PlayerRole.Attacker)
			{
				return this.JerseyIDRedGoalie;
			}
			return this.JerseyIDRedAttacker;
		}
		else
		{
			if (this.Role != PlayerRole.Attacker)
			{
				return this.JerseyIDBlueGoalie;
			}
			return this.JerseyIDBlueAttacker;
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00020838 File Offset: 0x0001EA38
	public int GetPlayerHeadgearID()
	{
		PlayerTeam team = this.Team;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				return 0;
			}
			if (this.Role != PlayerRole.Attacker)
			{
				return this.HeadgearIDRedGoalie;
			}
			return this.HeadgearIDRedAttacker;
		}
		else
		{
			if (this.Role != PlayerRole.Attacker)
			{
				return this.HeadgearIDBlueGoalie;
			}
			return this.HeadgearIDBlueAttacker;
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00020888 File Offset: 0x0001EA88
	public int GetPlayerStickSkinID()
	{
		PlayerTeam team = this.Team;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				return 0;
			}
			if (this.Role != PlayerRole.Attacker)
			{
				return this.StickSkinIDRedGoalie;
			}
			return this.StickSkinIDRedAttacker;
		}
		else
		{
			if (this.Role != PlayerRole.Attacker)
			{
				return this.StickSkinIDBlueGoalie;
			}
			return this.StickSkinIDBlueAttacker;
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x000208D8 File Offset: 0x0001EAD8
	public int GetPlayerStickShaftTapeID()
	{
		PlayerTeam team = this.Team;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				return 0;
			}
			if (this.Role != PlayerRole.Attacker)
			{
				return this.StickShaftTapeIDRedGoalie;
			}
			return this.StickShaftTapeIDRedAttacker;
		}
		else
		{
			if (this.Role != PlayerRole.Attacker)
			{
				return this.StickShaftTapeIDBlueGoalie;
			}
			return this.StickShaftTapeIDBlueAttacker;
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00020928 File Offset: 0x0001EB28
	public int GetPlayerStickBladeTapeID()
	{
		PlayerTeam team = this.Team;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				return 0;
			}
			if (this.Role != PlayerRole.Attacker)
			{
				return this.StickBladeTapeIDRedGoalie;
			}
			return this.StickBladeTapeIDRedAttacker;
		}
		else
		{
			if (this.Role != PlayerRole.Attacker)
			{
				return this.StickBladeTapeIDBlueGoalie;
			}
			return this.StickBladeTapeIDBlueAttacker;
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000A208 File Offset: 0x00008408
	private void OnPlayerGameStateChanged(PlayerGameState oldGameState, PlayerGameState newGameState)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerGameStateChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldGameState",
				oldGameState
			},
			{
				"newGameState",
				newGameState
			}
		});
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000A247 File Offset: 0x00008447
	private void OnPlayerCustomizationStateChanged(PlayerCustomizationState oldCustomizationState, PlayerCustomizationState newCustomizationState)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerCustomizationStateChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldCustomizationState",
				oldCustomizationState
			},
			{
				"newCustomizationState",
				newCustomizationState
			}
		});
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000A286 File Offset: 0x00008486
	private void OnPlayerHandednessChanged(PlayerHandedness oldHandedness, PlayerHandedness newHandedness)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerHandednessChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldHandedness",
				oldHandedness
			},
			{
				"newHandedness",
				newHandedness
			}
		});
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000A2C5 File Offset: 0x000084C5
	private void OnPlayerSteamIdChanged(FixedString32Bytes oldSteamId, FixedString32Bytes newSteamId)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerSteamIdChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldSteamId",
				oldSteamId
			},
			{
				"newSteamId",
				newSteamId
			}
		});
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000A304 File Offset: 0x00008504
	private void OnPlayerUsernameChanged(FixedString32Bytes oldUsername, FixedString32Bytes newUsername)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerUsernameChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldUsername",
				oldUsername
			},
			{
				"newUsername",
				newUsername
			}
		});
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000A343 File Offset: 0x00008543
	private void OnPlayerNumberChanged(int oldNumber, int newNumber)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerNumberChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldNumber",
				oldNumber
			},
			{
				"newNumber",
				newNumber
			}
		});
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000A382 File Offset: 0x00008582
	private void OnPlayerPatreonLevelChanged(int oldPatreonLevel, int newPatreonLevel)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerPatreonLevelChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldPatreonLevel",
				oldPatreonLevel
			},
			{
				"newPatreonLevel",
				newPatreonLevel
			}
		});
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000A3C1 File Offset: 0x000085C1
	private void OnPlayerAdminLevelChanged(int oldAdminLevel, int newAdminLevel)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerAdminLevelChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldAdminLevel",
				oldAdminLevel
			},
			{
				"newAdminLevel",
				newAdminLevel
			}
		});
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000A400 File Offset: 0x00008600
	private void OnPlayerGoalsChanged(int oldGoals, int newGoals)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerGoalsChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldGoals",
				oldGoals
			},
			{
				"newGoals",
				newGoals
			}
		});
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000A43F File Offset: 0x0000863F
	private void OnPlayerAssistsChanged(int oldAssists, int newAssists)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerAssistsChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldAssists",
				oldAssists
			},
			{
				"newAssists",
				newAssists
			}
		});
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000A47E File Offset: 0x0000867E
	private void OnPlayerPingChanged(ulong oldPing, ulong newPing)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerPingChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldPing",
				oldPing
			},
			{
				"newPing",
				newPing
			}
		});
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00020978 File Offset: 0x0001EB78
	private void OnPlayerPlayerPositionReferenceChanged(NetworkObjectReference oldPlayerPositionReference, NetworkObjectReference newPlayerPositionReference)
	{
		PlayerPosition playerPositionFromNetworkObjectReference = NetworkingUtils.GetPlayerPositionFromNetworkObjectReference(oldPlayerPositionReference);
		PlayerPosition playerPositionFromNetworkObjectReference2 = NetworkingUtils.GetPlayerPositionFromNetworkObjectReference(newPlayerPositionReference);
		this.PlayerPosition = playerPositionFromNetworkObjectReference2;
		this.OnPlayerPositionChanged(playerPositionFromNetworkObjectReference, this.PlayerPosition);
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000A4BD File Offset: 0x000086BD
	private void OnPlayerPositionChanged(PlayerPosition oldPlayerPosition, PlayerPosition newPlayerPosition)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerPositionChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldPlayerPosition",
				oldPlayerPosition
			},
			{
				"newPlayerPosition",
				newPlayerPosition
			}
		});
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000A4F2 File Offset: 0x000086F2
	private void OnPlayerIsMutedChanged(bool oldIsMuted, bool newIsMuted)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerIsMutedChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldIsMuted",
				oldIsMuted
			},
			{
				"newIsMuted",
				newIsMuted
			}
		});
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000A531 File Offset: 0x00008731
	private void OnPlayerIsReplayChanged(bool oldIsReplay, bool newIsReplay)
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerIsReplayChanged", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"oldIsReplay",
				oldIsReplay
			},
			{
				"newIsReplay",
				newIsReplay
			}
		});
	}

	// Token: 0x06000256 RID: 598 RVA: 0x000209A8 File Offset: 0x0001EBA8
	[Rpc(SendTo.Server, DeferLocal = true)]
	public void Client_RequestTeamRpc(PlayerTeam team, RpcParams rpcParams = default(RpcParams))
	{
		NetworkManager networkManager = base.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			uint rpcMethodId = 2620210071U;
			RpcAttribute.RpcAttributeParams attributeParams = new RpcAttribute.RpcAttributeParams
			{
				DeferLocal = true
			};
			FastBufferWriter fastBufferWriter = base.__beginSendRpc(rpcMethodId, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
			fastBufferWriter.WriteValueSafe<PlayerTeam>(team, default(FastBufferWriter.ForEnums));
			base.__endSendRpc(ref fastBufferWriter, 2620210071U, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		this.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
		if (rpcParams.Receive.SenderClientId != base.OwnerClientId)
		{
			return;
		}
		EventManager.TriggerEvent("Event_Server_OnPlayerRequestTeam", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"team",
				team
			}
		});
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00020AD0 File Offset: 0x0001ECD0
	[Rpc(SendTo.Server, DeferLocal = true)]
	public void Client_RequestClaimPositionRpc(NetworkObjectReference playerPositionReference, RpcParams rpcParams = default(RpcParams))
	{
		NetworkManager networkManager = base.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			uint rpcMethodId = 949682089U;
			RpcAttribute.RpcAttributeParams attributeParams = new RpcAttribute.RpcAttributeParams
			{
				DeferLocal = true
			};
			FastBufferWriter fastBufferWriter = base.__beginSendRpc(rpcMethodId, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
			fastBufferWriter.WriteValueSafe<NetworkObjectReference>(playerPositionReference, default(FastBufferWriter.ForNetworkSerializable));
			base.__endSendRpc(ref fastBufferWriter, 949682089U, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		this.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
		if (rpcParams.Receive.SenderClientId != base.OwnerClientId)
		{
			return;
		}
		PlayerPosition playerPositionFromNetworkObjectReference = NetworkingUtils.GetPlayerPositionFromNetworkObjectReference(playerPositionReference);
		EventManager.TriggerEvent("Event_Server_OnPlayerRequestPosition", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"playerPosition",
				playerPositionFromNetworkObjectReference
			}
		});
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00020BF8 File Offset: 0x0001EDF8
	[Rpc(SendTo.Server, DeferLocal = true)]
	public void Client_RequestTeamSelectRpc(RpcParams rpcParams = default(RpcParams))
	{
		NetworkManager networkManager = base.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			uint rpcMethodId = 4280154797U;
			RpcAttribute.RpcAttributeParams attributeParams = new RpcAttribute.RpcAttributeParams
			{
				DeferLocal = true
			};
			FastBufferWriter fastBufferWriter = base.__beginSendRpc(rpcMethodId, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
			base.__endSendRpc(ref fastBufferWriter, 4280154797U, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		this.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
		if (rpcParams.Receive.SenderClientId != base.OwnerClientId)
		{
			return;
		}
		EventManager.TriggerEvent("Event_Server_OnPlayerRequestTeamSelect", new Dictionary<string, object>
		{
			{
				"player",
				this
			}
		});
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00020CF4 File Offset: 0x0001EEF4
	[Rpc(SendTo.Server, DeferLocal = true)]
	public void Client_RequestPositionSelectRpc(RpcParams rpcParams = default(RpcParams))
	{
		NetworkManager networkManager = base.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			uint rpcMethodId = 3454979199U;
			RpcAttribute.RpcAttributeParams attributeParams = new RpcAttribute.RpcAttributeParams
			{
				DeferLocal = true
			};
			FastBufferWriter fastBufferWriter = base.__beginSendRpc(rpcMethodId, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
			base.__endSendRpc(ref fastBufferWriter, 3454979199U, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		this.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
		if (rpcParams.Receive.SenderClientId != base.OwnerClientId)
		{
			return;
		}
		EventManager.TriggerEvent("Event_Server_OnPlayerRequestPositionSelect", new Dictionary<string, object>
		{
			{
				"player",
				this
			}
		});
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00020DF0 File Offset: 0x0001EFF0
	[Rpc(SendTo.Server, DeferLocal = true)]
	public void Client_RequestHandednessRpc(PlayerHandedness handedness, RpcParams rpcParams = default(RpcParams))
	{
		NetworkManager networkManager = base.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			uint rpcMethodId = 744616166U;
			RpcAttribute.RpcAttributeParams attributeParams = new RpcAttribute.RpcAttributeParams
			{
				DeferLocal = true
			};
			FastBufferWriter fastBufferWriter = base.__beginSendRpc(rpcMethodId, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
			fastBufferWriter.WriteValueSafe<PlayerHandedness>(handedness, default(FastBufferWriter.ForEnums));
			base.__endSendRpc(ref fastBufferWriter, 744616166U, rpcParams, attributeParams, SendTo.Server, RpcDelivery.Reliable);
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		this.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
		if (rpcParams.Receive.SenderClientId != base.OwnerClientId)
		{
			return;
		}
		EventManager.TriggerEvent("Event_Server_OnPlayerRequestHandedness", new Dictionary<string, object>
		{
			{
				"player",
				this
			},
			{
				"handedness",
				handedness
			}
		});
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00020F18 File Offset: 0x0001F118
	public void Server_SetGameState(PlayerPhase? phase = null, PlayerTeam? team = null, PlayerRole? role = null, float? delay = null)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Server_CancelDelayedGameState();
		if (delay == null)
		{
			PlayerGameState value = new PlayerGameState
			{
				Phase = (phase ?? this.Phase),
				Team = (team ?? this.Team),
				Role = (role ?? this.Role)
			};
			this.GameState.Value = value;
			return;
		}
		this.delayedGameStateTween = DOVirtual.DelayedCall(delay.Value, delegate
		{
			this.Server_SetGameState(phase, team, role, null);
		}, true);
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000A570 File Offset: 0x00008770
	public void Server_CancelDelayedGameState()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Tween tween = this.delayedGameStateTween;
		if (tween == null)
		{
			return;
		}
		tween.Kill(false);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00021010 File Offset: 0x0001F210
	public void Server_SpawnCharacter(Vector3 position, Quaternion rotation, PlayerRole role)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Spawning character ({0})", base.OwnerClientId));
		if (this.IsCharacterSpawned)
		{
			this.Server_DespawnCharacter();
		}
		if (this.IsSpectatorCameraSpawned)
		{
			this.Server_DespawnSpectatorCamera();
		}
		this.Server_SpawnPlayerBody(position, rotation, role);
		this.Server_SpawnStick(this.StickPositioner.RaycastOriginPosition, Quaternion.LookRotation(this.PlayerBody.transform.right, Vector3.up), role);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0002109C File Offset: 0x0001F29C
	public void Server_SpawnPlayerCamera(PlayerBody playerBody)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Spawning PlayerCamera ({0})", base.OwnerClientId));
		PlayerCamera playerCamera = UnityEngine.Object.Instantiate<PlayerCamera>(this.playerCameraPrefab);
		playerCamera.InitializeNetworkVariables(new NetworkObjectReference(base.NetworkObject));
		playerCamera.NetworkObject.SpawnWithOwnership(base.OwnerClientId, false);
		if (!playerCamera.NetworkObject.TrySetParent(playerBody.transform, false))
		{
			Player.Logger.Error(string.Format("Failed to set parent for PlayerCamera ({0})", base.OwnerClientId));
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00021138 File Offset: 0x0001F338
	public void Server_SpawnPlayerBody(Vector3 position, Quaternion rotation, PlayerRole role)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Spawning PlayerBody ({0})", base.OwnerClientId));
		PlayerBody playerBody = null;
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				playerBody = this.playerBodyGoaliePrefab;
			}
		}
		else
		{
			playerBody = this.playerBodyAttackerPrefab;
		}
		if (playerBody == null)
		{
			Player.Logger.Error(string.Format("Failed to spawn PlayerBody ({0}): Missing prefab for role {1}", base.OwnerClientId, role));
			return;
		}
		PlayerBody playerBody2 = UnityEngine.Object.Instantiate<PlayerBody>(playerBody, position, rotation);
		playerBody2.InitializeNetworkVariables(new NetworkObjectReference(base.NetworkObject), 1f, 0f, false, false, false, false, false);
		playerBody2.NetworkObject.SpawnWithOwnership(base.OwnerClientId, false);
		if (playerBody2.NetworkObject.TrySetParent(base.gameObject.transform, true))
		{
			this.Server_SpawnPlayerCamera(playerBody2);
			this.Server_SpawnStickPositioner(playerBody2);
			return;
		}
		Player.Logger.Error(string.Format("Failed to set parent for PlayerBody ({0})", base.OwnerClientId));
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00021240 File Offset: 0x0001F440
	public void Server_SpawnStickPositioner(PlayerBody playerBody)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Spawning StickPositioner ({0})", base.OwnerClientId));
		StickPositioner stickPositioner = UnityEngine.Object.Instantiate<StickPositioner>(this.stickPositionerPrefab);
		stickPositioner.InitializeNetworkVariables(new NetworkObjectReference(base.NetworkObject));
		stickPositioner.NetworkObject.SpawnWithOwnership(base.OwnerClientId, false);
		if (!stickPositioner.NetworkObject.TrySetParent(playerBody.transform, false))
		{
			Player.Logger.Error(string.Format("Failed to set parent for StickPositioner ({0})", base.OwnerClientId));
		}
	}

	// Token: 0x06000261 RID: 609 RVA: 0x000212DC File Offset: 0x0001F4DC
	public void Server_SpawnStick(Vector3 position, Quaternion rotation, PlayerRole role)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Spawning Stick ({0})", base.OwnerClientId));
		Stick stick = null;
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				stick = this.stickGoaliePrefab;
			}
		}
		else
		{
			stick = this.stickAttackerPrefab;
		}
		if (stick == null)
		{
			Player.Logger.Error(string.Format("Failed to spawn Stick ({0}): Missing prefab for role {1}", base.OwnerClientId, role));
			return;
		}
		Stick stick2 = UnityEngine.Object.Instantiate<Stick>(stick, position, rotation);
		stick2.InitializeNetworkVariables(new NetworkObjectReference(base.NetworkObject));
		stick2.NetworkObject.SpawnWithOwnership(base.OwnerClientId, false);
		if (!stick2.NetworkObject.TrySetParent(base.gameObject.transform, true))
		{
			Player.Logger.Error(string.Format("Failed to set parent for Stick ({0})", base.OwnerClientId));
		}
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000A590 File Offset: 0x00008790
	public void Server_DespawnCharacter()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Despawning character ({0})", base.OwnerClientId));
		this.Server_DespawnPlayerBody();
		this.Server_DespawnStick();
	}

	// Token: 0x06000263 RID: 611 RVA: 0x000213C4 File Offset: 0x0001F5C4
	public void Server_DespawnPlayerCamera()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Despawning PlayerCamera ({0})", base.OwnerClientId));
		if (this.PlayerCamera && this.PlayerCamera.NetworkObject.IsSpawned)
		{
			this.PlayerCamera.NetworkObject.Despawn(true);
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00021430 File Offset: 0x0001F630
	public void Server_DespawnPlayerBody()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Despawning PlayerBody ({0})", base.OwnerClientId));
		this.Server_DespawnPlayerCamera();
		this.Server_DespawnStickPositioner();
		if (this.PlayerBody && this.PlayerBody.NetworkObject.IsSpawned)
		{
			this.PlayerBody.NetworkObject.Despawn(true);
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x000214A8 File Offset: 0x0001F6A8
	public void Server_DespawnStickPositioner()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Despawning StickPositioner ({0})", base.OwnerClientId));
		if (this.StickPositioner && this.StickPositioner.NetworkObject.IsSpawned)
		{
			this.StickPositioner.NetworkObject.Despawn(true);
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x00021514 File Offset: 0x0001F714
	public void Server_DespawnStick()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Despawning Stick ({0})", base.OwnerClientId));
		if (this.Stick && this.Stick.NetworkObject.IsSpawned)
		{
			this.Stick.NetworkObject.Despawn(true);
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x00021580 File Offset: 0x0001F780
	public void Server_SpawnSpectatorCamera(Vector3 position, Quaternion rotation)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Spawning SpectatorCamera ({0})", base.OwnerClientId));
		if (this.IsCharacterSpawned)
		{
			this.Server_DespawnCharacter();
		}
		if (this.IsSpectatorCameraSpawned)
		{
			this.Server_DespawnSpectatorCamera();
		}
		SpectatorCamera spectatorCamera = UnityEngine.Object.Instantiate<SpectatorCamera>(this.spectatorCameraPrefab, position, rotation);
		spectatorCamera.InitializeNetworkVariables(new NetworkObjectReference(base.NetworkObject));
		spectatorCamera.PlayerReference.Value = new NetworkObjectReference(base.NetworkObject);
		spectatorCamera.NetworkObject.SpawnWithOwnership(base.OwnerClientId, false);
		if (!spectatorCamera.NetworkObject.TrySetParent(base.gameObject.transform, true))
		{
			Player.Logger.Error(string.Format("Failed to set parent for SpectatorCamera ({0})", base.OwnerClientId));
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x00021654 File Offset: 0x0001F854
	public void Server_DespawnSpectatorCamera()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player.Logger.Info(string.Format("Despawning SpectatorCamera ({0})", base.OwnerClientId));
		if (this.SpectatorCamera && this.SpectatorCamera.NetworkObject.IsSpawned)
		{
			this.SpectatorCamera.NetworkObject.Despawn(true);
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x000216C0 File Offset: 0x0001F8C0
	public void Server_GoalScored()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		NetworkVariable<int> goals = this.Goals;
		int value = goals.Value;
		goals.Value = value + 1;
	}

	// Token: 0x0600026A RID: 618 RVA: 0x000216F0 File Offset: 0x0001F8F0
	public void Server_AssistScored()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		NetworkVariable<int> assists = this.Assists;
		int value = assists.Value;
		assists.Value = value + 1;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000A5CA File Offset: 0x000087CA
	public void Server_ResetPoints()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Goals.Value = 0;
		this.Assists.Value = 0;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000A5F1 File Offset: 0x000087F1
	public void Server_UpdatePing()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Ping.Value = NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetCurrentRtt(base.OwnerClientId);
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000A625 File Offset: 0x00008825
	public void Server_ConsumeChatTicket()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.ChatTickets = Mathf.Max(0f, this.ChatTickets - 1f);
	}

	// Token: 0x06000270 RID: 624 RVA: 0x00021720 File Offset: 0x0001F920
	protected override void __initializeVariables()
	{
		bool flag = this.GameState == null;
		if (flag)
		{
			throw new Exception("Player.GameState cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.GameState.Initialize(this);
		base.__nameNetworkVariable(this.GameState, "GameState");
		this.NetworkVariableFields.Add(this.GameState);
		flag = (this.CustomizationState == null);
		if (flag)
		{
			throw new Exception("Player.CustomizationState cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.CustomizationState.Initialize(this);
		base.__nameNetworkVariable(this.CustomizationState, "CustomizationState");
		this.NetworkVariableFields.Add(this.CustomizationState);
		flag = (this.Handedness == null);
		if (flag)
		{
			throw new Exception("Player.Handedness cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Handedness.Initialize(this);
		base.__nameNetworkVariable(this.Handedness, "Handedness");
		this.NetworkVariableFields.Add(this.Handedness);
		flag = (this.SteamId == null);
		if (flag)
		{
			throw new Exception("Player.SteamId cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.SteamId.Initialize(this);
		base.__nameNetworkVariable(this.SteamId, "SteamId");
		this.NetworkVariableFields.Add(this.SteamId);
		flag = (this.Username == null);
		if (flag)
		{
			throw new Exception("Player.Username cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Username.Initialize(this);
		base.__nameNetworkVariable(this.Username, "Username");
		this.NetworkVariableFields.Add(this.Username);
		flag = (this.Number == null);
		if (flag)
		{
			throw new Exception("Player.Number cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Number.Initialize(this);
		base.__nameNetworkVariable(this.Number, "Number");
		this.NetworkVariableFields.Add(this.Number);
		flag = (this.PatreonLevel == null);
		if (flag)
		{
			throw new Exception("Player.PatreonLevel cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.PatreonLevel.Initialize(this);
		base.__nameNetworkVariable(this.PatreonLevel, "PatreonLevel");
		this.NetworkVariableFields.Add(this.PatreonLevel);
		flag = (this.AdminLevel == null);
		if (flag)
		{
			throw new Exception("Player.AdminLevel cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.AdminLevel.Initialize(this);
		base.__nameNetworkVariable(this.AdminLevel, "AdminLevel");
		this.NetworkVariableFields.Add(this.AdminLevel);
		flag = (this.Goals == null);
		if (flag)
		{
			throw new Exception("Player.Goals cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Goals.Initialize(this);
		base.__nameNetworkVariable(this.Goals, "Goals");
		this.NetworkVariableFields.Add(this.Goals);
		flag = (this.Assists == null);
		if (flag)
		{
			throw new Exception("Player.Assists cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Assists.Initialize(this);
		base.__nameNetworkVariable(this.Assists, "Assists");
		this.NetworkVariableFields.Add(this.Assists);
		flag = (this.Ping == null);
		if (flag)
		{
			throw new Exception("Player.Ping cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Ping.Initialize(this);
		base.__nameNetworkVariable(this.Ping, "Ping");
		this.NetworkVariableFields.Add(this.Ping);
		flag = (this.PlayerPositionReference == null);
		if (flag)
		{
			throw new Exception("Player.PlayerPositionReference cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.PlayerPositionReference.Initialize(this);
		base.__nameNetworkVariable(this.PlayerPositionReference, "PlayerPositionReference");
		this.NetworkVariableFields.Add(this.PlayerPositionReference);
		flag = (this.IsMuted == null);
		if (flag)
		{
			throw new Exception("Player.IsMuted cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.IsMuted.Initialize(this);
		base.__nameNetworkVariable(this.IsMuted, "IsMuted");
		this.NetworkVariableFields.Add(this.IsMuted);
		flag = (this.IsReplay == null);
		if (flag)
		{
			throw new Exception("Player.IsReplay cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.IsReplay.Initialize(this);
		base.__nameNetworkVariable(this.IsReplay, "IsReplay");
		this.NetworkVariableFields.Add(this.IsReplay);
		base.__initializeVariables();
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00021B6C File Offset: 0x0001FD6C
	protected override void __initializeRpcs()
	{
		base.__registerRpc(2620210071U, new NetworkBehaviour.RpcReceiveHandler(Player.__rpc_handler_2620210071), "Client_RequestTeamRpc", RpcInvokePermission.Everyone);
		base.__registerRpc(949682089U, new NetworkBehaviour.RpcReceiveHandler(Player.__rpc_handler_949682089), "Client_RequestClaimPositionRpc", RpcInvokePermission.Everyone);
		base.__registerRpc(4280154797U, new NetworkBehaviour.RpcReceiveHandler(Player.__rpc_handler_4280154797), "Client_RequestTeamSelectRpc", RpcInvokePermission.Everyone);
		base.__registerRpc(3454979199U, new NetworkBehaviour.RpcReceiveHandler(Player.__rpc_handler_3454979199), "Client_RequestPositionSelectRpc", RpcInvokePermission.Everyone);
		base.__registerRpc(744616166U, new NetworkBehaviour.RpcReceiveHandler(Player.__rpc_handler_744616166), "Client_RequestHandednessRpc", RpcInvokePermission.Everyone);
		base.__initializeRpcs();
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00021C28 File Offset: 0x0001FE28
	private static void __rpc_handler_2620210071(NetworkBehaviour target, FastBufferReader reader, __RpcParams rpcParams)
	{
		NetworkManager networkManager = target.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			return;
		}
		PlayerTeam team;
		reader.ReadValueSafe<PlayerTeam>(out team, default(FastBufferWriter.ForEnums));
		RpcParams ext = rpcParams.Ext;
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Execute;
		((Player)target).Client_RequestTeamRpc(team, ext);
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00021CA8 File Offset: 0x0001FEA8
	private static void __rpc_handler_949682089(NetworkBehaviour target, FastBufferReader reader, __RpcParams rpcParams)
	{
		NetworkManager networkManager = target.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			return;
		}
		NetworkObjectReference playerPositionReference;
		reader.ReadValueSafe<NetworkObjectReference>(out playerPositionReference, default(FastBufferWriter.ForNetworkSerializable));
		RpcParams ext = rpcParams.Ext;
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Execute;
		((Player)target).Client_RequestClaimPositionRpc(playerPositionReference, ext);
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00021D28 File Offset: 0x0001FF28
	private static void __rpc_handler_4280154797(NetworkBehaviour target, FastBufferReader reader, __RpcParams rpcParams)
	{
		NetworkManager networkManager = target.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			return;
		}
		RpcParams ext = rpcParams.Ext;
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Execute;
		((Player)target).Client_RequestTeamSelectRpc(ext);
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00021D88 File Offset: 0x0001FF88
	private static void __rpc_handler_3454979199(NetworkBehaviour target, FastBufferReader reader, __RpcParams rpcParams)
	{
		NetworkManager networkManager = target.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			return;
		}
		RpcParams ext = rpcParams.Ext;
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Execute;
		((Player)target).Client_RequestPositionSelectRpc(ext);
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00021DE8 File Offset: 0x0001FFE8
	private static void __rpc_handler_744616166(NetworkBehaviour target, FastBufferReader reader, __RpcParams rpcParams)
	{
		NetworkManager networkManager = target.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			return;
		}
		PlayerHandedness handedness;
		reader.ReadValueSafe<PlayerHandedness>(out handedness, default(FastBufferWriter.ForEnums));
		RpcParams ext = rpcParams.Ext;
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Execute;
		((Player)target).Client_RequestHandednessRpc(handedness, ext);
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000A67B File Offset: 0x0000887B
	protected internal override string __getTypeName()
	{
		return "Player";
	}

	// Token: 0x04000188 RID: 392
	private static readonly global::Logger Logger = new global::Logger("Player");

	// Token: 0x04000189 RID: 393
	[Header("Settings")]
	[SerializeField]
	private int maxChatTickets = 3;

	// Token: 0x0400018A RID: 394
	[SerializeField]
	public float ChatTicketsPerSecond = 3f;

	// Token: 0x0400018B RID: 395
	[Header("Prefabs")]
	[SerializeField]
	private PlayerCamera playerCameraPrefab;

	// Token: 0x0400018C RID: 396
	[SerializeField]
	private PlayerBody playerBodyAttackerPrefab;

	// Token: 0x0400018D RID: 397
	[SerializeField]
	private PlayerBody playerBodyGoaliePrefab;

	// Token: 0x0400018E RID: 398
	[SerializeField]
	private StickPositioner stickPositionerPrefab;

	// Token: 0x0400018F RID: 399
	[SerializeField]
	private SpectatorCamera spectatorCameraPrefab;

	// Token: 0x04000190 RID: 400
	[SerializeField]
	private Stick stickAttackerPrefab;

	// Token: 0x04000191 RID: 401
	[SerializeField]
	private Stick stickGoaliePrefab;

	// Token: 0x04000192 RID: 402
	[HideInInspector]
	public NetworkVariable<PlayerGameState> GameState;

	// Token: 0x04000193 RID: 403
	[HideInInspector]
	public NetworkVariable<PlayerCustomizationState> CustomizationState;

	// Token: 0x04000194 RID: 404
	[HideInInspector]
	public NetworkVariable<PlayerHandedness> Handedness;

	// Token: 0x04000195 RID: 405
	[HideInInspector]
	public NetworkVariable<FixedString32Bytes> SteamId;

	// Token: 0x04000196 RID: 406
	[HideInInspector]
	public NetworkVariable<FixedString32Bytes> Username;

	// Token: 0x04000197 RID: 407
	[HideInInspector]
	public NetworkVariable<int> Number;

	// Token: 0x04000198 RID: 408
	[HideInInspector]
	public NetworkVariable<int> PatreonLevel;

	// Token: 0x04000199 RID: 409
	[HideInInspector]
	public NetworkVariable<int> AdminLevel;

	// Token: 0x0400019A RID: 410
	[HideInInspector]
	public NetworkVariable<int> Goals;

	// Token: 0x0400019B RID: 411
	[HideInInspector]
	public NetworkVariable<int> Assists;

	// Token: 0x0400019C RID: 412
	[HideInInspector]
	public NetworkVariable<ulong> Ping;

	// Token: 0x0400019D RID: 413
	[HideInInspector]
	public NetworkVariable<NetworkObjectReference> PlayerPositionReference;

	// Token: 0x0400019E RID: 414
	[HideInInspector]
	public NetworkVariable<bool> IsMuted;

	// Token: 0x0400019F RID: 415
	[HideInInspector]
	public NetworkVariable<bool> IsReplay;

	// Token: 0x040001A1 RID: 417
	[HideInInspector]
	public PlayerInput PlayerInput;

	// Token: 0x040001A2 RID: 418
	[HideInInspector]
	public SpectatorCamera SpectatorCamera;

	// Token: 0x040001A3 RID: 419
	[HideInInspector]
	public PlayerCamera PlayerCamera;

	// Token: 0x040001A4 RID: 420
	[HideInInspector]
	public PlayerBody PlayerBody;

	// Token: 0x040001A5 RID: 421
	[HideInInspector]
	public StickPositioner StickPositioner;

	// Token: 0x040001A6 RID: 422
	[HideInInspector]
	public Stick Stick;

	// Token: 0x040001A7 RID: 423
	[HideInInspector]
	public PlayerPosition PlayerPosition;

	// Token: 0x040001A8 RID: 424
	private bool isNetworkVariablesInitialized;

	// Token: 0x040001A9 RID: 425
	private Tween delayedGameStateTween;
}
