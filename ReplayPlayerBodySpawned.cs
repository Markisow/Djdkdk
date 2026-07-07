using System;
using Unity.Collections;
using UnityEngine;

// Token: 0x02000103 RID: 259
public struct ReplayPlayerBodySpawned
{
	// Token: 0x0400047B RID: 1147
	public ulong OwnerClientId;

	// Token: 0x0400047C RID: 1148
	public Vector3 Position;

	// Token: 0x0400047D RID: 1149
	public Quaternion Rotation;

	// Token: 0x0400047E RID: 1150
	public PlayerGameState GameState;

	// Token: 0x0400047F RID: 1151
	public PlayerCustomizationState CustomizationState;

	// Token: 0x04000480 RID: 1152
	public FixedString32Bytes Username;

	// Token: 0x04000481 RID: 1153
	public int Number;
}
