using System;
using DG.Tweening;
using UnityEngine.UIElements;

// Token: 0x02000181 RID: 385
public class UIChatMessage
{
	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00011210 File Offset: 0x0000F410
	private double expiryTimestamp
	{
		get
		{
			return this.ChatMessage.Timestamp + (double)(this.ExpiryTime * 1000f);
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001122B File Offset: 0x0000F42B
	private float expiresInTime
	{
		get
		{
			return (float)(this.expiryTimestamp - Utils.GetTimestamp()) / 1000f;
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00011240 File Offset: 0x0000F440
	private bool isExpired
	{
		get
		{
			return Utils.GetTimestamp() > this.expiryTimestamp;
		}
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00040538 File Offset: 0x0003E738
	public UIChatMessage(ChatMessage chatMessage, VisualElement visualElement, float expiryTime = 5f)
	{
		this.ChatMessage = chatMessage;
		this.VisualElement = visualElement;
		this.ExpiryTime = expiryTime;
		this.label = this.VisualElement.Query(null, null);
		this.Focus();
		this.StartExpiryTween();
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x0001124F File Offset: 0x0000F44F
	public void Focus()
	{
		Tween tween = this.blurTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.label.EnableInClassList("blurred", false);
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00011274 File Offset: 0x0000F474
	public void Blur()
	{
		if (!this.isExpired)
		{
			this.StartExpiryTween();
			return;
		}
		Tween tween = this.blurTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.label.EnableInClassList("blurred", true);
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x000112A8 File Offset: 0x0000F4A8
	public void Dispose()
	{
		Tween tween = this.blurTween;
		if (tween == null)
		{
			return;
		}
		tween.Kill(false);
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x000112BB File Offset: 0x0000F4BB
	private void StartExpiryTween()
	{
		Tween tween = this.blurTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.blurTween = DOVirtual.DelayedCall(this.expiresInTime, delegate
		{
			this.Blur();
		}, true);
	}

	// Token: 0x040006C2 RID: 1730
	public ChatMessage ChatMessage;

	// Token: 0x040006C3 RID: 1731
	public VisualElement VisualElement;

	// Token: 0x040006C4 RID: 1732
	public float ExpiryTime;

	// Token: 0x040006C5 RID: 1733
	private Label label;

	// Token: 0x040006C6 RID: 1734
	private Tween blurTween;
}
