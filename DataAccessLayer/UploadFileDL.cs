using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
//using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using ReactWeb.CommonLayer.Model;
using ReactWeb.DataAccessLayer;
using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ReactWeb.DataAccessLayer
{
    public class UploadFileDL : IUploadFileDL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;

        public UploadFileDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration["ConnectionStrings:CRUDCS"]);
            //_sqlConnection = new SqlConnection(_configuration["ConnectionStrings:SqlServerDBConnection"]);
        }

              public async Task<UploadExcelFileResponse> UploadExcelFile(UploadExcelFileRequest request, string path)
        {
            UploadExcelFileResponse response = new UploadExcelFileResponse();
            List<ExcelBulkUploadParameter> Parameters = new List<ExcelBulkUploadParameter>();
            DataSet dataSet;
            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                if (request.File.FileName.ToLower().Contains(value:".xlsx"))
                {
                    FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                    dataSet = reader.AsDataSet(
                        new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = false,
                            ConfigureDataTable = ( IExcelDataReader tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }

                        });
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ExcelBulkUploadParameter rows = new ExcelBulkUploadParameter();
                        rows.StudentName = dataSet.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[0]) : "-1";
                        rows.StudentRoll = dataSet.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]) : "-1";
                        rows.PhoneNumber = dataSet.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]) : "-1";
                        rows.StudentEmail = dataSet.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]) : "-1";
                        rows.StudentAddress = dataSet.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]) : "-1";

                        //rows.UserName = dataSet.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[0]) : "-1";
                        //rows.EmailID = dataSet.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]) : "-1";
                        //rows.MobileNumber = dataSet.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]) : "-1";
                        //rows.Age = dataSet.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[3]) : -1;
                        //rows.Salary = dataSet.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[4]) : -1;
                        //rows.Gender = dataSet.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[5]) : "-1";
                        Parameters.Add(rows);
                    }



                    //for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    //{
                    //    ExcelBulkUploadParameter rows = new ExcelBulkUploadParameter();
                    //    rows.StudentID = dataSet.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]) : -1;
                    //    rows.StudentName = dataSet.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]) : "-1";
                    //    rows.StudentDeptID = dataSet.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]) : "-1";
                    //    rows.StudentSemesterID = dataSet.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]) : "-1";
                    //    rows.StudentShiftID = dataSet.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]) : "-1";
                    //    rows.amount = dataSet.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToSingle(dataSet.Tables[0].Rows[i].ItemArray[5]) : -1;
                    //    Parameters.Add(rows);
                    //}

                    stream.Close();

                    if (Parameters.Count > 0)
                    {
                        if (ConnectionState.Open != _sqlConnection.State)
                        {
                            await _sqlConnection.OpenAsync();
                        }
                        //string SqlQuery = "INSERT INTO PaymentRequest (StudentID, StudentName, StudentDeptID, StudentSemesterID, StudentShiftID, amount) VALUES (@StudentID, @StudentName, @StudentDeptID, @StudentSemesterID, @StudentShiftID, @amount)";
                        //string SqlQuery = "INSERT INTO BulkUploadTable (UserName,EmailID,MobileNumber,Age,Salary,Gender) VALUES (@UserName, @EmailID, @MobileNumber, @Age, @Salary, @Gender)";

                        string SqlQuery = "INSERT INTO Students (StudentName, StudentRoll, PhoneNumber, StudentEmail, StudentAddress) VALUES (@StudentName, @StudentRoll, @PhoneNumber, @StudentEmail, @StudentAddress)";
                        foreach (ExcelBulkUploadParameter rows in Parameters)
                        {
                            using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                            {
                                //UserName, StudentSemesterID, StudentSemesterID, amount, StudentID, StudentShiftID
                                //sqlCommand.CommandText = SqlQueries.InsertBulkUploadData;
                                //sqlCommand.CommandText = SqlQuery;

                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.CommandTimeout = 180;

                                //sqlCommand.Parameters.AddWithValue("@UserName", rows.UserName);
                                //sqlCommand.Parameters.AddWithValue("@EmailID", rows.EmailID);
                                //sqlCommand.Parameters.AddWithValue("@MobileNumber", rows.MobileNumber);
                                //sqlCommand.Parameters.AddWithValue("@Age", rows.Age);
                                //sqlCommand.Parameters.AddWithValue("@Salary", rows.Salary);
                                //sqlCommand.Parameters.AddWithValue("@Gender", rows.Gender);

                                sqlCommand.Parameters.AddWithValue("@StudentName", rows.StudentName);
                                sqlCommand.Parameters.AddWithValue("@StudentRoll", rows.StudentRoll);
                                sqlCommand.Parameters.AddWithValue("@PhoneNumber", rows.PhoneNumber);
                                sqlCommand.Parameters.AddWithValue("@StudentEmail", rows.StudentEmail);
                                sqlCommand.Parameters.AddWithValue("@StudentAddress", rows.StudentAddress);

                                //sqlCommand.Parameters.AddWithValue("@StudentID", rows.StudentID);
                                //sqlCommand.Parameters.AddWithValue("@StudentName", rows.StudentName);
                                //sqlCommand.Parameters.AddWithValue("@StudentDeptID", rows.StudentDeptID);
                                //sqlCommand.Parameters.AddWithValue("@StudentSemesterID", rows.StudentSemesterID);
                                //sqlCommand.Parameters.AddWithValue("@StudentShiftID", rows.StudentShiftID);
                                //sqlCommand.Parameters.AddWithValue("@amount", rows.amount);
                                int Status = await sqlCommand.ExecuteNonQueryAsync();
                                if (Status <= 0)
                                {
                                    response.IsSuccess = false;
                                    response.Message = "Query Not Executed";
                                    return response;
                                }
                            }
                        }
                    }

                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid File";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }

    }
}
