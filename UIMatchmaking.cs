using System;
using UI;
using UnityEngine.UIElements;

// Token: 0x02000199 RID: 409
public class UIMatchmaking : UIView
{
	// Token: 0x06000BE9 RID: 3049 RVA: 0x00042448 File Offset: 0x00040648
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("MatchmakingView", null);
		this.matching = base.View.Query("Matching", null);
		this.matchingPhaseLabel = this.matching.Query("PhaseLabel", null);
		this.matchingTimeLabel = this.matching.Query("TimeLabel", null);
		this.matchingCloseIconButtonContainer = this.matching.Query("CloseIconButtonContainer", null);
		this.matchingCloseIconButton = this.matchingCloseIconButtonContainer.Query("IconButton", null);
		this.matchingCloseIconButton.clicked += this.OnClickMatchingClose;
		this.matchingConnectButton = this.matching.Query("ConnectButton", null);
		this.matchingConnectButton.clicked += this.OnClickMatchingConnect;
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x00011D38 File Offset: 0x0000FF38
	public void SetMatchingVisibility(bool isVisible)
	{
		this.matching.style.display = (isVisible ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x00011D56 File Offset: 0x0000FF56
	public void SetMatchingPhaseText(string text)
	{
		this.matchingPhaseLabel.text = text;
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00011D64 File Offset: 0x0000FF64
	public void SetMatchingTimeVisibility(bool isVisible)
	{
		this.matchingTimeLabel.style.display = (isVisible ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00042544 File Offset: 0x00040744
	public void SetMatchingTimeText(int seconds)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)seconds);
		if (timeSpan.TotalHours < 1.0)
		{
			this.matchingTimeLabel.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
			return;
		}
		this.matchingTimeLabel.text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x00011D82 File Offset: 0x0000FF82
	public void SetMatchingConnectButtonVisibility(bool isVisible)
	{
		this.matchingConnectButton.style.display = (isVisible ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00011DA0 File Offset: 0x0000FFA0
	public void SetMatchingCloseButtonVisibility(bool isVisible)
	{
		this.matchingCloseIconButtonContainer.style.display = (isVisible ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00011DBE File Offset: 0x0000FFBE
	private void OnClickMatchingClose()
	{
		EventManager.TriggerEvent("Event_OnMatchmakingMatchingClickClose", null);
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00011DCB File Offset: 0x0000FFCB
	private void OnClickMatchingConnect()
	{
		EventManager.TriggerEvent("Event_OnMatchmakingMatchingClickConnect", null);
	}

	// Token: 0x04000727 RID: 1831
	private VisualElement matching;

	// Token: 0x04000728 RID: 1832
	private Label matchingPhaseLabel;

	// Token: 0x04000729 RID: 1833
	private Label matchingTimeLabel;

	// Token: 0x0400072A RID: 1834
	private Button matchingConnectButton;

	// Token: 0x0400072B RID: 1835
	private TemplateContainer matchingCloseIconButtonContainer;

	// Token: 0x0400072C RID: 1836
	private IconButton matchingCloseIconButton;
}
