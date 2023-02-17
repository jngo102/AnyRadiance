namespace Modding.Delegates
{
	
	/// <summary>
	///     Called whenever localization specific strings are requested
	/// </summary>
	/// <param name="key">The key within the sheet</param>
	/// <param name="sheetTitle">The title of the sheet</param>
	/// <param name="orig">The original localized value</param>
	/// <returns>The modified localization, return *current* to keep as-is.</returns>
	public delegate string LanguageGetProxy(string key, string sheetTitle, string orig);
}