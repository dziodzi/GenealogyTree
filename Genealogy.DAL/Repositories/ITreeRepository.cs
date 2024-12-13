using Genealogy.DAL.Models;

namespace Genealogy.DAL.Repositories
{
    public interface ITreeRepository
    {
        List<Person> People { get; }
        void AddPerson(Person person);
        Person? GetPersonById(Guid id);
        void SaveData();
        void LoadData();
        void ClearTree();
    }
}