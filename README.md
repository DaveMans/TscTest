# TscTest

This Repository related about the **TSC technical evaluation (JOB)**

It contains these .net core projects:

- Core (DLL)
- Dal (DLL)
- Test
- Web Api
- Web App

  # How To setup the Database:

  - You need to configure the ConnString path in the **appsettings.json** inside the Web Api project, add your server ip and user credentials, you can set any database name, it will be created automatically.

- Open the console and navigate into the DAL project, then you need to execute the next command:

> dotnet ef database update

The previous command will execute the migrations to create the database and populate it with the seed data defined.

Now you can run the Web Api and the Web Api projects at the same time for tests.