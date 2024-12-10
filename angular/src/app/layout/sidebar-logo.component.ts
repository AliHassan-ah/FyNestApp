import { Component, ChangeDetectionStrategy } from "@angular/core";

@Component({
  selector: "sidebar-logo",
  templateUrl: "./sidebar-logo.component.html",
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarLogoComponent {}
