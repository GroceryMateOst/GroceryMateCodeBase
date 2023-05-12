import { PDFDownloadLink } from '@react-pdf/renderer';
import GroceryRequestPDF from './GroceryRequestPDF';
import { GroceryRequestDetailModel } from '../../models/GroceryRequestModel';
import { DownloadOutlined } from '@ant-design/icons';

interface PDFGeneratorProps {
	item: GroceryRequestDetailModel;
}

const PDFGenerator = ({ item }: PDFGeneratorProps) => (
	<div>
		<PDFDownloadLink
			document={<GroceryRequestPDF item={item} />}
			fileName={`Einkauf für ${item.client.user.firstName} ${item.client.user.secondName}.pdf`}
		>
			{({ blob, url, loading, error }) =>
				loading ? (
					'Lade Dokument'
				) : (
					<>
						PDF herunterladen
						<DownloadOutlined className="ml-[2px]" />
					</>
				)
			}
		</PDFDownloadLink>
	</div>
);

export default PDFGenerator;
