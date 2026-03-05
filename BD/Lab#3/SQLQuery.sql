use master
go
create database X_MyBase
on primary
(name = N'X_MyBase_mdf', filename = N'D:\Labs_4_sem\BD\Lab#3\X_MyBase_mdf.mdf',
size = 10240Kb, maxsize = UNLIMITED, filegrowth=1024Kb)
log on
(name = N'X_MyBase_log', filename = N'D:\Labs_4_sem\BD\Lab#3\X_MyBase_log.ldf',
size=10240Kb, maxsize=2048Gb, filegrowth = 10%)
go