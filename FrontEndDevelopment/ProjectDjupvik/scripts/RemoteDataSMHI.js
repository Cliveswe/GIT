/**
 * Get the data from a server and return the result as a json object otherwise undefined.
 * @param {web addres} url 
 */
export function RemoteDataSMHI(url) {
    // get data from a web site using fetch. 
    return fetch(url)
        .then((responce) => { //use promise to check status of the resulting fetch 
            if (responce.status == 200) {//HTTP status 200
                return responce.json(); //return json object
            } else {
                return 'undefined'; //something went wrong return undefine
            }
        });
}