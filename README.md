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
