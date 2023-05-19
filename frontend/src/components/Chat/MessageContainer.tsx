import { MessageModel } from '../../models/ChatModel';
import Message from './Message';

interface MessageContainerProps {
	messages: MessageModel[];
	userid: string;
}

const MessageContainer = ({ messages, userid }: MessageContainerProps) => {
	return (
		<div>
			{messages.map((m, index) => (
				<Message message={m} key={index} userid={userid} />
			))}
		</div>
	);
};

export default MessageContainer;
