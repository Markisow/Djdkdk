using System;

// Token: 0x0200020C RID: 524
public class TCPServerPreviewRequest : TCPServerMessage
{
	// Token: 0x06000F7C RID: 3964 RVA: 0x0001469A File Offset: 0x0001289A
	public TCPServerPreviewRequest()
	{
		base.type = TCPServerMessageType.PreviewRequest;
	}
}
