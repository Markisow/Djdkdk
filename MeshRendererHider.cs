using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class MeshRendererHider : MonoBehaviour
{
	// Token: 0x0600008F RID: 143 RVA: 0x0000905A File Offset: 0x0000725A
	private void Awake()
	{
		if (this.useChildrenMeshRenderers)
		{
			this.meshRenderers = new List<MeshRenderer>(base.GetComponentsInChildren<MeshRenderer>(true));
			this.meshRenderers.RemoveAll((MeshRenderer meshRenderer) => this.meshRendererBlacklist.Contains(meshRenderer));
		}
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00016928 File Offset: 0x00014B28
	public void HideMeshRenderers()
	{
		foreach (MeshRenderer meshRenderer in this.meshRenderers)
		{
			meshRenderer.enabled = false;
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x0001697C File Offset: 0x00014B7C
	public void ShowMeshRenderers()
	{
		foreach (MeshRenderer meshRenderer in this.meshRenderers)
		{
			meshRenderer.enabled = true;
		}
	}

	// Token: 0x04000040 RID: 64
	[Header("Settings")]
	[SerializeField]
	public List<MeshRenderer> meshRenderers;

	// Token: 0x04000041 RID: 65
	[SerializeField]
	public List<MeshRenderer> meshRendererBlacklist;

	// Token: 0x04000042 RID: 66
	[SerializeField]
	public bool useChildrenMeshRenderers = true;
}
