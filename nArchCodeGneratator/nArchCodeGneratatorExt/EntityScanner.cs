using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace nArchCodeGeneratorExt
{
    public class EntityScanner
    {
        public List<string> GetEntityClassNames()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            DTE dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE;
            IEnumerable<ProjectItems> myProjectItems = new List<ProjectItems>();
            var x = dte.Solution.Projects;
            Dictionary<string, List<string>> names = new Dictionary<string, List<string>>();

            if (dte != null)
            {
                Solution solution = dte.Solution;

                if (solution != null)
                {
                    foreach (Project project in solution.Projects)
                    {
                        // Iterate through project items
                        var classes = GetProjectItems(project.ProjectItems).Where(v=> v.Name.Contains(".cs"));
                        List<string> values = new List<string>();

                        foreach (var c in classes)
                        {
                            var eles = c.FileCodeModel;
                            if (eles == null)
                                continue;
                            foreach (var ele in eles.CodeElements)
                            {
                                if (ele is EnvDTE.CodeNamespace)
                                {
                                    var ns = ele as EnvDTE.CodeNamespace;
                                    // run through classes
                                    foreach (var property in ns.Members)
                                    {
                                        var member = property as CodeType;
                                        if (member == null)
                                            continue;

                                        foreach (var d in member.Bases)
                                        {
                                            var dClass = d as CodeClass;
                                            if (dClass == null)
                                                continue;

                                          values.Add(member.Name);
                                           
                                        }
                                    }
                                }
                            }
                        }
                        names.Add(project.Name, values);


                    }
                }
            }
            //var project = dte.ActiveDocument.ProjectItem.ContainingProject;
            //foreach (EnvDTE.CodeElement element in project.CodeModel.CodeElements)
            //{
            //    if (element.Kind == EnvDTE.vsCMElement.vsCMElementClass)
            //    {
            //        var myClass = (EnvDTE.CodeClass)element;
            //        // do stuff with that class here
            //    }
            //}
            //if (dte != null)
            //{
            //    Solution solution = dte.Solution;

            //    if (solution != null)
            //    {
            //        // Now you can use 'solution' for your needs
            //        // For example, you can access solution properties:
            //        string solutionName = solution.FullName;
            //        string solutionPath = solution.FullName;
            //        string solutionFileName = solution.FileName;

            //        // Access projects within the solution
            //        foreach (Project project in solution.Projects)
            //        {
            //            foreach (ProjectItem projectItem in project.Object)
            //            {
            //                foreach (var item in projectItem.ProjectItems)
            //                {

            //                    if (item == "BloodBrother")
            //                    {
            //                        foreach (CodeElement codeElement in projectItem.FileCodeModel.CodeElements)
            //                        {
            //                            if (codeElement.Kind == vsCMElement.vsCMElementClass)
            //                            {
            //                                CodeClass codeClass = codeElement as CodeClass;
            //                                if (codeClass != null)
            //                                {
            //                                    string className = codeClass.Name;
            //                                    // Now you have the class name for each class in the project
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}


            return assembly.GetTypes()
                           .Where(cls => cls.IsClass
                                      && !cls.IsAbstract
                                      && cls.Namespace.EndsWith("Entities"))
                           .Select(cls => cls.Name)
                           .ToList();

        }
        private Project GetActiveProject(DTE dte)
        {
            try
            {
                Array activeSolutionProjects = dte.ActiveSolutionProjects as Array;
                if (activeSolutionProjects != null && activeSolutionProjects.Length > 0)
                {
                    return activeSolutionProjects.GetValue(0) as Project;
                }
            }
            catch (Exception)
            {
                // Handle any exception that might occur when retrieving the active project
            }

            return null;
        }
        public IEnumerable<ProjectItem> GetProjectItems(EnvDTE.ProjectItems projectItems)
        {
            foreach (EnvDTE.ProjectItem item in projectItems)
            {
                yield return item;

                if (item.SubProject != null)
                {
                    foreach (EnvDTE.ProjectItem childItem in GetProjectItems(item.SubProject.ProjectItems))
                        yield return childItem;
                }
                else
                {
                    foreach (EnvDTE.ProjectItem childItem in GetProjectItems(item.ProjectItems))
                        yield return childItem;
                }
            }

        }
    }
  

}

