//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Entities;
using Infraestructure.Interfaces;
using Infraestructure.Models;
using Interfaces.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Services.Services
{
    public class TeacherServices : ITeacherServices
    {

        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitofwork;


        public TeacherServices(IConfiguration _configuration, IUnitOfWork _unitofwork)
        {
            configuration = _configuration;
            unitofwork = _unitofwork;
        }

        /// <inheritdoc />
        public async Task<ResultModel<Teacher[]>> TeacherList()
        {
            ResultModel<Teacher[]> ResultModel = new ResultModel<Teacher[]>();

            try
            {
                IEnumerable<Teacher> List = await unitofwork.GetRepository<Teacher>().Get();

                ResultModel.HasError = false;
                ResultModel.Messages = "Teacher Listados Con Exito";
                ResultModel.Data = List.ToArray();

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Listando Teacher";
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> TeacherAdd(Teacher TeacherModel)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();
            Teacher[] List = null;

            try
            {
                ResultModel<Teacher[]> Result = await TeacherList();
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

                Teacher Teacher = List.FirstOrDefault(x => x.Email == TeacherModel.Email);

                if (Teacher != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Identificacion ya Existe";
                    ResultModel.Data = null;
                    return ResultModel;
                }

                unitofwork.GetRepository<Teacher>().Add(TeacherModel);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Teacher No Creado";
                    ResultModel.Data = null;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "Teacher Creado Con Exito";
                ResultModel.Data = null;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Guardar Teacher: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<Teacher>> GetTeacherByTeacherId(int Id)
        {
            ResultModel<Teacher> ResultModel = new ResultModel<Teacher>();

            try
            {
                var Teacher = (await unitofwork.GetRepository<Teacher>().Get(x => x.TeacherId == Id)).FirstOrDefault();

                ResultModel.HasError = false;
                ResultModel.Messages = "Teacher Encontrado Con Exito";
                ResultModel.Data = Teacher;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Obteniendo Teacher";
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> TeacherUpdate(Teacher TeacherModel)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();

            try
            {
                ResultModel<Teacher> Result = await GetTeacherByTeacherId((int)TeacherModel.TeacherId);
                Teacher Teacher;

                if (!Result.HasError)
                {
                    Teacher = Result.Data;

                    if (Teacher != null)
                    {
                        Teacher.FirstName = TeacherModel.FirstName;
                        Teacher.LastName = TeacherModel.LastName;
                        Teacher.Specialty = TeacherModel.Specialty;
                        Teacher.Email = TeacherModel.Email;

                        unitofwork.GetRepository<Teacher>().Update(Teacher);

                        if (!unitofwork.SaveChanges())
                        {
                            ResultModel.HasError = true;
                            ResultModel.Messages = "Teacher No Modificar";
                            ResultModel.Data = null;
                            return ResultModel;
                        }

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Teacher Modificar Con Exito";
                        ResultModel.Data = null;

                        return ResultModel;

                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Teacher NO Encontrado";
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
                ResultModel.Messages = $"Error Técnico Al Modificar Teacher: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> TeacherDelete(int TeacherId)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();

            try
            {
                ResultModel<Teacher> Result = await GetTeacherByTeacherId(TeacherId);
                Teacher Teacher;

                if (!Result.HasError)
                {
                    Teacher = Result.Data;

                    if (Teacher != null)
                    {
                        unitofwork.GetRepository<Teacher>().Remove(Teacher);

                        if (!unitofwork.SaveChanges())
                        {
                            ResultModel.HasError = true;
                            ResultModel.Messages = "Teacher No Eliminado";
                            ResultModel.Data = null;
                            return ResultModel;
                        }

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Teacher Eliminado Con Exito";
                        ResultModel.Data = null;

                        return ResultModel;

                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Teacher NO Encontrado";
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
                ResultModel.Messages = $"Error Técnico Al Eliminar Teacher: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }




    }
}
