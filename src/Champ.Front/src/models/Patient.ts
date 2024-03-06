export type Patient = {
    patientId: number;
    photo: string;
    firstName: string;
    lastName: string;
    middleName: string;
    passportNumber: number;
    dateOfBirth: string;
    genderId: number;
    gender?: Gender;
    address: string;
    phoneNumber: string;
    email: string;
}

export type Gender = {
    genderId: number,
    name: string
}