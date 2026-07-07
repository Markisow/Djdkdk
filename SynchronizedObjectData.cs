using System;
using Unity.Netcode;

// Token: 0x02000148 RID: 328
public struct SynchronizedObjectData : INetworkSerializable, IEquatable<SynchronizedObjectData>
{
	// Token: 0x060009DB RID: 2523 RVA: 0x0003A78C File Offset: 0x0003898C
	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		if (serializer.IsReader)
		{
			FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
			fastBufferReader.ReadValueSafe<ushort>(out this.NetworkObjectId, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<short>(out this.X, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<short>(out this.Y, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<short>(out this.Z, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<short>(out this.Rx, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<short>(out this.Ry, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<short>(out this.Rz, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<short>(out this.Rw, default(FastBufferWriter.ForPrimitives));
			return;
		}
		FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
		fastBufferWriter.WriteValueSafe<ushort>(this.NetworkObjectId, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<short>(this.X, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<short>(this.Y, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<short>(this.Z, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<short>(this.Rx, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<short>(this.Ry, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<short>(this.Rz, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<short>(this.Rw, default(FastBufferWriter.ForPrimitives));
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0003A918 File Offset: 0x00038B18
	public bool Equals(SynchronizedObjectData other)
	{
		return this.NetworkObjectId == other.NetworkObjectId && this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.Rx == other.Rx && this.Ry == other.Ry && this.Rz == other.Rz && this.Rw == other.Rw;
	}

	// Token: 0x040005B8 RID: 1464
	public ushort NetworkObjectId;

	// Token: 0x040005B9 RID: 1465
	public short X;

	// Token: 0x040005BA RID: 1466
	public short Y;

	// Token: 0x040005BB RID: 1467
	public short Z;

	// Token: 0x040005BC RID: 1468
	public short Rx;

	// Token: 0x040005BD RID: 1469
	public short Ry;

	// Token: 0x040005BE RID: 1470
	public short Rz;

	// Token: 0x040005BF RID: 1471
	public short Rw;
}
