using System;
using System.Globalization;
using UnityEngine.UIElements;

// Token: 0x02000193 RID: 403
public class UIHUD : UIView
{
	// Token: 0x06000BB7 RID: 2999 RVA: 0x00041BC4 File Offset: 0x0003FDC4
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("HUDView", null);
		this.staminaProgressBar = base.View.Query("StaminaProgressBar", null);
		this.speed = base.View.Query("Speed", null);
		this.speedLabel = this.speed.Query("SpeedLabel", null);
		this.unitsLabel = this.speed.Query("UnitsLabel", null);
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x000119BF File Offset: 0x0000FBBF
	public void SetStamina(float value)
	{
		this.staminaProgressBar.EnableInClassList("warning", value < 0.25f);
		this.staminaProgressBar.value = value;
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x00041C58 File Offset: 0x0003FE58
	public void SetSpeed(float value)
	{
		float num = (float)Math.Round((double)((SettingsManager.Units == Units.Metric) ? Utils.GameUnitsToMetric(value) : Utils.GameUnitsToImperial(value)), 1);
		this.speedLabel.text = num.ToString("F1", CultureInfo.InvariantCulture);
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x000119E5 File Offset: 0x0000FBE5
	public void SetUnits(string units)
	{
		this.unitsLabel.text = units;
	}

	// Token: 0x04000707 RID: 1799
	private ProgressBar staminaProgressBar;

	// Token: 0x04000708 RID: 1800
	private VisualElement speed;

	// Token: 0x04000709 RID: 1801
	private Label speedLabel;

	// Token: 0x0400070A RID: 1802
	private Label unitsLabel;
}
