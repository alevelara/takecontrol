#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set environment variables
ENV DockerAPIPath=/takecontrol.API
ENV DockerAPP=/app
ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development

# Set work directory
WORKDIR $DockerAPP

# Copy dependencies
COPY . .

WORKDIR "${DockerAPP}${DockerAPIPath}"

ENTRYPOINT ["dotnet", "watch", "run"]

EXPOSE 5167