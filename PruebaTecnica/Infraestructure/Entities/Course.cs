//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Infraestructure.Entities
{
    public class Course
    {
        [Key]
        public int? CourseId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Column(TypeName = "TEXT")]
        public string? Description { get; set; }

        // Foreign Key
        public int? TeacherId { get; set; }

        // Navigation properties
        [ForeignKey("TeacherId")]
        public virtual Teacher? Teacher { get; set; }

        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
         

    }
}
