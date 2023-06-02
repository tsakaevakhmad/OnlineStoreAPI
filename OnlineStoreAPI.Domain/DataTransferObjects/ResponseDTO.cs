namespace OnlineStoreAPI.Domain.DataTransferObjects
{
    public class ResponseDTO<DTO> where DTO: class
    {
        public ResponseDTO(DTO body) 
        {
            Body = body;
        }
        public DTO Body { get; }
        public string Message { get; set; } = "Success";
    }
}
