# Doorz

**Doorz** est une application web personnel full-stack conçue pour centraliser tout ce dont un étudiant a besoin pour sa vie académique et professionnelle.

## 🎯 Objectif du projet
Doorz vise à regrouper sur une même plateforme plusieurs fonctionnalités essentielles :
- Offres de stages et d’emplois  
- Aides financières et bourses  
- Événements et formations  
- Kot à louer  

## 🧩 Fonctionnalités clés
- **Système d’invitations** : permet aux entreprises, écoles et organisations partenaires de rejoindre la plateforme.  
- **Gestion des rôles et permissions** : modèle d’accès détaillé (administrateurs, étudiants, entreprises, etc.) pour assurer la sécurité et la clarté des responsabilités.  
- **Architecture modulaire** : séparation claire entre le front-end, le back-end et la base de données.  
- **Sécurité et scalabilité** : intégration d’authentification JWT, Docker Compose et bonnes pratiques de développement.  

## ⚙️ Stack technique
- **Backend** : .NET 8 (Web API)  
- **Frontend** : Angular  17
- **Base de données** : MySQL  
- **Conteneurisation** : Docker Compose  

## 🚧 État du projet
Le développement est **en cours**.  
Authentification / Formulaires d'inscription, d'invitations en attribuant un role, de contact/ Système pour RGFPD => Complètes
De nouvelles fonctionnalités sont régulièrement ajoutées 



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






