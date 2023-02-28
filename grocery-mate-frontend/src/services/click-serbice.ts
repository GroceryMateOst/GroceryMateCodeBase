import { AxiosBaseService } from "./axios-base.service";

export class ClickService extends AxiosBaseService{
    constructor(){
        super('/Click');
    }

    public async sendClicks(number: number): Promise<any>{
        return await this.instance.post(`?number=${number}`).then(this.responseBody)
    }

    public async getClicks(): Promise<any>{
        return await this.instance.get()
    }
}