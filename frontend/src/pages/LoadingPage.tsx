import { Spin } from 'antd';

const Loading = () => {
	return (
		<div className="w-full h-full flex flex-col justify-center items-center">
			<Spin size="large" />
		</div>
	);
};

export default Loading;
