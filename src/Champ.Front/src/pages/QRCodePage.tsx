import { Card, CardBody, CardHeader } from "@/components/card/Card.tsx";
import { useLoaderData } from "react-router-dom";
import { Client } from "@/api/client.ts";
import { QRCodeSVG } from "qrcode.react";
import { Patient } from "@/models/Patient.ts";
import { RouterParams } from "@/utils/RouterUtils.ts";
import JSONPretty from "react-json-pretty";
import 'react-json-pretty/themes/monikai.css';


type QRCodePageLoaderParams = {
    id: number
}

export async function loader({ params: { id } }: RouterParams<QRCodePageLoaderParams>) {
    const client = new Client();
    const patientRepo = client.getPatients();
    return id && await patientRepo.get(id)
        .then(result => "type" in result ? null : result)
        .catch(_ => {
            return null;
        });
}

export function QRCodePage() {
    const patient = useLoaderData() as Patient | null;
    return (
        <Card>
            <CardHeader header="Код пациента" />
            <CardBody>
                <QRCodeSVG
                    className="w-[10rem]"
                    value={patient ? JSON.stringify(patient) : "Value was null"} />
                <JSONPretty data={patient ? JSON.stringify(patient) : "Value was null"} />
            </CardBody>
        </Card>
    );
}