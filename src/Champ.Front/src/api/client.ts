import { Patient } from "../models/Patient.ts";

export class Client {
    constructor() {
    }

    getPatients() {
        return new GenericRepository<Patient>("patient");
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
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        return fetch(`/api/${this.subroute}`, { method: "POST", body: JSON.stringify(body), headers: headers })
            .then(res => res && res.json());
    }

    async postFormField<TValue extends string | File>(body: TValue, id: number, field: string): Promise<any> {
        const headers = new Headers();
        headers.append("Content-Type", "multipart/form-data");
        let formData;
        
        // todo где то что то не отправляется - плохо
        if (body instanceof File) {
            formData = await body.formData();
        }
        
        if (body instanceof String) {
            formData = new FormData();
            formData.append(field, body);
        }
        
        return fetch(`/api/${this.subroute}/${id}/${field}`, {
            method: "POST",
            body: formData,
            headers: headers,
        })
            .then(res => res && res.json());
    }

    async put(body: T, id: number): Promise<T> {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        return fetch(`/api/${this.subroute}/${id}`, { method: "PUT", body: JSON.stringify(body), headers: headers })
            .then(res => res && res.json());
    }

    async delete(id: number) {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");
        return fetch(`/api/${this.subroute}/${id}`, { method: "DELETE", headers: headers })
            .then(res => res && res.json());
    }
}
