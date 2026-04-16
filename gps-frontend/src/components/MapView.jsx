import {
  MapContainer,
  TileLayer,
  Polyline,
  Marker,
  useMap
} from "react-leaflet";

import { useEffect, useMemo } from "react";
import L from "leaflet";

/* FIX Leaflet icons */
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl:
    "https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon-2x.png",
  iconUrl:
    "https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon.png",
  shadowUrl:
    "https://unpkg.com/leaflet@1.7.1/dist/images/marker-shadow.png"
});

function FitBounds({ positions }) {
  const map = useMap();

  useEffect(() => {
    if (!positions.length) return;

    const bounds = L.latLngBounds(positions);
    map.fitBounds(bounds, { padding: [20, 20] });

    setTimeout(() => {
      map.invalidateSize();
    }, 100);
  }, [positions, map]);

  return null;
}

export default function MapView({ track, currentIndex }) {
  const positions = useMemo(() => {
    return track.map(p => [p.lat, p.lon]);
  }, [track]);

  const current = track[currentIndex];

  // ❗ prevent crash
  if (!positions.length) {
    return <div style={{ height: "100%", width: "100%" }} />;
  }

  return (
    <MapContainer
      center={positions[0]}
      zoom={14}
      style={{ height: "100%", width: "100%" }}
    >
      <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />

      <FitBounds positions={positions} />

      <Polyline
        positions={positions}
        color="black"
        weight={4}
      />

      {current && (
        <Marker position={[current.lat, current.lon]} />
      )}
    </MapContainer>
  );
}