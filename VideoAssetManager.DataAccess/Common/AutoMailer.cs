namespace VideoAssetManager.DataAccess.Common
{
    /// <summary>    
    /// </summary>
    public class AutoMailer
    {
        private string mstrAutoMailerCode;
        private string mstrAutoMailerSubject;
        private string mstrAutoMailerBody;
        private bool mbCcAdmin;
        private bool mbBccAdmin;
        private string mstrCcList;
        private string mstrBccList;
        private bool mbAutoMailerEnabled;
        private bool mbOutlookMeetingMessage;

        public AutoMailer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool OutlookMeetingMessage
        {
            get
            {
                return mbOutlookMeetingMessage;
            }
            set
            {
                mbOutlookMeetingMessage = value;
            }
        }

        public string AutoMailerCode
        {
            get
            {
                return mstrAutoMailerCode;
            }
            set
            {
                mstrAutoMailerCode = value;
            }
        }

        public string AutoMailerSubject
        {
            get
            {
                return mstrAutoMailerSubject;
            }
            set
            {
                mstrAutoMailerSubject = value;
            }
        }

        public string AutoMailerBody
        {
            get
            {
                return mstrAutoMailerBody;
            }
            set
            {
                mstrAutoMailerBody = value;
            }
        }

        public bool CcAdmin
        {
            get
            {
                return mbCcAdmin;
            }
            set
            {
                mbCcAdmin = value;
            }
        }

        public bool BccAdmin
        {
            get
            {
                return mbBccAdmin;
            }
            set
            {
                mbBccAdmin = value;
            }
        }

        public string CcList
        {
            get
            {
                return mstrCcList;
            }
            set
            {
                mstrCcList = value;
            }
        }

        public string BccList
        {
            get
            {
                return mstrBccList;
            }
            set
            {
                mstrBccList = value;
            }
        }

        public bool AutoMailerEnabled
        {
            get
            {
                return mbAutoMailerEnabled;
            }
            set
            {
                mbAutoMailerEnabled = value;
            }
        }
    }
}