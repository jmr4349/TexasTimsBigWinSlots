using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TexasTimsBigWinSlots.Models
{
    public class Slotrandomizer
    {
        private readonly ILogger<Slotrandomizer> _logger;

        public Slotrandomizer(ILogger<Slotrandomizer> logger)
        {
            _logger = logger;
        }
        public String[] slotCall(int credits)
        {
            int creditAmount = new int();
            //randomizer function call to populate the slots
            var slotOutPut = randomizer();
            Random random = new Random();

            //check if the player is winning with this slot
            bool winning = isWinning(slotOutPut);

            if (credits > 40 && credits <= 60 && winning == true)
            {
                _logger.LogInformation("Checking for 30% re-roll chance");
                //if the player is winning the role with 40-60 credits, the 30% chance server roll call is made to see if it re-rolls.
                int roll = random.Next(100);
                if (roll < 30)
                {
                    _logger.LogInformation("The 30% re-roll conditions have been met! Re-rolling...");
                    slotOutPut = randomizer();
                }
            }
            else if (credits > 60 && winning == true)
            {
                _logger.LogInformation("Checking for 60% re-roll chance");
                //if the player is winning the role with 60 credits, the 60% chance server roll call is made to see if it re-rolls.
                int roll = random.Next(100);
                if (roll < 60)
                {
                    _logger.LogInformation("The 60% re-roll conditions have been met! Re-rolling...");
                    slotOutPut = randomizer();
                }
            }

            if (winning == true)
            {
                creditAmount = winningAmount(slotOutPut);
            }

            return slotOutPut;
        }

        private string[] randomizer()
        {
            var possSlots = new[] { "C", "L", "O", "W" };
            var output = new[] { "", "", "" };
            Random random = new Random();

            for (int i = 0; i < output.Length; i++)
            {

                output[i] = possSlots[random.Next(possSlots.Length)];
            }
            return output;
        }

        public bool isWinning(string[] slots)
        {
            int winnerCount = slots.Distinct().Count();
            if (winnerCount > 1)
            {
                _logger.LogInformation("Not a winner");
                return false;
            }
            else
            {
                _logger.LogInformation("Winner!");
                return true;
            }
        }

        public int winningAmount(string[] slots)
        {
            int amount = new int();

            foreach (string slot in slots)
            {
                //using switch expression for cleaner code and performance and its also just super cool. Its only usable in C# versions that are 8.0+. 
                amount = slot.ToUpperInvariant() switch
                {
                    "C" => 10,
                    "L" => 20,
                    "O" => 30,
                    "W" => 40,
                    _ => 0
                };
            }
            return amount;

        }
    }
}