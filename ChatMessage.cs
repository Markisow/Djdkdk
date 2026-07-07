using System;
using Unity.Collections;
using Unity.Netcode;

// Token: 0x0200009E RID: 158
public class ChatMessage : INetworkSerializable, IEquatable<ChatMessage>
{
	// Token: 0x06000525 RID: 1317 RVA: 0x0002BD14 File Offset: 0x00029F14
	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		bool flag = this.SteamID != null;
		serializer.SerializeValue<bool>(ref flag, default(FastBufferWriter.ForPrimitives));
		if (flag)
		{
			FixedString32Bytes valueOrDefault = this.SteamID.GetValueOrDefault();
			serializer.SerializeValue<FixedString32Bytes>(ref valueOrDefault, default(FastBufferWriter.ForFixedStrings));
			this.SteamID = new FixedString32Bytes?(valueOrDefault);
		}
		bool flag2 = this.Username != null;
		serializer.SerializeValue<bool>(ref flag2, default(FastBufferWriter.ForPrimitives));
		if (flag2)
		{
			FixedString32Bytes valueOrDefault2 = this.Username.GetValueOrDefault();
			serializer.SerializeValue<FixedString32Bytes>(ref valueOrDefault2, default(FastBufferWriter.ForFixedStrings));
			this.Username = new FixedString32Bytes?(valueOrDefault2);
		}
		bool flag3 = this.Team != null;
		serializer.SerializeValue<bool>(ref flag3, default(FastBufferWriter.ForPrimitives));
		if (flag3)
		{
			PlayerTeam valueOrDefault3 = this.Team.GetValueOrDefault();
			serializer.SerializeValue<PlayerTeam>(ref valueOrDefault3, default(FastBufferWriter.ForEnums));
			this.Team = new PlayerTeam?(valueOrDefault3);
		}
		serializer.SerializeValue<FixedString512Bytes>(ref this.Content, default(FastBufferWriter.ForFixedStrings));
		serializer.SerializeValue<double>(ref this.Timestamp, default(FastBufferWriter.ForPrimitives));
		serializer.SerializeValue<bool>(ref this.IsQuickChat, default(FastBufferWriter.ForPrimitives));
		serializer.SerializeValue<bool>(ref this.IsTeamChat, default(FastBufferWriter.ForPrimitives));
		serializer.SerializeValue<bool>(ref this.IsSystem, default(FastBufferWriter.ForPrimitives));
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0002BE7C File Offset: 0x0002A07C
	public bool Equals(ChatMessage other)
	{
		FixedString32Bytes? fixedString32Bytes = this.SteamID;
		FixedString32Bytes? fixedString32Bytes2 = other.SteamID;
		bool flag;
		if (fixedString32Bytes != null != (fixedString32Bytes2 != null))
		{
			flag = false;
		}
		else if (fixedString32Bytes == null)
		{
			flag = true;
		}
		else
		{
			FixedString32Bytes valueOrDefault = fixedString32Bytes.GetValueOrDefault();
			FixedString32Bytes valueOrDefault2 = fixedString32Bytes2.GetValueOrDefault();
			flag = (valueOrDefault == valueOrDefault2);
		}
		if (flag)
		{
			fixedString32Bytes2 = this.Username;
			fixedString32Bytes = other.Username;
			bool flag2;
			if (fixedString32Bytes2 != null != (fixedString32Bytes != null))
			{
				flag2 = false;
			}
			else if (fixedString32Bytes2 == null)
			{
				flag2 = true;
			}
			else
			{
				FixedString32Bytes valueOrDefault3 = fixedString32Bytes2.GetValueOrDefault();
				FixedString32Bytes valueOrDefault4 = fixedString32Bytes.GetValueOrDefault();
				flag2 = (valueOrDefault3 == valueOrDefault4);
			}
			if (flag2 && this.Content == other.Content && this.Timestamp == other.Timestamp && this.IsQuickChat == other.IsQuickChat && this.IsTeamChat == other.IsTeamChat)
			{
				return this.IsSystem == other.IsSystem;
			}
		}
		return false;
	}

	// Token: 0x04000325 RID: 805
	public FixedString32Bytes? SteamID;

	// Token: 0x04000326 RID: 806
	public FixedString32Bytes? Username;

	// Token: 0x04000327 RID: 807
	public PlayerTeam? Team;

	// Token: 0x04000328 RID: 808
	public FixedString512Bytes Content;

	// Token: 0x04000329 RID: 809
	public double Timestamp;

	// Token: 0x0400032A RID: 810
	public bool IsQuickChat;

	// Token: 0x0400032B RID: 811
	public bool IsTeamChat;

	// Token: 0x0400032C RID: 812
	public bool IsSystem;
}
