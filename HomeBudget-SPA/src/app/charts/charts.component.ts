import { Component, OnInit } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';
import { AuthService } from '../_services/auth.service';
import { IncomesService } from '../_services/incomes.service';
import { OutgoingsService } from '../_services/outgoings.service';
import { Outgoing } from '../_models/outgoing';
import { Income } from '../_models/income';
import { AlertifyService } from '../_services/alertify.service';
import { HttpRequest, HttpClient } from '@angular/common/http';
import { ChartsService } from '../_services/charts.service';
import { Bar } from '../_models/bar';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {
  outgoings: number[];
  incomes: number[];
  outgoingsPie: number[];
  incomesPie: number[];
  incomePie = true;
  categoriesIncome: string[];
  categoriesOutcome: string[];
  dates: string[];
  currentDay = new Date();
  firstDay: Date;
  lastDay: Date;

  public barChartOptions: ChartOptions = {
    responsive: true,
    scales: { xAxes: [{}], yAxes: [{}] },
    plugins: {
      datalabels: {
        anchor: 'end',
        align: 'end',
      }
    }
  };

  public pieChartOptions: ChartOptions = {
    responsive: true,
    legend: {
      position: 'top',
    },
    plugins: {
      datalabels: {
        formatter: (value, ctx) => {
          const label = ctx.chart.data.labels[ctx.dataIndex];
          return label;
        },
      },
    }
  };

   public pieChartLabels: Label[] = [['Download', 'Sales'], ['In', 'Store', 'Sales'], 'Mail Sales'];
  public pieChartData: number[] = [300, 500, 100];
  public pieChartLabels2: Label[] = [['Download', 'Sales'], ['In', 'Store', 'Sales'], 'Mail Sales'];
  public pieChartData2: number[] = [300, 500, 100];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartColors = [
    {
      backgroundColor: ['rgba(255,0,0,0.3)', 'rgba(0,255,0,0.3)', 'rgba(0,0,255,0.3)'],
    },
  ];

  public barChartLabels: Label[];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  

  public barChartData: ChartDataSets[] = [
  ];

  constructor(private authService: AuthService, private outgoingService: OutgoingsService, 
              private alertify: AlertifyService, private incomeService: IncomesService,
              private chartsService: ChartsService) { }

  ngOnInit() {
    this.firstDay = new Date(this.currentDay.getFullYear(),this.currentDay.getMonth(), 1);
    this.lastDay = new Date(this.currentDay.getFullYear(), this.currentDay.getMonth() + 1, 0, 23, 59, 59);

    this.load();
  }

  load() {
    this.chartsService.getBars(this.authService.decodedToken.nameid, this.firstDay.toLocaleDateString(), this.lastDay.toLocaleDateString())
    .subscribe(x => {
      this.outgoings = x.outgoings;
      this.incomes = x.incomes;
      this.dates = x.dates;
      this.barChartData = [
        { data: this.outgoings, label: 'Outgoings' },
        { data: this.incomes, label: 'Incomes' }
      ];
      this.barChartLabels = this.dates;
    }, error => {
      this.alertify.error("Cannot download data.");
    });

    this.chartsService.getPies(this.authService.decodedToken.nameid, this.firstDay.toLocaleDateString(), this.lastDay.toLocaleDateString())
    .subscribe(x => {
      this.outgoingsPie = x.outgoings;
      this.incomesPie = x.incomes;
      console.log(this.incomesPie);
      this.categoriesIncome = x.incomeCategories;
      this.categoriesOutcome = x.outgoingCategories;
      this.pieChartLabels = this.categoriesOutcome;
      this.pieChartData = this.outgoingsPie;
      this.pieChartLabels2 = this.categoriesIncome;
      this.pieChartData2 = this.incomesPie;
    }, error => {
      this.alertify.error("Cannot download data.");
    });
  }

  public chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
    console.log(event, active);
  }

  public chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
    console.log(event, active);
  }

}
