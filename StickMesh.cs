using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class StickMesh : MonoBehaviour
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A6A4 File Offset: 0x000088A4
	[HideInInspector]
	public Collider ShaftCollider
	{
		get
		{
			return this.shaftCollider;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A6AC File Offset: 0x000088AC
	[HideInInspector]
	public Collider BladeCollider
	{
		get
		{
			return this.bladeCollider;
		}
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000A6B4 File Offset: 0x000088B4
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.stickMeshRenderer.material);
		UnityEngine.Object.Destroy(this.shaftTapeMeshRenderer.material);
		UnityEngine.Object.Destroy(this.bladeTapeMeshRenderer.material);
	}

	// Token: 0x0600028F RID: 655 RVA: 0x000222C0 File Offset: 0x000204C0
	public void SetSkinID(int skinID, PlayerTeam team)
	{
		StickSkin stickSkin = this.skins.Find((StickSkin s) => s.ID == skinID && s.IsForTeam(team));
		if (stickSkin == null)
		{
			StickMesh.Logger.Warning(string.Format("Tried to set invalid skinID {0}", skinID));
			return;
		}
		UnityEngine.Object.Destroy(this.stickMeshRenderer.material);
		this.stickMeshRenderer.material = new Material(stickSkin.Material);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00022344 File Offset: 0x00020544
	public void SetShaftTapeID(int shaftTapeID)
	{
		if (shaftTapeID == -1)
		{
			this.shaftTapeGameObject.SetActive(false);
			return;
		}
		StickTape stickTape = this.shaftTapes.Find((StickTape t) => t.ID == shaftTapeID && t.Material != null);
		if (stickTape == null)
		{
			StickMesh.Logger.Warning(string.Format("Tried to set invalid shaftTapeID {0}", shaftTapeID));
			return;
		}
		this.shaftTapeGameObject.SetActive(true);
		UnityEngine.Object.Destroy(this.shaftTapeMeshRenderer.material);
		this.shaftTapeMeshRenderer.material = new Material(stickTape.Material);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x000223E4 File Offset: 0x000205E4
	public void SetBladeTapeID(int bladeTapeID)
	{
		if (bladeTapeID == -1)
		{
			this.bladeTapeGameObject.SetActive(false);
			return;
		}
		StickTape stickTape = this.bladeTapes.Find((StickTape t) => t.ID == bladeTapeID && t.Material != null);
		if (stickTape == null)
		{
			StickMesh.Logger.Warning(string.Format("Tried to set invalid bladeTapeID {0}", bladeTapeID));
			return;
		}
		this.bladeTapeGameObject.SetActive(true);
		UnityEngine.Object.Destroy(this.bladeTapeMeshRenderer.material);
		this.bladeTapeMeshRenderer.material = new Material(stickTape.Material);
	}

	// Token: 0x040001B9 RID: 441
	private static readonly global::Logger Logger = new global::Logger("StickMesh");

	// Token: 0x040001BA RID: 442
	[Header("Settings")]
	[SerializeField]
	private List<StickSkin> skins = new List<StickSkin>();

	// Token: 0x040001BB RID: 443
	[SerializeField]
	private List<StickTape> shaftTapes = new List<StickTape>();

	// Token: 0x040001BC RID: 444
	[SerializeField]
	private List<StickTape> bladeTapes = new List<StickTape>();

	// Token: 0x040001BD RID: 445
	[Header("References")]
	[SerializeField]
	private MeshRenderer stickMeshRenderer;

	// Token: 0x040001BE RID: 446
	[SerializeField]
	private GameObject shaftTapeGameObject;

	// Token: 0x040001BF RID: 447
	[SerializeField]
	private MeshRenderer shaftTapeMeshRenderer;

	// Token: 0x040001C0 RID: 448
	[SerializeField]
	private GameObject bladeTapeGameObject;

	// Token: 0x040001C1 RID: 449
	[SerializeField]
	private MeshRenderer bladeTapeMeshRenderer;

	// Token: 0x040001C2 RID: 450
	[Space(20f)]
	[SerializeField]
	private Collider shaftCollider;

	// Token: 0x040001C3 RID: 451
	[SerializeField]
	private Collider bladeCollider;
}
