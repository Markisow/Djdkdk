using System;

// Token: 0x0200020D RID: 525
public class TCPServerPreviewResponse : TCPServerMessage
{
	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000F7D RID: 3965 RVA: 0x000146A9 File Offset: 0x000128A9
	// (set) Token: 0x06000F7E RID: 3966 RVA: 0x000146B1 File Offset: 0x000128B1
	public string name { get; set; }

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000F7F RID: 3967 RVA: 0x000146BA File Offset: 0x000128BA
	// (set) Token: 0x06000F80 RID: 3968 RVA: 0x000146C2 File Offset: 0x000128C2
	public int players { get; set; }

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000F81 RID: 3969 RVA: 0x000146CB File Offset: 0x000128CB
	// (set) Token: 0x06000F82 RID: 3970 RVA: 0x000146D3 File Offset: 0x000128D3
	public int maxPlayers { get; set; }

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000F83 RID: 3971 RVA: 0x000146DC File Offset: 0x000128DC
	// (set) Token: 0x06000F84 RID: 3972 RVA: 0x000146E4 File Offset: 0x000128E4
	public bool isPasswordProtected { get; set; }

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000F85 RID: 3973 RVA: 0x000146ED File Offset: 0x000128ED
	// (set) Token: 0x06000F86 RID: 3974 RVA: 0x000146F5 File Offset: 0x000128F5
	public string[] clientRequiredModIds { get; set; }

	// Token: 0x06000F87 RID: 3975 RVA: 0x000146FE File Offset: 0x000128FE
	public TCPServerPreviewResponse()
	{
		base.type = TCPServerMessageType.PreviewResponse;
	}
}
