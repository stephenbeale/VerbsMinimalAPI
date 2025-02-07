using VerbsMinimalAPI.Entities;

namespace VerbsMinimalAPI.DataAccess
{
    public interface IDataAccess
    {
        Task<Verb> GetVerb(int id);
    }
}