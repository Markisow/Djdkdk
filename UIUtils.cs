using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

// Token: 0x02000204 RID: 516
public static class UIUtils
{
	// Token: 0x06000F32 RID: 3890 RVA: 0x0004DD28 File Offset: 0x0004BF28
	public static void SetTeamClass(VisualElement element, PlayerTeam team)
	{
		foreach (object obj in Enum.GetValues(typeof(PlayerTeam)))
		{
			PlayerTeam team2 = (PlayerTeam)obj;
			element.EnableInClassList(UIUtils.GetClassFromTeam(team2), false);
		}
		element.EnableInClassList(UIUtils.GetClassFromTeam(team), true);
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x000142DF File Offset: 0x000124DF
	public static string GetClassFromTeam(PlayerTeam team)
	{
		return "team" + team.ToString();
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
	public static List<VisualElement> GetVisualElementChildren(VisualElement element, bool recursive = false)
	{
		if (recursive)
		{
			List<VisualElement> list = new List<VisualElement>();
			foreach (VisualElement visualElement in element.hierarchy.Children())
			{
				list.Add(visualElement);
				list.AddRange(UIUtils.GetVisualElementChildren(visualElement, true));
			}
			return list;
		}
		return new List<VisualElement>(element.hierarchy.Children());
	}
}
