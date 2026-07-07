using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
public struct ReplayPlayerBodyMove
{
	// Token: 0x04000482 RID: 1154
	public ulong OwnerClientId;

	// Token: 0x04000483 RID: 1155
	public Vector3 Position;

	// Token: 0x04000484 RID: 1156
	public Quaternion Rotation;

	// Token: 0x04000485 RID: 1157
	public float Stamina;

	// Token: 0x04000486 RID: 1158
	public float Speed;

	// Token: 0x04000487 RID: 1159
	public bool IsSprinting;

	// Token: 0x04000488 RID: 1160
	public bool IsSliding;

	// Token: 0x04000489 RID: 1161
	public bool IsStopping;

	// Token: 0x0400048A RID: 1162
	public bool IsExtendedLeft;

	// Token: 0x0400048B RID: 1163
	public bool IsExtendedRight;
}
