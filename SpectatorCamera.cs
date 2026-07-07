using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class SpectatorCamera : BaseCamera
{
	// Token: 0x0600036E RID: 878 RVA: 0x00024E68 File Offset: 0x00023068
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(default(NetworkObjectReference));
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0000B245 File Offset: 0x00009445
	public override void OnNetworkSpawn()
	{
		NetworkVariable<NetworkObjectReference> playerReference = this.PlayerReference;
		playerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Combine(playerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerReferenceChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00024E8C File Offset: 0x0002308C
	protected override void OnNetworkPostSpawn()
	{
		this.position = base.transform.position;
		this.pitch = base.transform.eulerAngles.x;
		this.yaw = base.transform.eulerAngles.y;
		this.targetPitch = this.pitch;
		this.targetYaw = this.yaw;
		NetworkObjectReference value = this.PlayerReference.Value;
		this.HandlePlayerReference(default(NetworkObjectReference), value);
		EventManager.TriggerEvent("Event_Everyone_OnSpectatorCameraSpawned", new Dictionary<string, object>
		{
			{
				"spectatorCamera",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0000B274 File Offset: 0x00009474
	public override void OnNetworkDespawn()
	{
		NetworkVariable<NetworkObjectReference> playerReference = this.PlayerReference;
		playerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Remove(playerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerReferenceChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0000B2A3 File Offset: 0x000094A3
	public void InitializeNetworkVariables(NetworkObjectReference playerReference = default(NetworkObjectReference))
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.PlayerReference = new NetworkVariable<NetworkObjectReference>(playerReference, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00024F2C File Offset: 0x0002312C
	public override void OnTick(float deltaTime)
	{
		base.OnTick(deltaTime);
		if (!base.IsOwner)
		{
			return;
		}
		float d = (float)((InputManager.TurnRightAction.IsPressed() ? 1 : 0) + (InputManager.TurnLeftAction.IsPressed() ? -1 : 0));
		float d2 = (float)(InputManager.JumpAction.IsPressed() ? 1 : (InputManager.SlideAction.IsPressed() ? -1 : 0));
		float d3 = (float)((InputManager.MoveForwardAction.IsPressed() ? 1 : 0) + (InputManager.MoveBackwardAction.IsPressed() ? -1 : 0));
		bool flag = InputManager.SprintAction.IsPressed();
		Vector2 vector = InputManager.StickAction.ReadValue<Vector2>();
		if (GlobalStateManager.UIState.IsMouseRequired)
		{
			d = 0f;
			d2 = 0f;
			d3 = 0f;
			flag = false;
			vector = Vector2.zero;
		}
		float d4 = flag ? (this.movementSpeed * 2f) : this.movementSpeed;
		this.position += base.transform.right * d * d4 * deltaTime;
		this.position += base.transform.up * d2 * d4 * deltaTime;
		this.position += base.transform.forward * d3 * d4 * deltaTime;
		base.transform.position = Vector3.SmoothDamp(base.transform.position, this.position, ref this.positionVelocity, this.positionSmoothTime, float.PositiveInfinity, deltaTime);
		this.targetPitch -= vector.y * SettingsManager.LookSensitivity;
		this.targetYaw += vector.x * SettingsManager.LookSensitivity;
		this.targetPitch = Mathf.Clamp(this.targetPitch, this.pitchMin, this.pitchMax);
		this.pitch = Mathf.Lerp(this.pitch, this.targetPitch, this.lookSmoothing * deltaTime);
		this.yaw = Mathf.Lerp(this.yaw, this.targetYaw, this.lookSmoothing * deltaTime);
		base.transform.rotation = Quaternion.Euler(this.pitch, this.yaw, 0f);
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00025178 File Offset: 0x00023378
	private void HandlePlayerReference(NetworkObjectReference oldPlayerReference = default(NetworkObjectReference), NetworkObjectReference newPlayerReference = default(NetworkObjectReference))
	{
		NetworkObject networkObject;
		Player player = oldPlayerReference.TryGet(out networkObject, null) ? networkObject.GetComponent<Player>() : null;
		NetworkObject networkObject2;
		Player player2 = newPlayerReference.TryGet(out networkObject2, null) ? networkObject2.GetComponent<Player>() : null;
		if (player)
		{
			player.SpectatorCamera = null;
		}
		if (player2)
		{
			this.Player = player2;
			this.Player.SpectatorCamera = this;
			return;
		}
		this.Player = null;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0000B2C3 File Offset: 0x000094C3
	private void OnPlayerReferenceChanged(NetworkObjectReference oldPlayerReference, NetworkObjectReference newPlayerReference)
	{
		this.HandlePlayerReference(oldPlayerReference, newPlayerReference);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00025244 File Offset: 0x00023444
	protected override void __initializeVariables()
	{
		bool flag = this.PlayerReference == null;
		if (flag)
		{
			throw new Exception("SpectatorCamera.PlayerReference cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.PlayerReference.Initialize(this);
		base.__nameNetworkVariable(this.PlayerReference, "PlayerReference");
		this.NetworkVariableFields.Add(this.PlayerReference);
		base.__initializeVariables();
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00008D87 File Offset: 0x00006F87
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0000B2CD File Offset: 0x000094CD
	protected internal override string __getTypeName()
	{
		return "SpectatorCamera";
	}

	// Token: 0x0400026E RID: 622
	[Header("Movement Settings")]
	[SerializeField]
	private float movementSpeed = 5f;

	// Token: 0x0400026F RID: 623
	[SerializeField]
	private float positionSmoothTime = 0.25f;

	// Token: 0x04000270 RID: 624
	[SerializeField]
	private float lookSmoothing = 10f;

	// Token: 0x04000271 RID: 625
	[SerializeField]
	private float pitchMin = -89f;

	// Token: 0x04000272 RID: 626
	[SerializeField]
	private float pitchMax = 89f;

	// Token: 0x04000273 RID: 627
	[HideInInspector]
	public NetworkVariable<NetworkObjectReference> PlayerReference;

	// Token: 0x04000274 RID: 628
	[HideInInspector]
	public Player Player;

	// Token: 0x04000275 RID: 629
	private Vector3 position = Vector3.zero;

	// Token: 0x04000276 RID: 630
	private Vector3 positionVelocity = Vector3.zero;

	// Token: 0x04000277 RID: 631
	private float pitch;

	// Token: 0x04000278 RID: 632
	private float yaw;

	// Token: 0x04000279 RID: 633
	private float targetPitch;

	// Token: 0x0400027A RID: 634
	private float targetYaw;

	// Token: 0x0400027B RID: 635
	private bool isNetworkVariablesInitialized;
}
