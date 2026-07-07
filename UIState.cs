using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x020000AC RID: 172
public struct UIState
{
	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000C5CB File Offset: 0x0000A7CB
	public bool IsInteracting
	{
		get
		{
			return this.InteractingViews != null && this.InteractingViews.Count > 0;
		}
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x0000C5E5 File Offset: 0x0000A7E5
	public UIState()
	{
		this.Phase = UIPhase.None;
		this.IsMouseRequired = false;
		this.IsMouseOverUI = false;
		this.InteractingViews = new List<UIView>();
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0002D958 File Offset: 0x0002BB58
	public bool IsViewInteracting<T>() where T : UIView
	{
		if (this.InteractingViews == null)
		{
			return false;
		}
		using (List<UIView>.Enumerator enumerator = this.InteractingViews.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current is !!0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0000C607 File Offset: 0x0000A807
	public bool IsViewTopmostInteracting<T>() where T : UIView
	{
		return this.InteractingViews != null && this.InteractingViews.Count != 0 && this.InteractingViews[this.InteractingViews.Count - 1] is !!0;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x0000C640 File Offset: 0x0000A840
	public UIView GetTopmostInteractingView()
	{
		if (this.InteractingViews == null || this.InteractingViews.Count == 0)
		{
			return null;
		}
		return this.InteractingViews[this.InteractingViews.Count - 1];
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0000C671 File Offset: 0x0000A871
	public bool Equals(UIState other)
	{
		return this.Phase == other.Phase && this.IsMouseRequired == other.IsMouseRequired && this.IsMouseOverUI == other.IsMouseOverUI && this.InteractingViews.SequenceEqual(other.InteractingViews);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0002D9BC File Offset: 0x0002BBBC
	public override bool Equals(object obj)
	{
		if (obj is UIState)
		{
			UIState other = (UIState)obj;
			return this.Equals(other);
		}
		return false;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
	public override int GetHashCode()
	{
		return HashCode.Combine<UIPhase, bool, bool, List<UIView>>(this.Phase, this.IsMouseRequired, this.IsMouseOverUI, this.InteractingViews);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0002D9E4 File Offset: 0x0002BBE4
	public override string ToString()
	{
		return string.Format("Phase: {0}, IsMouseRequired: {1}, IsMouseOverUI: {2}, IsInteracting: {3}", new object[]
		{
			this.Phase,
			this.IsMouseRequired,
			this.IsMouseOverUI,
			this.IsInteracting
		});
	}

	// Token: 0x04000364 RID: 868
	public UIPhase Phase;

	// Token: 0x04000365 RID: 869
	public bool IsMouseRequired;

	// Token: 0x04000366 RID: 870
	public bool IsMouseOverUI;

	// Token: 0x04000367 RID: 871
	public List<UIView> InteractingViews;
}
