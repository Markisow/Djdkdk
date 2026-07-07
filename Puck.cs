using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class Puck : NetworkBehaviour
{
	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000316 RID: 790 RVA: 0x0000ADC0 File Offset: 0x00008FC0
	[HideInInspector]
	public float PredictedSpeed
	{
		get
		{
			return this.SynchronizedObject.PredictedLinearVelocity.magnitude;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000317 RID: 791 RVA: 0x0000ADD2 File Offset: 0x00008FD2
	[HideInInspector]
	public float PredictedAngularSpeed
	{
		get
		{
			return this.SynchronizedObject.PredictedAngularVelocity.magnitude;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000318 RID: 792 RVA: 0x0000ADE4 File Offset: 0x00008FE4
	// (set) Token: 0x06000319 RID: 793 RVA: 0x0000ADEC File Offset: 0x00008FEC
	[HideInInspector]
	public float ShotSpeed { get; private set; }

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600031A RID: 794 RVA: 0x0000ADF5 File Offset: 0x00008FF5
	// (set) Token: 0x0600031B RID: 795 RVA: 0x0000ADFD File Offset: 0x00008FFD
	[HideInInspector]
	public bool IsGrounded { get; private set; }

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x0600031C RID: 796 RVA: 0x0000AE06 File Offset: 0x00009006
	[HideInInspector]
	public SphereCollider NetSphereCollider
	{
		get
		{
			return this.netSphereCollider;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x0600031D RID: 797 RVA: 0x0000AE0E File Offset: 0x0000900E
	[HideInInspector]
	public Collider StickCollider
	{
		get
		{
			return this.stickCollider;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x0600031E RID: 798 RVA: 0x0000AE16 File Offset: 0x00009016
	[HideInInspector]
	public Collider IceCollider
	{
		get
		{
			return this.iceCollider;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600031F RID: 799 RVA: 0x0000AE1E File Offset: 0x0000901E
	// (set) Token: 0x06000320 RID: 800 RVA: 0x0000AE26 File Offset: 0x00009026
	[HideInInspector]
	public Stick TouchingStick { get; private set; }

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000321 RID: 801 RVA: 0x0000AE2F File Offset: 0x0000902F
	[HideInInspector]
	public bool IsTouchingStick
	{
		get
		{
			return this.TouchingStick != null;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000322 RID: 802 RVA: 0x0000AE3D File Offset: 0x0000903D
	[HideInInspector]
	public float MaxSpeed
	{
		get
		{
			return this.maxSpeed;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000323 RID: 803 RVA: 0x0000AE45 File Offset: 0x00009045
	[HideInInspector]
	public float MaxAngularSpeed
	{
		get
		{
			return this.maxAngularSpeed;
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00023EAC File Offset: 0x000220AC
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
		this.SynchronizedObject = base.GetComponent<SynchronizedObject>();
		this.NetworkObjectCollisionRecorder = base.GetComponent<NetworkObjectCollisionRecorder>();
		this.CollisionRecorder = base.GetComponent<CollisionRecorder>();
		CollisionRecorder collisionRecorder = this.CollisionRecorder;
		collisionRecorder.CollisionDeferred = (Action<GameObject, float>)Delegate.Combine(collisionRecorder.CollisionDeferred, new Action<GameObject, float>(this.OnCollisionDeferred));
		this.NetSphereCollider.enabled = false;
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00023F1C File Offset: 0x0002211C
	private void FixedUpdate()
	{
		this.Speed = this.Rigidbody.linearVelocity.magnitude;
		this.AngularSpeed = this.Rigidbody.angularVelocity.magnitude;
		this.IsGrounded = Physics.CheckSphere(base.transform.position, this.groundedCheckSphereRadius, this.groundedCheckSphereLayerMask);
		if (this.IsGrounded)
		{
			this.Rigidbody.centerOfMass = base.transform.TransformVector(this.groundedCenterOfMass);
		}
		else
		{
			this.Rigidbody.centerOfMass = Vector3.zero;
		}
		float num = this.IsGrounded ? 0f : Mathf.Clamp(this.PredictedSpeed * 0.025f, 0.15f, 0.75f);
		if (this.NetSphereCollider.radius < num)
		{
			this.NetSphereCollider.radius = num;
		}
		else if (this.NetSphereCollider.radius > num)
		{
			this.NetSphereCollider.radius = Mathf.Lerp(this.NetSphereCollider.radius, num, Time.fixedDeltaTime * 5f);
		}
		if (this.IsTouchingStick)
		{
			this.Server_UpdateStickTensor(this.stickTensor, Quaternion.identity);
			this.TouchingStick = null;
		}
		else
		{
			this.Server_UpdateStickTensor(this.defaultTensor, Quaternion.identity);
		}
		this.Server_UpdateAudio();
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0000AE4D File Offset: 0x0000904D
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(false);
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0000AE5D File Offset: 0x0000905D
	public override void OnNetworkSpawn()
	{
		NetworkVariable<bool> isReplay = this.IsReplay;
		isReplay.OnValueChanged = (NetworkVariable<bool>.OnValueChangedDelegate)Delegate.Combine(isReplay.OnValueChanged, new NetworkVariable<bool>.OnValueChangedDelegate(this.OnIsReplayChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000AE8C File Offset: 0x0000908C
	protected override void OnNetworkPostSpawn()
	{
		this.HandleIsReplay(false, this.IsReplay.Value);
		EventManager.TriggerEvent("Event_Everyone_OnPuckSpawned", new Dictionary<string, object>
		{
			{
				"puck",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00024070 File Offset: 0x00022270
	public override void OnNetworkDespawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnPuckDespawned", new Dictionary<string, object>
		{
			{
				"puck",
				this
			}
		});
		NetworkVariable<bool> isReplay = this.IsReplay;
		isReplay.OnValueChanged = (NetworkVariable<bool>.OnValueChangedDelegate)Delegate.Remove(isReplay.OnValueChanged, new NetworkVariable<bool>.OnValueChangedDelegate(this.OnIsReplayChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0000AEC1 File Offset: 0x000090C1
	public override void OnDestroy()
	{
		CollisionRecorder collisionRecorder = this.CollisionRecorder;
		collisionRecorder.CollisionDeferred = (Action<GameObject, float>)Delegate.Remove(collisionRecorder.CollisionDeferred, new Action<GameObject, float>(this.OnCollisionDeferred));
		base.transform.DOKill(false);
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0000AEF7 File Offset: 0x000090F7
	public void InitializeNetworkVariables(bool isReplay = false)
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.IsReplay = new NetworkVariable<bool>(isReplay, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0000AF17 File Offset: 0x00009117
	public void Server_Freeze(RigidbodyConstraints contstraints = RigidbodyConstraints.FreezeAll)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.constraints = contstraints;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0000AF32 File Offset: 0x00009132
	public void Server_Unfreeze()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.constraints = RigidbodyConstraints.None;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x000240C8 File Offset: 0x000222C8
	public List<KeyValuePair<Player, float>> GetPlayerCollisions()
	{
		List<KeyValuePair<Player, float>> list = this.NetworkObjectCollisionRecorder.NetworkObjectCollisions.Select(delegate(NetworkObjectCollision collision)
		{
			NetworkObject networkObject;
			if (collision.NetworkObjectReference.TryGet(out networkObject, null))
			{
				PlayerBody playerBody;
				networkObject.TryGetComponent<PlayerBody>(out playerBody);
				Stick stick;
				networkObject.TryGetComponent<Stick>(out stick);
				if (playerBody)
				{
					return new KeyValuePair<Player, float>(playerBody.Player, collision.Time);
				}
				if (stick)
				{
					return new KeyValuePair<Player, float>(stick.Player, collision.Time);
				}
			}
			return new KeyValuePair<Player, float>(null, collision.Time);
		}).ToList<KeyValuePair<Player, float>>();
		list.RemoveAll((KeyValuePair<Player, float> collision) => collision.Key == null);
		return list;
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00024130 File Offset: 0x00022330
	public List<KeyValuePair<Player, float>> GetPlayerCollisionsByTeam(PlayerTeam team)
	{
		return this.GetPlayerCollisions().Where(delegate(KeyValuePair<Player, float> collision)
		{
			Player key = collision.Key;
			return key != null && key.Team == team;
		}).ToList<KeyValuePair<Player, float>>();
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0000AF4D File Offset: 0x0000914D
	private void Server_UpdateStickTensor(Vector3 inertiaTensor, Quaternion inertiaTensorRotation)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.inertiaTensor = inertiaTensor;
		this.Rigidbody.inertiaTensorRotation = inertiaTensorRotation;
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00024168 File Offset: 0x00022368
	private void Server_UpdateAudio()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		float time = Mathf.Min(this.Speed / this.MaxSpeed, 1f);
		this.windAudioSource.Server_SetVolume(this.windVolumeCurve.Evaluate(time));
		float time2 = Mathf.Min(this.Speed / this.MaxSpeed, 1f);
		this.windAudioSource.Server_SetPitch(this.windPitchCurve.Evaluate(time2));
	}

	// Token: 0x06000332 RID: 818 RVA: 0x000241E0 File Offset: 0x000223E0
	private void HandleIsReplay(bool oldIsReplay = false, bool newIsReplay = false)
	{
		if (this.IsReplay.Value)
		{
			this.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			this.Rigidbody.interpolation = RigidbodyInterpolation.None;
			return;
		}
		this.Rigidbody.constraints = RigidbodyConstraints.None;
		this.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0002422C File Offset: 0x0002242C
	private void OnIsReplayChanged(bool oldIsReplay, bool newIsReplay)
	{
		this.HandleIsReplay(oldIsReplay, newIsReplay);
		EventManager.TriggerEvent("Event_Everyone_OnPuckIsReplayChanged", new Dictionary<string, object>
		{
			{
				"puck",
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

	// Token: 0x06000334 RID: 820 RVA: 0x00024280 File Offset: 0x00022480
	private void OnCollisionDeferred(GameObject gameObject, float force)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!gameObject)
		{
			return;
		}
		string a = LayerMask.LayerToName(gameObject.layer);
		if (a == "Goal Post")
		{
			this.hitGoalPostAudioSource.Server_Play(this.hitGoalPostVolumeCurve.Evaluate(force), this.hitGoalPostPitchCurve.Evaluate(force), true, -1, 0f, true, false, false, 0f, false, 0f, -1f);
			return;
		}
		if (!(a == "Boards"))
		{
			this.hitIceAudioSource.Server_Play(this.hitIceVolumeCurve.Evaluate(force), this.hitIcePitchCurve.Evaluate(force), true, -1, 0f, true, false, false, 0f, false, 0f, -1f);
			return;
		}
		this.hitBoardsAudioSource.Server_Play(this.hitBoardsVolumeCurve.Evaluate(force), this.hitBoardsPitchCurve.Evaluate(force), true, -1, 0f, true, false, false, 0f, false, 0f, -1f);
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00024384 File Offset: 0x00022584
	private void OnCollisionEnter(Collision collision)
	{
		Stick component = collision.gameObject.GetComponent<Stick>();
		if (component)
		{
			this.TouchingStick = component;
			this.ShotSpeed = 0f;
		}
		if (this.IsGrounded)
		{
			return;
		}
		string a = LayerMask.LayerToName(collision.gameObject.layer);
		Vector3 a2 = Vector3.zero;
		int num = 0;
		foreach (ContactPoint contactPoint in collision.contacts)
		{
			a2 += contactPoint.normal;
			num++;
		}
		a2 /= (float)num;
		float t = Mathf.Abs(Vector3.Dot(collision.relativeVelocity.normalized, a2.normalized));
		if (!(a == "Goal Net"))
		{
			a == "Goal Post";
			return;
		}
		if (this.Rigidbody.linearVelocity.magnitude > this.goalNetLinearVelocityMaximumMagnitude)
		{
			Vector3 b = this.Rigidbody.linearVelocity.normalized * this.goalNetLinearVelocityMaximumMagnitude;
			b.y = 0f;
			this.Rigidbody.linearVelocity = Vector3.Lerp(this.Rigidbody.linearVelocity, b, t);
		}
		if (this.Rigidbody.angularVelocity.magnitude > this.goalNetAngularVelocityMaximumMagnitude)
		{
			Vector3 b2 = this.Rigidbody.angularVelocity.normalized * this.goalNetAngularVelocityMaximumMagnitude;
			this.Rigidbody.angularVelocity = Vector3.Lerp(this.Rigidbody.angularVelocity, b2, t);
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0002451C File Offset: 0x0002271C
	private void OnCollisionStay(Collision collision)
	{
		Stick component = collision.gameObject.GetComponent<Stick>();
		if (!component)
		{
			return;
		}
		this.TouchingStick = component;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00024548 File Offset: 0x00022748
	private void OnCollisionExit(Collision collision)
	{
		if (!collision.gameObject.GetComponent<Stick>())
		{
			return;
		}
		this.ShotSpeed = this.Speed;
		this.Rigidbody.linearVelocity = Vector3.ClampMagnitude(this.Rigidbody.linearVelocity, this.MaxSpeed);
		this.Rigidbody.angularVelocity = Vector3.ClampMagnitude(this.Rigidbody.angularVelocity, this.MaxAngularSpeed);
		Vector3 force = new Vector3(0f, Mathf.Min(0f, -this.Rigidbody.linearVelocity.y), 0f) * 5f;
		this.Rigidbody.AddForce(force, ForceMode.Acceleration);
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0000AF74 File Offset: 0x00009174
	public void OnDrawGizmos()
	{
		if (!Application.isEditor)
		{
			return;
		}
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(base.transform.position, this.groundedCheckSphereRadius);
	}

	// Token: 0x0600033A RID: 826 RVA: 0x00024690 File Offset: 0x00022890
	protected override void __initializeVariables()
	{
		bool flag = this.IsReplay == null;
		if (flag)
		{
			throw new Exception("Puck.IsReplay cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.IsReplay.Initialize(this);
		base.__nameNetworkVariable(this.IsReplay, "IsReplay");
		this.NetworkVariableFields.Add(this.IsReplay);
		base.__initializeVariables();
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0000AF9E File Offset: 0x0000919E
	protected internal override string __getTypeName()
	{
		return "Puck";
	}

	// Token: 0x04000227 RID: 551
	[Header("Settings")]
	[SerializeField]
	private float maxSpeed = 30f;

	// Token: 0x04000228 RID: 552
	[SerializeField]
	private float maxAngularSpeed = 30f;

	// Token: 0x04000229 RID: 553
	[Space(20f)]
	[SerializeField]
	private Vector3 stickTensor = new Vector3(0.006f, 0.002f, 0.006f);

	// Token: 0x0400022A RID: 554
	[SerializeField]
	private Vector3 defaultTensor = new Vector3(0.002f, 0.002f, 0.002f);

	// Token: 0x0400022B RID: 555
	[Space(20f)]
	[SerializeField]
	private float groundedCheckSphereRadius = 0.075f;

	// Token: 0x0400022C RID: 556
	[SerializeField]
	private LayerMask groundedCheckSphereLayerMask;

	// Token: 0x0400022D RID: 557
	[Space(20f)]
	[SerializeField]
	private Vector3 groundedCenterOfMass = new Vector3(0f, -0.01f, 0f);

	// Token: 0x0400022E RID: 558
	[Space(20f)]
	[SerializeField]
	private float goalNetLinearVelocityMaximumMagnitude = 2f;

	// Token: 0x0400022F RID: 559
	[SerializeField]
	private float goalNetAngularVelocityMaximumMagnitude = 2f;

	// Token: 0x04000230 RID: 560
	[Space(20f)]
	[SerializeField]
	private AnimationCurve hitIceVolumeCurve;

	// Token: 0x04000231 RID: 561
	[SerializeField]
	private AnimationCurve hitIcePitchCurve;

	// Token: 0x04000232 RID: 562
	[SerializeField]
	private AnimationCurve hitBoardsVolumeCurve;

	// Token: 0x04000233 RID: 563
	[SerializeField]
	private AnimationCurve hitBoardsPitchCurve;

	// Token: 0x04000234 RID: 564
	[SerializeField]
	private AnimationCurve hitGoalPostVolumeCurve;

	// Token: 0x04000235 RID: 565
	[SerializeField]
	private AnimationCurve hitGoalPostPitchCurve;

	// Token: 0x04000236 RID: 566
	[SerializeField]
	private AnimationCurve windVolumeCurve;

	// Token: 0x04000237 RID: 567
	[SerializeField]
	private AnimationCurve windPitchCurve;

	// Token: 0x04000238 RID: 568
	[Header("References")]
	[SerializeField]
	private PuckElevationIndicator verticalityIndicator;

	// Token: 0x04000239 RID: 569
	[SerializeField]
	private SphereCollider netSphereCollider;

	// Token: 0x0400023A RID: 570
	[SerializeField]
	private Collider stickCollider;

	// Token: 0x0400023B RID: 571
	[SerializeField]
	private Collider iceCollider;

	// Token: 0x0400023C RID: 572
	[Space(20f)]
	[SerializeField]
	private SynchronizedAudio hitIceAudioSource;

	// Token: 0x0400023D RID: 573
	[SerializeField]
	private SynchronizedAudio hitBoardsAudioSource;

	// Token: 0x0400023E RID: 574
	[SerializeField]
	private SynchronizedAudio hitGoalPostAudioSource;

	// Token: 0x0400023F RID: 575
	[SerializeField]
	private SynchronizedAudio windAudioSource;

	// Token: 0x04000240 RID: 576
	[HideInInspector]
	public NetworkVariable<bool> IsReplay;

	// Token: 0x04000241 RID: 577
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x04000242 RID: 578
	[HideInInspector]
	public SynchronizedObject SynchronizedObject;

	// Token: 0x04000243 RID: 579
	[HideInInspector]
	public NetworkObjectCollisionRecorder NetworkObjectCollisionRecorder;

	// Token: 0x04000244 RID: 580
	[HideInInspector]
	public CollisionRecorder CollisionRecorder;

	// Token: 0x04000245 RID: 581
	[HideInInspector]
	public float Speed;

	// Token: 0x04000246 RID: 582
	[HideInInspector]
	public float AngularSpeed;

	// Token: 0x0400024A RID: 586
	private bool isNetworkVariablesInitialized;
}
