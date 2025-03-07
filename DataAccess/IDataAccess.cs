using VerbsMinimalAPI.Entities;

namespace VerbsMinimalAPI.DataAccess
{
    public interface IDataAccess
    {
		Task<Verb> GetVerbById(int id);
	}
}