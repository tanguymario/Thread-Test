using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Extensions methods.
/// This class contains extensions methods to avoid redundance
/// </summary>
public static class ExtensionsMethods 
{
	/// <summary>
	/// Determines if is null the specified something.
	/// </summary>
	/// <returns><c>true</c> if is null the specified something; otherwise, <c>false</c>.</returns>
	/// <param name="something">Something.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static bool IsNull<T>(this T something)
	{
		return something == null;
	}

    /// <summary>
    /// Determines if a parameter is not null.
    /// </summary>
    /// <param name="something">Something.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
	public static bool Exists<T>(this T something)
	{
		return !IsNull(something);
	}
}
