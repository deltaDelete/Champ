import * as z from "zod";

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

export const PatientSchema = z.object({
    patientId: z.number().int().optional(),
    firstName: z.string({ required_error: "firstName is required" }).min(2, { message: "firstName is too short" }),
    lastName: z.string({ required_error: "lastName is required" }),
    middleName: z.string(),
    passportNumber: z.number({ required_error: "passportNumber is required", coerce: true }),
    dateOfBirth: z.date({ required_error: "dateOfBirth is required", coerce: true }),
    genderId: z.string({ required_error: "genderId is required", coerce: true }),
    address: z.string({ required_error: "address is required" }),
    phoneNumber: z.string({ required_error: "phoneNumber is required" }),
    email: z.string({ required_error: "email is required" }).email({ message: "email is invalid" }),
});

export type Gender = {
    genderId: number,
    name: string
}