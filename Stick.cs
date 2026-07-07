using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class Stick : NetworkBehaviour
{
	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600029A RID: 666 RVA: 0x0000A77A File Offset: 0x0000897A
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

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x0600029B RID: 667 RVA: 0x0000A797 File Offset: 0x00008997
	[HideInInspector]
	public StickPositioner StickPositioner
	{
		get
		{
			if (!(this.Player == null))
			{
				return this.Player.StickPositioner;
			}
			return null;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A7B4 File Offset: 0x000089B4
	[HideInInspector]
	public StickMesh StickMesh
	{
		get
		{
			return this.stickMesh;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x0600029D RID: 669 RVA: 0x0000A7BC File Offset: 0x000089BC
	[HideInInspector]
	public Vector3 ShaftHandleLocalPosition
	{
		get
		{
			return this.shaftHandle.transform.localPosition;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x0600029E RID: 670 RVA: 0x0000A7CE File Offset: 0x000089CE
	[HideInInspector]
	public Vector3 BladeHandleLocalPosition
	{
		get
		{
			return this.bladeHandle.transform.localPosition;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600029F RID: 671 RVA: 0x0000A7E0 File Offset: 0x000089E0
	[HideInInspector]
	public Vector3 ShaftHandlePosition
	{
		get
		{
			return this.shaftHandle.transform.position;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000A7F2 File Offset: 0x000089F2
	[HideInInspector]
	public Vector3 BladeHandlePosition
	{
		get
		{
			return this.bladeHandle.transform.position;
		}
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000A804 File Offset: 0x00008A04
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
		this.NetworkObjectCollisionRecorder = base.GetComponent<NetworkObjectCollisionRecorder>();
		this.Length = Vector3.Distance(this.ShaftHandlePosition, this.BladeHandlePosition);
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x00022484 File Offset: 0x00020684
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(default(NetworkObjectReference));
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000A835 File Offset: 0x00008A35
	public override void OnNetworkSpawn()
	{
		NetworkVariable<NetworkObjectReference> playerReference = this.PlayerReference;
		playerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Combine(playerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerReferenceChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x000224A8 File Offset: 0x000206A8
	protected override void OnNetworkPostSpawn()
	{
		NetworkObjectReference value = this.PlayerReference.Value;
		this.HandlePlayerReference(default(NetworkObjectReference), value);
		EventManager.TriggerEvent("Event_Everyone_OnStickSpawned", new Dictionary<string, object>
		{
			{
				"stick",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x000224F4 File Offset: 0x000206F4
	public override void OnNetworkDespawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnStickDespawned", new Dictionary<string, object>
		{
			{
				"stick",
				this
			}
		});
		NetworkVariable<NetworkObjectReference> playerReference = this.PlayerReference;
		playerReference.OnValueChanged = (NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate)Delegate.Remove(playerReference.OnValueChanged, new NetworkVariable<NetworkObjectReference>.OnValueChangedDelegate(this.OnPlayerReferenceChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000A864 File Offset: 0x00008A64
	public override void OnDestroy()
	{
		base.transform.DOKill(false);
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000A873 File Offset: 0x00008A73
	public void InitializeNetworkVariables(NetworkObjectReference playerReference = default(NetworkObjectReference))
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.PlayerReference = new NetworkVariable<NetworkObjectReference>(playerReference, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0002254C File Offset: 0x0002074C
	private void FixedUpdate()
	{
		this.Server_FixedUpdate();
		if (!this.Player)
		{
			return;
		}
		float angle = (float)this.Player.PlayerInput.BladeAngleInput.ServerValue * this.bladeAngleStep;
		this.rotationContainer.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x000225A8 File Offset: 0x000207A8
	public void ApplyCustomizations()
	{
		if (this.Player.Team == PlayerTeam.None || this.Player.Role == PlayerRole.None)
		{
			return;
		}
		this.SetSkinID(this.Player.GetPlayerStickSkinID(), this.Player.Team);
		this.SetShaftTapeID(this.Player.GetPlayerStickShaftTapeID());
		this.SetBladeTapeID(this.Player.GetPlayerStickBladeTapeID());
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000A893 File Offset: 0x00008A93
	public void SetSkinID(int skinID, PlayerTeam team)
	{
		this.stickMesh.SetSkinID(skinID, team);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000A8A2 File Offset: 0x00008AA2
	public void SetShaftTapeID(int shaftTapeID)
	{
		this.stickMesh.SetShaftTapeID(shaftTapeID);
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000A8B0 File Offset: 0x00008AB0
	public void SetBladeTapeID(int bladeTapeID)
	{
		this.stickMesh.SetBladeTapeID(bladeTapeID);
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000A8BE File Offset: 0x00008ABE
	private void OnCollisionStay(Collision collision)
	{
		this.Server_OnCollisionStay(collision);
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00022610 File Offset: 0x00020810
	private void HandlePlayerReference(NetworkObjectReference oldPlayerReference = default(NetworkObjectReference), NetworkObjectReference newPlayerReference = default(NetworkObjectReference))
	{
		NetworkObject networkObject;
		Player player = oldPlayerReference.TryGet(out networkObject, null) ? networkObject.GetComponent<Player>() : null;
		NetworkObject networkObject2;
		Player player2 = newPlayerReference.TryGet(out networkObject2, null) ? networkObject2.GetComponent<Player>() : null;
		if (player)
		{
			player.Stick = null;
		}
		if (player2)
		{
			this.Player = player2;
			this.Player.Stick = this;
		}
		else
		{
			this.Player = null;
		}
		if (this.Player)
		{
			this.ApplyCustomizations();
			if (this.Player.IsReplay.Value)
			{
				this.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				this.Rigidbody.interpolation = RigidbodyInterpolation.None;
				return;
			}
			this.Rigidbody.constraints = RigidbodyConstraints.None;
			this.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x000226D4 File Offset: 0x000208D4
	private void OnPlayerReferenceChanged(NetworkObjectReference oldPlayerReference, NetworkObjectReference newPlayerReference)
	{
		this.HandlePlayerReference(oldPlayerReference, newPlayerReference);
		EventManager.TriggerEvent("Event_Everyone_OnStickPlayerReferenceChanged", new Dictionary<string, object>
		{
			{
				"stick",
				this
			},
			{
				"oldPlayerReference",
				oldPlayerReference
			},
			{
				"newPlayerReference",
				newPlayerReference
			}
		});
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00022728 File Offset: 0x00020928
	private void Server_FixedUpdate()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.Player || this.Player.IsReplay.Value)
		{
			return;
		}
		this.Server_UpdatePidControllers(Time.fixedDeltaTime);
		this.Server_ApplyForces();
		this.Server_ClearRollVelocity();
		this.Server_ResetRoll();
		this.Server_ApplyFeedbackForces();
		this.bladeHandleProportionalGainMultiplier = 1f;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00022790 File Offset: 0x00020990
	private void Server_UpdatePidControllers(float deltaTime)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.StickPositioner)
		{
			return;
		}
		this.shaftHandlePIDController.proportionalGain = this.shaftHandleProportionalGain * this.shaftHandleProportionalGainMultiplier;
		this.shaftHandlePIDController.integralGain = this.shaftHandleIntegralGain;
		this.shaftHandlePIDController.integralSaturation = this.shaftHandleIntegralSaturation;
		this.shaftHandlePIDController.derivativeGain = this.shaftHandleDerivativeGain;
		this.shaftHandlePIDController.derivativeSmoothing = this.shaftHandleDerivativeSmoothing;
		this.bladeHandlePIDController.proportionalGain = this.bladeHandleProportionalGain * this.bladeHandleProportionalGainMultiplier;
		this.bladeHandlePIDController.integralGain = this.bladeHandleIntegralGain;
		this.bladeHandlePIDController.integralSaturation = this.bladeHandleIntegralSaturation;
		this.bladeHandlePIDController.derivativeGain = this.bladeHandleDerivativeGain;
		this.bladeHandlePIDController.derivativeSmoothing = this.bladeHandleDerivativeSmoothing;
		this.shaftHandleForce = this.shaftHandlePIDController.Update(deltaTime, this.ShaftHandlePosition, this.StickPositioner.ShaftTargetPosition);
		this.bladeHandleForce = this.bladeHandlePIDController.Update(deltaTime, this.BladeHandlePosition, this.StickPositioner.BladeTargetPosition);
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x000228B8 File Offset: 0x00020AB8
	private void Server_ApplyForces()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.PlayerBody)
		{
			return;
		}
		this.Rigidbody.AddForceAtPosition(this.PlayerBody.Rigidbody.GetPointVelocity(this.shaftHandle.transform.position) * this.linearVelocityTransferMultiplier * Time.fixedDeltaTime, this.shaftHandle.transform.position, ForceMode.VelocityChange);
		this.Rigidbody.AddForceAtPosition(this.PlayerBody.Rigidbody.GetPointVelocity(this.bladeHandle.transform.position) * this.linearVelocityTransferMultiplier * Time.fixedDeltaTime, this.bladeHandle.transform.position, ForceMode.VelocityChange);
		this.Rigidbody.AddForceAtPosition(this.shaftHandleForce * Time.fixedDeltaTime, this.ShaftHandlePosition, ForceMode.VelocityChange);
		this.Rigidbody.AddForceAtPosition(this.bladeHandleForce * Time.fixedDeltaTime, this.BladeHandlePosition, ForceMode.VelocityChange);
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x000229C8 File Offset: 0x00020BC8
	private void Server_ClearRollVelocity()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Vector3 direction = base.transform.InverseTransformVector(this.Rigidbody.angularVelocity);
		direction.z = 0f;
		this.Rigidbody.angularVelocity = base.transform.TransformDirection(direction);
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x00022A1C File Offset: 0x00020C1C
	private void Server_ResetRoll()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Vector3 vector = Utils.WrapEulerAngles(base.transform.eulerAngles);
		Quaternion rotation = Quaternion.Euler(new Vector3(vector.x, vector.y, 0f));
		this.Rigidbody.MoveRotation(rotation);
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00022A70 File Offset: 0x00020C70
	private void Server_ApplyFeedbackForces()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.PlayerBody)
		{
			return;
		}
		Vector3 a = Vector3.Scale(this.Rigidbody.angularVelocity, new Vector3(0.5f, 1f, 0f)) * this.angularVelocityTransferMultiplier;
		if (this.transferAngularVelocity)
		{
			this.PlayerBody.Rigidbody.AddTorque(-a, ForceMode.Acceleration);
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00022AE8 File Offset: 0x00020CE8
	public void Server_Teleport(Vector3 position, Quaternion rotation)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.position = position;
		this.Rigidbody.rotation = rotation;
		this.Rigidbody.linearVelocity = Vector3.zero;
		this.Rigidbody.angularVelocity = Vector3.zero;
		this.shaftHandlePIDController.Reset();
		this.bladeHandlePIDController.Reset();
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000A8C7 File Offset: 0x00008AC7
	public void Server_Freeze(RigidbodyConstraints contstraints = RigidbodyConstraints.FreezeAll)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.constraints = contstraints;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0000A8E2 File Offset: 0x00008AE2
	public void Server_Unfreeze()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.constraints = RigidbodyConstraints.None;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00022B50 File Offset: 0x00020D50
	private void Server_OnCollisionStay(Collision collision)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Stick component = collision.gameObject.GetComponent<Stick>();
		if (!component)
		{
			return;
		}
		if (collision.contacts.Length == 0)
		{
			return;
		}
		ContactPoint contactPoint = collision.contacts[0];
		Component thisCollider = contactPoint.thisCollider;
		Collider otherCollider = contactPoint.otherCollider;
		if (thisCollider.tag != "Stick Blade" || otherCollider.tag != "Stick Shaft")
		{
			return;
		}
		Vector3 point = contactPoint.point;
		float num = Mathf.Clamp(Vector3.Distance(component.ShaftHandlePosition, point) / this.Length, this.minShaftHandleProportionalGainMultiplier, 1f);
		this.bladeHandleProportionalGainMultiplier = num;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00022CE8 File Offset: 0x00020EE8
	protected override void __initializeVariables()
	{
		bool flag = this.PlayerReference == null;
		if (flag)
		{
			throw new Exception("Stick.PlayerReference cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.PlayerReference.Initialize(this);
		base.__nameNetworkVariable(this.PlayerReference, "PlayerReference");
		this.NetworkVariableFields.Add(this.PlayerReference);
		base.__initializeVariables();
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000A90E File Offset: 0x00008B0E
	protected internal override string __getTypeName()
	{
		return "Stick";
	}

	// Token: 0x040001C8 RID: 456
	private static readonly global::Logger Logger = new global::Logger("Stick");

	// Token: 0x040001C9 RID: 457
	[Header("Settings")]
	[SerializeField]
	private float bladeAngleStep = 12.5f;

	// Token: 0x040001CA RID: 458
	[Space(20f)]
	[SerializeField]
	private bool transferAngularVelocity = true;

	// Token: 0x040001CB RID: 459
	[SerializeField]
	private float angularVelocityTransferMultiplier = 0.25f;

	// Token: 0x040001CC RID: 460
	[Space(20f)]
	[SerializeField]
	private float shaftHandleProportionalGain = 500f;

	// Token: 0x040001CD RID: 461
	[SerializeField]
	private float shaftHandleIntegralGain;

	// Token: 0x040001CE RID: 462
	[SerializeField]
	private float shaftHandleIntegralSaturation;

	// Token: 0x040001CF RID: 463
	[SerializeField]
	private float shaftHandleDerivativeGain = 20f;

	// Token: 0x040001D0 RID: 464
	[SerializeField]
	private float shaftHandleDerivativeSmoothing = 0.1f;

	// Token: 0x040001D1 RID: 465
	[SerializeField]
	private float minShaftHandleProportionalGainMultiplier = 0.25f;

	// Token: 0x040001D2 RID: 466
	[Space(20f)]
	[SerializeField]
	private float bladeHandleProportionalGain = 500f;

	// Token: 0x040001D3 RID: 467
	[SerializeField]
	private float bladeHandleIntegralGain;

	// Token: 0x040001D4 RID: 468
	[SerializeField]
	private float bladeHandleIntegralSaturation;

	// Token: 0x040001D5 RID: 469
	[SerializeField]
	private float bladeHandleDerivativeGain = 20f;

	// Token: 0x040001D6 RID: 470
	[SerializeField]
	private float bladeHandleDerivativeSmoothing = 0.1f;

	// Token: 0x040001D7 RID: 471
	[Space(20f)]
	[SerializeField]
	private float linearVelocityTransferMultiplier = 0.25f;

	// Token: 0x040001D8 RID: 472
	[Header("References")]
	[SerializeField]
	private GameObject shaftHandle;

	// Token: 0x040001D9 RID: 473
	[SerializeField]
	private GameObject bladeHandle;

	// Token: 0x040001DA RID: 474
	[SerializeField]
	private GameObject rotationContainer;

	// Token: 0x040001DB RID: 475
	[SerializeField]
	private StickMesh stickMesh;

	// Token: 0x040001DC RID: 476
	[HideInInspector]
	public NetworkVariable<NetworkObjectReference> PlayerReference;

	// Token: 0x040001DD RID: 477
	[HideInInspector]
	public Player Player;

	// Token: 0x040001DE RID: 478
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x040001DF RID: 479
	[HideInInspector]
	public NetworkObjectCollisionRecorder NetworkObjectCollisionRecorder;

	// Token: 0x040001E0 RID: 480
	[HideInInspector]
	public float Length;

	// Token: 0x040001E1 RID: 481
	private bool isNetworkVariablesInitialized;

	// Token: 0x040001E2 RID: 482
	private Vector3PIDController shaftHandlePIDController = new Vector3PIDController(0f, 0f, 0f);

	// Token: 0x040001E3 RID: 483
	private Vector3PIDController bladeHandlePIDController = new Vector3PIDController(0f, 0f, 0f);

	// Token: 0x040001E4 RID: 484
	private float shaftHandleProportionalGainMultiplier = 1f;

	// Token: 0x040001E5 RID: 485
	private float bladeHandleProportionalGainMultiplier = 1f;

	// Token: 0x040001E6 RID: 486
	private Vector3 shaftHandleForce = Vector3.zero;

	// Token: 0x040001E7 RID: 487
	private Vector3 bladeHandleForce = Vector3.zero;
}
