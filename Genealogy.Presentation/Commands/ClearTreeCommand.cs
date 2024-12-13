using Genealogy.BLL.Services;

namespace Genealogy.Presentation.Commands;

public class ClearTreeCommand(ITreeManager treeManager) : ICommand
{
    public void Execute()
    {
        treeManager.ClearTree();
    }
}