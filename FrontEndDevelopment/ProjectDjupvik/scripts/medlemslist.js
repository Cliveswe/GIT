import {
    ResultBuilder
} from './ResultBuilder.js';

function main() {
    //Add member list
    const FILE_PATH_MEMBERS = '../data/medlemslist.txt';
    const MEMBERS_TABLE_ID = document.getElementById('listMemlemar');
    const MEMBERS = 'medlem';
    //Create a custom event.
    var triggerEventMembers = new Event(MEMBERS);
    //Add Styrelsen list
    const FILE_PATH_STYRELSEN = '../data/kontaktuppgifterstyrelsen.txt';
    const STYRELSEN_TABLE_ID = document.getElementById('listStyrelsen');
    const MEMBERS_STYRELSEN = 'styrelsen';
    //Create a custom event.
    var triggerEventStyrelsen = new Event(MEMBERS_STYRELSEN);
   

     /* Start here
     */
    function Start() {
         MakeCollapsible();
        AddMemberListListner(MEMBERS_TABLE_ID, MEMBERS, FILE_PATH_MEMBERS, triggerEventMembers);
        AddMemberListListner(STYRELSEN_TABLE_ID, MEMBERS_STYRELSEN, FILE_PATH_STYRELSEN, triggerEventStyrelsen);
    }
    /**
     * Construct additionl table data.
     * @param {*array of members} data 
     */
    function displayResult(data, TABLE_ID) {
        TABLE_ID.innerHTML += ResultBuilder(data).join(''); //remove the commas that are part of array results;
    }
/**
 * 
 * @param {The target element} TABLE_ID 
 * @param {Event argument for a event} Event_Args 
 * @param {File to open} filePath 
 * @param {*} triggerEvent 
 */
    function AddMemberListListner(TABLE_ID, Event_Args, filePath, triggerEvent) {
        /**
         * Add a listener to the element that will hold a list of menbers.
         */
        TABLE_ID.addEventListener(Event_Args, function (e) {

            var result = null;
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.open("GET", filePath, false);
            xmlhttp.send();
            if (xmlhttp.status == 200) {
                result = xmlhttp.responseText;

            }
            let tmp = result.split('\n');
            let data = new Array();
            let resData = new Array();

            tmp.forEach(element => {
                //split the element
                data = element.split(',');
                //split member name
                data[0] = data[0].split(' ');
                resData.push(data);
            });

            displayResult(resData, TABLE_ID);
        }, false);
        /**
         * Trigger the event by code and not via user interface.
         */
        TABLE_ID.dispatchEvent(triggerEvent);
    }

    /**
     * Collapsible command
     */
    function MakeCollapsible() {
        var coll = document.getElementsByClassName("collapsible");
        var i;
        for (i = 0; i < coll.length; i++) {
            coll[i].addEventListener("click", function () {
                this.classList.toggle("active");
                var content = this.nextElementSibling;
                if (content.style.display === "block") {
                    content.style.display = "none";
                } else {
                    content.style.display = "block";
                }
            });
        }
    }

    Start();
}
main();
