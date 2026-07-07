using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[Serializable]
public class Headgear
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060000DA RID: 218 RVA: 0x000094AB File Offset: 0x000076AB
	public MeshRendererTexturer FlagMeshRendererTexturer
	{
		get
		{
			if (!this.FlagGameObject)
			{
				return null;
			}
			return this.FlagGameObject.GetComponent<MeshRendererTexturer>();
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00017680 File Offset: 0x00015880
	public bool IsForRole(PlayerRole role)
	{
		bool result;
		if (role != PlayerRole.Attacker)
		{
			if (role != PlayerRole.Goalie)
			{
				result = ((this.Role & HeadgearRole.Any) > (HeadgearRole)0);
			}
			else
			{
				result = ((this.Role & HeadgearRole.Goalie) > (HeadgearRole)0);
			}
		}
		else
		{
			result = ((this.Role & HeadgearRole.Attacker) > (HeadgearRole)0);
		}
		return result;
	}

	// Token: 0x04000090 RID: 144
	public int ID;

	// Token: 0x04000091 RID: 145
	public GameObject GameObject;

	// Token: 0x04000092 RID: 146
	public GameObject FlagGameObject;

	// Token: 0x04000093 RID: 147
	public HeadgearRole Role;
}
