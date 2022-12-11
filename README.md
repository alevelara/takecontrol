# takecontrol

## Docker for postgresql database

### Create Docker compose 

1. Looking at github repository.
[AspNetCorePostgreSQLDockerApp
](https://github.com/DanWahlin/AspNetCorePostgreSQLDockerApp)

### Running docker compose

1. Type the path where `docker-compose.yml` file is located.
2. Execute de command:

```
docker compose up -d
```

3. Setting up pgadmin:
- URL: `http:localhost:5050`
- Hostname: `postgres_container`
- username: `padel`
- password: `<defined in .env file>`

4. The swagger is not enabled and API is listening in port `80`.

### Running docker compose in development mode

In this mode, the dotnet command will be executed to keep watching the changes in folder and it is not necessary to build the docker image. The command is:

```
docker compose -f docker-compose.dev.yml up -d
```

The API will be listening in the url `http://localhost:5167/` and swagger is enabled by accessing to `http://localhost:5167/swagger`.

## Apply format style

1. Install dotnet format command.

```
dotnet tool install --global dotnet-format
```

2. Apply format to project into project path `./*.sln`.
```
dotnet-format
```