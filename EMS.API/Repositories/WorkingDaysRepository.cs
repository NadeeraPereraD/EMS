using EMS.API.Data;
using EMS.API.Interfaces;
using EMS.API.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EMS.API.Repositories
{
    public class WorkingDaysRepository : IWorkingDaysRepository
    {
        private readonly AppDbContext _context;

        public WorkingDaysRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(int days, string? ErrorMessage, string? SuccessMessage)> GetWorkingDaysAsync(DateTime startDate, DateTime endDate)
        {
            int days = 0;
            string? error = null,
            success = null;

            using var conn = _context.Database.GetDbConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_WorkingDays_Get";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
            cmd.Parameters.Add(new SqlParameter("@endDate", endDate));

            var pError = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var pSuccess = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(pError);
            cmd.Parameters.Add(pSuccess);

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                days = reader.GetInt32(reader.GetOrdinal("days"));
            }
            reader.Close();

            error = pError.Value as string;
            success = pSuccess.Value as string;
            return (days, error, success);
        }
    }
}
