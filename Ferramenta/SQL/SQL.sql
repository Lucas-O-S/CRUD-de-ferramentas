use auladb
go

CREATE TABLE [dbo].[ferramentas]( 
 Id [int] NOT NULL  primary key identity (1,1), 
 descricao [varchar](50) NULL, 
 FabricanteId  int ); 
 go

 create table fabricantes(
	id int not null primary key identity(1,1),
	nome varchar(100)
 )
 go

 insert into fabricantes values ('Fabricante 01')
 go
insert into fabricantes values ('Fabricante 02')
go
insert into fabricantes values ('Fabricante 03')
go

 create or alter procedure sp_insert_ferramentas(
	@descricao varchar(50),
	@fabricanteId int
 )
 as
 begin
	insert into ferramentas (descricao, fabricanteId) values (@descricao, @fabricanteId)
 end
 go

  create or alter procedure sp_update_ferramentas(
	@id int,
	@descricao varchar(50),
	@fabricanteId int
 )
 as
 begin
	update ferramentas set descricao = @descricao, fabricanteId = @fabricanteId where id = @id
 end
 go

  create or alter procedure sp_delete_ferramentas(
	@id int
 )
 as
 begin
	delete ferramentas where id = @id
 end
 go

 create or alter procedure sp_consulta_ferramentas(
	@id int
 )
 as
 begin
	select * from ferramentas where id = @id
 end
 go

 
 create or alter procedure sp_lista_ferramentas
 as
 begin
	select * from ferramentas
 end
 go

  create or alter procedure sp_consulta_fabricantes(
	@id int
 )
 as
 begin
	select * from fabricantes where id = @id
 end
 go

 
 create or alter procedure sp_lista_fabricantes
 as
 begin
	select * from fabricantes
 end
 go

