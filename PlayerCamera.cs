using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class PlayerCamera : BaseCamera
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000171 RID: 369 RVA: 0x00009B71 File Offset: 0x00007D71
	[HideInInspector]
	public PlayerBody PlayerBody
	{
		get
		{
			if (!(this.Player == null))
			{
				return this.Player.PlayerBody;
			}
			return null;
		}
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0001A120 File Offset: 0x00018320
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(default(NetworkObjectReference));
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00009B8E File Offset: 0x00007D8E
	public override void OnNetworkSpawn()
	{
		NetworkVariable<NetworkObjectReference> playerReference = this.PlayerReference;
		playerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Combine(playerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerReferenceChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0001A144 File Offset: 0x00018344
	protected override void OnNetworkPostSpawn()
	{
		NetworkObjectReference value = this.PlayerReference.Value;
		this.HandlePlayerReference(default(NetworkObjectReference), value);
		EventManager.TriggerEvent("Event_Everyone_OnPlayerCameraSpawned", new Dictionary<string, object>
		{
			{
				"playerCamera",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00009BBD File Offset: 0x00007DBD
	public override void OnNetworkDespawn()
	{
		NetworkVariable<NetworkObjectReference> playerReference = this.PlayerReference;
		playerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Remove(playerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerReferenceChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00009BEC File Offset: 0x00007DEC
	public void InitializeNetworkVariables(NetworkObjectReference playerReference = default(NetworkObjectReference))
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.PlayerReference = new NetworkVariable<NetworkObjectReference>(playerReference, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00009C0C File Offset: 0x00007E0C
	public override bool Enable()
	{
		bool flag = base.Enable();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnPlayerCameraEnabled", new Dictionary<string, object>
			{
				{
					"playerCamera",
					this
				}
			});
		}
		return flag;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00009C32 File Offset: 0x00007E32
	public override bool Disable()
	{
		bool flag = base.Disable();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnPlayerCameraDisabled", new Dictionary<string, object>
			{
				{
					"playerCamera",
					this
				}
			});
		}
		return flag;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0001A190 File Offset: 0x00018390
	public override void OnTick(float deltaTime)
	{
		base.OnTick(deltaTime);
		if (!this.Player)
		{
			return;
		}
		PlayerInput playerInput = this.Player.PlayerInput;
		if (!playerInput)
		{
			return;
		}
		playerInput.UpdateLookAngle(deltaTime);
		base.transform.localRotation = Quaternion.Euler(this.Player.IsLocalPlayer ? playerInput.LookAngleInput.ClientValue : playerInput.LookAngleInput.ServerValue);
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0001A208 File Offset: 0x00018408
	private void HandlePlayerReference(NetworkObjectReference oldPlayerReference = default(NetworkObjectReference), NetworkObjectReference newPlayerReference = default(NetworkObjectReference))
	{
		NetworkObject networkObject;
		Player player = oldPlayerReference.TryGet(out networkObject, null) ? networkObject.GetComponent<Player>() : null;
		NetworkObject networkObject2;
		Player player2 = newPlayerReference.TryGet(out networkObject2, null) ? networkObject2.GetComponent<Player>() : null;
		if (player)
		{
			player.PlayerCamera = null;
		}
		if (player2)
		{
			this.Player = player2;
			this.Player.PlayerCamera = this;
			return;
		}
		this.Player = null;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00009C58 File Offset: 0x00007E58
	private void OnPlayerReferenceChanged(NetworkObjectReference oldPlayerReference, NetworkObjectReference newPlayerReference)
	{
		this.HandlePlayerReference(oldPlayerReference, newPlayerReference);
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0001A274 File Offset: 0x00018474
	protected override void __initializeVariables()
	{
		bool flag = this.PlayerReference == null;
		if (flag)
		{
			throw new Exception("PlayerCamera.PlayerReference cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.PlayerReference.Initialize(this);
		base.__nameNetworkVariable(this.PlayerReference, "PlayerReference");
		this.NetworkVariableFields.Add(this.PlayerReference);
		base.__initializeVariables();
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00008D87 File Offset: 0x00006F87
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00009C62 File Offset: 0x00007E62
	protected internal override string __getTypeName()
	{
		return "PlayerCamera";
	}

	// Token: 0x04000125 RID: 293
	[HideInInspector]
	public NetworkVariable<NetworkObjectReference> PlayerReference;

	// Token: 0x04000126 RID: 294
	[HideInInspector]
	public Player Player;

	// Token: 0x04000127 RID: 295
	private bool isNetworkVariablesInitialized;
}
