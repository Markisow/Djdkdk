using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class PhysicsManagerController : MonoBehaviour
{
	// Token: 0x06000704 RID: 1796 RVA: 0x0000D732 File Offset: 0x0000B932
	private void Awake()
	{
		this.physicsManager = base.GetComponent<PhysicsManager>();
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnDestroy()
	{
	}

	// Token: 0x04000447 RID: 1095
	private PhysicsManager physicsManager;
}
