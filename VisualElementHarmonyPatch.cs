using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine.UIElements;

// Token: 0x020000E7 RID: 231
public static class VisualElementHarmonyPatch
{
	// Token: 0x060006F7 RID: 1783 RVA: 0x0000D67C File Offset: 0x0000B87C
	private static bool IsEditor(VisualElement element)
	{
		return element != null && element.panel != null && element.panel.contextType == ContextType.Editor;
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0000D69B File Offset: 0x0000B89B
	public static void Patch()
	{
		VisualElementHarmonyPatch.Logger.Info("Applying patches");
		VisualElementHarmonyPatch.harmony.PatchAll();
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0000D6B6 File Offset: 0x0000B8B6
	public static void Unpatch()
	{
		VisualElementHarmonyPatch.Logger.Info("Removing patches");
		VisualElementHarmonyPatch.harmony.UnpatchSelf();
	}

	// Token: 0x04000442 RID: 1090
	private static readonly Logger Logger = new Logger("VisualElementHarmonyPatch");

	// Token: 0x04000443 RID: 1091
	private static readonly Harmony harmony = new Harmony("Puck.VisualElement");

	// Token: 0x020000E8 RID: 232
	[HarmonyPatch(typeof(VisualElement), "IncrementVersion")]
	private static class IncrementVersionPatch
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00031524 File Offset: 0x0002F724
		[HarmonyPostfix]
		public static void Postfix(VisualElement __instance, VersionChangeType changeType)
		{
			if (VisualElementHarmonyPatch.IsEditor(__instance))
			{
				return;
			}
			if (changeType == VersionChangeType.Hierarchy)
			{
				HierarchyChangedEvent pooled = EventBase<HierarchyChangedEvent>.GetPooled();
				pooled.target = __instance;
				__instance.SendEvent(pooled);
				return;
			}
			if (changeType == VersionChangeType.DisableRendering)
			{
				RenderingToggledEvent pooled2 = EventBase<RenderingToggledEvent>.GetPooled();
				pooled2.target = __instance;
				__instance.SendEvent(pooled2);
				return;
			}
		}
	}

	// Token: 0x020000E9 RID: 233
	[HarmonyPatch(typeof(VisualElement.Hierarchy), "PutChildAtIndex")]
	private static class PutChildAtIndexPatch
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x00031570 File Offset: 0x0002F770
		[HarmonyPostfix]
		public static void Postfix(VisualElement.Hierarchy __instance, VisualElement child, int index)
		{
			FieldInfo fieldInfo = AccessTools.Field(typeof(VisualElement.Hierarchy), "m_Owner");
			VisualElement visualElement = ((fieldInfo != null) ? fieldInfo.GetValue(__instance) : null) as VisualElement;
			if (VisualElementHarmonyPatch.IsEditor(visualElement))
			{
				return;
			}
			ChildAddedEvent pooled = EventBase<ChildAddedEvent>.GetPooled();
			pooled.index = index;
			pooled.child = child;
			pooled.target = visualElement;
			visualElement.SendEvent(pooled);
		}
	}

	// Token: 0x020000EA RID: 234
	[HarmonyPatch(typeof(VisualElement.Hierarchy), "RemoveChildAtIndex")]
	private static class RemoveChildAtIndexPatch
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x000315D4 File Offset: 0x0002F7D4
		[HarmonyPrefix]
		public static bool Prefix(VisualElement.Hierarchy __instance, int index)
		{
			FieldInfo fieldInfo = AccessTools.Field(typeof(VisualElement.Hierarchy), "m_Owner");
			VisualElement visualElement = ((fieldInfo != null) ? fieldInfo.GetValue(__instance) : null) as VisualElement;
			if (VisualElementHarmonyPatch.IsEditor(visualElement))
			{
				return true;
			}
			try
			{
				BeforeChildRemovedEvent pooled = EventBase<BeforeChildRemovedEvent>.GetPooled();
				pooled.index = index;
				pooled.child = visualElement.ElementAt(index);
				pooled.target = visualElement;
				visualElement.SendEvent(pooled);
			}
			catch (Exception arg)
			{
				VisualElementHarmonyPatch.Logger.Error(string.Format("Exception in BeforeChildRemovedEvent on {0}: {1}", ((visualElement != null) ? visualElement.name : null) ?? "null", arg));
			}
			return true;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00031680 File Offset: 0x0002F880
		[HarmonyPostfix]
		public static void Postfix(VisualElement.Hierarchy __instance, int index)
		{
			FieldInfo fieldInfo = AccessTools.Field(typeof(VisualElement.Hierarchy), "m_Owner");
			VisualElement visualElement = ((fieldInfo != null) ? fieldInfo.GetValue(__instance) : null) as VisualElement;
			if (VisualElementHarmonyPatch.IsEditor(visualElement))
			{
				return;
			}
			ChildRemovedEvent pooled = EventBase<ChildRemovedEvent>.GetPooled();
			pooled.index = index;
			pooled.target = visualElement;
			visualElement.SendEvent(pooled);
		}
	}
}
