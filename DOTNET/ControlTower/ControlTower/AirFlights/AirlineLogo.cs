using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ControlTower.Publishers
{
    class AirlineLogo
    {
        #region Properties
        private int carrierCodeMaxLength;
        private readonly string resourceDirectory = "Resources/";
        public enum CarrierLogosShortCodeEnum { aer, swi, haw, jap, png }

        private Dictionary<CarrierLogosShortCodeEnum, string> listOfCarrierLogos;
        #endregion

        /// <summary>
        /// Is the flight code a recognised carrier.
        /// </summary>
        /// <param name="carrier">Flight code used to determine the carrier</param>
        /// <param name="carrierValue">Recognised carries</param>
        /// <returns>True if the carrier is recognised otherwise false</returns>
        public bool IsValidCarrierShortCode(string carrier, out CarrierLogosShortCodeEnum carrierValue)
        {
            bool isValid = false;
            
            isValid = Enum.TryParse(carrier.ToLower(), true, out carrierValue);
            return isValid;
        }
        /// <summary>
        /// Get a default image for unrecognised carriers.
        /// </summary>
        /// <returns>Carrier image as ImageSource</returns>
        public ImageSource GetStandardImage()
        {
            string path = resourceDirectory + "airPlaneRoute.jpg";
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        /// <summary>
        /// Get the short code from a flight code.
        /// </summary>
        /// <param name="code">The flight code</param>
        /// <returns>Short version of the flight code as a string</returns>
        public string GetShotCode(string code)
        {
            if(code.Length > carrierCodeMaxLength)
            {
                return code.Substring(0, carrierCodeMaxLength);
            }
            return string.Empty;
        }
        /// <summary>
        /// Get the path of the image source for a flight code.
        /// </summary>
        /// <param name="carrierLogosShortCode">The flight code</param>
        /// <param name="imageSource">Carrier image as ImageSource</param>
        /// <returns>True if the flight code is a recognised carrier</returns>
        public bool GetImage(string carrierLogosShortCode, out ImageSource imageSource)
        {
            string path = string.Empty;
            CarrierLogosShortCodeEnum value;
            bool isValid = false;

            bool carrierIsValid = IsValidCarrierShortCode(GetShotCode(carrierLogosShortCode), out value);
            bool pathIsValid = listOfCarrierLogos.TryGetValue(value, out path);

            if (carrierIsValid && pathIsValid)
            {
                path = resourceDirectory + path;
                imageSource = new BitmapImage(new Uri(path , UriKind.Relative));
                isValid = true;
            }
            else
            {
                imageSource = null;
            }
          
            return isValid;
        }
        public AirlineLogo()
        {
            listOfCarrierLogos = new Dictionary<CarrierLogosShortCodeEnum, string>(){
               { CarrierLogosShortCodeEnum.aer, "aerlingus.jpg"},
               { CarrierLogosShortCodeEnum.swi, "airline-logos-swiss.png"},
               { CarrierLogosShortCodeEnum.haw, "hawaiian.jpg"},
               { CarrierLogosShortCodeEnum.jap, "japan.png"},
               { CarrierLogosShortCodeEnum.png ,"PNG.jpg"} };
            //set the max length of the flight short 
            carrierCodeMaxLength = CarrierLogosShortCodeEnum.aer.ToString().Length;
        }
    }
}
