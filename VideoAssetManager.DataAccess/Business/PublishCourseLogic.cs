using VideoAssetManager.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VideoAssetManager.DataAccess.Business
{
    public class PublishCourseLogic
    {       
        public DataSet InsertStageData(int Id, string StageGuid)
        {
            DataAccessPublishCourse objDataAccess = new DataAccessPublishCourse();
            return objDataAccess.InsertStageData(Id, StageGuid);
        }
    }
}