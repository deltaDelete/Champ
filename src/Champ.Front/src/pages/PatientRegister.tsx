import { Card, CardBody, CardHeader } from "../components/card/Card.tsx";
import {
    Control, FieldPath,
    FieldValues,
    SubmitErrorHandler,
    SubmitHandler,
    useForm,
} from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Gender, Patient, PatientSchema } from "../models/Patient.ts";
import React, { useState } from "react";
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
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@shadcn/components/ui/form.tsx";
import { Input } from "@shadcn/components/ui/input.tsx";
import { toast } from "../../@shadcn/components/ui/use-toast.ts";
import { useMaskito } from "@maskito/react";

const client = new Client();
const patientRepo = client.getPatients();

export default function PatientRegister() {
    const form = useForm<Patient>({
        resolver: zodResolver(PatientSchema),
    });
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

    const onSubmit: SubmitHandler<Patient> = async data => {
        // await patientRepo.post(data).then(
        //     _ =>
        toast({
            title: "Добавлен пациент",
            description: <p className="break-all">{JSON.stringify(data)}</p>,
        });
        // );
        console.log(JSON.stringify(data));
    };

    const onInvalid: SubmitErrorHandler<Patient> = async (errors, event) => {
        console.log(errors, event);
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
    
    // TODO: Разбраться с масками
    const maskito = useMaskito({
        options: {
            mask: /^(\d{4}\s\d{6})$/
        }
    })

    return (
        <>
            <Card>
                <CardHeader header="Регистрация" />
                <CardBody>
                    <Form {...form}>
                        <form className="flex flex-col gap-1" onSubmit={form.handleSubmit(onSubmit, onInvalid)}>
                            <TextInput control={form.control} name={"lastName"} placeholder={"Иванов"}
                                       label={"Фамилия"} />
                            <TextInput control={form.control} name={"firstName"} placeholder={"Иван"} label={"Имя"} />
                            <TextInput control={form.control} name={"middleName"} placeholder={"Иванович"}
                                       label={"Отчество"} />
                            <NumberInput control={form.control} name={"passportNumber"} placeholder={"1234 123456"}
                                         ref={maskito}
                                         label={"Серия и номер паспорта"} />
                            <FormField
                                control={form.control} name="dateOfBirth"
                                render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Дата рождения</FormLabel>
                                        <FormControl>
                                            <Input placeholder="01.01.1980" type={"date"} {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} />
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
                            <TextInput control={form.control} name={"email"} placeholder={"i.ivanov@mail.ru"}
                                       label={"Электронная почта"} />
                            {/* @ts-ignore */}
                            <Selector collection={genders} keySelector={v => v.name.toString()}
                                      valueSelector={v => v.genderId.toString()} displayNameSelector={v => v.name}
                                      control={form.control} name={"genderId"} placeholder={"Выберите ваш пол"}
                                      label={"Пол"} />
                            <Button type={"submit"} className="mt-2">Зарегистрироваться</Button>
                        </form>
                    </Form>
                </CardBody>
            </Card>
        </>
    )
        ;
}

interface IFormControl<TFieldValues extends FieldValues = FieldValues> {
    control: Control<TFieldValues>,
    name: FieldPath<TFieldValues>,
    placeholder: string | undefined,
    label: string | undefined,
    ref?: React.RefCallback<any>
}

interface TextInputProps<TFieldValues extends FieldValues = FieldValues> extends IFormControl<TFieldValues> {
}

interface NumberInputProps<TFieldValues extends FieldValues = FieldValues> extends IFormControl<TFieldValues> {
    min?: number | string,
    max?: number | string
}

interface SelectProps<T, TFieldValues extends FieldValues = FieldValues> extends IFormControl<TFieldValues> {
    collection: T[],
    keySelector: <T>(data: T) => string,
    valueSelector: <T>(data: T) => string,
    displayNameSelector: <T>(data: T) => string,
}

function TextInput<TFieldValues extends FieldValues = FieldValues>(props: TextInputProps<TFieldValues>) {
    return (
        <FormField render={
            ({ field }) => (
                <FormItem ref={props.ref}>
                    {props.label && <FormLabel>{props.label}</FormLabel>}
                    <FormControl>
                        <Input placeholder={props.placeholder} {...field} />
                    </FormControl>
                    <FormMessage />
                </FormItem>
            )} control={props.control} name={props.name} />
    );
}

function NumberInput<TFieldValues extends FieldValues = FieldValues>(props: NumberInputProps<TFieldValues>) {
    return (

        <FormField render={
            ({ field }) => (
                <FormItem>
                    {props.label && <FormLabel>{props.label}</FormLabel>}
                    <FormControl>
                        <Input placeholder={props.placeholder} type="number" {...field}
                               max={props.max} min={props.min} />
                    </FormControl>
                    <FormMessage />
                </FormItem>
            )} control={props.control} name={props.name} />
    );
}

function Selector<T, TFieldValues extends FieldValues = FieldValues>(props: SelectProps<T, TFieldValues>) {
    return (
        <FormField name={props.name} control={props.control}
                   render={
                       ({ field }) => (
                           <FormItem>
                               {props.label && <FormLabel>{props.label}</FormLabel>}
                               <Select onValueChange={field.onChange} defaultValue={field.value}>
                                   <FormControl>
                                       {/* @ts-ignore */}
                                       <SelectTrigger>
                                           <SelectValue placeholder={props.placeholder} />
                                       </SelectTrigger>
                                   </FormControl>
                                   <SelectContent>
                                       {props.collection.map(value => (
                                           <SelectItem key={props.keySelector(value)}
                                                       value={props.valueSelector(value)}>{props.displayNameSelector(value)}</SelectItem>
                                       ))}
                                   </SelectContent>
                               </Select>
                               <FormMessage />
                           </FormItem>
                       )
                   } />
    );
}