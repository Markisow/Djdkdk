using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200002B RID: 43
[ExecuteInEditMode]
public class PlayerHead : MonoBehaviour
{
	// Token: 0x060000DF RID: 223 RVA: 0x000176C0 File Offset: 0x000158C0
	public void SetFlagID(int flagID)
	{
		this.headgear.ForEach(delegate(Headgear h)
		{
			if (h.FlagGameObject != null)
			{
				h.FlagGameObject.SetActive(false);
			}
		});
		if (flagID == -1)
		{
			return;
		}
		Flag flag = this.flags.FirstOrDefault((Flag f) => f.ID == flagID);
		if (flag == null)
		{
			PlayerHead.Logger.Warning(string.Format("Tried to set invalid flagID {0}", flagID));
			return;
		}
		this.headgear.ForEach(delegate(Headgear h)
		{
			if (h.FlagGameObject != null)
			{
				h.FlagGameObject.SetActive(true);
				if (h.FlagMeshRendererTexturer != null)
				{
					h.FlagMeshRendererTexturer.SetTexture(flag.Texture);
				}
			}
		});
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0001776C File Offset: 0x0001596C
	public void SetHeadgearID(int headgearID, PlayerRole role)
	{
		this.headgear.ForEach(delegate(Headgear h)
		{
			h.GameObject.SetActive(false);
		});
		if (headgearID == -1)
		{
			return;
		}
		Headgear headgear = this.headgear.FirstOrDefault((Headgear h) => h.ID == headgearID && h.IsForRole(role));
		if (headgear == null)
		{
			PlayerHead.Logger.Warning(string.Format("Tried to set invalid headgearID {0} for role {1}", headgearID, role));
			return;
		}
		headgear.GameObject.SetActive(true);
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00017814 File Offset: 0x00015A14
	public void SetMustacheID(int mustacheID)
	{
		this.mustaches.ForEach(delegate(Mustache m)
		{
			m.GameObject.SetActive(false);
		});
		if (mustacheID == -1)
		{
			return;
		}
		Mustache mustache = this.mustaches.FirstOrDefault((Mustache m) => m.ID == mustacheID);
		if (mustache == null)
		{
			PlayerHead.Logger.Warning(string.Format("Tried to set invalid mustacheID {0}", mustacheID));
			return;
		}
		mustache.GameObject.SetActive(true);
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x000178AC File Offset: 0x00015AAC
	public void SetBeardID(int beardID)
	{
		this.beards.ForEach(delegate(Beard b)
		{
			b.GameObject.SetActive(false);
		});
		if (beardID == -1)
		{
			return;
		}
		Beard beard = this.beards.FirstOrDefault((Beard b) => b.ID == beardID);
		if (beard == null)
		{
			PlayerHead.Logger.Warning(string.Format("Tried to set invalid beardID {0}", beardID));
			return;
		}
		beard.GameObject.SetActive(true);
	}

	// Token: 0x04000098 RID: 152
	private static readonly global::Logger Logger = new global::Logger("PlayerHead");

	// Token: 0x04000099 RID: 153
	[Header("References")]
	[SerializeField]
	private List<Flag> flags = new List<Flag>();

	// Token: 0x0400009A RID: 154
	[SerializeField]
	private List<Headgear> headgear = new List<Headgear>();

	// Token: 0x0400009B RID: 155
	[SerializeField]
	private List<Mustache> mustaches = new List<Mustache>();

	// Token: 0x0400009C RID: 156
	[SerializeField]
	private List<Beard> beards = new List<Beard>();
}
