using System;

// Token: 0x02000127 RID: 295
public class ModConfig
{
	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000834 RID: 2100 RVA: 0x0000E6DA File Offset: 0x0000C8DA
	// (set) Token: 0x06000835 RID: 2101 RVA: 0x0000E6E2 File Offset: 0x0000C8E2
	public string id { get; set; }

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000836 RID: 2102 RVA: 0x0000E6EB File Offset: 0x0000C8EB
	// (set) Token: 0x06000837 RID: 2103 RVA: 0x0000E6F3 File Offset: 0x0000C8F3
	public bool isEnabled { get; set; }

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000838 RID: 2104 RVA: 0x0000E6FC File Offset: 0x0000C8FC
	// (set) Token: 0x06000839 RID: 2105 RVA: 0x0000E704 File Offset: 0x0000C904
	public bool isClientRequired { get; set; }
}
