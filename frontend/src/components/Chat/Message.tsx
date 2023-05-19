import { MessageModel } from '../../models/ChatModel';

interface MessageContainerProps {
	message: MessageModel;
	userid: string;
}

const Message = ({ message, userid }: MessageContainerProps) => {
	function padZero(num: number): string {
		return num.toString().padStart(2, '0');
	}

	function parseDateString(input: string | undefined): string {
		if (input == null) {
			return '';
		}
		const date = new Date(input);
		date.setHours(date.getHours());

		const formattedDate = `${padZero(date.getMonth() + 1)}.${padZero(
			date.getDate()
		)}.${date.getFullYear()}`;
		const formattedTime = `${padZero(date.getHours())}:${padZero(
			date.getMinutes()
		)}`;

		return `${formattedDate} / ${formattedTime}`;
	}

	return (
		<div>
			<div
				className={`chat ${
					message.senderId === userid ? 'chat-end' : 'chat-start'
				}`}
			>
				<div className="chat-header">
					{parseDateString(message.messageDate)}
				</div>
				<div className="chat-bubble bg-#8fb69c">{message.messageText}</div>
			</div>
		</div>
	);
};

export default Message;
