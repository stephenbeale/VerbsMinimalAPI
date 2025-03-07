namespace VerbsMinimalAPI
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
	using VerbsMinimalAPI.DataAccess;

	public static class ApiCalls
        {
            public static IApplicationBuilder MapApiCalls(this WebApplication app)
            {
                app.MapGet("/verb/{id}", GetVerbById).WithName(nameof(GetVerbById));
                return app;
            }

            //[Authorize]
            public static async Task<IResult> GetVerbById(ILoggerFactory loggerFactory, IDataAccess dataAccess, [FromRoute] int id)
            {
                ILogger logger = loggerFactory.CreateLogger(nameof(GetVerbById));

                try
                {
                    var verb = await dataAccess.GetVerbById(id);
                    return Results.Ok(verb);
                }
                catch (Exception ex)
                {
                    string errorMessage = "Something went wrong getting the verb.";
                    logger.LogError(ex, errorMessage);
                    return Results.BadRequest(errorMessage);
                }
            }            
        }
    }
