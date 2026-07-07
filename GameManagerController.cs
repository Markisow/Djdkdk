using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class GameManagerController : MonoBehaviour
{
	// Token: 0x0600056C RID: 1388 RVA: 0x0000C533 File Offset: 0x0000A733
	private void Awake()
	{
		this.gameManager = base.GetComponent<GameManager>();
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnDestroy()
	{
	}

	// Token: 0x04000357 RID: 855
	private GameManager gameManager;
}
