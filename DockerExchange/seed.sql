CREATE TABLE Machines
(
    ID integer NOT NULL,
    Name character(50),
    CONSTRAINT Machines_pkey PRIMARY KEY (ID)
);

CREATE TABLE MachineJobs
(
    ID integer NOT NULL,
    MachineID integer NOT NULL,
    StartDate timestamp without time zone,
    EndDate timestamp without time zone,
    CONSTRAINT MachineJobs_pkey PRIMARY KEY (ID),
    CONSTRAINT MachineJobs_Machine FOREIGN KEY (MachineID)
        REFERENCES public.Machines (ID) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
);


