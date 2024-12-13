using Genealogy.DAL.Enums;
using Genealogy.DAL.Models;

namespace Genealogy.BLL.Services
{
    public interface IRelationshipHandler
    {
        void HandleRelationship(Person person1, Person person2, RelationshipType relationshipType);
    }
}