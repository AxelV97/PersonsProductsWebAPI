USE [PersonsProductsDb]
GO

USE [PersonsProductsDb]
GO

select *
from dbo.People
inner join dbo.Addresses on dbo.People.Id = dbo.Addresses.PersonId
inner join dbo.Products on dbo.People.Id = dbo.Products.PersonId

select *
from People

select *
from Addresses

select *
from Products


--DELETE FROM People
--DELETE FROM Addresses
--DELETE FROM Products
