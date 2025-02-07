import { createRoot } from 'react-dom/client';
import Map from './containers/Map';
import 'bootstrap/dist/css/bootstrap.min.css';

const rootNode = document.getElementById('root');
const root = createRoot(rootNode);

root.render(<Map />);