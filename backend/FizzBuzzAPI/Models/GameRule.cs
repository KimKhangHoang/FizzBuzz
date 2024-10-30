using System.ComponentModel.DataAnnotations;

namespace FizzBuzzAPI.Models
{
    public class GameRule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int Range { get; set; }

        public List<Rule> Rules { get; set; } = new List<Rule>();

        // Parameterless constructor
        public GameRule() {
            Name = "";
            Author = "";
        }

        public GameRule(string name, string author, int range) : this()
        {
            Name = name;
            Author = author;
            Range = range;
        }

        public void AddRule(int divisor, string replacement)
        {
            Rules.Add(new Rule(divisor, replacement));
        }

        public string GetResult(int number)
        {
            if (number > Range)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Number is out of range.");
            }

            // Loop through each defined rule
            List<string> result = new List<string>();
            foreach (var rule in Rules)
            {
                // Check if the number is divisible by any of the rules
                if (number % rule.Divisor == 0)
                {
                    result.Add(rule.Replacement);
                }
            }

            // Return the number replacements or the number itself
            return result.Count > 0 ? string.Join("", result) : number.ToString();
        }
    }

    // Helper class to represent individual rules
    public class Rule
    {
        public int Id { get; set; }
        public int Divisor { get; set; }
        public string Replacement { get; set; }

        // Foreign key for the GameRule it belongs to
        public int GameRuleId { get; set; }

        // Parameterless constructor
        public Rule() {
            Replacement = "";
        }

        public Rule(int divisor, string replacement)
        {
            Divisor = divisor;
            Replacement = replacement;
        }
    }
}
