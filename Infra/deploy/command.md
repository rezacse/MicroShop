# 1. Build the image

//../../MicroShop.Services.AuthAPI/

docker build -f Dockerfile.dev -t rezacse08/dev-auth-api:latest .

# 2. Tag with docker hub

docker tag dev-auth-api:latest rezacse08/dev-auth-api:latest

# 3. Push to docker hub

docker push rezacse08/dev-auth-api:latest

# 2. Load it into Kubernetes (Docker Desktop uses the same daemon)

kubectl apply -f deployment.dev.yaml

# 3. Check status

kubectl get pods

# 4. Test

curl http://localhost:30080

# debug the image to run

docker run -it --rm <image_name>:<tag> /bin/bash
