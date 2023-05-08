
create database JewelleryManagementSystem

use JewelleryManagementSystem

create table Jeweller
(
	jeweller_id varchar(25) not null primary key,
	jeweller_name varchar(50) not null,
	jeweller_password varchar(20) not null

);

insert into Jeweller values ('A1', 'Huzaifa Admin', '1234');
insert into Jeweller values ('A2', 'Awais Admin', '4321');

select * from Jeweller

create table Vendor
(
	vendor_id int primary key identity(1,1),
	vendor_name varchar(50) not null,
	vendor_email varchar(50) not null unique,
	vendor_password varchar(20) not null,
	vendor_contact varchar(11) not null unique,
	vendor_cnic varchar(15) not null unique,
	vendor_address varchar(200) not null,
	vendor_tax int not null,
	vendor_bank_name varchar(100) not null,
	vendor_acc_title varchar(50) not null,
	vendor_acc_no varchar(30) not null unique,
	vendor_licenseNo varchar(10),
	vendor_licenseImg image,
	vendor_status varchar(12) default 'Non-verified'

);

select * from Vendor

create table Product
(
	prd_id int primary key identity(1,1),
	prd_name varchar(50) not null,
	prd_description varchar(50),
	prd_unit varchar(50) not null
	
);

insert into Product values ('Gold', 'Raw Material', 'Grams');
insert into Product values ('Gold', 'Raw Material', 'KG');

select * from Product

create table Quotation_Request
(
	req_id int primary key identity(1,1),
	prd_id int foreign key references Product(prd_id),
	jeweller_id varchar(25) foreign key references Jeweller(jeweller_id),
	prd_quantity int not null,
	order_deadline date not null
);

select * from Quotation_Request

create table Quotation_Response
(
	req_id int foreign key references Quotation_Request(req_id),
	vendor_id int foreign key references Vendor(vendor_id),
	price_perunit int not null,

	primary key (req_id, vendor_id)

);

select * from Quotation_Response

create table PurchaseOrder
(
	order_id int foreign key references Quotation_Request(req_id) primary key,
	vendor_id int foreign key references Vendor(vendor_id),
	total_amount int not null

);

select * from PurchaseOrder

create table Payment
(
	order_id int foreign key references PurchaseOrder(order_id),
	payment_type varchar(20) not null,
	netprice int not null,

	primary key(order_id , payment_type)

);

select * from Payment

create table Inventory
(
	order_id int foreign key references PurchaseOrder(order_id) primary key,
	prd_name varchar(50) not null,
	prd_description varchar(50),
	prd_quantity int not null,	
	prd_unit varchar(50) not null,
	total_amount int not null

);

select * from Inventory

create table GRN
(
	order_id int foreign key references PurchaseOrder(order_id) primary key,
	vendor_name varchar(50) not null,
	prd_name varchar(50) not null,
	prd_quantity int not null,
	prd_unit varchar(10) not null,
	total_amount int not null,
	receiving_date date not null

);

select * from GRN
