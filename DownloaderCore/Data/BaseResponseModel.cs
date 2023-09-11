using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloaderCore.Data
{
    public class BaseResponseModel
    {

        //  VARIABLES

        public List<string>? ErrorMessages { get; private set; }


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

        #region ERROR MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Add error message. </summary>
        /// <param name="message"> Error message. </param>
        public void AddError(string message)
        {
            if (ErrorMessages == null)
                ErrorMessages = new List<string>();

            ErrorMessages.Add(message);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Remove error message. </summary>
        /// <param name="message"> Error message. </param>
        public void RemoveError(string message)
        {
            if (ErrorMessages?.Any(m => m == message) ?? false)
                ErrorMessages.Remove(message);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Remove error message. </summary>
        /// <param name="errorMessageIndex"> Error message index. </param>
        public void RemoveError(int errorMessageIndex)
        {
            if (ErrorMessages != null && errorMessageIndex >= 0 && errorMessageIndex < ErrorMessages.Count - 1)
                ErrorMessages.RemoveAt(errorMessageIndex);
        }

        #endregion ERROR MANAGEMENT METHODS

    }
}
