using Genealogy.DAL.Models;

namespace Genealogy.BLL.Services
{
    public interface ITreeManager
    {
        void AddPerson(string fullName, string birthDateString, string genderString);
        void SetRelationship(string person1IdString, string person2IdString, string relationshipString);
        List<(Person person, string role)> GetRelatives(Guid personId);
        List<Person> FindCommonAncestors(Guid person1Id, Guid person2Id);
        int CalculateAgeAtBirth(Guid parentId, Guid childId);
        string GetTreeRepresentation();
        void ClearTree();
    }
}