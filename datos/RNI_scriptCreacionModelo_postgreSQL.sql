-- Scripts de clase - Mayo 21 de 2024 
-- Curso de T�picos Avanzados de base de datos - UPB 202410
-- Juan Dario Rodas - juand.rodasm@upb.edu.co

-- Proyecto: RNI - Reactores Nucleares de Investigaci�n
-- Motor de Base de datos: PostgreSQL 16.x

-- ***********************************
-- Abastecimiento de imagen en Docker
-- ***********************************
 
-- Descargar la imagen
docker pull postgres:latest

-- Crear el contenedor
docker run --name postgres-rni -e POSTGRES_PASSWORD=unaClav3 -d -p 5432:5432 postgres:latest

-- ****************************************
-- Creaci�n de base de datos y usuarios
-- ****************************************

-- Con usuario Root:

-- crear el esquema la base de datos
create database rni_db;

-- Conectarse a la base de datos
\c rni_db;

-- Creamos un esquema para almacenar todo el modelo de datos del dominio
create schema inicial;
create schema core;

-- crear el usuario con el que se implementar� la creaci�n del modelo
create user rni_app with encrypted password 'unaClav3';

-- asignaci�n de privilegios para el usuario
grant connect on database rni_db to rni_app;
grant create on database rni_db to rni_app;
grant create, usage on schema inicial, core to rni_app;
alter user rni_app set search_path to core;


-- crear el usuario con el que se conectar� la aplicaci�n
create user rni_usr with encrypted password 'unaClav3';

-- asignaci�n de privilegios para el usuario
grant connect on database rni_db to rni_usr;
grant usage on schema inicial, core to rni_usr;
alter default privileges for user rni_app in schema core grant insert, update, delete, select on tables to rni_usr;
alter default privileges for user rni_app in schema core grant execute on routines TO rni_usr;
alter user rni_usr set search_path to core;

