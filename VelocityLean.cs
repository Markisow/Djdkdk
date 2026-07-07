using System;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200003B RID: 59
[RequireComponent(typeof(Rigidbody))]
public class VelocityLean : MonoBehaviour
{
	// Token: 0x0600016E RID: 366 RVA: 0x00009B2F File Offset: 0x00007D2F
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0001A014 File Offset: 0x00018214
	private void FixedUpdate()
	{
		float d = this.UseWorldLinearVelocity ? this.Rigidbody.linearVelocity.magnitude : this.MovementDirection.InverseTransformVector(this.Rigidbody.linearVelocity).z;
		float y = this.MovementDirection.InverseTransformVector(this.Rigidbody.angularVelocity).y;
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.AddTorque(d * (this.Inverted ? (-base.transform.right) : base.transform.right) * this.linearForceMultiplier * this.LinearIntensity, ForceMode.Acceleration);
		this.Rigidbody.AddTorque(-y * (this.Inverted ? (-base.transform.forward) : base.transform.forward) * this.angularForceMultiplier * this.AngularIntensity, ForceMode.Acceleration);
	}

	// Token: 0x0400011D RID: 285
	[Header("Settings")]
	[SerializeField]
	private float linearForceMultiplier = 1f;

	// Token: 0x0400011E RID: 286
	[SerializeField]
	private float angularForceMultiplier = 6f;

	// Token: 0x0400011F RID: 287
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x04000120 RID: 288
	[HideInInspector]
	public float LinearIntensity = 1f;

	// Token: 0x04000121 RID: 289
	[HideInInspector]
	public float AngularIntensity = 1f;

	// Token: 0x04000122 RID: 290
	[HideInInspector]
	public bool Inverted;

	// Token: 0x04000123 RID: 291
	[HideInInspector]
	public bool UseWorldLinearVelocity;

	// Token: 0x04000124 RID: 292
	[HideInInspector]
	public Transform MovementDirection;
}
