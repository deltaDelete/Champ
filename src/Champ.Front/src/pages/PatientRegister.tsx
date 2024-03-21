import { Card, CardBody, CardHeader } from "../components/card/Card.tsx";
import {
    SubmitErrorHandler,
    SubmitHandler,
    useForm,
} from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
    Gender, InsuranceCompany,
    Patient,
    PatientRegisterSchema,
    PatientRegisterType,
} from "../models/Patient.ts";
import { useEffect, useState } from "react";
import { Client } from "../api/client.ts";
import { Button } from "@shadcn/components/ui/button.tsx";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@shadcn/components/ui/form.tsx";
import { Input } from "@shadcn/components/ui/input.tsx";
import { toast } from "@shadcn/components/ui/use-toast.ts";
import { TextInput } from "@/components/input/TextInput.tsx";
import { Selector } from "@/components/input/Selector.tsx";
import { NumberInput } from "@/components/input/NumberInput.tsx";
import JSONPretty from "react-json-pretty";

const client = new Client();
const patientRepo = client.getPatients();

export default function PatientRegister() {
    const form = useForm<PatientRegisterType>({
        resolver: zodResolver(PatientRegisterSchema),
        values: (import.meta.env.DEV) ? ({
            patient: {
                "firstName": "Ss",
                "lastName": "Ss",
                "middleName": "Ss",
                "passportNumber": 1234123456,
                "dateOfBirth": "2004-01-01",
                "genderId": "1",
                "occupation": "FE",
                "address": "1234",
                "phoneNumber": "+71234",
                "email": "a@a.ru",
            },
            policy: {
                "policyId": 1234,
                "expirationDate": null,
                "insuranceCompanyId": "1",
            },
        }) : undefined,
    });

    const [photo, setPhoto] = useState<File>();

    const [genders, _] = useState<Array<Gender>>([
        {
            genderId: 1,
            name: "Мужской",
        },
        {
            genderId: 2,
            name: "Женский",
        },
    ]);

    const [companies, setCompanies] = useState<Array<InsuranceCompany>>([
        {
            "insuranceCompanyId": 1,
            "name": "АО \"СОГАЗ\"",
        },
        {
            "insuranceCompanyId": 2,
            "name": "СПАО \"Ингосстрах\"",
        },
        {
            "insuranceCompanyId": 3,
            "name": "ПАО СК \"Росгосстрах\"",
        },
        {
            "insuranceCompanyId": 4,
            "name": "АО \"АльфаСтрахование\"",
        },
        {
            "insuranceCompanyId": 5,
            "name": "ООО \"Группа Ренессанс Страхование\"",
        },
        {
            "insuranceCompanyId": 6,
            "name": "АО \"Русский стандарт Страхование\"",
        },
        {
            "insuranceCompanyId": 7,
            "name": "ООО РСО \"ЕВРОИНС\"",
        },
        {
            "insuranceCompanyId": 8,
            "name": "САО \"ВСК\"",
        },
        {
            "insuranceCompanyId": 9,
            "name": "ООО СК \"ВТБ Страхование\"",
        },
    ]);

    useEffect(() => {
        async function retrieveCompanies() {
            const data = await client.getCompanies().getAll();
            setCompanies(data);
        }

        retrieveCompanies();
    }, []);

    const onSubmit: SubmitHandler<PatientRegisterType> = async data => {
        console.log(data);
        const patient = await patientRepo.post(data.patient).then(
            async result => {
                toast({
                    title: "Добавлен пациент",
                    description: <JSONPretty data={data} />,
                });
                return result;
            },
        )
            .then(async result => {
                photo && await patientRepo.postFormField(photo, result.patientId, "photo");
                return result;
            });
        console.log(JSON.stringify(patient));

    };

    const onInvalid: SubmitErrorHandler<PatientRegisterType> = async (errors, event) => {
        toast({
            title: "Ошибка",
            description: <div className="flex flex-col">
                {
                    Object.entries(errors).map(([_, value], __, ___) =>
                        value?.message && <p className="break-all">{value.message}</p>)
                }
            </div>,
        });
    };

    return (
        <>
            <Card>
                <CardHeader header="Регистрация" />
                <CardBody>
                    <Form {...form}>
                        <form className="flex flex-col gap-1" onSubmit={form.handleSubmit(onSubmit, onInvalid)}>
                            <TextInput control={form.control} name={"patient.lastName"} placeholder={"Иванов"}
                                       autoComplete="given"
                                       label={"Фамилия"} />
                            <TextInput control={form.control}
                                       autoComplete="family-name"
                                       name={"patient.firstName"}
                                       placeholder={"Иван"}
                                       label={"Имя"} />
                            <TextInput control={form.control}
                                       autoComplete="additional-name"
                                       name={"patient.middleName"}
                                       placeholder={"Иванович"}
                                       label={"Отчество"} />
                            <NumberInput control={form.control}
                                         name={"patient.passportNumber"}
                                         placeholder={"1234 123456"}
                                         label={"Серия и номер паспорта"} />
                            <TextInput control={form.control} name={"patient.occupation"}
                                       placeholder={"Название компании"}
                                       label={"Место работы"} />
                            <FormField
                                control={form.control} name="patient.dateOfBirth"
                                render={
                                    ({ field }) => (
                                        <FormItem>
                                            <FormLabel>Дата рождения</FormLabel>
                                            <FormControl>
                                                <Input placeholder="01.01.1980"
                                                       autoComplete="bday"
                                                       type={"date"} {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )} />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Адрес</FormLabel>
                                        <FormControl>
                                            <Input autoComplete="address"
                                                   placeholder="г. Абакан, ул. Пушкина, д. 1, кв. 1" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="patient.address" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Номер телефона</FormLabel>
                                        <FormControl>
                                            <Input autoComplete="tel-national" placeholder="+79123456789" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="patient.phoneNumber" />
                            <TextInput control={form.control} name={"patient.email"} placeholder={"i.ivanov@mail.ru"}
                                       autoComplete="email"
                                       label={"Электронная почта"} />
                            {/* @ts-ignore */}
                            <Selector collection={genders} keySelector={v => v.name.toString()}
                                      autoComplete="sex"
                                      valueSelector={v => v.genderId.toString()} displayNameSelector={v => v.name}
                                      control={form.control} name={"patient.genderId"} placeholder={"Выберите ваш пол"}
                                      label={"Пол"} />

                            {/* TODO: Поля: Номер страхового полиса, страховая компания */}
                            <NumberInput control={form.control}
                                         label={"Номер страхового полиса"}
                                         name={"policy.policyId"} />
                            <Selector collection={companies}
                                      label={"Страховая компания"}
                                      keySelector={(x: InsuranceCompany) => x.insuranceCompanyId.toString()}
                                      valueSelector={(x: InsuranceCompany) => x.insuranceCompanyId.toString()}
                                      displayNameSelector={(x: InsuranceCompany) => x.name}
                                      control={form.control} name={"policy.insuranceCompanyId"}
                                      placeholder={"Ваша страховая компания"} />
                            <FormField
                                control={form.control} name="policy.expirationDate"
                                render={
                                    ({ field }) => (
                                        <FormItem>
                                            <FormLabel>Дата окончания действия полиса</FormLabel>
                                            <FormControl>
                                                <Input placeholder="01.01.1980"
                                                       autoComplete="bday"
                                                       type={"date"} {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )} />

                            <FormItem>
                                <FormLabel> Фото</FormLabel>
                                <FormControl>
                                    <Input
                                        onChange={(e) =>
                                            /*@ts-ignore*/
                                            setPhoto(e.target.files[0])}
                                        type="file"
                                        multiple={false} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                            <Button type={"submit"} className="mt-2">Зарегистрироваться</Button>
                        </form>
                    </Form>
                </CardBody>
            </Card>
        </>
    )
        ;
}