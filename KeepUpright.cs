using System;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000022 RID: 34
[RequireComponent(typeof(Rigidbody))]
public class KeepUpright : MonoBehaviour
{
	// Token: 0x060000C0 RID: 192 RVA: 0x000092C0 File Offset: 0x000074C0
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00016FE8 File Offset: 0x000151E8
	private void FixedUpdate()
	{
		this.pidController.proportionalGain = this.proportionalGain * this.Balance;
		this.pidController.integralGain = this.integralGain * this.Balance;
		this.pidController.derivativeGain = this.derivativeGain * this.Balance;
		Vector3 a = Vector3.Cross(this.pidController.Update(Time.fixedDeltaTime, base.transform.up, Vector3.up), Vector3.up);
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Rigidbody.AddTorque(-a, ForceMode.Acceleration);
	}

	// Token: 0x04000060 RID: 96
	[Header("Settings")]
	[SerializeField]
	private float proportionalGain = 50f;

	// Token: 0x04000061 RID: 97
	[SerializeField]
	private float integralGain;

	// Token: 0x04000062 RID: 98
	[SerializeField]
	private float derivativeGain = 5f;

	// Token: 0x04000063 RID: 99
	[HideInInspector]
	public float Balance = 1f;

	// Token: 0x04000064 RID: 100
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x04000065 RID: 101
	private Vector3PIDController pidController = new Vector3PIDController(0f, 0f, 0f);
}
