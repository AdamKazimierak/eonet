import { DivIcon } from 'leaflet';
import Fire from 'bootstrap-icons/icons/fire.svg';
import Snow from 'bootstrap-icons/icons/snow.svg';
import TemperatureHigh from 'bootstrap-icons/icons/thermometer-high.svg';
import Tsunami from 'bootstrap-icons/icons/tsunami.svg';
import Storm from 'bootstrap-icons/icons/cloud-lightning-rain.svg';
import Radioactive from 'bootstrap-icons/icons/radioactive.svg';
import Drough from 'bootstrap-icons/icons/sun.svg';
import Haze from 'bootstrap-icons/icons/cloud-haze.svg';
import Slide from 'bootstrap-icons/icons/water.svg';

const icons = {
    fire: new DivIcon({
        html: Fire,
        iconSize: [24, 24],
        className: 'icon-container'
    }),
    highTemperature: new DivIcon({
        html: TemperatureHigh,
        iconSize: [24, 24],
        className: 'icon-container'
    }),
    snow: new DivIcon({
        html: Snow,
        iconSize: [24, 24],
        className: 'icon-container'
    }),
    radioactive: new DivIcon({
        html: Radioactive,
        iconSize: [24, 24],
        className: 'icon-container'
    }),
    tsunami: new DivIcon({
        html: Tsunami,
        iconSize: [24, 24],
        className: 'icon-container'
    }),
    haze: new DivIcon({
        html: Haze,
        iconSize: [24, 24],
        className: 'icon-container'
    }),
    slide: new DivIcon({
        html: Slide,
        iconSize: [24, 24],
        className: 'icon-container'
    }),
    drought: new DivIcon({
        html: Drough,
        iconSize: [24, 24],
        className: 'icon-container'
    }),
    storm: new DivIcon({
        html: Storm,
        iconSize: [24, 24],
        className: 'icon-container'
    })
}

export function getIcon(defaultValue: any, categoryId: string): DivIcon {
    switch (categoryId) {
        case "wildfires":
            return icons.fire;
        case "volcanoes":
            return icons.fire;
        case "tempExtremes":
            return icons.highTemperature;
        case "snow":
            return icons.snow;
        case "severeStorms":
            return icons.storm;
        case "seaLakeIce":
            return icons.snow;
        case "manmade":
            return icons.radioactive;
        case "landslides":
            return icons.slide;
        case "floods":
            return icons.tsunami;
        case "earthquakes":
            return icons.fire;
        case "dustHaze":
            return icons.haze;
        case "drought":
            return icons.drought;
        default:
            return defaultValue;
    }
}