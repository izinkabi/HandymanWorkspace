﻿using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.DataAccess
{
    public class ServiceData
    {
       public List<ServiceModel> GetAllServices()
        {
            SQLDataAccess sql = new SQLDataAccess();
            var _output = sql.LoadData<ServiceModel, dynamic>("Service.spGetAllServices", new { }, "HandymanDB");
            return _output;
        }

        ////Getting all Jobs
        //public List<JobModel> GetAllJobs()
        //{
        //    SQLDataAccess sql = new SQLDataAccess();

        //    var output = sql.LoadData<JobModel, dynamic>("Service.spGetAllJobs", new { }, "HandymanDB");

        //    return output;

        //}

        ////Get Jobs by Category Id
        //public List<JobModel> GetJobsByCategoryId(int Id)
        //{
        //    SQLDataAccess sql = new SQLDataAccess();

        //    var p = new { CategoryID = Id };

        //    var output = sql.LoadData<JobModel, dynamic>("Service.spGetJobByCategoryId", p, "HandymanDB");

        //    return output;

        //}

        ////Posting a new Job
        //public void PostJob(JobModel job)
        //{
        //    SQLDataAccess sql = new SQLDataAccess();
        //    var j = new { Title = job.Title, JobPosition = job.JobPosition, CategoryID = job.CategoryID, JobImage = job.JobImage };
        //    sql.SaveData("Service.spServiceInsert", j, "HandymanDB");
        //}

        ////Get all Job Categories
        //public List<JobCategoryModel> GetJobCategories()
        //{
        //    SQLDataAccess sql = new SQLDataAccess();
        //    var output = sql.LoadData<JobCategoryModel, dynamic>("Service.spGetAllJobCategories", new { }, "HandymanDB");
        //    return output;
        //}
    }
}
