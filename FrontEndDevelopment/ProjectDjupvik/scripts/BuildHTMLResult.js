const TITLE = `<h3>Väder</h3>`;
const FORCAST_TODAY = `<h4>Senaste dygnet</h4>`;
const FORCAST_TOMORROW = `<h4>Nästa dygnet</h4>`;
const rowStart = `  <tr>`;
const rowEnd = `  </tr>`;
const coloumStart = `     <td>`;
const coloumEnd = `</td>`;
const TIME = `Kl`;
const TEMP = `Temp`;
const VIND = `Vind`;
const HIMMEL = `Himmel`;

const FORCAST_HOURS = 3;
let resultArray;

/**
 * Start building dynamic web code.
 * @param {2 days of data, array} data 
 */
export function BuildHTMLResult(data) {
    let x = 0;
    let y = FORCAST_HOURS;
    resultArray = new Array();

    BuildTitle();

    data.forEach(element => {
        let z = x % 3;
        if (z == 0) {
            BuildForcastTitle(x);
            BuildTableTop();
            BuildTableHeaders();
        }
        InsertNewRowBegin();
        InsertTimeStamp(element);
        //get weather data
        element.parameters.forEach(element => {
            InsertTemperature(element);
            InsertWindDirection(element);
            InsertWindSpeed(element);
            InsertCloudCover(element);
        });

        InsertNewRowEnd();
        x++;
    });

    BuildTableEnd();
    let result = new Array();
    result = resultArray;
    return result;
}
/**
 * Insert cloud cover
 * @param {object containing cloud cover data} cloudCover 
 */
function InsertCloudCover(cloudCover) {
    let cloudSituation = ['Nearly clear sky', 'Variable cloudiness', 'Half clear sky',
        'Cloudy sky', 'Overcast', 'Fog', 'Light rain showers', 'Moderate rain showers',
        'Heavy rain showers', 'Thunderstorm', 'Light sleet showers', 'Moderate sleet showers',
        'Heavy sleet showers', 'Light snow showers', 'Moderate snow showers', 'Heavy snow showers',
        'Light rain', 'Moderate rain', 'Heavy rain', 'Thunder', 'Light sleet', 'Moderate sleet',
        'Heavy sleet', 'Light snowfall', 'Moderate snowfall', 'Heavy snowfall'
    ];
    if (cloudCover.name == 'Wsymb2') {
        InserColoumnData(cloudSituation[cloudCover.values[0]]);
    }
}
/**
 * Inset wind speed
 * @param {object containing wind speed data} windSpeed 
 */
function InsertWindSpeed(windSpeed) {
    if (windSpeed.name == 'ws') {
        resultArray.push(` (` + Math.round(windSpeed.values[0]) + `) ` + coloumEnd);
    }
}
/**
 * Inset wind direction
 * @param {object containing wind direction data} windDirection 
 */
function InsertWindDirection(windDirection) {
    let direction;

    if (windDirection.name == 'wd') {
        switch (Math.round(windDirection.values[0] / 45)) {
            case 0:
                direction = `&#8595;`;
                break;
            case 1:
                direction = `&#8601;`;
                break;
            case 2:
                direction = `&#8592;`;
                break;
            case 3:
                direction = `&#8598;`;
                break;
            case 4:
                direction = `&#8593;`;
                break;
            case 5:
                direction = `&#8599;`;
                break;
            case 6:
                direction = `&#8594;`;
                break;
            case 7:
                direction = `&#8600;`;
                break;
            case 8:
                direction = `&#8595;`;
                break;
            default:
                break;
        }
        resultArray.push(coloumStart + direction);
    }
}
/**
 * Inset temperature
 * @param {object containing temperature data} temp 
 */
function InsertTemperature(temp) {
    if (temp.name == 't') {
        InserColoumnData(temp.values[0]);
    }
}

function InsertNewRowBegin() {
    resultArray.push(rowStart);
}

function InsertNewRowEnd() {
    resultArray.push(rowEnd);
}

/**
 * Insert the time stamp
 * @param {element object} time 
 */
function InsertTimeStamp(time) {
    let start = time.validTime.search('T');
    let str = time.validTime.substr(start + 1, 2);

    InserColoumnData(str);
}
/**
 * Insert a string as a table coloumn.
 * @param {data as string} str 
 */
function InserColoumnData(str) {
    resultArray.push(coloumStart + str + coloumEnd);
}
/**
 * Set the title for each forcast.
 * @param {Integer to determine forcast day} index 
 */
function BuildForcastTitle(index) {
    if (index == 0) {
        BuildForcast(FORCAST_TODAY);
    } else {
        BuildTableEnd();
        BuildForcast(FORCAST_TOMORROW);
    }
}

function BuildTableHeaders() {
    AddTableHeader(TIME);
    AddTableHeader(TEMP);
    AddTableHeader(VIND);
    AddTableHeader(HIMMEL);
}

function AddTableHeader(header) {
    let _head_start = `<th>`;
    let _head_end = `</th>`;
    resultArray.push(_head_start);
    resultArray.push(header);
    resultArray.push(_head_end);
}
/**
 * Create the table header.
 */
function BuildTableTop() {
    let _table = `<table>`;
    let _table_head = `<thead>`;
    let _table_row = `<tr>`;

    resultArray.push(_table);
    resultArray.push(_table_head);
    resultArray.push(_table_row);
}
/**
 * Create the closing end of the table
 */
function BuildTableEnd() {
    let _table_end = `</table>`;
    let _table_head_end = `</thead>`;
    let _table_row_end = `</tr>`;

    resultArray.push(_table_row_end);
    resultArray.push(_table_head_end);
    resultArray.push(_table_end);
}

/**
 * Add the forcast title.
 * @param {string frocast days} day 
 */
function BuildForcast(day) {
    resultArray.push(day);
}
/**
 * Add a header to the weather wedigt.
 */
function BuildTitle() {
    resultArray.push(TITLE);
}