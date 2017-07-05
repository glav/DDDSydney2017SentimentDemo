import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html',
    styleUrls: ['./fetchdata.component.css']
})
export class FetchDataComponent {
    public emails: EmailInfo[];
    public originUrl: string;
    http: Http;
    public showAll: boolean;

    constructor(http: Http, @Inject('ORIGIN_URL') originUrl: string) {
        this.originUrl = originUrl;
        this.http = http;

        // Do initil retrieve
        this.getLatestData();

        // Then set periodic calls in motion
        setInterval(() => {
            this.getLatestData();    
        }, 5000);
    }

    getLatestData(): void {
        this.http.get(this.originUrl + '/api/SampleData/EmailSentiment').subscribe(result => {
            this.emails = result.json() as EmailInfo[];
        });
    }

    showAllToggle(): void {
        this.showAll = !this.showAll;
    }
}

interface EmailInfo {
    from: string;
    timeOfMail: string;
    body: string;

}
