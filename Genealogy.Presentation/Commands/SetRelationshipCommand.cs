using Genealogy.BLL.Services;

namespace Genealogy.Presentation.Commands;

public class SetRelationshipCommand(ITreeManager treeManager) : ICommand
{
    public void Execute()
    {
        Console.Write("Enter Relationship (ParentChild/Spouse): ");
        var relationship = Console.ReadLine();
        Console.Write("Enter First Person ID: ");
        var person1Id = Console.ReadLine();
        Console.Write("Enter Second Person ID: ");
        var person2Id = Console.ReadLine();

        treeManager.SetRelationship(person1Id!, person2Id!, relationship!);
    }
}