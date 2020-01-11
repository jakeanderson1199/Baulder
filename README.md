# Bauld

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 8.0.3.

The server is written using C# and uses ASP.NET Core as a web framework

Bauld is a web application intended to mimic the popular board game Balderdash. In the game multiple players are asked to give a defition to a funny word, or a description for why a person was famous. These responses are then displayed to all players and they are asked to vote for what they think the correct description is. The Bauld application eliminates the need for a judge because the server is able to check what the correct answer is and allocate points accordingly. 

The application is currently playable but still has many bugs that need to be fixed. The main reason for this project was for me to increase my knowledge of Web Frameworks and to better understand the development process of a Web Application. As I learn more about Web Development, I will improve the game by decreasing the amount of bugs that occur.

## Development server
To run the project in development mode using Visual Studio Code follow these steps.

Run 
```sh 
ng serve 
``` 
to start the Angular server which runs on port 4200. 

Run C# server by pressing F5.

Requests not filled by the C# server will be proxied to port 4200 and fulfilled by the Angular server.

Navigate to `http://localhost:5000/`. The app will automatically reload if you change any of the source files.

## Deployment

To build the Angular application navigate to the Bauld.Web/ClientApp directory and run 
```sh 
ng build --prod 
``` 

This will compile the Angular application and place the files in the ClientApp/dist directory

To build the C# server run 
```sh 
dotnet publish -c Release. 
``` 
The C# server source code will be placed under Bauld.Web/bin/Release/netcoreapp2.2

Copy the ClientApp/dist folder into the bin/Release/netcoreapp2.2/publish folder

The publish folder now has all the files required to run the application

To test locally go to publish folder and run 
```sh 
dotnet Bauld.Web.dll
``` 

To publish to Azure, right click on the web app under the Azure extension in Visual Studio Code and click Deploy to Web App

Select the publish folder and deploy, currently only works using the Windows OS
