using System.Collections.Generic;
using System.Linq;


namespace chkam05.ZtmDataDownloader.Data.Base
{
    public class BaseResponseModel
    {

        //  VARIABLES

        public List<string> ErrorMessages { get; private set; }


        //  GETTERS & SETTERS

        public bool HasErrors
        {
            get => ErrorMessages != null && ErrorMessages.Any();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> BaseResponseModel class constructor. </summary>
        public BaseResponseModel()
        {
            //
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Add error message. </summary>
        /// <param name="message"> Error message. </param>
        public void AddError(string message)
        {
            if (ErrorMessages == null)
                ErrorMessages = new List<string>();

            ErrorMessages.Add(message);
        }

    }
}
