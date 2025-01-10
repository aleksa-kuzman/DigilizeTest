# DigilizeTest
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Test12345678" -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server --> for the database

# OPEN api definition

https://localhost:7019/openapi/v1.json


## Import postman collection to testg
DigilizeTest - v1.postman_collection.json

# Note

Hello Guys,
I implemented 3/4 functionalities, I shoul've gone with event sourcing for no.4, 
but since I just switched environments from Win + Visual Studio -> Mint Linux  + VSCode
I implemented standard crud, since I was a bit afraid that I would lose time on my environment,
Looking back that was wrong, I could do event sourcing approach as well in another hour.


