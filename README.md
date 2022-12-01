# takecontrol

# Docker for postgresql database

## Create Docker compose 

1. Looking at github repository.
[AspNetCorePostgreSQLDockerApp
](https://github.com/DanWahlin/AspNetCorePostgreSQLDockerApp)

### Starting a container with https support using docker compose

1. Generate certificate and configure local machine:

```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p { paco1234 }
dotnet dev-certs https --trust
```

1. Create `docker-compose.yml` file. ()
...