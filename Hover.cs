using System;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000021 RID: 33
[RequireComponent(typeof(Rigidbody))]
public class Hover : MonoBehaviour
{
	// Token: 0x060000BD RID: 189 RVA: 0x000092B2 File Offset: 0x000074B2
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00016E58 File Offset: 0x00015058
	private void FixedUpdate()
	{
		this.pidController.proportionalGain = this.proportionalGain;
		this.pidController.integralGain = this.integralGain;
		this.pidController.derivativeGain = this.derivativeGain;
		Vector3 vector = base.transform.position + this.raycastOffset;
		Vector3 down = Vector3.down;
		Debug.DrawRay(vector, down * this.raycastDistance, Color.black);
		RaycastHit raycastHit;
		if (Physics.Raycast(vector, down, out raycastHit, this.raycastDistance, this.raycastLayerMask))
		{
			this.currentDistance = raycastHit.distance;
		}
		else
		{
			this.currentDistance = this.raycastDistance;
		}
		this.IsGrounded = (this.currentDistance < this.raycastDistance);
		float num = this.pidController.Update(Time.fixedDeltaTime, this.currentDistance, this.TargetDistance);
		num = Mathf.Clamp(num, 0f, this.maxForce);
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.AddForce(Vector3.up * num, ForceMode.Acceleration);
	}

	// Token: 0x04000054 RID: 84
	[Header("Settings")]
	[SerializeField]
	private float maxForce = 40f;

	// Token: 0x04000055 RID: 85
	[SerializeField]
	private Vector3 raycastOffset = new Vector3(0f, 1f, 0f);

	// Token: 0x04000056 RID: 86
	[SerializeField]
	private float raycastDistance = 1.45f;

	// Token: 0x04000057 RID: 87
	[SerializeField]
	private LayerMask raycastLayerMask;

	// Token: 0x04000058 RID: 88
	[Space(20f)]
	[SerializeField]
	private float proportionalGain = 100f;

	// Token: 0x04000059 RID: 89
	[SerializeField]
	private float integralGain;

	// Token: 0x0400005A RID: 90
	[SerializeField]
	private float derivativeGain = 15f;

	// Token: 0x0400005B RID: 91
	[Space(20f)]
	public float TargetDistance = 1f;

	// Token: 0x0400005C RID: 92
	[HideInInspector]
	public bool IsGrounded;

	// Token: 0x0400005D RID: 93
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x0400005E RID: 94
	private PIDController pidController = new PIDController(0f, 0f, 0f);

	// Token: 0x0400005F RID: 95
	private float currentDistance;
}
