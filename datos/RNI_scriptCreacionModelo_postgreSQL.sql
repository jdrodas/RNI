-- Scripts de clase - Mayo 21 de 2024 
-- Curso de Tópicos Avanzados de base de datos - UPB 202410
-- Juan Dario Rodas - juand.rodasm@upb.edu.co

-- Proyecto: RNI - Reactores Nucleares de Investigación
-- Motor de Base de datos: PostgreSQL 16.x

-- ***********************************
-- Abastecimiento de imagen en Docker
-- ***********************************
 
-- Descargar la imagen
docker pull postgres:latest

-- Crear el contenedor
docker run --name postgres-rni -e POSTGRES_PASSWORD=unaClav3 -d -p 5432:5432 postgres:latest

-- ****************************************
-- Creación de base de datos y usuarios
-- ****************************************

-- Con usuario Root:

-- crear el esquema la base de datos
create database rni_db;

-- Conectarse a la base de datos
\c rni_db;

-- Creamos un esquema para almacenar todo el modelo de datos del dominio
create schema inicial;
create schema core;

-- crear el usuario con el que se implementará la creación del modelo
create user rni_app with encrypted password 'unaClav3';

-- asignación de privilegios para el usuario
grant connect on database rni_db to rni_app;
grant create on database rni_db to rni_app;
grant create, usage on schema inicial, core to rni_app;
alter user rni_app set search_path to core;


-- crear el usuario con el que se conectará la aplicación
create user rni_usr with encrypted password 'unaClav3';

-- asignación de privilegios para el usuario
grant connect on database rni_db to rni_usr;
grant usage on schema inicial, core to rni_usr;
alter default privileges for user rni_app in schema core grant insert, update, delete, select on tables to rni_usr;
alter default privileges for user rni_app in schema core grant execute on routines TO rni_usr;
alter user rni_usr set search_path to core;

-- ----------------------------------------
-- Script de creación de tablas y vistas
-- ----------------------------------------

-- ******************************************
-- Esquema inicial
-- ******************************************

-- Tabla datos_rni
create table inicial.datos_rni
(
    nombre_reactor varchar(100),
    ubicacion_pais varchar(100),
    ubicacion_ciudad varchar(100),
    tipo_reactor varchar(50),
    potencia_termica float,
    estado_reactor varchar(100),
    fecha_primera_reaccion varchar(50)
);

-- ******************************************
-- Esquema core
-- ******************************************

-- ######################
-- Tabla ubicaciones
create table core.ubicaciones
(
    id      integer generated always as identity constraint ubicaciones_pk primary key,
    ciudad  varchar(100) not null,
    pais    varchar(100) not null,
    constraint ubicaciones_ciudad_pais_uk unique (ciudad, pais)
);

comment on table core.ubicaciones is 'Ubicaciones de los reactores';
comment on column core.ubicaciones.id is 'id de la ubicación';
comment on column core.ubicaciones.ciudad is 'Nombre de la ciudad';
comment on column core.ubicaciones.pais is 'Nombre del pais';

-- Cargue de datos desde la tabla inicial
insert into core.ubicaciones (ciudad, pais)
select distinct ubicacion_ciudad, ubicacion_pais
from inicial.datos_rni
where ubicacion_pais in
(
    'Argentina',
    'Brazil',
    'Chile',
    'Colombia',
    'Jamaica',
    'Mexico',
    'Peru',
    'Uruguay',
    'Venezuela, Bolivarian Republic of'
);

-- ######################
-- Tabla tipos_reactores
create table core.tipos_reactores
(
    id      integer generated always as identity constraint tipos_reactores_pk primary key,
    nombre  varchar(50) not null constraint tipos_reactores_nombre_uk unique
);

comment on table core.tipos_reactores is 'Tipos de reactores';
comment on column core.tipos_reactores.id is 'id del tipo de reactor';
comment on column core.tipos_reactores.nombre is 'Nombre del tipo de reactor';

-- Cargue de datos desde la tabla inicial
insert into core.tipos_reactores (nombre)
select distinct tipo_reactor
from inicial.datos_rni;

