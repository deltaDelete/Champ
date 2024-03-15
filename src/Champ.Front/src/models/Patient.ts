import * as z from "zod";

export type Patient = {
    patientId: number;
    photo?: File;
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
};

function minLength(minLength: number) {
    return `Минимальная длина ${minLength}`;
}

export const PatientSchema = z.object(
    {
        patientId: z.number().int().optional(),
        firstName: z.string({ required_error: "Обязательное поле" }).min(1, minLength(1)),
        lastName: z.string({ required_error: "Обязательное поле" }).min(1, minLength(1)),
        middleName: z.string().optional(),
        passportNumber: z
            .number({
                required_error: "Обязятальное поле",
                coerce: true,
            })
            .min(1, minLength(1))
            .max(9_999_999_999),
        dateOfBirth: z.string({ required_error: "Обязательное поле", coerce: true }).transform(x => Date.parse(x)),
        genderId: z.string({ required_error: "Обязательное поле", coerce: true }).transform(x => Number(x)),
        address: z.string({ required_error: "Обязательное поле" }),
        phoneNumber: z.string({ required_error: "Обязательное поле" }),
        email: z
            .string({ required_error: "Обязательное поле" })
            .email({ message: "Неверный формат электронной почты" }),
        occupation: z.string().optional()
    },
    {
        required_error: "Обязятальное поле",
    },
);

export type Gender = {
    genderId: number;
    name: string;
};

export type Policy = {
    policyId: number;
    insuranceCompany: string;
    patientId: number;
    expirationDate?: Date;
};

export const PolicySchema = z.object(
    {
        policyId: z.number({ required_error: "Обязательное поле" }),
        expirationDate: z
            .string()
            .nullable()
            .optional()
            .transform(x => x && Date.parse(x)),
        insuranceCompanyId: z.string({ required_error: "Обязательное поле" }).transform(x => Number(x)),
    },
    {
        required_error: "Обязательное поле",
    },
);

export const PatientRegisterSchema = z.object({
    patient: PatientSchema,
    policy: PolicySchema,
});

export type PatientRegisterType = {
    patient: Patient;
    policy: Policy;
};

export type InsuranceCompany = {
    insuranceCompanyId: number;
    name: string;
};
