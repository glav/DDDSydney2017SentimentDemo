import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public emails: EmailInfo[];

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        http.get(originUrl + '/api/SampleData/EmailSentiment').subscribe(result => {
            this.emails = result.json() as EmailInfo[];
        });
    }
}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

interface EmailInfo {
    from: string;
    timeOfMail: string;
    body: string;

}
