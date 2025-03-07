using Dapper;
using System.Data;
using System.Data.SqlClient;
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

		public async Task<Verb> GetVerbById(int id)
		{
			var connectionString = _configuration.GetConnectionString("Default");

			using (IDbConnection dbConnection = new SqlConnection(connectionString))
			{
				string query = @"
                    SELECT ID, Verb AS Name
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
