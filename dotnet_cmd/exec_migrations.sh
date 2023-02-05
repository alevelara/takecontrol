#! /bin/bash

echo "Execute migration for takecontrol.Infrastructure"

pushd ../TakeControl/takecontrol.Infrastructure;
dotnet ef database update;
popd;

echo ""
echo "------------------"
echo "Execute migration for takecontrol.EmailEngine"
pushd ../TakeControl/takecontrol.EmailEngine;
dotnet ef database update;
popd;

echo ""
echo "------------------"
echo "Execute migration for takecontrol.EmailEngine"
pushd ../TakeControl/takecontrol.EmailEngine;
dotnet ef database update;
popd;

echo ""
echo "------------------"
echo "Execute migration for takecontrol.Identity "
pushd ../TakeControl/takecontrol.Identity;
dotnet ef database update;
popd;