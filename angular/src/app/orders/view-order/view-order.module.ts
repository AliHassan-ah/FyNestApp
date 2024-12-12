import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ViewOrderComponent } from "./view-order.component";
import { ViewOrderRoutingModule } from "./view-order-routing.module";
import { AvatarModule } from "primeng/avatar";
import { BadgeModule } from "primeng/badge";
import { TableModule } from "primeng/table";
import { TagModule } from "primeng/tag";
import { RatingModule } from "primeng/rating";
import { ButtonModule } from "primeng/button";
import { PaginatorModule } from "primeng/paginator";
import { DropdownModule } from "primeng/dropdown";
import { FormsModule } from "@angular/forms";
import { MultiSelectModule } from "primeng/multiselect";
import { SelectButtonModule } from "primeng/selectbutton";
import { CalendarModule } from "primeng/calendar";
import { SharedModule } from "@shared/shared.module";
import { CardModule } from "primeng/card";

@NgModule({
  declarations: [ViewOrderComponent],
  imports: [
    SharedModule,
    CommonModule,
    ViewOrderRoutingModule,
    AvatarModule,
    BadgeModule,
    TableModule,
    RatingModule,
    TagModule,
    ButtonModule,
    PaginatorModule,
    DropdownModule,
    FormsModule,
    MultiSelectModule,
    SelectButtonModule,
    CalendarModule,
    CardModule,
  ],
})
export class ViewOrderModule {}
