
--CREATE DATABASE DemoDatabase

--CREATE TABLE Employee(
--EmpId INT PRIMARY KEY IDENTITY(1,1),
--EmpName VARCHAR(100),
--EmpEmailId VARCHAR(50),
--EMPAddress VARCHAR(100))

--INSERT INTO Employee (EmpName,EmpEmailId,EMPAddress)
--VALUES ('Chaitali','chaitali.narkhede1991@gmail.com','Pune')


--CREATE TABLE UserInfo(
--UserId INT PRIMARY KEY IDENTITY(1,1),
--UserName VARCHAR(100),
--Passw VARCHAR(100),
--FirstName VARCHAR(100),
--LastName VARCHAR(100)
--)


--CREATE TABLE RefreshTokenId(
--RefreshTokenId INT PRIMARY KEY IDENTITY(1,1),
--UserId INT FOREIGN KEY REFERENCES UserInfo(UserId),
--RefreshTokenValue VARCHAR(500),
--ExpiryTime DATE
--)


--insert into UserInfo VALUES
--('chaitali.narkhede1991@gmail.com','abc','Chaitali','Narkhede')


select * from UserInfo