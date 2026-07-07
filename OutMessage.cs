using System;
using System.Collections.Generic;
using System.Text.Json;

// Token: 0x0200015A RID: 346
public class OutMessage
{
	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00010C2D File Offset: 0x0000EE2D
	public bool IsRequestMessage
	{
		get
		{
			return this.ResponseMessageName != null;
		}
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x00010C38 File Offset: 0x0000EE38
	public OutMessage(string messageName, Dictionary<string, object> data = null, string responseMessageName = null)
	{
		this.MessageName = messageName;
		this.Data = data;
		this.ResponseMessageName = responseMessageName;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0003D030 File Offset: 0x0003B230
	public override string ToString()
	{
		string result;
		try
		{
			result = JsonSerializer.Serialize<Dictionary<string, object>>(this.Data, WebSocketManager.JsonOptions);
		}
		catch
		{
			result = null;
		}
		return result;
	}

	// Token: 0x0400061E RID: 1566
	private static readonly Logger Logger = new Logger("WebsocketManager");

	// Token: 0x0400061F RID: 1567
	public readonly string MessageName;

	// Token: 0x04000620 RID: 1568
	public readonly Dictionary<string, object> Data;

	// Token: 0x04000621 RID: 1569
	public readonly string ResponseMessageName;
}
