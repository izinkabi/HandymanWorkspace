# Handyman System in .NET Core and SQL - 

## A system of three components implemented in SQL databse, .NET Core API, .NET Class Library and Two UI projects (Razor Pages and Blazor Server) . 

This is a progressive web solution for finding local service providers(Handymen), it also serves as tamplate for:


*.NET Identity configuration
* SQL CRUD
* Dependency injection
* MVVM Design Pattern
*.NET API CRUD
* Blazor JS Interlop
* Uploading a file using Blazor FileInput

## Branch (API_Beta-Version)

 ## Check the document
 
 Click <a href="" >here</a> for docs
 
 ## How to install
 
 1. Clone the repo, preferebly using Visual Studio 2022 Source Control
 2. Update the nuget packages 
 3. Open the database project and publish the database
 4. Copy the full path of the folder wwwroot/excel/images/service and paste it in appsettings.json as a value for "ExcelFileStorage"

## How to contribute

Anyone willing contribute is highly welcome, since this is a mini project written as a tamplate while touching on real life solutions there is room for  improvements and for external assistance.


## Known issues 

This is built from little to no design, hence there is an issue of allocating a local storage for excel files because it is a recently added feature that is still under review but there will be a better way to approach this
