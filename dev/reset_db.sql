\set ON_ERROR_STOP on

\c postgres

DROP DATABASE IF EXISTS my_app;
DROP ROLE IF EXISTS my_app_api;
DROP ROLE IF EXISTS my_app_worker;

\ir '../db/1_init_server.sql'
\ir '../db/2_create_db.sql'
\ir '../db/3_apply_db_changes.sql'
