using System;
using UI;
using UnityEngine.UIElements;

// Token: 0x020001AB RID: 427
public class UIPlay : UIView
{
	// Token: 0x06000C98 RID: 3224 RVA: 0x0004503C File Offset: 0x0004323C
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("PlayView", null);
		this.play = base.View.Query("Play", null);
		this.closeIconButton = base.View.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnClickClose;
		this.threeVsThreeButton = this.play.Query("ThreeVsThreePlayButtonContainer", null).First().Query(null, null);
		this.threeVsThreeButton.clicked += this.OnClickThreeVersusThree;
		this.fiveVsFiveButton = this.play.Query("FiveVsFivePlayButtonContainer", null).First().Query(null, null);
		this.fiveVsFiveButton.clicked += this.OnClickFiveVersusFive;
		this.practiceButton = this.play.Query("PracticePlayButtonContainer", null).First().Query(null, null);
		this.practiceButton.clicked += this.OnClickPractice;
		this.serverBrowserButton = this.play.Query("ServerBrowserPlayButtonContainer", null).First().Query(null, null);
		this.serverBrowserButton.clicked += this.OnClickServerBrowser;
		this.statistics = this.play.Query("Statistics", null);
		this.playersLabel = this.statistics.Query("PlayersLabel", null);
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x000126ED File Offset: 0x000108ED
	public void SetThreeVsThreeButtonEnabled(bool enabled)
	{
		this.threeVsThreeButton.SetEnabled(enabled);
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x000126FB File Offset: 0x000108FB
	public void SetThreeVsThreeButtonDescription(string description)
	{
		this.threeVsThreeButton.Description = description;
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00012709 File Offset: 0x00010909
	public void SetFiveVsFiveButtonEnabled(bool enabled)
	{
		this.fiveVsFiveButton.SetEnabled(enabled);
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x00012717 File Offset: 0x00010917
	public void SetFiveVsFiveButtonDescription(string description)
	{
		this.fiveVsFiveButton.Description = description;
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00012725 File Offset: 0x00010925
	public void SetStatistics(int players)
	{
		this.playersLabel.text = string.Format("PLAYERS ONLINE: {0}", players);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00012742 File Offset: 0x00010942
	private void OnClickClose()
	{
		EventManager.TriggerEvent("Event_OnPlayClickClose", null);
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0001274F File Offset: 0x0001094F
	private void OnClickThreeVersusThree()
	{
		EventManager.TriggerEvent("Event_OnPlayClickThreeVsThree", null);
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0001275C File Offset: 0x0001095C
	private void OnClickFiveVersusFive()
	{
		EventManager.TriggerEvent("Event_OnPlayClickFiveVsFive", null);
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x00012769 File Offset: 0x00010969
	private void OnClickPractice()
	{
		EventManager.TriggerEvent("Event_OnPlayClickPractice", null);
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00012776 File Offset: 0x00010976
	private void OnClickServerBrowser()
	{
		EventManager.TriggerEvent("Event_OnPlayClickServerBrowser", null);
	}

	// Token: 0x0400078F RID: 1935
	private VisualElement play;

	// Token: 0x04000790 RID: 1936
	private IconButton closeIconButton;

	// Token: 0x04000791 RID: 1937
	private PlayButton threeVsThreeButton;

	// Token: 0x04000792 RID: 1938
	private PlayButton fiveVsFiveButton;

	// Token: 0x04000793 RID: 1939
	private PlayButton practiceButton;

	// Token: 0x04000794 RID: 1940
	private PlayButton serverBrowserButton;

	// Token: 0x04000795 RID: 1941
	private VisualElement statistics;

	// Token: 0x04000796 RID: 1942
	private Label playersLabel;
}
