namespace Review.Comments.Entities
{
    public class Comment:BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
    }
}
