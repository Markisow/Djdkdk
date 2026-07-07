using System;

// Token: 0x020000C9 RID: 201
public class BasePluginState
{
	// Token: 0x06000622 RID: 1570 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
	public bool Equals(BasePluginState other)
	{
		return this.Path == other.Path && this.IsReady == other.IsReady && this.IsEnabled == other.IsEnabled;
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0002FAFC File Offset: 0x0002DCFC
	public override bool Equals(object obj)
	{
		BasePluginState basePluginState = obj as BasePluginState;
		return basePluginState != null && this.Equals(basePluginState);
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
	public override int GetHashCode()
	{
		return HashCode.Combine<string, bool, bool>(this.Path, this.IsReady, this.IsEnabled);
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
	public override string ToString()
	{
		return string.Format("Path={0}, IsReady={1}, IsEnabled={2}", this.Path, this.IsReady, this.IsEnabled);
	}

	// Token: 0x040003DB RID: 987
	public string Path;

	// Token: 0x040003DC RID: 988
	public bool IsReady;

	// Token: 0x040003DD RID: 989
	public bool IsEnabled;
}
