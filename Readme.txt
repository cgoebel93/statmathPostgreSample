
1. Powershell im Repository-Verzeichnis öffnen
2. Programm builden:
	docker build -t consoleapp .
2. Oder Programm pullen:
	docker pull cgoebel93/statmathpostgresample:latest
3. Services erstellen: 
	docker-compose up -d --build
3. Projekt starten:
	docker run --rm -it consoleapp