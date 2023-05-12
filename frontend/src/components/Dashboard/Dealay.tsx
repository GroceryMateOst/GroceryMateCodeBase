import { Skeleton } from 'antd';
import { useState, useEffect } from 'react';

interface DelayedProps {
	children: JSX.Element;
	waitBeforeShow?: number;
	placeHolderSize: string;
}

const Delayed = ({
	children,
	waitBeforeShow = 500,
	placeHolderSize,
}: DelayedProps) => {
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
		<Skeleton.Image
			active
			style={{ width: placeHolderSize, height: placeHolderSize }}
		/>
	);
};

export default Delayed;
