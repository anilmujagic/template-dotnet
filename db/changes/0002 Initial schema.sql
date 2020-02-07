\set ON_ERROR_STOP on

SET search_path TO app, public;

DO $$
BEGIN
    ----------------------------------------------------------------------------------------------------
    -- item
    ----------------------------------------------------------------------------------------------------
    CREATE TABLE item (
        item_id bigint GENERATED BY DEFAULT AS IDENTITY NOT NULL,
        name text NOT NULL,
        is_processed boolean NOT NULL,

        CONSTRAINT item__pk PRIMARY KEY (item_id),
        CONSTRAINT item__uk__name UNIQUE (name)
    );

    ----------------------------------------------------------------------------------------------------
    -- database_version entry
    ----------------------------------------------------------------------------------------------------
    INSERT INTO database_version (version_number, description)
    VALUES (2, 'Initial schema');
END
$$;
