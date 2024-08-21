Name: Givindu Perera
Project Name: DxDyIntegrationTest

Fetch Data: The app will fetch data from the SWAPI API for Planets and Ships.
Modify Data: Use the UI to modify the data. Can edit any field available.
Submit Data: After modification, submit the data, which will be sent to a another endpoint.

Technologies Used: 

Backend: .NET Core 8
Frontend: React
Database: MSSQL
API Source: SWAPI
Version Control: GIT



Backend Setup

1. Clone the repository: 

git clone https://github.com/Givindu/Swapi_Data_Integration.git
cd DxDyIntegrationTest


2. Set up the database:

Run the script.sql file located in the /DB folder on your SQL database.
Update the connection string in the appsettings.json file with your database credentials.

3. Restore NuGet packages

4. Run the backend



Frontend Setup

1. Navigate to the frontend folder:

cd integration-frontend

2. Install dependencies:

Install dependencies:

3. Run the code:

npm start