import { CommonModule } from "@angular/common";
import { NgModule, ModuleWithProviders } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { ModalModule } from "ngx-bootstrap/modal";
import { TabsModule } from "ngx-bootstrap/tabs";
import { NgxPaginationModule } from "ngx-pagination";
import { AppAuthService } from "./auth/app-auth.service";
import { AppRouteGuard } from "./auth/auth-route-guard";
import { LayoutStoreService } from "./layout/layout-store.service";
import { AppSessionService } from "./session/app-session.service";
import { AbpPaginationControlsComponent } from "./components/pagination/abp-pagination-controls.component";
import { AbpValidationSummaryComponent } from "./components/validation/abp-validation.summary.component";
import { AbpModalHeaderComponent } from "./components/modal/abp-modal-header.component";
import { AbpModalFooterComponent } from "./components/modal/abp-modal-footer.component";
import { LocalizePipe } from "@shared/pipes/localize.pipe";
import { BusyDirective } from "./directives/busy.directive";
import { EqualValidator } from "./directives/equal-validator.directive";
import { AppUrlService } from "./nav/app-url.service";

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgxPaginationModule,
    FormsModule,
    ModalModule,
    BsDropdownModule,
    CollapseModule,
    TabsModule,
  ],
  declarations: [
    AbpPaginationControlsComponent,
    AbpValidationSummaryComponent,
    AbpModalHeaderComponent,
    AbpModalFooterComponent,
    LocalizePipe,
    BusyDirective,
    EqualValidator,
  ],
  exports: [
    AbpPaginationControlsComponent,
    AbpValidationSummaryComponent,
    AbpModalHeaderComponent,
    AbpModalFooterComponent,
    LocalizePipe,
    BusyDirective,
    EqualValidator,
    FormsModule,
    NgxPaginationModule,
    ModalModule,
    BsDropdownModule,
    CollapseModule,
    TabsModule,
  ],
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<SharedModule> {
    return {
      ngModule: SharedModule,
      providers: [
        AppSessionService,
        AppUrlService,
        AppAuthService,
        AppRouteGuard,
        LayoutStoreService,
      ],
    };
  }
}
