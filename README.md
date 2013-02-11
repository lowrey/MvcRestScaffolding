MVC REST Scaffolding
=========

This project is a simple job-based REST server done with ASP.NET MVC.  By itself, it does not do much but it is easily extendable 
for any sort of project where you need to create some requests, do some processing on them, and then expose the result on a server.  
You can create jobs, edit jobs, delete jobs, and view jobs via the standardized CRUD actions define in REST architecture.  
Jobs contain the following (but are designed for easy extendablity):

  - An ID
  - A callback URL
  - A status
  - Data

Each job is added to a SQLite database and immediately put into a seperate thread for *processing*.  
The scaffolding only currently changes the status immediately to *processed*.

Tech
-----------

Various external libraries are used in order to get this scaffolding up, all which can be found on NuGet:

  - Entity Framework 
  - SQLite
  - Log4net
  - JSON.NET 
  - RESTSharp

Installation
--------------

1. Clone the repo
2. Download missing packages from NuGet
3. Run ASP.NET test webserver
4. Run tests


License
-

MIT
  
    