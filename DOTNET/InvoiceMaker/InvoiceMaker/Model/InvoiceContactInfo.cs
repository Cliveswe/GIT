///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.Model
{
    class InvoiceContactInfo
    {
        /// <summary>
        /// URL
        /// </summary>
        public string URL
        {
            get {
                return _url;
            }
            private set {
                _url = value;
            }
        }
        private string _url;
        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber
        {
            get {
                return _phoneNumber;
            }
            private set {
                _phoneNumber = value;
            }
        }
        private string _phoneNumber;
        /// <summary>
        /// Add a phone number.
        /// </summary>
        /// <param name="msg">String containing a phone number.</param>
        public void AddPhoneNumber(string msg)
        {
            PhoneNumber = msg;
        }
        /// <summary>
        /// Add the web address (URL).
        /// </summary>
        /// <param name="msg">String containing the web address</param>
        public void AddURL(string msg)
        {
            URL = msg;
        }
        public InvoiceContactInfo()
        {
            URL = string.Empty;
            PhoneNumber = string.Empty;
        }
    }
}
