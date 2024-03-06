import { Card, CardBody, CardHeader } from "../components/card/Card.tsx";
import { SubmitHandler, useForm } from "react-hook-form";
import { Gender, Patient } from "../models/Patient.ts";
import { useEffect, useState } from "react";
import { Client } from "../api/client.ts";
import { List } from "linqts";

const client = new Client();
const genderRepo = client.getGenders();
const patientRepo = client.getPatients();

export default function PatientRegister() {
    const form = useForm<Patient>();
    const [genders, setGenders] = useState<Array<Gender>>([]);
    useEffect(() => {
        async function get() {
            const items = await genderRepo.getAll();
            genders.push(...items);
            setGenders(genders);
        }

        get().catch(e => console.log(e));
    }, []);

    const onSubmit: SubmitHandler<Patient> = async data => {
        await patientRepo.post(data);
    };

    return (
        <>
            <Card>
                <CardHeader header="Данные" />
                <CardBody>
                    <form className="flex flex-col gap-1" onSubmit={form.handleSubmit(onSubmit)}>
                        <input
                            type="text"
                            aria-invalid={!!form.formState.errors.lastName}
                            {...form.register("lastName", { minLength: 2 })}
                        />
                        <input
                            type="text"
                            aria-invalid={!!form.formState.errors.firstName}
                            {...form.register("firstName", { minLength: 2 })}
                        />
                        <input
                            type="text"
                            aria-invalid={!!form.formState.errors.middleName}
                            {...form.register("middleName", { minLength: 2 })}
                        />
                        <select {...form.register("genderId")}>
                            {genders.map(value => (
                                <option value={value.genderId}>{value.name}</option>
                            ))}
                        </select>
                        <input className="primary-button" type="submit" value="Зарегистрироваться" />
                    </form>
                </CardBody>
            </Card>
        </>
    );
}
