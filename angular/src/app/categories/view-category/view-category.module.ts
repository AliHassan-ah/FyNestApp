import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ViewCategoryRoutingModule } from "./view-category-routing.module";
import { ViewCategoryComponent } from "./view-category.component";

// import { SharedModule } from "@shared/shared.module";
// import { EditProductDialogComponent } from "./edit-product/edit-product-dialog.component";
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
  declarations: [ViewCategoryComponent],
  imports: [
    SharedModule,
    CommonModule,
    ViewCategoryRoutingModule,
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
export class ViewCategoryModule {}
