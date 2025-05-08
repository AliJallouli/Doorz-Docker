#Important pour les certificats:
================================
	- Prérequis 
	***********
		mkcert --version
		oppenssl version
		
	- Executez le script:
	*********************
		Ouvrez PowerShell en tant qu'administrateur dans le dossier du docker compose
		Exécutez la commande: .\generate-certificates.ps1
		
	- En cas de problème avec la génération des certificats:
	********************************************************
		Certificat Backend (Si problème)
		--------------------------------
			Supprimer les fichiers dans C:\Users\alija\Desktop\Doorz\Doors-BE\WebApi\certs
			ouvrir Gitbash dans: C:\Users\alija\Desktop\Doorz\Doors-BE\WebApi\certs
			cd C:\Users\alija\Desktop\Doorz\Doors-BE\WebApi\certs
			mkcert -cert-file localhost.pem -key-file localhost-key.pem localhost
			openssl pkcs12 -export -out localhost.pfx -inkey localhost-key.pem -in localhost.pem -passout pass:password
			
		Certificat Frontend (Si problème)
		---------------------------------
			cd Doors-FE
			mkcert localhost
	

#Docker
=======
docker-compose down -v --remove-orphans
docker-compose build --no-cache
docker-compose up

#Docker links
=============
 - Front
  	https://localhost:4200/
 - Backend
	https://localhost:7200/swagger/index.html
 - Serveur pour email
	http://localhost:8025/#

#Docker logs
============
docker logs -f doors-backend
docker logs -f doors-frontend

#Docker Mysql
=============
docker exec -it mysql mysql -uroot -proot
use doors;

#Docker Environnements:
=======================
docker exec -it doors-backend printenv
docker exec -it doors-frontend printenv





DEV
===
 - Containers Docker
	MailHog: docker run -d --name devMailHog -p 1025:1025 -p 8025:8025 mailhog/mailhog
	RabbitMQ: docker run -d --name devRabbitMQ -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:3-management
 - Front
  	https://localhost:4200/
 - Backend
	https://localhost:7200/swagger/index.html
 - Serveur pour email
	http://localhost:8025/# ----> pour les email






