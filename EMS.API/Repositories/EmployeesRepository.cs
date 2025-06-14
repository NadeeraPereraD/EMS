using EMS.API.Data;
using EMS.API.Interfaces;
using EMS.API.Models.DTOs;
using EMS.API.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EMS.API.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly AppDbContext _context;

        public EmployeesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(EmployeesCreateDto dto)
        {
            var conn = _context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_Employees_Create";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@name", dto.name));
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@email", dto.email));
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@jobPosition", dto.jobPosition));

            var pError = new Microsoft.Data.SqlClient.SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pError);

            var pSuccess = new Microsoft.Data.SqlClient.SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pSuccess);
            await cmd.ExecuteNonQueryAsync();
            var errorMsg = pError.Value as string;
            var successMsg = pSuccess.Value as string;
            var isSuccess = string.IsNullOrEmpty(errorMsg);

            return (isSuccess, errorMsg, successMsg);
        }
        public async Task<(IEnumerable<Employee> employees, string? ErrorMessage, string? SuccessMessage)> GetAllAsync()
        {
            var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            var employee = await _context.Employees
                .FromSqlRaw("EXEC dbo.usp_Employees_GetAll @ErrorMessage OUTPUT, @SuccessMessage OUTPUT",
                             errorParam, successParam)
                .ToListAsync();

            var errorMsg = errorParam.Value as string;
            var successMsg = successParam.Value as string;

            return (employee, errorMsg, successMsg);
        }
        public async Task<(Employee? Entity, string? ErrorMessage, string? SuccessMessage)> GetByKeyAsync(string name)
        {
            Employee? entity = null;
            string? error = null,
            success = null;

            using var conn = _context.Database.GetDbConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_Employees_GetByCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@name", name));

            var pError = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var pSuccess = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pError);
            cmd.Parameters.Add(pSuccess);

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                entity = new Employee
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    name = reader.GetString(reader.GetOrdinal("name")),
                    email = reader.GetString(reader.GetOrdinal("email")),
                    jobPosition = reader.GetString(reader.GetOrdinal("jobPosition")),
                    isActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                };
            }
            reader.Close();

            error = pError.Value as string;
            success = pSuccess.Value as string;
            return (entity, error, success);
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateByKeyAsync(EmployeesCreateDto dto)
        {
            var conn = _context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_Employees_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@name", dto.name));
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@email", dto.email));
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@jobPosition", dto.jobPosition));

            var pError = new Microsoft.Data.SqlClient.SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pError);

            var pSuccess = new Microsoft.Data.SqlClient.SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pSuccess);
            await cmd.ExecuteNonQueryAsync();
            var errorMsg = pError.Value as string;
            var successMsg = pSuccess.Value as string;
            var isSuccess = string.IsNullOrEmpty(errorMsg);

            return (isSuccess, errorMsg, successMsg);
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> SoftDeleteByName(EmployeesSoftDeleteDto dto)
        {
            var conn = _context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_Employees_SoftDelete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@name", dto.name));
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@isActive", dto.isActive));

            var pError = new Microsoft.Data.SqlClient.SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pError);

            var pSuccess = new Microsoft.Data.SqlClient.SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(pSuccess);
            await cmd.ExecuteNonQueryAsync();
            var errorMsg = pError.Value as string;
            var successMsg = pSuccess.Value as string;
            var isSuccess = string.IsNullOrEmpty(errorMsg);

            return (isSuccess, errorMsg, successMsg);
        }
        public async Task<(IEnumerable<Employee> employees, string? ErrorMessage, string? SuccessMessage)> GetAllInactiveAsync()
        {
            var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            var employee = await _context.Employees
                .FromSqlRaw("EXEC dbo.usp_Employees_GetAll_Inactive @ErrorMessage OUTPUT, @SuccessMessage OUTPUT",
                             errorParam, successParam)
                .ToListAsync();

            var errorMsg = errorParam.Value as string;
            var successMsg = successParam.Value as string;

            return (employee, errorMsg, successMsg);
        }
    }
}
