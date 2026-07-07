using System;
using UnityEngine;

// Token: 0x020001F4 RID: 500
public class PIDController
{
	// Token: 0x06000EC4 RID: 3780 RVA: 0x0004C4C0 File Offset: 0x0004A6C0
	public PIDController(float proportionalGain = 0f, float integralGain = 0f, float derivativeGain = 0f)
	{
		this.proportionalGain = proportionalGain;
		this.integralGain = integralGain;
		this.derivativeGain = derivativeGain;
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0004C514 File Offset: 0x0004A714
	public float Update(float deltaTime, float currentValue, float targetValue)
	{
		if (deltaTime <= 0f)
		{
			return 0f;
		}
		float num = targetValue - currentValue;
		float num2 = (num - this.errorLast) / deltaTime;
		this.errorLast = num;
		float num3 = (currentValue - this.valueLast) / deltaTime;
		this.valueLast = currentValue;
		float value = this.integrationStored + num * deltaTime;
		this.integrationStored = Mathf.Clamp(value, -this.integralSaturation, this.integralSaturation);
		float num4 = 0f;
		if (this.derivativeInitialized)
		{
			if (this.derivativeMeasurement == DerivativeMeasurement.Velocity)
			{
				num4 = -num3;
			}
			else
			{
				num4 = num2;
			}
			num4 = Mathf.Lerp(this.derivativeLast, num4, this.derivativeSmoothing);
			this.derivativeLast = num4;
		}
		else
		{
			this.derivativeInitialized = true;
			this.derivativeLast = 0f;
		}
		float num5 = this.proportionalGain * num;
		float num6 = this.integralGain * this.integrationStored;
		float num7 = this.derivativeGain * num4;
		return Mathf.Clamp(num5 + num6 + num7, this.outputMin, this.outputMax);
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0004C608 File Offset: 0x0004A808
	public float UpdateAngle(float deltaTime, float currentValue, float targetValue)
	{
		if (deltaTime <= 0f)
		{
			return 0f;
		}
		float num = this.AngleDifference(targetValue, currentValue);
		float num2 = this.AngleDifference(num, this.errorLast) / deltaTime;
		this.errorLast = num;
		float num3 = this.AngleDifference(currentValue, this.valueLast) / deltaTime;
		this.valueLast = currentValue;
		float value = this.integrationStored + num * deltaTime;
		this.integrationStored = Mathf.Clamp(value, -this.integralSaturation, this.integralSaturation);
		float num4 = 0f;
		if (this.derivativeInitialized)
		{
			if (this.derivativeMeasurement == DerivativeMeasurement.Velocity)
			{
				num4 = -num3;
			}
			else
			{
				num4 = num2;
			}
			num4 = Mathf.Lerp(this.derivativeLast, num4, this.derivativeSmoothing);
			this.derivativeLast = num4;
		}
		else
		{
			this.derivativeInitialized = true;
			this.derivativeLast = 0f;
		}
		float num5 = this.proportionalGain * num;
		float num6 = this.integralGain * this.integrationStored;
		float num7 = this.derivativeGain * num4;
		return Mathf.Clamp(num5 + num6 + num7, this.outputMin, this.outputMax);
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x00013F98 File Offset: 0x00012198
	public void Reset()
	{
		this.derivativeInitialized = false;
		this.errorLast = 0f;
		this.valueLast = 0f;
		this.integrationStored = 0f;
		this.derivativeLast = 0f;
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00013FCD File Offset: 0x000121CD
	private float AngleDifference(float a, float b)
	{
		return Mathf.DeltaAngle(b, a);
	}

	// Token: 0x040008FC RID: 2300
	public float proportionalGain;

	// Token: 0x040008FD RID: 2301
	public float integralGain;

	// Token: 0x040008FE RID: 2302
	public float integralSaturation = float.MaxValue;

	// Token: 0x040008FF RID: 2303
	public float derivativeGain;

	// Token: 0x04000900 RID: 2304
	public float derivativeSmoothing = 1f;

	// Token: 0x04000901 RID: 2305
	public float outputMin = float.MinValue;

	// Token: 0x04000902 RID: 2306
	public float outputMax = float.MaxValue;

	// Token: 0x04000903 RID: 2307
	private float errorLast;

	// Token: 0x04000904 RID: 2308
	private float valueLast;

	// Token: 0x04000905 RID: 2309
	private float integrationStored;

	// Token: 0x04000906 RID: 2310
	private float derivativeLast;

	// Token: 0x04000907 RID: 2311
	private bool derivativeInitialized;

	// Token: 0x04000908 RID: 2312
	public DerivativeMeasurement derivativeMeasurement;
}
