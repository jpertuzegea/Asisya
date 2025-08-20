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

namespace PruebaTecnicaApi.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherServices ITeacherervices;

        public TeacherController(ITeacherServices ITeacherervices)
        {
            this.ITeacherervices = ITeacherervices;
        }

        /// <summary>
        /// Lista estudiantes
        /// </summary>
        [HttpGet("TeacherList")]
        public async Task<ActionResult<ResultModel<Teacher[]>>> Teacher()
        {
            return await ITeacherervices.TeacherList();
        }

        /// <summary>
        /// Agrega estudiantes
        /// </summary>
        [HttpPost("TeacherAdd")]
        public async Task<ActionResult<ResultModel<string>>> TeacherAdd([FromBody] Teacher TeacherModel)
        {
            return await ITeacherervices.TeacherAdd(TeacherModel);
        }

        /// <summary>
        /// Obtiene estudiantes por id 
        /// </summary>
        [HttpPost("GetTeacherByTeacherId")]
        public async Task<ActionResult<ResultModel<Teacher>>> GetTeacherByTeacherId([FromBody] int TeacherId)
        {
            return await ITeacherervices.GetTeacherByTeacherId(TeacherId);
        }


        /// <summary>
        /// Actualiza estudiantes
        /// </summary>

        [HttpPut("TeacherUpdt")]
        public async Task<ActionResult<ResultModel<string>>> TeacherUpdt([FromBody] Teacher TeacherModel)
        {
            return await ITeacherervices.TeacherUpdate(TeacherModel);
        }


        /// <summary>
        /// Elimina estudiantes
        /// </summary>
        [HttpDelete("TeacherDelete/{teacherId}")]
        public async Task<ActionResult<ResultModel<string>>> TeacherDelete(int teacherId)
        {
            return await ITeacherervices.TeacherDelete(teacherId);
        }

    }
}
