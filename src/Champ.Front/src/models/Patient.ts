import * as z from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { tuple, ZodString } from "zod";

export type Patient = {
    patientId: number;
    photo: string;
    firstName: string;
    lastName: string;
    middleName: string;
    passportNumber: number;
    dateOfBirth: string;
    genderId: number | string;
    gender?: Gender;
    address: string;
    phoneNumber: string;
    email: string;
}

function minLength(minLength: number) {
    return `Минимальная длина ${minLength}`;
}

export const PatientSchema = z.object({
        patientId: z.number().int().optional(),
        firstName: z.string({ required_error: "`Имя` обязательное поле" }).min(1, minLength(1)),
        lastName: z.string({ required_error: "`Фамилия` обязательное поле" }).min(1, minLength(1)),
        middleName: z.string().optional(),
        passportNumber: z.number({ required_error: "`Номер паспорта` обязятальное поле", coerce: true }).min(1, minLength(1)).max(9_999_999_999),
        dateOfBirth: z.date({ required_error: "`Дата рождения` обязательное поле", coerce: true }),
        genderId: z.string({ required_error: "`Пол` обязательное поле", coerce: true }),
        address: z.string({ required_error: "`Адрес` обязательное поле" }),
        phoneNumber: z.string({ required_error: "`Номер телефона` обязательное поле" }),
        email: z.string({ required_error: "`Эл. почта` обязательное поле" }).email({ message: "Неверный формат электронной почты" }),
    },
    {
        required_error: "Обязятальное поле",
    });

export type Gender = {
    genderId: number,
    name: string
}