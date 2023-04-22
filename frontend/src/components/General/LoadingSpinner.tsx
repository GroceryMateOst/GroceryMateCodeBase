import { Spin } from 'antd';

const Spinner = () => {
	return (
		<Spin tip="Loading" size="large">
			<div className="content" />
		</Spin>
	);
};

export default Spinner;
