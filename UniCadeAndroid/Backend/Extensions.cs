using System;

namespace UniCadeAndroid.Backend
{
		public static class Extensions
		{
			public static bool ContainsIgnoreCase(this string text, string value,
				StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
			{
				return text.IndexOf(value, stringComparison) >= 0;
			}
		}
}
