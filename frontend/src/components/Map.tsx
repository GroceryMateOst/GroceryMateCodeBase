import 'leaflet/dist/leaflet.css';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';

interface MapProps {
	latitude: number;
	longitude: number;
	height: string;
	width: string;
	zoom?: number;
}

const Map = ({ latitude, longitude, height, width, zoom }: MapProps) => {
	return (
		<MapContainer
			center={[latitude, longitude]}
			zoom={zoom ?? 17}
			scrollWheelZoom={true}
			style={{ height, width }}
		>
			<TileLayer
				attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
				url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
			/>
			<Marker position={[latitude, longitude]}>
				<Popup>
					A pretty CSS3 popup. <br /> Easily customizable.
				</Popup>
			</Marker>
		</MapContainer>
	);
};

export default Map;
