using HandymanUIApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HandymanUIApp.Controllers
{
    public class Consumer : Controller
    {
      
        protected List<JobModel>? jobs;
      


        // GET: Consumer
        public ActionResult CustomerHome()
        {
            if(jobs == null)
            {
                jobs = LoadJobs();
            }
                
            return View(jobs);
        }
        private List<JobModel> LoadJobs()
        {
            if (jobs == null)
            {  
                jobs = new List<JobModel>
                {
                    new JobModel
                    {
                        Id = 4,
                        JobCategory = "Cleanig",
                        JobDecription = "mach.jpg",
                        JobName = "House Kitchen Cleaning",
                        JobImageUrl = "image"
                    },
                    new JobModel
                    {
                        Id = 5,
                        JobCategory = "Mechanic",
                        JobDecription = "mach.jpg",
                        JobName = "Car enginefixing"
                    },
                    new JobModel
                    {
                       Id = 3,
                        JobCategory = "Electronics",
                        JobDecription = "mach.jpg",
                        JobName = "Car lights fixing",
                        JobImageUrl = "image"
                    },
                    new JobModel
                    {
                        Id = 7,
                        JobCategory = "Gardening",
                        JobDecription = "mach.jpg",
                        JobName = "Grass cuttig",
                        JobImageUrl = "image"
                    }
                };

           }
            return jobs;
        }
        // GET: Consumer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Consumer/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Consumer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Consumer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Consumer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Consumer/Delete/5
        public ActionResult Delete(int id)
        {
            JobModel removedJob = new();
            if (jobs == null)
            {
                jobs = LoadJobs();
            }
            foreach (var j in jobs)
            {

            
                
               if(j.Id==id)
                    removedJob.Id = j.Id;
                    removedJob.JobCategory = j.JobCategory;
                    removedJob.JobName = j.JobName;
                    removedJob.JobDecription = j.JobDecription;   
                    removedJob.JobImageUrl = j.JobImageUrl;   
               return View(removedJob);
            }
           
            return View();
        }

        // POST: Consumer/Delete/5
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id, IFormCollection collection)
        {
            try
            {
                if (jobs == null)
                {
                    jobs = LoadJobs();
                }
                foreach(var j in jobs)
                {
                    if (j.Id == id)
                        jobs.Remove(j);
                }
                return RedirectToAction(nameof(CustomerHome ), jobs);
            }
            catch
            {
                return View();
            }
        }
        
       

    }
}
