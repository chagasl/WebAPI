using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private static List<string> Messages = new List<string>();

        // Endpoint para enviar mensagens
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("A mensagem não pode estar vazia.");
            }

            Messages.Add(message);
            return Ok("Mensagem recebida com sucesso!");
        }

        // Endpoint para receber mensagens
        [HttpGet("get")]
        public IActionResult GetMessages()
        {
            return Ok(Messages);
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "API is running" });
        }
    }
}
