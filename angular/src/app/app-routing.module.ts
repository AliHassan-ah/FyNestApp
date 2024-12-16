import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AppRouteGuard } from "@shared/auth/auth-route-guard";
import { AppComponent } from "./app.component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "",
        component: AppComponent,
        children: [
          {
            path: "dashboard",
            loadChildren: () =>
              import("./dashboard/dashboard.module").then(
                (m) => m.DashboardModule
              ),
            canActivate: [AppRouteGuard],
          },
          {
            path: "products",
            loadChildren: () =>
              import("./products/products.module").then(
                (m) => m.ProductsModule
              ),
            canActivate: [AppRouteGuard],
          },
          {
            path: "create-product",
            loadChildren: () =>
              import("./products/create-product/create-product.module").then(
                (m) => m.CreateProductModule
              ),
            canActivate: [AppRouteGuard],
          },
          {
            path: "categories",
            loadChildren: () =>
              import("./categories/categories.module").then(
                (m) => m.CategoriesModule
              ),
            canActivate: [AppRouteGuard],
          },
          {
            path: "create-category",
            loadChildren: () =>
              import(
                "./categories/create-category/create-category.module"
              ).then((m) => m.CreateCategoryModule),
            canActivate: [AppRouteGuard],
          },
          {
            path: "edit-category",
            loadChildren: () =>
              import("./categories/edit-category/edit-category.module").then(
                (m) => m.EditCategoryModule
              ),
            canActivate: [AppRouteGuard],
          },
          {
            path: "view-category",
            loadChildren: () =>
              import("./categories/view-category/view-category.module").then(
                (m) => m.ViewCategoryModule
              ),
            canActivate: [AppRouteGuard],
          },
          {
            path: "orders",
            loadChildren: () =>
              import("./orders/orders.module").then((m) => m.OrdersModule),
            canActivate: [AppRouteGuard],
          },
          {
            path: "view-order",
            loadChildren: () =>
              import("./orders/view-order/view-order.module").then(
                (m) => m.ViewOrderModule
              ),
            canActivate: [AppRouteGuard],
          },
          {
            path: "about",
            loadChildren: () =>
              import("./about/about.module").then((m) => m.AboutModule),
            canActivate: [AppRouteGuard],
          },
          {
            path: "users",
            loadChildren: () =>
              import("./users/users.module").then((m) => m.UsersModule),
            data: { permission: "Pages.Users" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "roles",
            loadChildren: () =>
              import("./roles/roles.module").then((m) => m.RolesModule),
            data: { permission: "Pages.Roles" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "tenants",
            loadChildren: () =>
              import("./tenants/tenants.module").then((m) => m.TenantsModule),
            data: { permission: "Pages.Tenants" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "update-password",
            loadChildren: () =>
              import("./users/users.module").then((m) => m.UsersModule),
            canActivate: [AppRouteGuard],
          },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
