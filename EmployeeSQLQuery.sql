CREATE DATABASE EmployeeDB
GO
Create Table Employee(
Id int IDENTITY PRIMARY KEY,
FirstName nvarchar(max) NOT NULL,
MiddleName nvarchar(max) NOT NULL,
LastName nvarchar(max) NOT NULL,
CreatedAt DATETIME null,
UpdatedAt DATETIME null,
)
GO

Create Procedure Sp_InsertEmployee
(
@FirstName nvarchar(max),
@MiddleName nvarchar(max),
@LastName nvarchar(max),
@CreatedAt DATETIME,
@UpdatedAt DATETIME
)
AS
BEGIN
INSERT INTO Employee VALUES(@FirstName,@MiddleName,@LastName,@CreatedAt,@UpdatedAt)
END
Go
EXEC Sp_InsertEmployee 'Nabiha','Islam','Naisha','2021/10/21','2021/10/21'
EXEC Sp_InsertEmployee 'Jamiya','Islam','Faria','2021/10/21','2021/10/21'
EXEC Sp_InsertEmployee 'Saira','Islam','Ira','2021/10/21','2021/10/21'
EXEC Sp_InsertEmployee 'Samiha','Islam','Jannat','2021/10/21','2021/10/21'
EXEC Sp_InsertEmployee 'Jannatul','Ferdous','Sumi','2021/10/21','2021/10/21'
EXEC Sp_InsertEmployee 'Abul','Mohammad','Jakir','2021/10/21','2021/10/21'
EXEC Sp_InsertEmployee 'Hasan','Mahmud','Jamil','2021/10/21','2021/10/21'
EXEC Sp_InsertEmployee 'Abul','Barkat','Somon','2021/10/21','2021/10/21'
GO