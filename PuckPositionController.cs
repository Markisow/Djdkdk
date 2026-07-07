using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class PuckPositionController : MonoBehaviour
{
	// Token: 0x06000314 RID: 788 RVA: 0x0000ADB2 File Offset: 0x00008FB2
	private void Awake()
	{
		this.puckPosition = base.GetComponent<PuckPosition>();
	}

	// Token: 0x04000226 RID: 550
	private PuckPosition puckPosition;
}
