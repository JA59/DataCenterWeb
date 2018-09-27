import { Component, OnInit, OnDestroy } from '@angular/core';
import { ExperimentCountService } from '../../services/experimentcount.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'demo-3',
  templateUrl: './demo-3.component.html',
  styleUrls: ['./demo-3.component.css']
})

export class Demo3Component implements OnInit, OnDestroy{
  public currentCount: number;
  subscription: Subscription | null = null;

  // CounterService injected
  constructor(private counterService: ExperimentCountService) 
  {
    this.currentCount = -1;
  }

  ngOnInit() {
    this.currentCount = this.counterService.getLatest();
    this.subscription = this.counterService.experimentCountObservable.subscribe(data => {
      this.currentCount = data;
    });

    this.counterService.start();

  }

  ngOnDestroy() {
    this.counterService.stop();
    if (this.subscription != null) {
      this.subscription.unsubscribe();
    }
  }
}


