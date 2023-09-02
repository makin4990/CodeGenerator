using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator;

public class PersistenceGenerator : Generator
{
    private readonly List<ClassModel> _classList;
    private readonly string _basePath;


    public PersistenceGenerator(List<ClassModel> classList, string basePath)
    {
        _classList = classList;
        _basePath = basePath;
    }

    public override void Generate()
    {
        GeneratePersistenceServiceRegistration();
        GenereteRepository();
    }
    private void GeneratePersistenceServiceRegistration()
    {
        var filePath = _basePath  +  @"\BloodBrother.Persistence\PersistenceServiceRegistration.cs";
        string fileContent = File.ReadAllText(filePath);

        string[] lines = fileContent.Split('\n');
        int lastServicesIndex = -1;
        for (int i = lines.Length - 1; i >= 0; i--)
        {
            if (lines[i].Trim().StartsWith("services"))
            {
                lastServicesIndex = i;
                break;
            }
        }

        if (lastServicesIndex != -1)
        {
            foreach (var model in _classList)
            {
                bool isAdded = false;
               StringBuilder newLines = new();
                for (int i =0; i < lines.Length; i++)
                {
                    if (i == lastServicesIndex+1 && !isAdded)
                    {
                        string newLine = "            services.AddScoped<I{model.Name}Repository, {model.Name}Repository>();";
                        newLines.AppendLine(newLine);
                        lastServicesIndex++;
                        isAdded = true;
                    }
                    newLines.Append(lines[i]);
                }
                File.WriteAllText(filePath, newLines.ToString());
            }


        }

    }
    private void GenereteRepository()
    {
        foreach (var model in _classList)
        {
            StringBuilder sb = new StringBuilder();
            string path = _basePath  +  @$"\BloodBrother.Persistence\Repositories\{model.Name}Repository.cs";

            sb.AppendLine("using BloodBrother.Application.Services.Repositories;");
            sb.AppendLine("using BloodBrother.Domain.Entities;");
            sb.AppendLine("using BloodBrother.Persistence.Contexts;");
            sb.AppendLine("using CoreFramework.Persistence.Repositories;");
            sb.AppendLine("");
            sb.AppendLine("namespace BloodBrother.Persistence.Repositories;");
            sb.AppendLine("");
            sb.AppendLine($"public class PartyRepository: EfRepositoryBase<{model.Name}, BaseDbContext>, I{model.Name}Repository");
            sb.AppendLine("{");
            sb.AppendLine($"    public {model.Name}Repository(BaseDbContext context):base(context)");
            sb.AppendLine("    {");
            sb.AppendLine("");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine("");

            string repoCode = sb.ToString();
            File.WriteAllText(path, repoCode);

        }
       
    }

}

