using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class SynchronizedObjectController : MonoBehaviour
{
	// Token: 0x060003BC RID: 956 RVA: 0x0000B61C File Offset: 0x0000981C
	private void Awake()
	{
		this.synchronizedObject = base.GetComponent<SynchronizedObject>();
	}

	// Token: 0x040002A3 RID: 675
	private SynchronizedObject synchronizedObject;
}
