using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;


namespace VideoAssetManager.Business
{
   
    public class DataAccessMailUtility : DataAccess
    {
        public DataAccessMailUtility()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataSet GetAutoMailer()
        {
            return ExecuteQuery("SP_GetAllAutoMailers", null);
        }

        public DataSet GetAllAutoMailersParameters()
        {
            return ExecuteQuery("SP_GetAutoMailersParameters", null);
        }

      
    
    }
}
