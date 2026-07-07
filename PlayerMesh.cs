using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class PlayerMesh : MonoBehaviour
{
	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000101 RID: 257 RVA: 0x000096B4 File Offset: 0x000078B4
	// (set) Token: 0x06000102 RID: 258 RVA: 0x000096BC File Offset: 0x000078BC
	public float Stretch
	{
		get
		{
			return this.stretch;
		}
		set
		{
			if (this.stretch == value)
			{
				return;
			}
			this.stretch = value;
			this.OnStretchChanged();
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x000096D5 File Offset: 0x000078D5
	private void Awake()
	{
		this.initialGroinBonePosition = this.groinBone.localPosition;
		this.initialTorsoBonePosition = this.torsoBone.localPosition;
		this.initialHeadBonePosition = this.headBone.localPosition;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00017B7C File Offset: 0x00015D7C
	public void LookAt(Vector3 targetPosition, float deltaTime, bool rotateTorso = true, bool rotateHead = true)
	{
		Quaternion b = Utils.GetLocalLookRotation(this.torsoBone, targetPosition);
		if (rotateTorso && rotateHead)
		{
			b = Utils.GetLocalLookRotation(this.torsoBone, targetPosition);
			b = Quaternion.Slerp(Quaternion.identity, b, 0.5f);
		}
		else if (rotateTorso)
		{
			b = Utils.GetLocalLookRotation(this.torsoBone, targetPosition);
		}
		else if (rotateHead)
		{
			b = Utils.GetLocalLookRotation(this.headBone, targetPosition);
		}
		Vector3 vector = Utils.WrapEulerAngles(b.eulerAngles);
		vector = Utils.Vector3Clamp(vector, new Vector3(-11.25f, -45f, 0f), new Vector3(45f, 45f, 0f));
		if (rotateTorso)
		{
			this.torsoBone.localRotation = Quaternion.Lerp(this.torsoBone.localRotation, Quaternion.Euler(vector), this.lookAtSpeed * deltaTime);
		}
		if (rotateHead)
		{
			this.headBone.localRotation = Quaternion.Lerp(this.headBone.localRotation, Quaternion.Euler(vector), this.lookAtSpeed * deltaTime);
		}
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000970A File Offset: 0x0000790A
	public void SetUsername(string username)
	{
		this.PlayerTorso.SetUsername(username);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00009718 File Offset: 0x00007918
	public void SetNumber(string number)
	{
		this.PlayerTorso.SetNumber(number);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00009726 File Offset: 0x00007926
	public void SetLegsPadsActive(bool isActive)
	{
		this.PlayerLegPadLeft.gameObject.SetActive(isActive);
		this.PlayerLegPadRight.gameObject.SetActive(isActive);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000974A File Offset: 0x0000794A
	public void SetFlagID(int flagID)
	{
		this.PlayerHead.SetFlagID(flagID);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00009758 File Offset: 0x00007958
	public void SetHeadgearID(int headgearID, PlayerRole role)
	{
		this.PlayerHead.SetHeadgearID(headgearID, role);
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00009767 File Offset: 0x00007967
	public void SetMustacheID(int mustacheID)
	{
		this.PlayerHead.SetMustacheID(mustacheID);
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00009775 File Offset: 0x00007975
	public void SetBeardID(int beardID)
	{
		this.PlayerHead.SetBeardID(beardID);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00009783 File Offset: 0x00007983
	public void SetJerseyID(int jerseyID, PlayerTeam team)
	{
		this.PlayerTorso.SetJerseyID(jerseyID, team);
		this.PlayerGroin.SetJerseyID(jerseyID, team);
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00017C74 File Offset: 0x00015E74
	private void OnStretchChanged()
	{
		this.groinBone.localPosition = this.initialGroinBonePosition * this.Stretch;
		this.torsoBone.localPosition = this.initialTorsoBonePosition * this.Stretch;
		this.headBone.localPosition = this.initialHeadBonePosition * this.Stretch;
	}

	// Token: 0x040000B6 RID: 182
	[Header("Settings")]
	[SerializeField]
	private float lookAtSpeed = 10f;

	// Token: 0x040000B7 RID: 183
	[Header("References")]
	[SerializeField]
	private Transform groinBone;

	// Token: 0x040000B8 RID: 184
	[SerializeField]
	private Transform torsoBone;

	// Token: 0x040000B9 RID: 185
	[SerializeField]
	private Transform headBone;

	// Token: 0x040000BA RID: 186
	[SerializeField]
	public PlayerHead PlayerHead;

	// Token: 0x040000BB RID: 187
	[SerializeField]
	public PlayerTorso PlayerTorso;

	// Token: 0x040000BC RID: 188
	[SerializeField]
	public PlayerGroin PlayerGroin;

	// Token: 0x040000BD RID: 189
	[SerializeField]
	public PlayerLegPad PlayerLegPadLeft;

	// Token: 0x040000BE RID: 190
	[SerializeField]
	public PlayerLegPad PlayerLegPadRight;

	// Token: 0x040000BF RID: 191
	private float stretch = 1f;

	// Token: 0x040000C0 RID: 192
	private Vector3 initialGroinBonePosition;

	// Token: 0x040000C1 RID: 193
	private Vector3 initialTorsoBonePosition;

	// Token: 0x040000C2 RID: 194
	private Vector3 initialHeadBonePosition;
}
