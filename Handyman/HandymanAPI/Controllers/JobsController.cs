using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
    public class JobsController : ApiController
    {
        
        JobData jobData;

        // GET: Jobs
        [Route("GetJobs")]
        public List<JobModel> GetServices()
        {
           jobData = new JobData();
            return jobData.GetAllJobs();
        }

        // POST: api/Jobs/Job
        [Route("PostJob")]     
        public void Post(JobModel job)
        {
            jobData = new JobData();
            jobData.PostJob(job);
        }

        // GET: api/Jobs
        [Route("api/GetJobCategories")]
        public List<JobCategoryModel> GetJobCategories()
        {
            jobData = new JobData();
            return jobData.GetJobCategories();
        }

        // GET: api/Services/5
        [Route("api/GetJobByCategoryId")]
        public List<JobModel> GetJobByCategoryId(int Id)
        {
            jobData = new JobData();
            return jobData.GetJobsByCategoryId(Id);
        }
    

    }
}