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
    [Route("api/Student")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentController : ControllerBase
    {

        private readonly IStudentServices IStudentervices;

        public StudentController(IStudentServices IStudentervices)
        {
            this.IStudentervices = IStudentervices;
        }


        /// <summary>
        /// Lista estudiantes
        /// </summary>
        [HttpGet("StudentList")]
        public async Task<ActionResult<ResultModel<Student[]>>> Student()
        {
            return await IStudentervices.StudentList();
        }

        /// <summary>
        /// AAgrega nuevos estudiantes
        /// </summary>
        [HttpPost("StudentAdd")]
        public async Task<ActionResult<ResultModel<string>>> StudentAdd([FromBody] Student StudentModel)
        {
            return await IStudentervices.StudentAdd(StudentModel);
        }

        /// <summary>
        /// Obtien estudiantes por id 
        /// </summary>
        [HttpPost("GetStudentByStudentId")]
        public async Task<ActionResult<ResultModel<Student>>> GetStudentByStudentId([FromBody] int StudentId)
        {
            return await IStudentervices.GetStudentByStudentId(StudentId);
        }

        /// <summary>
        /// Actualiza estudiantes
        /// </summary>
        [HttpPut("StudentUpdt")]
        public async Task<ActionResult<ResultModel<string>>> StudentUpdt([FromBody] Student StudentModel)
        {
            return await IStudentervices.StudentUpdate(StudentModel);
        }

        /// <summary>
        /// Elimina estudiantes
        /// </summary>
        [HttpDelete("StudentDelete/{studentId}")]
        public async Task<ActionResult<ResultModel<string>>> StudentDelete(int StudentId)
        {
            return await IStudentervices.StudentDelete(StudentId);
        }
         
    }
}
