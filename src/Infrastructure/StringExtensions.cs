using System.Collections.Generic;
using System.Text;

namespace Itemize.Infrastructure
{
    public static class StringExtensions {        

        public static string Pluralized(this string self, int count = 2) {

            int pluralStart = self.IndexOf('^');

            if (pluralStart == -1)
                return self;

            int pluralEnd = self.IndexOf(' ', pluralStart);

            if (pluralEnd == -1)
                pluralEnd = self.Length - 1;

            string singular = self.Substring(pluralStart+1, pluralEnd-pluralStart);
            string plural = Infrastructure.Pluralize.SharedInstance.PluralOf(singular, count);

            string text = self.Remove(pluralStart, pluralEnd-pluralStart+1);
            text = text.Insert(pluralStart, plural);

            return text;
        }

        public static bool StartsWithAny(this string text, IEnumerable<string> those) {

            foreach (string that in those) {
                if (text.StartsWith(that))
                    return true;
            }

            return false;
        }

        public static string Capitalized(this string text) {

            var builder = new StringBuilder(text);

            return builder.ToString();
        }

        public static string ClearOptionals(this string text) {

            // Replace optional text
            while (text.Contains("? ")) {
                text = text.Replace("? ", "");
            }

            return text;
        }
    }
}
