using System;
using System.Linq;
using System.Collections.Generic;

namespace Timecards.Infrastructure.Model
{
    public class ProjectResult
    {
        public List<Project> Projects { get; set; }

        public List<Project> GetProjectTree()
        {
            List<Project> projectTree = new List<Project>();
            Projects
                .Where(p => p.ParentProjectId == null)
                .OrderBy(p => p.ProjectType)
                .ThenBy(p => p.ParentName)
                .ToList()
                .ForEach(x =>
                {
                    x.Name = GetExpectedLengthName(x.Name);
                    projectTree.Add(x);
                    var subProjects = Projects.Where(p => p.ParentProjectId == x.ProjectId).ToList();
                    if (subProjects.Any())
                    {
                        projectTree.AddRange(subProjects);
                    }
                });

            return projectTree;
        }

        private static string GetExpectedLengthName(string name)
        {
            var MaxNameDisplayLength = 70;
            var expectedNameLength = (MaxNameDisplayLength - name.Length) / 2 + name.Length;
            return name.PadLeft(expectedNameLength, '-') +
                   name.PadRight(expectedNameLength, '-').Substring(name.Length);
        }
    }

    public enum ProjectType : byte
    {
        Global,
        Custom
    }

    public class Project
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public ProjectType ProjectType { get; set; }
        public Guid? ParentProjectId { get; set; }
    }
}