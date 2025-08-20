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
    public interface IStudentServices
    {

        /// <summary>
        /// AAgrega nuevos estudiantes
        /// </summary>
        Task<ResultModel<string>> StudentAdd(Student StudentModel);


        /// <summary>
        /// Lista estudiantes
        /// </summary>
        Task<ResultModel<Student[]>> StudentList();

        /// <summary>
        /// Obtien estudiantes por id 
        /// </summary>
        Task<ResultModel<Student>> GetStudentByStudentId(int Id);

        /// <summary>
        /// Actualiza estudiantes
        /// </summary>
        Task<ResultModel<string>> StudentUpdate(Student StudentModel);

        /// <summary>
        /// Elimina estudiantes
        /// </summary>
        Task<ResultModel<string>> StudentDelete(int StudentId);


    }
}
