import {
  RemoteDataSMHI
} from './RemoteDataSMHI.js';
import {
  ResultBuilderSMHI
} from './ResultBuilderSMHI.js';
import {
  BuildHTMLResult
}
from './BuildHTMLResult.js';

/**
 * Main function that starts the web application.
 */
function ForcastSMHIRepport() {

  const TABLE_ID = document.getElementById('smhi-widget');
  const URL = 'https://opendata-download-metfcst.smhi.se/';
  const API = 'api/category/pmp3g/version/2/geotype/point/lon/18.1489/lat/57.3081/data.json';

  /**
   * Start the web app with asychronous code.
   */
  async function StartForcastRepport() {
    //get the data from the web site
    let data = await RemoteDataSMHI(URL + API)
      .then(data => {
        return data;
      });
    //asychronous executation done, resume sychronous executation.
    //something went wrong 
    if (data == undefined) {
      console.log('error in function Start()');
      console.log(data);
      return;
    };

    //build the rows and columns for the web app.
    //let resultBuilder = ResultBuilderSMHI(data).join(''); //remove the commas that are part of array results
    let resultBuilder = ResultBuilderSMHI(data);
    let buildHTMLResult = BuildHTMLResult(resultBuilder).join('');
    //update the HTML file
    TABLE_ID.insertAdjacentHTML('afterend', buildHTMLResult, "text/html");
    
  };
  
  StartForcastRepport();
}
ForcastSMHIRepport();
