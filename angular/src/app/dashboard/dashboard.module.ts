import { NgModule } from "@angular/core";
import { SharedModule } from "@shared/shared.module";
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { DashBoardComponent } from "./dashboard.component";
import { ChartModule } from "primeng/chart";
import { CardModule } from "primeng/card";
import { TagModule } from "primeng/tag";
import { TableModule } from "primeng/table";
import { PaginatorModule } from "primeng/paginator";

@NgModule({
  declarations: [DashBoardComponent],
  imports: [
    SharedModule,
    DashboardRoutingModule,
    ChartModule,
    CardModule,
    TagModule,
    TableModule,
    PaginatorModule,
  ],
})
export class DashboardModule {}
