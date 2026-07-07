using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class SynchronizedObject : NetworkBehaviour
{
	// Token: 0x060003B0 RID: 944 RVA: 0x0000B5E4 File Offset: 0x000097E4
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00025DCC File Offset: 0x00023FCC
	protected override void OnNetworkPostSpawn()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			this.Rigidbody.isKinematic = true;
			this.Rigidbody.interpolation = RigidbodyInterpolation.None;
		}
		EventManager.TriggerEvent("Event_Everyone_OnSynchronizedObjectSpawned", new Dictionary<string, object>
		{
			{
				"synchronizedObject",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0000B5F2 File Offset: 0x000097F2
	public override void OnNetworkDespawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnSynchronizedObjectDespawned", new Dictionary<string, object>
		{
			{
				"synchronizedObject",
				this
			}
		});
		base.OnNetworkDespawn();
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00025E20 File Offset: 0x00024020
	public void OnClientTick(Vector3 position, Quaternion rotation, float serverDeltaTime)
	{
		this.PredictedLinearVelocity = (position - base.transform.position) / serverDeltaTime;
		this.PredictedAngularVelocity = (rotation * Quaternion.Inverse(this.lastReceivedRotation)).eulerAngles / serverDeltaTime;
		this.lastReceivedPosition = position;
		this.lastReceivedRotation = rotation;
		base.transform.position = position;
		base.transform.rotation = rotation;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00025E98 File Offset: 0x00024098
	public SynchronizedObjectSnapshot OnClientSmoothTick(Vector3 position, Quaternion rotation, SynchronizedObject synchronizedObject, float serverDeltaTime)
	{
		Vector3 linearVelocity = (position - this.lastReceivedPosition) / serverDeltaTime;
		Vector3 angularVelocity = (rotation * Quaternion.Inverse(this.lastReceivedRotation)).eulerAngles / serverDeltaTime;
		this.lastReceivedPosition = position;
		this.lastReceivedRotation = rotation;
		return new SynchronizedObjectSnapshot
		{
			SynchronizedObject = synchronizedObject,
			Position = position,
			Rotation = rotation,
			LinearVelocity = linearVelocity,
			AngularVelocity = angularVelocity
		};
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00025F10 File Offset: 0x00024110
	public ValueTuple<Vector3, Quaternion, ulong> OnServerTick(float serverDeltaTime)
	{
		this.PredictedLinearVelocity = (base.transform.position - this.lastSentPosition) / serverDeltaTime;
		this.PredictedAngularVelocity = Quaternion.Inverse(base.transform.rotation) * this.lastSentRotation.eulerAngles / serverDeltaTime;
		this.lastSentPosition = base.transform.position;
		this.lastSentRotation = base.transform.rotation;
		return new ValueTuple<Vector3, Quaternion, ulong>(base.transform.position, base.transform.rotation, base.NetworkObjectId);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00025FB0 File Offset: 0x000241B0
	public bool ShouldSendPosition(int tickRate)
	{
		float num = Vector3.Distance(this.lastSentPosition, base.transform.position);
		float num2 = this.positionThreshold * (float)(100 / tickRate);
		return num > num2;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00025FE4 File Offset: 0x000241E4
	public bool ShouldSendRotation(int tickRate)
	{
		float num = Quaternion.Angle(this.lastSentRotation, base.transform.rotation);
		float num2 = this.rotationThreshold * (float)(100 / tickRate);
		return num > num2;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060003BB RID: 955 RVA: 0x0000B615 File Offset: 0x00009815
	protected internal override string __getTypeName()
	{
		return "SynchronizedObject";
	}

	// Token: 0x0400029A RID: 666
	[Header("Settings")]
	[SerializeField]
	private float positionThreshold = 0.001f;

	// Token: 0x0400029B RID: 667
	[SerializeField]
	private float rotationThreshold = 0.01f;

	// Token: 0x0400029C RID: 668
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x0400029D RID: 669
	[HideInInspector]
	public Vector3 PredictedLinearVelocity = Vector3.zero;

	// Token: 0x0400029E RID: 670
	[HideInInspector]
	public Vector3 PredictedAngularVelocity = Vector3.zero;

	// Token: 0x0400029F RID: 671
	private Vector3 lastSentPosition = Vector3.zero;

	// Token: 0x040002A0 RID: 672
	private Quaternion lastSentRotation = Quaternion.identity;

	// Token: 0x040002A1 RID: 673
	private Vector3 lastReceivedPosition = Vector3.zero;

	// Token: 0x040002A2 RID: 674
	private Quaternion lastReceivedRotation = Quaternion.identity;
}
