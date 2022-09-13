
namespace Review.Votes.Entities
{
    public class Vote:BaseEntity
    {
        public int Value { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}