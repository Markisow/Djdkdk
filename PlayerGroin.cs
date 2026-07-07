using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000024 RID: 36
[RequireComponent(typeof(MeshRendererTexturer))]
[ExecuteInEditMode]
public class PlayerGroin : MonoBehaviour
{
	// Token: 0x060000D3 RID: 211 RVA: 0x0000945B File Offset: 0x0000765B
	private void Awake()
	{
		this.meshRendererTexturer = base.GetComponent<MeshRendererTexturer>();
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00017610 File Offset: 0x00015810
	public void SetJerseyID(int jerseyID, PlayerTeam team)
	{
		Jersey jersey = this.jerseys.Find((Jersey j) => j.ID == jerseyID && j.IsForTeam(team));
		if (jersey == null)
		{
			PlayerGroin.Logger.Warning(string.Format("Tried to set invalid jerseyID {0}", jerseyID));
			return;
		}
		this.meshRendererTexturer.SetTexture(jersey.Texture);
	}

	// Token: 0x04000085 RID: 133
	private static readonly global::Logger Logger = new global::Logger("PlayerGroin");

	// Token: 0x04000086 RID: 134
	[Header("References")]
	[SerializeField]
	private List<Jersey> jerseys = new List<Jersey>();

	// Token: 0x04000087 RID: 135
	private MeshRendererTexturer meshRendererTexturer;
}
