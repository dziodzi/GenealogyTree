using Genealogy.BLL.Services;

namespace Genealogy.Presentation.Commands;

public class ShowRelativesCommand(ITreeManager treeManager) : ICommand
{
    public void Execute()
    {
        Console.Write("Enter Person ID: ");
        var personId = Guid.Parse(Console.ReadLine()!);

        var relatives = treeManager.GetRelatives(personId);
        foreach (var (relative, role) in relatives)
        {
            Console.WriteLine($"{relative.FullName} ({role})");
        }
    }
}