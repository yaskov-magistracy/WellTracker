cd cloud 
sudo docker compose pull
docker stop $(docker ps -q)
sudo docker compose -f docker-compose.yaml up --build -d