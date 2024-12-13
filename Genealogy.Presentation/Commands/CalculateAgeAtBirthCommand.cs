using Genealogy.BLL.Services;

namespace Genealogy.Presentation.Commands;

public class CalculateAgeAtBirthCommand(ITreeManager treeManager) : ICommand
{
    public void Execute()
    {
        Console.Write("Enter Parent ID: ");
        var parentId = Guid.Parse(Console.ReadLine()!);
        Console.Write("Enter Child ID: ");
        var childId = Guid.Parse(Console.ReadLine()!);

        var ageAtBirth = treeManager.CalculateAgeAtBirth(parentId, childId);
        Console.WriteLine($"Age at birth: {ageAtBirth}");
    }
}