USE master;
GO

IF DB_ID (N'DrugStore') IS NOT NULL
ALTER DATABASE DrugStore SET SINGLE_USER with ROLLBACK IMMEDIATE
IF DB_ID (N'DrugStore') IS NOT NULL
DROP DATABASE DrugStore;
GO

create database DrugStore collate Polish_CI_AS;
go
use DrugStore

create table [Manufacturers](
	[Id] int primary key identity,
	[Name] varchar(100),
	[Address] varchar(255),
	[City] varchar(50),
	[Country] varchar(50)
)

create table [Medicines] (
	[Id] int primary key identity,
	[Name] varchar(50) not null,
	[ManufacturerId] int not null,
	[Price] decimal(10, 3),
	[StockQty] int,
	[IsPrescription] bit,
	constraint FK_Medicines_Manufacturers foreign key ([ManufacturerId]) references [Manufacturers] (Id)
)

create table [Prescriptions] (
	[Id] int primary key identity,
	[CustomerName] varchar(100),
	[CustomerPesel] char(11) not null,
	[PrescriptionNumber] varchar(30)
)

create table [Orders] (
	[Id] int primary key identity,
	[CreatedOn] datetimeoffset not null	
)

create table [OrderItems] (
	[Id] int primary key identity,
	[OrderId] int not null,
	[MedicineId] int not null,
	[PrescriptionId] int,
	[Quantity] int null,
	[DeliveredOn] datetimeoffset,
	constraint FK_OrderItems_Orders foreign key ([OrderId]) references [Orders](Id),
	constraint FK_OrderItems_Medicines foreign key ([MedicineId]) references [Medicines](Id),
	constraint FK_OrderItems_Prescriptions foreign key ([PrescriptionId]) references [Prescriptions](Id)
)

insert into Manufacturers (Name, Address, City, Country) values ('Polfarma S.A.', 'Lecznicza 2', 'Warszawa', 'Polska');
insert into Manufacturers (Name, Address, City, Country) values ('Pracownia homeopatyczna sp. z o.o.', 'Leœna 20', 'Brzeg Dolny', 'Polska');
insert into Manufacturers (Name, Address, City, Country) values ('Pigu³a PPHU', 'G³ówna 34', 'Z¹bki', 'Polska');
insert into Manufacturers (Name, Address, City, Country) values ('Hurtownia leków "Med-hurt"', 'Cicha 2', 'Ciechanów', '');
insert into Manufacturers (Name, Address, City, Country) values ('Pfizer GmbH', 'GrosseStrasse 1', 'Linz', 'Niemcy');

insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Vitamina A', 4, 34.12, 32, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Vitamina B', 4, 12.00, 53, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Vitamina B 6', 1, 12.00, 435, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Vitamina B 12', 4, 9.12, 34, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Vitamina B 13', 1, 23.53, 42, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Vitamina B 14 Forte', 4, 23.43, 23, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Vitamina C Gigant 1000 mg', 4, 15.23, 532, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Multivitamina Pokrzywa', 2, 34.00, 3, 1);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Cardio Lek 40 mg tabletki', 3, 96.00, 4, 1);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Fostex 100+6 inhalator', 5, 96.20, 5, 1);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Lek na wszystko', 3, NULL , NULL , 1);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('£ó¿ko terapeutyczne', 4, 4232.23, NULL , NULL);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Brain Healer', 2, 834.00, 2, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Neomag Rozkurcz nogi', 5, 19.32, 43, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Neomag Skurcz Forte', 4, 23.35, 43, 0);