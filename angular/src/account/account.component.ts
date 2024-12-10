import {
  Component,
  OnInit,
  ViewEncapsulation,
  Injector,
  Renderer2,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";
@Component({
  templateUrl: "./account.component.html",
  styleUrl: "./account.css",
  encapsulation: ViewEncapsulation.None,
})
export class AccountComponent extends AppComponentBase implements OnInit {
  stateOptions: any[] = [
    { label: "Login", value: "login" },
    { label: "Register", value: "register" },
  ];
  value: string = "login";

  constructor(
    injector: Injector,
    private renderer: Renderer2,
    private router: Router
  ) {
    super(injector);
  }

  showTenantChange(): boolean {
    return abp.multiTenancy.isEnabled;
  }

  ngOnInit(): void {
    this.renderer.addClass(document.body, "login-page");
  }
  onStateChange(event: any) {
    if (event.value === "login") {
      this.router.navigate(["/account/login"]); // Navigate to login page
    } else if (event.value === "register") {
      this.router.navigate(["/account/register"]); // Navigate to register page
    }
  }
}
