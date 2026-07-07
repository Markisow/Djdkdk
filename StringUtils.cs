using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020001FA RID: 506
public static class StringUtils
{
	// Token: 0x06000EDB RID: 3803 RVA: 0x00014063 File Offset: 0x00012263
	static StringUtils()
	{
		StringUtils.LoadProfanityWords();
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x0004CB00 File Offset: 0x0004AD00
	private static void LoadProfanityWords()
	{
		try
		{
			string[] array = JsonSerializer.Deserialize<string[]>(Resources.Load<TextAsset>("profanity_words").text, null);
			string[] value = (from w in array
			where !string.IsNullOrWhiteSpace(w)
			orderby w.Length descending
			select Regex.Escape(w)).ToArray<string>();
			StringUtils.profanityRegex = new Regex("(?<![a-zA-Z])(" + string.Join("|", value) + ")(?![a-zA-Z])", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			StringUtils.Logger.Info(string.Format("Loaded {0} profanity words and compiled regex", array.Length));
		}
		catch (Exception ex)
		{
			StringUtils.Logger.Error("Error loading profanity words asset: " + ex.Message);
		}
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x0004CC08 File Offset: 0x0004AE08
	public static string FilterStringNotLetters(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		StringBuilder stringBuilder = new StringBuilder(text.Length);
		foreach (char c in text)
		{
			if (char.IsLetter(c))
			{
				stringBuilder.Append(c);
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0004CC5C File Offset: 0x0004AE5C
	public static string FilterStringSpecialCharacters(string text, string[] characterWhitelist = null, string[] characterBlacklist = null)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		StringBuilder stringBuilder = new StringBuilder(text.Length);
		TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator(text);
		while (textElementEnumerator.MoveNext())
		{
			string textElement = textElementEnumerator.GetTextElement();
			if (characterBlacklist == null || !characterBlacklist.Contains(textElement))
			{
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(textElement, 0);
				if (textElement.All(new Func<char, bool>(char.IsLetterOrDigit)) || textElement.All(new Func<char, bool>(char.IsWhiteSpace)) || unicodeCategory == UnicodeCategory.ConnectorPunctuation || unicodeCategory == UnicodeCategory.DashPunctuation || unicodeCategory == UnicodeCategory.OpenPunctuation || unicodeCategory == UnicodeCategory.ClosePunctuation || unicodeCategory == UnicodeCategory.InitialQuotePunctuation || unicodeCategory == UnicodeCategory.FinalQuotePunctuation || unicodeCategory == UnicodeCategory.OtherPunctuation || unicodeCategory == UnicodeCategory.MathSymbol || unicodeCategory == UnicodeCategory.CurrencySymbol || (characterWhitelist != null && characterWhitelist.Contains(textElement)))
				{
					stringBuilder.Append(textElement);
				}
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x0004CD20 File Offset: 0x0004AF20
	public static string FilterStringProfanity(string text, bool replaceWithStars = false)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		if (StringUtils.profanityRegex == null)
		{
			return text;
		}
		string text2;
		if (replaceWithStars)
		{
			text2 = StringUtils.profanityRegex.Replace(text, (Match match) => new string('*', match.Length));
		}
		else
		{
			text2 = StringUtils.profanityRegex.Replace(text, string.Empty);
		}
		if (!replaceWithStars)
		{
			text2 = Regex.Replace(text2, "\\s+", " ").Trim();
		}
		return text2;
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x0004CD9C File Offset: 0x0004AF9C
	public static string FilterStringRichText(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		string[] tagsToRemove = new string[]
		{
			"align",
			"allcaps",
			"alpha",
			"b",
			"br",
			"color",
			"cspace",
			"font",
			"font-weight",
			"gradient",
			"i",
			"indent",
			"line-height",
			"line-indent",
			"link",
			"lowercase",
			"margin",
			"mark",
			"mspace",
			"nobr",
			"noparse",
			"page",
			"pos",
			"rotate",
			"s",
			"size",
			"smallcaps",
			"space",
			"sprite",
			"strikethrough",
			"style",
			"sub",
			"sup",
			"u",
			"uppercase",
			"voffset",
			"width"
		};
		string pattern = "</?(\\w+(?:-\\w+)?)(?:\\s+[^>]*)?>";
		return Regex.Replace(text, pattern, delegate(Match match)
		{
			string value = match.Groups[1].Value.ToLower();
			if (tagsToRemove.Contains(value))
			{
				return string.Empty;
			}
			return match.Value;
		}, RegexOptions.IgnoreCase);
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00014079 File Offset: 0x00012279
	public static string WrapInColor(string text, string color)
	{
		if (string.IsNullOrEmpty(color))
		{
			return text;
		}
		return string.Concat(new string[]
		{
			"<color=",
			color,
			">",
			text,
			"</color>"
		});
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x0004CF24 File Offset: 0x0004B124
	public static string WrapInTeamColor(string text, PlayerTeam team)
	{
		string text2;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				text2 = "#404040";
			}
			else
			{
				text2 = "#d13333";
			}
		}
		else
		{
			text2 = "#3b82f6";
		}
		string text3 = text2;
		if (text3 == null)
		{
			return text;
		}
		return string.Concat(new string[]
		{
			"<color=",
			text3,
			">",
			text,
			"</color>"
		});
	}

	// Token: 0x04000918 RID: 2328
	private static readonly global::Logger Logger = new global::Logger("StringUtils");

	// Token: 0x04000919 RID: 2329
	private static Regex profanityRegex;
}
