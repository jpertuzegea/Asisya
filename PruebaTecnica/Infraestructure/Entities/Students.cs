//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Entities
{

    public class Student
    {
        [Key]
        public int? StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string IdentificationNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }
        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }


}
