import React, { useEffect, useRef } from "react";
import "./TomTomMap.css"

const TomTomMap = ({ locations, setDistance }) => {
    const mapRef = useRef();
    const ttRef = useRef();
    
  
    useEffect(() => {
      const tt = window.tt;
      const map = tt.map({
        key: process.env.REACT_APP_TOM_TOM_API_KEY,
        container: "map",
        style: "tomtom://vector/1/basic-main",
      });
      // map.addControl(new tt.FullscreenControl())
      // map.addControl(new tt.NavigationControl())
  
      mapRef.current = map;
      ttRef.current = tt;
  
      function createMarkerElement(type) {
        var element = document.createElement("div");
        element.className = "route-marker " + type;
        return element;
      }
  
      function addMarkers(feature) {
        var startPoint, endPoint;
        if (feature.geometry.type === "MultiLineString") {
          startPoint = feature.geometry.coordinates[0][0]; //get first point from first line
          endPoint = feature.geometry.coordinates.slice(-1)[0].slice(-1)[0]; //get last point from last line
        } else {
          startPoint = feature.geometry.coordinates[0];
          endPoint = feature.geometry.coordinates.slice(-1)[0];
        }
  
        new tt.Marker({ element: createMarkerElement("start") })
          .setLngLat(startPoint)
          .addTo(map);
        new tt.Marker({ element: createMarkerElement("finish") })
          .setLngLat(endPoint)
          .addTo(map);
      }
  
      function findFirstBuildingLayerId() {
        var layers = map.getStyle().layers;
        for (var index in layers) {
          if (layers[index].type === "fill-extrusion") {
            return layers[index].id;
          }
        }
  
        throw new Error(
          "Map style does not contain any layer with fill-extrusion type."
        );
      }
  
      map.once("load", function () {
        tt.services
          .calculateRoute({
            key: process.env.REACT_APP_TOM_TOM_API_KEY,
            traffic: false,
            locations: locations,
          })
          .go()
          .then(function (response) {
            var geojson = response.toGeoJson();
            map.addLayer(
              {
                id: "route",
                type: "line",
                source: {
                  type: "geojson",
                  data: geojson,
                },
                paint: {
                  "line-color": "#4a90e2",
                  "line-width": 8,
                },
              },
              findFirstBuildingLayerId()
            );
  
            addMarkers(geojson.features[0]);
            const summary = geojson.features[0].properties.summary;
            setDistance(summary.lengthInMeters/1000.0)
  
            var bounds = new tt.LngLatBounds();
            geojson.features[0].geometry.coordinates.forEach(function (point) {
              bounds.extend(tt.LngLat.convert(point));
            });
            map.fitBounds(bounds, { duration: 0, padding: 5 });
          });
      });
    }, []);
  
    // useEffect(() => {
    //   new ttRef.current.Marker({
    //     color: '#2aceeb',
    //     width: '20',
    //     height: '20'
    //   })
    //   .setLngLat([10.05, 20.42])
    //   .addTo(mapRef.current)
    // }, [])
  
    return (
      <div id="map" ref={mapRef} style={{ display: "flex", height: "100%" }}></div>
    );
};

export default TomTomMap;
//"4.8786,52.3679:4.8798,52.3679"