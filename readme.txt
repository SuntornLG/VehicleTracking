
VehicleTracking Api Readme file.
--------------------------------

Start run with REST API name VehicleTracking.


The project has separated by multiple layers all of the parts are working corresponding to each other.
All builed with .net core fraemwork include REST API and class library with code first approach in entity framework core.

For main dependency
-- Microsoft.EntityFramworkCore(3.1.3) for data contact
-- Microsoft.IdentityModel.Tokens(6.5.0) for security
-- System.IndentityModel.Tokents.Jwt(6.5.0) for security
-- AutoMapper.Extension.Microsoft.DependencyInection(7.0.0) for mapping bettween domain object and data tranfer object
-- Swashbuckle for api document.
-- Nlog for logging.



-Start from first layer Project name VehicleTracking.
   This is the REST API main project it provides contact functionality such as Vehicle register, Record vehicle position,
   Get current vehicle position, Get position by period of time, User registration and Authentication user.

- Second layer project name Service.
  This is the business logic layer it work closely with VehicleTracking project and Repository project. 
  It setting at the middle between VehicleTracking and Repository.

- Third layer project name Repository. 
  This project work closely with business layer and Entities project. 
  The advance of Repository project is that it no need to know what kind of database. 
  It just provide contact and decouple from database.

- Fourth layer project name Entities.
  This project is low level layer it contain database layer and all data models, database context.

- Fifth project name LoggerService.
  This is the utility project provide logging behavior of the project include error, info and etc.


-- For Extensibility
   As develope code first approach we can add more properties to table easier by using database migration command

---
End of document