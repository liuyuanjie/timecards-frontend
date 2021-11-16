using System;
using System.Collections.Generic;

namespace Timecards.Infrastructure.Model.Response
{
    public class SearchTimecardsResult
    {
        public Guid TimecardsId { get; set; }
        public string ProjectName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public StatusType StatusType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalHour { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public string DisplayStartDate => StartDate.ToString("D");
        public string DisplayEndDate => EndDate.ToString("D");
    }
}