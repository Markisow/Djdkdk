using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public static class SnapshotInterpolation
{
	// Token: 0x06000ED1 RID: 3793 RVA: 0x0001400C File Offset: 0x0001220C
	public static double Timescale(double drift, double catchupSpeed, double slowdownSpeed, double absoluteCatchupNegativeThreshold, double absoluteCatchupPositiveThreshold)
	{
		if (drift > absoluteCatchupPositiveThreshold)
		{
			return 1.0 + catchupSpeed;
		}
		if (drift < absoluteCatchupNegativeThreshold)
		{
			return 1.0 - slowdownSpeed;
		}
		return 1.0;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x00014038 File Offset: 0x00012238
	public static double DynamicAdjustment(double sendInterval, double jitterStandardDeviation, double dynamicAdjustmentTolerance)
	{
		return (sendInterval + jitterStandardDeviation) / sendInterval + dynamicAdjustmentTolerance;
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0004C808 File Offset: 0x0004AA08
	public static bool InsertIfNotExists<T>(SortedList<double, T> buffer, int bufferLimit, T snapshot) where T : Snapshot
	{
		if (buffer.Count >= bufferLimit)
		{
			return false;
		}
		int count = buffer.Count;
		buffer[snapshot.remoteTime] = snapshot;
		return buffer.Count > count;
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0004C844 File Offset: 0x0004AA44
	public static double TimelineClamp(double localTimeline, double bufferTime, double latestRemoteTime)
	{
		double num = latestRemoteTime - bufferTime;
		double min = num - bufferTime;
		double max = num + bufferTime;
		return Math.Clamp(localTimeline, min, max);
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0004C864 File Offset: 0x0004AA64
	public static void InsertAndAdjust<T>(SortedList<double, T> buffer, int bufferLimit, T snapshot, ref double localTimeline, ref double localTimescale, float sendInterval, double bufferTime, double catchupSpeed, double slowdownSpeed, ref ExponentialMovingAverage driftEma, float catchupNegativeThreshold, float catchupPositiveThreshold, ref ExponentialMovingAverage deliveryTimeEma) where T : Snapshot
	{
		if (buffer.Count == 0)
		{
			localTimeline = snapshot.remoteTime - bufferTime;
		}
		if (SnapshotInterpolation.InsertIfNotExists<T>(buffer, bufferLimit, snapshot))
		{
			if (buffer.Count >= 2)
			{
				T t = buffer.Values[buffer.Count - 2];
				double localTime = t.localTime;
				t = buffer.Values[buffer.Count - 1];
				double newValue = t.localTime - localTime;
				deliveryTimeEma.Add(newValue);
			}
			double remoteTime = snapshot.remoteTime;
			localTimeline = SnapshotInterpolation.TimelineClamp(localTimeline, bufferTime, remoteTime);
			double newValue2 = remoteTime - localTimeline;
			driftEma.Add(newValue2);
			double drift = driftEma.Value - bufferTime;
			double absoluteCatchupNegativeThreshold = (double)(sendInterval * catchupNegativeThreshold);
			double absoluteCatchupPositiveThreshold = (double)(sendInterval * catchupPositiveThreshold);
			localTimescale = SnapshotInterpolation.Timescale(drift, catchupSpeed, slowdownSpeed, absoluteCatchupNegativeThreshold, absoluteCatchupPositiveThreshold);
		}
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0004C948 File Offset: 0x0004AB48
	public static void Sample<T>(SortedList<double, T> buffer, double localTimeline, out int from, out int to, out double t) where T : Snapshot
	{
		from = -1;
		to = -1;
		t = 0.0;
		for (int i = 0; i < buffer.Count - 1; i++)
		{
			T t2 = buffer.Values[i];
			T t3 = buffer.Values[i + 1];
			if (localTimeline >= t2.remoteTime && localTimeline <= t3.remoteTime)
			{
				from = i;
				to = i + 1;
				t = (double)Mathf.InverseLerp((float)t2.remoteTime, (float)t3.remoteTime, (float)localTimeline);
				return;
			}
		}
		T t4 = buffer.Values[0];
		if (t4.remoteTime > localTimeline)
		{
			from = (to = 0);
			t = 0.0;
			return;
		}
		from = (to = buffer.Count - 1);
		t = 0.0;
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x00014041 File Offset: 0x00012241
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void StepTime(double deltaTime, ref double localTimeline, double localTimescale)
	{
		localTimeline += deltaTime * localTimescale;
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0004CA38 File Offset: 0x0004AC38
	public static void StepInterpolation<T>(SortedList<double, T> buffer, double localTimeline, out T fromSnapshot, out T toSnapshot, out double t) where T : Snapshot
	{
		int num;
		int index;
		SnapshotInterpolation.Sample<T>(buffer, localTimeline, out num, out index, out t);
		fromSnapshot = buffer.Values[num];
		toSnapshot = buffer.Values[index];
		buffer.RemoveRange(num);
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x0001404B File Offset: 0x0001224B
	public static void Step<T>(SortedList<double, T> buffer, double deltaTime, ref double localTimeline, double localTimescale, out T fromSnapshot, out T toSnapshot, out double t) where T : Snapshot
	{
		SnapshotInterpolation.StepTime(deltaTime, ref localTimeline, localTimescale);
		SnapshotInterpolation.StepInterpolation<T>(buffer, localTimeline, out fromSnapshot, out toSnapshot, out t);
	}
}
