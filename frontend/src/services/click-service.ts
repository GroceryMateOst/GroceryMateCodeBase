import { AxiosBaseService } from './axios-base.service';

export default class ClickService extends AxiosBaseService {
	constructor() {
		super('/Click');
	}

	// eslint-disable-line no-use-before-define
	public async sendClicks(number: number): Promise<void> {
		return this.instance.post(`?number=${number}`).then(this.responseBody);
	}
}
