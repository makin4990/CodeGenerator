using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeGeneratorExt.Models;

namespace CodeGeneratorExt.Generator
{


    public class ApplicationGenerator : Generator
    {
        private readonly List<ClassModel> _classList;
        private readonly string _basePath;

        public ApplicationGenerator(List<ClassModel> classList, string basePath)
        {
            _classList = classList;
            _basePath = basePath;
        }

        public override void Generate()
        {
            GenerateCreateCommands();
            GenerateUpdateCommands();
            GenerateDtos();
            GenerateModels();
            GenerateMappingProfile();
            GetListPartyQueries();
            GetPartyByIdQueries();
            GetListByDynamicQuery();
            GenerateConstants();
            GenerateServices();
        }
        private void GenerateCreateCommands()
        {

            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                string createPath = _basePath  +  @$"\Features\Commands\Create{model.Name}\Create{model.Name}Command.cs";
               
                sb.AppendLine("using AutoMapper;");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Dtos;");
                sb.AppendLine("using BloodBrother.Application.Services.Repositories;");
                sb.AppendLine("using BloodBrother.Domain.Entities;");
                sb.AppendLine("using MediatR;");
                sb.AppendLine("");
                sb.AppendLine($"namespace BloodBrother.Application.Features.{model.Name}Features.Commands.Create{model.Name}");
                sb.AppendLine("{");
                sb.AppendLine($"    public class Create{model.Name}Command:IRequest<Create{model.Name}Dto>");
                sb.AppendLine("    {");

                foreach (var item in model.ClassProperties)
                {
                    sb.AppendLine("    " + item.FullText);
                }
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("    }");
                sb.AppendLine($"    public class Create{model.Name}CommandHandler : IRequestHandler<Create{model.Name}Command, Create{model.Name}Dto>");
                sb.AppendLine("    {");
                sb.AppendLine($"        private readonly I{model.Name}Repository _{model.Name.ToLower()}Repository;");
                sb.AppendLine($"        private readonly IMapper _mapper;");
                sb.AppendLine($"        //private readonly PartyBusinessRules _partyBusinessRules;");
                sb.AppendLine($"");
                sb.AppendLine($"        public Create{model.Name}CommandHandler(I{model.Name}Repository {model.Name.ToLower()}Repository, IMapper mapper)");
                sb.AppendLine("        {");
                sb.AppendLine($"            _{model.Name.ToLower()}Repository = {model.Name.ToLower()}Repository;");
                sb.AppendLine($"            _mapper = mapper;");
                sb.AppendLine("        }");
                sb.AppendLine($"");
                sb.AppendLine($"        public async Task<Create{model.Name}Dto> Handle(Create{model.Name}Command request, CancellationToken cancellationToken)");
                sb.AppendLine("        {");
                sb.AppendLine($"            {model.Name} mapped{model.Name} = _mapper.Map<Party>(request);");
                sb.AppendLine($"            {model.Name} created{model.Name} = await _{model.Name.ToLower()}Repository.AddAsync(mapped{model.Name});");
                sb.AppendLine($"            Create{model.Name}Dto created{model.Name}Dto = _mapper.Map<Create{model.Name}Dto>(created{model.Name});");
                sb.AppendLine($"            return created{model.Name}Dto;");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("}");

                var classCode = sb.ToString();
                File.WriteAllText(createPath, classCode);
              


            }
        }
        private void GenerateUpdateCommands()
        {

            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                string createPath = _basePath  +  @$"\Features\Commands\Update{model.Name}\Update{model.Name}Command.cs";

                sb.AppendLine("using AutoMapper;");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Commands.Create{model.Name};");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Dtos;");
                sb.AppendLine("using BloodBrother.Application.Services.Repositories;");
                sb.AppendLine("using BloodBrother.Domain.Entities;");
                sb.AppendLine("using MediatR;");
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using System.Linq;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine("using System.Threading.Tasks;");
                sb.AppendLine("");
                sb.AppendLine($"namespace BloodBrother.Application.Features.{model.Name}Features.Commands.Update{model.Name};");
                sb.AppendLine("");
                sb.AppendLine($"public class Update{model.Name}Command: IRequest<Update{model.Name}Dto>");
                sb.AppendLine("{");
                foreach (var item in model.ClassProperties)
                {
                    sb.AppendLine(item.FullText);
                }
                sb.AppendLine("}");
                sb.AppendLine("");
                sb.AppendLine($"public class Update{model.Name}CommandHandler : IRequestHandler<Update{model.Name}Command,Update{model.Name}Dto>");
                sb.AppendLine("{");
                sb.AppendLine($"    private readonly I{model.Name}Repository _{model.Name.ToLower()}Repository;");
                sb.AppendLine("    private readonly IMapper _mapper;");
                sb.AppendLine($"    //private readonly {model.Name}BusinessRules _{model.Name.ToLower()}BusinessRules;");
                sb.AppendLine("");
                sb.AppendLine($"    public Update{model.Name}CommandHandler(I{model.Name}Repository {model.Name.ToLower()}Repository, IMapper mapper)");
                sb.AppendLine("    {");
                sb.AppendLine($"        _{model.Name.ToLower()}Repository = {model.Name.ToLower()}Repository;");
                sb.AppendLine("        _mapper = mapper;");
                sb.AppendLine("    }");
                sb.AppendLine("");
                sb.AppendLine($"    public async Task<Update{model.Name}Dto> Handle(Update{model.Name}Command request, CancellationToken cancellationToken)");
                sb.AppendLine("    {");
                sb.AppendLine("");
                sb.AppendLine($"        {model.Name}? entityToUpdate = await _{model.Name.ToLower()}Repository.GetAsync(i => i.Id == request.Id);");
                sb.AppendLine($"        {model.Name}? mapped{model.Name} = _mapper.Map(request,entityToUpdate);");
                sb.AppendLine($"        {model.Name}? UpdatedEntity = await _{model.Name.ToLower()}Repository.UpdateAsync(mapped{model.Name});");
                sb.AppendLine($"        Update{model.Name}Dto updatedDto = _mapper.Map<Update{model.Name}Dto>(UpdatedEntity);");
                sb.AppendLine("        return updatedDto;");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                sb.AppendLine("");

