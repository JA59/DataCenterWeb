import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { HttpClient } from '@angular/common/http';
import { ISystemOverview } from '../interfaces/isystemoverview';

@Injectable()
export class ExperimentCountService {

  private timer;                                                                        // periodic timer
  private sub: Subscription;                                                            // internal timer subscription object
  private experimentCountSubject = new Subject<number>();                               // subject for observation
  private experimentCount: number;                                                      // current counter value
  public  experimentCountObservable = this.experimentCountSubject.asObservable();       // Observable counter stream

  constructor(private http: HttpClient) {
    this.experimentCount = -1;
  }

  //
  // Start the periodic update
  //
  public start() {
    this.timer = Observable.timer(2000, 1000); // after two seconds, fire every second
    // subscribing to a observable returns a subscription object
    this.sub = this.timer.subscribe(t => this.doFetch(t));
  }

  //
  // Stop the periodic update
  //
  public stop() {
    // stop subscribing to the timer object
    this.sub.unsubscribe();
  }

  //
  // getLatest()
  // Returns the experimentCount value
  //
  public getLatest(): number {
    return this.experimentCount;
  }

  private doFetch(tick) {
    // Get the latest experiment count from the SystemOverview controller
    this.http.get<ISystemOverview>('api/SystemOverview/Summary').subscribe(result => {
      this.doUpdate(result.ExperimentCount);
    }, error => {
      this.doUpdate(-1);
    });
  }

  //
  // doUpdate(tick)
  // Update the experiment count and notify all of the subscribers
  //
  private doUpdate(count: number) {
    this.experimentCount = count;
    this.experimentCountSubject.next(this.experimentCount);
  }
}

