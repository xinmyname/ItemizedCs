using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Itemize.Infrastructure {

    public class Pluralize {

        private readonly List<string> _uncountables = new List<string> { 
                "access", "accommodation", "adulthood", "advertising", "advice",
                "aggression", "aid", "air", "alcohol", "anger", "applause",
                "arithmetic", "art", "assistance", "athletics", "attention",
                "bacon", "baggage", "ballet", "beauty", "beef", "beer", "biology",
                "botany", "bread", "butter", "carbon", "cash", "chaos", "cheese",
                "chess", "childhood", "clothing", "coal", "coffee", "commerce",
                "compassion", "comprehension", "content", "corruption", "cotton",
                "courage", "currency", "dancing", "danger", "data", "delight",
                "dignity", "dirt", "distribution", "dust", "economics", "education",
                "electricity", "employment", "engineering", "envy", "equipment",
                "ethics", "evidence", "evolution", "faith", "fame", "fish", "flour", "flu",
                "food", "freedom", "fuel", "fun", "furniture", "garbage", "garlic",
                "genetics", "gold", "golf", "gossip", "grammar", "gratitude", "grief",
                "ground", "guilt", "gymnastics", "hair", "happiness", "hardware",
                "harm", "hate", "hatred", "health", "heat", "height", "help", "homework",
                "honesty", "honey", "hospitality", "housework", "humour", "hunger",
                "hydrogen", "ice", "ice", "cream", "importance", "inflation", "information",
                "injustice", "innocence", "iron", "irony", "jealousy", "jelly", "judo",
                "karate", "kindness", "knowledge", "labour", "lack", "laughter", "lava",
                "leather", "leisure", "lightning", "linguistics", "litter", "livestock",
                "logic", "loneliness", "luck", "luggage", "machinery", "magic",
                "management", "mankind", "marble", "mathematics", "mayonnaise",
                "measles", "meat", "methane", "milk", "money", "mud", "music", "nature",
                "news", "nitrogen", "nonsense", "nurture", "nutrition", "obedience",
                "obesity", "oil", "oxygen", "passion", "pasta", "patience", "permission",
                "physics", "poetry", "pollution", "poverty", "power", "pronunciation",
                "psychology", "publicity", "quartz", "racism", "rain", "relaxation",
                "reliability", "research", "respect", "revenge", "rice", "rubbish",
                "rum", "salad", "satire", "seaside", "shame", "shopping", "silence",
                "sleep", "smoke", "smoking", "snow", "soap", "software", "soil",
                "sorrow", "soup", "speed", "spelling", "steam", "stuff", "stupidity",
                "sunshine", "symmetry", "tennis", "thirst", "thunder", "toast",
                "tolerance", "toys", "traffic", "transporation", "travel", "trust", "understanding",
                "unemployment", "unity", "validity", "veal", "vengeance", "violence",
                "sheep", "deer", "moose", "swine", "bison", "corps", "means", "series",
                "scissors", "species"
            };

        private readonly IList<(string rule, string template)> _rules = new List<(string rule, string template)> {
            (rule: "(th)is$", template: "$1ese"),
            (rule: "(th)at$", template: "$1ose"),
            (rule: "(millen)ium$", template: "$1ia"),
            (rule: "(l)eaf$", template: "$1eaves"),
            (rule: "(r)oof$", template: "$1oofs"),
            (rule: "(gen)us$", template: "$1era"),
            (rule: "(embarg)o$", template: "$1oes"),
            (rule: "arf$", template: "arves"),
            (rule: "^(b|tabl)eau$", template: "$1eaux"),
            (rule: "^(append|matr)ix$", template: "$1ices"),
            (rule: "^(ind)ex$", template: "$1ices"),
            (rule: "^(a)pparatus$", template: "$1pparatuses"),
            (rule: "^(a)lumna$", template: "$1lumnae"),
            (rule: "^(alg|vertebr|vit)a$", template: "$1ae"),
            (rule: "^(d)ie$", template: "$1ice"),
            (rule: "(m|l)ouse$", template: "$1ice"),
            (rule: "^(p)erson$", template: "$1eople"),
            (rule: "^(o)x$", template: "$1xen"),
            (rule: "^(c)hild$", template: "$1hildren"),
            (rule: "(g)oose$", template: "$1eese"),
            (rule: "(t)ooth$", template: "$1eeth"),
            (rule: "lf$", template: "lves"),
            (rule: "(f)oot$", template: "$1eet"),
            (rule: "^(|wo|work|fire)man$", template: "$1men"),
            (rule: "(potat|tomat|volcan)o$", template: "$1oes"),
            (rule: "(criteri|phenomen)on$", template: "$1a"),
            (rule: "(nebul)a", template: "$1ae"),
            (rule: "oof$", template: "ooves"),
            (rule: "ium$", template: "ia"),
            (rule: "um$", template: "a"),
            (rule: "oaf$", template: "oaves"),
            (rule: "(thie)f$", template: "$1ves"),
            (rule: "fe$", template: "ves"),
            (rule: "(buffal|carg|mosquit|torped|zer|vet|her|ech)o$", template: "$1oes"),
            (rule: "o$", template: "os"),
            (rule: "ch$", template: "ches"),
            (rule: "sis$", template: "ses"),
            (rule: "(corp)us$", template: "$1ora"),
            (rule: "(cact|nucle|alumn|bacill|fung|radi|stimul|syllab)us$", template: "$1i"),
            (rule: "(ax)is", template: "$1es"),
            (rule: "(sh|zz|ss)$", template: "$1es"),
            (rule: "x$", template: "xes"),
            (rule: "(t|r|l|b)y$", template: "$1ies"),
            (rule: "s$", template: "ses"),
            (rule: "$", template: "s")        
        };

        private static readonly Lazy<Pluralize> LazyInstance = new Lazy<Pluralize>(() => new Pluralize());

        public static Pluralize SharedInstance => LazyInstance.Value;

        public string PluralOf(string word = "", int count = 2) {

            if (count == 1 || String.IsNullOrEmpty(word) || _uncountables.Contains(word))
                return word;
            
            foreach (var pair in _rules)
            {
                var regex = new Regex(pair.rule, RegexOptions.IgnoreCase|RegexOptions.Compiled);

                if (!regex.IsMatch(word))
                    continue;

                string newValue = regex.Replace(word, pair.template);

                if (newValue != word)
                    return newValue;
            }

            return word;
        }
    }

    public static class StringExtensions
    {
        public static string Pluralize(this string self, int count = 2)
        {
            return Infrastructure.Pluralize.SharedInstance.PluralOf(self, count);
        }
    }
}
    



