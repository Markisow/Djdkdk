using System;
using UnityEngine.UIElements;

// Token: 0x020000E5 RID: 229
public class BeforeChildRemovedEvent : EventBase<BeforeChildRemovedEvent>
{
	// Token: 0x0400043F RID: 1087
	public int index;

	// Token: 0x04000440 RID: 1088
	public VisualElement child;
}
