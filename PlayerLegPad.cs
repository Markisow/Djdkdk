using System;
using AYellowpaper.SerializedCollections;
using DG.Tweening;
using DG.Tweening.CustomPlugins;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class PlayerLegPad : MonoBehaviour
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060000F4 RID: 244 RVA: 0x000095EC File Offset: 0x000077EC
	// (set) Token: 0x060000F5 RID: 245 RVA: 0x000095F4 File Offset: 0x000077F4
	public PlayerLegPadState State
	{
		get
		{
			return this.state;
		}
		set
		{
			this.OnStateChanged(this.state, value);
			this.state = value;
		}
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x0000960A File Offset: 0x0000780A
	private void Awake()
	{
		this.localPosition = base.transform.localPosition;
		this.localRotation = base.transform.localRotation;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00017944 File Offset: 0x00015B44
	private void Update()
	{
		this.ShootLegPadRaycast();
		base.transform.localPosition = new Vector3(this.localPosition.x, this.localYPosition, this.localPosition.z);
		base.transform.localRotation = this.localRotation;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000895D File Offset: 0x00006B5D
	private void FixedUpdate()
	{
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x0000962E File Offset: 0x0000782E
	private void OnDestroy()
	{
		Tween tween = this.localPositionTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		Tween tween2 = this.localRotationTween;
		if (tween2 == null)
		{
			return;
		}
		tween2.Kill(false);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00017994 File Offset: 0x00015B94
	public void ShootLegPadRaycast()
	{
		Vector3 vector = base.transform.parent.TransformPoint(this.localPosition);
		vector.y = base.transform.parent.position.y;
		vector += base.transform.parent.up;
		Vector3 vector2 = -base.transform.parent.up;
		Debug.DrawRay(vector, vector2 * this.raycastDistance, Color.red);
		RaycastHit raycastHit;
		if (Physics.Raycast(vector, vector2, out raycastHit, this.raycastDistance, this.raycastLayerMask))
		{
			this.localYPosition = base.transform.parent.InverseTransformPoint(raycastHit.point).y + this.localPosition.y;
			return;
		}
		this.localYPosition = base.transform.parent.InverseTransformPoint(vector + vector2 * this.raycastDistance).y + this.localPosition.y;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00017A9C File Offset: 0x00015C9C
	private void OnStateChanged(PlayerLegPadState oldState, PlayerLegPadState newState)
	{
		Tween tween = this.localPositionTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		Tween tween2 = this.localRotationTween;
		if (tween2 != null)
		{
			tween2.Kill(false);
		}
		if (oldState == PlayerLegPadState.Butterfly && newState == PlayerLegPadState.ButterflyExtended)
		{
			this.localPosition = this.positions[oldState].localPosition;
			this.localRotation = this.positions[oldState].localRotation;
		}
		this.localPositionTween = DOTween.To(() => this.localPosition, delegate(Vector3 value)
		{
			this.localPosition = value;
		}, this.positions[newState].localPosition, this.transitionDuration);
		this.localRotationTween = DOTween.To<Quaternion, Quaternion, NoOptions>(PureQuaternionPlugin.Plug(), () => this.localRotation, delegate(Quaternion value)
		{
			this.localRotation = value;
		}, this.positions[newState].localRotation, this.transitionDuration);
	}

	// Token: 0x040000AC RID: 172
	[Header("Settings")]
	[SerializeField]
	private float raycastDistance = 1f;

	// Token: 0x040000AD RID: 173
	[SerializeField]
	private float transitionDuration = 0.15f;

	// Token: 0x040000AE RID: 174
	[Space(20f)]
	[SerializeField]
	private LayerMask raycastLayerMask;

	// Token: 0x040000AF RID: 175
	[Header("References")]
	[SerializeField]
	private SerializedDictionary<PlayerLegPadState, Transform> positions = new SerializedDictionary<PlayerLegPadState, Transform>();

	// Token: 0x040000B0 RID: 176
	private PlayerLegPadState state;

	// Token: 0x040000B1 RID: 177
	private Vector3 localPosition = Vector3.zero;

	// Token: 0x040000B2 RID: 178
	private float localYPosition;

	// Token: 0x040000B3 RID: 179
	private Quaternion localRotation = Quaternion.identity;

	// Token: 0x040000B4 RID: 180
	private Tween localPositionTween;

	// Token: 0x040000B5 RID: 181
	private Tween localRotationTween;
}
