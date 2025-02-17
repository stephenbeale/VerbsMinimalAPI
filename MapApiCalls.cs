namespace VerbsMinimalAPI
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
	using VerbsMinimalAPI.DataAccess;

	public static class ApiCalls
        {
            public static IApplicationBuilder MapApiCalls(this WebApplication app)
            {
                app.MapGet("/verb/{id}", GetVerb).WithName(nameof(GetVerb));
                return app;
            }

            [Authorize]
            public static async Task<IResult> GetVerb(ILoggerFactory loggerFactory, IDataAccess dataAccess, [FromRoute] string id)
            {
                ILogger logger = loggerFactory.CreateLogger(nameof(GetVerb));

                try
                {
                    var verb = await dataAccess.GetVerb(id);
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
