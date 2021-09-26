using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrisonerZero.Handlers
{
    internal class RandomCommand
    {
        public static readonly IEnumerable<string> Commands = new string[] { "random", "rnd", "r"};

        readonly static Random Rnd = new Random();

        internal static Task<string> GetRandom(string payload)
        {
            if (string.IsNullOrWhiteSpace(payload))
            {
                payload = "Да|Нет";
            }
            if (int.TryParse(payload, out var random))
            {
                return Task.FromResult($"Случайное число до {random}: {1+Rnd.Next(random)}");
            }
            var m = Regex.Match(payload, "^\\s*(?'one':-?\\d+)\\s*..\\s*(?'two'-?\\d+)\\s*$");
            if (m.Success)
            {
                var one = int.Parse(m.Groups["one"].Value);
                var two = int.Parse(m.Groups["two"].Value);
                var min = Math.Min(one, two);
                var max = Math.Max(one, two);
                return Task.FromResult($"Случаное число от {min} до {max}: {Rnd.Next(min, max)}");
            }
            var all = Regex.Split(payload, "[|,]");
            return Task.FromResult($"Случайный выбор: {all[Rnd.Next(all.Length)].Trim()}");
        }
    }
}
