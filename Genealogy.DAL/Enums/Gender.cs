namespace Genealogy.DAL.Enums
{
    public enum Gender
    {
        Male,
        Female
    }

    public static class GenderExtensions
    {
        public static string ToRole(this Gender gender, bool isParent)
        {
            if (isParent) return gender == Gender.Male ? "Father" : "Mother";
            return gender == Gender.Male ? "Son" : "Daughter";
        }
    }
}