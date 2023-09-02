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
        public List<string> GetNameSpaces()
        {
          
            DTE dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE;
            IEnumerable<ProjectItems> myProjectItems = new List<ProjectItems>();
            var sln = dte.Solution;

            List<Project> projects = new List<Project>();
            projects.AddRange(sln.Projects.Cast<Project>());
            List<Project> subProjects = new List<Project>();
            List<Project> subsubProjects = new List<Project>();

            for (int i = 0; i < projects.Count; i++)
                subProjects.AddRange(projects[i].ProjectItems.Cast<ProjectItem>().Select(x => x.SubProject).OfType<Project>());
            for (int i = 0; i < subProjects.Count; i++)
                subsubProjects.AddRange(subProjects[i].ProjectItems.Cast<ProjectItem>().Select(x => x.SubProject).OfType<Project>());
            List<string> projectNameList = new List<string>();

            foreach (Project subProjectName in subsubProjects)
            {
                string projectName = Convert.ToString(subProjectName.Name);

                if (!projectName.StartsWith("Core"))
                {
                    projectNameList.Add(projectName);

                }
            }
            return projectNameList;

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

