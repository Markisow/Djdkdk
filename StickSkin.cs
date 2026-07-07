using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
[Serializable]
public class StickSkin
{
	// Token: 0x06000289 RID: 649 RVA: 0x00022280 File Offset: 0x00020480
	public bool IsForTeam(PlayerTeam team)
	{
		bool result;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				result = ((this.Team & StickSkinTeam.Any) > (StickSkinTeam)0);
			}
			else
			{
				result = ((this.Team & StickSkinTeam.Red) > (StickSkinTeam)0);
			}
		}
		else
		{
			result = ((this.Team & StickSkinTeam.Blue) > (StickSkinTeam)0);
		}
		return result;
	}

	// Token: 0x040001B4 RID: 436
	public int ID;

	// Token: 0x040001B5 RID: 437
	public StickSkinTeam Team;

	// Token: 0x040001B6 RID: 438
	public Material Material;
}
