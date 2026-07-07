using System;
using DG.Tweening;
using UnityEngine.UIElements;

// Token: 0x02000168 RID: 360
public class UIAnnouncements : UIView
{
	// Token: 0x06000AC1 RID: 2753 RVA: 0x0003DE68 File Offset: 0x0003C068
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("AnnouncementsView", null);
		this.announcements = base.View.Query("Announcements", null);
		this.score = this.announcements.Query("Score", null);
		this.headerLabel = this.score.Query("HeaderLabel", null);
		this.goalLabel = this.score.Query("GoalLabel", null);
		this.assistLabel = this.score.Query("AssistLabel", null);
		this.HideScore();
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x00010F42 File Offset: 0x0000F142
	public override bool Show()
	{
		return SettingsManager.ShowGameUserInterface && base.Show();
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x0003DF20 File Offset: 0x0003C120
	public void ShowScore(PlayerTeam team, Player goalPlayer, Player assistPlayer, Player secondAssistPlayer)
	{
		this.headerLabel.text = string.Empty;
		this.goalLabel.text = string.Empty;
		this.assistLabel.text = string.Empty;
		this.score.style.display = DisplayStyle.Flex;
		Tween tween = this.autoHideTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.autoHideTween = DOVirtual.DelayedCall(5f, delegate
		{
			this.HideScore();
		}, true);
		UIUtils.SetTeamClass(this.score, team);
		if (team != PlayerTeam.Blue)
		{
			if (team == PlayerTeam.Red)
			{
				this.headerLabel.text = "RED SCORES!";
			}
		}
		else
		{
			this.headerLabel.text = "BLUE SCORES!";
		}
		if (goalPlayer)
		{
			this.goalLabel.text = string.Format("#{0} {1}", goalPlayer.Number.Value, goalPlayer.Username.Value);
		}
		if (assistPlayer)
		{
			this.assistLabel.text = string.Format("#{0} {1}", assistPlayer.Number.Value, assistPlayer.Username.Value);
		}
		if (secondAssistPlayer)
		{
			Label label = this.assistLabel;
			label.text += string.Format(" & #{0} {1}", secondAssistPlayer.Number.Value, secondAssistPlayer.Username.Value);
		}
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
	public void HideScore()
	{
		this.headerLabel.text = string.Empty;
		this.goalLabel.text = string.Empty;
		this.assistLabel.text = string.Empty;
		this.score.style.display = DisplayStyle.None;
		Tween tween = this.autoHideTween;
		if (tween == null)
		{
			return;
		}
		tween.Kill(false);
	}

	// Token: 0x0400064E RID: 1614
	private VisualElement announcements;

	// Token: 0x0400064F RID: 1615
	private VisualElement score;

	// Token: 0x04000650 RID: 1616
	private Label headerLabel;

	// Token: 0x04000651 RID: 1617
	private Label goalLabel;

	// Token: 0x04000652 RID: 1618
	private Label assistLabel;

	// Token: 0x04000653 RID: 1619
	private Tween autoHideTween;
}
