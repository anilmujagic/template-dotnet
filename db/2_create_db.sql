\set ON_ERROR_STOP on

\c postgres

CREATE DATABASE my_app;
\c my_app

SET search_path TO public;

-- Create extensions
-- CREATE EXTENSION adminpack;
CREATE EXTENSION pgcrypto; -- UUID (Guid) Support

-- Create schemas
CREATE SCHEMA IF NOT EXISTS app;

-- Set default permissions
ALTER DEFAULT PRIVILEGES
GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO my_app_api;
ALTER DEFAULT PRIVILEGES
GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO my_app_worker;

ALTER DEFAULT PRIVILEGES
GRANT SELECT, USAGE ON SEQUENCES TO my_app_api;
ALTER DEFAULT PRIVILEGES
GRANT SELECT, USAGE ON SEQUENCES TO my_app_worker;

-- Set permissions on schemas
GRANT USAGE ON SCHEMA public TO my_app_api;
GRANT USAGE ON SCHEMA public TO my_app_worker;

GRANT USAGE ON SCHEMA app TO my_app_api;
GRANT USAGE ON SCHEMA app TO my_app_worker;
