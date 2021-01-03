# Logging Demo

## Prerequisites
docker
docker-compose

## Optional
curl (required for running send-requests.sh)

## Running the project
1. Open the project directory
2. Run `docker-compose up` to launch the services
3. Open requests.http and send some requests (requires VSCode REST Client extension)
   Alternatively, you can run send-requests.sh to automate this

## Viewing log output
1. Navigate to [Kibana](http://localhost:5601/app/discover)
2. Create a new index `app-*`
3. Set the timestamp field to `@timestamp`
4. Navigate to Management -> Index Patterns -> app-*
5. Click the refresh icon in the top right to refresh the field list
6. Navigate to the "Discover" tab and run some queries