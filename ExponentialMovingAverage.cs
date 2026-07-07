using System;

// Token: 0x020001F5 RID: 501
public struct ExponentialMovingAverage
{
	// Token: 0x06000EC9 RID: 3785 RVA: 0x0004C708 File Offset: 0x0004A908
	public ExponentialMovingAverage(int n)
	{
		this.alpha = 2.0 / (double)(n + 1);
		this.initialized = false;
		this.Value = 0.0;
		this.Variance = 0.0;
		this.StandardDeviation = 0.0;
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0004C760 File Offset: 0x0004A960
	public void Add(double newValue)
	{
		if (this.initialized)
		{
			double num = newValue - this.Value;
			this.Value += this.alpha * num;
			this.Variance = (1.0 - this.alpha) * (this.Variance + this.alpha * num * num);
			this.StandardDeviation = Math.Sqrt(this.Variance);
			return;
		}
		this.Value = newValue;
		this.initialized = true;
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x00013FD6 File Offset: 0x000121D6
	public void Reset()
	{
		this.initialized = false;
		this.Value = 0.0;
		this.Variance = 0.0;
		this.StandardDeviation = 0.0;
	}

	// Token: 0x04000909 RID: 2313
	private readonly double alpha;

	// Token: 0x0400090A RID: 2314
	private bool initialized;

	// Token: 0x0400090B RID: 2315
	public double Value;

	// Token: 0x0400090C RID: 2316
	public double Variance;

	// Token: 0x0400090D RID: 2317
	public double StandardDeviation;
}
