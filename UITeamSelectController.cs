using System;

// Token: 0x020001D3 RID: 467
public class UITeamSelectController : UIViewController<UITeamSelect>
{
	// Token: 0x06000E06 RID: 3590 RVA: 0x000138DB File Offset: 0x00011ADB
	public override void Awake()
	{
		base.Awake();
		this.uiTeamSelect = base.GetComponent<UITeamSelect>();
	}

	// Token: 0x04000855 RID: 2133
	private UITeamSelect uiTeamSelect;
}
