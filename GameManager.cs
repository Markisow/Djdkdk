using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class GameManager : NetworkBehaviourSingleton<GameManager>
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000C3D7 File Offset: 0x0000A5D7
	[HideInInspector]
	public GamePhase Phase
	{
		get
		{
			return this.GameState.Value.Phase;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
	[HideInInspector]
	public int Tick
	{
		get
		{
			return this.GameState.Value.Tick;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000C3FB File Offset: 0x0000A5FB
	[HideInInspector]
	public int Period
	{
		get
		{
			return this.GameState.Value.Period;
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000C40D File Offset: 0x0000A60D
	[HideInInspector]
	public int BlueScore
	{
		get
		{
			return this.GameState.Value.BlueScore;
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000C41F File Offset: 0x0000A61F
	[HideInInspector]
	public int RedScore
	{
		get
		{
			return this.GameState.Value.RedScore;
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000C431 File Offset: 0x0000A631
	[HideInInspector]
	public bool IsOvertime
	{
		get
		{
			return this.GameState.Value.IsOvertime;
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0002D074 File Offset: 0x0002B274
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		if (this.GameState == null)
		{
			this.GameState = new NetworkVariable<GameState>(default(GameState), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		}
		if (networkManager.IsServer)
		{
			this.GameState.Value = default(GameState);
		}
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0000C443 File Offset: 0x0000A643
	public override void OnNetworkSpawn()
	{
		NetworkVariable<GameState> gameState = this.GameState;
		gameState.OnValueChanged = (NetworkVariable<GameState>.OnValueChangedDelegate)Delegate.Combine(gameState.OnValueChanged, new NetworkVariable<GameState>.OnValueChangedDelegate(this.OnGameStateChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0000C472 File Offset: 0x0000A672
	protected override void OnNetworkPostSpawn()
	{
		if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsConnectedClient)
		{
			this.ProcessInitialNetworkVariableValues();
		}
		base.OnNetworkPostSpawn();
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0000C498 File Offset: 0x0000A698
	protected override void OnNetworkSessionSynchronized()
	{
		this.ProcessInitialNetworkVariableValues();
		base.OnNetworkSessionSynchronized();
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
	public override void OnNetworkDespawn()
	{
		if (NetworkManager.Singleton.IsServer)
		{
			Tween tween = this.tickTween;
			if (tween != null)
			{
				tween.Kill(false);
			}
		}
		NetworkVariable<GameState> gameState = this.GameState;
		gameState.OnValueChanged = (NetworkVariable<GameState>.OnValueChangedDelegate)Delegate.Remove(gameState.OnValueChanged, new NetworkVariable<GameState>.OnValueChangedDelegate(this.OnGameStateChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0002D11C File Offset: 0x0002B31C
	private void ProcessInitialNetworkVariableValues()
	{
		this.OnGameStateChanged(default(GameState), this.GameState.Value);
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
	private void OnGameStateChanged(GameState oldGameState, GameState newGameState)
	{
		EventManager.TriggerEvent("Event_Everyone_OnGameStateChanged", new Dictionary<string, object>
		{
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

	// Token: 0x06000562 RID: 1378 RVA: 0x0002D144 File Offset: 0x0002B344
	private void Server_Tick()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		int? tick = new int?(Mathf.Max(0, this.GameState.Value.Tick - 1));
		this.Server_SetGameState(null, tick, null, null, null, null);
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0002D1B4 File Offset: 0x0002B3B4
	public void Server_StartTicking()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Tween tween = this.tickTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.tickTween = DOVirtual.DelayedCall(1f, new TweenCallback(this.Server_Tick), true).SetLoops(-1);
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0000C4D9 File Offset: 0x0000A6D9
	public void Server_StopTicking()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Tween tween = this.tickTween;
		if (tween == null)
		{
			return;
		}
		tween.Kill(false);
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0002D204 File Offset: 0x0002B404
	public void Server_SetGameState(GamePhase? phase = null, int? tick = null, int? period = null, int? blueScore = null, int? redScore = null, bool? isOvertime = null)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		GameState value = new GameState
		{
			Phase = (phase ?? this.GameState.Value.Phase),
			Tick = (tick ?? this.GameState.Value.Tick),
			Period = (period ?? this.GameState.Value.Period),
			BlueScore = (blueScore ?? this.GameState.Value.BlueScore),
			RedScore = (redScore ?? this.GameState.Value.RedScore),
			IsOvertime = (isOvertime ?? this.GameState.Value.IsOvertime)
		};
		this.GameState.Value = value;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0002D33C File Offset: 0x0002B53C
	[Rpc(SendTo.Everyone, InvokePermission = RpcInvokePermission.Server, DeferLocal = true)]
	public void Server_NotifyGoalScoredRpc(PlayerTeam byTeam, NetworkObjectReference goalPlayerNetworkObjectReference, NetworkObjectReference assistPlayerNetworkObjectReference, NetworkObjectReference secondAssistPlayerNetworkObjectReference, NetworkObjectReference puckNetworkObjectReference)
	{
		NetworkManager networkManager = base.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			uint rpcMethodId = 1809670267U;
			RpcParams rpcParams2;
			RpcParams rpcParams = rpcParams2;
			RpcAttribute.RpcAttributeParams attributeParams = new RpcAttribute.RpcAttributeParams
			{
				InvokePermission = RpcInvokePermission.Server,
				DeferLocal = true
			};
			FastBufferWriter fastBufferWriter = base.__beginSendRpc(rpcMethodId, rpcParams, attributeParams, SendTo.Everyone, RpcDelivery.Reliable);
			fastBufferWriter.WriteValueSafe<PlayerTeam>(byTeam, default(FastBufferWriter.ForEnums));
			fastBufferWriter.WriteValueSafe<NetworkObjectReference>(goalPlayerNetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
			fastBufferWriter.WriteValueSafe<NetworkObjectReference>(assistPlayerNetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
			fastBufferWriter.WriteValueSafe<NetworkObjectReference>(secondAssistPlayerNetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
			fastBufferWriter.WriteValueSafe<NetworkObjectReference>(puckNetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
			base.__endSendRpc(ref fastBufferWriter, 1809670267U, rpcParams2, attributeParams, SendTo.Everyone, RpcDelivery.Reliable);
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		this.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
		Player playerFromNetworkObjectReference = NetworkingUtils.GetPlayerFromNetworkObjectReference(goalPlayerNetworkObjectReference);
		Player playerFromNetworkObjectReference2 = NetworkingUtils.GetPlayerFromNetworkObjectReference(assistPlayerNetworkObjectReference);
		Player playerFromNetworkObjectReference3 = NetworkingUtils.GetPlayerFromNetworkObjectReference(secondAssistPlayerNetworkObjectReference);
		Puck puckFromNetworkObjectReference = NetworkingUtils.GetPuckFromNetworkObjectReference(puckNetworkObjectReference);
		EventManager.TriggerEvent("Event_Everyone_OnGoalScored", new Dictionary<string, object>
		{
			{
				"byTeam",
				byTeam
			},
			{
				"goalPlayer",
				playerFromNetworkObjectReference
			},
			{
				"assistPlayer",
				playerFromNetworkObjectReference2
			},
			{
				"secondAssistPlayer",
				playerFromNetworkObjectReference3
			},
			{
				"puck",
				puckFromNetworkObjectReference
			}
		});
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0002D50C File Offset: 0x0002B70C
	protected override void __initializeVariables()
	{
		bool flag = this.GameState == null;
		if (flag)
		{
			throw new Exception("GameManager.GameState cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.GameState.Initialize(this);
		base.__nameNetworkVariable(this.GameState, "GameState");
		this.NetworkVariableFields.Add(this.GameState);
		base.__initializeVariables();
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0000C501 File Offset: 0x0000A701
	protected override void __initializeRpcs()
	{
		base.__registerRpc(1809670267U, new NetworkBehaviour.RpcReceiveHandler(GameManager.__rpc_handler_1809670267), "Server_NotifyGoalScoredRpc", RpcInvokePermission.Server);
		base.__initializeRpcs();
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0002D570 File Offset: 0x0002B770
	private static void __rpc_handler_1809670267(NetworkBehaviour target, FastBufferReader reader, __RpcParams rpcParams)
	{
		NetworkManager networkManager = target.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			return;
		}
		PlayerTeam byTeam;
		reader.ReadValueSafe<PlayerTeam>(out byTeam, default(FastBufferWriter.ForEnums));
		NetworkObjectReference goalPlayerNetworkObjectReference;
		reader.ReadValueSafe<NetworkObjectReference>(out goalPlayerNetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
		NetworkObjectReference assistPlayerNetworkObjectReference;
		reader.ReadValueSafe<NetworkObjectReference>(out assistPlayerNetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
		NetworkObjectReference secondAssistPlayerNetworkObjectReference;
		reader.ReadValueSafe<NetworkObjectReference>(out secondAssistPlayerNetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
		NetworkObjectReference puckNetworkObjectReference;
		reader.ReadValueSafe<NetworkObjectReference>(out puckNetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Execute;
		((GameManager)target).Server_NotifyGoalScoredRpc(byTeam, goalPlayerNetworkObjectReference, assistPlayerNetworkObjectReference, secondAssistPlayerNetworkObjectReference, puckNetworkObjectReference);
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0000C52C File Offset: 0x0000A72C
	protected internal override string __getTypeName()
	{
		return "GameManager";
	}

	// Token: 0x04000355 RID: 853
	[HideInInspector]
	public NetworkVariable<GameState> GameState;

	// Token: 0x04000356 RID: 854
	private Tween tickTween;
}
