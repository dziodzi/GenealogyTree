using Genealogy.BLL.Exceptions;
using Genealogy.DAL.Enums;
using Genealogy.DAL.Models;
using Genealogy.DAL.Repositories;

namespace Genealogy.BLL.Services
{
    public class RelationshipHandler(ITreeRepository repository) : IRelationshipHandler
    {
        public void HandleRelationship(Person person1, Person person2, RelationshipType relationshipType)
    {
        if (person1.Id == person2.Id)
            throw new SelfRelationshipException("A person cannot have a relationship with themselves.");
        
        RemoveExistingRelationships(person1, person2);

        switch (relationshipType)
        {
            case RelationshipType.ParentChild:
                person1.ChildIds.Add(person2.Id);
                person2.ParentIds.Add(person1.Id);
                Console.WriteLine($"{person1.FullName} is now the parent of {person2.FullName}.");
                break;

            case RelationshipType.Spouse:
                if (person1.Gender == person2.Gender)
                    throw new InvalidRelationshipException("Cannot set spouse relationship between persons of the same gender.");

                person1.SpouseId = person2.Id;
                person2.SpouseId = person1.Id;

                var role1 = person1.Gender == Gender.Male ? "Husband" : "Wife";
                var role2 = person2.Gender == Gender.Male ? "Husband" : "Wife";
                Console.WriteLine($"{person1.FullName} ({role1}) and {person2.FullName} ({role2}) are now spouses.");
                break;

            default:
                throw new InvalidRelationshipTypeException("Invalid relationship type.");
        }

        repository.SaveData();
    }

    private static void RemoveExistingRelationships(Person person1, Person person2)
    {
        if (person1.SpouseId == person2.Id)
        {
            person1.SpouseId = null;
            person2.SpouseId = null;
            Console.WriteLine($"Removed spouse relationship between {person1.FullName} and {person2.FullName}.");
        }

        if (person1.ChildIds.Contains(person2.Id))
        {
            person1.ChildIds.Remove(person2.Id);
            person2.ParentIds.Remove(person1.Id);
            Console.WriteLine($"Removed parent-child relationship between {person1.FullName} and {person2.FullName}.");
        }

        if (!person2.ChildIds.Contains(person1.Id)) return;
        person2.ChildIds.Remove(person1.Id);
        person1.ParentIds.Remove(person2.Id);
        Console.WriteLine($"Removed child-parent relationship between {person1.FullName} and {person2.FullName}.");
    }
}

}