using System;
using UnityEngine;

// Token: 0x02000209 RID: 521
public class Vector3PIDController
{
	// Token: 0x06000F73 RID: 3955 RVA: 0x0004E944 File Offset: 0x0004CB44
	public Vector3PIDController(float proportionalGain = 0f, float integralGain = 0f, float derivativeGain = 0f)
	{
		this.proportionalGain = proportionalGain;
		this.integralGain = integralGain;
		this.derivativeGain = derivativeGain;
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x0004E9C4 File Offset: 0x0004CBC4
	public Vector3 Update(float deltaTime, Vector3 currentValue, Vector3 targetValue)
	{
		if (deltaTime <= 0f)
		{
			return Vector3.zero;
		}
		Vector3 a = targetValue - currentValue;
		Vector3 vector = (a - this.errorLast) / deltaTime;
		this.errorLast = a;
		Vector3 a2 = (currentValue - this.valueLast) / deltaTime;
		this.valueLast = currentValue;
		Vector3 value = this.integrationStored + a * deltaTime;
		this.integrationStored = this.ClampVector3(value, -this.integralSaturation, this.integralSaturation);
		Vector3 vector2 = Vector3.zero;
		if (this.derivativeInitialized)
		{
			if (this.derivativeMeasurement == DerivativeMeasurement.Velocity)
			{
				vector2 = -a2;
			}
			else
			{
				vector2 = vector;
			}
			vector2 = Vector3.Lerp(this.derivativeLast, vector2, this.derivativeSmoothing);
			this.derivativeLast = vector2;
		}
		else
		{
			this.derivativeInitialized = true;
			this.derivativeLast = Vector3.zero;
		}
		Vector3 a3 = this.proportionalGain * a;
		Vector3 b = this.integralGain * this.integrationStored;
		Vector3 b2 = this.derivativeGain * vector2;
		Vector3 value2 = a3 + b + b2;
		return this.ClampVector3(value2, this.outputMin, this.outputMax);
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x0004EAF0 File Offset: 0x0004CCF0
	public Vector3 UpdateAngle(float deltaTime, Vector3 currentValue, Vector3 targetValue)
	{
		if (deltaTime <= 0f)
		{
			return Vector3.zero;
		}
		Vector3 a = this.AngleDifference(targetValue, currentValue);
		Vector3 vector = this.AngleDifference(a, this.errorLast) / deltaTime;
		this.errorLast = a;
		Vector3 a2 = this.AngleDifference(currentValue, this.valueLast) / deltaTime;
		this.valueLast = currentValue;
		Vector3 value = this.integrationStored + a * deltaTime;
		this.integrationStored = this.ClampVector3(value, -this.integralSaturation, this.integralSaturation);
		Vector3 vector2 = Vector3.zero;
		if (this.derivativeInitialized)
		{
			if (this.derivativeMeasurement == DerivativeMeasurement.Velocity)
			{
				vector2 = -a2;
			}
			else
			{
				vector2 = vector;
			}
			vector2 = Vector3.Lerp(this.derivativeLast, vector2, this.derivativeSmoothing);
			this.derivativeLast = vector2;
		}
		else
		{
			this.derivativeInitialized = true;
			this.derivativeLast = Vector3.zero;
		}
		Vector3 a3 = this.proportionalGain * a;
		Vector3 b = this.integralGain * this.integrationStored;
		Vector3 b2 = this.derivativeGain * vector2;
		Vector3 value2 = a3 + b + b2;
		return this.ClampVector3(value2, this.outputMin, this.outputMax);
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x000145EC File Offset: 0x000127EC
	public void Reset()
	{
		this.derivativeInitialized = false;
		this.errorLast = Vector3.zero;
		this.valueLast = Vector3.zero;
		this.integrationStored = Vector3.zero;
		this.derivativeLast = Vector3.zero;
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00014621 File Offset: 0x00012821
	private Vector3 AngleDifference(Vector3 a, Vector3 b)
	{
		return new Vector3(Mathf.DeltaAngle(b.x, a.x), Mathf.DeltaAngle(b.y, a.y), Mathf.DeltaAngle(b.z, a.z));
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x0001465B File Offset: 0x0001285B
	private Vector3 ClampVector3(Vector3 value, float min, float max)
	{
		return new Vector3(Mathf.Clamp(value.x, min, max), Mathf.Clamp(value.y, min, max), Mathf.Clamp(value.z, min, max));
	}

	// Token: 0x0400094A RID: 2378
	public float proportionalGain;

	// Token: 0x0400094B RID: 2379
	public float integralGain;

	// Token: 0x0400094C RID: 2380
	public float integralSaturation = float.MaxValue;

	// Token: 0x0400094D RID: 2381
	public float derivativeGain;

	// Token: 0x0400094E RID: 2382
	public float derivativeSmoothing = 1f;

	// Token: 0x0400094F RID: 2383
	public float outputMin = float.MinValue;

	// Token: 0x04000950 RID: 2384
	public float outputMax = float.MaxValue;

	// Token: 0x04000951 RID: 2385
	private Vector3 errorLast = Vector3.zero;

	// Token: 0x04000952 RID: 2386
	private Vector3 valueLast = Vector3.zero;

	// Token: 0x04000953 RID: 2387
	private Vector3 integrationStored = Vector3.zero;

	// Token: 0x04000954 RID: 2388
	private Vector3 derivativeLast = Vector3.zero;

	// Token: 0x04000955 RID: 2389
	private bool derivativeInitialized;

	// Token: 0x04000956 RID: 2390
	public DerivativeMeasurement derivativeMeasurement;
}
