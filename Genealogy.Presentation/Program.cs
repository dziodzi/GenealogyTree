using Genealogy.BLL.Services;
using Microsoft.Extensions.Configuration;
using Genealogy.DAL.Repositories;
using Genealogy.Presentation.Commands;

namespace Genealogy.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            var filePath = configuration.GetSection("RepositorySettings:FilePath").Value;
            
            var repository = new TreeRepository(filePath);
            var manager = new TreeManager(repository);

            var commands = new Dictionary<string, ICommand>
            {
                { "1", new AddPersonCommand(manager) },
                { "2", new SetRelationshipCommand(manager) },
                { "3", new ShowRelativesCommand(manager) },
                { "4", new ShowTreeCommand(manager) },
                { "5", new FindCommonAncestorsCommand(manager) },
                { "6", new CalculateAgeAtBirthCommand(manager) },
                { "7", new ClearTreeCommand(manager) }
            };

            while (true)
            {
                Console.WriteLine("1. Add Person\n2. Set Relationship\n3. Show Relatives\n4. Show Tree\n5. Find Common Ancestors\n6. Calculate Age At Birth\n7. Clear Tree\n8. Exit");
                var choice = Console.ReadLine();

                try
                {
                    if (choice == "8")
                        return;

                    if (commands.ContainsKey(choice))
                    {
                        commands[choice].Execute();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
