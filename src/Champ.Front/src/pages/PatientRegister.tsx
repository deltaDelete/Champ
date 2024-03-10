import { Card, CardBody, CardHeader } from "../components/card/Card.tsx";
import { RegisterOptions, SubmitErrorHandler, SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Gender, Patient, PatientSchema } from "../models/Patient.ts";
import React, { useEffect, useState } from "react";
import { Client } from "../api/client.ts";
import { Button } from "@shadcn/components/ui/button.tsx";
import {
    Select,
    SelectItem,
    SelectTrigger,
    SelectValue,
    SelectContent,
} from "@shadcn/components/ui/select.tsx";
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@shadcn/components/ui/form.tsx";
import { Input } from "@shadcn/components/ui/input.tsx";

const client = new Client();
const genderRepo = client.getGenders();
const patientRepo = client.getPatients();

export default function PatientRegister() {
    const form = useForm<Patient>({
        resolver: zodResolver(PatientSchema),
    });
    const [genders, setGenders] = useState<Array<Gender>>([]);
    useEffect(() => {
        async function get() {
            const items = await genderRepo.getAll();
            setGenders(items);
        }

        get().catch(e => console.log(e));
    }, []);

    const onSubmit: SubmitHandler<Patient> = async data => {
        // await patientRepo.post(data);
        console.log(data);
    };

    const onInvalid: SubmitErrorHandler<Patient> = async (errors, event) => {
        console.log(errors, event);
    };

    return (
        <>
            <Card className={"m-4"}>
                <CardHeader header="Данные" />
                <CardBody>
                    <Form {...form}>
                        <form className="flex flex-col gap-1" onSubmit={form.handleSubmit(onSubmit, onInvalid)}>
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Фамилия</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Иванов" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="lastName" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Имя</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Иван" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="firstName" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Отчество</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Иванович" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="middleName" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Паспорт</FormLabel>
                                        <FormControl>
                                            <Input placeholder="9999 999999" type={"number"} {...field}
                                                   max={9_999_999_999} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="passportNumber" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Дата рождения</FormLabel>
                                        <FormControl>
                                            <Input placeholder="01.01.1980" type={"date"} {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="dateOfBirth" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Адрес</FormLabel>
                                        <FormControl>
                                            <Input placeholder="г. Абакан, ул. Пушкина, д. 1, кв. 1" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="address" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Номер телефона</FormLabel>
                                        <FormControl>
                                            <Input placeholder="+79123456789" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="phoneNumber" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Электронная почта</FormLabel>
                                        <FormControl>
                                            <Input placeholder="i.ivanov@example.ru" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="email" />
                            <FormField name="genderId" control={form.control}
                                       render={
                                           ({ field }) => (
                                               <FormItem>
                                                   <FormLabel>Пол</FormLabel>
                                                   <Select onValueChange={field.onChange} defaultValue={field.value}>
                                                       <FormControl>
                                                           {/* @ts-ignore */}
                                                           <SelectTrigger>
                                                               <SelectValue placeholder="Выберите пол" />
                                                           </SelectTrigger>
                                                       </FormControl>
                                                       <SelectContent>
                                                           {genders.map(value => (
                                                               <SelectItem key={value.genderId}
                                                                           value={value.genderId.toString()}>{value.name}</SelectItem>
                                                           ))}
                                                       </SelectContent>
                                                   </Select>
                                                   <FormMessage />
                                               </FormItem>
                                           )
                                       } />
                            <Button type={"submit"}>Зарегистрироваться</Button>
                        </form>
                    </Form>
                </CardBody>
            </Card>
        </>
    );
}
