# TelePort
This is an asp.net core 3.1 project that enables to calculate measure between two cities.

The project contains repository pattern, service layers, dependency injection, poco classes, JWt Bearer token, MsSql, swager and async processes.

If you don't need to authorize please add a comment block to the authorize attribute and uncomment AllowAnonymous attribute above the CalculateMeasure method.

If you want to use Authorize attribute, you should set a database whose name is teleport_db then add a user table to the database that you can find the script of it from teleport.data layer => database_setup folder => database_setup.sql file. 
You need to set your database connection string to appsettings.json file.

After executing the script i mentioned above, you will have an "admin" user and it's password : 123456 
That is a sample user you can register new users or login with this user and get bearer token.

Calculate measure method is giving measurement between two cities. 
in this method ReturnValueType is an optional property if you give "km" the distance returned will be km type. The default value is mile.

Please don't hesitate to ask me any question about project and framework. My email address is : efecetir@gmail.com

Efecan Cetir
