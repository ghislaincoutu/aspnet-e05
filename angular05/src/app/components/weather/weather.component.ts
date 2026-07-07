import { Component, OnInit } from '@angular/core';
import { WeatherService, Weather } from '../../services/weather.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-weather',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './weather.component.html'
})

export class WeatherComponent implements OnInit {
  weathers: Weather[] = [];
  weather12: Weather = { id: 0, city: '', temperature: 0, condition: '' };
  cities = ['Montréal', 'Québec', 'Trois-Rivières'];
  conditions = ['Ensoleillé', 'Nuageux', 'Pluvieux', 'Orageux'];
  constructor(private service: WeatherService) { }

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getAll().subscribe(data => this.weathers = data);
  }

  save() {
    if (this.weather12.id === 0) {
      this.service.create(this.weather12).subscribe(() => this.load());
    } else {
      this.service.update(this.weather12).subscribe(() => this.load());
    }
    this.weather12 = { id: 0, city: '', temperature: 0, condition: '' };
  }

  edit(w: Weather) {
    this.weather12 = { ...w };
  }

  delete(id: number) {
    this.service.delete(id).subscribe(() => this.load());
  }
}