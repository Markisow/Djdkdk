using System;
using DG.Tweening;
using UI;
using UnityEngine.UIElements;

// Token: 0x020001A5 RID: 421
public class Overlay
{
	// Token: 0x06000C70 RID: 3184 RVA: 0x000447C0 File Offset: 0x000429C0
	public Overlay(VisualElement visualElement, string identifier, bool requiresSpinner = false)
	{
		this.VisualElement = visualElement;
		this.Identifier = identifier;
		this.VisualElement.style.display = DisplayStyle.None;
		this.VisualElement.style.opacity = 0f;
		this.spinner = this.VisualElement.Query(null, null);
		this.spinner.style.display = (requiresSpinner ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00044848 File Offset: 0x00042A48
	public void Show()
	{
		Tween tween = this.fadeOutTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		if (this.fadeInTween != null)
		{
			this.fadeInTween.Kill(false);
		}
		if (this.autoHideTween != null)
		{
			this.autoHideTween.Kill(false);
		}
		Action showing = this.Showing;
		if (showing != null)
		{
			showing();
		}
		if (this.FadeIn)
		{
			this.fadeInTween = DOVirtual.Float(this.VisualElement.style.opacity.value, 1f, this.FadeTime, delegate(float value)
			{
				this.VisualElement.style.opacity = value;
			}).OnStart(delegate
			{
				this.VisualElement.style.display = DisplayStyle.Flex;
				Action shown2 = this.Shown;
				if (shown2 == null)
				{
					return;
				}
				shown2();
			});
			if (this.AutoHide)
			{
				this.autoHideTween = DOVirtual.DelayedCall(this.FadeTime + this.HideTimeout, delegate
				{
					this.Hide();
				}, true);
				return;
			}
		}
		else
		{
			this.VisualElement.style.display = DisplayStyle.Flex;
			this.VisualElement.style.opacity = 1f;
			Action shown = this.Shown;
			if (shown != null)
			{
				shown();
			}
			if (this.AutoHide)
			{
				this.autoHideTween = DOVirtual.DelayedCall(this.HideTimeout, delegate
				{
					this.Hide();
				}, true);
			}
		}
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0004498C File Offset: 0x00042B8C
	public void Hide()
	{
		Tween tween = this.fadeInTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		if (this.fadeOutTween != null)
		{
			this.fadeOutTween.Kill(false);
		}
		if (this.autoHideTween != null)
		{
			this.autoHideTween.Kill(false);
		}
		Action hiding = this.Hiding;
		if (hiding != null)
		{
			hiding();
		}
		if (this.FadeOut)
		{
			this.fadeOutTween = DOVirtual.Float(this.VisualElement.style.opacity.value, 0f, this.FadeTime, delegate(float value)
			{
				this.VisualElement.style.opacity = value;
			}).OnComplete(delegate
			{
				this.VisualElement.style.display = DisplayStyle.None;
				Action hidden2 = this.Hidden;
				if (hidden2 == null)
				{
					return;
				}
				hidden2();
			});
			return;
		}
		this.VisualElement.style.display = DisplayStyle.None;
		this.VisualElement.style.opacity = 0f;
		Action hidden = this.Hidden;
		if (hidden == null)
		{
			return;
		}
		hidden();
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x00012482 File Offset: 0x00010682
	public void Dispose()
	{
		Tween tween = this.fadeInTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		Tween tween2 = this.fadeOutTween;
		if (tween2 != null)
		{
			tween2.Kill(false);
		}
		Tween tween3 = this.autoHideTween;
		if (tween3 == null)
		{
			return;
		}
		tween3.Kill(false);
	}

	// Token: 0x04000770 RID: 1904
	public VisualElement VisualElement;

	// Token: 0x04000771 RID: 1905
	public string Identifier;

	// Token: 0x04000772 RID: 1906
	public bool RequiresSpinner;

	// Token: 0x04000773 RID: 1907
	public bool FadeIn;

	// Token: 0x04000774 RID: 1908
	public bool FadeOut;

	// Token: 0x04000775 RID: 1909
	public float FadeTime;

	// Token: 0x04000776 RID: 1910
	public bool AutoHide;

	// Token: 0x04000777 RID: 1911
	public float HideTimeout;

	// Token: 0x04000778 RID: 1912
	public Action Showing;

	// Token: 0x04000779 RID: 1913
	public Action Shown;

	// Token: 0x0400077A RID: 1914
	public Action Hiding;

	// Token: 0x0400077B RID: 1915
	public Action Hidden;

	// Token: 0x0400077C RID: 1916
	private Tween fadeInTween;

	// Token: 0x0400077D RID: 1917
	private Tween fadeOutTween;

	// Token: 0x0400077E RID: 1918
	private Tween autoHideTween;

	// Token: 0x0400077F RID: 1919
	private Spinner spinner;
}
