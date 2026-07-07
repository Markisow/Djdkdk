using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class MeshRendererTexturer : MonoBehaviour
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000094 RID: 148 RVA: 0x000090AB File Offset: 0x000072AB
	public MeshRenderer MeshRenderer
	{
		get
		{
			if (!this.meshRenderer)
			{
				this.meshRenderer = base.GetComponent<MeshRenderer>();
			}
			return this.meshRenderer;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000095 RID: 149 RVA: 0x000169D0 File Offset: 0x00014BD0
	public Material Material
	{
		get
		{
			if (!this.material)
			{
				if (Application.isPlaying)
				{
					this.material = this.MeshRenderer.material;
					this.MeshRenderer.material = this.material;
					this.isMaterialInstantiated = true;
				}
				else
				{
					this.material = this.MeshRenderer.sharedMaterial;
				}
			}
			return this.material;
		}
	}

	// Token: 0x06000096 RID: 150 RVA: 0x000090CC File Offset: 0x000072CC
	private void OnDestroy()
	{
		if (this.isMaterialInstantiated)
		{
			UnityEngine.Object.Destroy(this.Material);
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000090E1 File Offset: 0x000072E1
	public void SetTexture(Texture texture)
	{
		this.Material.mainTexture = texture;
	}

	// Token: 0x04000043 RID: 67
	[Header("References")]
	[SerializeField]
	private MeshRenderer meshRenderer;

	// Token: 0x04000044 RID: 68
	[SerializeField]
	private Material material;

	// Token: 0x04000045 RID: 69
	private bool isMaterialInstantiated;
}
