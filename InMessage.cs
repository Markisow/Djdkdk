using System;
using System.Collections.Generic;
using System.Text.Json;
using SocketIOClient;
using SocketIOClient.Common.Messages;

// Token: 0x0200015B RID: 347
public class InMessage
{
	// Token: 0x06000A80 RID: 2688 RVA: 0x00010C66 File Offset: 0x0000EE66
	public InMessage(string messageName, IEventContext eventContext = null, IDataMessage dataMessage = null)
	{
		this.MessageName = messageName;
		this.EventContext = eventContext;
		this.DataMessage = dataMessage;
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0003D068 File Offset: 0x0003B268
	public T GetData<T>()
	{
		if (this.EventContext != null)
		{
			return this.EventContext.GetValue<T>(0);
		}
		if (this.DataMessage != null)
		{
			return this.DataMessage.GetValue<T>(0);
		}
		return default(!!0);
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0003D0A8 File Offset: 0x0003B2A8
	public void Respond(Dictionary<string, object> data)
	{
		try
		{
			if (this.EventContext == null)
			{
				throw new Exception("Cannot send response to message without event context");
			}
			string text = JsonSerializer.Serialize<Dictionary<string, object>>(data, WebSocketManager.JsonOptions);
			InMessage.Logger.Info(string.Concat(new string[]
			{
				"WebSocket sending response to message ",
				this.MessageName,
				" (",
				text,
				")"
			}));
			this.EventContext.SendAckDataAsync(new object[]
			{
				data
			});
		}
		catch (Exception ex)
		{
			InMessage.Logger.Error("WebSocket failed to send response to message " + this.MessageName + ": " + ex.Message);
		}
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00010C83 File Offset: 0x0000EE83
	public override string ToString()
	{
		if (this.EventContext != null)
		{
			return this.EventContext.RawText;
		}
		if (this.DataMessage != null)
		{
			return this.DataMessage.RawText;
		}
		return null;
	}

	// Token: 0x04000622 RID: 1570
	private static readonly Logger Logger = new Logger("WebsocketManager");

	// Token: 0x04000623 RID: 1571
	public readonly string MessageName;

	// Token: 0x04000624 RID: 1572
	public readonly IEventContext EventContext;

	// Token: 0x04000625 RID: 1573
	public readonly IDataMessage DataMessage;
}
