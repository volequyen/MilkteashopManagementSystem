create database QLTiemtrasuatakeaway
go

use QLTiemtrasuatakeaway
go

--drink
--drinkCategory
--Account
--Bill
--BillInfor

create table Account
(
	username nvarchar(100)  primary key,
	displayname nvarchar(100) not null,
	password nvarchar(1000)  not null default 1
)
go

ALTER TABLE Account
ADD CONSTRAINT DF_Account_Password DEFAULT '1' FOR password;


create table drinkCategory
(
	id int identity primary key,
	name nvarchar(100) not null
)
go

create table drink
(
	id int identity primary key,
	name nvarchar(100) not null,
	idCategory int not null,
	price int not null,
	foreign key (idCategory) references dbo.drinkCategory(id)
)
go

create table bill
(
	id int identity primary key,
	dateofbill date not null,
	status int not null default 0 --1 la da thanh toan, 0 la chua thanh toan
	
)
go

create table billInfor
(
	id int identity primary key,
	idBill int not null,
	idDrink int not null,
	count int not null default 0
	foreign key (idBill) references dbo.bill(id),
	foreign key (idDrink) references dbo.drink(id)
)
go

create table admin
(
	username nvarchar(100)  primary key,
	displayname nvarchar(100) not null,
	password nvarchar(1000)  not null,
)
go

alter table dbo.bill add totalPrice int

alter proc USP_GetListBill
@dateofbill date
as
begin
	select b.id as [ID], b.dateofbill as [Ngày xuất hóa đơn], b.totalPrice as [Tổng tiền]
	from dbo.bill as b
	where b.dateofbill = @dateofbill and status = 1
end
go


CREATE PROCEDURE USP_UpdateAcc
    @username NVARCHAR(100), 
    @displayname NVARCHAR(100), 
    @password NVARCHAR(100), 
    @newpassword NVARCHAR(100)
AS
BEGIN
    DECLARE @rightPass INT = 0;
    SELECT @rightPass = COUNT(*) FROM Account WHERE username = @username AND password = @password;
    
    IF (@rightPass = 1)
    BEGIN
        IF (@newpassword IS NULL OR @newpassword = '')
        BEGIN
            UPDATE Account SET displayname = @displayname WHERE username = @username;
        END
        ELSE 
        BEGIN
            UPDATE Account SET displayname = @displayname, password = @newpassword WHERE username = @username;
        END
    END
END
GO

CREATE PROCEDURE USP_UpdateAd
    @username NVARCHAR(100), 
    @displayname NVARCHAR(100), 
    @password NVARCHAR(100), 
    @newpassword NVARCHAR(100)
AS
BEGIN
    DECLARE @rightPass INT = 0;
    SELECT @rightPass = COUNT(*) FROM admin WHERE username = @username AND password = @password;
    
    IF (@rightPass = 1)
    BEGIN
        IF (@newpassword IS NULL OR @newpassword = '')
        BEGIN
            UPDATE admin SET displayname = @displayname WHERE username = @username;
        END
        ELSE 
        BEGIN
            UPDATE admin SET displayname = @displayname, password = @newpassword WHERE username = @username;
        END
    END
END
GO

EXEC USP_UpdateAd @username = 'vlq', @displayname = 'Võ Lê Quyên', @password = '123', @newpassword = '1';




insert dbo.drink (name, idCategory, price) value 








