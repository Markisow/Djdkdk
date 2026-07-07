using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class UIViewController<T> : MonoBehaviour where T : UIView
{
	// Token: 0x06000ABE RID: 2750 RVA: 0x00010F34 File Offset: 0x0000F134
	public virtual void Awake()
	{
		this.uiView = base.GetComponent<T>();
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x0000895D File Offset: 0x00006B5D
	public virtual void OnDestroy()
	{
	}

	// Token: 0x0400064D RID: 1613
	private T uiView;
}
