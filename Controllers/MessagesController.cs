using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private static List<Message> _messages = new List<Message>();
        private static int _nextId = 1;

        // Endpoint para enviar mensagens
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest("Message content cannot be empty.");
            }

            var message = new Message
            {
                Id = _nextId++,
                Content = content,
                IsRead = false,
                Timestamp = DateTime.Now
            };

            _messages.Add(message);
            return Ok(new { success = true, message = "Success" });
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
