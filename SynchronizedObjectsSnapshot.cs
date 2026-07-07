using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020001FE RID: 510
public struct SynchronizedObjectsSnapshot : Snapshot
{
	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000140E6 File Offset: 0x000122E6
	// (set) Token: 0x06000EED RID: 3821 RVA: 0x000140EE File Offset: 0x000122EE
	public double remoteTime { readonly get; set; }

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000140F7 File Offset: 0x000122F7
	// (set) Token: 0x06000EEF RID: 3823 RVA: 0x000140FF File Offset: 0x000122FF
	public double localTime { readonly get; set; }

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00014108 File Offset: 0x00012308
	// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x00014110 File Offset: 0x00012310
	public List<SynchronizedObjectSnapshot> snapshots { readonly get; set; }

	// Token: 0x06000EF2 RID: 3826 RVA: 0x00014119 File Offset: 0x00012319
	public SynchronizedObjectsSnapshot(double remoteTime, double localTime, List<SynchronizedObjectSnapshot> snapshots)
	{
		this.remoteTime = remoteTime;
		this.localTime = localTime;
		this.snapshots = snapshots;
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x0004CFC4 File Offset: 0x0004B1C4
	public static void Interpolate(SynchronizedObjectsSnapshot from, SynchronizedObjectsSnapshot to, double t)
	{
		using (List<SynchronizedObjectSnapshot>.Enumerator enumerator = to.snapshots.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SynchronizedObjectSnapshot toSnapshot = enumerator.Current;
				SynchronizedObjectSnapshot synchronizedObjectSnapshot = from.snapshots.FirstOrDefault((SynchronizedObjectSnapshot snapshot) => snapshot.SynchronizedObject == toSnapshot.SynchronizedObject);
				if ((synchronizedObjectSnapshot != null && !(synchronizedObjectSnapshot.SynchronizedObject == null)) || (toSnapshot != null && !(toSnapshot.SynchronizedObject == null)))
				{
					if (synchronizedObjectSnapshot == null || synchronizedObjectSnapshot.SynchronizedObject == null)
					{
						toSnapshot.SynchronizedObject.transform.position = toSnapshot.Position;
						toSnapshot.SynchronizedObject.transform.rotation = toSnapshot.Rotation;
						toSnapshot.SynchronizedObject.PredictedLinearVelocity = toSnapshot.LinearVelocity;
						toSnapshot.SynchronizedObject.PredictedAngularVelocity = toSnapshot.AngularVelocity;
					}
					else if (toSnapshot == null || toSnapshot.SynchronizedObject == null)
					{
						synchronizedObjectSnapshot.SynchronizedObject.transform.position = toSnapshot.Position;
						synchronizedObjectSnapshot.SynchronizedObject.transform.rotation = toSnapshot.Rotation;
						synchronizedObjectSnapshot.SynchronizedObject.PredictedLinearVelocity = toSnapshot.LinearVelocity;
						synchronizedObjectSnapshot.SynchronizedObject.PredictedAngularVelocity = toSnapshot.AngularVelocity;
					}
					else
					{
						toSnapshot.SynchronizedObject.transform.position = Vector3.LerpUnclamped(synchronizedObjectSnapshot.Position, toSnapshot.Position, (float)t);
						toSnapshot.SynchronizedObject.transform.rotation = Quaternion.SlerpUnclamped(synchronizedObjectSnapshot.Rotation, toSnapshot.Rotation, (float)t);
						toSnapshot.SynchronizedObject.PredictedLinearVelocity = Vector3.LerpUnclamped(synchronizedObjectSnapshot.LinearVelocity, toSnapshot.LinearVelocity, (float)t);
						toSnapshot.SynchronizedObject.PredictedAngularVelocity = Vector3.LerpUnclamped(synchronizedObjectSnapshot.AngularVelocity, toSnapshot.AngularVelocity, (float)t);
					}
				}
			}
		}
	}
}
