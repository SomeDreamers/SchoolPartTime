using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    public class JobListView:PageViewModel
    {
        public List<Job> Jobs { get; set; }

        public List<JobModel> JobModels { get; set; }

        public JobListView(int page, int size, int total, List<Job> jobs) : base(page,size,total)
        {
            Jobs = jobs;
        }

        public JobListView(int page, int size, int total, List<JobModel> jobModels) : base(page, size, total)
        {
            JobModels = jobModels;
        }
    }
}
