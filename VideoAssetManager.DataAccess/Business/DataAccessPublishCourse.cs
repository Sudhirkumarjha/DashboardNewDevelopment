using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace VideoAssetManager.Business
{
    public class DataAccessPublishCourse : DataAccess
    {
        
        public DataSet InsertStageData(int Id, string StageGuid)
        {
            List<DbParameter> objGenericList = new List<DbParameter>();

            DbParameter objParam = NewParameter();
            objParam.ParameterName = "@Id";
            objParam.DbType = DbType.Int32;
            objParam.Value = Id;
            objGenericList.Add(objParam);

            objParam = NewParameter();
            objParam.ParameterName = "@StageGuid";
            objParam.DbType = DbType.String;
            objParam.Value = StageGuid;
            objGenericList.Add(objParam);

            return ExecuteQuery("Sp_InsertStageLookup", objGenericList.ToArray());
        }
    }
}