namespace Genealogy.BLL.Services;

public interface IValidationService
{
    void ValidateGender(string gender);
    void ValidateDate(string date);
    void ValidateUuid(string uuid);
    void ValidateRelationshipType(string relationshipType);
}