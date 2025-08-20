//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Dtos;
using Infraestructure.Entities;
using Infraestructure.Interfaces;
using Infraestructure.Models;
using Interfaces.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Services.Services
{
    public class GradeServices : IGradeServices
    {

        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitofwork;


        public GradeServices(IConfiguration _configuration, IUnitOfWork _unitofwork)
        {
            configuration = _configuration;
            unitofwork = _unitofwork;
        }

        /// <inheritdoc />
        public async Task<ResultModel<Grade[]>> GradeList()
        {
            ResultModel<Grade[]> ResultModel = new ResultModel<Grade[]>();

            try
            {
                IEnumerable<Grade> List = await unitofwork.GetRepository<Grade>().Get();

                ResultModel.HasError = false;
                ResultModel.Messages = "Grade Listados Con Exito";
                ResultModel.Data = List.ToArray();

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Listando Grade";
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }


        /// <inheritdoc />
        public async Task<ResultModel<string>> GradeAdd(GradeDto GradeModel)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();
            Grade[] List = null;

            try
            {
                ResultModel<Grade[]> Result = await GradeList();
                if (!Result.HasError)
                {
                    List = Result.Data;
                }
                else
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = Result.Messages;
                    ResultModel.Data = Result.ExceptionMessage;
                    return ResultModel;
                }


                var grade = new Grade
                {
                    StudentId = (int)GradeModel.StudentId,
                    CourseId = (int)GradeModel.CourseId,
                    GradeValue = GradeModel.GradeValue,
                    GradeDate = GradeModel.GradeDate
                };

                unitofwork.GetRepository<Grade>().Add(grade);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Grade No Creado";
                    ResultModel.Data = null;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "Grade Creado Con Exito";
                ResultModel.Data = null;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Guardar Grade: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }


        /// <inheritdoc />
        public async Task<ResultModel<Grade>> GetGradeByGradeId(int Id)
        {
            ResultModel<Grade> ResultModel = new ResultModel<Grade>();

            try
            {
                var Grade = (await unitofwork.GetRepository<Grade>().Get(x => x.GradeId == Id)).FirstOrDefault();

                ResultModel.HasError = false;
                ResultModel.Messages = "Grade Encontrado Con Exito";
                ResultModel.Data = Grade;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Obteniendo Grade";
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> GradeUpdate(GradeDto GradeModel)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();

            try
            {
                ResultModel<Grade> Result = await GetGradeByGradeId((int)GradeModel.GradeId);
                Grade Grade;

                if (!Result.HasError)
                {
                    Grade = Result.Data;

                    if (Grade != null)
                    {
                       
                        // Solo permito cambiar la calificacion
                        Grade.GradeValue = GradeModel.GradeValue;

                        unitofwork.GetRepository<Grade>().Update(Grade);

                        if (!unitofwork.SaveChanges())
                        {
                            ResultModel.HasError = true;
                            ResultModel.Messages = "Grade No Modificar";
                            ResultModel.Data = null;
                            return ResultModel;
                        }

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Grade Modificar Con Exito";
                        ResultModel.Data = null;

                        return ResultModel;

                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Grade NO Encontrado";
                        ResultModel.Data = null;

                        return ResultModel;
                    }

                }
                else
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = Result.Messages;
                    ResultModel.Data = Result.ExceptionMessage;
                    return ResultModel;
                }
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Modificar Grade: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> GradeDelete(int GradeId)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();

            try
            {
                ResultModel<Grade> Result = await GetGradeByGradeId(GradeId);
                Grade Grade;

                if (!Result.HasError)
                {
                    Grade = Result.Data;

                    if (Grade != null)
                    {
                        unitofwork.GetRepository<Grade>().Remove(Grade);

                        if (!unitofwork.SaveChanges())
                        {
                            ResultModel.HasError = true;
                            ResultModel.Messages = "Grade No Eliminado";
                            ResultModel.Data = null;
                            return ResultModel;
                        }

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Grade Eliminado Con Exito";
                        ResultModel.Data = null;

                        return ResultModel;

                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Grade NO Encontrado";
                        ResultModel.Data = null;

                        return ResultModel;
                    }

                }
                else
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = Result.Messages;
                    ResultModel.Data = Result.ExceptionMessage;
                    return ResultModel;
                }
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Eliminar Grade: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }




    }
}
