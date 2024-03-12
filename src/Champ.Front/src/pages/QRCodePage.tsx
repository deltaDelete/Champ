import { Card, CardBody, CardHeader } from "@/components/card/Card.tsx";
import { useLoaderData } from "react-router-dom";
import { Client } from "@/api/client.ts";
import { QRCodeSVG } from "qrcode.react";


type QRCodePageLoaderParams = {
    params: {
        id: number
    }
}

export async function loader({ params }: QRCodePageLoaderParams | any) {
    const client = new Client();
    const patientRepo = client.getPatients();
    const patient = await patientRepo.get(params.id);
    return { patient };
}

export function QRCodePage() {
    // TODO: Выяснить
    const { patient } = useLoaderData();
    return (
        <Card>
            <CardHeader header="Код пациента" />
            <CardBody>
                <QRCodeSVG value={patient} />
            </CardBody>
        </Card>
    );
}