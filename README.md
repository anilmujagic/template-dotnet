# .NET Core Application Template

This is a template for a typical .NET Core server application, serving as a quick start for a new project.

The template covers following aspects:

- Database quick start:
    - Scripts for creating a database and setting initial permissions.
    - Simple manual change-scripts workflow.
    - A script for reseting the database and re-applying all the change-scripts.
- Generating model classes and Entity Framework [DAL](https://en.wikipedia.org/wiki/Data_access_layer) mapping from DB schema.
- Implementation of a generic Repository and Unit of Work patterns, for abstracting away the EF [DAL](https://en.wikipedia.org/wiki/Data_access_layer).
- Wiring up the dependency injection.
- Build script (only bash at the moment).


## Prerequisites

- .NET Core (template is based on v3.1, but it's easy to adjust to different version)
- F# interactive command line tool (`dotnet fsi`)
- PostgreSQL


## Quick Start

Let's assume you're starting a new `MyAwesomeNewProject` and want to use the template for it.

- Get the code

    ```bash
    # Clone the repo
    $ git clone --depth=1 https://github.com/anilmujagic/template-dotnet.git MyAwesomeNewProject

    # Go into the repo dir
    cd MyAwesomeNewProject

    # Remove the .git directory corresponding to this repo
    $ rm -rf .git

    # Optionally initialize your new git repo
    $ git init
    ```

- Rename the code

    Make sure you're in the repo root directory.
    You're changing the `MyApp` name used in the template to the name of your project: `MyAwesomeNewProject`

    ```bash
    $ dev/rename_code.sh MyApp MyAwesomeNewProject
    ```

    Output looks like this:

    ```
    Old name: MyApp
    New name: MyAwesomeNewProject
    Old DB name: my_app
    New DB name: my_awesome_new_project
    Renaming directories...
    Renaming files...
    Renaming code...
    Renaming DB...
    ```

- Create the DB

    ```bash
    $ psql -f dev/reset_db.sql

    # If user is not configured in PostgreSQL, you might have to use sudo:
    $ sudo -u postgres psql -f dev/reset_db.sql
    ```

    This will create DB user(s) on PostgreSQL server, create a DB (`my_awesome_new_project`) and set necessary permissions, as well as execute any change scripts.

- Run it

    ```bash
    $ cd src/MyAwesomeNewProject.Api
    $ dotnet run
    ```

- Try it

    ```bash
    # Add some test data into the DB
    $ psql -d my_awesome_new_project -c "INSERT INTO app.item (name, is_processed) values ('Alice', TRUE), ('Bob', FALSE)"

    # Query the API
    $ curl http://localhost:5000/items
    ```

    Output:

    ```
    [{"itemId":1,"name":"Alice","isProcessed":true},{"itemId":2,"name":"Bob","isProcessed":false}]
    ```
