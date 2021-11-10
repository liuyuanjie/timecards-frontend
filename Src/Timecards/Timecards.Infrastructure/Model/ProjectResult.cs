using System;

namespace Timecards.Infrastructure.Model
{
    public class ProjectResult
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public ProjectType ProjectType { get; set; }
    }
    
    public enum ProjectType : byte
    {
        Global,
        Custom
    }
}