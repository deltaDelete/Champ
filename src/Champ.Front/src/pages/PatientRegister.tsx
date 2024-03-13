import { Card, CardBody, CardHeader } from "../components/card/Card.tsx";
import {
    SubmitErrorHandler,
    SubmitHandler,
    useForm,
} from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Gender, Patient, PatientSchema } from "../models/Patient.ts";
import { useState } from "react";
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
import { useMaskito } from "@maskito/react";
import { TextInput } from "@/components/input/TextInput.tsx";
import { Selector } from "@/components/input/Selector.tsx";
import { NumberInput } from "@/components/input/NumberInput.tsx";

const client = new Client();
const patientRepo = client.getPatients();

export default function PatientRegister() {
    const form = useForm<Patient>({
        resolver: zodResolver(PatientSchema),
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

    const onSubmit: SubmitHandler<Patient> = async data => {
        const patient = await patientRepo.post(data).then(
            async result => {
                toast({
                    title: "Добавлен пациент",
                    description: <p className="break-all">{JSON.stringify(data)}</p>,
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

    const onInvalid: SubmitErrorHandler<Patient> = async (errors, event) => {
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
            mask: /^(\d{4}\s\d{6})$/,
        },
    });

    return (
        <>
            <Card>
                <CardHeader header="Регистрация" />
                <CardBody>
                    <Form {...form}>
                        <form className="flex flex-col gap-1" onSubmit={form.handleSubmit(onSubmit, onInvalid)}>
                            <TextInput control={form.control} name={"lastName"} placeholder={"Иванов"}
                                       autoComplete="given"
                                       label={"Фамилия"} />
                            <TextInput control={form.control}
                                       autoComplete="family-name"
                                       name={"firstName"}
                                       placeholder={"Иван"}
                                       label={"Имя"} />
                            <TextInput control={form.control}
                                       autoComplete="additional-name"
                                       name={"middleName"}
                                       placeholder={"Иванович"}
                                       label={"Отчество"} />
                            <NumberInput control={form.control}
                                         name={"passportNumber"}
                                         placeholder={"1234 123456"}
                                         ref={maskito}
                                         label={"Серия и номер паспорта"} />
                            <FormField
                                control={form.control} name="dateOfBirth"
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
                                            <Input autoComplete="address" placeholder="г. Абакан, ул. Пушкина, д. 1, кв. 1" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="address" />
                            <FormField render={
                                ({ field }) => (
                                    <FormItem>
                                        <FormLabel>Номер телефона</FormLabel>
                                        <FormControl>
                                            <Input autoComplete="tel-national" placeholder="+79123456789" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )} control={form.control} name="phoneNumber" />
                            <TextInput control={form.control} name={"email"} placeholder={"i.ivanov@mail.ru"}
                                       autoComplete="email"
                                       label={"Электронная почта"} />
                            {/* @ts-ignore */}
                            <Selector collection={genders} keySelector={v => v.name.toString()}
                                      autoComplete="sex"
                                      valueSelector={v => v.genderId.toString()} displayNameSelector={v => v.name}
                                      control={form.control} name={"genderId"} placeholder={"Выберите ваш пол"}
                                      label={"Пол"} />

                            {/* TODO: Поля: Номер страхового полиса, страховая компания */}

                            <FormItem>
                                <FormLabel>Фото</FormLabel>
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