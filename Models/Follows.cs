using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoX.Models
{
    [Table("follows")]
    public class Follow
    {
        public int Id { get; set; }

        [Column("follower_id")]
        public int FollowerId { get; set; }

        [Column("followed_id")]
        public int FollowedId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("FollowerId")]
        public Usuario? Follower { get; set; }

        [ForeignKey("FollowedId")]
        public Usuario? Followed { get; set; }
    }
}