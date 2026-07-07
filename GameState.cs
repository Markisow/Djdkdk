using System;
using Unity.Netcode;

// Token: 0x020000A6 RID: 166
public struct GameState : INetworkSerializable, IEquatable<GameState>
{
	// Token: 0x06000553 RID: 1363 RVA: 0x0002CECC File Offset: 0x0002B0CC
	public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
	{
		if (serializer.IsReader)
		{
			FastBufferReader fastBufferReader = serializer.GetFastBufferReader();
			fastBufferReader.ReadValueSafe<GamePhase>(out this.Phase, default(FastBufferWriter.ForEnums));
			fastBufferReader.ReadValueSafe<int>(out this.Tick, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<int>(out this.Period, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<int>(out this.BlueScore, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<int>(out this.RedScore, default(FastBufferWriter.ForPrimitives));
			fastBufferReader.ReadValueSafe<bool>(out this.IsOvertime, default(FastBufferWriter.ForPrimitives));
			return;
		}
		FastBufferWriter fastBufferWriter = serializer.GetFastBufferWriter();
		fastBufferWriter.WriteValueSafe<GamePhase>(this.Phase, default(FastBufferWriter.ForEnums));
		fastBufferWriter.WriteValueSafe<int>(this.Tick, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<int>(this.Period, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<int>(this.BlueScore, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<int>(this.RedScore, default(FastBufferWriter.ForPrimitives));
		fastBufferWriter.WriteValueSafe<bool>(this.IsOvertime, default(FastBufferWriter.ForPrimitives));
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0002D000 File Offset: 0x0002B200
	public bool Equals(GameState other)
	{
		return this.Phase.Equals(other.Phase) && this.Tick == other.Tick && this.Period == other.Period && this.BlueScore == other.BlueScore && this.RedScore == other.RedScore && this.IsOvertime == other.IsOvertime;
	}

	// Token: 0x0400034F RID: 847
	public GamePhase Phase;

	// Token: 0x04000350 RID: 848
	public int Tick;

	// Token: 0x04000351 RID: 849
	public int Period;

	// Token: 0x04000352 RID: 850
	public int BlueScore;

	// Token: 0x04000353 RID: 851
	public int RedScore;

	// Token: 0x04000354 RID: 852
	public bool IsOvertime;
}
