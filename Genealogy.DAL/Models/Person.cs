using Genealogy.DAL.Enums;

namespace Genealogy.DAL.Models
{
    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public List<Guid> ParentIds { get; set; } = new();
        public List<Guid> ChildIds { get; set; } = new();
        public Guid? SpouseId { get; set; }
    }
}