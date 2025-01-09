using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexasTimsBigWinSlots.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TexasTimsBigWinSlots.Controllers
{
    [ApiController]
    [Route("thehouse/[controller]")]

    public class Cashoutcontroller : ControllerBase
    {
        #region logging
        private readonly ILogger<Cashoutcontroller> _logger;

        public Cashoutcontroller(ILogger<Cashoutcontroller> logger)
        {
            _logger = logger;
        }
        #endregion

        [HttpPost]
        public IActionResult cashout([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Player data is required");
            }
            //function called to save the player information to a save file in the applications base directory
            createSaveFile(player);

            return StatusCode(201, player);
        }

        private void createSaveFile(Player player)
        {
            //serializing the player object into a json object and defining file path
            string json = JsonSerializer.Serialize(player);
            //the specific filepath is *\TexasTimsBigWinSlots\TexasTimsBigWinSlotsBackend\bin\Debug\net8.0\SaveFiles
            string folderpath = Path.Combine(AppContext.BaseDirectory, "SaveFiles");
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            string filepath = Path.Combine(folderpath, "playerdata.json");
            _logger.LogInformation("Saving player data to file....");
            System.IO.File.WriteAllText(filepath, json);
            _logger.LogInformation("Saved to file!");
        }

    }
}