                var classCode = sb.ToString();
                File.WriteAllText(createPath, classCode);
            }
        }
        private void GenerateDtos()
        {
            foreach (var model in _classList)
            {

                var path = _basePath  +  @$"\Features\{model.Name}Features\Dtos";
               
                var createClassCode = model.ToCreateDto();
                File.WriteAllText(path, createClassCode);

                var updateClassCode = model.ToUpdateDto();
                File.WriteAllText(path, updateClassCode);

                var listClassCode = model.ToListModel();
                File.WriteAllText(path, listClassCode);

                var dtoCode = model.ToDto();
                File.WriteAllText(path, dtoCode);

            }
        }
        private void GenerateModels()
        {
            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                var path = _basePath  +  @$"\Features\{model.Name}Features\Dtos";
             
                sb.AppendLine("using CoreFramework.Persistence.Paging;");
                sb.AppendLine("");
                sb.AppendLine("namespace BloodBrother.Application.Features.PartyFeatures.Models;");
                sb.AppendLine("public class PartyListModel:BasePageableModel");
                sb.AppendLine("{");
                sb.AppendLine($"    public IList<{model.Name}ListModel> Items {{ get; set; }}");
                sb.AppendLine("}");

                var createClassCode = sb.ToString();
                File.WriteAllText(path, createClassCode);
            }
        }
        private void GenerateMappingProfile()
        {
            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                var path = _basePath  +  @$"\Features\{model.Name}Features\Profiles\MappingProfiles.cs";

                sb.AppendLine("using AutoMapper;");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Commands.Create{model.Name};");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Dtos;");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Models;");
                sb.AppendLine("using BloodBrother.Domain.Entities;");
                sb.AppendLine("using CoreFramework.Persistence.Paging;");
                sb.AppendLine("");
                sb.AppendLine($"namespace BloodBrother.Application.Features.{model.Name}Features.Profiles;");
                sb.AppendLine("");
                sb.AppendLine("public class MappingProfiles : Profile");
                sb.AppendLine("{");
                sb.AppendLine("    public MappingProfiles()");
                sb.AppendLine("    {");
                sb.AppendLine("        CreateMap<{model.Name}, {model.Name}ListModelDto>().ReverseMap().ForAllMembers(opt => opt.Condition(src => src != null));");
                sb.AppendLine("");
                sb.AppendLine("        CreateMap<IPaginate<{model.Name}>, {model.Name}ListModel>().ReverseMap().ForAllMembers(opt => opt.Condition(src => src != null));");
                sb.AppendLine("        CreateMap<Create{model.Name}Command, {model.Name}>().ReverseMap().ForAllMembers(opt => opt.Condition(src => src != null));");
                sb.AppendLine("        CreateMap<Update{model.Name}Command, {model.Name}>().ReverseMap().ForAllMembers(opt => opt.Condition(src => src != null));");
                sb.AppendLine("        CreateMap<Create{model.Name}Dto, {model.Name}>().ReverseMap().ForAllMembers(opt => opt.Condition(src => src != null));");
                sb.AppendLine("        CreateMap<Update{model.Name}Dto, {model.Name}>().ReverseMap().ForAllMembers(opt => opt.Condition(src => src != null));");
                sb.AppendLine("        CreateMap<{model.Name}Dto, {model.Name}>().ReverseMap().ForAllMembers(opt => opt.Condition(src => src != null));");
                sb.AppendLine("");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                sb.AppendLine("");
            }
        }
        private void GetListPartyQueries()
        {
            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                var path = _basePath  +  @$"\Features\{model.Name}Features\Queries\GetList{model.Name}\GetList{model.Name}Query.cs";

                sb.AppendLine("using AutoMapper;");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Models;");
                sb.AppendLine("using BloodBrother.Application.Services.Repositories;");
                sb.AppendLine("using BloodBrother.Domain.Entities;");
                sb.AppendLine("using CoreFramework.Application.Requests;");
                sb.AppendLine("using CoreFramework.Persistence.Paging;");
                sb.AppendLine("using MediatR;");
                sb.AppendLine("");
                sb.AppendLine($"namespace BloodBrother.Application.Features.{model.Name}Features.Queries.GetListParty;");
                sb.AppendLine("");
                sb.AppendLine($"public class GetList{model.Name}Query:IRequest<{model.Name}ListModel>");
                sb.AppendLine("{");
                sb.AppendLine("    public PageRequest  PageRequest { get; set; }");
                sb.AppendLine($"    public class GetList{model.Name}QueryHandler : IRequestHandler<GetList{model.Name}Query, {model.Name}ListModel>");
                sb.AppendLine("    {");
                sb.AppendLine("        private readonly IMapper _mapper;");
                sb.AppendLine($"        private readonly I{model.Name}Repository _{model.Name.ToLower()}Repository;");
                sb.AppendLine("");
                sb.AppendLine($"        public GetList{model.Name}QueryHandler(IMapper mapper, I{model.Name}Repository {model.Name.ToLower()}Repository)");
                sb.AppendLine("        {");
                sb.AppendLine("            _mapper = mapper;");
                sb.AppendLine($"            _{model.Name.ToLower()}Repository = {model.Name.ToLower()}Repository;");
                sb.AppendLine("        }");
                sb.AppendLine("");
                sb.AppendLine($"        public async Task<{model.Name}ListModel> Handle(GetList{model.Name}Query request, CancellationToken cancellationToken)");
                sb.AppendLine("        {");
                sb.AppendLine($"            IPaginate<{model.Name}> {model.Name.ToLower()}List = await _{model.Name.ToLower()}Repository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);");
                sb.AppendLine($"            {model.Name}ListModel {model.Name.ToLower()}ListModel = _mapper.Map<IPaginate<{model.Name}>, {model.Name}ListModel>({model.Name.ToLower()}List);");
                sb.AppendLine($"            return {model.Name.ToLower()}ListModel;");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                sb.AppendLine("");
            }
        }
        private void GetPartyByIdQueries()
        {
            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                var path = _basePath  +  @$"\Features\{model.Name}Features\Queries\Get{model.Name}ById\Get{model.Name}ByIdQuery.cs";

                sb.AppendLine("using AutoMapper;");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}Features.Dtos;");
                sb.AppendLine("using BloodBrother.Application.Services.Repositories;");
                sb.AppendLine("using BloodBrother.Domain.Entities;");
                sb.AppendLine("using MediatR;");
                sb.AppendLine("");
                sb.AppendLine($"namespace BloodBrother.Application.Features.{model.Name}Features.Queries.Get{model.Name}ById;");
                sb.AppendLine("");
                sb.AppendLine($"public class Get{model.Name}ByIdQuery: IRequest<{model.Name}Dto>");
                sb.AppendLine("{");
                sb.AppendLine("    public int Id { get; set; }");
                sb.AppendLine("}");
                sb.AppendLine($"public class Get{model.Name}ByIdQueryHandler : IRequestHandler<Get{model.Name}ByIdQuery, {model.Name}Dto>");
                sb.AppendLine("{");
                sb.AppendLine("    private readonly IMapper _mapper;");
                sb.AppendLine($"    private readonly I{model.Name}Repository _{model.Name.ToLower()}Repository;");
                sb.AppendLine("");
                sb.AppendLine($"    public Get{model.Name}ByIdQueryHandler(IMapper mapper, I{model.Name}Repository {model.Name.ToLower()}Repository)");
                sb.AppendLine("    {");
                sb.AppendLine("        _mapper = mapper;");
                sb.AppendLine($"        _{model.Name.ToLower()}Repository = {model.Name.ToLower()}Repository;");
                sb.AppendLine("    }");
                sb.AppendLine("");
                sb.AppendLine($"    public async Task<{model.Name}Dto> Handle(Get{model.Name}ByIdQuery request, CancellationToken cancellationToken)");
                sb.AppendLine("    {");
                sb.AppendLine($"        {model.Name} {model.Name.ToLower()} = await _{model.Name.ToLower()}Repository.GetAsync(i=> i.Id == request.Id);");
                sb.AppendLine($"        {model.Name}Dto {model.Name.ToLower()}Dto = _mapper.Map<{model.Name}, {model.Name}Dto>({model.Name.ToLower()});");
                sb.AppendLine($"        return {model.Name.ToLower()}Dto;");
                sb.AppendLine("    }");
                sb.AppendLine("}");

                string getByIdQueryCode = sb.ToString();
                File.WriteAllText(path, getByIdQueryCode);
            }
        }
        private void GetListByDynamicQuery()
        {
            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                var path = _basePath  +  @$"\Features\{model.Name}Features\Queries\GetList{model.Name}ByDynamic\GetList{model.Name}ByDynamicQuery.cs";

                sb.AppendLine("using AutoMapper;");
                sb.AppendLine($"using BloodBrother.Application.Features.{model.Name}.Models;");
                sb.AppendLine("using BloodBrother.Application.Services.Repositories;");
                sb.AppendLine("using BloodBrother.Domain.Entities;");
                sb.AppendLine("using CoreFramework.Application.Requests;");
                sb.AppendLine("using CoreFramework.Persistence.Dynamic;");
                sb.AppendLine("using CoreFramework.Persistence.Paging;");
                sb.AppendLine("using MediatR;");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine($"namespace BloodBrother.Application.Features.{model.Name.ToLower()}.Queries.GetList{model.Name}ByDynamic;");
                sb.AppendLine("");
                sb.AppendLine($"public class GetList{model.Name}ByDynamicQuery : IRequest<{model.Name}ListModel>");
                sb.AppendLine("{");
                sb.AppendLine("    public Dynamic Dynamic { get; set; }");
                sb.AppendLine("    public PageRequest PageRequest { get; set; }");
                sb.AppendLine("");
                sb.AppendLine("}");
                sb.AppendLine($"public class GetList{model.Name}ByDynamicQueryHandler : IRequestHandler<GetList{model.Name}ByDynamicQuery, {model.Name}ListModel>");
                sb.AppendLine("{");
                sb.AppendLine("    private readonly IMapper _mapper;");
                sb.AppendLine($"    private readonly I{model.Name}EntityRepository _{model.Name.ToLower()}EntityRepository;");
                sb.AppendLine("");
                sb.AppendLine($"    public GetList{model.Name}ByDynamicQueryHandler(IMapper mappler, I{model.Name}EntityRepository {model.Name.ToLower()}EntityRepository)");
                sb.AppendLine("    {");
                sb.AppendLine("        _mapper = mappler;");
                sb.AppendLine($"        _{model.Name.ToLower()}EntityRepository = {model.Name.ToLower()}EntityRepository;");
                sb.AppendLine("    }");
                sb.AppendLine("");
                sb.AppendLine($"    public async Task<{model.Name}ListModel> Handle(GetList{model.Name}ByDynamicQuery request, CancellationToken cancellationToken)");
                sb.AppendLine("    {");
                sb.AppendLine($"        IPaginate<{model.Name}Entity> {model.Name.ToLower()}s = await _{model.Name.ToLower()}EntityRepository.GetListByDynamicAsync(request.Dynamic,");
                sb.AppendLine("            index: request.PageRequest.Page,");
                sb.AppendLine("            size: request.PageRequest.PageSize);");
                sb.AppendLine($"        {model.Name}ListModel mappedModels = _mapper.Map<{model.Name}ListModel>({model.Name.ToLower()}s);");
                sb.AppendLine("");
                sb.AppendLine("        return mappedModels;");
                sb.AppendLine("");
                sb.AppendLine("    }");
                sb.AppendLine("}");

                string getListByDynamicCode = sb.ToString();
                File.WriteAllText(path, getListByDynamicCode);
            }
        }
        private void GenerateConstants() 
        {
            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                var path = _basePath  +  @$"\Features\{model.Name}\Constants\OperationClaims.cs";

                sb.AppendLine($" BloodBrother.Application.Features.{model.Name}.Constants");
                sb.AppendLine("{");
                sb.AppendLine("    public static class OperationClaims");
                sb.AppendLine("    {");
                sb.AppendLine($"        public const string {model.Name}Add = \"{model.Name}\";");
                sb.AppendLine("    }");
                sb.AppendLine("}");

                string getListByDynamicCode = sb.ToString();
                File.WriteAllText(path, getListByDynamicCode);
            }
        }
        private void GenerateServices()
        {
            foreach (var model in _classList)
            {
                StringBuilder sb = new StringBuilder();
                var path = _basePath  +  @$"\Services\Repositories\I{model.Name}Repository.cs";

                sb.AppendLine("using BloodBrother.Domain.Entities;");
                sb.AppendLine("using CoreFramework.Persistence.Repositories;");
                sb.AppendLine("");
                sb.AppendLine("namespace BloodBrother.Application.Services.Repositories");
                sb.AppendLine("{");
                sb.AppendLine($"    public interface I{model.Name}Repository : IAsyncRepository<{model.Name}>, IRepository<{model.Name}>");
                sb.AppendLine("    {");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                sb.AppendLine("");

                string repositoryServiceCode = sb.ToString();
                File.WriteAllText(path, repositoryServiceCode);
            }

        }
    }

}