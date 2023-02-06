import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
//import { EventEmitter } from 'events';

@Component({
  selector: 'app-stars',
  templateUrl: './stars.component.html',
  styleUrls: ['./stars.component.css']
})
export class StarsComponent implements OnChanges, OnInit {
    @Input() stars: number;
    @Output() ChildClicked: EventEmitter<string> = new EventEmitter<string>();
    starWidth: number;
  constructor() { }

  ngOnInit() {
  }

  ngOnChanges(): void {
        this.starWidth = this.stars * 75 / 5;
  }

    popup(): void {
        this.ChildClicked.emit(`Pop up value ${this.starWidth}`);
    }
 }
