#!/bin/sh

rm -rf ./ChatApp/bin

dotnet publish -c Release -r linux-x64  --self-contained ./ChatApp

