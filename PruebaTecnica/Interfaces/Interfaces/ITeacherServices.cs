//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Entities;
using Infraestructure.Models;

namespace Interfaces.Interfaces
{
    public interface ITeacherServices
    {
        /// <summary>
        /// Agrega estudiantes
        /// </summary>
        Task<ResultModel<string>> TeacherAdd(Teacher TeacherModel);

        /// <summary>
        /// Lista estudiantes
        /// </summary>
        Task<ResultModel<Teacher[]>> TeacherList();

        /// <summary>
        /// Obtiene estudiantes por id 
        /// </summary>
        Task<ResultModel<Teacher>> GetTeacherByTeacherId(int Id);

        /// <summary>
        /// Actualiza estudiantes
        /// </summary>
        Task<ResultModel<string>> TeacherUpdate(Teacher TeacherModel);

        /// <summary>
        /// Elimina estudiantes
        /// </summary>
        Task<ResultModel<string>> TeacherDelete(int TeacherId);


    }
}
