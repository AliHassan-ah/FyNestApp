import { Component } from "@angular/core";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { CreateProductComponent } from "./create-product/create-product.component";
import { Router } from "@angular/router";
@Component({
  selector: "app-products",
  standalone: false,
  templateUrl: "./products.component.html",
  styleUrl: "./products.component.css",
})
export class ProductsComponent {
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
  createProduct(): void {
    // this.showCreateOrEditProductDialog();
    this.router.navigate(["/app/create-product"]);
  }
  editProduct(): void {
    this.showCreateOrEditProductDialog();
  }
  showCreateOrEditProductDialog(id?: number): void {
    let createOrEditProductDialog: BsModalRef;
    if (!id) {
      createOrEditProductDialog = this._modalService.show(
        CreateProductComponent,
        {
          class: "modal-lg",
        }
      );
    } else {
      // createOrEditProductDialog = this._modalService.show(
      //   EditProductDialogComponent,
      //   {
      //     class: "modal-lg",
      //     initialState: {
      //       id: id,
      //     },
      //   }
      // );
    }
    createOrEditProductDialog.content.onSave.subscribe(() => {
      // this.getProducts();
    });

    createOrEditProductDialog.content.onSave.subscribe(() => {
      // this.getProducts();
    });
  }
}
