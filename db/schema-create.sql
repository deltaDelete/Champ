CREATE TABLE `Genders`
(
    `GenderId` INT PRIMARY KEY AUTO_INCREMENT,
    `Name`     VARCHAR(255)
);

CREATE TABLE `Patients`
(
    `PatientId`      INT PRIMARY KEY AUTO_INCREMENT,
    `Photo`          BLOB,
    `FirstName`      VARCHAR(255) NOT NULL,
    `LastName`       VARCHAR(255) NOT NULL,
    `MiddleName`     VARCHAR(255) NOT NULL,
    `PassportNumber` VARCHAR(20)  NOT NULL,
    `DateOfBirth`    DATE         NOT NULL,
    `GenderId`       INT          NOT NULL,
    `Address`        VARCHAR(255) NOT NULL,
    `PhoneNumber`    VARCHAR(20)  NOT NULL,
    `Email`          VARCHAR(255) NOT NULL
);

CREATE TABLE `Policies`
(
    `PolicyId`         INT PRIMARY KEY AUTO_INCREMENT,
    `PolicyNumber`     BIGINT       NOT NULL,
    `InsuranceCompany` VARCHAR(255) NOT NULL,
    `ExpirationDate`   DATE         NOT NULL,
    `PatientId`        INT          NOT NULL
);

CREATE TABLE `Diagnoses`
(
    `DiagnosisId`    INT PRIMARY KEY AUTO_INCREMENT,
    `PatientId`      INT          NOT NULL,
    `Diagnosis`      VARCHAR(255) NOT NULL,
    `MedicalHistory` TEXT         NOT NULL
);

CREATE TABLE `Doctors`
(
    `DoctorId`   INT PRIMARY KEY AUTO_INCREMENT,
    `FirstName`  VARCHAR(255) NOT NULL,
    `LastName`   VARCHAR(255) NOT NULL,
    `MiddleName` VARCHAR(255) NOT NULL
);

CREATE TABLE `ProcedureTypes`
(
    `ProcedureTypeId`   INT PRIMARY KEY AUTO_INCREMENT,
    `ProcedureTypeName` VARCHAR(50)
);

CREATE TABLE `Procedures`
(
    `ProcedureId`     INT PRIMARY KEY AUTO_INCREMENT,
    `PatientId`       INT          NOT NULL,
    `ProcedureDate`   DATE         NOT NULL,
    `DoctorId`        INT          NOT NULL,
    `ProcedureTypeId` INT          NOT NULL,
    `ProcedureName`   VARCHAR(255) NOT NULL,
    `Results`         TEXT         NOT NULL,
    `Recommendations` TEXT         NOT NULL
);

CREATE TABLE `Visits`
(
    `VisitId`   INT PRIMARY KEY AUTO_INCREMENT,
    `PatientId` INT  NOT NULL,
    `Date`      DATE NOT NULL
);

CREATE TABLE `MedicalRecords`
(
    `MedicalRecordId`   INT PRIMARY KEY AUTO_INCREMENT,
    `PatientId`         INT         NOT NULL,
    `MedicalCardNumber` VARCHAR(20) NOT NULL,
    `DateOfIssue`       DATE        NOT NULL
);

CREATE TABLE `Hospitalizations`
(
    `HospitalizationId` INT PRIMARY KEY AUTO_INCREMENT,
    `PatientId`         INT          NOT NULL,
    `AdmissionDate`     DATE         NOT NULL,
    `DischargeDate`     DATE         NOT NULL,
    `Reason`            VARCHAR(255) NOT NULL,
    `Additional`        JSON         NOT NULL
);

ALTER TABLE `Patients`
    ADD FOREIGN KEY (`GenderId`) REFERENCES `Genders` (`GenderId`);

ALTER TABLE `Policies`
    ADD FOREIGN KEY (`PatientId`) REFERENCES `Patients` (`PatientId`);

ALTER TABLE `Diagnoses`
    ADD FOREIGN KEY (`PatientId`) REFERENCES `Patients` (`PatientId`);

ALTER TABLE `Hospitalizations`
    ADD FOREIGN KEY (`PatientId`) REFERENCES `Patients` (`PatientId`);

ALTER TABLE `Procedures`
    ADD FOREIGN KEY (`PatientId`) REFERENCES `Patients` (`PatientId`);

ALTER TABLE `Procedures`
    ADD FOREIGN KEY (`DoctorId`) REFERENCES `Doctors` (`DoctorId`);

ALTER TABLE `Procedures`
    ADD FOREIGN KEY (`ProcedureTypeId`) REFERENCES `ProcedureTypes` (`ProcedureTypeId`);

ALTER TABLE `Visits`
    ADD FOREIGN KEY (`PatientId`) REFERENCES `Patients` (`PatientId`);

ALTER TABLE `MedicalRecords`
    ADD FOREIGN KEY (`PatientId`) REFERENCES `Patients` (`PatientId`);
