using System;

// Token: 0x020000AE RID: 174
public struct ConnectionState
{
	// Token: 0x0600058C RID: 1420 RVA: 0x0000C6CF File Offset: 0x0000A8CF
	public ConnectionState()
	{
		this.Connection = null;
		this.ConnectionRejection = null;
		this.Disconnection = null;
		this.LastConnection = null;
		this.PendingConnection = null;
		this.Phase = ConnectionPhase.Disconnected;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0002DA3C File Offset: 0x0002BC3C
	public bool Equals(ConnectionState other)
	{
		return this.Connection == other.Connection && this.LastConnection == other.LastConnection && this.ConnectionRejection == other.ConnectionRejection && this.Disconnection == other.Disconnection && this.PendingConnection == other.PendingConnection && this.Phase == other.Phase;
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0002DAA0 File Offset: 0x0002BCA0
	public override bool Equals(object obj)
	{
		if (obj is ConnectionState)
		{
			ConnectionState other = (ConnectionState)obj;
			return this.Equals(other);
		}
		return false;
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0000C6FB File Offset: 0x0000A8FB
	public override int GetHashCode()
	{
		return HashCode.Combine<Connection, Connection, ConnectionRejection, Disconnection, Connection, ConnectionPhase>(this.Connection, this.LastConnection, this.ConnectionRejection, this.Disconnection, this.PendingConnection, this.Phase);
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0002DAC8 File Offset: 0x0002BCC8
	public override string ToString()
	{
		string format = "Connection: {0}, LastConnection: {1}, ConnectionRejection: {2}, Disconnection: {3}, PendingConnection: {4}, Phase: {5}";
		object[] array = new object[6];
		int num = 0;
		Connection connection = this.Connection;
		array[num] = (((connection != null) ? connection.ToString() : null) ?? "null");
		int num2 = 1;
		Connection lastConnection = this.LastConnection;
		array[num2] = (((lastConnection != null) ? lastConnection.ToString() : null) ?? "null");
		int num3 = 2;
		ConnectionRejection connectionRejection = this.ConnectionRejection;
		array[num3] = (((connectionRejection != null) ? connectionRejection.ToString() : null) ?? "null");
		int num4 = 3;
		Disconnection disconnection = this.Disconnection;
		array[num4] = (((disconnection != null) ? disconnection.ToString() : null) ?? "null");
		int num5 = 4;
		Connection pendingConnection = this.PendingConnection;
		array[num5] = (((pendingConnection != null) ? pendingConnection.ToString() : null) ?? "null");
		array[5] = this.Phase;
		return string.Format(format, array);
	}

	// Token: 0x0400036C RID: 876
	public Connection Connection;

	// Token: 0x0400036D RID: 877
	public ConnectionRejection ConnectionRejection;

	// Token: 0x0400036E RID: 878
	public Disconnection Disconnection;

	// Token: 0x0400036F RID: 879
	public Connection LastConnection;

	// Token: 0x04000370 RID: 880
	public Connection PendingConnection;

	// Token: 0x04000371 RID: 881
	public ConnectionPhase Phase;
}
