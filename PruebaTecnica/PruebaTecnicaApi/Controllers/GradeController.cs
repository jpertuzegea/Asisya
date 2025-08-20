//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Dtos;
using Infraestructure.Entities;
using Infraestructure.Models;
using Interfaces.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace PruebaTecnicaApi.Controllers
{
    [Route("api/Grade")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GradeController : ControllerBase
    {

        private readonly IGradeServices IGradeervices;

        public GradeController(IGradeServices IGradeervices)
        {
            this.IGradeervices = IGradeervices;
        }

        /// <summary>
        /// Lista todos los cursos
        /// </summary>
        [HttpGet("GradeList")]
        public async Task<ActionResult<ResultModel<Grade[]>>> Grade()
        {
            return await IGradeervices.GradeList();
        }


        /// <summary>
        /// Agrega nuevos cursos
        /// </summary>
        [HttpPost("GradeAdd")]
        public async Task<ActionResult<ResultModel<string>>> GradeAdd([FromBody] GradeDto GradeModel)
        {
            return await IGradeervices.GradeAdd(GradeModel);
        }

        /// <summary>
        /// Obtiene un curso por ID.
        /// </summary>
        [HttpPost("GetGradeByGradeId")]
        public async Task<ActionResult<ResultModel<Grade>>> GetGradeByGradeId([FromBody] int GradeId)
        {
            return await IGradeervices.GetGradeByGradeId(GradeId);
        }

        /// <summary>
        /// Actualiza un curso
        /// </summary>
        [HttpPut("GradeUpdt")]
        public async Task<ActionResult<ResultModel<string>>> GradeUpdt([FromBody] GradeDto GradeModel)
        {
            return await IGradeervices.GradeUpdate(GradeModel);
        }

        /// <summary>
        /// Elimina un curso
        /// </summary>
        [HttpDelete("GradeDelete/{gradeId}")]
        public async Task<ActionResult<ResultModel<string>>> GradeDelete( int gradeId)
        {
            return await IGradeervices.GradeDelete(gradeId);
        }
         
    }
}
