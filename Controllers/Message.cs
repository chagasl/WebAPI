namespace WebAPI.Controllers
{
    public class Message
    {
        //public int Id { get; set; } // Identificador único
        public string Content { get; set; } // Conteúdo da mensagem
        public bool IsRead { get; set; } // Indica se a mensagem foi lida
        public DateTime Timestamp { get; set; } // Data/hora da mensagem
    }
}
