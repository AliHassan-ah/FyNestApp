import { Component, ChangeDetectionStrategy } from "@angular/core";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {}
