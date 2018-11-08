import { Component } from '@angular/core';
import { MonitoringService } from '../services/monitoring.service';


@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;


  constructor(private monitoringService: MonitoringService) {
    this.monitoringService.logPageView("Page view: Counter");
  }

  public incrementCounter() {
    this.currentCount++;
  }
}
