import { GroceryRequestDetailModel } from '../../models/GroceryRequestModel';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr';
import { useState } from 'react';
import { Dialog } from '@material-ui/core';
import Chat from './Chat';
import UserService from '../../services/user-service';
import Chatservice from '../../services/chat-service';
import { MessageModel } from '../../models/ChatModel';
import { WechatOutlined } from '@ant-design/icons';

interface ChatOverlayProps {
	item: GroceryRequestDetailModel;
	markMessageAsRead: (item: GroceryRequestDetailModel) => void;
}

const ChatOverlay = ({ item, markMessageAsRead }: ChatOverlayProps) => {
	const [messages, setMessages] = useState<MessageModel[]>([]);
	const [open, setOpen] = useState(false);
	const [connection, setConnection] = useState<HubConnection | null>(null);
	const [userid, setUserid] = useState<string>('');

	const getCurrentUserId = async () => {
		const userService = new UserService();
		const response = await userService.getUserSettings();
		setUserid(response.user.userId || '');
	};

	const joinChat = async () => {
		if (connection != null) {
			return;
		}

		const socketConnection = new HubConnectionBuilder()
			.withUrl(
				// eslint-disable-next-line @typescript-eslint/restrict-template-expressions
				`${import.meta.env.VITE_SERVER_BASE_URL}/chatsocket`
			)
			.build();

		socketConnection.on(
			'ReceiveMessage',
			(messageText: string, receiverId: string, senderId: string) => {
				setMessages((prevMessages) => [
					...prevMessages,
					{
						senderId,
						receiverId,
						messageText,
						messageRead: false,
					},
				]);
			}
		);

		await socketConnection.start();
		await socketConnection.invoke('JoinChat', {
			user: userid,
			shoppingId: item.groceryRequestId,
		});
		setConnection(socketConnection);
	};

	const closeConnection = async () => {
		await connection?.stop();
	};

	const getPreviousMessages = async () => {
		const chatService = new Chatservice();
		const response = await chatService.getMessages(item.groceryRequestId);
		setMessages(response.messages);
	};

	const setAllReceivedMessagesRead = async () => {
		const chatService = new Chatservice();
		markMessageAsRead(item);
		await chatService.readMessages(item.groceryRequestId);
	};

	const handleClickOpen = async () => {
		await getPreviousMessages();
		await joinChat();
		await getCurrentUserId();
		setOpen(true);
		await setAllReceivedMessagesRead();
	};

	const handleClose = async () => {
		setOpen(false);
		await closeConnection();
	};

	return (
		<div className="px-4">
			<button
				onClick={handleClickOpen}
				className="bg-transparent border-none flex items-center"
			>
				<span>
					Chat <WechatOutlined />
				</span>
				{item.unreadMessages > 0 && (
					<div className="ml-2 bg-red-500 text-white w-5 h-5 flex items-center justify-center rounded-full">
						{JSON.stringify(item.unreadMessages)}
					</div>
				)}
			</button>

			<Dialog open={open} onClose={handleClose} maxWidth="sm" fullWidth={true}>
				<Chat
					item={item}
					messages={messages}
					connection={connection}
					userid={userid}
				/>
			</Dialog>
		</div>
	);
};

export default ChatOverlay;
