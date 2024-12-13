using Genealogy.BLL.Services;

namespace Genealogy.Presentation.Commands;

public class AddPersonCommand(ITreeManager treeManager) : ICommand
{
    public void Execute()
    {
        Console.Write("Enter Full Name: ");
        var name = Console.ReadLine();
        Console.Write("Enter Birth Date (yyyy-MM-dd): ");
        var birthDate = Console.ReadLine();
        Console.Write("Enter Gender (Male/Female): ");
        var gender = Console.ReadLine();

        treeManager.AddPerson(name!, birthDate!, gender!);
    }
}
