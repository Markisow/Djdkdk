using System;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public static class CountryUtils
{
	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00013DAD File Offset: 0x00011FAD
	// (set) Token: 0x06000E73 RID: 3699 RVA: 0x00013DB4 File Offset: 0x00011FB4
	public static List<Country> Countries { get; private set; } = new List<Country>();

	// Token: 0x06000E74 RID: 3700 RVA: 0x00013DBC File Offset: 0x00011FBC
	static CountryUtils()
	{
		CountryUtils.LoadCountries();
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0004BE84 File Offset: 0x0004A084
	public static Country GetCountryByCode(string code)
	{
		return CountryUtils.Countries.Find((Country country) => country.code.Equals(code, StringComparison.OrdinalIgnoreCase));
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0004BEB4 File Offset: 0x0004A0B4
	public static Country GetCountryByName(string name)
	{
		return CountryUtils.Countries.Find((Country country) => country.name.Equals(name, StringComparison.OrdinalIgnoreCase));
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0004BEE4 File Offset: 0x0004A0E4
	private static void LoadCountries()
	{
		try
		{
			CountryUtils.Countries = JsonSerializer.Deserialize<List<Country>>(Resources.Load<TextAsset>("countries").text, null);
			CountryUtils.Logger.Info(string.Format("Loaded {0} countries", CountryUtils.Countries.Count));
		}
		catch (Exception ex)
		{
			CountryUtils.Logger.Error("Error loading countries asset: " + ex.Message);
		}
	}

	// Token: 0x040008E1 RID: 2273
	private static readonly global::Logger Logger = new global::Logger("CountryUtils");
}
