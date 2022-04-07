# ROBOT_apocalypse

How to Run Application

Setp 1. Modify SqlServer Connection in the AppSetting.Json (DefaultConnection).
Step 2. Build the application
Step 3. Run the application using VS2019
Step 4. Swagger page will open by default which conatins documentation of apis. User can test all the api from here also.

All End points - 
1. Add survivors to the database
    POST:https://localhost:44309/api/v1/Survivor/AddSurvivor
    Payload: '{
    "id": 0,
    "name": "string",
    "age": 0,
    "gender": "string",
    "latitude": "string",
    "longitude": "string",
    "inventories": [
      {
        "id": 0,
        "inventoryName": "string",
        "noOfDays": 0
      }
    ]
  }'
  
2. Update survivor location
PATCH:https://localhost:44309/api/v1/Survivor/UpdateSurvivorLocation
Payload:
'{
  "survivorId": 1,
  "latitude": "string",
  "longitude": "string"
}'

3. Flag survivor as infected
POST:https://localhost:44309/api/v1/Survivor/ReportContaminatedSurvivor?survivorID=1&reporterSurvivorId=2

4. Percentage of infected survivors.
GET:https://localhost:44309/api/v1/Survivor/GetInfactedSurviourPercentage

5. Percentage of non-infected survivors.
GET:https://localhost:44309/api/v1/Survivor/GetNonInfactedSurviourPercentage

6. List of infected survivors
GET:https://localhost:44309/api/v1/Survivor/GetInfactedSurviours

7. List of non-infected survivors
GET:https://localhost:44309/api/v1/Survivor/GetNonInfactedSurviours

8. List of robots
GET: https://localhost:44309/api/v1/Robot/GetAllRobots

