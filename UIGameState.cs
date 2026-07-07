using System;
using UnityEngine.UIElements;

// Token: 0x02000191 RID: 401
public class UIGameState : UIView
{
	// Token: 0x06000BAC RID: 2988 RVA: 0x000419C0 File Offset: 0x0003FBC0
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("GameStateView", null);
		this.gameState = base.View.Query("GameState", null);
		this.blueScoreLabel = this.gameState.Query("BlueScoreLabel", null);
		this.redScoreLabel = this.gameState.Query("RedScoreLabel", null);
		this.timeLabel = this.gameState.Query("TimeLabel", null);
		this.phaseLabel = this.gameState.Query("PhaseLabel", null);
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00010F42 File Offset: 0x0000F142
	public override bool Show()
	{
		return SettingsManager.ShowGameUserInterface && base.Show();
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x00041A70 File Offset: 0x0003FC70
	public void SetScore(PlayerTeam team, int score)
	{
		if (team == PlayerTeam.Blue)
		{
			this.blueScoreLabel.text = string.Format("{0}", score);
			return;
		}
		if (team == PlayerTeam.Red)
		{
			this.redScoreLabel.text = string.Format("{0}", score);
		}
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x00041ABC File Offset: 0x0003FCBC
	public void SetTick(int tick)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)tick);
		if (timeSpan.TotalHours < 1.0)
		{
			this.timeLabel.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
			return;
		}
		this.timeLabel.text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x000118FA File Offset: 0x0000FAFA
	public void SetPhase(string text)
	{
		this.phaseLabel.text = text;
	}

	// Token: 0x04000701 RID: 1793
	private VisualElement gameState;

	// Token: 0x04000702 RID: 1794
	private Label blueScoreLabel;

	// Token: 0x04000703 RID: 1795
	private Label redScoreLabel;

	// Token: 0x04000704 RID: 1796
	private Label timeLabel;

	// Token: 0x04000705 RID: 1797
	private Label phaseLabel;
}
