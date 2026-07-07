using System;
using Unity.Collections;

// Token: 0x02000100 RID: 256
public struct ReplayPlayerSpawned
{
	// Token: 0x0400046B RID: 1131
	public ulong OwnerClientId;

	// Token: 0x0400046C RID: 1132
	public PlayerGameState GameState;

	// Token: 0x0400046D RID: 1133
	public PlayerCustomizationState CustomizationState;

	// Token: 0x0400046E RID: 1134
	public PlayerHandedness Handedness;

	// Token: 0x0400046F RID: 1135
	public FixedString32Bytes SteamId;

	// Token: 0x04000470 RID: 1136
	public FixedString32Bytes Username;

	// Token: 0x04000471 RID: 1137
	public int Number;

	// Token: 0x04000472 RID: 1138
	public int PatreonLevel;

	// Token: 0x04000473 RID: 1139
	public int AdminLevel;

	// Token: 0x04000474 RID: 1140
	public bool IsMuted;
}
