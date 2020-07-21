using System;
using System.Text.RegularExpressions;

namespace Differential.Context
{
    public class SingularToPlural
    {
        public SingularToPlural()
        {
        }

        public static string ConvertToPlural(string word)
        {
			if (word.EndsWith("s") ||
				word.EndsWith("sh") ||
				word.EndsWith("ch") ||
				word.EndsWith("o") ||
				word.EndsWith("x"))
			{
				return word + "es";
			}
			else if (word.EndsWith("f") || word.EndsWith("fe"))
			{
				return Regex.Replace(word, "fe?$", "ves");
			}
			else if (Regex.IsMatch(word,".*[^aiueo]y"))
			{
				return Regex.Replace(word, "y$", "ies");
			}
			else
			{
				return word + "s";
			}
		}
    }
}
