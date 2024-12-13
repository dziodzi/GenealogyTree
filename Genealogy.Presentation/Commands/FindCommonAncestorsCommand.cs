using Genealogy.BLL.Services;

namespace Genealogy.Presentation.Commands;

public class FindCommonAncestorsCommand(ITreeManager treeManager) : ICommand
{
    public void Execute()
    {
        Console.Write("Enter First Person ID: ");
        var firstPersonId = Guid.Parse(Console.ReadLine()!);
        Console.Write("Enter Second Person ID: ");
        var secondPersonId = Guid.Parse(Console.ReadLine()!);

        var commonAncestors = treeManager.FindCommonAncestors(firstPersonId, secondPersonId);
        Console.WriteLine("Common Ancestors: " + string.Join(", ", commonAncestors.Select(a => a.FullName)));
    }
}