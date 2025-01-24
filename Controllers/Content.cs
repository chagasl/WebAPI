namespace WebAPI.Controllers
{
    public class Content
    {
        public Int32 Id { get; set; } 
        public int RequestCode { get; set; }
        public required string Message { get; set; } 
    }
}
