using Cocona;
using System.Reflection;
using TheLair.CLI.Enums;

namespace TheLair.CLI.Commands;

public class CreateCommand
{
    [Command("create-repository")]
    public int CreateRepo([Argument]string name, [Argument]string context)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resourceName = "TheLair.CLI.Resources.RepositoryTemplate.txt";

        using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
        using (StreamReader reader = new StreamReader(stream))
        {
            string result = reader.ReadToEnd()
                .Replace("{Name}", name)
                .Replace("{Context}", context);
            
            File.WriteAllText($"{name}Repository.cs", result);
        }

        return (0);
    }

    [Command("create-entity")]
    public int CreateEntity([Argument] string name)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resourceName = "TheLair.CLI.Resources.EntityTemplate.txt";

        using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
        using (StreamReader reader = new StreamReader(stream))
        {
            string result = reader.ReadToEnd()
                .Replace("{Name}", name);

            File.WriteAllText($"{name}.cs", result);
        }

        return (0);
    }
}
