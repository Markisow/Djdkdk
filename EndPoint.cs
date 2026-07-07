using System;

// Token: 0x02000226 RID: 550
public class EndPoint : IEquatable<EndPoint>
{
	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000147DE File Offset: 0x000129DE
	// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x000147E6 File Offset: 0x000129E6
	public string ipAddress { get; set; }

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x000147EF File Offset: 0x000129EF
	// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x000147F7 File Offset: 0x000129F7
	public ushort port { get; set; }

	// Token: 0x06000FB6 RID: 4022 RVA: 0x00014800 File Offset: 0x00012A00
	public EndPoint(string ipAddress, ushort port)
	{
		this.ipAddress = ipAddress;
		this.port = port;
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x00014816 File Offset: 0x00012A16
	public bool Equals(EndPoint other)
	{
		return other != null && this.ipAddress == other.ipAddress && this.port == other.port;
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x0004EC20 File Offset: 0x0004CE20
	public override bool Equals(object obj)
	{
		EndPoint endPoint = obj as EndPoint;
		return endPoint != null && this.Equals(endPoint);
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00014840 File Offset: 0x00012A40
	public static bool operator ==(EndPoint a, EndPoint b)
	{
		if (a == null)
		{
			return b == null;
		}
		return a.Equals(b);
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x00014851 File Offset: 0x00012A51
	public static bool operator !=(EndPoint a, EndPoint b)
	{
		return !(a == b);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x0001485D File Offset: 0x00012A5D
	public override int GetHashCode()
	{
		return HashCode.Combine<string, ushort>(this.ipAddress, this.port);
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x00014870 File Offset: 0x00012A70
	public override string ToString()
	{
		return string.Format("{0}:{1}", this.ipAddress, this.port);
	}
}
