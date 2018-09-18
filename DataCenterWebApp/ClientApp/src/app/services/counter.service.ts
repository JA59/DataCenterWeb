import { EventEmitter, Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, } from "rxjs";
import 'rxjs/Rx';

@Injectable()
export class CounterService {
    counter: number = 0; 
    private clock: Observable<number> | null = null;


    constructor(@Inject(PLATFORM_ID) private platformId: any) {

    }

    public startCounting() {
        this.clock = Observable.interval(3000).map(tick => this.counter++);
    }

    public getClock(): Observable<number> | null {
        return this.clock;
    }


}

