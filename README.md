# TscTest

This Repository contains a solution made for the **TSC technical evaluation (JOB)** 

Requirement: The creation of a REST JSON API is requested, for country administration https://en.wikipedia.org/wiki/ISO_3166-1 and its subdivisions.

This solution contains the following projects below:

- Core (DLL)
- Dal (DLL)
- Tests (XUnit)
- Web Api
- Web App (React)

  # How To setup the Database:

- You need to configure the ConnString path in the **appsettings.json** inside the Web Api project, add your server ip and user credentials, you can set any database name, it will be created automatically.
- Open the console and navigate into the DAL project, then you need to execute the next command:

> dotnet ef database update

The previous command will execute the migrations to create the database and populate it with the seed data defined.

Now you can run the Web Api and the Web Api projects at the same time for tests.