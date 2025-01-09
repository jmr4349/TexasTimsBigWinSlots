using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexasTimsBigWinSlots.Models;
using Microsoft.Extensions.Logging;

namespace TexasTimsBigWinSlots.Controllers
{
    [ApiController]
    [Route("thehouse/[controller]")]

    public class SlotController : ControllerBase
    {
        //adding logging capabilities
        #region logging
        private readonly ILogger<SlotController> _logger;

        public SlotController(ILogger<SlotController> logger)
        {
            _logger = logger;
        }
        #endregion

        // GET: thehouse/slot/{credits}
        [HttpGet("{credits}")]
        public IActionResult getSlots(int credits)
        {
            int creditAmount = 0;

            //added logging capabilities for testing
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<Slotrandomizer>();

            //declaring randomizer custom class and calling it for value of slots
            Slotrandomizer randomizer = new Slotrandomizer(logger);
            string[] slots = randomizer.slotCall(credits);

            bool isWinning = randomizer.isWinning(slots);
            if (isWinning == true)
            {
                creditAmount = randomizer.winningAmount(slots);
            }
            //slot custom class for response to front end
            var response = new ApiResponse()
            {
                slotData = slots,
                credits = creditAmount,
                winning = isWinning
            };

            _logger.LogInformation("Sending slot information to client...");
            return Ok(response);
        }

    }
}