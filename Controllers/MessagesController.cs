using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private static List<Content> _messages = new List<Content>();

        // Endpoint para enviar mensagens
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] string messageContent)
        {
            if (string.IsNullOrWhiteSpace(messageContent))
            {
                return BadRequest("Message content cannot be empty.");
            }

            var splitMessage = Regex.Split(messageContent, ",");

            var leftMessage = string.Join(", ", splitMessage.Skip(2));

            var content = new Content
            {
                Id = Convert.ToInt32(splitMessage[0]),
                RequestCode = Convert.ToInt16(splitMessage[1]),
                Message = leftMessage
            };

            _messages.Add(content);
            return Ok(new { success = true, message = "ok" });
        }

        // Endpoint para receber mensagens
        [HttpGet("get")]
        public IActionResult Get([FromQuery] Int32 id)
        {
            var message = _messages.Where(m => m.Id == id).ToList();

            _messages.RemoveAll(m => m.Id == id);

            return Ok(message);
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "API is running" });
        }


    }
}
