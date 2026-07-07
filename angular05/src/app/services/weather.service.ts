import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Weather {
  id: number;
  city: string;
  temperature: number;
  condition: string;
}

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  private api = "/api/weather";
  constructor(private http: HttpClient) { }

  getAll(): Observable<Weather[]> {
    return this.http.get<Weather[]>(this.api);
  }

  create(weather: Weather) {
    return this.http.post(this.api, weather);
  }

  update(weather: Weather) {
    return this.http.put(`${this.api}/${weather.id}`, weather);
  }

  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
}