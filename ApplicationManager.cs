using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000082 RID: 130
public static class ApplicationManager
{
	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000BB99 File Offset: 0x00009D99
	public static bool IsDedicatedGameServer
	{
		get
		{
			return Application.isBatchMode;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000485 RID: 1157 RVA: 0x00029310 File Offset: 0x00027510
	public static ushort Version
	{
		get
		{
			ushort result;
			if (!ushort.TryParse(Application.version, out result))
			{
				return 0;
			}
			return result;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000BBA0 File Offset: 0x00009DA0
	// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000BBA7 File Offset: 0x00009DA7
	private static bool IsDisplayChangeInProgress
	{
		get
		{
			return ApplicationManager.isDisplayChangeInProgress;
		}
		set
		{
			if (ApplicationManager.isDisplayChangeInProgress == value)
			{
				return;
			}
			ApplicationManager.isDisplayChangeInProgress = value;
			ApplicationManager.OnIsDisplayChangeInProgressChanged();
		}
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0000BBBD File Offset: 0x00009DBD
	public static void Initialize()
	{
		ApplicationManagerController.Initialize();
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0000BBC5 File Offset: 0x00009DC5
	public static void Dispose()
	{
		ApplicationManagerController.Dispose();
		Tween tween = ApplicationManager.mouseVisibilityDebounceTween;
		if (tween == null)
		{
			return;
		}
		tween.Kill(false);
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0000BBDC File Offset: 0x00009DDC
	public static void SetFullScreenMode(FullScreenMode mode)
	{
		ApplicationManager.Logger.Info(string.Format("Setting full screen mode to {0}", mode));
		Screen.fullScreenMode = mode;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00029330 File Offset: 0x00027530
	public static Task SetDisplay(int index)
	{
		ApplicationManager.<SetDisplay>d__13 <SetDisplay>d__;
		<SetDisplay>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<SetDisplay>d__.index = index;
		<SetDisplay>d__.<>1__state = -1;
		<SetDisplay>d__.<>t__builder.Start<ApplicationManager.<SetDisplay>d__13>(ref <SetDisplay>d__);
		return <SetDisplay>d__.<>t__builder.Task;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00029374 File Offset: 0x00027574
	public static void SetResolution(int index)
	{
		ApplicationManager.Logger.Info(string.Format("Setting resolution to {0}", index));
		if (ApplicationManager.IsDisplayChangeInProgress)
		{
			return;
		}
		List<Resolution> resolutions = Utils.GetResolutions();
		if (index < 0 || index >= resolutions.Count)
		{
			return;
		}
		Resolution resolution = resolutions[index];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0000BBFE File Offset: 0x00009DFE
	public static void SetVSync(bool isEnabled)
	{
		ApplicationManager.Logger.Info(string.Format("Setting vSync to {0}", isEnabled));
		QualitySettings.vSyncCount = (isEnabled ? 1 : 0);
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0000BC26 File Offset: 0x00009E26
	public static void SetTargetFrameRate(int targetFrameRate)
	{
		ApplicationManager.Logger.Info(string.Format("Setting target frame rate to {0}", targetFrameRate));
		Application.targetFrameRate = targetFrameRate;
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x000293E0 File Offset: 0x000275E0
	public static void SetQuality(ApplicationQuality quality)
	{
		ApplicationManager.Logger.Info(string.Format("Setting quality to {0}", quality));
		int vSyncCount = QualitySettings.vSyncCount;
		switch (quality)
		{
		case ApplicationQuality.Low:
			QualitySettings.SetQualityLevel(0, true);
			break;
		case ApplicationQuality.Medium:
			QualitySettings.SetQualityLevel(2, true);
			break;
		case ApplicationQuality.High:
			QualitySettings.SetQualityLevel(4, true);
			break;
		case ApplicationQuality.Ultra:
			QualitySettings.SetQualityLevel(5, true);
			break;
		default:
			QualitySettings.SetQualityLevel(4, true);
			break;
		}
		QualitySettings.vSyncCount = vSyncCount;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00029458 File Offset: 0x00027658
	public static void SetMouseVisibility(bool isVisible)
	{
		Tween tween = ApplicationManager.mouseVisibilityDebounceTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		ApplicationManager.mouseVisibilityDebounceTween = DOVirtual.DelayedCall(0f, delegate
		{
			ApplicationManager.Logger.Info(string.Format("Setting mouse visibility to {0}", isVisible));
			if (isVisible)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				return;
			}
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}, true);
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x000294A0 File Offset: 0x000276A0
	private static void OnIsDisplayChangeInProgressChanged()
	{
		ApplicationManager.Logger.Info(string.Format("Display change in progress: {0}", ApplicationManager.isDisplayChangeInProgress));
		EventManager.TriggerEvent("Event_OnIsDisplayChangeInProgressChanged", new Dictionary<string, object>
		{
			{
				"isDisplayChangeInProgress",
				ApplicationManager.isDisplayChangeInProgress
			}
		});
	}

	// Token: 0x040002D0 RID: 720
	private static readonly global::Logger Logger = new global::Logger("ApplicationManager");

	// Token: 0x040002D1 RID: 721
	private static bool isDisplayChangeInProgress = false;

	// Token: 0x040002D2 RID: 722
	private static Tween mouseVisibilityDebounceTween;
}
