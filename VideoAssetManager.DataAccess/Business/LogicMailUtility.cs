
using System.Data;
using System.Threading.Tasks;

using System.Threading;
using System.Collections;
using System;
using VideoAssetManager.DataAccess.Common;
using System.Collections.Generic;
using VideoAssetManager.CommonUtils;

namespace VideoAssetManager.Business
{
    public partial class LogicMailUtility
    {
        public List<AutoMailer> GetAutoMailerConfiguration()
        {
                     DataAccessMailUtility objData = new DataAccessMailUtility();

                    var objAutoMailersDataSet = objData.GetAutoMailer();                  

                    var nLoopCounter = objAutoMailersDataSet.Tables[0].Rows.Count;                 

                    List<AutoMailer> autoMailerList = new List<AutoMailer>();

                    for (int i = 0; i < nLoopCounter; i++)
                    {                
                        AutoMailer autoMailer = new AutoMailer
                        {
                            AutoMailerCode = (string)objAutoMailersDataSet.Tables[0].Rows[i]["AutomailerCode"],
                            AutoMailerSubject = (string)objAutoMailersDataSet.Tables[0].Rows[i]["AutomailerSubject"],
                            AutoMailerBody = (string)objAutoMailersDataSet.Tables[0].Rows[i]["AutomailerBody"],
                            CcAdmin = (bool)objAutoMailersDataSet.Tables[0].Rows[i]["IsCCAdmin"],
                            BccAdmin = (bool)objAutoMailersDataSet.Tables[0].Rows[i]["IsBCCAdmin"],
                            OutlookMeetingMessage =false// (bool)objAutoMailersDataSet.Tables[0].Rows[i]["IsOutlookMeetingMessage"]

                        };
                        autoMailerList.Add(autoMailer);
                        }
            return autoMailerList;
        }

        public List<AutoMailerParameters> GetAutoMailerParameters()
        {
            DataAccessMailUtility objData = new DataAccessMailUtility();

            DataSet objParameters = objData.GetAllAutoMailersParameters();

            objData = null;

            var nLoopCounter = objParameters.Tables[0].Rows.Count;

            List<AutoMailerParameters> parametersList = new List<AutoMailerParameters>();

            for (int i = 0; i < nLoopCounter; i++)
            {
                AutoMailerParameters parameters = new AutoMailerParameters()
                {

                    AutoMailerCode = (string)objParameters.Tables[0].Rows[i]["AutomailerCode"],
                    Parameters = RekhtaUtility.Split((string)objParameters.Tables[0].Rows[i]["ParameterList"], ",")
                };
            parametersList.Add(parameters);
            }

            return parametersList;
        }
    }
}