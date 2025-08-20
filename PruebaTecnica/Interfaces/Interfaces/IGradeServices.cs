//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Dtos;
using Infraestructure.Entities;
using Infraestructure.Models;

namespace Interfaces.Interfaces
{
    public interface IGradeServices
    {
        Task<ResultModel<string>> GradeAdd(GradeDto GradeModel);

        Task<ResultModel<Grade[]>> GradeList();

        Task<ResultModel<Grade>> GetGradeByGradeId(int Id);

        Task<ResultModel<string>> GradeUpdate(GradeDto GradeModel);

        Task<ResultModel<string>> GradeDelete(int GradeId);


    }
}
