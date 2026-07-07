using System;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class VoteManagerController : MonoBehaviour
{
	// Token: 0x06000A77 RID: 2679 RVA: 0x00010C0E File Offset: 0x0000EE0E
	private void Awake()
	{
		this.voteManager = base.GetComponent<VoteManager>();
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnDestroy()
	{
	}

	// Token: 0x0400061C RID: 1564
	private static readonly global::Logger Logger = new global::Logger("VoteManagerController");

	// Token: 0x0400061D RID: 1565
	private VoteManager voteManager;
}
