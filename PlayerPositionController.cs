using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class PlayerPositionController : MonoBehaviour
{
	// Token: 0x060000B9 RID: 185 RVA: 0x000092A4 File Offset: 0x000074A4
	private void Awake()
	{
		this.playerPosition = base.GetComponent<PlayerPosition>();
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000895D File Offset: 0x00006B5D
	public void Start()
	{
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnDestroy()
	{
	}

	// Token: 0x04000053 RID: 83
	private PlayerPosition playerPosition;
}
