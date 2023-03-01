#! /bin/bash

echo "Removing database"
docker exec -it postgres_container psql -U padel -d TakeControl -c "DROP SCHEMA public CASCADE;CREATE SCHEMA public;"

echo "Execute migration for takecontrol.Infrastructure"

pushd ../TakeControl/takecontrol.Infrastructure;
dotnet ef migrations add migrations_1;
dotnet ef database update;
popd;

echo ""
echo "------------------"
echo "Execute migration for takecontrol.Identity "
pushd ../TakeControl/takecontrol.Identity;
dotnet ef migrations add migrations_1;
dotnet ef database update;
popd;

echo ""
echo "------------------"
echo "Execute migration for takecontrol.EmailEngine"
pushd ../TakeControl/takecontrol.EmailEngine;
dotnet ef migrations add migrations_1;
dotnet ef database update;
popd;
