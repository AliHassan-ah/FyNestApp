import { Component } from "@angular/core";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { Router } from "@angular/router";

@Component({
  selector: "app-categories",
  templateUrl: "./categories.component.html",
  styleUrl: "./categories.component.css",
})
export class CategoriesComponent {
  stateOptions: any[] = [
    { label: "All", value: "all" },
    { label: "Published", value: "published" },
    { label: "Low Stock", value: "lowStock" },
    { label: "Draft", value: "draft" },
  ];
  value: string = "off";
  constructor(private _modalService: BsModalService, private router: Router) {}
  onStateChange(event: any) {
    // if (event.value === "login") {
    //   this.router.navigate(["/account/login"]); // Navigate to login page
    // } else if (event.value === "register") {
    //   this.router.navigate(["/account/register"]); // Navigate to register page
    // }
  }
  createCategory(): void {
    // this.showCreateOrEditProductDialog();
    this.router.navigate(["/app/create-category"]);
  }
  editProduct(): void {}
}
