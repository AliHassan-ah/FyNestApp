import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ProductsComponent } from "./products.component";
import { ProductsRoutingModule } from "./products-routing.module";
// import { SharedModule } from "@shared/shared.module";
// import { CreateProductDialogComponent } from "./create-product/create-product-dialog.component";
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

@NgModule({
  declarations: [
    ProductsComponent,
    // CreateProductDialogComponent,
    // EditProductDialogComponent,
  ],
  imports: [
    // SharedModule,
    CommonModule,
    ProductsRoutingModule,
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
  ],
})
export class ProductsModule {}
