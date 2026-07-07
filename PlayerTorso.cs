using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000036 RID: 54
[RequireComponent(typeof(MeshRendererTexturer))]
[ExecuteInEditMode]
public class PlayerTorso : MonoBehaviour
{
	// Token: 0x06000111 RID: 273 RVA: 0x000097BD File Offset: 0x000079BD
	private void Awake()
	{
		this.meshRendererTexturer = base.GetComponent<MeshRendererTexturer>();
	}

	// Token: 0x06000112 RID: 274 RVA: 0x000097CB File Offset: 0x000079CB
	public void SetUsername(string username)
	{
		if (string.IsNullOrEmpty(username))
		{
			this.usernameText.gameObject.SetActive(false);
			return;
		}
		this.usernameText.text = username;
		this.usernameText.gameObject.SetActive(true);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00009804 File Offset: 0x00007A04
	public void SetNumber(string number)
	{
		if (string.IsNullOrEmpty(number))
		{
			this.numberText.gameObject.SetActive(false);
			return;
		}
		this.numberText.text = number;
		this.numberText.gameObject.SetActive(true);
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00017D18 File Offset: 0x00015F18
	public void SetJerseyID(int jerseyID, PlayerTeam team)
	{
		Jersey jersey = this.jerseys.Find((Jersey j) => j.ID == jerseyID && j.IsForTeam(team));
		if (jersey == null)
		{
			PlayerTorso.Logger.Warning(string.Format("Tried to set invalid jerseyID {0} for team {1}", jerseyID, team));
			return;
		}
		this.meshRendererTexturer.SetTexture(jersey.Texture);
	}

	// Token: 0x040000CA RID: 202
	private static readonly global::Logger Logger = new global::Logger("PlayerTorso");

	// Token: 0x040000CB RID: 203
	[Header("Settings")]
	[SerializeField]
	private List<Jersey> jerseys = new List<Jersey>();

	// Token: 0x040000CC RID: 204
	[Header("References")]
	[SerializeField]
	private TMP_Text usernameText;

	// Token: 0x040000CD RID: 205
	[SerializeField]
	private TMP_Text numberText;

	// Token: 0x040000CE RID: 206
	private MeshRendererTexturer meshRendererTexturer;
}
