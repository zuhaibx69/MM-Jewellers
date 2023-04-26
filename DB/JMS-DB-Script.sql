
create database JewelleryManagementSystem

use JewelleryManagementSystem

create table Jeweller
(
	jeweller_id varchar(25) not null primary key,
	jeweller_name varchar(50) not null,
	jeweller_password varchar(20) not null

);

insert into Jeweller values ('A1', 'Huzaifa Admin', '1234');
insert into Jeweller values ('A2', 'Admin', '1234');
select * from Jeweller


create table Vendor
(
	vendor_id int primary key identity(1,1),
	vendor_name varchar(50) not null,
	vendor_email varchar(50) not null unique,
	vendor_password varchar(20) not null,
	vendor_contact varchar(11) not null unique,
	vendor_licenseNo varchar(10)

);
insert into Vendor Values('zuhaib','zuhaib@gmail.com','123','04323123121','V1')
select * from Vendor


create table Product
(
	prd_id int primary key identity(1,1),
	prd_name varchar(50) not null unique,
	prd_description varchar(50),
	prd_unitPrice int not null,
	prd_unit varchar(50) not null
	
);

insert into Product values ('Gold', 'Raw Material', 15000, 'Grams');
select * from Product


create table Inventory
(
	prd_id int foreign key references Product(prd_id) primary key,
	prd_quantity int not null,
	prd_price int not null

);

insert into Inventory values (1, 0, 0);
select * from Inventory

create table Quotation_Request
(
	req_id int primary key identity(1,1),
	prd_id int foreign key references Product(prd_id),
	prd_quantity int not null,
	order_deadline date not null
);

insert into Quotation_Request values(1,5,'5/5/2002')
select * from Quotation_Request

create table Quotation_Response
(
	req_id int foreign key references Quotation_Request(req_id),
	vendor_id int foreign key references Vendor(vendor_id),
	price_perunit int not null,

	primary key (req_id, vendor_id)

);

create table PurchaseOrder
(
	order_id int foreign key references Quotation_Request(req_id) primary key,
	vendor_id int foreign key references Vendor(vendor_id)

);

create table Payment
(
	order_id int foreign key references PurchaseOrder(order_id),
	payment_type varchar(20) not null,
	netprice int not null,

	primary key(order_id , payment_type)

);

