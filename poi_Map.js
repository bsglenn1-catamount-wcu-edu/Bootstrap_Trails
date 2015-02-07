var myMap;
var currentPositionMarker;
var currentPositionAccuracyCircle;
var listOfPaths = [];
var geoXmlDoc;

//The ready function
$(document).ready(function () {

    console.log("Inside ready function");

    //drawMap();
    drawScreen();
    StartCurrentPositionWatcher();

});


//---------------------------------------------------------
// name: drawMap()
//---------------------------------------------------------
function drawMap(parLat, parLong) {

    console.log("Inside drawMap function");

    //Create an object to hold the map center coordinates
    //Note: Asheville
    var mapCenterCoords = new google.maps.LatLng(parLat, parLong);

    //Create an object to hold our map options
    var mapOptions = {
        center: mapCenterCoords,
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    //Create the map
    myMap = new google.maps.Map(document.getElementById("divMapCanvas"),
                                            mapOptions);
}

//Name: drawScreen()
function drawScreen() {
    console.log("Insise drawScreen function");

    var lat = 35.310581;
    var long = -83.193939;

    drawMap(lat, long);

    loadListOfPath();
   
}

//Name: loadListOfPath
function loadListOfPath() {
    console.log("Inside loadListOfPath");
    var pathFile, pathURL;
    pathFile = getQuerystring('qryPathFile');
    console.log("pathFile =" + pathFile);
    pathURL = "Paths/" + pathFile + ".js";

    $.ajax({
        type: "GET",
        url: pathURL,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: cb_LoadListOfPaths_Success,
        error: cb_LoadListOfPaths_Error
    })
}

//name: cb_LoadListOfPaths_Success
function cb_LoadListOfPaths_Success(parListOfPaths) {
    console.log("---------------------------------------");
    console.log("Inside cb_LoadListOfPaths_Success SUCCESS CALLBACK function");
    console.log("---------------------------------------");

    //Add tracks to map
    addPathsToMap(parListOfPaths);
}

//name: cb_LoadListOfPaths_Error
function cb_LoadListOfPaths_Error() {
    console.log("ERROR LOADING LIST OF PATHS DATA");
}

//Name: addPathsToMap
function addPathsToMap(parListOfPaths) {

    var pathID;
    listOfPaths = parListOfPaths;
    console.log("listOfPaths = " + listOfPaths);

    for (var i = 0; i < listOfPaths.length; i++) {
        pathID = listOfPaths[i].PathID;
        console.log("pathID = " + pathID);

    }
    var myParser = new geoXML3.parser({ map: myMap, afterParse: cb_KMLDataParsed });
    myParser.parse('KML/CampusWalking.kml');
    console.dir(myParser);
    console.dir(myParser.options);
    console.dir(myParser.docs);
}

//name: cb_KMLDataParsed
function cb_KMLDataParsed(doc) {

    geoXmlDoc = doc[0];

    console.dir(doc[0]);
    console.log("doc[0].baseUrl = " + doc[0].baseUrl);
    console.dir(doc[0].placemarks);
    console.log("doc[0].placemarks[0].description = " + doc[0].placemarks[0].description);
    console.log("doc[0].placemarks[0].name = " + doc[0].placemarks[0].name);
    console.log("doc[0].placemarks[1].name = " + doc[0].placemarks[1].name);
    console.log("doc[0].placemarks[2].name = " + doc[0].placemarks[2].name);
    console.log("doc[0].placemarks[3].name = " + doc[0].placemarks[3].name);
    console.log("doc[0].placemarks[4].name = " + doc[0].placemarks[4].name);
    console.log("doc[0].placemarks[5].name = " + doc[0].placemarks[5].name);
    console.dir(doc[0].placemarks[0].LineString[0].coordinates);
    console.dir(doc[0].placemarks[1].polyline);
    console.log(doc[0].placemarks[1].polyline.strokeWeight);

    for (var n = 0; n < listOfPaths.length; n++) {
        console.log("inside first loop");
        var pathID;
        pathID = listOfPaths[n].PathID;
        console.log("pathID from array =" + pathID);

        for (var i = 0; i < doc[0].placemarks.length; i++) {
            console.log("inside second loop");

            if (doc[0].placemarks[i].name == pathID) {
                console.log("pathID = " + pathID);
                console.log("doc[0].placemarks[i].name =" + doc[0].placemarks[i].name);

                doc[0].placemarks[i].polyline.strokeWeight = 2;
                doc[0].placemarks[i].polyline += doc[0].placemarks[i].polyline;
            }

            else {
                doc[0].placemarks[i].polyline.strokeWeight = 0;
            }
        }
    }
}

//========================
//Name: StartCurrentPositionWatcher
//===================================
function StartCurrentPositionWatcher() {

    //Check that browser supports geolocation
    //If it doesn't, display message and stop any more current position code from running
    if (!navigator.geolocation) {
        alert("GeoLocation info not available");
        return;
    }

    //Get current position -- whenever position changes -- 
    //and pass it to callback function
    navigator.geolocation.watchPosition(cb_UpdateCurrentPositionMarker,
                                            cb_UpdateCurrentPositionMarker_Error,
                                            {
                                                enableHighAccuracy: true,
                                                maximumAge: 1000 //Retrieve new info if older than 5 seconds 
                                            }
                                            );
}


//=================================
// Name: cb_UpdateCurrentPositionMarker
//==================================
function cb_UpdateCurrentPositionMarker(positionObject) {

    var lat = positionObject.coords.latitude;
    var long = positionObject.coords.longitude;
    var myLatlng = new google.maps.LatLng(lat, long);
    var accuracy = positionObject.coords.accuracy;
    console.log("Current location: (" + lat + ", " + long + "), " +
                                 "Accuracy: " + accuracy + " meters");



    //Clear old marker (if exists)
    if (currentPositionMarker) {
        currentPositionMarker.setMap(null);
    }

    //Add marker to map
    currentPositionMarker = new google.maps.Marker({
        position: myLatlng,
        map: myMap,
        icon: 'Images/pin_red.png'
    });

    var displayContent = "Accuracy:  " + accuracy + " meters.";

    //Set content of infoWindow
    var infoWindow = new google.maps.InfoWindow({ content: displayContent });

    //Add event handler to display infoWindo when user clicks on marker
    google.maps.event.addListener(currentPositionMarker, 'click',
                                             function () {
                                                 infoWindow.open(myMap, currentPositionMarker);
                                             });

    // Construct the circle
    var currentPositionAccuracyCircleOptions = {
        strokeColor: "#3399FF",
        strokeOpacity: 0.7,
        strokeWeight: 1,
        fillColor: "#CCFFFF",
        fillOpacity: 0.35,
        map: myMap,
        center: myLatlng,
        radius: accuracy
    };

    //Clear old circle (if exists)
    if (currentPositionAccuracyCircle) {
        currentPositionAccuracyCircle.setMap(null);
    }
    currentPositionAccuracyCircle = new google.maps.Circle(currentPositionAccuracyCircleOptions);

}

//=====================================
// Name:            cb_UpdateCurrentPositionMarker_Error
// Description:     Callback function: 
//=====================================
function cb_UpdateCurrentPositionMarker_Error(err) {

    var errorMessage;

    if (err.code == 1) {
        errorMessage = "You chose not to share your location info.";
    }
    else if (err.code == 2) {
        errorMessage = "Location information currently unavailable!";
    }
    else if (err.code == 3) {
        errorMessage = "Timed out waiting to receive location information!";
    }
    else {
        errorMessage = "Unknown error occured";
    }
}

//--------------------------------------------------------------------
// Name:  getQuerystring Function
// Description: gets the value equal to "poi" in the url and returns 
// an integer. Additional info about this function can be viewed at
// http://www.bloggingdeveloper.com/post/javascript-querystring-parse
// Get-QueryString-with-Client-Side-JavaScript.aspx
//--------------------------------------------------------------------
function getQuerystring(key, default_) {
    if (default_ === null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs === null) {
        return default_;
    } else {
        return qs[1];
    }
}