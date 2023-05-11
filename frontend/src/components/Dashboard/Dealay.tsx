﻿import { Skeleton } from 'antd';
import { useState, useEffect } from 'react';

type Props = {
	children: JSX.Element;
	waitBeforeShow?: number;
};

const Delayed = ({ children, waitBeforeShow = 500 }: Props) => {
	const [isShown, setIsShown] = useState(false);

	useEffect(() => {
		const timer = setTimeout(() => {
			setIsShown(true);
		}, waitBeforeShow);
		return () => clearTimeout(timer);
	}, [waitBeforeShow]);

	return isShown ? (
		children
	) : (
		<Skeleton.Image active style={{ width: '500px', height: '500px' }} />
	);
};

export default Delayed;
