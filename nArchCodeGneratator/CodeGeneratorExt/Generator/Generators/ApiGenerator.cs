using CodeGeneratorExt.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeGeneratorExt.Generator
{
    public class ApiGenerator : Generator
    {
        private readonly List<ClassModel> _classList;
        private readonly string _basePath;


        public ApiGenerator(List<ClassModel> classList, string basePath)
        {
            _classList = classList;
            _basePath = basePath;
        }

        public override void Generate()
        {
            foreach (var model in _classList)
            {
                string path = _basePath  +  @$"\Controllers\{model.Name}Controller.cs";
                StringBuilder sb = new();
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Commands.Create{model.Name};");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Dtos;");
                sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
                sb.AppendLine("");
                sb.AppendLine("namespace BloodBrother.WebAPI.Controllers");
                sb.AppendLine("{");
                sb.AppendLine();
                sb.AppendLine();

                sb.AppendLine(GenerateAddEndPoint());
                sb.AppendLine();
                sb.AppendLine();

                sb.AppendLine(GenerateGetByDynamicPoint());
                sb.AppendLine();
                sb.AppendLine();

                sb.AppendLine(GenerateGetListPoint());
                sb.AppendLine();
                sb.AppendLine();

                sb.AppendLine(GenerateUpdatePoint());
                sb.AppendLine("}");
                sb.AppendLine("");
                string controllerCode = sb.ToString();

                string directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                File.WriteAllText(path, controllerCode);
            }
           
        }

        private string GenerateAddEndPoint()
        {
            StringBuilder sb = new();
            foreach (var model in _classList)
            {


                sb.AppendLine("    [Route(\"api / [controller]\")]");
                sb.AppendLine("    [ApiController]");
                sb.AppendLine("    public class PartiesController : BaseController");
                sb.AppendLine("    {");
                sb.AppendLine("");
                sb.AppendLine("        [HttpPost]");
                sb.AppendLine($"        public async Task<IActionResult> Add([FromBody] Create{model.Name}Command create{model.Name}Command)");
                sb.AppendLine("        {");
                sb.AppendLine($"            Create{model.Name}Dto result = await Mediator.Send(create{model.Name}Command);");
                sb.AppendLine("            return Created(\"\", result);");
                sb.AppendLine("        }");
                sb.AppendLine("    }");

            }
            return sb.ToString();
        }

        private string GenerateGetByDynamicPoint()
        {
            StringBuilder sb = new();
            foreach (var model in _classList)
            {


                sb.AppendLine("");
                sb.AppendLine("        [HttpPost(\"GetList / ByDynamic\")]");
                sb.AppendLine("        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)");
                sb.AppendLine("        {");
                sb.AppendLine($"            GetList{model.Name}ByDynamicQuery getListByDynamicQuery = new GetList{model.Name}ByDynamicQuery {{ PageRequest = pageRequest, Dynamic = dynamic }};");
                sb.AppendLine($"            {model.Name}ListModel result = await Mediator.Send(getListByDynamicQuery);");
                sb.AppendLine("            return Ok(result);");
                sb.AppendLine("");
                sb.AppendLine("        }");

            }
            return sb.ToString();
        }
        private string GenerateGetListPoint()
        {
            StringBuilder sb = new();
            foreach (var model in _classList)
            {


                sb.AppendLine("        [HttpPost(\"GetList / All\")]");
                sb.AppendLine("        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)");
                sb.AppendLine("        {");
                sb.AppendLine($"            GetList{model.Name}Query getListQuery = new GetList{model.Name}Query {{ PageRequest = pageRequest }};");
                sb.AppendLine($"            {model.Name}ListModel result = await Mediator.Send(getListQuery);");
                sb.AppendLine("            return Ok(result);");
                sb.AppendLine("");
                sb.AppendLine("        }");

            }
            return sb.ToString();
        }

        private string GenerateUpdatePoint()
        {
            StringBuilder sb = new();
            foreach (var model in _classList)
            {


                sb.AppendLine("        [HttpPost(\"Update\")]");
                sb.AppendLine($"        public async Task<ActionResult> Update([FromQuery] {model.Name}PartyCommand update{model.Name}Command)");
                sb.AppendLine("        {");
                sb.AppendLine($"            Update{model.Name}Dto result = await Mediator.Send(update{model.Name}Command);");
                sb.AppendLine("            return Ok(result);");
                sb.AppendLine("");
                sb.AppendLine("        }");

            }
            return sb.ToString();
        }
    }

}

