import { PDFDownloadLink } from '@react-pdf/renderer';
import GroceryRequestPDF from './GroceryRequestPDF';
import { GroceryRequestDetailModel } from '../../models/GroceryRequestModel';

interface PDFGeneratorProps {
	item: GroceryRequestDetailModel;
}

const PDFGenerator = ({ item }: PDFGeneratorProps) => (
	<div>
		<PDFDownloadLink
			document={<GroceryRequestPDF item={item} />}
			fileName="document.pdf"
		>
			{({ blob, url, loading, error }) =>
				loading ? 'Loading document...' : 'Einkauf als PDF exportieren'
			}
		</PDFDownloadLink>
	</div>
);

export default PDFGenerator;
