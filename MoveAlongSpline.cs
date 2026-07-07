using System;
using UnityEngine;
using UnityEngine.Splines;

// Token: 0x020001E7 RID: 487
public class MoveAlongSpline : MonoBehaviour
{
	// Token: 0x06000E87 RID: 3719 RVA: 0x0004C01C File Offset: 0x0004A21C
	private void Update()
	{
		this.splinePosition += Time.deltaTime * this.speed;
		if (this.splinePosition >= 1f)
		{
			this.splinePosition = 0f;
		}
		Vector3 position = this.spline.EvaluatePosition(this.splinePosition);
		base.transform.position = position;
		base.transform.LookAt(Vector3.zero);
	}

	// Token: 0x040008E7 RID: 2279
	[Header("Settings")]
	public float speed = 1f;

	// Token: 0x040008E8 RID: 2280
	[Header("References")]
	public SplineContainer spline;

	// Token: 0x040008E9 RID: 2281
	private float splinePosition;
}
