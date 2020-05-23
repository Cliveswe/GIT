///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.Model
{
    class InvoiceAddress
    {
        public enum InvoiceAddressKeyEnum
        {
            CompanyName, ContactPerson, StreetAddress, ZipCode, City, Country
        }

        /// <summary>
        /// Complete address.
        /// </summary>
        public string[] Address
        {
            get {
                return _address;
            }
            private set {
                _address = value;
            }
        }
        private string[] _address;

        /// <summary>
        /// Get the address as a string.
        /// </summary>
        /// <returns>String containing the address.</returns>
        public string GetAddress()
        {
            string res = string.Empty;

            for (int i = 0; i < Address.Length; i++)
            {
                if (Address[i] != null)
                {
                    res += Address[i];
                    if (i < (int)InvoiceAddressKeyEnum.Country)
                    {
                        res += ", ";
                    }
                }

            }
            return res;
        }

        /// <summary>
        /// Add address data to the address object.
        /// </summary>
        /// <param name="keyEnum">InvoiceAddressKeyEnum a key used to insert the data.</param>
        /// <param name="msg">String containing address information.</param>
        public void AddAddressData(InvoiceAddressKeyEnum keyEnum, string msg)
        {
            Address[(int)keyEnum] = msg;
        }
        public InvoiceAddress()
        {
            Address = new string[(int)InvoiceAddressKeyEnum.Country + 1];
        }
    }
}
