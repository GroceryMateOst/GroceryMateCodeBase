import { HubConnection } from '@microsoft/signalr';
import { GroceryRequestDetailModel } from '../../models/GroceryRequestModel';
import { MessageModel } from '../../models/ChatModel';
import { useState } from 'react';
import MessageContainer from './MessageContainer';
import { UserModelGrocery } from '../../models/UserModel';
import { DialogTitle, DialogActions, DialogContent } from '@material-ui/core';

interface ChatProps {
	item: GroceryRequestDetailModel;
	messages: MessageModel[];
	connection: HubConnection | null;
	userid: string;
}

const Chat = ({ item, messages, connection, userid }: ChatProps) => {
	const [currentMessage, setCurrentMessage] = useState<string>('');
	const [chatpartner] = useState<UserModelGrocery>(
		userid == item.client.user.userId ? item.contractor : item.client
	);

	const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		setCurrentMessage(e.target.value);
	};

	const sendMessage = async (e: React.MouseEvent<HTMLButtonElement>) => {
		e.preventDefault();
		console.log('sending ' + currentMessage);
		let receiver: string;

		if (userid == item.client.user.userId) {
			receiver = item.contractor.user.userId;
		} else {
			receiver = item.client.user.userId;
		}

		try {
			await connection?.invoke('SendMessage', currentMessage, receiver, userid);
			setCurrentMessage('');
		} catch (exception) {
			console.log(exception);
		}
	};

	return (
		<div>
			<DialogTitle
				style={{ position: 'sticky', top: 0, zIndex: '1' }}
				className="bg-[#8fb69c]"
			>
				<div>
					<div className="containerWrap flex justify-between">
						<h3>
							Chat mit {chatpartner.user.firstName}{' '}
							{chatpartner.user.secondName}
						</h3>
					</div>
				</div>
			</DialogTitle>

			<DialogContent dividers style={{ minHeight: '400px' }}>
				<MessageContainer
					messages={messages}
					userid={userid}
				></MessageContainer>
			</DialogContent>
			<DialogActions
				style={{ position: 'sticky', bottom: 0, background: '#fff' }}
				className="bg-[#8fb69c]"
			>
				<div className="bg-[#8fb69c]  w-full py-5 shadow-lg">
					<form className="px-2 containerWrap flex">
						<input
							value={currentMessage}
							onChange={(e) => handleInputChange(e)}
							className="input w-full focus:outline-none bg-gray-100 rounded-r-none"
							type="text"
						/>
						<button
							className="w-auto bg-gray-500 text-white rounded-r-lg px-5 text-sm"
							onClick={(e) => sendMessage(e)}
						>
							Senden
						</button>
					</form>
				</div>
			</DialogActions>
		</div>
	);
};

export default Chat;
