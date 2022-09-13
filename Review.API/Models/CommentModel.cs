namespace Review.API.Models
{
    public class CommentModel:BaseModel
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
