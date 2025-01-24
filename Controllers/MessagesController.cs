using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private static List<Content> _messages = new List<Content>();
        //private static int _nextId = 1;

        // Endpoint para enviar mensagens
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] string messageContent)
        {
            if (string.IsNullOrWhiteSpace(messageContent))
            {
                return BadRequest("Message content cannot be empty.");
            }

            var content = new Content
            {
                //Id = _nextId++,
                Message = messageContent,
                IsRead = false,
                Timestamp = DateTime.Now
            };

            _messages.Add(content);
            return Ok(new { success = true, message = "ok" });
        }

        // Endpoint para receber mensagens
        [HttpGet("get")]
        public IActionResult GetMessages([FromQuery] int limit = 10)
        {
            var unreadMessages = _messages
                .Where(m => !m.IsRead)
                .OrderBy(m => m.Timestamp)
                .Take(limit)
                .ToList();

            // Marcar as mensagens como lidas
            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
            }

            return Ok(unreadMessages);
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "API is running" });
        }
    }
}
