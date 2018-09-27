import { Component, Inject, Input, OnInit, ViewChild, ElementRef, AfterViewInit, OnDestroy, HostListener } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { ISystemOverview } from '../interfaces/isystemoverview';
import { SystemOverviewService } from '../services/systemoverview.service';

//
// dashboard component
//
// The dashboard component uses the SystemOverviewService to obtain the latest
// system overview information.  
//

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [DatePipe]

})

export class DashboardComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() class: string | undefined;
  @ViewChild('cns') cns: ElementRef | undefined;
  systemOverview: ISystemOverview | null = null;
  myCanvas: HTMLElement | undefined;
  subscription: Subscription | null = null;
  currentWidth: number = 0;

  constructor(
    public systemOverviewService: SystemOverviewService,
    private datePipe: DatePipe) {
  }

  // Whenever windows is resized, remember the width
  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.currentWidth = event.target.innerWidth;
    //console.log("Width: " + this.currentWidth);
  }

  ngOnInit() {
    // get the current width of our display area
    this.currentWidth = window.innerWidth;

    // subscribe to changes from the SystemOverviewService
    this.subscription = this.systemOverviewService.systemOverviewObservable.subscribe(data => {
      this.newData(data);
    });
  }

  ngAfterViewInit() {
    // view is initialized, so the canvas element is ready
    if (this.cns instanceof ElementRef) {
      if (this.cns.nativeElement instanceof HTMLElement) {
        this.myCanvas = this.cns.nativeElement;
        this.drawPage(this.systemOverviewService.getLatest());
      }
    }
  }

  ngOnDestroy() {
    // unsubscribe to changes from the SystemOverviewService
    if (this.subscription != null) {
      this.subscription.unsubscribe();
    }
  }

  newData(overview: ISystemOverview | null) {
    // the SystemOverviewSerice has notified us of a new ISystemOverview object.
    this.drawPage(overview);
  }

  calculateCardClasses(index: number) {
    var status = 'Unknown';
    if (this.systemOverview != null) {
      if (index == 1) {
          status = this.systemOverview.ICDataCenterStatus;
      }
      if (index == 2) {
        status = this.systemOverview.DataCenterWebAppStatus;
      }
    }
    return this.calculateCardClassesFromStatus(status);
  }

  calculateCardClassesFromStatus(status: string) {
    var isError = status == 'Offline';
    var isOk= status == 'OK';
    var isWarning = !isError && !isOk;
    return {
      card: true,
      'text-white': true,
      'bg-success': isOk,
      'bg-warning': isWarning,
      'bg-danger': isError,
      'mb-3': true
    };
  }


  drawPage(overview: ISystemOverview | null) {
        try {
            this.systemOverview = overview;
          if (this.myCanvas != undefined) {
            if (this.currentWidth > 1200) {
            // we are in large mode
              this.drawContents();
            }
          }
        }
        catch (e) {
          console.error(e);
        }
  }

  //
  // Everything below is about drawing objects on a canvas.
  // All data to be drawn is obtained from the systemOverview object
  //

  drawContents() {

        if (this.systemOverview != null) {
            //console.log("HomeComponent is drawing canvas contetns ");
            if (this.myCanvas instanceof HTMLCanvasElement) {
                var ctx = this.myCanvas.getContext("2d");
                if (ctx instanceof CanvasRenderingContext2D) {
                    ctx.clearRect(0, 0, this.myCanvas.width, this.myCanvas.height);
                    var count: string = this.systemOverview.ExperimentCount < 0 ? "???" : String(this.systemOverview.ExperimentCount);
                  this.drawDisk(ctx, 210, 275, 150, 150, '#979797', '#777777', count, "Experiments");
                  var bc: string = '#5cb85c'; // OK = green
                    if (this.systemOverview.ICDataCenterStatus == "Offline")
                      bc = '#d9534f'; // Offline = red
                    else if (this.systemOverview.ICDataCenterStatus == "Error")
                      bc = '#f0ad4e'; // Online Error = yellow
                    else if (this.systemOverview.ICDataCenterStatus == "Warning")
                      bc = '#f0ad4e'; // Online Warning = yellow
                    else if (this.systemOverview.ICDataCenterStatus == "Unknown")
                      bc = '#f0ad4e'; // Unknown = yellow
                    this.drawSubSystem(ctx, 185, 45, 200, 200, 15, bc, 'white', 'iC Data Center',
                        'Server:',
                        'Version:',
                        'Status',
                        this.systemOverview.ICDataCenterAddress,
                        this.systemOverview.ICDataCenterVersion,
                        this.systemOverview.ICDataCenterStatus);
                  bc = '#5cb85c'; // OK = green
                    if (this.systemOverview.DataCenterWebAppStatus == "Offline")
                      bc = '#d9534f'; // Error = red
                    else if (this.systemOverview.DataCenterWebAppStatus == "Error")
                      bc = '#d9534f'; // Error = red
                    else if (this.systemOverview.DataCenterWebAppStatus == "Warning")
                      bc = '#f0ad4e'; // Warning = yellow
                    else if (this.systemOverview.DataCenterWebAppStatus == "Unknown")
                      bc = '#f0ad4e'; // Unknown = yellow
                    this.drawSubSystem(ctx, 420, 45, 200, 200, 15, bc, 'white', 'ASP.NET Web Server',
                        'Server:',
                        'Version:',
                        'Status',
                        this.systemOverview.DataCenterWebAppAddress,
                        this.systemOverview.DataCenterWebAppVersion,
                        this.systemOverview.DataCenterWebAppStatus);
                  this.drawSubSystem(ctx, 655, 75, 200, 150, 15, '#5bc0de', 'white', 'Browser',
                        '',
                        'User:',
                        'Roles:',
                        '',
                        this.systemOverview.LoggedOnUser,
                        this.systemOverview.LoggedOnRole,);
                  this.drawDocument(ctx, 50, 120, '#979797', '#777777', this.datePipe.transform(this.systemOverview.LastImportDate, 'MMM dd, hh:mm aa'));

                    this.drawArrow(ctx, 110, 150);
                    this.drawArrow2(ctx, 390, 150);
                    this.drawArrow2(ctx, 625, 150);
                  this.drawLastUpdate(ctx, 0, 500, this.datePipe.transform(this.systemOverview.LastUpdate, 'MMM dd, hh:mm:ss aa'));
                }
            }
        }
  }

  drawDisk(context: CanvasRenderingContext2D, x: number, y: number, w: number, h: number, color: string, color2: string, line1: string, line2: string) {
        context.strokeStyle = color2;


        context.beginPath();
        context.rect(x, y + h / 8 - 1, w, h - h / 4 + 2);
        context.fillStyle = color;
        context.fill();


        context.beginPath(); //to draw the top circle
        for (var i = 0 * Math.PI; i < 2 * Math.PI; i += 0.001) {

            var xPos = (x + w / 2) - (w / 2 * Math.sin(i)) *
                Math.sin(0 * Math.PI) + (w / 2 * Math.cos(i)) *
                Math.cos(0 * Math.PI);

            var yPos = (y + h / 8) + (h / 8 * Math.cos(i)) *
                Math.sin(0 * Math.PI) + (h / 8 *
                    Math.sin(i)) * Math.cos(0 * Math.PI);

            if (i == 0) {
                context.moveTo(xPos, yPos);

            }
            else {
                context.lineTo(xPos, yPos);
            }
        }
        context.fillStyle = color2;
        context.fill();

        context.beginPath();
        context.moveTo(x, y + h / 8);
        context.lineTo(x, y + h - h / 8);

        for (var i = 0 * Math.PI; i < Math.PI; i += 0.001) {
            xPos = (x + w / 2) - (w / 2 * Math.sin(i)) * Math.sin(0 * Math.PI) + (w / 2 * Math.cos(i)) * Math.cos(0 * Math.PI);
            yPos = (y + h - h / 8) + (h / 8 * Math.cos(i)) * Math.sin(0 * Math.PI) + (h / 8 * Math.sin(i)) * Math.cos(0 * Math.PI);

            if (i == 0) {
                context.moveTo(xPos, yPos);

            }
            else {
                context.lineTo(xPos, yPos);
            }
        }

        context.moveTo(x + w, y + h / 8);
        context.lineTo(x + w, y + h - h / 8);


        context.fillStyle = color;
        context.fill();
        context.stroke();

        context.fillStyle = 'white';
        context.font = "normal 16px Arial";
        context.textAlign = "center";
        context.fillText(line1, x + w / 2, y + 85, w - 10);
        context.fillText(line2, x + w / 2, y + 110, w - 10);


        context.beginPath();
        context.moveTo(x + w / 2, y + 20);
        context.lineTo(x + w / 2, y - 50);
        context.closePath();
        context.strokeStyle = 'black';

        context.stroke();
  }

  drawSubSystem(context: CanvasRenderingContext2D, x: number, y: number, width: number, height: number, rad: number, fill: string, stroke: string,
        title: string, line1: string, line2: string, line3: string,
        address: string | undefined, version: string | undefined, status: string | undefined) {
        var radius = { tl: rad, tr: rad, br: rad, bl: rad };

        context.beginPath();
        context.moveTo(x + radius.tl, y);
        context.lineTo(x + width - radius.tr, y);
        context.quadraticCurveTo(x + width, y, x + width, y + radius.tr);
        context.lineTo(x + width, y + height - radius.br);
        context.quadraticCurveTo(x + width, y + height, x + width - radius.br, y + height);
        context.lineTo(x + radius.bl, y + height);
        context.quadraticCurveTo(x, y + height, x, y + height - radius.bl);
        context.lineTo(x, y + radius.tl);
        context.quadraticCurveTo(x, y, x + radius.tl, y);
        context.closePath();




        context.fillStyle = fill;
        context.fill();
        context.strokeStyle = stroke;
        context.stroke();

        context.beginPath();
        context.moveTo(x, y + 40);
        context.lineTo(x + width, y + 40);
        context.strokeStyle = stroke;
        context.stroke();

        context.fillStyle = 'white';
        context.font = "normal 16px Arial";
        context.textAlign = "center";
        context.fillText(title, x + width / 2, y + 30, width - 10);
        if (address != undefined) {
          context.fillText(address, x + width / 2, y + 65, width - 10);
        }
        context.textAlign = "left";
        context.font = "normal 16px Arial";
        if (version != undefined) {
            context.fillText(line2, x + 15, y + (height/2) + 10);
          context.fillText(version, x + 90, y + (height / 2) + 10);
        }
        if (status != undefined) {
          context.fillText(line3, x + 15, y + (height / 2) + 35);
          context.fillText(status, x + 90, y + (height / 2) + 35);
        }
  }

  drawDocument(context: CanvasRenderingContext2D, x: number, y: number, fill1: string, fill2: string, arrival: string) {

        context.beginPath();
        context.moveTo(x, y);
        context.lineTo(x + 30, y);
        context.lineTo(x + 40, y + 10);
        context.lineTo(x + 40, y + 60);
        context.lineTo(x, y + 60);
        context.lineTo(x, y);
        context.closePath();
        context.fillStyle = fill1;
        context.fill();
        context.strokeStyle = fill2;
        context.stroke();

        context.beginPath();
        context.moveTo(x + 30, y);
        context.lineTo(x + 30, y + 10);
        context.lineTo(x + 40, y + 10);
        context.lineTo(x + 30, y);
        context.closePath();
        context.fillStyle = fill2;
        context.fill();

        context.fillStyle = 'white';
        context.textAlign = "center";
        context.font = "normal 16px Arial";
        context.fillText("Last Arrival", x + 20, y + 80);
        context.fillText(arrival, x + 20, y + 100);
        context.font = "normal 16px Arial";
        context.fillText("Drop folder", x + 20, y - 20);

  }

  drawLastUpdate(context: CanvasRenderingContext2D, x: number, y: number, lastUpdate: string) {

        context.fillStyle = 'white';
        context.textAlign = "left";
        context.font = "normal 16px Arial";
        context.fillText("Last Update: " + lastUpdate, x, y);
  }

  drawArrow(context: CanvasRenderingContext2D, x: number, y: number) {

        context.beginPath();
        context.moveTo(x, y);
        context.lineTo(x + 20, y);
        context.lineTo(x + 15, y + 5);
        context.moveTo(x + 20, y);
        context.lineTo(x + 15, y - 5);
        context.closePath();
        context.strokeStyle = 'white';
        context.stroke();

  }

  drawArrow2(context: CanvasRenderingContext2D, x: number, y: number) {

        context.beginPath();
        context.moveTo(x, y);
        context.lineTo(x + 25, y);
        context.lineTo(x + 20, y + 5);
        context.moveTo(x + 25, y);
        context.lineTo(x + 20, y - 5);
        context.moveTo(x, y);
        context.lineTo(x + 5, y - 5);
        context.moveTo(x, y);
        context.lineTo(x + 5, y + 5);
        context.closePath();
        context.strokeStyle = 'white';
        context.stroke();

  }

};
