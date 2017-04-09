using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    public class JobListView:PageViewModel
    {
        public List<Job> Jobs { get; set; }
        public JobListView(int page, int size, int total, List<Job> jobs) : base(page,size,total)
        {
            Jobs = jobs;
        }
    }
}
