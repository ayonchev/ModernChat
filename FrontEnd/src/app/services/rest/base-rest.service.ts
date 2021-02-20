import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BaseRestService {

  constructor(private http: HttpClient) { }

  get<T>(path: string, routeData?: string, queryData?: any) : Observable<T[]> {
    let url: string = environment.backEndUrl + path;

    if(routeData) 
      url += `/${routeData}`;

    if(queryData) {
      let keyValuePairsString = Object.keys(queryData).map(key => `${key}=${queryData[key]}`).join('&');

      url += keyValuePairsString.length > 0
        ? `?${keyValuePairsString}` 
        : '';
    }

    return this.http.get<T[]>(url);
  }

  getPage<T>(path: string, skip: number, take: number) : Observable<T[]> {
    return this.http.get<T[]>(environment.backEndUrl + path + `?skip=${skip}&take=${take}`);
  }

  post<T>(path: string, model: T) : Observable<any> {
    return this.http.post<any>(environment.backEndUrl + path, model);
  }
}
