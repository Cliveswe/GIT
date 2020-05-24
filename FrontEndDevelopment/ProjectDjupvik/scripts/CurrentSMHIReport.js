import {
    RemoteDataSMHI
} from './RemoteDataSMHI.js';
import * as SMHIData from './ResultBuilderSMHI.js';

/**
 * Main function that start the web application.
 */
function CurrentSMHIReport() {
    const TABLE_ID = document.getElementById('smhi-widget-Current-Conditions');
    const URL = 'https://opendata-download-metfcst.smhi.se/';
    const API = 'api/category/pmp3g/version/2/geotype/point/lon/18.1489/lat/57.3081/data.json';

    /**
     * Start the web app with asychronous code.
     */
    async function StartCurrentConditions() {
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

        //retreive the data required for this app
        let resultFromSMHI;
        let something;
        //get the current weathrer conditions
        resultFromSMHI = SMHIData.ResultBuilderSMHICurrentForcast(data);
        //get the HTML code to insert the current weather conditions
        TABLE_ID.innerHTML = SMHIData.TableData(resultFromSMHI);
    }
    StartCurrentConditions();
}
CurrentSMHIReport();
