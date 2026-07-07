using System;
using System.Diagnostics;

// Token: 0x020000E0 RID: 224
public static class PatchManager
{
	// Token: 0x060006EF RID: 1775 RVA: 0x000314A4 File Offset: 0x0002F6A4
	public static void Initialize()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		VisualElementHarmonyPatch.Patch();
		stopwatch.Stop();
		PatchManager.Logger.Info(string.Format("Patching took {0}ms", stopwatch.ElapsedMilliseconds));
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x000314E4 File Offset: 0x0002F6E4
	public static void Dispose()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		VisualElementHarmonyPatch.Unpatch();
		stopwatch.Stop();
		PatchManager.Logger.Info(string.Format("Unpatching took {0}ms", stopwatch.ElapsedMilliseconds));
	}

	// Token: 0x04000436 RID: 1078
	private static readonly Logger Logger = new Logger("PatchManager");
}
