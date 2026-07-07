using System;
using Unity.Collections;
using Unity.Netcode;

// Token: 0x0200012A RID: 298
public struct Server : INetworkSerializable, IEquatable<Server>
{
	// Token: 0x0600085A RID: 2138 RVA: 0x00035328 File Offset: 0x00033528
	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		if (serializer.IsReader)
		{
			FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
			fastBufferReader.ReadValueSafe<FixedString32Bytes>(out this.IpAddress, default(FastBufferWriter.ForFixedStrings));
			fastBufferReader.ReadValueSafe<ushort>(out this.Port, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<FixedString128Bytes>(out this.Name, default(FastBufferWriter.ForFixedStrings));
			fastBufferReader.ReadValueSafe<int>(out this.MaxPlayers, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<int>(out this.TickRate, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<bool>(out this.UseVoip, default(FastBufferWriter.ForPrimitives));
			return;
		}
		FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
		fastBufferWriter.WriteValueSafe<FixedString32Bytes>(this.IpAddress, default(FastBufferWriter.ForFixedStrings));
		fastBufferWriter.WriteValueSafe<ushort>(this.Port, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<FixedString128Bytes>(this.Name, default(FastBufferWriter.ForFixedStrings));
		fastBufferWriter.WriteValueSafe<int>(this.MaxPlayers, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<int>(this.TickRate, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<bool>(this.UseVoip, default(FastBufferWriter.ForPrimitives));
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0003545C File Offset: 0x0003365C
	public bool Equals(Server other)
	{
		return this.IpAddress == other.IpAddress && this.Port == other.Port && this.Name == other.Name && this.MaxPlayers == other.MaxPlayers && this.TickRate == other.TickRate && this.UseVoip == other.UseVoip;
	}

	// Token: 0x04000503 RID: 1283
	public FixedString32Bytes IpAddress;

	// Token: 0x04000504 RID: 1284
	public ushort Port;

	// Token: 0x04000505 RID: 1285
	public FixedString128Bytes Name;

	// Token: 0x04000506 RID: 1286
	public int MaxPlayers;

	// Token: 0x04000507 RID: 1287
	public int TickRate;

	// Token: 0x04000508 RID: 1288
	public bool UseVoip;
}
