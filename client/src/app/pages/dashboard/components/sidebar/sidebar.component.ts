import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html'
})
export class SidebarComponent{
  collapseShow = "hidden";
  constructor() { }

  ngOnInit() {
  }
  toggleCollapseShow(classes) {
    this.collapseShow = classes;
  }
}
