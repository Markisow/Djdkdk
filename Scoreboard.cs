using System;
using TMPro;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class Scoreboard : MonoBehaviour
{
	// Token: 0x06000359 RID: 857 RVA: 0x000248C0 File Offset: 0x00022AC0
	public void TurnOn()
	{
		this.minutesText.gameObject.SetActive(true);
		this.secondsText.gameObject.SetActive(true);
		this.periodText.gameObject.SetActive(true);
		this.blueScoreText.gameObject.SetActive(true);
		this.redScoreText.gameObject.SetActive(true);
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00024924 File Offset: 0x00022B24
	public void TurnOff()
	{
		this.minutesText.gameObject.SetActive(false);
		this.secondsText.gameObject.SetActive(false);
		this.periodText.gameObject.SetActive(false);
		this.blueScoreText.gameObject.SetActive(false);
		this.redScoreText.gameObject.SetActive(false);
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00024988 File Offset: 0x00022B88
	public void SetTick(int tick)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)tick);
		this.minutesText.text = timeSpan.Minutes.ToString("D2");
		this.secondsText.text = timeSpan.Seconds.ToString("D2");
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0000B109 File Offset: 0x00009309
	public void SetPeriod(int period)
	{
		this.periodText.text = period.ToString();
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0000B11D File Offset: 0x0000931D
	public void SetBlueScore(int score)
	{
		this.blueScoreText.text = score.ToString();
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0000B131 File Offset: 0x00009331
	public void SetRedScore(int score)
	{
		this.redScoreText.text = score.ToString();
	}

	// Token: 0x04000259 RID: 601
	[Header("References")]
	[SerializeField]
	private TMP_Text minutesText;

	// Token: 0x0400025A RID: 602
	[SerializeField]
	private TMP_Text secondsText;

	// Token: 0x0400025B RID: 603
	[SerializeField]
	private TMP_Text periodText;

	// Token: 0x0400025C RID: 604
	[SerializeField]
	private TMP_Text blueScoreText;

	// Token: 0x0400025D RID: 605
	[SerializeField]
	private TMP_Text redScoreText;
}
