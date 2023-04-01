#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set environment variables
ENV DockerHOME=/usr/src/Takecontrol
ENV DockerAPIPath=/Takecontrol.API
ENV DockerAPIProj=Takecontrol.API.csproj
ENV DockerAPP=/app
ENV DockerAPPDLL=Takecontrol.API.dll

# Set work directory
WORKDIR $DockerHOME

# Copy dependencies
COPY . $DockerHOME
RUN dotnet restore "${DockerHOME}${DockerAPIPath}"
COPY . $DockerHOME

WORKDIR "${DockerHOME}${DockerAPIPath}"
RUN dotnet build $DockerAPIProj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish $DockerAPIProj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Takecontrol.API.dll"]

EXPOSE 80
EXPOSE 443