(function($) {
    $(function () {
        'use strict';
      

        //MAP
        var allAirports = [];
        var allFlights = [];

        var mapOptions = {
            airports: [],
            originAirport: {}
        };

        var map;

        // svg path for target icon
        var targetSVG = "M9,0C4.029,0,0,4.029,0,9s4.029,9,9,9s9-4.029,9-9S13.971,0,9,0z M9,15.93 c-3.83,0-6.93-3.1-6.93-6.93S5.17,2.07,9,2.07s6.93,3.1,6.93,6.93S12.83,15.93,9,15.93 M12.5,9c0,1.933-1.567,3.5-3.5,3.5S5.5,10.933,5.5,9S7.067,5.5,9,5.5 S12.5,7.067,12.5,9z";
        // svg path for plane icon
        var planeSVG = "m2,106h28l24,30h72l-44,-133h35l80,132h98c21,0 21,34 0,34l-98,0 -80,134h-35l43,-133h-71l-24,30h-28l15,-47";


        $(function () {
            $.getJSON("/Home/GetModel", function (data) {
                allAirports = data.Airports;
                console.log(allAirports);
                allFlights = data.Flights;
                console.log(allFlights);

                getLocation();

            });
        });
       
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(setInitialPosition, showError);
            } else {
                console.log("Geolocation is not supported by this browser.");
            }
        }

        function setInitialPosition(position) {
            var closestAirport = findClosestAirport(position);
            mapOptions.originAirport = closestAirport;
            mapOptions.airports = airportsFrom(closestAirport);
            $("#origin").text(closestAirport.Name);
           
            createMap(mapOptions);
        }

        function airportsFrom(closestAirport) {
            var airports = [];
            for (var i = 0; i < allFlights.length; i++) {
                var flight = allFlights[i];
                if (flight.OriginAirport === closestAirport.Name) {
                    for (var j = 0; j < allAirports.length; j++) {
                        var airport = allAirports[j];
                        if (airport.Name === flight.DestinationAirport) {
                            airports.push(airport);
                        }
                    }
                }
            }
            console.log(airports);
            return airports;
        }

        function findClosestAirport(position) {
            var closestAirport;
            var minDistance = Number.MAX_VALUE;
            for (var i = 0; i < allAirports.length; i++) {
                var airport = allAirports[i];          
                var latitudeDiff = Math.abs(position.coords.latitude - airport.Latitude);
                var longitudeDiff = Math.abs(position.coords.longitude - airport.Longitude);
                var distance = Math.pow(latitudeDiff, 2) + Math.pow(longitudeDiff, 2);
         
                if (distance < minDistance) {
                    minDistance = distance;
                    closestAirport = allAirports[i];
                }
            }
            return closestAirport;
        }

        function showError(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    mapOptions.originAirport.Latitude = 55.7408;
                    mapOptions.originAirport.Longitude = 9.1526;
                    mapOptions.originAirport.Name = "CPH";
                    mapOptions.airports = airportsFrom(mapOptions.originAirport);
                    $("#origin").text(mapOptions.originAirport.Name);
                    createMap(mapOptions);
                    console.log("PERMISSION_DENIED");
                    break;
                case error.POSITION_UNAVAILABLE:
                    mapOptions.originAirport.Latitude = 55.7408;
                    mapOptions.originAirport.Longitude = 9.1526;
                    mapOptions.originAirport.Name = "CPH";
                    mapOptions.airports = airportsFrom(mapOptions.originAirport);
                    $("#origin").text(mapOptions.originAirport.Name);
                    createMap(mapOptions);
                    console.log("POSITION_UNAVAILABLE");
                    break;
                case error.TIMEOUT:
                    mapOptions.originAirport.Latitude = 55.7408;
                    mapOptions.originAirport.Longitude = 9.1526;
                    mapOptions.originAirport.Name = "CPH";
                    mapOptions.airports = airportsFrom(mapOptions.originAirport);
                    $("#origin").text(mapOptions.originAirport.Name);
                    createMap(mapOptions);
                    console.log("TIMEOUT");
                    break;
                case error.UNKNOWN_ERROR:
                    mapOptions.originAirport.Latitude = 55.7408;
                    mapOptions.originAirport.Longitude = 9.1526;
                    mapOptions.originAirport.Name = "CPH";
                    mapOptions.airports = airportsFrom(mapOptions.originAirport);
                    $("#origin").text(mapOptions.originAirport.Name);
                    createMap(mapOptions);
                    console.log("UNKNOWN_ERROR");
                    break;
            }
        }
  
        function createMap(mapOptions) {
            var mapImagesArray = [];
            mapImagesArray.push({
                svgPath: targetSVG,
                title: mapOptions.originAirport.Name,
                latitude: mapOptions.originAirport.Latitude,
                longitude: mapOptions.originAirport.Longitude,
                label: mapOptions.originAirport.Name,
                color:"#fc4a1a",
                selectable: true
            });
            for (var index = 0; index < mapOptions.airports.length; index++) {
                var airport = mapOptions.airports[index];

                var mapImage = {
                    svgPath: targetSVG,
                    title: airport.Name,
                    latitude: airport.Latitude,
                    longitude: airport.Longitude,
                    label: airport.Name,
                    selectable: true,
                    
                }
                mapImagesArray.push(mapImage);
            }

            map = AmCharts.makeChart("mapdiv", {
                "type": "map",
                "dataProvider": {
                    "map": "worldLow",
                    zoomLevel: 5,
                    zoomLongitude: mapOptions.originAirport.Longitude,
                    zoomLatitude: mapOptions.originAirport.Latitude,

                    "getAreasFromMap": true,
                    images: mapImagesArray
                },

                "areasSettings": {
                    "autoZoom": false,
                    "selectedColor": "#CC0000",
                    color:"#f7b733"
                },

                imagesSettings: {
                    color: "#585869",
                    rollOverColor: "#4abdac",
                    selectedColor: "#4abdac",
                    pauseDuration: 0.2,
                    animationDuration: 2.5,
                    adjustAnimationSpeed: true
                },
                linesSettings: {
                    color: "#585869",
                    alpha: 0.4
                },
                mouseWheelZoomEnabled: true
            });
            map.addListener("clickMapObject", handleMapObjectClick);
        }
        function handleMapObjectClick(event) {
            if (event.mapObject.cname === "MapImage") {
                var latitude = event.mapObject.latitude;
                var longitude = event.mapObject.longitude;

                var planes = [{
                    svgPath: planeSVG,
                    positionOnLine: 0,
                    color: "#000000",
                    alpha: 0.1,
                    animateAlongLine: true,
                    lineId: "line2",
                    flipDirection: true,
                    loop: true,
                    scale: 0.02,
                    positionScale: 1.2
                },
                    {
                        svgPath: planeSVG,
                        positionOnLine: 0,
                        color: "#585869",
                        animateAlongLine: true,
                        lineId: "line1",
                        flipDirection: true,
                        loop: true,
                        scale: 0.02,
                        positionScale: 1.5
                    }
                ];

                var count = 0;
                for (var index = 0; index < map.dataProvider.images.length; index++) {
                    var element = map.dataProvider.images[index];
                    if (element.svgPath === planeSVG) {
                        count++;
                    }
                }
                if (count === 0) {
                    map.dataProvider.images = map.dataProvider.images.concat(planes);
                }

                map.dataProvider.lines = [{
                    id: "line1",
                    arc: -0.85,
                    alpha: 0.3,
                    latitudes: [mapOptions.originAirport.Latitude, latitude],
                    longitudes: [mapOptions.originAirport.Longitude, longitude]
                }, {
                    id: "line2",
                    alpha: 0,
                    color: "#000000",
                    latitudes: [mapOptions.originAirport.Latitude, latitude],
                    longitudes: [mapOptions.originAirport.Longitude, longitude]
                }];

                map.validateData();
            }
        }

        $("#sun").click(function () {
            $("#sun").css("background-image", "url(../Content/sun_white.png)");
            $("#all").css("background-image", "url(../Content/network.png)");
            $("#snow").css("background-image", "url(../Content/snow.png)");
            $("#city").css("background-image", "url(../Content/city.png)");



            console.log("clicks");
            var mapImagesArray = [];
            mapImagesArray.push({
                svgPath: targetSVG,
                title: mapOptions.originAirport.Name,
                latitude: mapOptions.originAirport.Latitude,
                longitude: mapOptions.originAirport.Longitude,
                label: mapOptions.originAirport.Name,
                color: "#fc4a1a",
                selectable: true
            });
            for (var index = 0; index < mapOptions.airports.length; index++) {
                var airport = mapOptions.airports[index];
                console.log(airport)
                for (var i = 0; i < airport.Filters.length; i++) {
                    var filter = airport.Filters[i];
                    if (filter === "Sun & sea") {
                        var mapImage = {
                            svgPath: targetSVG,
                            title: airport.Name,
                            latitude: airport.Latitude,
                            longitude: airport.Longitude,
                            label: airport.Name,
                            selectable: true
                        }
                        mapImagesArray.push(mapImage);
                    }
                }
               
            }
            map.dataProvider.images = mapImagesArray;
            map.dataProvider.lines = [];
            map.validateData();
        });

        $("#snow").click(function () {
            $("#sun").css("background-image", "url(../Content/sun.png)");
            $("#all").css("background-image", "url(../Content/network.png)");
            $("#snow").css("background-image", "url(../Content/snow_white.png)");
            $("#city").css("background-image", "url(../Content/city.png)");



            console.log("clicks");
            var mapImagesArray = [];
            mapImagesArray.push({
                svgPath: targetSVG,
                title: mapOptions.originAirport.Name,
                latitude: mapOptions.originAirport.Latitude,
                longitude: mapOptions.originAirport.Longitude,
                label: mapOptions.originAirport.Name,
                color: "#fc4a1a",
                selectable: true
            });
            for (var index = 0; index < mapOptions.airports.length; index++) {
                var airport = mapOptions.airports[index];
                console.log(airport)
                for (var i = 0; i < airport.Filters.length; i++) {
                    var filter = airport.Filters[i];
                    if (filter === "Snow & ski") {
                        var mapImage = {
                            svgPath: targetSVG,
                            title: airport.Name,
                            latitude: airport.Latitude,
                            longitude: airport.Longitude,
                            label: airport.Name,
                            selectable: true
                        }
                        mapImagesArray.push(mapImage);
                    }
                }

            }
            map.dataProvider.lines = [];
            map.dataProvider.images = mapImagesArray;
            map.validateData();
        });

        $("#city").click(function () {
            $("#sun").css("background-image", "url(../Content/sun.png)");
            $("#all").css("background-image", "url(../Content/network.png)");
            $("#snow").css("background-image", "url(../Content/snow.png)");
            $("#city").css("background-image", "url(../Content/city_white.png)");
            console.log("clicks");
            var mapImagesArray = [];
            mapImagesArray.push({
                svgPath: targetSVG,
                title: mapOptions.originAirport.Name,
                latitude: mapOptions.originAirport.Latitude,
                longitude: mapOptions.originAirport.Longitude,
                label: mapOptions.originAirport.Name,
                color: "#fc4a1a",
                selectable: true
            });
            for (var index = 0; index < mapOptions.airports.length; index++) {
                var airport = mapOptions.airports[index];
                console.log(airport)
                for (var i = 0; i < airport.Filters.length; i++) {
                    var filter = airport.Filters[i];
                    if (filter === "Big city") {
                        var mapImage = {
                            svgPath: targetSVG,
                            title: airport.Name,
                            latitude: airport.Latitude,
                            longitude: airport.Longitude,
                            label: airport.Name,
                            selectable: true
                        }
                        mapImagesArray.push(mapImage);
                    }
                }

            }
            map.dataProvider.images = mapImagesArray;
            map.dataProvider.lines = [];
            map.validateData();
        });

        $("#all").click(function () {
            $("#sun").css("background-image", "url(../Content/sun.png)");
            $("#all").css("background-image", "url(../Content/network_white.png)");
            $("#snow").css("background-image", "url(../Content/snow.png)");
            $("#city").css("background-image", "url(../Content/city.png)");


            console.log("clicks");
            var mapImagesArray = [];
            mapImagesArray.push({
                svgPath: targetSVG,
                title: mapOptions.originAirport.Name,
                latitude: mapOptions.originAirport.Latitude,
                longitude: mapOptions.originAirport.Longitude,
                label: mapOptions.originAirport.Name,
                color: "#fc4a1a",
                selectable: true
            });
            for (var index = 0; index < mapOptions.airports.length; index++) {
                var airport = mapOptions.airports[index];
                console.log(airport)
                for (var i = 0; i < airport.Filters.length; i++) {
                    var filter = airport.Filters[i];
                    if (filter === "Everywhere") {
                        var mapImage = {
                            svgPath: targetSVG,
                            title: airport.Name,
                            latitude: airport.Latitude,
                            longitude: airport.Longitude,
                            label: airport.Name,
                            selectable: true
                        }
                        mapImagesArray.push(mapImage);
                    }
                }

            }
            map.dataProvider.images = mapImagesArray;
            map.dataProvider.lines = [];
            map.validateData();
        });

    });
})(jQuery);