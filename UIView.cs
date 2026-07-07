using System;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x02000166 RID: 358
public class UIView : MonoBehaviour
{
	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00010E84 File Offset: 0x0000F084
	// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0003DD74 File Offset: 0x0003BF74
	public bool IsVisible
	{
		get
		{
			return this.isVisible;
		}
		set
		{
			if (this.isVisible == value)
			{
				return;
			}
			bool oldIsVisible = this.isVisible;
			this.isVisible = value;
			this.OnIsVisibileChanged(oldIsVisible, this.isVisible);
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00010E8C File Offset: 0x0000F08C
	// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0003DDA8 File Offset: 0x0003BFA8
	public bool IsFocused
	{
		get
		{
			return this.isFocused;
		}
		set
		{
			if (this.isFocused == value)
			{
				return;
			}
			bool oldIsFocused = this.isFocused;
			this.isFocused = value;
			this.OnIsFocusedChanged(oldIsFocused, this.isFocused);
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0003DDDC File Offset: 0x0003BFDC
	[HideInInspector]
	public int Order
	{
		get
		{
			if (!(this.View.parent is TemplateContainer))
			{
				return this.View.parent.IndexOf(this.View);
			}
			return this.View.parent.parent.IndexOf(this.View.parent);
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00010E94 File Offset: 0x0000F094
	// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0003DE34 File Offset: 0x0003C034
	public VisualElement View
	{
		get
		{
			return this.view;
		}
		set
		{
			if (this.view == value)
			{
				return;
			}
			VisualElement oldView = this.view;
			this.view = value;
			this.OnViewChanged(oldView, this.view);
		}
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00010E9C File Offset: 0x0000F09C
	public virtual bool Show()
	{
		if (this.IsVisible)
		{
			return false;
		}
		this.IsVisible = true;
		return true;
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x00010EB0 File Offset: 0x0000F0B0
	public virtual bool Hide()
	{
		if (!this.IsVisible || this.AlwaysVisible)
		{
			return false;
		}
		this.IsVisible = false;
		return true;
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x00010ECC File Offset: 0x0000F0CC
	public virtual bool Toggle()
	{
		if (this.IsVisible)
		{
			return this.Hide();
		}
		return this.Show();
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x00010EE3 File Offset: 0x0000F0E3
	private void OnIsVisibileChanged(bool oldIsVisible, bool newIsVisible)
	{
		this.View.style.display = (newIsVisible ? DisplayStyle.Flex : DisplayStyle.None);
		Action<UIView> onVisibility = this.OnVisibility;
		if (onVisibility == null)
		{
			return;
		}
		onVisibility(this);
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x00010F12 File Offset: 0x0000F112
	private void OnIsFocusedChanged(bool oldIsFocused, bool newIsFocused)
	{
		Action<UIView> onFocus = this.OnFocus;
		if (onFocus == null)
		{
			return;
		}
		onFocus(this);
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnViewChanged(VisualElement oldView, VisualElement newView)
	{
	}

	// Token: 0x04000642 RID: 1602
	[Header("UI Settings")]
	public bool FocusRequiresMouse;

	// Token: 0x04000643 RID: 1603
	public bool FocusIsInteractive;

	// Token: 0x04000644 RID: 1604
	public bool VisibilityRequiresMouse;

	// Token: 0x04000645 RID: 1605
	public bool VisibilityIsInteractive;

	// Token: 0x04000646 RID: 1606
	public bool AlwaysVisible;

	// Token: 0x04000647 RID: 1607
	private bool isVisible = true;

	// Token: 0x04000648 RID: 1608
	private bool isFocused;

	// Token: 0x04000649 RID: 1609
	[HideInInspector]
	public Action<UIView> OnVisibility;

	// Token: 0x0400064A RID: 1610
	[HideInInspector]
	public Action<UIView> OnFocus;

	// Token: 0x0400064B RID: 1611
	[HideInInspector]
	public VisualElement RootVisualElement;

	// Token: 0x0400064C RID: 1612
	private VisualElement view;
}
