using Genealogy.DAL.Models;
using System.Text.Json;

namespace Genealogy.DAL.Repositories
{
    public class TreeRepository : ITreeRepository
    {
        private readonly string _filePath;
        public List<Person> People { get; private set; } = new();

        public TreeRepository(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        public void SaveData()
        {
            File.WriteAllText(_filePath, JsonSerializer.Serialize(People));
        }

        public void LoadData()
        {
            if (File.Exists(_filePath))
                People = JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(_filePath)) ?? new();
        }

        public void AddPerson(Person person)
        {
            People.Add(person);
            SaveData();
        }

        public Person? GetPersonById(Guid id)
        {
            return People.FirstOrDefault(p => p.Id == id);
        }

        public void ClearTree()
        {
            People.Clear();
            SaveData();
        }
    }
}