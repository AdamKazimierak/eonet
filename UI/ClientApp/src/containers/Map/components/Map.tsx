import { DivIcon, marker } from 'leaflet';
import { TileLayer, GeoJSON, MapContainer, } from 'react-leaflet';
import { getIcon } from './iconProvider';
import { FeatureCollection } from 'geojson';
import { useEffect, useRef } from 'react';

interface Props {
    featureCollection: FeatureCollection;
}

export default function Map({ featureCollection }: Props) {
    const lastRun = useRef("");

    useEffect(function forceRefresh() {
        lastRun.current = new Date().toISOString();
    }, [featureCollection]);

    return <MapContainer
        className="map-container"
        center={[0, 0]}
        zoom={4}>
        <TileLayer
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <GeoJSON
            key={lastRun.current}
            data={featureCollection}
            pointToLayer={(features, latlng) => {
                const icon = getIcon(null, features.properties.category?.id)
                const m = marker(latlng);
                return m.setIcon(icon ?? m.getIcon());
            }}
            onEachFeature={(feature, layer) => {
                layer.bindPopup(`${feature.properties.title}`);
            }} />
    </MapContainer>
}