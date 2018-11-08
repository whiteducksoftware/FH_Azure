import { Component, OnInit } from '@angular/core';
import { MonitoringService } from '../services/monitoring.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  constructor(private monitoringService: MonitoringService) {
    this.monitoringService.logPageView("Page view: File-Upload");
  }

  ngOnInit() {

  }

}
