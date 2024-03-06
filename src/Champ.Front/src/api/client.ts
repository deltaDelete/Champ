import {Gender, Patient} from "../models/Patient.ts";

export class Client {
    constructor() {}

    getGenders(): GenericRepository<Gender> {
        return new GenericRepository<Gender>("gender");
    }
    
    getPatients() {
        return new GenericRepository<Patient>("patient")
    }
}

class GenericRepository<T> {
    private subroute: string;
    constructor(subroute: string) {
        this.subroute = subroute;
    }
    
    async get(id: number): Promise<T> {
        return fetch(`/api/${this.subroute}/${id}`, { method: "GET" })
            .then(res => res && res.json());
    }

    async getAll(): Promise<T[]> {
        return fetch(`/api/${this.subroute}`, { method: "GET" })
            .then(res => res && res.json());
    }

    async post(body: T): Promise<T> {
        return fetch(`/api/${this.subroute}`, { method: "POST", body: JSON.stringify(body) })
            .then(res => res && res.json());
    }

    async put(body: T, id: number): Promise<T> {
        return fetch(`/api/${this.subroute}/${id}`, { method: "PUT", body: JSON.stringify(body) })
            .then(res => res && res.json());
    }

    async delete(id: number) {
        return fetch(`/api/${this.subroute}/${id}`, { method: "DELETE" })
            .then(res => res && res.json());
    }
}
