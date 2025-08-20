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
    public interface ICourseServices
    {
        /// <summary>
        /// Agrega nuevos cursos
        /// </summary>
        Task<ResultModel<string>> CourseAdd(Course CourseModel);

        /// <summary>
        /// Lista todos los curso
        /// </summary>
        Task<ResultModel<Course[]>> CourseList();


        /// <summary>
        /// Obtiene un curso por ID.
        /// </summary>
        Task<ResultModel<Course>> GetCourseByCourseId(int Id);


        /// <summary>
        /// Actualiza un curso
        /// </summary>
        Task<ResultModel<string>> CourseUpdate(Course CourseModel);

        /// <summary>
        /// Elimina un curso
        /// </summary>
        Task<ResultModel<string>> CourseDelete(int CourseId);
    }
}
