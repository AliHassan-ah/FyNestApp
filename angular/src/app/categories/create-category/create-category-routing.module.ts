import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CreateCategoryComponent } from "./create-category.component";

const routes: Routes = [
  {
    path: "",
    component: CreateCategoryComponent,
    pathMatch: "full",
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CreateCategoryRoutingModule {}
