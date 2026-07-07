using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
[Serializable]
public class Jersey
{
	// Token: 0x0600010F RID: 271 RVA: 0x00017CD8 File Offset: 0x00015ED8
	public bool IsForTeam(PlayerTeam team)
	{
		bool result;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				result = ((this.Team & JerseyTeam.Any) > (JerseyTeam)0);
			}
			else
			{
				result = ((this.Team & JerseyTeam.Red) > (JerseyTeam)0);
			}
		}
		else
		{
			result = ((this.Team & JerseyTeam.Blue) > (JerseyTeam)0);
		}
		return result;
	}

	// Token: 0x040000C7 RID: 199
	public int ID;

	// Token: 0x040000C8 RID: 200
	public JerseyTeam Team;

	// Token: 0x040000C9 RID: 201
	public Texture Texture;
}
