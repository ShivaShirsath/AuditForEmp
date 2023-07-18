USE [master];
GO

-- Create the server-level audit
CREATE SERVER AUDIT [AuditName]
TO FILE (FILEPATH = N'C:\Audits\')
WITH (ON_FAILURE = CONTINUE);
GO

-- Enable the server-level audit
ALTER SERVER AUDIT [AuditName] WITH (STATE = ON);
GO

-- Create the database-level audit specification for the Employee and Address tables
USE [master];
GO

-- Enable database-level audit
CREATE DATABASE AUDIT SPECIFICATION [AuditSpec]
FOR SERVER AUDIT [AuditName]
ADD (SELECT, DELETE, INSERT, UPDATE ON SCHEMA::dbo BY public)
WITH (STATE = ON);
GO

-- Enable table-level audit for Employee and Address tables
USE [master];
GO

-- Enable table-level audit for Employee table
ALTER TABLE dbo.Employees
    ADD CreatedBy VARCHAR(255) CONSTRAINT [Audit_Employee_CreatedBy] DEFAULT (SUSER_SNAME()),
        CreatedDate DATETIME CONSTRAINT [Audit_Employee_CreatedDate] DEFAULT (GETDATE()),
        ModifiedBy VARCHAR(255) CONSTRAINT [Audit_Employee_ModifiedBy] DEFAULT (SUSER_SNAME()),
        ModifiedDate DATETIME CONSTRAINT [Audit_Employee_ModifiedDate] DEFAULT (GETDATE());
GO

-- Enable table-level audit for Address table
ALTER TABLE dbo.Address
    ADD CreatedBy VARCHAR(255) CONSTRAINT [Audit_Address_CreatedBy] DEFAULT (SUSER_SNAME()),
        CreatedDate DATETIME CONSTRAINT [Audit_Address_CreatedDate] DEFAULT (GETDATE()),
        ModifiedBy VARCHAR(255) CONSTRAINT [Audit_Address_ModifiedBy] DEFAULT (SUSER_SNAME()),
        ModifiedDate DATETIME CONSTRAINT [Audit_Address_ModifiedDate] DEFAULT (GETDATE());
GO


ALTER TABLE dbo.Employee
    ADD CONSTRAINT [Audit_Employee_CreatedBy]
    DEFAULT (SUSER_SNAME()) FOR CreatedBy;
GO

ALTER TABLE dbo.Employee
    ADD CONSTRAINT [Audit_Employee_CreatedDate]
    DEFAULT (GETDATE()) FOR CreatedDate;
GO

ALTER TABLE dbo.Employee
    ADD CONSTRAINT [Audit_Employee_ModifiedBy]
    DEFAULT (SUSER_SNAME()) FOR ModifiedBy;
GO

ALTER TABLE dbo.Employee
    ADD CONSTRAINT [Audit_Employee_ModifiedDate]
    DEFAULT (GETDATE()) FOR ModifiedDate;
GO

ALTER TABLE dbo.Address
    ADD CONSTRAINT [Audit_Address_CreatedBy]
    DEFAULT (SUSER_SNAME()) FOR CreatedBy;
GO

ALTER TABLE dbo.Address
    ADD CONSTRAINT [Audit_Address_CreatedDate]
    DEFAULT (GETDATE()) FOR CreatedDate;
GO

ALTER TABLE dbo.Address
    ADD CONSTRAINT [Audit_Address_ModifiedBy]
    DEFAULT (SUSER_SNAME()) FOR ModifiedBy;
GO

ALTER TABLE dbo.Address
    ADD CONSTRAINT [Audit_Address_ModifiedDate]
    DEFAULT (GETDATE()) FOR ModifiedDate;
GO




SELECT 
    *
FROM sys.fn_get_audit_file('C:\Audits\AuditName_*.sqlaudit', DEFAULT, DEFAULT)
ORDER BY event_time DESC;