-- ############################
-- Tabla estados_reactores
create table core.estados_reactores
(
    id      integer generated always as identity constraint estados_reactores_pk primary key,
    nombre  varchar(50) not null constraint estados_reactores_nombre_uk unique
);

comment on table core.estados_reactores is 'Estados de los reactores';
comment on column core.estados_reactores.id is 'id del estado de reactor';
comment on column core.estados_reactores.nombre is 'Nombre del estado de reactor';

-- Cargue de datos desde la tabla inicial
insert into core.estados_reactores (nombre)
select distinct estado_reactor
from inicial.datos_rni

-- ############################
-- Tabla reactores
create table core.reactores
(
    id                      integer generated always as identity constraint reactores_pk primary key,
    nombre                  varchar(100) not null,
    potencia_termica        float not null,
    ubicacion_id            integer not null constraint reactor_ubicacion_fk references core.ubicaciones,
    estado_id               integer not null constraint reactor_estado_fk references core.estados_reactores,
    tipo_id                 integer not null constraint reactor_tipo_fk references core.tipos_reactores,
    fecha_primera_reaccion  date
);

alter table reactores add constraint reactor_nombre_ubicacion_uk unique (nombre, ubicacion_id);

comment on table core.reactores is 'Reactores Nucleares de Investigación';
comment on column core.reactores.id is 'id del reactor';
comment on column core.reactores.nombre is 'Nombre del reactor';
comment on column core.reactores.potencia_termica is 'Potencia térmica en KW del reactor';
comment on column core.reactores.ubicacion_id is 'Id de la ubicación del reactor';
comment on column core.reactores.estado_id is 'Id del estado del reactor';
comment on column core.reactores.tipo_id is 'Id del tipo del reactor';
comment on column core.reactores.fecha_primera_reaccion is 'Fecha de la primera reacción del reactor';

-- Actualizamos FK para cargar posteriormente datos de reactores
alter table inicial.datos_rni add column estado_id integer default 0;
alter table inicial.datos_rni add column tipo_id integer default 0;
alter table inicial.datos_rni add column ubicacion_id integer default 0;

update inicial.datos_rni dr
set estado_id = (
    select id from core.estados_reactores er
              where er.nombre = dr.estado_reactor
    )
where dr.estado_id = 0;

update inicial.datos_rni dr
set tipo_id = (
    select id from core.tipos_reactores tr
              where tr.nombre = dr.tipo_reactor
    )
where dr.tipo_id = 0;

-- Cargue de datos para los reactores que todavía no han comenzado operacion
insert into core.reactores (nombre, potencia_termica, ubicacion_id, estado_id, tipo_id)
select rni.nombre_reactor, rni.potencia_termica, rni.ubicacion_id, rni.estado_id, rni.tipo_id
from inicial.datos_rni rni
where ubicacion_id is not null
and fecha_primera_reaccion = 'NA';

-- Cargue de datos para los reactores que están activos y están en las ubicaciones Latinoamericanas
insert into core.reactores (
    nombre, potencia_termica, ubicacion_id, 
    estado_id, tipo_id,fecha_primera_reaccion)
select 
    rni.nombre_reactor, rni.potencia_termica, rni.ubicacion_id, 
    rni.estado_id, rni.tipo_id, substr(rni.fecha_primera_reaccion,0,11)::date
from inicial.datos_rni rni
where ubicacion_id !=0
and fecha_primera_reaccion != 'NA';

-- ###########################
-- Vista v_info_reactores
create or replace view core.v_info_reactores as
(
    select distinct
        r.id reactor_id,
        r.nombre reactor_nombre,
        r.potencia_termica,
        u.id ubicacion_id,
        u.pais ubicacion_pais,
        u.ciudad ubicacion_ciudad,
        r.estado_id,
        er.nombre reactor_estado,
        r.tipo_id,
        tr.nombre reactor_tipo,
        r.fecha_primera_reaccion
    from core.reactores r
        join core.ubicaciones u on r.ubicacion_id = u.id
        join core.estados_reactores er on r.estado_id = er.id
        join core.tipos_reactores tr on r.tipo_id = tr.id
);

select *
from core.v_info_reactores;
