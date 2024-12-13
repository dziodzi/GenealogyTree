using Genealogy.BLL.Exceptions;
using Genealogy.DAL.Enums;
using Genealogy.DAL.Models;
using Genealogy.DAL.Repositories;

namespace Genealogy.BLL.Services
{
    public class TreeManager(ITreeRepository repository) : ITreeManager
    {
        private readonly IValidationService _validationService = new ValidationService();
        private readonly IRelationshipHandler _relationshipHandler = new RelationshipHandler(repository);

        public void AddPerson(string fullName, string birthDateString, string genderString)
        {
            _validationService.ValidateGender(genderString);
            _validationService.ValidateDate(birthDateString);

            var gender = Enum.Parse<Gender>(genderString, true);
            var birthDate = DateTime.Parse(birthDateString);

            var person = new Person { FullName = fullName, BirthDate = birthDate, Gender = gender };
            repository.AddPerson(person);

            Console.WriteLine($"Added person: {person.FullName} (ID: {person.Id})");
        }

        public void SetRelationship(string person1IdString, string person2IdString, string relationshipString)
        {
            _validationService.ValidateUuid(person1IdString);
            _validationService.ValidateUuid(person2IdString);
            _validationService.ValidateRelationshipType(relationshipString);

            var person1Id = Guid.Parse(person1IdString);
            var person2Id = Guid.Parse(person2IdString);
            var relationshipType = Enum.Parse<RelationshipType>(relationshipString, true);

            var person1 = repository.GetPersonById(person1Id) ?? throw new PersonNotFoundException("First person not found.");
            var person2 = repository.GetPersonById(person2Id) ?? throw new PersonNotFoundException("Second person not found.");

            _relationshipHandler.HandleRelationship(person1, person2, relationshipType);
        }

        public List<(Person person, string role)> GetRelatives(Guid personId)
        {
            var person = repository.GetPersonById(personId) ?? throw new PersonNotFoundException("Person not found.");

            var relatives = person.ParentIds
                .Select(repository.GetPersonById)
                .OfType<Person>()
                .Select(parent => (parent, parent.Gender.ToRole(isParent: true)))
                .ToList();

            relatives.AddRange(person.ChildIds
                .Select(repository.GetPersonById)
                .OfType<Person>()
                .Select(child => (child, child.Gender.ToRole(isParent: false))));

            if (person.SpouseId.HasValue)
            {
                var spouse = repository.GetPersonById(person.SpouseId.Value);
                if (spouse != null)
                    relatives.Add((spouse, "Spouse"));
            }

            return relatives;
        }

        public List<Person> FindCommonAncestors(Guid person1Id, Guid person2Id)
        {
            var person1 = repository.GetPersonById(person1Id);
            var person2 = repository.GetPersonById(person2Id);

            if (person1 == null || person2 == null)
                throw new PersonNotFoundException("One or both persons not found.");

            var ancestors1 = GetAllAncestors(person1);
            var ancestors2 = GetAllAncestors(person2);

            return ancestors1.Intersect(ancestors2).ToList();
        }

        private List<Person> GetAllAncestors(Person person)
        {
            var ancestors = new List<Person>();
            foreach (var parent in person.ParentIds.Select(repository.GetPersonById).OfType<Person>())
            {
                ancestors.Add(parent);
                ancestors.AddRange(GetAllAncestors(parent));
            }
            return ancestors;
        }

        public int CalculateAgeAtBirth(Guid parentId, Guid childId)
        {
            var parent = repository.GetPersonById(parentId);
            var child = repository.GetPersonById(childId);

            if (parent == null || child == null)
                throw new PersonNotFoundException("One or both persons not found.");

            return (child.BirthDate - parent.BirthDate).Days / 365;
        }

        public string GetTreeRepresentation()
        {
            var roots = repository.People.Where(p => p.ParentIds.Count == 0).ToList();
            return string.Join("\n", roots.Select((r, i) => BuildTree(r.Id, "", i == roots.Count - 1)));

            string BuildTree(Guid personId, string prefix = "", bool isLast = true)
            {
                var person = repository.GetPersonById(personId);
                if (person == null) return "";

                var connector = isLast ? "└── " : "├── ";
                var result = $"{prefix}{connector}{person.FullName} ({person.BirthDate.ToShortDateString()})\n";

                var children = person.ChildIds.Select(repository.GetPersonById).Where(p => p != null).ToList();

                for (var i = 0; i < children.Count; i++)
                {
                    result += BuildTree(children[i]!.Id, prefix + (isLast ? "    " : "│   "), i == children.Count - 1);
                }

                return result;
            }
        }

        public void ClearTree()
        {
            repository.ClearTree();
            Console.WriteLine("Tree cleared.");
        }
    }
}
