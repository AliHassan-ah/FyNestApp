import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ViewCategoryComponent } from "./view-category.component";

const routes: Routes = [
  {
    path: "",
    component: ViewCategoryComponent,
    pathMatch: "full",
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ViewCategoryRoutingModule {}
