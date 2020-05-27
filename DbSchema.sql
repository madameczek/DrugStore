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
insert into Manufacturers (Name, Address, City, Country) values ('American NoProblem Corp.', 'Green House', 'Washingtown', 'USA');
insert into Manufacturers (Name, Address, City, Country) values ('3M Poland', 'Warszawska 345', 'Nadarzyn', 'Polska');

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
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Vitamina B29 Fortress', 6, 999.950, 23, 1);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Nolpaza 20 mg', 4, 33.300, 4, 1);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Slow-Mag 60 kaps.', 5, 23.230, 4, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Woda utleniona 2%', 3, 4.120, 55, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Maseczka 3M z filtrem', 7, 23.500, 45, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Maseczka 3M jednorazowa', 7, 2.600, 99, 0);
insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) values ('Filtr do maseczki 3M', 7, 2.780, 234, 0);

insert into Orders (CreatedOn) values ('2020.05.26 13:46:14 +02:00');
insert into Orders (CreatedOn) values ('2020.05.20 14:19:28 +02:00');
insert into Orders (CreatedOn) values ('2020.05.26 14:23:25 +02:00');

insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (1, 1, NULL, 2, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (1, 1, NULL, 2, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (1, 4, NULL, 1, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (1, 7, NULL, 2, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (1, 4, NULL, 1, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (2, 15, NULL, 1, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (2, 13, NULL, 1, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (2, 7, NULL, 3, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (2, 3, NULL, 2, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (2, 2, NULL, 1, NULL);
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (3, 7, NULL, 0, '2020.05.26 14:24:01 +02:00');
insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) values (3, 15, NULL, 1, NULL);