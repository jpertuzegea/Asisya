//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Entities
{
    public class Teacher
    {

        [Key]
        public int? TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Specialty { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();


    }
}
