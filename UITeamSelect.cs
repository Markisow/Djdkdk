using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

// Token: 0x020001D2 RID: 466
public class UITeamSelect : UIView
{
	// Token: 0x06000E01 RID: 3585 RVA: 0x0004A86C File Offset: 0x00048A6C
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("TeamSelectView", null);
		this.teamSelect = base.View.Query("TeamSelect", null);
		this.blueButton = this.teamSelect.Query("BlueButton", null);
		this.blueButton.clicked += this.OnClickTeamBlue;
		this.redButton = this.teamSelect.Query("RedButton", null);
		this.redButton.clicked += this.OnClickTeamRed;
		this.spectatorButton = this.teamSelect.Query("SpectatorButton", null);
		this.spectatorButton.clicked += this.OnClickTeamSpectator;
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x00013875 File Offset: 0x00011A75
	private void OnClickTeamBlue()
	{
		EventManager.TriggerEvent("Event_OnTeamSelectClickTeam", new Dictionary<string, object>
		{
			{
				"team",
				PlayerTeam.Blue
			}
		});
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x00013897 File Offset: 0x00011A97
	private void OnClickTeamRed()
	{
		EventManager.TriggerEvent("Event_OnTeamSelectClickTeam", new Dictionary<string, object>
		{
			{
				"team",
				PlayerTeam.Red
			}
		});
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x000138B9 File Offset: 0x00011AB9
	private void OnClickTeamSpectator()
	{
		EventManager.TriggerEvent("Event_OnTeamSelectClickTeam", new Dictionary<string, object>
		{
			{
				"team",
				PlayerTeam.Spectator
			}
		});
	}

	// Token: 0x04000851 RID: 2129
	private VisualElement teamSelect;

	// Token: 0x04000852 RID: 2130
	private Button blueButton;

	// Token: 0x04000853 RID: 2131
	private Button redButton;

	// Token: 0x04000854 RID: 2132
	private Button spectatorButton;
}
