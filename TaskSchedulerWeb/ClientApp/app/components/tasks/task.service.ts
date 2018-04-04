import { Injectable, Inject } from '@angular/core';
import { Headers, RequestOptions, Http } from '@angular/http';
import 'rxjs';
import 'rxjs/add/operator/map';
import { TaskViewModel } from './task-model/task';
import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http/src/static_response';

@Injectable()
export class TaskService {

    public headers: any = new Headers({ 'Content-Type': 'application/json' });
    public options: any = new RequestOptions({ headers: this.headers });

    constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) {
    }

    getTasks(): Promise<TaskViewModel[]> {
        return this.http.get(this.originUrl + '/api/Task/getTasks')
            .map((response) => {
                return response.json();
            })
            .toPromise();
    }

    getTask(params: any): Promise<TaskViewModel> {
        return this.http.get(`${this.originUrl}/api/Task/edit`, { search: params })
            .map((response) => {
                return response.json()
            })
            .toPromise();
    }

    editTask(model: TaskViewModel): Promise<Response> {
        return this.http.post(`${this.originUrl}/api/Task/edit`, JSON.stringify(model), this.options).toPromise();
    }

    deleteTask(taskId: string): Promise<Response> {
        return this.http.delete(`${this.originUrl}/api/Task/delete?taskId=${taskId}`, this.options).toPromise();
    }
}