select * from Products_Source
select * from Products_Destination

delete from Products_Destination

Create Table Products_Source
(
 [Id] int primary key,
 [Name] nvarchar(50),
 [Description] nvarchar(250)
)
GO

Declare @Id int
Set @Id = 1

While(@Id <= 300000)
Begin
 Insert into Products_Source values
 (@Id, 'Product - ' + CAST(@Id as nvarchar(20)),
 'Product - ' + CAST(@Id as nvarchar(20)) + ' Description')

 Print @Id
 Set @Id = @Id + 1
End
GO

Create Table Products_Destination
(
 [Id] int primary key,
 [Name] nvarchar(50),
 [Description] nvarchar(250)
)
GO