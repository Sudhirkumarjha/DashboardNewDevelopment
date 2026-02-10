namespace VideoAssetManager.DataAccess.Common
{

    /// <summary>    
    /// </summary>
    public class AutoMailerParameters
    {
        private string mstrAutoMailerCode;
        private string[] mstrParameters;

        public AutoMailerParameters()
        {
            //
            // TODO: Add constructor logic here
            //
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

        public string[] Parameters
        {
            get
            {
                return mstrParameters;
            }
            set
            {
                mstrParameters = value;
            }
        }
    }
}