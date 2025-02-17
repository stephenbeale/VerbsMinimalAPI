using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using VerbsMinimalAPI.Entities;

namespace VerbsMinimalAPI.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _configuration;

        public DataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Verb> GetVerb(string id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                string query = @"
                    SELECT Verb
                    FROM dbo.Verbs
                    WHERE ID = @id
                ";

                try
                {
                    var result = await dbConnection.QueryAsync<Verb>(query, new { id });
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    throw;
                }
            }
        }
	}
}
