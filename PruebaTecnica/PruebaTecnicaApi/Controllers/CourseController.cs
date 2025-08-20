//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Entities;
using Infraestructure.Models;
using Interfaces.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace PruebaTecnicaApi.Controllers
{
    [Route("api/Course")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CourseController : ControllerBase
    {

        private readonly ICourseServices ICourseervices;

        public CourseController(ICourseServices ICourseervices)
        {
            this.ICourseervices = ICourseervices;
        }

        /// <summary>
        /// Lista todos los curso
        /// </summary>
        [HttpGet("CourseList")]
        public async Task<ActionResult<ResultModel<Course[]>>> Course()
        {
            return await ICourseervices.CourseList();
        }


        /// <summary>
        /// Agrega nuevos cursos
        /// </summary>
        [HttpPost("CourseAdd")]
        public async Task<ActionResult<ResultModel<string>>> CourseAdd([FromBody] Course CourseModel)
        {
            return await ICourseervices.CourseAdd(CourseModel);
        }

        /// <summary>
        /// Obtiene un curso por ID.
        /// </summary>
        [HttpPost("GetCourseByCourseId")]
        public async Task<ActionResult<ResultModel<Course>>> GetCourseByCourseId([FromBody] int CourseId)
        {
            return await ICourseervices.GetCourseByCourseId(CourseId);
        }

        /// <summary>
        /// Actualiza un curso
        /// </summary>
        [HttpPut("CourseUpdt")]
        public async Task<ActionResult<ResultModel<string>>> CourseUpdt([FromBody] Course CourseModel)
        {
            return await ICourseervices.CourseUpdate(CourseModel);
        }

        /// <summary>
        /// Elimina un curso
        /// </summary>
        [HttpDelete("CourseDelete/{courseId}")]
        public async Task<ActionResult<ResultModel<string>>> CourseDelete(int courseId)
        {
            return await ICourseervices.CourseDelete(courseId);
        }
         

    }
}
