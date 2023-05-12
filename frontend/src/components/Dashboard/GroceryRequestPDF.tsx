import { Page, Document, Text, StyleSheet, View } from '@react-pdf/renderer';
import { GroceryRequestDetailModel } from '../../models/GroceryRequestModel';

interface GroceryRequestPDFProps {
	item: GroceryRequestDetailModel;
}

const styles = StyleSheet.create({
	page: {
		marginLeft: 20,
		marginRight: 20,
		marginTop: 40,
	},
	title: {
		fontSize: 35,
		fontWeight: 'bold',
		marginTop: 40,
		textAlign: 'center',
	},
	subtitle: {
		fontSize: 20,
		fontWeight: 'bold',
		marginBottom: 10,
		marginTop: 40,
		marginRight: 40,
		borderBottom: 1,
		paddingBottom: 10,
	},
	checkbox: {
		border: 1,
		width: 20,
		height: 20,
		marginRight: 10,
	},
	grocery: {
		display: 'flex',
		flexDirection: 'row',
		marginTop: 10,
	},
	details: {
		display: 'flex',
		flexDirection: 'row',
	},
	detailsContent: {
		marginLeft: 20,
	},
});

const formateDate = (dateString: string) => {
	const date = new Date(dateString);
	return `${date.getDate()}.${date.getMonth()}.${date.getFullYear()}`;
};

const GroceryRequestPDF = ({ item }: GroceryRequestPDFProps) => (
	<Document>
		<Page>
			<View style={styles.title}>
				<Text>
					Einkauf von {item.client.user.firstName} {item.client.user.secondName}
				</Text>
			</View>
			<View style={styles.page}>
				<View>
					<View style={styles.subtitle}>
						<Text>Adresse</Text>
					</View>
					<Text>
						{item.client.address.street} {item.client.address.houseNr}
					</Text>
					<Text>
						{item.client.address.zipCode} {item.client.address.city}
					</Text>
					<Text>{item.client.address.state}</Text>
					<Text>Schweiz</Text>
				</View>
				<View>
					<View style={styles.subtitle}>
						<Text>Details</Text>
					</View>
					<View style={styles.details}>
						<View>
							<Text>Bevorzugter Laden: </Text>
							<Text>Einkaufszeitraum: </Text>
						</View>
						<View style={styles.detailsContent}>
							<Text>{item.preferredStore}</Text>
							<Text>
								{formateDate(item.fromDate)}
								{' bis '}
								{formateDate(item.toDate)}
							</Text>
						</View>
					</View>
				</View>
				<View>
					<View style={styles.subtitle}>
						<Text>Einkaufsliste</Text>
					</View>
					{item.shoppingList.map((entry, index) => {
						return (
							<View key={index} style={styles.grocery}>
								<View style={styles.checkbox}></View>
								<Text>{entry.description}</Text>
							</View>
						);
					})}
				</View>
			</View>
		</Page>
	</Document>
);

export default GroceryRequestPDF;
