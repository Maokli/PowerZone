import { ViewportScroller } from '@angular/common';
import { Component } from '@angular/core';
import * as $ from 'jquery';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  showMenu = false;
  ngOnInit() {
    $('ul').on('click', 'a', function(event) {

      const href = $(this).attr('href');

      $('html, body').animate(
        {
          scrollTop: $(href).offset().top
        },
        700
      );
    });
  }
  toggleNavbar() {
    this.showMenu = !this.showMenu;
  }
}
