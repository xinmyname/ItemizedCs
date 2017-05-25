using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Itemize.Infrastructure {

    public class Pluralize
    {
        public struct RulePair
        {
            public string Rule { get; set; }
            public string Template { get; set; }
        }

        private readonly List<string> _uncountables;
        private readonly IList<RulePair> _rules = new List<RulePair>();
        private static readonly Lazy<Pluralize> LazyInstance = new Lazy<Pluralize>(() => new Pluralize());

        public static Pluralize SharedInstance => LazyInstance.Value;

        private Pluralize()
        {
            _uncountables = new List<string>
            { 
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
                "unemployment", "unity", "validity", "veal", "vengeance", "violence"
            };

            Add(rule: "$", with: "$1s");
            Add(rule: "s$", with: "$1ses");
            Add(rule: "(t|r|l|b)y$", with: "$1ies");
            Add(rule: "x$", with: "$1xes");
            Add(rule: "(sh|zz|ss)$", with: "$1es");
            Add(rule: "(ax)is", with: "$1es");
            Add(rule: "(cact|nucle|alumn|bacill|fung|radi|stimul|syllab)us$", with: "$1i");
            Add(rule: "(corp)us$", with: "$1ora");
            Add(rule: "sis$", with: "$1ses");
            Add(rule: "ch$", with: "$1ches");
            Add(rule: "o$", with: "$1os");
            Add(rule: "(buffal|carg|mosquit|torped|zer|vet|her|ech)o$", with: "$1oes");
            Add(rule: "fe$", with: "$1ves");
            Add(rule: "(thie)f$", with: "$1ves");
            Add(rule: "oaf$", with: "$1oaves");
            Add(rule: "um$", with: "$1a");
            Add(rule: "ium$", with: "$1ia");
            Add(rule: "oof$", with: "$1ooves");
            Add(rule: "(nebul)a", with: "$1ae");
            Add(rule: "(criteri|phenomen)on$", with: "$1a");
            Add(rule: "(potat|tomat|volcan)o$", with: "$1oes");
            Add(rule: "^(|wo|work|fire)man$", with: "$1men");
            Add(rule: "(f)oot$", with: "$1eet");
            Add(rule: "lf$", with: "$1lves");
            Add(rule: "(t)ooth$", with: "$1eeth");
            Add(rule: "(g)oose$", with: "$1eese");
            Add(rule: "^(c)hild$", with: "$1hildren");
            Add(rule: "^(o)x$", with: "$1xen");
            Add(rule: "^(p)erson$", with: "$1eople");
            Add(rule: "(m|l)ouse$", with: "$1ice");
            Add(rule: "^(d)ie$", with: "$1ice");
            Add(rule: "^(alg|vertebr|vit)a$", with: "$1ae");
            Add(rule: "^(a)lumna$", with: "$1lumnae");
            Add(rule: "^(a)pparatus$", with: "$1pparatuses");
            Add(rule: "^(ind)ex$", with: "$1ices");
            Add(rule: "^(append|matr)ix$", with: "$1ices");
            Add(rule: "^(b|tabl)eau$", with: "$1eaux");
            Add(rule: "arf$", with: "$1arves");
            Add(rule: "(embarg)o$", with: "$1oes");
            Add(rule: "(gen)us$", with: "$1era");
            Add(rule: "(r)oof$", with: "$1oofs");
            Add(rule: "(l)eaf$", with: "$1eaves");
            Add(rule: "(millen)ium$", with: "$1ia");
            Add(rule: "(th)at$", with: "$1ose");
            Add(rule: "(th)is$", with: "$1ese");

            Unchanging(word: "sheep");
            Unchanging(word: "deer");
            Unchanging(word: "moose");
            Unchanging(word: "swine");
            Unchanging(word: "bison");
            Unchanging(word: "corps");
            Unchanging(word: "means");
            Unchanging(word: "series");
            Unchanging(word: "scissors");
            Unchanging(word: "species");
        }

        private void Add(string rule, string with)
        {
            _rules.Insert(0, new RulePair{ Rule = rule, Template = with});
        }

        private void Unchanging(string word)
        {
            _uncountables.Insert(0, word.ToLower());
        }

        public string Apply(string word)
        {
            if (_uncountables.Contains(word.ToLower()) || word.Length == 0)
                return word;

            foreach (var pair in _rules)
            {
                string newValue = RegExReplace(input: word, pattern: pair.Rule, template: pair.Template);
                if (newValue != word)
                    return newValue;
            }

            return word;
        }

        private string RegExReplace(string input, string pattern, string template)
        {
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            // TODO: This is broken!
            return regex.Replace(input, template);
        }
    }

    public static class StringExtensions
    {
        public static string Pluralize(this string self, int count = 2, string with = "")
        {
            if (count == 1)
                return self;

            if (with.Length == 0)
                return Infrastructure.Pluralize.SharedInstance.Apply(word: self);

            return with;
        }
    }
}
    



