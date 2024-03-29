# .NET Core Application Template

This is a template for a typical .NET Core application, serving as a quick start for a new project.
Although conceptualized as a server application, the template code is easily convertible to any type of application using database for data persistence.

The template covers following aspects:

- Database quick start:
    - Scripts for creating a database and setting initial permissions.
    - Simple manual change-scripts workflow.
    - A script for resetting the database and re-applying all the change-scripts.
- Generating model classes and Entity Framework [DAL](https://en.wikipedia.org/wiki/Data_access_layer) mapping from DB schema.
- Implementation of a generic Repository and Unit of Work patterns, for abstracting away the EF [DAL](https://en.wikipedia.org/wiki/Data_access_layer).
- Wiring up the dependency injection.
- Build script (only bash at the moment).


## Prerequisites

- .NET Core (template is based on v6.0, but it's easy to adjust to different version)
- F# interactive command line tool (`dotnet fsi`)
- PostgreSQL
- Bash shell


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

    # Initialize your new git repo
    $ git init
    $ git add .
    ```

- Rename the code

    Make sure you're in the repo root directory.
    You're changing the `MyApp` name used in the template to the name of your project: `MyAwesomeNewProject`

    ```bash
    $ dev/rename_code.sh MyApp MyAwesomeNewProject
    ```

    Output:

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

    Get rid of the irrelevant README content

    ```bash
    $ echo '# MyAwesomeNewProject' > README.md
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
    $ psql -d my_awesome_new_project -c "INSERT INTO app.item (name, is_processed) VALUES ('Foo', TRUE), ('Bar', FALSE)"

    # Query the API
    $ curl http://localhost:5000/items
    ```

    Output:

    ```
    [{"itemId":1,"name":"Foo","isProcessed":true},{"itemId":2,"name":"Bar","isProcessed":false}]
    ```

- Commit and push to your new GitHub repo

    Create `my-awesome-new-project` repo on GitHub first, and then:

    ```bash
    $ git add .
    $ git commit -m 'Initial commit'
    $ git remote add origin https://github.com/you/my-awesome-new-project.git
    $ git push -u origin master
    ```
