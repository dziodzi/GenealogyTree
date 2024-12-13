using Genealogy.BLL.Exceptions;
using Genealogy.DAL.Enums;

namespace Genealogy.BLL.Services;

public class ValidationService : IValidationService
{
    public void ValidateGender(string gender)
    {
        if (!Enum.TryParse<Gender>(gender, true, out _))
            throw new InvalidGenderException("Invalid gender. Please use 'Male' or 'Female'.");
    }

    public void ValidateDate(string date)
    {
        if (!DateTime.TryParse(date, out _))
            throw new InvalidBirthDateException("Invalid date format. Please use 'yyyy-MM-dd'.");
    }

    public void ValidateUuid(string uuid)
    {
        if (!Guid.TryParse(uuid, out _))
            throw new InvalidPersonIdException("Invalid UUID format.");
    }

    public void ValidateRelationshipType(string relationshipType)
    {
        if (!Enum.TryParse<RelationshipType>(relationshipType, true, out _))
            throw new InvalidRelationshipTypeException("Invalid relationship type.");
    }
}