namespace VerbsMinimalAPI
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VerbsAPI.DataAccess;

        public static class ApiCalls
        {
            public static IApplicationBuilder MapApiCalls(this WebApplication app)
            {
                app.MapGet("/verb/{id}", GetVerbById).WithName(nameof(GetVerbById));
                app.MapGet("/register/formal/{formal}", GetSubjectPronounsByRegister).WithName(nameof(GetSubjectPronounsByRegister));

                return app;
            }

            [Authorize]
            public static async Task<IResult> GetVerbById(ILoggerFactory loggerFactory, IDataAccess dataAccess, [FromRoute] string id)
            {
                ILogger logger = loggerFactory.CreateLogger(nameof(GetVerbById));

                try
                {
                    string verb = await dataAccess.GetVerbByIdAsync(id);
                    return Results.Ok(verb);
                }
                catch (Exception ex)
                {
                    string errorMessage = "Something went wrong getting the verb.";
                    logger.LogError(ex, errorMessage);
                    return Results.BadRequest(errorMessage);
                }
            }

            [Authorize]
            public static async Task<IResult> GetSubjectPronounsByRegister
    (ILoggerFactory loggerFactory, IDataAccess dataAccess, [FromRoute] string id, [FromRoute] bool isFormal)
            {
                ILogger logger = loggerFactory.CreateLogger(nameof(GetSubjectPronounsByRegister));

                try
                {
                    string verb = await dataAccess.GetSubjectPronounsByRegister(id);
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

}
