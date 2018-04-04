import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { TaskService } from '../task.service';
import { TaskViewModel } from '../task-model/task';
import { retry } from 'rxjs/operator/retry';

@Component({
    selector: 'task-list',
    templateUrl: './task-list.component.html',
    styleUrls: ['./task-list.component.css'],
    providers: [TaskService]
})
export class TaskComponent {
    public tasks: TaskViewModel[];
    public priorityList: Array<number>;

    constructor(private http: Http,
        @Inject('ORIGIN_URL') originUrl: string,
        private taskService: TaskService) {

        this.tasks = new Array<TaskViewModel>();
        this.priorityList = [0, 1, 2];

        this.loadTasks();
    }

    loadTasks() {
        this.taskService.getTasks()
            .then(tasksResult => this.tasks = tasksResult)
            .catch(() => { console.log("Erro ao buscar tarefas") });
    }

    new() {
        var task = new TaskViewModel();
        task.priority = 2;
        task.editing = true;

        this.tasks.push(task);
    }

    edit(task: TaskViewModel) {
        task.editing = !task.editing;
    }

    save(task: TaskViewModel) {
        this.taskService.editTask(task)
            .then((result) => {
                var taskResult = result.json() as TaskViewModel;
                task.id = taskResult.id;
                task.editing = false;
            })
            .catch(() => { console.log("Erro ao salvar tarefa") });
    }
    
    setImportant(task: TaskViewModel) {
        task.important = !task.important;
        this.taskService.editTask(task)
            .then(tasksResult => {
                task.editing = false;
            })
            .catch(() => { console.log("Erro ao salvar tarefa") });
    }

    remove(task: TaskViewModel) {
        if (!task.id) {
            this.tasks.splice(this.tasks.indexOf(task), 1);
            return;
        }

        this.taskService.deleteTask(task.id)
            .then(tasksResult => this.loadTasks())
            .catch(() => { console.log("Erro ao remover tarefa") });
    }
}


