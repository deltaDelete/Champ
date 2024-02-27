CREATE TABLE Patients
(
    PatientId      INT PRIMARY KEY AUTO_INCREMENT,
    Photo          BLOB         NULL,
    FirstName      VARCHAR(255) NOT NULL,
    LastName       VARCHAR(255) NOT NULL,
    MiddleName     VARCHAR(255) NOT NULL,
    PassportNumber VARCHAR(20)  NOT NULL,
    DateOfBirth    DATE         NOT NULL,
    GenderId       INT          NOT NULL,
    Address        VARCHAR(255) NOT NULL,
    PhoneNumber    VARCHAR(20)  NOT NULL,
    Email          VARCHAR(255) NOT NULL,
    FOREIGN KEY (GenderId) REFERENCES Genders(GenderId)
);

CREATE TABLE Diagnoses
(
    DiagnosisId    INT PRIMARY KEY AUTO_INCREMENT,
    PatientId      INT          NOT NULL,
    Diagnosis      VARCHAR(255) NOT NULL,
    MedicalHistory TEXT         NOT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients (PatientId)
);

CREATE TABLE Hospitalizations
(
    HospitalizationId INT PRIMARY KEY AUTO_INCREMENT,
    PatientId         INT          NOT NULL,
    AdmissionDate     DATE         NOT NULL,
    DischargeDate     DATE         NOT NULL,
    Reason            VARCHAR(255) NOT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients (PatientId)
);

CREATE TABLE Doctors
(
    DoctorId   INT PRIMARY KEY AUTO_INCREMENT,
    FirstName  VARCHAR(255) NOT NULL,
    LastName   VARCHAR(255) NOT NULL,
    MiddleName VARCHAR(255) NOT NULL
);

CREATE TABLE ProcedureTypes
(
    ProcedureTypeId   INT PRIMARY KEY AUTO_INCREMENT,
    ProcedureTypeName VARCHAR(50)
);

CREATE TABLE Procedures
(
    ProcedureId     INT PRIMARY KEY AUTO_INCREMENT,
    PatientId       INT          NOT NULL,
    ProcedureDate   DATE         NOT NULL,
    DoctorId        INT          NOT NULL,
    ProcedureTypeId INT          NOT NULL,
    ProcedureName   VARCHAR(255) NOT NULL,
    Results         TEXT         NOT NULL,
    Recommendations TEXT         NOT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients (PatientId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors (DoctorId),
    FOREIGN KEY (ProcedureTypeId) REFERENCES ProcedureTypes (ProcedureTypeId)
);

CREATE TABLE Visits
(
    VisitId       INT PRIMARY KEY AUTO_INCREMENT,
    PatientId     INT  NOT NULL,
    LastVisitDate DATE NOT NULL,
    NextVisitDate DATE NOT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients (PatientId)
);

CREATE TABLE MedicalRecords
(
    MedicalRecordId   INT PRIMARY KEY AUTO_INCREMENT,
    PatientId         INT         NOT NULL,
    MedicalCardNumber VARCHAR(20) NOT NULL,
    DateOfIssue       DATE        NOT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients (PatientId)
);