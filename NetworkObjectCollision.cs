using System;
using Unity.Netcode;

// Token: 0x0200001D RID: 29
public struct NetworkObjectCollision : INetworkSerializable, IEquatable<NetworkObjectCollision>
{
	// Token: 0x06000099 RID: 153 RVA: 0x00016A34 File Offset: 0x00014C34
	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		if (serializer.IsReader)
		{
			FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
			fastBufferReader.ReadValueSafe<NetworkObjectReference>(out this.NetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
			fastBufferReader.ReadValueSafe<float>(out this.Time, default(FastBufferWriter.ForPrimitives));
			return;
		}
		FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
		fastBufferWriter.WriteValueSafe<NetworkObjectReference>(this.NetworkObjectReference, default(FastBufferWriter.ForNetworkSerializable));
		fastBufferWriter.WriteValueSafe<float>(this.Time, default(FastBufferWriter.ForPrimitives));
	}

	// Token: 0x0600009A RID: 154 RVA: 0x000090EF File Offset: 0x000072EF
	public bool Equals(NetworkObjectCollision other)
	{
		return this.NetworkObjectReference.Equals(other.NetworkObjectReference) && this.Time == other.Time;
	}

	// Token: 0x04000046 RID: 70
	public NetworkObjectReference NetworkObjectReference;

	// Token: 0x04000047 RID: 71
	public float Time;
}
