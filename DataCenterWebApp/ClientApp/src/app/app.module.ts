import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AuthService } from './services/auth.service';
import { AuthInterceptor } from './services/auth.interceptor';
import { CounterService } from './services/counter.service';
import { DragService } from './services/drag.service';
import { SystemOverviewService } from './services/systemoverview.service';
import { PageService } from './services/page.service';

import { ExperimentFilterPipe } from './services/experimentfilter.pipe';
import { HighlighterPipe } from './services/highlighter.pipe';

import { HighlightDirective } from './services/highlight.directive';
import { DraggableDirective } from './services/draggable.directive';
import { DropTargetDirective } from './services/droptarget.directive';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ExperimentPageComponent } from './experiment-page/experiment-page.component';
import { ExperimentItemComponent } from './experiment-item/experiment-item.component';
import { AboutComponent } from './about/about.component';
import { DemoComponent } from './demo/demo.component';
import { DragDropTestComponent } from './drag-drop-test/drag-drop-test.component';
import { HeaderBarComponent } from './headerbar/headerbar.component';
import { ExperimentComponent } from './experiments/experiment.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PageNotFoundComponent } from './pagenotfound/pagenotfound.component';
import { SystemOverviewComponent } from './system-overview/system-overview.component';

import { SlideIntroductionComponent } from './about/slide-introduction/slide-introduction.component';
import { SlideOverviewComponent } from './about/slide-overview/slide-overview.component';
import { Slide1Component } from './about/slide1/slide1.component';
import { Slide2Component } from './about/slide2/slide2.component';
import { Slide3Component } from './about/slide3/slide3.component';
import { Slide4Component } from './about/slide4/slide4.component';
import { Slide5Component } from './about/slide5/slide5.component';
import { Slide6Component } from './about/slide6/slide6.component';
import { Slide7Component } from './about/slide7/slide7.component';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ExperimentPageComponent,
    ExperimentItemComponent,
    AboutComponent,
    DemoComponent,
    DragDropTestComponent,
    HeaderBarComponent,
    ExperimentComponent,
    DashboardComponent,
    PageNotFoundComponent,
    SystemOverviewComponent,
    ExperimentFilterPipe,
    HighlighterPipe,
    HighlightDirective,
    DraggableDirective,
    DropTargetDirective,
    SlideIntroductionComponent,
    SlideOverviewComponent,
    Slide1Component,
    Slide2Component,
    Slide3Component,
    Slide4Component,
    Slide5Component,
    Slide6Component,
    Slide7Component

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'plannedexperiment/:id', component: ExperimentItemComponent },
      { path: 'about', component: AboutComponent },
      { path: 'demo', component: DemoComponent },
      { path: 'experiments', component: ExperimentComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: '**', component: PageNotFoundComponent }
    ])
  ],
  providers: [
    AuthService,
    CounterService,
    DragService,
    SystemOverviewService,
    PageService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
