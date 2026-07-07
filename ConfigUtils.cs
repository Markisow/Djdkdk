using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

// Token: 0x020001DF RID: 479
public static class ConfigUtils
{
	// Token: 0x06000E63 RID: 3683 RVA: 0x0004BC74 File Offset: 0x00049E74
	public static T LoadConfigFromFile<T>(string filePath, bool createIfNotExists = true) where T : class, new()
	{
		T result;
		if (string.IsNullOrEmpty(filePath))
		{
			result = default(!!0);
			return result;
		}
		if (!File.Exists(filePath) && createIfNotExists)
		{
			ConfigUtils.SaveConfigToFile<T>(filePath, Activator.CreateInstance<T>());
		}
		try
		{
			result = ConfigUtils.LoadConfigFromSerializedString<T>(File.ReadAllText(filePath));
		}
		catch (Exception ex)
		{
			ConfigUtils.Logger.Error("Error loading config from " + filePath + ": " + ex.Message);
			result = default(!!0);
		}
		return result;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0004BCF8 File Offset: 0x00049EF8
	public static T LoadConfigFromSerializedString<T>(string serializedString) where T : class, new()
	{
		T result;
		if (string.IsNullOrEmpty(serializedString))
		{
			result = default(!!0);
			return result;
		}
		try
		{
			result = JsonSerializer.Deserialize<T>(serializedString, null);
		}
		catch (Exception ex)
		{
			ConfigUtils.Logger.Error("Error loading config from serialized string: " + ex.Message);
			result = default(!!0);
		}
		return result;
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0004BD5C File Offset: 0x00049F5C
	public static void SaveConfigToFile<T>(string filePath, T config)
	{
		try
		{
			JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
			{
				WriteIndented = true
			};
			jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			string text = JsonSerializer.Serialize<T>(config, jsonSerializerOptions);
			ConfigUtils.Logger.Info("Serialized config " + typeof(!!0).Name + ": " + text);
			File.WriteAllText(filePath, text);
		}
		catch (Exception ex)
		{
			ConfigUtils.Logger.Error("Error saving config to " + filePath + ": " + ex.Message);
		}
	}

	// Token: 0x04000879 RID: 2169
	private static readonly Logger Logger = new Logger("ConfigUtils");
}
