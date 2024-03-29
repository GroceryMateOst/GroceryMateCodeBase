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

### Database
After the steps in the set-up have been taken, the following steps must be carried out in order to start developing.
In the terminal, navigate to the grocery-mate-backend folder and execute the following command which will start/create the database.
```
docker-compose up
```

Incase you need to delete the database you can run the following commands.
If the container is still running:
```
docker-compose down
```
then
```
docker volum remove grocery-mate-backend_db
```

to delete the volume where the data is stored. **Remember** all this commands have to be run in the grocery-mate-backend folder!
Then open a second terminal in the same folder to start the backend and execute this command.

### Backend
If you start the application for the first time or you add some migration to the backend for the database you first need to run this command.
```
dotnet ef database update
```
To start the backend server you can use this command.
```
dotnet run
``` 

### Frontend
When starting the frontend, the following command must be executed the first time and after each time new dependencies (node modules) are added.
```
yarn install
```
after that you can start the application with
```
yarn dev
```
**Important:** The commands for the frontend must be made in the folder grocery-mate-frontend and also in a new terminal!

## Links and Connections
If you did the the steps above correct you should have access to following addresses
* **Frontend** [http://localhost:5173/](http://localhost:5173/)
* **Swagger** [https://localhost:7167/swagger](https://localhost:7167/swagger)
* **Database** Host: localhost Port: 5433 Username: postgres Password: postgres 
