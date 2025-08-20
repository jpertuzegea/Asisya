//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Dtos;
using Infraestructure.Models;

namespace Interfaces.Interfaces
{
    public interface ILoginServices
    {

        /// <summary>
        /// Permite iniciar sesion User:jorge Password:123456789
        /// </summary>
        Task<ResultModel<LoginModel>> Login(LoginModel LoginModel);
    }
}
