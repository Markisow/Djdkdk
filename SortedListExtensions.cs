using System;
using System.Collections.Generic;

// Token: 0x020001F7 RID: 503
public static class SortedListExtensions
{
	// Token: 0x06000ED0 RID: 3792 RVA: 0x0004C7DC File Offset: 0x0004A9DC
	public static void RemoveRange<T, U>(this SortedList<T, U> list, int amount)
	{
		int num = 0;
		while (num < amount && num < list.Count)
		{
			list.RemoveAt(0);
			num++;
		}
	}
}
