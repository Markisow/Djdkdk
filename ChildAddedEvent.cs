using System;
using UnityEngine.UIElements;

// Token: 0x020000E4 RID: 228
public class ChildAddedEvent : EventBase<ChildAddedEvent>
{
	// Token: 0x0400043D RID: 1085
	public int index;

	// Token: 0x0400043E RID: 1086
	public VisualElement child;
}
