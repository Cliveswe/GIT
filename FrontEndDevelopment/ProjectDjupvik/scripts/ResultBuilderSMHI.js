const FIRST_TIME = '06';
const SECOND_TIME = '12';
const THIRD_TIME = '18';
const DELIMITER = 'T';
const MAX_NUMBER_OF_DATA_POINTS = 6;
const MAX_SERIES = 2;
const tableStart = `<div class="responsive-table">`;
const tableEnd = `</div>`;
const rowStart = `<p class="table-row">`;
const rowEnd = `</p>`;
const columnHeaderStart = `<p class="table-column-header">`;
const columnHeaderEnd = `</p>`;
const coloumStart = `<section class="table-column">`;
const coloumEnd = `</section>`;
const TITLE = "Väder:";
const TIME = `Kl`;
const TEMP = `Temp`;
const VIND = `Vind`;
const HIMMEL = `Himmel`;

/**
 * Object to hold weather conditions
 */
class CurrentConditions {

    constructor() {}
    set timeStamp(timeStamp) {
        this._timeStamp = timeStamp;
    };
    get timeStamp() {
        return this._timeStamp;
    }
    setTemperature(temperature) {
        this.temperature = temperature;
    };
    setWindDirection(windDirection) {
        this.windDirection = windDirection;
    };
    setWindSpeed(windSpeed) {
        this.windSpeed = windSpeed;
    };
    setCloudCover(cloudCover) {
        this.cloudCover = cloudCover;
    };
    getTemperature() {
        return Math.round(this.temperature);
    };
    getHour() {
        var index = this.timeStamp.search('T');
        return this.timeStamp.substr(index + 1, 2);
    }
    getCurrentWindDirectionAsASCIICharacter() {
        let direction = ``;
        switch (Math.round(this.windDirection / 45)) {
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
        return direction;
    };

    /**
     * Insert cloud cover
     * @param {object containing cloud cover data} cloudCover 
     */
    getCloudCover() {
        let cloudSituation = ['Nearly clear sky', 'Variable cloudiness', 'Half clear sky',
            'Cloudy sky', 'Overcast', 'Fog', 'Light rain showers', 'Moderate rain showers',
            'Heavy rain showers', 'Thunderstorm', 'Light sleet showers', 'Moderate sleet showers',
            'Heavy sleet showers', 'Light snow showers', 'Moderate snow showers', 'Heavy snow showers',
            'Light rain', 'Moderate rain', 'Heavy rain', 'Thunder', 'Light sleet', 'Moderate sleet',
            'Heavy sleet', 'Light snowfall', 'Moderate snowfall', 'Heavy snowfall'
        ];

        //return InsertColoumnData(cloudSituation[this.cloudCover]);
        return cloudSituation[this.cloudCover];
    };
    getWindConditionsAsTableData() {
        let str;
        let speed = Math.round(this.windSpeed);
        str = `(` + speed + `)`;
        return `${str}`;
    };

    /**
     * Print the current state of the object.
     */
    ToString() {
        console.log(`date: ${this.timeStamp}, temp: ${this.temperature}, direction: ${this.windDirection}, speed: ${this.windSpeed}, cloudCover:  ${this.cloudCover}.`);
    };
}

/**
 * Generate HTML code of weather contitions.
 * Note: this is not a table but a grouing of <section> for columns 
 * and <p> for rows to simulate a table. A better solution for responsiven design.
 * @param {Object containg werater data} weatherConditions 
 */
export var TableData = function GetCurrentConditionsAsTableElement(weatherConditions) {
    let str = tableStart + TITLE + coloumStart; //time
    str += columnHeaderStart + TIME + columnHeaderEnd;
    str += rowStart + weatherConditions.getHour() + rowEnd;
    str += coloumEnd;
    str += coloumStart; //temperature
    str += columnHeaderStart + TEMP + columnHeaderEnd;
    str += rowStart + weatherConditions.getTemperature() + rowEnd;
    str += coloumEnd;
    str += coloumStart; //special case for wind
    str += columnHeaderStart + VIND + columnHeaderEnd;
    str += rowStart + weatherConditions.getCurrentWindDirectionAsASCIICharacter();
    str += ` `;
    str += weatherConditions.getWindConditionsAsTableData() + rowEnd;
    str += coloumEnd;
    str += coloumStart; //cloud cover
    str += columnHeaderStart + HIMMEL + columnHeaderEnd;
    str += rowStart + weatherConditions.getCloudCover() + rowEnd;
    str += coloumEnd + tableEnd;

    return str;
}
/**
 * Get the latest weather conditions from SMHI data.
 */
export function ResultBuilderSMHICurrentForcast(dataObject) {
    let data = dataObject.timeSeries[0].parameters;
    let currentConditions = new CurrentConditions();

    currentConditions.timeStamp = dataObject.timeSeries[0].validTime;
    data.forEach(CurrentConditions => {

        if (CurrentConditions.name == 't') {
            currentConditions.setTemperature(CurrentConditions.values[0]);
        }
        if (CurrentConditions.name == 'wd') {
            currentConditions.setWindDirection(CurrentConditions.values[0]);
        }
        if (CurrentConditions.name == 'ws') {
            currentConditions.setWindSpeed(CurrentConditions.values[0]);
        }
        if (CurrentConditions.name == 'Wsymb2') {
            currentConditions.setCloudCover(CurrentConditions.values[0]);
        }
    });

    return currentConditions;
}
/**
 * Build the row and columns for the results form tipset.
 * @param {json object containg tipset results} dataObjct 
 */
export function ResultBuilderSMHI(dataObjct) {
    let data;

    data = Start(dataObjct);

    return data;
}
/**
 * Start building the result.
 * @param {*json data} data 
 */
function Start(data) {
    let res = new Array();
    res = data.timeSeries;
    let newData = GetDaysAndHours(res, DELIMITER);
    newData.length = MAX_NUMBER_OF_DATA_POINTS;
    return newData;

}

/**
 * Get the forcast for 06, 12 and 18 hours. For all days.
 * @param {jason data} data 
 * @param {delimiter 'T'} v 
 */
function GetDaysAndHours(data, v) {
    let getSerie = MAX_SERIES;
    let start;
    let res = new Array();
    data.forEach(element => {
        start = element.validTime.search(v);
        let str = element.validTime.substr(start + 1, getSerie);
        if ((str == FIRST_TIME) ||
            (str == SECOND_TIME) ||
            (str == THIRD_TIME)) {
            res.push(element);
        }
    });
    return res;
}
