using System;

// Token: 0x020001AA RID: 426
public class UIPauseMenuController : UIViewController<UIPauseMenu>
{
	// Token: 0x06000C95 RID: 3221 RVA: 0x000126C9 File Offset: 0x000108C9
	public override void Awake()
	{
		base.Awake();
		this.uiPauseMenu = base.GetComponent<UIPauseMenu>();
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x000126DD File Offset: 0x000108DD
	public override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x0400078E RID: 1934
	private UIPauseMenu uiPauseMenu;
}
