//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Dtos
{
    public class GradeDto
    {
        public int? GradeId { get; set; }
        public int? StudentId { get; set; }
        
        public int? CourseId { get; set; }

        [Range(0, 10)]
        public decimal GradeValue { get; set; }

        public DateTime? GradeDate { get; set; }
    }

}
