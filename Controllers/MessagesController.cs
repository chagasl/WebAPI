using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private static List<Content> _requests = new List<Content>();
        private static List<Content> _done = new List<Content>();

        [HttpPost("sendRequest")]
        public IActionResult SendRequest([FromBody] string requestMessage)
        {
            if (string.IsNullOrWhiteSpace(requestMessage))
            {
                return BadRequest("Message content cannot be empty.");
            }

            var splitMessage = Regex.Split(requestMessage, ",");

            var leftMessage = string.Join(",", splitMessage.Skip(2));

            var content = new Content
            {
                Id = Convert.ToInt32(splitMessage[0]),
                RequestCode = Convert.ToInt16(splitMessage[1]),
                Message = leftMessage
            };

            _requests.Add(content);
            return Ok(new { success = true, message = "ok" });
        }

        [HttpPost("sendDone")]
        public IActionResult SendMessage([FromBody] string doneMessage)
        {
            if (string.IsNullOrWhiteSpace(doneMessage))
            {
                return BadRequest("Message content cannot be empty.");
            }

            var splitMessage = Regex.Split(doneMessage, ",");

            var leftMessage = string.Join(",", splitMessage.Skip(2));

            var content = new Content
            {
                Id = Convert.ToInt32(splitMessage[0]),
                RequestCode = Convert.ToInt16(splitMessage[1]),
                Message = leftMessage
            };

            _done.Add(content);
            return Ok(new { success = true, message = "ok" });
        }

        [HttpGet("getRequests")]
        public IActionResult GetRequests()
        {
            var message = _requests.ToList();

            _requests.Clear();

            return Ok(message);
        }

        [HttpGet("getDone")]
        public IActionResult GetDone([FromQuery] Int32 id)
        {
            var message = _done.Where(m => m.Id == id).ToList();

            _done.RemoveAll(m => m.Id == id);

            return Ok(message);
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "API is running" });
        }


    }
}
