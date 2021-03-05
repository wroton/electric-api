CREATE TABLE Client.Clients
(
	Id INT NOT NULL IDENTITY (-2147483648, 1),
	[Name] NVARCHAR(200) NOT NULL,
	AddressLine1 VARCHAR(150) NOT NULL,
	AddressLine2 VARCHAR(150) NULL,
	City VARCHAR(100) NOT NULL,
	[State] CHAR(2) NOT NULL,
	ZipCode VARCHAR(10) NOT NULL
	CONSTRAINT [PK_Client_Clients] PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT [CK_Client_Clients_Name] CHECK (LTRIM(RTRIM([Name])) != ''),
	CONSTRAINT [CK_Client_Clients_AddressLine1IsPopulated] CHECK (LTRIM(RTRIM(AddressLine1)) != ''),
	CONSTRAINT [CK_Client_Clients_AddressLine2IsPopulated] CHECK (LTRIM(RTRIM(AddressLine2)) != ''),
	CONSTRAINT [CK_Client_Clients_CityIsPopulated] CHECK(LTRIM(RTRIM(City)) != ''),
	CONSTRAINT [CK_Client_Clients_StateIsPopulated] CHECK(LTRIM(RTRIM([State])) != ''),
	CONSTRAINT [CK_Client_Clients_ZipCodeIsPopulated] CHECK(LTRIM(RTRIM(ZipCode)) != '')
);
