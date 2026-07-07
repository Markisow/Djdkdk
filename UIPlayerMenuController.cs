using System;

// Token: 0x020001AF RID: 431
public class UIPlayerMenuController : UIViewController<UIPlayerMenu>
{
	// Token: 0x06000CB9 RID: 3257 RVA: 0x00012830 File Offset: 0x00010A30
	public override void Awake()
	{
		base.Awake();
		this.uiPlayerMenu = base.GetComponent<UIPlayerMenu>();
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x00012844 File Offset: 0x00010A44
	public override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x0400079F RID: 1951
	private UIPlayerMenu uiPlayerMenu;
}
