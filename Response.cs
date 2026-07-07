using System;

// Token: 0x0200020E RID: 526
public class Response<TSuccessData, TErrorData>
{
	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0001470D File Offset: 0x0001290D
	// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00014715 File Offset: 0x00012915
	public bool success { get; set; }

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0001471E File Offset: 0x0001291E
	// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00014726 File Offset: 0x00012926
	public TSuccessData data { get; set; }

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0001472F File Offset: 0x0001292F
	// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00014737 File Offset: 0x00012937
	public TErrorData errorData { get; set; }
}
