using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class SmoothPositioner : MonoBehaviour
{
	// Token: 0x06000365 RID: 869 RVA: 0x0000B18E File Offset: 0x0000938E
	private void Start()
	{
		this.SetPosition(this.initialPosition, true);
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0000B19D File Offset: 0x0000939D
	private void OnDestroy()
	{
		Tween tween = this.positionTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		Tween tween2 = this.rotationTween;
		if (tween2 == null)
		{
			return;
		}
		tween2.Kill(false);
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00024A64 File Offset: 0x00022C64
	public void SetPosition(string positionName, bool instant = false)
	{
		if (!this.positions.ContainsKey(positionName))
		{
			SmoothPositioner.Logger.Error("Target position " + positionName + " does not exist");
			return;
		}
		if (instant)
		{
			Tween tween = this.positionTween;
			if (tween != null)
			{
				tween.Kill(false);
			}
			Tween tween2 = this.rotationTween;
			if (tween2 != null)
			{
				tween2.Kill(false);
			}
			base.transform.position = this.positions[positionName].position;
			base.transform.rotation = this.positions[positionName].rotation;
		}
		else
		{
			if (this.currentPosition == positionName)
			{
				return;
			}
			Tween tween3 = this.positionTween;
			if (tween3 != null)
			{
				tween3.Kill(false);
			}
			Tween tween4 = this.rotationTween;
			if (tween4 != null)
			{
				tween4.Kill(false);
			}
			this.positionTween = base.transform.DOMove(this.positions[positionName].position, this.transitionDuration, false).SetEase(this.transitionEase);
			this.rotationTween = base.transform.DORotateQuaternion(this.positions[positionName].rotation, this.transitionDuration).SetEase(this.transitionEase);
		}
		this.currentPosition = positionName;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00024BA0 File Offset: 0x00022DA0
	private void OnDrawGizmos()
	{
		if (!Application.isEditor)
		{
			return;
		}
		foreach (KeyValuePair<string, Transform> keyValuePair in this.positions)
		{
			string key = keyValuePair.Key;
			Transform value = keyValuePair.Value;
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(value.position, 0.05f);
			Gizmos.matrix = value.localToWorldMatrix;
			Gizmos.DrawFrustum(Vector3.zero, 60f, 1f, 0f, 1f);
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.color = Color.green;
			Gizmos.DrawLine(value.position, value.position + value.forward * 1f);
		}
	}

	// Token: 0x0400025F RID: 607
	private static readonly global::Logger Logger = new global::Logger("SmoothPositioner");

	// Token: 0x04000260 RID: 608
	[Header("Settings")]
	[SerializeField]
	private SerializedDictionary<string, Transform> positions = new SerializedDictionary<string, Transform>();

	// Token: 0x04000261 RID: 609
	[SerializeField]
	private string initialPosition;

	// Token: 0x04000262 RID: 610
	[SerializeField]
	private float transitionDuration = 0.5f;

	// Token: 0x04000263 RID: 611
	[SerializeField]
	private Ease transitionEase = Ease.Linear;

	// Token: 0x04000264 RID: 612
	private string currentPosition;

	// Token: 0x04000265 RID: 613
	private Tween positionTween;

	// Token: 0x04000266 RID: 614
	private Tween rotationTween;
}
