# GroceryMate CodeBase

## Set-up

### Frontend
The frontend is a React single page application written in Typescript. For the development you have to install [NodeJS](https://nodejs.org/). If NodeJS is installed, [yarn](https://yarnpkg.com/) must be installed using [npm](https://www.npmjs.com/), which can be done with the following command.
```
npm install --global yarn
````


### Backend and DB
The backend is a .NET application, more precisely an [ASP.NET REST Api](https://dotnet.microsoft.com/en-us/apps/aspnet/apis). Which persists the data in a relational form via the [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/). So to run the backend you have to install the [Microsoft .NET SDK](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net70). After that navigate over a terminal into the folder grocery-mate-backend and run following command. 

```
dotnet restore
```
In this project the database is integrated via a dockerized [Postgres-Db](https://www.postgresql.org/). To run it you have to install [Docker](https://www.docker.com/) on your Computer.

## Development

After the steps in the set-up have been taken, the following steps must be carried out in order to start developing.
In the terminal, navigate to the grocery-mate-backend folder and execute the following command which will start/create the database.
```
docker-compose up
```
Then open a second terminal in the same folder to start the backend and execute this command.
```
dotnet run
``` 

When starting the frontend, the following command must be executed the first time and after each time new dependencies are added.
```
yarn install
```
after that you can start the application with
```
yarn dev
```
**Important:** The commands for the frontend must be made in the folder grocery-mate-frontend and also in a new terminal!
