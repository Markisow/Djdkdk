using System;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200003A RID: 58
[RequireComponent(typeof(Rigidbody))]
public class Skate : MonoBehaviour
{
	// Token: 0x0600016B RID: 363 RVA: 0x00009B03 File Offset: 0x00007D03
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00019F64 File Offset: 0x00018164
	private void FixedUpdate()
	{
		Vector3 vector = this.MovementDirection.InverseTransformVector(this.Rigidbody.linearVelocity);
		vector.y = 0f;
		vector.z = 0f;
		float num = -vector.x;
		this.IsTractionLost = (num > this.traction * Time.fixedDeltaTime);
		num = Mathf.Clamp(num, -this.traction * Time.fixedDeltaTime, this.traction * Time.fixedDeltaTime);
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.AddForce(this.MovementDirection.right * num * this.Intensity, ForceMode.VelocityChange);
	}

	// Token: 0x04000118 RID: 280
	[Header("Settings")]
	[SerializeField]
	private float traction = 0.15f;

	// Token: 0x04000119 RID: 281
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x0400011A RID: 282
	[HideInInspector]
	public float Intensity = 1f;

	// Token: 0x0400011B RID: 283
	[HideInInspector]
	public bool IsTractionLost;

	// Token: 0x0400011C RID: 284
	[HideInInspector]
	public Transform MovementDirection;
}
