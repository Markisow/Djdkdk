using System;
using Unity.Netcode;

// Token: 0x02000047 RID: 71
public struct PlayerGameState : INetworkSerializable, IEquatable<PlayerGameState>
{
	// Token: 0x06000216 RID: 534 RVA: 0x0001F9BC File Offset: 0x0001DBBC
	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		if (serializer.IsReader)
		{
			FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
			fastBufferReader.ReadValueSafe<PlayerPhase>(out this.Phase, default(FastBufferWriter.ForEnums));
			fastBufferReader.ReadValueSafe<PlayerTeam>(out this.Team, default(FastBufferWriter.ForEnums));
			fastBufferReader.ReadValueSafe<PlayerRole>(out this.Role, default(FastBufferWriter.ForEnums));
			return;
		}
		FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
		fastBufferWriter.WriteValueSafe<PlayerPhase>(this.Phase, default(FastBufferWriter.ForEnums));
		fastBufferWriter.WriteValueSafe<PlayerTeam>(this.Team, default(FastBufferWriter.ForEnums));
		fastBufferWriter.WriteValueSafe<PlayerRole>(this.Role, default(FastBufferWriter.ForEnums));
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00009F41 File Offset: 0x00008141
	public bool Equals(PlayerGameState other)
	{
		return this.Phase == other.Phase && this.Team == other.Team && this.Role == other.Role;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00009F6F File Offset: 0x0000816F
	public override string ToString()
	{
		return string.Format("Phase: {0}, Team: {1}, Role: {2}", this.Phase, this.Team, this.Role);
	}

	// Token: 0x0400016E RID: 366
	public PlayerPhase Phase;

	// Token: 0x0400016F RID: 367
	public PlayerTeam Team;

	// Token: 0x04000170 RID: 368
	public PlayerRole Role;
}
