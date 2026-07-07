using System;

// Token: 0x0200008E RID: 142
public struct TransactionState
{
	// Token: 0x060004BF RID: 1215 RVA: 0x0000BE0D File Offset: 0x0000A00D
	public bool Equals(TransactionState other)
	{
		return this.Phase == other.Phase;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00029FB4 File Offset: 0x000281B4
	public override bool Equals(object obj)
	{
		if (obj is TransactionState)
		{
			TransactionState other = (TransactionState)obj;
			return this.Equals(other);
		}
		return false;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0000BE1D File Offset: 0x0000A01D
	public override int GetHashCode()
	{
		return HashCode.Combine<TransactionPhase>(this.Phase);
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0000BE2A File Offset: 0x0000A02A
	public override string ToString()
	{
		return string.Format("Phase: {0}", this.Phase);
	}

	// Token: 0x040002F3 RID: 755
	public TransactionPhase Phase;
}
