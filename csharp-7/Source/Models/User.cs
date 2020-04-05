using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Codenation.Challenge.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("full_name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Column("nickname")]
        [StringLength(50)]
        public string Nickname { get; set; }

        [Required]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [Timestamp]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
