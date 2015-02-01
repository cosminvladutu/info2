$(document).ready(function () {

    //Show The Map
    showMap();

    // When The Viewing Window Is Resized
    $(window).resize(function () {
        showMap();
    });

});



function showMap() {

    //If HTML5 Geolocation Is Supported In This Browser
    if (navigator.geolocation) {

        //Use HTML5 Geolocation API To Get Current Position
        navigator.geolocation.getCurrentPosition(function (position) {

            //Get Latitude From Geolocation API
            var latitude = position.coords.latitude;

            //Get Longitude From Geolocation API
            var longitude = position.coords.longitude;

            //Define New Google Map With Lat / Lon
            var coords = new google.maps.LatLng(latitude, longitude);

            //Specify Google Map Options
            var mapOptions = {
                zoom: 16,
                center: coords,
                mapTypeControl: true,
                navigationControlOptions: { style: google.maps.NavigationControlStyle.SMALL }, mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("mapContainer"), mapOptions);
            var marker = new google.maps.Marker({
                position: coords,
                map: map,
                title: "You Are Here!"
            });

        }
        );

    } else {

        //Otherwise - Gracefully Fall Back If Not Supported... Probably Best Not To Use A JS Alert Though :)
        alert("Geolocation API is not supported in your browser.");
    }

}





