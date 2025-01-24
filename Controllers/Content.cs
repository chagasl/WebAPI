namespace WebAPI.Controllers
{
    public class Content
    {
        //public int Id { get; set; } // Identificador único
        public required string Message { get; set; } // Conteúdo da mensagem
        public bool IsRead { get; set; } // Indica se a mensagem foi lida
        public DateTime Timestamp { get; set; } // Data/hora da mensagem
    }
}
