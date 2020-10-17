How to run the programm in docker:

1. open powershell in this folder and execute the following commands

2. docker pull cgoebel93/statmathpostgresample

3. docker-compose up -d --build

4. docker run --rm -it cgoebel93/statmathpostgresample



Check database:

1. open http://localhost:8080

2. login
	user = admin@admin.com
	password = admin

3. create server 
	host = postgreDB
	username = test
	password = test