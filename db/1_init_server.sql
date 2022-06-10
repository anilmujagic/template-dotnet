\set ON_ERROR_STOP on

\c postgres

CREATE USER my_app_api WITH PASSWORD 'testpass1';
CREATE USER my_app_worker WITH PASSWORD 'testpass1';

-- Password should be changed on real environment
-- ALTER USER my_app_api WITH PASSWORD 'XXXXXXXXXX';
-- ALTER USER my_app_worker WITH PASSWORD 'XXXXXXXXXX';
