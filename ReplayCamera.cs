using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class ReplayCamera : BaseCamera
{
	// Token: 0x06000350 RID: 848 RVA: 0x000247BC File Offset: 0x000229BC
	public override void OnTick(float deltaTime)
	{
		base.OnTick(deltaTime);
		if (!this.Target)
		{
			return;
		}
		Vector3 normalized = (this.CenterPoint - this.Target.position).normalized;
		Vector3 b = this.Target.position + normalized * this.followDistance;
		b.y = this.followHeight;
		base.transform.position = Vector3.Lerp(base.transform.position, b, deltaTime * this.followSpeed);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(this.Target.position - base.transform.position), deltaTime * this.rotationSpeed);
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00016048 File Offset: 0x00014248
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00008D87 File Offset: 0x00006F87
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0000B0BA File Offset: 0x000092BA
	protected internal override string __getTypeName()
	{
		return "ReplayCamera";
	}

	// Token: 0x04000252 RID: 594
	[Header("Settings")]
	[SerializeField]
	private float followSpeed = 10f;

	// Token: 0x04000253 RID: 595
	[SerializeField]
	private float followDistance = 5f;

	// Token: 0x04000254 RID: 596
	[SerializeField]
	private float followHeight = 5f;

	// Token: 0x04000255 RID: 597
	[SerializeField]
	private float rotationSpeed = 10f;

	// Token: 0x04000256 RID: 598
	[HideInInspector]
	public Transform Target;

	// Token: 0x04000257 RID: 599
	[HideInInspector]
	public Vector3 CenterPoint = Vector3.zero;
}
