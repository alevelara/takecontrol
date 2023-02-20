#! /bin/bash

echo "Remove migration for takecontrol.Infrastructure"

pushd ../TakeControl/takecontrol.Infrastructure/Migrations;
find -type f -name '*migrations_*' -delete;
popd;

echo ""
echo "------------------"
echo "Remove migration for takecontrol.EmailEngine"
pushd ../TakeControl/takecontrol.EmailEngine/Migrations;
find -type f -name '*migrations_*' -delete;
popd;

echo ""
echo "------------------"
echo "Remove migration for takecontrol.Identity "
pushd ../TakeControl/takecontrol.Identity/Migrations;
find -type f -name '*migrations_*' -delete;
popd;