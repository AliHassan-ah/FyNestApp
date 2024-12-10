import { Component } from "@angular/core";

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
  onStateChange(event: any) {
    // if (event.value === "login") {
    //   this.router.navigate(["/account/login"]); // Navigate to login page
    // } else if (event.value === "register") {
    //   this.router.navigate(["/account/register"]); // Navigate to register page
    // }
  }
}
