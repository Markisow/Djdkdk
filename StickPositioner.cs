using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class StickPositioner : NetworkBehaviour
{
	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000A992 File Offset: 0x00008B92
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

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000A9AF File Offset: 0x00008BAF
	[HideInInspector]
	public Stick Stick
	{
		get
		{
			if (!(this.Player == null))
			{
				return this.Player.Stick;
			}
			return null;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060002CA RID: 714 RVA: 0x0000A9CC File Offset: 0x00008BCC
	[HideInInspector]
	public Vector3 BladeTargetPosition
	{
		get
		{
			return this.bladeTarget.transform.position;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060002CB RID: 715 RVA: 0x0000A9DE File Offset: 0x00008BDE
	[HideInInspector]
	public Vector3 BladeTargetVelocity
	{
		get
		{
			return this.bladeTargetVelocity;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060002CC RID: 716 RVA: 0x0000A9E6 File Offset: 0x00008BE6
	[HideInInspector]
	public Vector3 ShaftTargetPosition
	{
		get
		{
			return this.shaftTarget.transform.position;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A9F8 File Offset: 0x00008BF8
	[HideInInspector]
	public Vector3 RaycastOriginPosition
	{
		get
		{
			return this.raycastOrigin.transform.position;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060002CE RID: 718 RVA: 0x0000AA0A File Offset: 0x00008C0A
	private Vector3 BladeTargetFocusPointInitialLocalPosition
	{
		get
		{
			if (this.Handedness != PlayerHandedness.Left)
			{
				return this.bladeTargetFocusPointInitialLocalPosition;
			}
			return new Vector3(-this.bladeTargetFocusPointInitialLocalPosition.x, this.bladeTargetFocusPointInitialLocalPosition.y, this.bladeTargetFocusPointInitialLocalPosition.z);
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060002CF RID: 719 RVA: 0x0000AA43 File Offset: 0x00008C43
	private Vector3 RaycastOriginInitialLocalPosition
	{
		get
		{
			if (this.Handedness != PlayerHandedness.Left)
			{
				return this.raycastOriginInitialLocalPosition;
			}
			return new Vector3(-this.raycastOriginInitialLocalPosition.x, this.raycastOriginInitialLocalPosition.y, this.raycastOriginInitialLocalPosition.z);
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0000AA7C File Offset: 0x00008C7C
	private void Awake()
	{
		this.bladeTargetFocusPointInitialLocalPosition = this.bladeTargetFocusPoint.transform.localPosition;
		this.raycastOriginInitialLocalPosition = this.raycastOrigin.transform.localPosition;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00022DFC File Offset: 0x00020FFC
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(default(NetworkObjectReference));
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0000AAAA File Offset: 0x00008CAA
	public override void OnNetworkSpawn()
	{
		NetworkVariable<NetworkObjectReference> playerReference = this.PlayerReference;
		playerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Combine(playerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerReferenceChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00022E20 File Offset: 0x00021020
	protected override void OnNetworkPostSpawn()
	{
		NetworkObjectReference value = this.PlayerReference.Value;
		this.HandlePlayerReference(default(NetworkObjectReference), value);
		EventManager.TriggerEvent("Event_Everyone_OnStickPositionerSpawned", new Dictionary<string, object>
		{
			{
				"stickPositioner",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0000AAD9 File Offset: 0x00008CD9
	public override void OnNetworkDespawn()
	{
		NetworkVariable<NetworkObjectReference> playerReference = this.PlayerReference;
		playerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Remove(playerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerReferenceChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0000AB08 File Offset: 0x00008D08
	public void InitializeNetworkVariables(NetworkObjectReference playerReference = default(NetworkObjectReference))
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.PlayerReference = new NetworkVariable<NetworkObjectReference>(playerReference, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00022E6C File Offset: 0x0002106C
	private void FixedUpdate()
	{
		if (!this.Player)
		{
			return;
		}
		this.pidController.proportionalGain = this.proportionalGain;
		this.pidController.integralGain = this.integralGain;
		this.pidController.integralSaturation = this.integralSaturation;
		this.pidController.derivativeMeasurement = this.derivativeMeasurement;
		this.pidController.derivativeGain = this.derivativeGain;
		this.pidController.derivativeSmoothing = this.derivativeSmoothing;
		this.pidController.outputMin = this.outputMin;
		this.pidController.outputMax = this.outputMax;
		this.raycastOriginAngleTarget = this.Player.PlayerInput.StickRaycastOriginAngleInput.ServerValue;
		this.ShootPaddingRay();
		this.RotateRaycastOrigin();
		this.ShootRaycast();
		this.UpdateAudio();
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00022F44 File Offset: 0x00021144
	private void ShootPaddingRay()
	{
		Vector3 vector = new Vector3(0f, this.RaycastOriginInitialLocalPosition.y, 0f);
		Vector3 normalized = (this.RaycastOriginInitialLocalPosition - vector).normalized;
		float num = Vector3.Distance(vector, this.RaycastOriginInitialLocalPosition) + this.raycastOriginPadding;
		Vector3 vector2 = base.transform.TransformPoint(vector);
		Vector3 vector3 = base.transform.TransformDirection(normalized);
		Debug.DrawRay(vector2, vector3 * num, Color.yellow);
		RaycastHit raycastHit;
		if (Physics.Raycast(vector2, vector3, out raycastHit, num, this.raycastLayerMask))
		{
			this.raycastOrigin.transform.localPosition = vector + normalized * (raycastHit.distance - this.raycastOriginPadding);
			return;
		}
		this.raycastOrigin.transform.localPosition = this.RaycastOriginInitialLocalPosition;
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0002301C File Offset: 0x0002121C
	private void RotateRaycastOrigin()
	{
		this.raycastOriginAngleDelta = this.pidController.Update(Time.fixedDeltaTime, this.raycastOriginAngle, this.raycastOriginAngleTarget);
		this.raycastOriginAngle += this.raycastOriginAngleDelta * Time.fixedDeltaTime;
		this.raycastOrigin.transform.localRotation = Quaternion.Euler(this.raycastOriginAngle);
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0002309C File Offset: 0x0002129C
	private void ShootRaycast()
	{
		RaycastHit hit;
		Vector3 vector5;
		if (Physics.Raycast(this.raycastOrigin.transform.position, this.raycastOrigin.transform.forward, out hit, this.maximumReach, this.raycastLayerMask))
		{
			this.OnGrounded(hit.transform.gameObject);
			Vector3 vector = this.raycastOrigin.transform.position + this.raycastOrigin.transform.forward * this.maximumReach;
			Vector3 b = Vector3.Scale(Utils.Vector3Abs(hit.normal), hit.point);
			Vector3 vector2 = vector - Vector3.Scale(Utils.Vector3Abs(hit.normal), vector) + b;
			Debug.DrawRay(this.raycastOrigin.transform.position, this.raycastOrigin.transform.forward * hit.distance, Color.red);
			Vector3 normalized = (vector2 - this.raycastOrigin.transform.position).normalized;
			RaycastHit raycastHit;
			if (Physics.Raycast(this.raycastOrigin.transform.position, normalized, out raycastHit, this.maximumReach, this.raycastLayerMask))
			{
				Vector3 vector3 = this.raycastOrigin.transform.position + this.raycastOrigin.transform.forward * this.maximumReach;
				Vector3 b2 = Vector3.Scale(Utils.Vector3Abs(raycastHit.normal), raycastHit.point);
				Vector3 vector4 = vector3 - Vector3.Scale(Utils.Vector3Abs(raycastHit.normal), vector3) + b2;
				if (hit.normal == Vector3.up && raycastHit.normal == Vector3.up)
				{
					vector5 = vector2;
				}
				else if (hit.normal == Vector3.up && raycastHit.normal != Vector3.up)
				{
					vector5 = vector4;
					vector5.y = Mathf.Max(0f, vector5.y);
				}
				else
				{
					vector5 = vector2;
					vector5.y = Mathf.Max(0f, vector5.y);
				}
				Debug.DrawRay(this.raycastOrigin.transform.position, normalized * raycastHit.distance, Color.blue);
			}
			else
			{
				vector5 = hit.point;
			}
			this.ApplySoftCollision(hit, vector5);
		}
		else
		{
			this.OnUngrounded();
			vector5 = this.raycastOrigin.transform.position + this.raycastOrigin.transform.forward * this.maximumReach;
			Debug.DrawRay(this.raycastOrigin.transform.position, this.raycastOrigin.transform.forward * this.maximumReach, Color.red);
		}
		this.PositionBladeTarget(vector5);
		this.PositionBladeTargetFocusPoint(vector5);
		this.RotateBladeTargetFocusPoint();
	}

	// Token: 0x060002DA RID: 730 RVA: 0x000233A0 File Offset: 0x000215A0
	private void PositionBladeTarget(Vector3 hitPosition)
	{
		this.bladeTarget.transform.position = hitPosition;
		this.bladeTarget.transform.rotation = Quaternion.LookRotation(this.bladeTarget.transform.position - this.bladeTargetFocusPoint.transform.position);
		this.bladeTargetVelocity = (this.bladeTarget.transform.position - this.lastBladeTargetPosition) / Time.fixedDeltaTime;
		this.lastBladeTargetPosition = this.bladeTarget.transform.position;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0002343C File Offset: 0x0002163C
	private void PositionBladeTargetFocusPoint(Vector3 hitPosition)
	{
		float num = Vector3.Distance(this.raycastOrigin.transform.position, new Vector3(hitPosition.x, this.raycastOrigin.transform.position.y, hitPosition.z));
		float d = this.maximumReach - num;
		Vector3 vector = this.raycastOrigin.transform.localPosition - base.transform.InverseTransformPoint(hitPosition);
		Vector3 normalized = new Vector3(vector.x, 0f, vector.z).normalized;
		Vector3 a = base.transform.TransformDirection(normalized);
		Debug.DrawRay(this.bladeTargetFocusPoint.transform.position, -a * d, Color.grey);
		Vector3 b = normalized * d;
		this.bladeTargetFocusPoint.transform.localPosition = this.BladeTargetFocusPointInitialLocalPosition + b;
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0002352C File Offset: 0x0002172C
	private void RotateBladeTargetFocusPoint()
	{
		PlayerInput playerInput = this.Player.PlayerInput;
		if (!playerInput)
		{
			return;
		}
		float num = Mathf.Lerp(1f, 0f, (playerInput.MaximumStickRaycastOriginAngle.x - this.raycastOriginAngle.x) / this.bladeTargetRotationThreshold);
		num *= (float)((this.Handedness == PlayerHandedness.Left) ? -1 : 1);
		this.bladeTargetFocusPoint.transform.localPosition = Utils.RotatePointAroundPivot(this.bladeTargetFocusPoint.transform.localPosition, this.bladeTarget.transform.localPosition, new Vector3(0f, this.bladeTargetMaxAngle * num, 0f));
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000AB28 File Offset: 0x00008D28
	public void PrepareShaftTarget(Stick stick)
	{
		this.shaftTarget.transform.localPosition = stick.ShaftHandleLocalPosition - stick.BladeHandleLocalPosition;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x000235DC File Offset: 0x000217DC
	private void ApplySoftCollision(RaycastHit hit, Vector3 hitPosition)
	{
		if (!this.applySoftCollision)
		{
			return;
		}
		if (!this.PlayerBody)
		{
			return;
		}
		if (hit.collider.CompareTag("Soft Collider"))
		{
			float d = this.maximumReach - hit.distance;
			float magnitude = Vector3.Cross(hit.normal, this.raycastOrigin.transform.forward).magnitude;
			float num = 1f - magnitude;
			Debug.DrawRay(hitPosition, hit.normal * d * this.softCollisionForce, Color.green);
			this.PlayerBody.Rigidbody.AddForceAtPosition(hit.normal * d * (this.softCollisionForce * num), hitPosition, ForceMode.Acceleration);
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x000236A4 File Offset: 0x000218A4
	private void UpdateAudio()
	{
		this.windAudioSource.transform.position = this.BladeTargetPosition;
		this.iceHitAudioSource.transform.position = this.BladeTargetPosition;
		this.iceDragAudioSource.transform.position = this.BladeTargetPosition;
		float num = this.IsGrounded ? this.iceDragVolumeCurve.Evaluate(this.BladeTargetVelocity.magnitude) : 0f;
		if (num > this.iceDragVolume)
		{
			this.iceDragVolume = num;
		}
		else
		{
			this.iceDragVolume = Mathf.Lerp(this.iceDragVolume, num, Time.fixedDeltaTime * this.iceDragVolumeFallOffSpeed);
		}
		this.iceDragPitch = this.iceDragPitchCurve.Evaluate(this.BladeTargetVelocity.magnitude);
		this.iceDragAudioSource.Server_SetVolume(this.iceDragVolume);
		this.iceDragAudioSource.Server_SetPitch(this.iceDragPitch);
		float volume = this.windVolumeCurve.Evaluate(this.raycastOriginAngleDelta.magnitude);
		float pitch = this.windPitchCurve.Evaluate(this.raycastOriginAngleDelta.magnitude);
		this.windAudioSource.Server_SetVolume(volume);
		this.windAudioSource.Server_SetPitch(pitch);
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x000237D4 File Offset: 0x000219D4
	private void OnGrounded(GameObject ground)
	{
		if (this.IsGrounded)
		{
			return;
		}
		if (ground.layer == LayerMask.NameToLayer("Ice"))
		{
			float volume = this.iceHitVolumeCurve.Evaluate(Mathf.Abs(this.raycastOriginAngleDelta.x));
			float pitch = this.iceHitPitchCurve.Evaluate(Mathf.Abs(this.raycastOriginAngleDelta.x));
			this.iceHitAudioSource.Server_Play(volume, pitch, true, -1, 0f, true, false, false, 0f, false, 0f, -1f);
		}
		this.IsGrounded = true;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000AB4B File Offset: 0x00008D4B
	private void OnUngrounded()
	{
		if (!this.IsGrounded)
		{
			return;
		}
		this.IsGrounded = false;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00023864 File Offset: 0x00021A64
	private void HandlePlayerReference(NetworkObjectReference oldPlayerReference = default(NetworkObjectReference), NetworkObjectReference newPlayerReference = default(NetworkObjectReference))
	{
		NetworkObject networkObject;
		Player player = oldPlayerReference.TryGet(out networkObject, null) ? networkObject.GetComponent<Player>() : null;
		NetworkObject networkObject2;
		Player player2 = newPlayerReference.TryGet(out networkObject2, null) ? networkObject2.GetComponent<Player>() : null;
		if (player)
		{
			player.StickPositioner = null;
		}
		if (player2)
		{
			this.Player = player2;
			this.Player.StickPositioner = this;
		}
		else
		{
			this.Player = null;
		}
		if (this.Player)
		{
			if (this.Player.Stick)
			{
				this.PrepareShaftTarget(this.Player.Stick);
			}
			this.Handedness = this.Player.Handedness.Value;
		}
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0000AB5D File Offset: 0x00008D5D
	private void OnPlayerReferenceChanged(NetworkObjectReference oldPlayerReference, NetworkObjectReference newPlayerReference)
	{
		this.HandlePlayerReference(oldPlayerReference, newPlayerReference);
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00023A18 File Offset: 0x00021C18
	protected override void __initializeVariables()
	{
		bool flag = this.PlayerReference == null;
		if (flag)
		{
			throw new Exception("StickPositioner.PlayerReference cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.PlayerReference.Initialize(this);
		base.__nameNetworkVariable(this.PlayerReference, "PlayerReference");
		this.NetworkVariableFields.Add(this.PlayerReference);
		base.__initializeVariables();
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0000AB67 File Offset: 0x00008D67
	protected internal override string __getTypeName()
	{
		return "StickPositioner";
	}

	// Token: 0x040001E9 RID: 489
	[Header("Settings")]
	[SerializeField]
	private float proportionalGain = 0.75f;

	// Token: 0x040001EA RID: 490
	[SerializeField]
	private float integralGain = 5f;

	// Token: 0x040001EB RID: 491
	[SerializeField]
	private float integralSaturation = 5f;

	// Token: 0x040001EC RID: 492
	[SerializeField]
	private DerivativeMeasurement derivativeMeasurement;

	// Token: 0x040001ED RID: 493
	[SerializeField]
	private float derivativeGain;

	// Token: 0x040001EE RID: 494
	[SerializeField]
	private float derivativeSmoothing;

	// Token: 0x040001EF RID: 495
	[SerializeField]
	private float outputMin = -15f;

	// Token: 0x040001F0 RID: 496
	[SerializeField]
	private float outputMax = 15f;

	// Token: 0x040001F1 RID: 497
	[Space(20f)]
	[SerializeField]
	private float maximumReach = 2.5f;

	// Token: 0x040001F2 RID: 498
	[Space(20f)]
	[SerializeField]
	private float bladeTargetRotationThreshold = 25f;

	// Token: 0x040001F3 RID: 499
	[SerializeField]
	private float bladeTargetMaxAngle = 45f;

	// Token: 0x040001F4 RID: 500
	[Space(20f)]
	[SerializeField]
	private LayerMask raycastLayerMask;

	// Token: 0x040001F5 RID: 501
	[Space(20f)]
	[SerializeField]
	private float raycastOriginPadding = 0.2f;

	// Token: 0x040001F6 RID: 502
	[Space(20f)]
	[SerializeField]
	private bool applySoftCollision = true;

	// Token: 0x040001F7 RID: 503
	[SerializeField]
	private float softCollisionForce = 1f;

	// Token: 0x040001F8 RID: 504
	[Space(20f)]
	[SerializeField]
	private AnimationCurve windVolumeCurve;

	// Token: 0x040001F9 RID: 505
	[SerializeField]
	private AnimationCurve windPitchCurve;

	// Token: 0x040001FA RID: 506
	[SerializeField]
	private AnimationCurve iceHitVolumeCurve;

	// Token: 0x040001FB RID: 507
	[SerializeField]
	private AnimationCurve iceHitPitchCurve;

	// Token: 0x040001FC RID: 508
	[SerializeField]
	private AnimationCurve iceDragVolumeCurve;

	// Token: 0x040001FD RID: 509
	[SerializeField]
	private AnimationCurve iceDragPitchCurve;

	// Token: 0x040001FE RID: 510
	[Header("References")]
	[SerializeField]
	private GameObject raycastOrigin;

	// Token: 0x040001FF RID: 511
	[SerializeField]
	private GameObject bladeTargetFocusPoint;

	// Token: 0x04000200 RID: 512
	[SerializeField]
	private GameObject bladeTarget;

	// Token: 0x04000201 RID: 513
	[SerializeField]
	private GameObject shaftTarget;

	// Token: 0x04000202 RID: 514
	[Space(20f)]
	[SerializeField]
	private SynchronizedAudio windAudioSource;

	// Token: 0x04000203 RID: 515
	[SerializeField]
	private SynchronizedAudio iceHitAudioSource;

	// Token: 0x04000204 RID: 516
	[SerializeField]
	private SynchronizedAudio iceDragAudioSource;

	// Token: 0x04000205 RID: 517
	[SerializeField]
	private float iceDragVolumeFallOffSpeed = 10f;

	// Token: 0x04000206 RID: 518
	[HideInInspector]
	public NetworkVariable<NetworkObjectReference> PlayerReference;

	// Token: 0x04000207 RID: 519
	[HideInInspector]
	public Player Player;

	// Token: 0x04000208 RID: 520
	[HideInInspector]
	public bool IsGrounded;

	// Token: 0x04000209 RID: 521
	[HideInInspector]
	public PlayerHandedness Handedness;

	// Token: 0x0400020A RID: 522
	private bool isNetworkVariablesInitialized;

	// Token: 0x0400020B RID: 523
	private Vector3 lastBladeTargetPosition = Vector3.zero;

	// Token: 0x0400020C RID: 524
	private Vector3 bladeTargetVelocity = Vector3.zero;

	// Token: 0x0400020D RID: 525
	private Vector3 bladeTargetFocusPointInitialLocalPosition = Vector3.zero;

	// Token: 0x0400020E RID: 526
	private Vector3 raycastOriginInitialLocalPosition = Vector3.zero;

	// Token: 0x0400020F RID: 527
	private Vector2 raycastOriginAngleTarget = Vector2.zero;

	// Token: 0x04000210 RID: 528
	private Vector2 raycastOriginAngle = Vector2.zero;

	// Token: 0x04000211 RID: 529
	private Vector2 raycastOriginAngleDelta = Vector3.zero;

	// Token: 0x04000212 RID: 530
	private float iceDragVolume;

	// Token: 0x04000213 RID: 531
	private float iceDragPitch;

	// Token: 0x04000214 RID: 532
	private Vector3PIDController pidController = new Vector3PIDController(0f, 0f, 0f);
}
