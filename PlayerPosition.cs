using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class PlayerPosition : NetworkBehaviour
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000A9 RID: 169 RVA: 0x000091E4 File Offset: 0x000073E4
	[HideInInspector]
	public bool IsClaimed
	{
		get
		{
			return this.ClaimedByPlayer != null;
		}
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00016C6C File Offset: 0x00014E6C
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(default(NetworkObjectReference));
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x060000AB RID: 171 RVA: 0x000091F2 File Offset: 0x000073F2
	public override void OnNetworkSpawn()
	{
		NetworkVariable<NetworkObjectReference> claimedByPlayerReference = this.ClaimedByPlayerReference;
		claimedByPlayerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Combine(claimedByPlayerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnClaimedByReferenceChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00016C90 File Offset: 0x00014E90
	protected override void OnNetworkPostSpawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerPositionSpawned", new Dictionary<string, object>
		{
			{
				"playerPosition",
				this
			}
		});
		if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsConnectedClient)
		{
			this.ProcessInitialNetworkVariableValues();
		}
		base.OnNetworkPostSpawn();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00009221 File Offset: 0x00007421
	protected override void OnNetworkSessionSynchronized()
	{
		this.ProcessInitialNetworkVariableValues();
		base.OnNetworkSessionSynchronized();
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00016CDC File Offset: 0x00014EDC
	public override void OnNetworkDespawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnPlayerPositionDespawned", new Dictionary<string, object>
		{
			{
				"playerPosition",
				this
			}
		});
		NetworkVariable<NetworkObjectReference> claimedByPlayerReference = this.ClaimedByPlayerReference;
		claimedByPlayerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Remove(claimedByPlayerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnClaimedByReferenceChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x060000AF RID: 175 RVA: 0x0000922F File Offset: 0x0000742F
	public void InitializeNetworkVariables(NetworkObjectReference claimedByPlayerReference = default(NetworkObjectReference))
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.ClaimedByPlayerReference = new NetworkVariable<NetworkObjectReference>(claimedByPlayerReference, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00016D34 File Offset: 0x00014F34
	private void ProcessInitialNetworkVariableValues()
	{
		this.OnClaimedByReferenceChanged(default(NetworkObjectReference), this.ClaimedByPlayerReference.Value);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00016D5C File Offset: 0x00014F5C
	private void OnClaimedByReferenceChanged(NetworkObjectReference oldClaimedByReferece, NetworkObjectReference newClaimedByReferece)
	{
		Player playerFromNetworkObjectReference = NetworkingUtils.GetPlayerFromNetworkObjectReference(oldClaimedByReferece);
		Player playerFromNetworkObjectReference2 = NetworkingUtils.GetPlayerFromNetworkObjectReference(newClaimedByReferece);
		this.ClaimedByPlayer = playerFromNetworkObjectReference2;
		EventManager.TriggerEvent("Event_Everyone_OnPlayerPositionClaimedByPlayerChanged", new Dictionary<string, object>
		{
			{
				"playerPosition",
				this
			},
			{
				"oldClaimedByPlayer",
				playerFromNetworkObjectReference
			},
			{
				"newClaimedByPlayer",
				playerFromNetworkObjectReference2
			}
		});
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x0000924F File Offset: 0x0000744F
	public void Server_Claim(Player player)
	{
		PlayerPosition.Logger.Info(string.Format("Position {0} claimed by {1}", this.Name, player.OwnerClientId));
		this.ClaimedByPlayerReference.Value = new NetworkObjectReference(player.NetworkObject);
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00016DB4 File Offset: 0x00014FB4
	public void Server_Unclaim()
	{
		PlayerPosition.Logger.Info("Position " + this.Name + " unclaimed");
		this.ClaimedByPlayerReference.Value = default(NetworkObjectReference);
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00016DF4 File Offset: 0x00014FF4
	protected override void __initializeVariables()
	{
		bool flag = this.ClaimedByPlayerReference == null;
		if (flag)
		{
			throw new Exception("PlayerPosition.ClaimedByPlayerReference cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.ClaimedByPlayerReference.Initialize(this);
		base.__nameNetworkVariable(this.ClaimedByPlayerReference, "ClaimedByPlayerReference");
		this.NetworkVariableFields.Add(this.ClaimedByPlayerReference);
		base.__initializeVariables();
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x0000929D File Offset: 0x0000749D
	protected internal override string __getTypeName()
	{
		return "PlayerPosition";
	}

	// Token: 0x0400004C RID: 76
	private static readonly global::Logger Logger = new global::Logger("PlayerPosition");

	// Token: 0x0400004D RID: 77
	[Header("Settings")]
	public string Name;

	// Token: 0x0400004E RID: 78
	public PlayerTeam Team;

	// Token: 0x0400004F RID: 79
	public PlayerRole Role;

	// Token: 0x04000050 RID: 80
	[HideInInspector]
	public NetworkVariable<NetworkObjectReference> ClaimedByPlayerReference;

	// Token: 0x04000051 RID: 81
	[HideInInspector]
	public Player ClaimedByPlayer;

	// Token: 0x04000052 RID: 82
	private bool isNetworkVariablesInitialized;
}
