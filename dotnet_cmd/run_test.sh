#! /bin/bash

docker exec -it postgres_container psql -U padel -d TakeControlTest -c "DROP SCHEMA public CASCADE;CREATE SCHEMA public;"
dotnet test ./TakeControl/TakeControl.sln --filter Category=IntegrationTests