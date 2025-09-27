echo on
IF NOT EXIST "local\.env" (copy /b docker-env-shared + local\docker-env-local local\.env)

cd local

docker compose pull
docker-compose -f docker-compose.yaml up --build
@echo off
pause