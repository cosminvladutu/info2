var $message_area = jQuery('#results');


jQuery(document).ready(function () {
    $message_area.html('<i>Locating you...</i>  ');
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
        // Find location

        function foundLocation(position) {
            $message_area.children().remove();
            // To see everything available in the position.coords array:
            // for (key in position.coords) {alert(key)}
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;
            var accuracy = position.coords.accuracy;
            // altitude = position.coords.altitude;
            // heading = position.coords.heading;
            // speed = position.coords.speed;
            var map;

            //alert(latitude + ": " + longitude +": " + accuracy );

            // Start bweaver's code
            var centerPosition = new google.maps.LatLng(latitude, longitude);
            var options = {
                zoom: 12,
                center: centerPosition,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            map = new google.maps.Map($('#map')[0], options);

            var marker = new google.maps.Marker({
                position: centerPosition,
                map: map,
                icon: 'http://google-maps-icons.googlecode.com/files/sailboat-tourism.png'
            });

            var circle = new google.maps.Circle({
                center: centerPosition,
                radius: accuracy,
                map: map,
                fillColor: '#0000FF',
                fillOpacity: 0.5,
                strokeColor: '#0000FF',
                strokeOpacity: 1.0
            });



            //set the zoom level to the circle's size
            map.fitBounds(circle.getBounds());
            // End bweaver's code



            // var myOptions = {
            // zoom: 12,
            //   center: new google.maps.LatLng(latitude, longitude),
            //   mapTypeId: 'roadmap'
            // mapTypeId: 'terrain'
            // };
            // map = new google.maps.Map($('#map')[0], myOptions);
            // var myLatlng = new google.maps.LatLng(latitude, longitude);
            // var marker = new google.maps.Marker({
            //    position: myLatlng,
            //   title: "Hello World!"
            // });
            //marker.setMap(map);

            $message_area.append('Your latitude: ' + latitude + ' and longitude: ' + longitude + ' and accuracy: ' + accuracy + ':')
        }, function (error) {
            switch (error.code) {
                case error.TIMEOUT:
                    $message_area.append('Timeout error while finding your location');
                    break;
                case error.POSITION_UNAVAILABLE:
                    $message_area.append('Position unavailable error while finding your location');
                    break;
                case error.PERMISSION_DENIED:
                    $message_area.append('Permission denied error while finding your location');
                    break;
                case error.UNKNOWN_ERROR:
                    $message_area.append('Unknown error while finding your location');
                    break;
            }
        });






    }
});