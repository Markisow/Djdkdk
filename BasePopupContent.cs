using System;
using UnityEngine.UIElements;

// Token: 0x020001B0 RID: 432
public class BasePopupContent
{
	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00012854 File Offset: 0x00010A54
	// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0001285C File Offset: 0x00010A5C
	public VisualElement VisualElement { get; set; }

	// Token: 0x06000CBE RID: 3262 RVA: 0x00012865 File Offset: 0x00010A65
	public BasePopupContent(VisualTreeAsset asset)
	{
		this.asset = asset;
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00012874 File Offset: 0x00010A74
	public virtual void Initialize()
	{
		this.VisualElement = this.asset.Instantiate();
		Action initialized = this.Initialized;
		if (initialized == null)
		{
			return;
		}
		initialized();
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00012897 File Offset: 0x00010A97
	public virtual void Dispose()
	{
		Action disposed = this.Disposed;
		if (disposed == null)
		{
			return;
		}
		disposed();
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0000895D File Offset: 0x00006B5D
	internal virtual void Update()
	{
	}

	// Token: 0x040007A1 RID: 1953
	public Action Initialized;

	// Token: 0x040007A2 RID: 1954
	public Action Disposed;

	// Token: 0x040007A3 RID: 1955
	private VisualTreeAsset asset;
}
