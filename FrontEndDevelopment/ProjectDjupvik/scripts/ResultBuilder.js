const rowStart = `  <tr>`;
const rowEnd = `  </tr>`;
const coloumStart = `     <td>`;
const coloumEnd = `</td>`;

export function ResultBuilder(dataObject) {
    let data;

    data = Start(dataObject);
    return data;
}
/**
 * Loop through the data object and create table data.
 * @param {array object} data 
 */
function Start(data) {
    let result = new Array(); //resulting table

    //loop through the data
    data.forEach(element => {
        result.push(rowStart);
        result.join(BuildRow(element, result));
        result.push(rowEnd);
    });
    return result;
}
/**
 * Build a table row by adding colums of data and return an array.
 * @param {a single element containing game result} newRow 
 * @param {array of results} resultArray 
 */
function BuildRow(newRow, resultArray) {
    //split name
    resultArray.push(NameSplit(newRow[0], resultArray));
    //add remaining data
    for (var index = 1; index < newRow.length; index++) {
        resultArray.push(coloumStart + newRow[index] + coloumEnd);
    }
    return resultArray;
}
/**
 * Split the persons name into it components
 * @param {Christian and Surname of a person} name 
 * @param {An array of members} resultArray 
 */
function NameSplit(name, resultArray) {
    resultArray.push(coloumStart + name[0] + coloumEnd +
        coloumStart + name[1] + coloumEnd);
    return resultArray;
}
