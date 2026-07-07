using System;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public static class ItemManager
{
	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000CCAF File Offset: 0x0000AEAF
	// (set) Token: 0x06000607 RID: 1543 RVA: 0x0000CCB6 File Offset: 0x0000AEB6
	public static List<Item> Items { get; private set; } = new List<Item>();

	// Token: 0x06000608 RID: 1544 RVA: 0x0000CCBE File Offset: 0x0000AEBE
	static ItemManager()
	{
		ItemManager.LoadItems();
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0000CCDE File Offset: 0x0000AEDE
	public static void Initialize()
	{
		ItemManagerController.Initialize();
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
	public static void Dispose()
	{
		ItemManagerController.Dispose();
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0002F69C File Offset: 0x0002D89C
	public static Item GetItemById(int id)
	{
		return ItemManager.Items.Find((Item item) => item.id == id);
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0002F6CC File Offset: 0x0002D8CC
	public static List<Item> GetItemsByCategories(string[] categories)
	{
		Predicate<string> <>9__1;
		return ItemManager.Items.FindAll(delegate(Item item)
		{
			string[] categories2 = item.categories;
			Predicate<string> match;
			if ((match = <>9__1) == null)
			{
				match = (<>9__1 = ((string itemCategory) => Array.IndexOf<string>(categories, itemCategory) >= 0));
			}
			return Array.Exists<string>(categories2, match);
		});
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0002F6FC File Offset: 0x0002D8FC
	private static void LoadItems()
	{
		try
		{
			ItemManager.Items = JsonSerializer.Deserialize<List<Item>>(Resources.Load<TextAsset>("items").text, null);
			ItemManager.Logger.Info(string.Format("Loaded {0} items", ItemManager.Items.Count));
		}
		catch (Exception ex)
		{
			ItemManager.Logger.Error("Error loading items asset: " + ex.Message);
		}
	}

	// Token: 0x040003CF RID: 975
	private static readonly global::Logger Logger = new global::Logger("ItemManager");
}
