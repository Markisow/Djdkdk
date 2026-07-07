using System;

// Token: 0x020000D7 RID: 215
public enum ConnectionRejectionCode
{
	// Token: 0x04000418 RID: 1048
	Unreachable,
	// Token: 0x04000419 RID: 1049
	ServerFull,
	// Token: 0x0400041A RID: 1050
	TimedOut,
	// Token: 0x0400041B RID: 1051
	Banned,
	// Token: 0x0400041C RID: 1052
	NotWhitelisted,
	// Token: 0x0400041D RID: 1053
	MissingPassword,
	// Token: 0x0400041E RID: 1054
	InvalidPassword,
	// Token: 0x0400041F RID: 1055
	MissingMods,
	// Token: 0x04000420 RID: 1056
	Unknown
}
