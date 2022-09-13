namespace Review.API.Models
{
    public class VoteModel:BaseModel
    {
        public int Value { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
