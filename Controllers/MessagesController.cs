using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private static List<Content> _messages = new List<Content>();

        // Endpoint para enviar mensagens
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] string messageContent, Int32 id)
        {
            if (string.IsNullOrWhiteSpace(messageContent))
            {
                return BadRequest("Message content cannot be empty.");
            }

            var content = new Content
            {
                Id = id,
                Message = messageContent
            };

            _messages.Add(content);
            return Ok(new { success = true, message = "ok" });
        }

        // Endpoint para receber mensagens
        [HttpGet("get")]
        public IActionResult Get([FromQuery] Int32 id)
        {
            var message = _messages.Where(m => m.Id == id);

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
