using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class PuckElevationIndicator : MonoBehaviour
{
	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000302 RID: 770 RVA: 0x0000AC5C File Offset: 0x00008E5C
	// (set) Token: 0x06000303 RID: 771 RVA: 0x0000AC64 File Offset: 0x00008E64
	public bool IsVisible
	{
		get
		{
			return this.isVisible;
		}
		set
		{
			this.isVisible = value;
			this.planeMeshRenderer.enabled = this.isVisible;
			this.lineRenderer.enabled = this.isVisible;
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0000AC8F File Offset: 0x00008E8F
	private void Awake()
	{
		this.lineRenderer.positionCount = 2;
		this.material = this.planeMeshRenderer.material;
		this.planeMeshRenderer.enabled = false;
		this.lineRenderer.enabled = false;
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0000ACC6 File Offset: 0x00008EC6
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.material);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00023D54 File Offset: 0x00021F54
	private void Update()
	{
		if (!this.IsVisible)
		{
			return;
		}
		if (!this.material)
		{
			return;
		}
		Debug.DrawRay(base.transform.position, Vector3.down * float.PositiveInfinity, Color.black);
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, float.PositiveInfinity, this.raycastLayerMask))
		{
			this.planeMeshRenderer.transform.position = raycastHit.point - Vector3.up * this.raycastVerticalOffset;
			this.planeMeshRenderer.transform.rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
			this.material.SetFloat("_Size", Mathf.Clamp(raycastHit.distance / this.maximumDistance, 0f, 1f));
			this.UpdateLineRendererPositions(raycastHit.point);
			this.planeMeshRenderer.enabled = true;
			this.lineRenderer.enabled = true;
			return;
		}
		this.planeMeshRenderer.enabled = false;
		this.lineRenderer.enabled = false;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0000ACD3 File Offset: 0x00008ED3
	private void UpdateLineRendererPositions(Vector3 hitPosition)
	{
		this.lineRenderer.SetPosition(0, hitPosition);
		this.lineRenderer.SetPosition(1, base.transform.position);
	}

	// Token: 0x0400021D RID: 541
	[Header("Settings")]
	[SerializeField]
	private float maximumDistance = 15f;

	// Token: 0x0400021E RID: 542
	[SerializeField]
	private float raycastVerticalOffset = 0.01f;

	// Token: 0x0400021F RID: 543
	[SerializeField]
	private LayerMask raycastLayerMask;

	// Token: 0x04000220 RID: 544
	[Header("References")]
	[SerializeField]
	private MeshRenderer planeMeshRenderer;

	// Token: 0x04000221 RID: 545
	[SerializeField]
	private LineRenderer lineRenderer;

	// Token: 0x04000222 RID: 546
	private bool isVisible;

	// Token: 0x04000223 RID: 547
	private Material material;
}
