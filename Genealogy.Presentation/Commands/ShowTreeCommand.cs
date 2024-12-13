using Genealogy.BLL.Services;

namespace Genealogy.Presentation.Commands;

public class ShowTreeCommand(ITreeManager treeManager) : ICommand
{
    public void Execute()
    {
        Console.WriteLine(treeManager.GetTreeRepresentation());
    }
}