#! /bin/bash

echo "Remove migration for takecontrol.Infrastructure"

pushd ../TakeControl/TakeControl.User.Infrastructure/Migrations;
find -type f -name '*migrations_*' -delete;
popd;

echo ""
echo "------------------"
echo "Remove migration for takecontrol.EmailEngine"
pushd ../TakeControl/TakeControl.Emails.Infrastructure/Migrations;
find -type f -name '*migrations_*' -delete;
popd;

echo ""
echo "------------------"
echo "Remove migration for takecontrol.Identity "
pushd ../TakeControl/TakeControl.Credential.Infrastructure/Migrations;
find -type f -name '*migrations_*' -delete;
popd;