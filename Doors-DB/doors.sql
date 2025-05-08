SET NAMES utf8mb4;
SET CHARACTER SET utf8mb4;
SET collation_connection = 'utf8mb4_unicode_ci';
USE doors;

/*************************************************************************************************
    1. Tables de Référence (Indépendantes ou avec dépendances minimales)
**************************************************************************************************/

/* Table super_role : Rôles globaux des utilisateurs dans le système */
CREATE TABLE super_role (
    super_role_id INT AUTO_INCREMENT PRIMARY KEY,      -- Identifiant unique du rôle global
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du rôle (ex. : Admin, User)
    description TEXT,                                  -- Description du rôle
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur (sera lié à users plus tard)
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour (sera lié à users plus tard)
) ENGINE=InnoDB;

/* Table entity_types : Types d’entités dans le système */
CREATE TABLE entity_types (
    entity_type_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique du type d’entité
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Institution, Company)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table institution_type : Types d’institutions éducatives */
CREATE TABLE institution_type (
    institution_type_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du type d’institution
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Université)
    description VARCHAR(191) NULL,                     -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table community : Communautés éducatives */
CREATE TABLE community (
    community_id INT AUTO_INCREMENT PRIMARY KEY,       -- Identifiant unique de la communauté
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom de la communauté (ex. : Française)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table legal_status : Statuts juridiques des entités */
CREATE TABLE legal_status (
    legal_status_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique du statut juridique
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du statut (ex. : Public)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table education_level : Niveaux d’enseignement */
CREATE TABLE education_level (
    education_level_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du niveau d’enseignement
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du niveau (ex. : Supérieur)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table network : Réseaux d’enseignement */
CREATE TABLE network (
    network_id INT AUTO_INCREMENT PRIMARY KEY,         -- Identifiant unique du réseau
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du réseau (ex. : Officiel)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table authority : Autorités de tutelle */
CREATE TABLE authority (
    authority_id INT AUTO_INCREMENT PRIMARY KEY,       -- Identifiant unique de l’autorité
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom de l’autorité (ex. : Ministère)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table campus_type : Types de campus */
CREATE TABLE campus_type (
    campus_type_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique du type de campus
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Principal)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table company_type : Types d’entreprises */
CREATE TABLE company_type (
    company_type_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique du type d’entreprise
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : PME)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table study_level : Niveaux d’études des étudiants */
CREATE TABLE study_level (
    study_level_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique du niveau d’études
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du niveau (ex. : Bachelier)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur (sera lié à users)
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour (sera lié à users)
) ENGINE=InnoDB;

/* Table owner_type : Types de propriétaires pour les paiements */
CREATE TABLE owner_type (
    owner_type_id INT AUTO_INCREMENT PRIMARY KEY,      -- Identifiant unique du type de propriétaire
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Company, Landlord)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table housing_type : Types de logements */
CREATE TABLE housing_type (
    housing_type_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique du type de logement
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Appartement)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table peb_rating : Classifications énergétiques PEB */
CREATE TABLE peb_rating (
    peb_rating_id INT AUTO_INCREMENT PRIMARY KEY,      -- Identifiant unique de la classification
    name VARCHAR(10) NOT NULL UNIQUE,                  -- Nom de la classification (ex. : A, B)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table application_status : Statuts des candidatures */
CREATE TABLE application_status (
    application_status_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du statut
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du statut (ex. : En attente)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur (sera lié à users)
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour (sera lié à users)
) ENGINE=InnoDB;

/* Table offer_type : Types d’offres */
CREATE TABLE offer_type (
    offer_type_id INT AUTO_INCREMENT PRIMARY KEY,      -- Identifiant unique du type d’offre
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Stage, Job)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur (sera lié à users)
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour (sera lié à users)
) ENGINE=InnoDB;

/* Table contract_type : Types de contrats */
CREATE TABLE contract_type (
    contract_type_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique du type de contrat
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : CDD)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur (sera lié à users)
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour (sera lié à users)
) ENGINE=InnoDB;

/* Table schedule_type : Types d’horaires */
CREATE TABLE schedule_type (
    schedule_type_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique du type d’horaire
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Plein temps)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table spoken_language : Langues parlées */
CREATE TABLE spoken_language (
    language_id INT AUTO_INCREMENT PRIMARY KEY,        -- Identifiant unique de la langue
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom de la langue (ex. : Français)
    code VARCHAR(5) NOT NULL UNIQUE,                   -- Code ISO de la langue (ex. : fr)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur (sera lié à users)
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour (sera lié à users)
) ENGINE=InnoDB;

/* Table duration_unit : Unités de durée */
CREATE TABLE duration_unit (
    duration_unit_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique de l’unité de durée
    name VARCHAR(20) NOT NULL UNIQUE,                  -- Nom de l’unité (ex. : mois)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur (sera lié à users)
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour (sera lié à users)
) ENGINE=InnoDB;

/* Table facility_type : Types d’équipements */
CREATE TABLE facility_type (
    facility_type_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique du type d’équipement
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Bibliothèque)
    description TEXT,                                  -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table degree_category : Catégories de diplômes */
CREATE TABLE degree_category (
    degree_category_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de la catégorie
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom de la catégorie (ex. : Sciences)
    description TEXT,                                  -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table tuition_type : Types de frais d’inscription */
CREATE TABLE tuition_type (
    tuition_type_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique du type de frais
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Annuel)
    description TEXT,                                  -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table cycle : Cycles d’enseignement */
CREATE TABLE cycle (
    cycle_id INT AUTO_INCREMENT PRIMARY KEY,           -- Identifiant unique du cycle
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du cycle (ex. : Premier cycle)
    description TEXT,                                  -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table certification_type : Types de certifications */
CREATE TABLE certification_type (
    certification_type_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du type de certification
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Certificat)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table delivery_mode : Modes de délivrance des formations */
CREATE TABLE delivery_mode (
    delivery_mode_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique du mode de délivrance
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du mode (ex. : Présentiel)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table mime_type : Types MIME pour les fichiers */
CREATE TABLE mime_type (
    mime_type_id INT AUTO_INCREMENT PRIMARY KEY,       -- Identifiant unique du type MIME
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : image/jpeg)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table amenity : Commodités disponibles dans les logements */
CREATE TABLE amenity (
    amenity_id INT AUTO_INCREMENT PRIMARY KEY,         -- Identifiant unique de la commodité
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom de la commodité (ex. : Wi-Fi)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table notification_type : Types de notifications */
CREATE TABLE notification_type (
    notification_type_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du type de notification
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Alerte)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table study_domain : Domaines d’études */
CREATE TABLE study_domain (
    domain_id INT AUTO_INCREMENT PRIMARY KEY,          -- Identifiant unique du domaine
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du domaine (ex. : Informatique)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table semester : Semestres académiques */
CREATE TABLE semester (
    semester_id INT AUTO_INCREMENT PRIMARY KEY,        -- Identifiant unique du semestre
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du semestre (ex. : Semestre 1)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table activity_type : Types d’activités pédagogiques */
CREATE TABLE activity_type (
    activity_type_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique du type d’activité
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Cours magistral)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur (sera lié à users)
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour (sera lié à users)
) ENGINE=InnoDB;

/* Table prerequisite_type : Types de prérequis */
CREATE TABLE prerequisite_type (
    prerequisite_type_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du type de prérequis
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Diplôme)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table prerequisite_source : Sources des prérequis */
CREATE TABLE prerequisite_source (
    prerequisite_source_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de la source
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom de la source (ex. : Institution)
    description VARCHAR(191),                          -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table currency : Devises supportées pour les paiements */
CREATE TABLE currency (
    currency_id INT AUTO_INCREMENT PRIMARY KEY,        -- Identifiant unique de la devise
    code CHAR(3) NOT NULL UNIQUE,                      -- Code ISO de la devise (ex. : EUR)
    name VARCHAR(50) NOT NULL,                         -- Nom de la devise (ex. : Euro)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table payment_method : Méthodes de paiement disponibles */
CREATE TABLE payment_method (
    payment_method_id INT AUTO_INCREMENT PRIMARY KEY,  -- Identifiant unique de la méthode
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom de la méthode (ex. : Carte bancaire)
    description TEXT,                                  -- Description optionnelle (déjà présente)
    is_active BOOLEAN DEFAULT TRUE,                    -- Indique si la méthode est active
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table tax : Taxes applicables aux paiements */
CREATE TABLE tax (
    tax_id INT AUTO_INCREMENT PRIMARY KEY,             -- Identifiant unique de la taxe
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom de la taxe (ex. : TVA)
    rate DECIMAL(5,2) NOT NULL CHECK (rate >= 0),      -- Taux de la taxe (ex. : 21.00)
    description TEXT,                                  -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table payment_status : Statuts des paiements */
CREATE TABLE payment_status (
    payment_status_id INT AUTO_INCREMENT PRIMARY KEY,  -- Identifiant unique du statut
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du statut (ex. : En attente)
    description TEXT,                                  -- Description optionnelle (déjà présente)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table role : Rôles spécifiques aux entités */
CREATE TABLE role (
    role_id INT AUTO_INCREMENT PRIMARY KEY,            -- Identifiant unique du rôle
    name VARCHAR(30) NOT NULL,                         -- Nom du rôle (ex. : Admin, Student)
    entity_type_id INT NOT NULL,                       -- Référence au type d’entité (ex. 1 pour Institution)
    description TEXT,                                  -- Ajout de la colonne description
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    UNIQUE KEY uk_role_name_entity (name, entity_type_id) -- Unicité du couple nom/type d’entité
) ENGINE=InnoDB;

/* Table degree_type : Types de diplômes */
CREATE TABLE degree_type (
    degree_type_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique du type de diplôme
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type (ex. : Bachelier)
    description TEXT,                                  -- Description optionnelle (déjà présente)
    cycle_id INT,                                      -- Référence au cycle (optionnel)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table users : Utilisateurs enregistrés dans le système */
CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,            -- Identifiant unique de l’utilisateur
    email VARCHAR(191) UNIQUE NOT NULL,                -- Adresse email unique pour l’identification
    password_hash VARCHAR(191) NOT NULL,               -- Mot de passe haché pour la sécurité
    first_name VARCHAR(100),                           -- Prénom de l’utilisateur
    last_name VARCHAR(100),                            -- Nom de famille de l’utilisateur
    super_role_id INT NOT NULL,                        -- Rôle global de l’utilisateur dans le système
    is_verified BOOLEAN DEFAULT FALSE,                 -- Indique si l’email a été vérifié
    last_login_at TIMESTAMP NULL DEFAULT NULL,         -- Date et heure de la dernière connexion réussie
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    emailConfirmationToken NVARCHAR(36) NULL,          -- Jeton de confirmation d’email
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table token_type : Types de jetons de sécurité */
CREATE TABLE token_type (
    token_type_id INT AUTO_INCREMENT PRIMARY KEY,                      -- ID unique du type de jeton
    name VARCHAR(50) NOT NULL UNIQUE,                                  -- Nom unique (ex: 'EMAIL_CONFIRMATION', 'PASSWORD_RESET')
    description TEXT,                                                  -- Description facultative
    default_expiration_minutes INT NOT NULL DEFAULT 60,                -- Durée de vie par défaut du jeton (en minutes)
    min_delay_minutes INT NOT NULL DEFAULT 5,                          -- Délai minimal entre deux demandes (en minutes)
    max_requests_per_window INT NOT NULL DEFAULT 1,                    -- Nombre max de requêtes dans une fenêtre donnée
    rate_limit_window_minutes INT DEFAULT 60,                          -- Durée de la fenêtre de rate limiting (en minutes)
    is_rate_limited BOOLEAN NOT NULL DEFAULT TRUE,                     -- Si le type est soumis au rate limiting
    token_required BOOLEAN NOT NULL DEFAULT TRUE,                      -- Si un token cliquable est requis
    code_otp_required BOOLEAN NOT NULL DEFAULT FALSE,                  -- Si un code OTP (à 6 chiffres) est requis
	max_otp_attempts INT NOT NULL DEFAULT 5,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,                     -- Date de création
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de dernière mise à jour
) ENGINE=InnoDB;

/* Table event_owner : Entité polymorphique pour gérer les propriétaires d'événements */
CREATE TABLE event_owner (
    event_owner_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique du propriétaire d’événement
    owner_type VARCHAR(50) NOT NULL,                   -- Type de propriétaire (ex. : Institution, Company)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP     -- Date de création
) ENGINE=InnoDB;

/* Table housing_owner : Entité polymorphique pour gérer les propriétaires de logements */
CREATE TABLE housing_owner (
    housing_owner_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique du propriétaire de logement
    owner_type VARCHAR(50) NOT NULL,                   -- Type de propriétaire (ex. : Landlord, Campus)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP     -- Date de création
) ENGINE=InnoDB;

/* Table location : Centralise les informations d’adresse et de géolocalisation */
CREATE TABLE location (
    location_id INT AUTO_INCREMENT PRIMARY KEY,        -- Identifiant unique de la localisation
    street VARCHAR(191) NOT NULL,                      -- Nom de la rue
    number VARCHAR(10) NOT NULL,                       -- Numéro de la rue
    postal_code VARCHAR(10) NOT NULL,                  -- Code postal
    city VARCHAR(100) NOT NULL,                        -- Ville
    country VARCHAR(100) NOT NULL,                     -- Pays
    latitude DECIMAL(9,6) NULL,                        -- Coordonnée latitude pour la géolocalisation
    longitude DECIMAL(9,6) NULL,                       -- Coordonnée longitude pour la géolocalisation
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table invitation_type : Types d'invitations */
CREATE TABLE invitation_type (
    invitation_type_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du type d'invitation
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du type d'invitation
    description TEXT,                                  -- Description du type
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Date de création
    updated_at DATETIME NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table user_agents : Gestion des agents utilisateurs */
CREATE TABLE user_agents (
    user_agent_id INT AUTO_INCREMENT PRIMARY KEY,      -- Identifiant unique de l’agent utilisateur
    user_agent_value VARCHAR(255) NOT NULL UNIQUE,           -- Valeur de l’agent utilisateur
    browser VARCHAR(100),                              -- Nom du navigateur
    operating_system VARCHAR(100),                     -- Système d’exploitation
    device_type VARCHAR(50),                           -- Type d’appareil
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,     -- Date de création
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/*************************************************************************************************
    2. Tables Dépendantes (avec clés étrangères ou relations)
**************************************************************************************************/

/* Table entity_type_translations : Traductions des types d'entités */
CREATE TABLE entity_type_translations (
    entity_type_translation_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de la traduction
    entity_type_id INT NOT NULL,                               -- Référence à entity_types
    language_id INT NOT NULL,                                  -- Référence à spoken_language
    translated_name VARCHAR(50) NOT NULL,                      -- Nom traduit (ex. "Entreprise" en français)
    translated_description TEXT,                               -- Description traduite
    UNIQUE KEY uk_entity_type_lang (entity_type_id, language_id) -- Unicité du couple entity_type_id/language_id
) ENGINE=InnoDB;

/* Table institution_type_translations : Traductions des types d'institutions */
CREATE TABLE institution_type_translations (
    institution_type_translation_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de la traduction
    institution_type_id INT NOT NULL,                              -- Référence à institution_type
    language_id INT NOT NULL,                                      -- Référence à spoken_language
    translated_name VARCHAR(50) NOT NULL,                          -- Nom traduit (ex. "University" en anglais)
    translated_description TEXT,                                   -- Description traduite (facultatif, NULL ici)
    UNIQUE KEY uk_institution_type_lang (institution_type_id, language_id) -- Unicité du couple
) ENGINE=InnoDB;

/* Table role_translations : Traductions des rôles */
CREATE TABLE role_translations (
    role_translation_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de la traduction
    role_id INT NOT NULL,                               -- Référence à role
    language_id INT NOT NULL,                           -- Référence à spoken_language
    translated_name VARCHAR(30) NOT NULL,               -- Nom traduit
    translated_description TEXT,                        -- Description traduite
    UNIQUE KEY uk_role_lang (role_id, language_id)      -- Unicité du couple role_id/language_id
) ENGINE=InnoDB;

/* Table entities : Entités polymorphiques */
CREATE TABLE entities (
    entity_id INT AUTO_INCREMENT PRIMARY KEY,          -- Identifiant unique de l’entité
    entity_type_id INT NOT NULL,                       -- Référence au type d’entité
    specific_entity_id INT NULL,                       -- Identifiant spécifique dans la table correspondante
    name VARCHAR(191) NOT NULL,                        -- Nom de l’entité
    parent_entity_id INT NULL,                         -- Référence à une entité parente (optionnelle)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT                                     -- Référence à l’utilisateur créateur
) ENGINE=InnoDB;

/* Table contact : Informations de contact des utilisateurs ou entités */
CREATE TABLE contact (
    contact_id INT AUTO_INCREMENT PRIMARY KEY,         -- Identifiant unique des informations de contact
    location_id INT NULL,                              -- Référence à la localisation (optionnelle)
    phone VARCHAR(20) NULL,                            -- Numéro de téléphone
    contact_email VARCHAR(191) NULL,                   -- Adresse email de contact (différente de l’email principal)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table institution : Institutions éducatives */
CREATE TABLE institution (
    institution_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique de l’institution
    event_owner_id INT UNIQUE,                         -- Clé polymorphique vers event_owner
    entity_id INT NOT NULL UNIQUE,                     -- Référence à entities
    name VARCHAR(50) NOT NULL,                         -- Nom officiel de l’institution
    acronym VARCHAR(10),                               -- Acronyme de l’institution (ex. : UCL)
    description TEXT,                                  -- Description de l’institution
    website VARCHAR(191),                              -- URL du site web officiel
    logo VARCHAR(191),                                 -- Chemin vers le fichier du logo
    institution_type_id INT NOT NULL,                  -- Type d’institution (ex. : Université)
    community_id INT NULL,                             -- Communauté associée (ex. : Française)
    legal_status_id INT NULL,                          -- Statut juridique (ex. : Public)
    network_id INT NULL,                               -- Réseau d’enseignement (ex. : Officiel)
    official_code VARCHAR(20) UNIQUE,                  -- Code officiel attribué à l’institution
    founding_date DATE,                                -- Date de fondation
    student_count INT CHECK (student_count >= 0),      -- Nombre total d’étudiants inscrits
    is_officially_recognized BOOLEAN DEFAULT TRUE,     -- Indique si l’institution est officiellement reconnue
    authority_id INT,                                  -- Autorité de tutelle
    education_level_id INT NULL,                       -- Niveau d’enseignement principal
    is_modular BOOLEAN DEFAULT FALSE,                  -- Indique si les formations sont modulaires
    target_audience VARCHAR(50),                       -- Public cible (ex. : Étudiants adultes)
    average_tuition_fee DECIMAL(10,2) CHECK (average_tuition_fee >= 0), -- Frais moyens d’inscription
    like_count INT DEFAULT 0 CHECK (like_count >= 0),  -- Nombre de "j’aime" reçus
    visit_count INT DEFAULT 0 CHECK (visit_count >= 0),-- Nombre de visites sur le profil
    contact_id INT,                                    -- Référence aux informations de contact
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table campus : Campus physiques rattachés à une institution */
CREATE TABLE campus (
    campus_id INT AUTO_INCREMENT PRIMARY KEY,          -- Identifiant unique du campus
    event_owner_id INT UNIQUE,                         -- Clé polymorphique vers event_owner
    housing_owner_id INT UNIQUE,                       -- Clé polymorphique vers housing_owner
    entity_id INT NOT NULL UNIQUE,                     -- Référence à entities
    name VARCHAR(50) NOT NULL,                         -- Nom du campus
    acronym VARCHAR(10),                               -- Acronyme du campus
    description TEXT,                                  -- Description du campus
    official_code VARCHAR(20) UNIQUE,                  -- Code officiel du campus
    opening_date DATE,                                 -- Date d’ouverture
    capacity INT CHECK (capacity >= 0),                -- Capacité d’accueil en nombre d’étudiants
    area DECIMAL(10,2) CHECK (area >= 0),              -- Surface en mètres carrés
    like_count INT DEFAULT 0 CHECK (like_count >= 0),  -- Nombre de "j’aime" reçus
    rating DECIMAL(3,2) CHECK (rating >= 0 AND rating <= 5), -- Note moyenne attribuée par les étudiants
    visit_count INT DEFAULT 0 CHECK (visit_count >= 0),-- Nombre de visites sur le profil
    logo VARCHAR(191),                                 -- Chemin vers le fichier du logo
    use_institution_data BOOLEAN DEFAULT TRUE,         -- Indique si les données de l’institution sont utilisées par défaut
    campus_type_id INT,                                -- Type de campus (ex. : Principal)
    is_active BOOLEAN DEFAULT TRUE,                    -- Indique si le campus est actif
    contact_id INT,                                    -- Référence aux informations de contact
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table company : Entreprises enregistrées dans le système */
CREATE TABLE company (
    company_id INT AUTO_INCREMENT PRIMARY KEY,         -- Identifiant unique de l’entreprise
    event_owner_id INT UNIQUE,                         -- Clé polymorphique vers event_owner
    housing_owner_id INT UNIQUE,                       -- Clé polymorphique vers housing_owner
    entity_id INT NOT NULL UNIQUE,                     -- Référence à entities
    name VARCHAR(100) NOT NULL,                        -- Nom officiel de l’entreprise
    acronym VARCHAR(10),                               -- Acronyme de l’entreprise
    company_number VARCHAR(12) UNIQUE,                 -- Numéro d’entreprise unique
    sector VARCHAR(50),                                -- Secteur d’activité (ex. : Technologie)
    website VARCHAR(191),                              -- URL du site web officiel
    description TEXT,                                  -- Description de l’entreprise
    collaborator_count INT CHECK (collaborator_count >= 0), -- Nombre de collaborateurs employés
    logo VARCHAR(191),                                 -- Chemin vers le fichier du logo
    responsible_user_id INT,                           -- Référence à l’utilisateur responsable
    company_type_id INT,                               -- Type d’entreprise (ex. : PME)
    capacity INT CHECK (capacity >= 0),                -- Capacité (ex. : nombre de stagiaires possibles)
    founding_date DATE,                                -- Date de fondation
    is_active BOOLEAN DEFAULT TRUE,                    -- Indique si l’entreprise est active
    like_count INT DEFAULT 0 CHECK (like_count >= 0),  -- Nombre de "j’aime" reçus
    rating DECIMAL(3,2) CHECK (rating >= 0 AND rating <= 5), -- Note moyenne attribuée par les étudiants
    visit_count INT DEFAULT 0 CHECK (visit_count >= 0),-- Nombre de visites sur le profil
    contact_id INT,                                    -- Référence aux informations de contact
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table landlord : Bailleurs individuels proposant des logements */
CREATE TABLE landlord (
    landlord_id INT AUTO_INCREMENT PRIMARY KEY,        -- Identifiant unique du bailleur
    housing_owner_id INT UNIQUE,                       -- Clé polymorphique vers housing_owner
    entity_id INT NOT NULL UNIQUE,                     -- Référence à entities
    contact_id INT NULL,                               -- Référence aux informations de contact
    housing_count INT DEFAULT 0 CHECK (housing_count >= 0), -- Nombre de logements proposés
    rating DECIMAL(3,2) CHECK (rating >= 0 AND rating <= 5), -- Note moyenne attribuée par les locataires
    is_active BOOLEAN DEFAULT TRUE,                    -- Indique si le bailleur est actif
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table entity_user : Associe un utilisateur à une entité spécifique */
CREATE TABLE entity_user (
    entity_user_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique de l’association
    entity_id INT NOT NULL,                            -- Référence à entities
    user_id INT NOT NULL,                              -- Référence à users
    role_id INT NOT NULL,                              -- Référence à role
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    UNIQUE KEY uk_entity_user (entity_id, user_id)     -- Unicité du couple entity_id/user_id
) ENGINE=InnoDB;

/* Table student : Profils spécifiques des étudiants */
CREATE TABLE student (
    student_id INT AUTO_INCREMENT PRIMARY KEY,         -- Identifiant unique de l’étudiant
    entity_id INT NOT NULL,                            -- Référence à entities
    birth_date DATE NULL,                              -- Date de naissance de l’étudiant
    bio TEXT NULL,                                     -- Biographie ou description personnelle
    linkedin VARCHAR(191) NULL,                        -- URL du profil LinkedIn
    github VARCHAR(191) NULL,                          -- URL du profil GitHub
    portfolio VARCHAR(191) NULL,                       -- URL du portfolio personnel
    expected_graduation_year INT,                      -- Année prévue de fin d’études
    preferred_job_type ENUM('Stage','Job étudiant','Alternance') NULL, -- Type de travail préféré
    preferred_location VARCHAR(100) NULL,              -- Localisation préférée pour le travail ou le logement
    notification_enabled BOOLEAN DEFAULT TRUE,         -- Activation des notifications pour l’étudiant
    study_field VARCHAR(100),                          -- Domaine d’études principal
    cv_path VARCHAR(191),                              -- Chemin vers le CV par défaut
    study_level_id INT,                                -- Référence au niveau d’études actuel
    contact_id INT,                                    -- Référence aux informations de contact
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table security_token : Jetons de sécurité pour les utilisateurs */
CREATE TABLE security_token (
    security_token_id INT AUTO_INCREMENT PRIMARY KEY,  -- Identifiant unique du jeton
    user_id INT NOT NULL,                              -- Utilisateur lié
    token_type_id INT NOT NULL,                        -- Type de jeton (FK)
    token_hash VARCHAR(191) NOT NULL UNIQUE,           -- Valeur du jeton
	code_otp_hash VARCHAR(191) NULL,
	otp_attempt_count INT NOT NULL DEFAULT 0,
    ip_address VARCHAR(45),                            -- IP d'origine
    user_agent_id INT NULL,                            -- Clé étrangère vers user_agents
    device_id VARCHAR(255),                            -- Identifiant d'appareil, si applicable
    metadata JSON NULL,                                -- Métadonnées optionnelles
    expires_at DATETIME NOT NULL,                      -- Date d'expiration
    used BOOLEAN DEFAULT FALSE,                        -- Si le jeton a été utilisé
    revoked BOOLEAN DEFAULT FALSE,                     -- Si le jeton a été explicitement révoqué
    revoked_at DATETIME NULL,                          -- Date de révocation
    consumed_at DATETIME NULL,                         -- Date d'utilisation du jeton
    resend_count INT DEFAULT 0,                        -- Combien de fois un jeton de même type a été renvoyé
    last_sent_at DATETIME NULL,                        -- Dernier envoi du jeton (utile pour throttle)
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,     -- Date de création
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;


/* Table superadmin_invitation : Invitations créées par le superadmin */
CREATE TABLE superadmin_invitation (
    superadmin_invitation_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de l’invitation
    email VARCHAR(191) NOT NULL UNIQUE,                -- Email de la personne invitée
    invitation_token VARCHAR(191) NOT NULL UNIQUE,     -- Token unique pour valider l’inscription
    invitation_type_id INT NOT NULL,                   -- Référence au type d'invitation
    expires_at DATETIME NOT NULL,                      -- Date et heure d’expiration
    used BOOLEAN DEFAULT FALSE,                        -- Indique si l’invitation a été utilisée
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    created_by INT                                     -- Référence à l’utilisateur créateur
) ENGINE=InnoDB;

/* Table superadmin_invitation_entity : Associe une invitation à une entité */
CREATE TABLE superadmin_invitation_entity (
    superadmin_invitation_entity_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de l’association
    superadmin_invitation_id INT NOT NULL,             -- Référence à l’invitation
    entity_id INT NOT NULL,                            -- ID de l’entité spécifique
    role_id INT NOT NULL,                              -- Rôle attribué dans l’entité
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    UNIQUE (superadmin_invitation_id)                  -- Une invitation ne peut être liée qu’à une entité
) ENGINE=InnoDB;

/* Table campus_allowed_domain : Domaines email autorisés pour l’inscription à un campus */
CREATE TABLE campus_allowed_domain (
    campus_allowed_domain_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du domaine
    campus_id INT NOT NULL,                            -- Référence au campus concerné
    domain VARCHAR(100) NOT NULL UNIQUE,               -- Domaine email autorisé (ex. : uclouvain.be)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP     -- Date de création
) ENGINE=InnoDB;

/* Table login_attempt : Suivi des tentatives de connexion */
CREATE TABLE login_attempt (
    login_attempt_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique de la tentative
    user_id INT NULL,                                  -- Référence à l’utilisateur (NULL si email inconnu)
    email VARCHAR(191) NOT NULL,                       -- Email utilisé lors de la tentative
    ip_address VARCHAR(45) NOT NULL,                   -- Adresse IP (IPv4 ou IPv6)
    user_agent_id INT NULL,                            -- Référence au User-Agent
    attempt_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  -- Date et heure de la tentative
    locked_until TIMESTAMP NULL,                       -- Date jusqu’à laquelle le compte est bloqué
    success BOOLEAN DEFAULT FALSE                      -- Indique si la tentative a réussi
) ENGINE=InnoDB;

/* Table sessionEvents : Suivi des événements de session */
CREATE TABLE sessionEvents (
    sessionEvent_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique de l’événement
    user_id INT NOT NULL,                              -- Référence à l’utilisateur
    eventType VARCHAR(50) NOT NULL                     -- Type d’événement (Login ou Logout)
        CHECK (eventType IN ('Login', 'Logout')),
    ipAddress VARCHAR(45) NOT NULL,                    -- Adresse IP (IPv4 ou IPv6)
    user_agent_id INT NULL,                            -- Référence au User-Agent
    eventTime TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP -- Date et heure de l’événement
) ENGINE=InnoDB;

/* Table refresh_token : Jetons de rafraîchissement */
CREATE TABLE refresh_token (
    refresh_token_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique du jeton
    user_id INT NOT NULL,                              -- Référence à l’utilisateur
    token VARCHAR(200) NOT NULL UNIQUE,                -- Valeur unique du jeton
    sessionEvent_id INT NOT NULL,                      -- Référence à l’événement de session (ex. Login)
    expires_at DATETIME NOT NULL,                      -- Date et heure d’expiration
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP     -- Date de création
) ENGINE=InnoDB;
ALTER TABLE refresh_token
ADD COLUMN used BOOLEAN NOT NULL DEFAULT FALSE;
ALTER TABLE refresh_token
ADD COLUMN used_at DATETIME NULL DEFAULT NULL;



/* Table notification : Notifications envoyées aux utilisateurs */
CREATE TABLE notification (
    notification_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique de la notification
    user_id INT NOT NULL,                              -- Référence à l’utilisateur destinataire
    notification_type_id INT NOT NULL,                 -- Type de notification (ex. : Alerte)
    entity_type VARCHAR(50) NULL,                      -- Type d’entité concernée (ex. : Housing)
    entity_id INT NULL,                                -- Identifiant de l’entité concernée
    message TEXT NOT NULL,                             -- Contenu de la notification
    is_read BOOLEAN DEFAULT FALSE,                     -- Indique si la notification a été lue
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table student_friendship : Relations d’amitié entre étudiants */
CREATE TABLE student_friendship (
    student_friendship_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de la relation
    student_id_1 INT NOT NULL,                         -- Référence au premier étudiant
    student_id_2 INT NOT NULL,                         -- Référence au second étudiant
    status ENUM('Pending', 'Accepted', 'Rejected') DEFAULT 'Pending', -- Statut de la demande d’amitié
    request_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  -- Date de la demande
    response_date TIMESTAMP NULL,                      -- Date de la réponse (acceptation ou rejet)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT,                                    -- Référence à l’utilisateur ayant mis à jour
    CONSTRAINT chk_not_self_friendship CHECK (student_id_1 != student_id_2), -- Empêche une amitié avec soi-même
    UNIQUE (student_id_1, student_id_2)                -- Unicité de la paire d’étudiants
) ENGINE=InnoDB;

/* Table cv : Gestion des CV créés par les étudiants */
CREATE TABLE cv (
    cv_id INT AUTO_INCREMENT PRIMARY KEY,              -- Identifiant unique du CV
    student_id INT NOT NULL,                           -- Référence à l’étudiant propriétaire
    title VARCHAR(100) NOT NULL,                       -- Titre du CV
    objective TEXT NULL,                               -- Objectif professionnel
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    is_active BOOLEAN DEFAULT FALSE,                   -- Indique si le CV est actif
    is_default BOOLEAN DEFAULT FALSE                   -- Indique si c’est le CV par défaut
) ENGINE=InnoDB;

/* Table experience : Expériences professionnelles associées à un CV */
CREATE TABLE experience (
    experience_id INT AUTO_INCREMENT PRIMARY KEY,      -- Identifiant unique de l’expérience
    cv_id INT NOT NULL,                                -- Référence au CV associé
    job_title VARCHAR(100) NOT NULL,                   -- Titre du poste
    company VARCHAR(100) NOT NULL,                     -- Nom de l’entreprise
    location VARCHAR(100) NULL,                        -- Lieu de l’expérience
    start_date DATE NOT NULL,                          -- Date de début
    end_date DATE NULL,                                -- Date de fin (NULL si en cours)
    description TEXT NULL                              -- Description des responsabilités
) ENGINE=InnoDB;

/* Table education : Parcours éducatifs associés à un CV */
CREATE TABLE education (
    education_id INT AUTO_INCREMENT PRIMARY KEY,       -- Identifiant unique de la formation
    cv_id INT NOT NULL,                                -- Référence au CV associé
    degree VARCHAR(100) NOT NULL,                      -- Nom du diplôme
    institution VARCHAR(100) NOT NULL,                 -- Nom de l’institution
    location VARCHAR(100) NULL,                        -- Lieu de la formation
    start_date DATE NOT NULL,                          -- Date de début
    end_date DATE NULL,                                -- Date de fin (NULL si en cours)
    description TEXT NULL                              -- Description des études
) ENGINE=InnoDB;

/* Table cv_skill : Compétences associées à un CV */
CREATE TABLE cv_skill (
    cv_skill_id INT AUTO_INCREMENT PRIMARY KEY,        -- Identifiant unique de la compétence
    cv_id INT NOT NULL,                                -- Référence au CV associé
    name VARCHAR(50) NOT NULL,                         -- Nom de la compétence
    proficiency ENUM('Débutant', 'Intermédiaire', 'Avancé', 'Expert') DEFAULT 'Intermédiaire' -- Niveau de maîtrise
) ENGINE=InnoDB;

/* Table cv_language : Langues associées à un CV */
CREATE TABLE cv_language (
    cv_language_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique de la langue
    language_id INT,                                   -- Référence à la langue parlée
    cv_id INT NOT NULL,                                -- Référence au CV associé
    level ENUM('A1', 'A2', 'B1', 'B2', 'C1', 'C2') DEFAULT 'B1' -- Niveau selon le cadre européen
) ENGINE=InnoDB;

/* Table payment_plan : Plans de paiement proposés aux utilisateurs */
CREATE TABLE payment_plan (
    payment_plan_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique du plan
    name VARCHAR(50) NOT NULL UNIQUE,                  -- Nom du plan (ex. : "Premium Mensuel")
    description TEXT,                                  -- Description du plan
    price DECIMAL(10,2) NOT NULL CHECK (price >= 0),   -- Prix du plan
    currency_id INT NOT NULL,                          -- Devise du prix
    duration_days INT NULL,                            -- Durée du plan en jours (NULL si non applicable)
    is_recurring BOOLEAN DEFAULT FALSE,                -- Indique si le plan est récurrent
    max_offer_count INT NULL,                          -- Nombre maximum d’offres autorisées
    max_housing_count INT NULL,                        -- Nombre maximum de logements autorisés
    max_event_count INT NULL,                          -- Nombre maximum d’événements autorisés
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table payment : Paiements effectués par les utilisateurs */
CREATE TABLE payment (
    payment_id INT AUTO_INCREMENT PRIMARY KEY,         -- Identifiant unique du paiement
    payment_plan_id INT NOT NULL,                      -- Référence au plan de paiement
    owner_type_id INT NOT NULL,                        -- Type de propriétaire (ex. : Company)
    owner_id INT NOT NULL,                             -- Identifiant du propriétaire
    amount DECIMAL(10,2) NOT NULL CHECK (amount >= 0), -- Montant hors taxes
    tax_id INT NULL,                                   -- Référence à la taxe appliquée
    tax_amount DECIMAL(10,2) NULL CHECK (tax_amount >= 0), -- Montant de la taxe
    total_amount DECIMAL(10,2) NOT NULL CHECK (total_amount >= 0), -- Montant total (incluant taxes)
    currency_id INT NOT NULL,                          -- Devise du paiement
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  -- Date de réalisation du paiement
    payment_method_id INT NOT NULL,                    -- Méthode de paiement utilisée
    payment_status_id INT NOT NULL DEFAULT 1,          -- Statut du paiement (1 = En attente)
    transaction_reference VARCHAR(100) UNIQUE,         -- Référence unique de la transaction
    external_token VARCHAR(191) NULL,                  -- Jeton externe (ex. : Stripe)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table housing : Logements proposés */
CREATE TABLE housing (
    housing_id INT AUTO_INCREMENT PRIMARY KEY,         -- Identifiant unique du logement
    housing_owner_id INT NOT NULL,                     -- Référence au propriétaire polymorphique
    title VARCHAR(100) NOT NULL,                       -- Titre de l’annonce
    description TEXT NOT NULL,                         -- Description détaillée du logement
    price DECIMAL(10,2) NOT NULL CHECK (price >= 0),   -- Prix mensuel du loyer
    location_id INT NOT NULL,                          -- Référence à la localisation
    charges DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (charges >= 0), -- Charges mensuelles
    deposit DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (deposit >= 0), -- Montant de la caution
    size DECIMAL(5,2) CHECK (size > 0),                -- Surface en mètres carrés
    bedroom_count INT CHECK (bedroom_count >= 1),      -- Nombre de chambres
    capacity INT CHECK (capacity >= 1),                -- Capacité d’accueil en personnes
    housing_type_id INT NOT NULL,                      -- Type de logement (ex. : Appartement)
    peb_rating_id INT NOT NULL,                        -- Classification énergétique PEB
    status ENUM('Disponible','Réservé','Loué') DEFAULT 'Disponible', -- Statut actuel du logement
    availability_date DATE,                            -- Date de disponibilité
    end_availability_date DATE,                        -- Date de fin de disponibilité
    preferences TEXT,                                  -- Préférences du propriétaire
    legal_compliance BOOLEAN DEFAULT FALSE,            -- Conformité légale du logement
    sponsored BOOLEAN DEFAULT FALSE,                   -- Indique si l’annonce est sponsorisée
    view_count INT DEFAULT 0 CHECK (view_count >= 0),  -- Nombre de vues de l’annonce
    application_count INT DEFAULT 0 CHECK (application_count >= 0), -- Nombre de candidatures reçues
    like_count INT DEFAULT 0 CHECK (like_count >= 0),  -- Nombre de "j’aime" reçus
    visit_count INT DEFAULT 0 CHECK (visit_count >= 0),-- Nombre de visites planifiées
    last_viewed_at TIMESTAMP NULL,                     -- Date de la dernière consultation
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    payment_id INT NULL DEFAULT NULL                   -- Référence au paiement associé (optionnel)
) ENGINE=InnoDB;

/* Table housing_amenity : Relation N:M entre logements et commodités */
CREATE TABLE housing_amenity (
    housing_id INT,                                    -- Référence au logement
    amenity_id INT,                                    -- Référence à la commodité
    PRIMARY KEY (housing_id, amenity_id)               -- Clé primaire composite
) ENGINE=InnoDB;

/* Table housing_application : Candidatures des étudiants pour les logements */
CREATE TABLE housing_application (
    housing_application_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de la candidature
    student_id INT NOT NULL,                           -- Référence à l’étudiant candidat
    housing_id INT NOT NULL,                           -- Référence au logement ciblé
    application_status_id INT DEFAULT 1,               -- Statut de la candidature (1 = En attente)
    message TEXT,                                      -- Message accompagnant la candidature
    applied_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP     -- Date de soumission de la candidature
) ENGINE=InnoDB;

/* Table housing_visit : Demandes de visites pour les logements */
CREATE TABLE housing_visit (
    housing_visit_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique de la demande de visite
    student_id INT NOT NULL,                           -- Référence à l’étudiant demandeur
    housing_id INT NOT NULL,                           -- Référence au logement ciblé
    application_status_id INT DEFAULT 1,               -- Statut de la demande (1 = En attente)
    confirmed_date_time DATETIME NULL,                 -- Date et heure confirmées pour la visite
    student_message TEXT,                              -- Message de l’étudiant
    landlord_message TEXT,                             -- Message du bailleur
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table housing_visit_range : Plages horaires proposées pour les visites */
CREATE TABLE housing_visit_range (
    housing_visit_range_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de la plage horaire
    housing_visit_id INT NOT NULL,                     -- Référence à la demande de visite
    start_datetime DATETIME NOT NULL,                  -- Début de la plage horaire
    end_datetime DATETIME NOT NULL,                    -- Fin de la plage horaire
    message TEXT,                                      -- Message associé à la proposition
    range_status ENUM('Proposed', 'Selected', 'Declined', 'Countered') DEFAULT 'Proposed', -- Statut de la plage
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    CONSTRAINT chk_range_dates CHECK (end_datetime > start_datetime) -- Vérifie que la fin est après le début
) ENGINE=InnoDB;

/* Table offer : Offres d’emploi ou de stage */
CREATE TABLE offer (
    offer_id INT AUTO_INCREMENT PRIMARY KEY,           -- Identifiant unique de l’offre
    company_id INT NOT NULL,                           -- Référence à l’entreprise émettrice
    offer_type_id INT NOT NULL,                        -- Type d’offre (ex. : Stage, Job)
    title VARCHAR(50) NOT NULL,                        -- Titre de l’offre
    description TEXT,                                  -- Description détaillée de l’offre
    location_id INT NULL,                              -- Référence à la localisation (optionnelle)
    duration SMALLINT,                                 -- Durée de l’offre (en unité spécifiée)
    duration_unit_id INT,                              -- Unité de la durée (ex. : mois)
    work_hours SMALLINT,                               -- Heures de travail par semaine
    student_job_hours INT CHECK (student_job_hours >= 0 AND student_job_hours <= 475), -- Heures pour job étudiant (limite légale belge : 475h/an)
    salary DECIMAL(7,2),                               -- Salaire proposé
    start_date DATE,                                   -- Date de début de l’offre
    contract_type_id INT,                              -- Type de contrat (ex. : CDD, CDI)
    schedule_type_id INT,                              -- Type d’horaire (ex. : Plein temps)
    ects_credits INT CHECK (ects_credits >= 0),        -- Crédits ECTS attribués (si applicable)
    language_id INT NOT NULL,                          -- Langue principale de l’offre
    remote_possible BOOLEAN DEFAULT FALSE,             -- Possibilité de télétravail
    sponsored BOOLEAN DEFAULT FALSE,                   -- Indique si l’offre est sponsorisée
    cv_required BOOLEAN DEFAULT FALSE,                 -- CV requis pour postuler
    cover_letter_required BOOLEAN DEFAULT FALSE,       -- Lettre de motivation requise pour postuler
    experience_required BOOLEAN DEFAULT FALSE,         -- Expérience préalable requise
    deadline DATE,                                     -- Date limite de candidature
    active BOOLEAN DEFAULT FALSE,                      -- Indique si l’offre est active
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT,                                    -- Référence à l’utilisateur ayant mis à jour
    payment_id INT NULL DEFAULT NULL                   -- Référence au paiement associé (optionnel)
) ENGINE=InnoDB;

/* Table application : Candidatures des étudiants aux offres */
CREATE TABLE application (
    application_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique de la candidature
    student_id INT,                                    -- Référence à l’étudiant candidat
    offer_id INT,                                      -- Référence à l’offre ciblée
    application_status_id INT DEFAULT 1,               -- Statut de la candidature (1 = En attente)
    cv_path VARCHAR(191),                              -- Chemin vers le CV soumis
    cover_letter_path VARCHAR(191),                    -- Chemin vers la lettre de motivation soumise
    reason TEXT,                                       -- Motivation ou commentaire du candidat
    viewed_by_company BOOLEAN DEFAULT FALSE,           -- Indique si l’entreprise a vu la candidature
    applied_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP     -- Date de soumission de la candidature
) ENGINE=InnoDB;

/* Table offer_favorite : Offres mises en favoris par les étudiants */
CREATE TABLE offer_favorite (
    offer_favorite_id INT AUTO_INCREMENT PRIMARY KEY,  -- Identifiant unique du favori
    student_id INT NOT NULL,                           -- Référence à l’étudiant
    offer_id INT NOT NULL,                             -- Référence à l’offre mise en favori
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de mise en favori
    UNIQUE (student_id, offer_id)                      -- Unicité pour éviter les doublons
) ENGINE=InnoDB;

/* Table work_hours : Suivi des heures travaillées pour les jobs étudiants */
CREATE TABLE work_hours (
    work_hour_id INT AUTO_INCREMENT PRIMARY KEY,       -- Identifiant unique de l’enregistrement
    student_id INT NOT NULL,                           -- Référence à l’étudiant
    offer_id INT NOT NULL,                             -- Référence à l’offre concernée
    employer_id INT NOT NULL,                          -- Référence à l’entreprise employeuse
    hours_worked DECIMAL(5,2) CHECK (hours_worked >= 0), -- Nombre d’heures travaillées
    work_date DATE NOT NULL,                           -- Date du travail
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP     -- Date de création
) ENGINE=InnoDB;

/* Table entity_review : Évaluations des entités par les étudiants */
CREATE TABLE entity_review (
    entity_review_id INT AUTO_INCREMENT PRIMARY KEY,   -- Identifiant unique de l’évaluation
    student_id INT NOT NULL,                           -- Référence à l’étudiant évaluateur
    entity_type VARCHAR(50) NOT NULL,                  -- Type d’entité évaluée (ex. : Institution, Housing)
    entity_id INT NOT NULL,                            -- Identifiant de l’entité évaluée
    rating DECIMAL(2,1) CHECK (rating >= 0 AND rating <= 5), -- Note attribuée (0 à 5)
    comment TEXT,                                      -- Commentaire de l’évaluation
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT,                                    -- Référence à l’utilisateur ayant mis à jour
    UNIQUE (student_id, entity_type, entity_id)        -- Unicité pour éviter plusieurs évaluations par étudiant
) ENGINE=InnoDB;

/* Table student_referral : Parrainages entre étudiants */
CREATE TABLE student_referral (
    student_referral_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du parrainage
    referring_student_id INT NOT NULL,                 -- Référence à l’étudiant parrain
    referred_student_id INT NOT NULL,                  -- Référence à l’étudiant parrainé
    entity_type VARCHAR(50) NOT NULL,                  -- Type d’entité concernée (ex. : Institution)
    entity_id INT NOT NULL,                            -- Identifiant de l’entité concernée
    reward DECIMAL(7,2) CHECK (reward >= 0),           -- Récompense éventuelle pour le parrain
    referral_status ENUM('Pending', 'Completed', 'Rejected') DEFAULT 'Pending', -- Statut du parrainage
    message TEXT,                                      -- Message accompagnant la recommandation
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT,                                    -- Référence à l’utilisateur ayant mis à jour
    CONSTRAINT chk_not_self_referral CHECK (referring_student_id != referred_student_id), -- Empêche un auto-parrainage
    UNIQUE (referring_student_id, referred_student_id, entity_type, entity_id) -- Unicité de la relation
) ENGINE=InnoDB;

/* Table payment_log : Historique des changements de statut des paiements */
CREATE TABLE payment_log (
    payment_log_id INT AUTO_INCREMENT PRIMARY KEY,     -- Identifiant unique de l’entrée
    payment_id INT NOT NULL,                           -- Référence au paiement concerné
    previous_status_id INT NULL,                       -- Statut précédent (NULL si premier statut)
    new_status_id INT NOT NULL,                        -- Nouveau statut
    log_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,      -- Date du changement
    details TEXT,                                      -- Détails supplémentaires
    created_by INT                                     -- Référence à l’utilisateur créateur
) ENGINE=InnoDB;

/* Table payment_item : Détails des items inclus dans un paiement */
CREATE TABLE payment_item (
    payment_item_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique de l’item
    payment_id INT NOT NULL,                           -- Référence au paiement
    amount DECIMAL(10,2) NOT NULL CHECK (amount >= 0), -- Montant de l’item
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table payment_item_entity : Liens entre items de paiement et entités */
CREATE TABLE payment_item_entity (
    payment_item_entity_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de l’association
    payment_item_id INT NOT NULL,                      -- Référence à l’item de paiement
    entity_type VARCHAR(50) NOT NULL,                  -- Type d’entité associée (ex. : Housing)
    entity_id INT NOT NULL,                            -- Identifiant de l’entité associée
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    UNIQUE (payment_item_id, entity_type, entity_id)   -- Unicité pour éviter les doublons
) ENGINE=InnoDB;

/* Table refund : Remboursements effectués sur des paiements */
CREATE TABLE refund (
    refund_id INT AUTO_INCREMENT PRIMARY KEY,          -- Identifiant unique du remboursement
    payment_id INT NOT NULL,                           -- Référence au paiement remboursé
    amount DECIMAL(10,2) NOT NULL CHECK (amount >= 0), -- Montant remboursé
    currency_id INT NOT NULL,                          -- Devise du remboursement
    refund_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,   -- Date du remboursement
    refund_reference VARCHAR(100) UNIQUE,              -- Référence unique du remboursement
    reason TEXT,                                       -- Raison du remboursement
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table campus_facility : Équipements disponibles sur un campus */
CREATE TABLE campus_facility (
    campus_facility_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique de l’équipement
    campus_id INT NOT NULL,                            -- Référence au campus
    facility_type_id INT NOT NULL,                     -- Type d’équipement (ex. : Bibliothèque)
    quantity INT DEFAULT 1 CHECK (quantity >= 0),      -- Nombre d’unités disponibles
    details TEXT,                                      -- Détails supplémentaires
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table degree : Diplômes proposés par les campus */
CREATE TABLE degree (
    degree_id INT AUTO_INCREMENT PRIMARY KEY,          -- Identifiant unique du diplôme
    campus_id INT,                                     -- Référence au campus proposant le diplôme
    name VARCHAR(50) NOT NULL,                         -- Nom du diplôme
    degree_type_id INT NOT NULL,                       -- Type de diplôme (ex. : Bachelier)
    degree_category_id INT,                            -- Catégorie du diplôme (ex. : Sciences)
    duration INT CHECK (duration > 0 OR duration IS NULL), -- Durée du programme
    duration_unit_id INT,                              -- Unité de la durée (ex. : années)
    credits INT CHECK (credits > 0 OR credits IS NULL), -- Nombre de crédits ECTS
    cost DECIMAL(7,2) CHECK (cost >= 0),               -- Coût du diplôme
    tuition_type_id INT NOT NULL DEFAULT 1,            -- Type de frais (ex. : Annuel)
    language_id INT,                                   -- Langue d’enseignement
    schedule_type_id INT NOT NULL,                     -- Type d’horaire (ex. : Plein temps)
    is_alternance BOOLEAN DEFAULT FALSE,               -- Indique si le programme est en alternance
    certification_type_id INT,                         -- Type de certification délivrée
    delivery_mode_id INT NOT NULL DEFAULT 1,           -- Mode de délivrance (ex. : Présentiel)
    qualification_level INT CHECK (qualification_level BETWEEN 1 AND 8 OR qualification_level IS NULL), -- Niveau de qualification (1-8)
    is_active BOOLEAN DEFAULT TRUE,                    -- Indique si le diplôme est actif
    like_count INT DEFAULT 0 CHECK (like_count >= 0),  -- Nombre de "j’aime" reçus
    visit_count INT DEFAULT 0 CHECK (visit_count >= 0),-- Nombre de visites sur le profil
    description TEXT,                                  -- Description du diplôme
    financability_required_credits INT CHECK (financability_required_credits >= 0 OR financability_required_credits IS NULL), -- Crédits requis pour financement
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table degree_partnership : Partenariats entre diplômes et entreprises */
CREATE TABLE degree_partnership (
    degree_partnership_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du partenariat
    degree_id INT NOT NULL,                            -- Référence au diplôme
    company_id INT NULL,                               -- Référence à l’entreprise partenaire (optionnelle)
    partner_name VARCHAR(100) NULL,                    -- Nom du partenaire (si non une company enregistrée)
    partnership_type VARCHAR(50),                      -- Type de partenariat (ex. : Stage)
    role VARCHAR(100),                                 -- Rôle du partenaire dans le diplôme
    start_date DATE NULL,                              -- Date de début du partenariat
    end_date DATE NULL,                                -- Date de fin du partenariat
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table offer_degree : Relation entre offres et diplômes */
CREATE TABLE offer_degree (
    offer_degree_id INT AUTO_INCREMENT PRIMARY KEY,    -- Identifiant unique de l’association
    offer_id INT NOT NULL,                             -- Référence à l’offre
    degree_id INT NOT NULL,                            -- Référence au diplôme requis ou recommandé
    mandatory BOOLEAN DEFAULT FALSE,                   -- Indique si le diplôme est obligatoire
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Date de mise à jour
) ENGINE=InnoDB;

/* Table specialty : Spécialités ou options au sein d’un diplôme */
CREATE TABLE specialty (
    specialty_id INT AUTO_INCREMENT PRIMARY KEY,       -- Identifiant unique de la spécialité
    degree_id INT,                                     -- Référence au diplôme parent
    domain_id INT NOT NULL,                            -- Référence au domaine d’études
    name VARCHAR(50) NOT NULL,                         -- Nom de la spécialité
    description TEXT,                                  -- Description de la spécialité
    outcomes TEXT,                                     -- Résultats attendus
    officially_recognized BOOLEAN DEFAULT FALSE,       -- Indique si la spécialité est officiellement reconnue
    like_count INT DEFAULT 0 CHECK (like_count >= 0),  -- Nombre de "j’aime" reçus
    visit_count INT DEFAULT 0 CHECK (visit_count >= 0),-- Nombre de visites sur le profil
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table ue : Unités d’enseignement (UE) composant une spécialité */
CREATE TABLE ue (
    ue_id INT AUTO_INCREMENT PRIMARY KEY,              -- Identifiant unique de l’UE
    specialty_id INT,                                  -- Référence à la spécialité parent
    name VARCHAR(100) NOT NULL,                        -- Nom de l’unité d’enseignement
    year INT NOT NULL CHECK (year >= 1),               -- Année du programme (ex. : 1ère année)
    semester_id INT NOT NULL,                          -- Référence au semestre
    credit_count INT NOT NULL CHECK (credit_count > 0), -- Nombre de crédits ECTS
    description TEXT,                                  -- Description de l’UE
    mandatory BOOLEAN DEFAULT TRUE,                    -- Indique si l’UE est obligatoire
    prerequisite_ue_id INT NULL,                       -- Référence à une UE préalable (optionnelle)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table ua : Activités d’apprentissage (UA) au sein d’une UE */
CREATE TABLE ua (
    ua_id INT AUTO_INCREMENT PRIMARY KEY,              -- Identifiant unique de l’UA
    ue_id INT,                                         -- Référence à l’UE parent
    name VARCHAR(100) NOT NULL,                        -- Nom de l’activité d’apprentissage
    activity_type_id INT NOT NULL,                     -- Type d’activité (ex. : Cours magistral)
    credit_count INT NOT NULL CHECK (credit_count > 0), -- Nombre de crédits ECTS
    description TEXT,                                  -- Description de l’UA
    mandatory BOOLEAN DEFAULT TRUE,                    -- Indique si l’UA est obligatoire
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table bridge : Passerelles entre diplômes */
CREATE TABLE bridge (
    bridge_id INT AUTO_INCREMENT PRIMARY KEY,          -- Identifiant unique de la passerelle
    from_degree_id INT,                                -- Référence au diplôme source
    to_degree_id INT,                                  -- Référence au diplôme cible
    additional_credit_count INT CHECK (additional_credit_count >= 0), -- Crédits supplémentaires requis
    description TEXT,                                  -- Description de la passerelle
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table degree_prerequisite : Prérequis pour accéder à un diplôme */
CREATE TABLE degree_prerequisite (
    degree_prerequisite_id INT AUTO_INCREMENT PRIMARY KEY, -- Identifiant unique du prérequis
    degree_id INT NOT NULL,                            -- Référence au diplôme concerné
    prerequisite_type_id INT NOT NULL,                 -- Type de prérequis (ex. : Diplôme)
    prerequisite_source_id INT NOT NULL DEFAULT 3,     -- Source du prérequis (ex. : Institution)
    description TEXT NOT NULL,                         -- Description du prérequis
    required_degree_id INT NULL,                       -- Référence à un diplôme requis (optionnel)
    minimum_grade DECIMAL(4,2) NULL CHECK (minimum_grade >= 0 AND minimum_grade <= 100), -- Note minimale requise
    mandatory BOOLEAN DEFAULT TRUE,                    -- Indique si le prérequis est obligatoire
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table event : Événements organisés par diverses entités */
CREATE TABLE event (
    event_id INT AUTO_INCREMENT PRIMARY KEY,           -- Identifiant unique de l’événement
    event_owner_id INT NOT NULL,                       -- Référence au propriétaire polymorphique
    title VARCHAR(100) NOT NULL,                       -- Titre de l’événement
    description TEXT,                                  -- Description de l’événement
    start_date_time DATETIME NOT NULL,                 -- Date et heure de début
    end_date_time DATETIME NOT NULL,                   -- Date et heure de fin
    location_id INT NOT NULL,                          -- Référence à la localisation
    registration_required BOOLEAN DEFAULT FALSE,       -- Inscription requise ou non
    registration_link VARCHAR(191) NULL,               -- Lien d’inscription (si requis)
    is_public BOOLEAN DEFAULT FALSE,                   -- Indique si l’événement est public
    like_count INT DEFAULT 0 CHECK (like_count >= 0),  -- Nombre de "j’aime" reçus
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT,                                    -- Référence à l’utilisateur ayant mis à jour
    payment_id INT NULL DEFAULT NULL,                  -- Référence au paiement associé (optionnel)
    CONSTRAINT chk_registration CHECK (
        (registration_required = TRUE AND registration_link IS NOT NULL)
        OR
        (registration_required = FALSE AND registration_link IS NULL)
    )                                                  -- Vérifie la cohérence entre inscription et lien
) ENGINE=InnoDB;

/* Table event_interest : Intérêts exprimés par les étudiants pour les événements */
CREATE TABLE event_interest (
    event_interest_id INT AUTO_INCREMENT PRIMARY KEY,  -- Identifiant unique de l’intérêt
    event_id INT NOT NULL,                             -- Référence à l’événement
    student_id INT NOT NULL,                           -- Référence à l’étudiant intéressé
    interest_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Date de l’expression d’intérêt
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table image : Gestion des images associées aux entités */
CREATE TABLE image (
    image_id INT AUTO_INCREMENT PRIMARY KEY,           -- Identifiant unique de l’image
    entity_type VARCHAR(50) NOT NULL,                  -- Type d’entité associée (ex. : Housing)
    entity_id INT NOT NULL,                            -- Identifiant de l’entité associée
    image_path VARCHAR(191) NOT NULL,                  -- Chemin vers le fichier image
    mime_type_id INT NOT NULL,                         -- Type MIME de l’image (ex. : image/jpeg)
    is_primary BOOLEAN DEFAULT FALSE,                  -- Indique si c’est l’image principale
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT                                     -- Référence à l’utilisateur ayant mis à jour
) ENGINE=InnoDB;

/* Table entity_like : Likes attribués par les étudiants aux entités */
CREATE TABLE entity_like (
    like_id INT AUTO_INCREMENT PRIMARY KEY,            -- Identifiant unique du "j’aime"
    student_id INT NOT NULL,                           -- Référence à l’étudiant qui aime
    entity_type VARCHAR(50) NOT NULL,                  -- Type d’entité aimée (ex. : Housing)
    entity_id INT NOT NULL,                            -- Identifiant de l’entité aimée
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    -- Date de création
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, -- Date de mise à jour
    created_by INT,                                    -- Référence à l’utilisateur créateur
    updated_by INT,                                    -- Référence à l’utilisateur ayant mis à jour
    CONSTRAINT unique_like_per_student_entity UNIQUE (student_id, entity_type, entity_id) -- Unicité pour éviter plusieurs "j’aime"
) ENGINE=InnoDB;

/*************************************************************************************************
    3. Ajout des Contraintes de Clés Étrangères (après création de toutes les tables)
**************************************************************************************************/

/* Contraintes pour super_role */
ALTER TABLE super_role
    ADD CONSTRAINT fk_super_role_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_super_role_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour role */
ALTER TABLE role
    ADD CONSTRAINT fk_role_entity_type FOREIGN KEY (entity_type_id) REFERENCES entity_types(entity_type_id) ON DELETE RESTRICT;

/* Contraintes pour entity_type_translations */
ALTER TABLE entity_type_translations
    ADD CONSTRAINT fk_entity_type_translations_entity_type FOREIGN KEY (entity_type_id) REFERENCES entity_types(entity_type_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_entity_type_translations_language FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT;

/* Contraintes pour institution_type_translations */
ALTER TABLE institution_type_translations
    ADD CONSTRAINT fk_institution_type_translations_institution_type FOREIGN KEY (institution_type_id) REFERENCES institution_type(institution_type_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_institution_type_translations_language FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT;

/* Contraintes pour role_translations */
ALTER TABLE role_translations
    ADD CONSTRAINT fk_role_translations_role FOREIGN KEY (role_id) REFERENCES role(role_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_role_translations_language FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT;

/* Contraintes pour degree_type */
ALTER TABLE degree_type
    ADD CONSTRAINT fk_degree_type_cycle FOREIGN KEY (cycle_id) REFERENCES cycle(cycle_id) ON DELETE SET NULL;

/* Contraintes pour users */
ALTER TABLE users
    ADD CONSTRAINT fk_user_super_role FOREIGN KEY (super_role_id) REFERENCES super_role(super_role_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_users_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_users_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

ALTER TABLE security_token
    ADD CONSTRAINT fk_security_token_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_security_token_type FOREIGN KEY (token_type_id) REFERENCES token_type(token_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_security_token_user_agent FOREIGN KEY (user_agent_id) REFERENCES user_agents(user_agent_id) ON DELETE SET NULL;


/* Contraintes pour study_level */
ALTER TABLE study_level
    ADD CONSTRAINT fk_study_level_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_study_level_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour application_status */
ALTER TABLE application_status
    ADD CONSTRAINT fk_application_status_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_application_status_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour offer_type */
ALTER TABLE offer_type
    ADD CONSTRAINT fk_offer_type_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_offer_type_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour contract_type */
ALTER TABLE contract_type
    ADD CONSTRAINT fk_contract_type_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_contract_type_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour spoken_language */
ALTER TABLE spoken_language
    ADD CONSTRAINT fk_spoken_language_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_spoken_language_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour duration_unit */
ALTER TABLE duration_unit
    ADD CONSTRAINT fk_duration_unit_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_duration_unit_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour activity_type */
ALTER TABLE activity_type
    ADD CONSTRAINT fk_activity_type_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_activity_type_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour location */
ALTER TABLE location
    ADD CONSTRAINT fk_location_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_location_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour contact */
ALTER TABLE contact
    ADD CONSTRAINT fk_contact_location FOREIGN KEY (location_id) REFERENCES location(location_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_contact_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_contact_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour entities */
ALTER TABLE entities
    ADD CONSTRAINT fk_entities_type FOREIGN KEY (entity_type_id) REFERENCES entity_types(entity_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_entities_parent FOREIGN KEY (parent_entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_entities_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour institution */
ALTER TABLE institution
    ADD CONSTRAINT fk_institution_type FOREIGN KEY (institution_type_id) REFERENCES institution_type(institution_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_institution_community FOREIGN KEY (community_id) REFERENCES community(community_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_institution_legal_status FOREIGN KEY (legal_status_id) REFERENCES legal_status(legal_status_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_institution_network FOREIGN KEY (network_id) REFERENCES network(network_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_institution_authority FOREIGN KEY (authority_id) REFERENCES authority(authority_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_institution_education_level FOREIGN KEY (education_level_id) REFERENCES education_level(education_level_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_institution_contact FOREIGN KEY (contact_id) REFERENCES contact(contact_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_institution_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_institution_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_institution_owner FOREIGN KEY (event_owner_id) REFERENCES event_owner(event_owner_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_institution_entity FOREIGN KEY (entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE;

/* Contraintes pour campus */
ALTER TABLE campus
    ADD CONSTRAINT fk_campus_type FOREIGN KEY (campus_type_id) REFERENCES campus_type(campus_type_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_campus_contact FOREIGN KEY (contact_id) REFERENCES contact(contact_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_campus_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_campus_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_campus_owner FOREIGN KEY (event_owner_id) REFERENCES event_owner(event_owner_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_campus_housing_owner FOREIGN KEY (housing_owner_id) REFERENCES housing_owner(housing_owner_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_campus_entity FOREIGN KEY (entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE;

/* Contraintes pour company */
ALTER TABLE company
    ADD CONSTRAINT fk_company_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_company_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_company_responsible_user FOREIGN KEY (responsible_user_id) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_company_type FOREIGN KEY (company_type_id) REFERENCES company_type(company_type_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_company_contact FOREIGN KEY (contact_id) REFERENCES contact(contact_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_company_event_owner FOREIGN KEY (event_owner_id) REFERENCES event_owner(event_owner_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_company_housing_owner FOREIGN KEY (housing_owner_id) REFERENCES housing_owner(housing_owner_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_company_entity FOREIGN KEY (entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE;

/* Contraintes pour landlord */
ALTER TABLE landlord
    ADD CONSTRAINT fk_landlord_owner FOREIGN KEY (housing_owner_id) REFERENCES housing_owner(housing_owner_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_landlord_contact FOREIGN KEY (contact_id) REFERENCES contact(contact_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_landlord_entity FOREIGN KEY (entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE;

/* Contraintes pour entity_user */
ALTER TABLE entity_user
    ADD CONSTRAINT fk_entity_users_entity FOREIGN KEY (entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_entity_users_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_entity_users_role FOREIGN KEY (role_id) REFERENCES role(role_id) ON DELETE RESTRICT;

/* Contraintes pour student */
ALTER TABLE student
    ADD CONSTRAINT fk_student_entity FOREIGN KEY (entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_student_study_level FOREIGN KEY (study_level_id) REFERENCES study_level(study_level_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_student_contact FOREIGN KEY (contact_id) REFERENCES contact(contact_id) ON DELETE SET NULL;

/* Contraintes pour superadmin_invitation */
ALTER TABLE superadmin_invitation
    ADD CONSTRAINT fk_invitation_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_invitation_type FOREIGN KEY (invitation_type_id) REFERENCES invitation_type(invitation_type_id) ON DELETE RESTRICT;

/* Contraintes pour superadmin_invitation_entity */
ALTER TABLE superadmin_invitation_entity
    ADD CONSTRAINT fk_invitation_entity_invitation FOREIGN KEY (superadmin_invitation_id) REFERENCES superadmin_invitation(superadmin_invitation_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_invitation_entity_role FOREIGN KEY (role_id) REFERENCES role(role_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_invitation_entity_entity FOREIGN KEY (entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE;

/* Contraintes pour campus_allowed_domain */
ALTER TABLE campus_allowed_domain
    ADD CONSTRAINT fk_campus_allowed_domain_campus FOREIGN KEY (campus_id) REFERENCES campus(campus_id) ON DELETE CASCADE;

/* Contraintes pour login_attempt */
ALTER TABLE login_attempt
    ADD CONSTRAINT fk_login_attempt_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_login_attempt_user_agent FOREIGN KEY (user_agent_id) REFERENCES user_agents(user_agent_id) ON DELETE SET NULL;

/* Contraintes pour sessionEvents */
ALTER TABLE sessionEvents
    ADD CONSTRAINT fk_session_events_users FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_session_events_user_agent FOREIGN KEY (user_agent_id) REFERENCES user_agents(user_agent_id) ON DELETE SET NULL;

/* Contraintes pour refresh_token */
ALTER TABLE refresh_token
    ADD CONSTRAINT fk_refresh_token_users FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_refresh_token_sessionEvents FOREIGN KEY (sessionEvent_id) REFERENCES sessionEvents(sessionEvent_id) ON DELETE CASCADE;

/* Contraintes pour notification */
ALTER TABLE notification
    ADD CONSTRAINT fk_notification_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_notification_type FOREIGN KEY (notification_type_id) REFERENCES notification_type(notification_type_id) ON DELETE RESTRICT;

/* Contraintes pour student_friendship */
ALTER TABLE student_friendship
    ADD CONSTRAINT fk_student_friendship_student_1 FOREIGN KEY (student_id_1) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_student_friendship_student_2 FOREIGN KEY (student_id_2) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_student_friendship_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_student_friendship_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour cv */
ALTER TABLE cv
    ADD CONSTRAINT fk_cv_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE;

/* Contraintes pour experience */
ALTER TABLE experience
    ADD CONSTRAINT fk_experience_cv FOREIGN KEY (cv_id) REFERENCES cv(cv_id) ON DELETE CASCADE;

/* Contraintes pour education */
ALTER TABLE education
    ADD CONSTRAINT fk_education_cv FOREIGN KEY (cv_id) REFERENCES cv(cv_id) ON DELETE CASCADE;

/* Contraintes pour cv_skill */
ALTER TABLE cv_skill
    ADD CONSTRAINT fk_cv_skill_cv FOREIGN KEY (cv_id) REFERENCES cv(cv_id) ON DELETE CASCADE;

/* Contraintes pour cv_language */
ALTER TABLE cv_language
    ADD CONSTRAINT fk_cv_language_cv FOREIGN KEY (cv_id) REFERENCES cv(cv_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_cv_language_language FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT;

/* Contraintes pour payment_plan */
ALTER TABLE payment_plan
    ADD CONSTRAINT fk_payment_plan_currency FOREIGN KEY (currency_id) REFERENCES currency(currency_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_payment_plan_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_payment_plan_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour payment */
ALTER TABLE payment
    ADD CONSTRAINT fk_payment_plan FOREIGN KEY (payment_plan_id) REFERENCES payment_plan(payment_plan_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_payment_owner_type FOREIGN KEY (owner_type_id) REFERENCES owner_type(owner_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_payment_tax FOREIGN KEY (tax_id) REFERENCES tax(tax_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_payment_currency FOREIGN KEY (currency_id) REFERENCES currency(currency_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_payment_method FOREIGN KEY (payment_method_id) REFERENCES payment_method(payment_method_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_payment_status FOREIGN KEY (payment_status_id) REFERENCES payment_status(payment_status_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_payment_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_payment_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour housing */
ALTER TABLE housing
    ADD CONSTRAINT fk_housing_location FOREIGN KEY (location_id) REFERENCES location(location_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_housing_type FOREIGN KEY (housing_type_id) REFERENCES housing_type(housing_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_housing_peb_rating FOREIGN KEY (peb_rating_id) REFERENCES peb_rating(peb_rating_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_housing_owner FOREIGN KEY (housing_owner_id) REFERENCES housing_owner(housing_owner_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_housing_payment FOREIGN KEY (payment_id) REFERENCES payment(payment_id) ON DELETE SET NULL;

/* Contraintes pour housing_amenity */
ALTER TABLE housing_amenity
    ADD CONSTRAINT fk_housing_amenity_housing FOREIGN KEY (housing_id) REFERENCES housing(housing_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_housing_amenity_amenity FOREIGN KEY (amenity_id) REFERENCES amenity(amenity_id) ON DELETE CASCADE;

/* Contraintes pour housing_application */
ALTER TABLE housing_application
    ADD CONSTRAINT fk_housing_application_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_housing_application_housing FOREIGN KEY (housing_id) REFERENCES housing(housing_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_housing_application_status FOREIGN KEY (application_status_id) REFERENCES application_status(application_status_id) ON DELETE SET NULL;

/* Contraintes pour housing_visit */
ALTER TABLE housing_visit
    ADD CONSTRAINT fk_housing_visit_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_housing_visit_housing FOREIGN KEY (housing_id) REFERENCES housing(housing_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_housing_visit_status FOREIGN KEY (application_status_id) REFERENCES application_status(application_status_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_housing_visit_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_housing_visit_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour housing_visit_range */
ALTER TABLE housing_visit_range
    ADD CONSTRAINT fk_housing_visit_range_visit FOREIGN KEY (housing_visit_id) REFERENCES housing_visit(housing_visit_id) ON DELETE CASCADE;

/* Contraintes pour offer */
ALTER TABLE offer
    ADD CONSTRAINT fk_offer_location FOREIGN KEY (location_id) REFERENCES location(location_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_offer_company FOREIGN KEY (company_id) REFERENCES company(company_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_offer_type FOREIGN KEY (offer_type_id) REFERENCES offer_type(offer_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_offer_duration_unit FOREIGN KEY (duration_unit_id) REFERENCES duration_unit(duration_unit_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_offer_contract_type FOREIGN KEY (contract_type_id) REFERENCES contract_type(contract_type_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_offer_schedule_type FOREIGN KEY (schedule_type_id) REFERENCES schedule_type(schedule_type_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_offer_language FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_offer_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_offer_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_offer_payment FOREIGN KEY (payment_id) REFERENCES payment(payment_id) ON DELETE SET NULL;

/* Contraintes pour application */
ALTER TABLE application
    ADD CONSTRAINT fk_application_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_application_offer FOREIGN KEY (offer_id) REFERENCES offer(offer_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_application_status FOREIGN KEY (application_status_id) REFERENCES application_status(application_status_id) ON DELETE SET NULL;

/* Contraintes pour offer_favorite */
ALTER TABLE offer_favorite
    ADD CONSTRAINT fk_offer_favorite_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_offer_favorite_offer FOREIGN KEY (offer_id) REFERENCES offer(offer_id) ON DELETE CASCADE;

/* Contraintes pour work_hours */
ALTER TABLE work_hours
    ADD CONSTRAINT fk_work_hours_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_work_hours_offer FOREIGN KEY (offer_id) REFERENCES offer(offer_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_work_hours_employer FOREIGN KEY (employer_id) REFERENCES company(company_id) ON DELETE CASCADE;

/* Contraintes pour entity_review */
ALTER TABLE entity_review
    ADD CONSTRAINT fk_entity_review_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_entity_review_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_entity_review_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour student_referral */
ALTER TABLE student_referral
    ADD CONSTRAINT fk_referral_referring_student FOREIGN KEY (referring_student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_referral_referred_student FOREIGN KEY (referred_student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_referral_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_referral_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour payment_log */
ALTER TABLE payment_log
    ADD CONSTRAINT fk_payment_log_payment FOREIGN KEY (payment_id) REFERENCES payment(payment_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_payment_log_previous_status FOREIGN KEY (previous_status_id) REFERENCES payment_status(payment_status_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_payment_log_new_status FOREIGN KEY (new_status_id) REFERENCES payment_status(payment_status_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_payment_log_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour payment_item */
ALTER TABLE payment_item
    ADD CONSTRAINT fk_payment_item_payment FOREIGN KEY (payment_id) REFERENCES payment(payment_id) ON DELETE CASCADE;

/* Contraintes pour payment_item_entity */
ALTER TABLE payment_item_entity
    ADD CONSTRAINT fk_payment_item_entity_item FOREIGN KEY (payment_item_id) REFERENCES payment_item(payment_item_id) ON DELETE CASCADE;

/* Contraintes pour refund */
ALTER TABLE refund
    ADD CONSTRAINT fk_refund_payment FOREIGN KEY (payment_id) REFERENCES payment(payment_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_refund_currency FOREIGN KEY (currency_id) REFERENCES currency(currency_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_refund_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_refund_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour campus_facility */
ALTER TABLE campus_facility
    ADD CONSTRAINT fk_campus_facility_campus FOREIGN KEY (campus_id) REFERENCES campus(campus_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_campus_facility_type FOREIGN KEY (facility_type_id) REFERENCES facility_type(facility_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_campus_facility_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_campus_facility_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour degree */
ALTER TABLE degree
    ADD CONSTRAINT fk_degree_campus FOREIGN KEY (campus_id) REFERENCES campus(campus_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_degree_type FOREIGN KEY (degree_type_id) REFERENCES degree_type(degree_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_degree_category FOREIGN KEY (degree_category_id) REFERENCES degree_category(degree_category_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_degree_duration_unit FOREIGN KEY (duration_unit_id) REFERENCES duration_unit(duration_unit_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_degree_tuition_type FOREIGN KEY (tuition_type_id) REFERENCES tuition_type(tuition_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_degree_language FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_degree_schedule_type FOREIGN KEY (schedule_type_id) REFERENCES schedule_type(schedule_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_degree_certification_type FOREIGN KEY (certification_type_id) REFERENCES certification_type(certification_type_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_degree_delivery_mode FOREIGN KEY (delivery_mode_id) REFERENCES delivery_mode(delivery_mode_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_degree_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_degree_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour degree_partnership */
ALTER TABLE degree_partnership
    ADD CONSTRAINT fk_degree_partnership_degree FOREIGN KEY (degree_id) REFERENCES degree(degree_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_degree_partnership_company FOREIGN KEY (company_id) REFERENCES company(company_id) ON DELETE SET NULL;

/* Contraintes pour offer_degree */
ALTER TABLE offer_degree
    ADD CONSTRAINT fk_offer_degree_offer FOREIGN KEY (offer_id) REFERENCES offer(offer_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_offer_degree_degree FOREIGN KEY (degree_id) REFERENCES degree(degree_id) ON DELETE CASCADE;

/* Contraintes pour specialty */
ALTER TABLE specialty
    ADD CONSTRAINT fk_specialty_degree FOREIGN KEY (degree_id) REFERENCES degree(degree_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_specialty_domain FOREIGN KEY (domain_id) REFERENCES study_domain(domain_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_specialty_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_specialty_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour ue */
ALTER TABLE ue
    ADD CONSTRAINT fk_ue_specialty FOREIGN KEY (specialty_id) REFERENCES specialty(specialty_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_ue_semester FOREIGN KEY (semester_id) REFERENCES semester(semester_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_ue_prerequisite FOREIGN KEY (prerequisite_ue_id) REFERENCES ue(ue_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_ue_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_ue_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour ua */
ALTER TABLE ua
    ADD CONSTRAINT fk_ua_ue FOREIGN KEY (ue_id) REFERENCES ue(ue_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_ua_activity_type FOREIGN KEY (activity_type_id) REFERENCES activity_type(activity_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_ua_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_ua_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour bridge */
ALTER TABLE bridge
    ADD CONSTRAINT fk_bridge_from_degree FOREIGN KEY (from_degree_id) REFERENCES degree(degree_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_bridge_to_degree FOREIGN KEY (to_degree_id) REFERENCES degree(degree_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_bridge_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_bridge_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour degree_prerequisite */
ALTER TABLE degree_prerequisite
    ADD CONSTRAINT fk_degree_prerequisite_degree FOREIGN KEY (degree_id) REFERENCES degree(degree_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_degree_prerequisite_type FOREIGN KEY (prerequisite_type_id) REFERENCES prerequisite_type(prerequisite_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_degree_prerequisite_source FOREIGN KEY (prerequisite_source_id) REFERENCES prerequisite_source(prerequisite_source_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_degree_prerequisite_required_degree FOREIGN KEY (required_degree_id) REFERENCES degree(degree_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_degree_prerequisite_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_degree_prerequisite_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour event */
ALTER TABLE event
    ADD CONSTRAINT fk_event_location FOREIGN KEY (location_id) REFERENCES location(location_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_event_owner FOREIGN KEY (event_owner_id) REFERENCES event_owner(event_owner_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_event_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_event_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_event_payment FOREIGN KEY (payment_id) REFERENCES payment(payment_id) ON DELETE SET NULL;

/* Contraintes pour event_interest */
ALTER TABLE event_interest
    ADD CONSTRAINT fk_event_interest_event FOREIGN KEY (event_id) REFERENCES event(event_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_event_interest_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_event_interest_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_event_interest_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour image */
ALTER TABLE image
    ADD CONSTRAINT fk_image_mime_type FOREIGN KEY (mime_type_id) REFERENCES mime_type(mime_type_id) ON DELETE RESTRICT,
    ADD CONSTRAINT fk_image_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_image_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;

/* Contraintes pour entity_like */
ALTER TABLE entity_like
    ADD CONSTRAINT fk_like_student FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    ADD CONSTRAINT fk_like_created_by FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE SET NULL,
    ADD CONSTRAINT fk_like_updated_by FOREIGN KEY (updated_by) REFERENCES users(user_id) ON DELETE SET NULL;


/************************************************************************************
    Translation
************************************************************************************/

/* super_role_translations */
CREATE TABLE super_role_translations (
    super_role_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    super_role_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_super_role_lang (super_role_id, language_id),
    FOREIGN KEY (super_role_id) REFERENCES super_role(super_role_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* community_translations */
CREATE TABLE community_translations (
    community_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    community_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_community_lang (community_id, language_id),
    FOREIGN KEY (community_id) REFERENCES community(community_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* legal_status_translations */
CREATE TABLE legal_status_translations (
    legal_status_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    legal_status_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_legal_status_lang (legal_status_id, language_id),
    FOREIGN KEY (legal_status_id) REFERENCES legal_status(legal_status_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* education_level_translations */
CREATE TABLE education_level_translations (
    education_level_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    education_level_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_education_level_lang (education_level_id, language_id),
    FOREIGN KEY (education_level_id) REFERENCES education_level(education_level_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* network_translations */
CREATE TABLE network_translations (
    network_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    network_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_network_lang (network_id, language_id),
    FOREIGN KEY (network_id) REFERENCES network(network_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* authority_translations */
CREATE TABLE authority_translations (
    authority_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    authority_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_authority_lang (authority_id, language_id),
    FOREIGN KEY (authority_id) REFERENCES authority(authority_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* campus_type_translations */
CREATE TABLE campus_type_translations (
    campus_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    campus_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_campus_type_lang (campus_type_id, language_id),
    FOREIGN KEY (campus_type_id) REFERENCES campus_type(campus_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* company_type_translations */
CREATE TABLE company_type_translations (
    company_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    company_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_company_type_lang (company_type_id, language_id),
    FOREIGN KEY (company_type_id) REFERENCES company_type(company_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* study_level_translations */
CREATE TABLE study_level_translations (
    study_level_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    study_level_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_study_level_lang (study_level_id, language_id),
    FOREIGN KEY (study_level_id) REFERENCES study_level(study_level_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* owner_type_translations */
CREATE TABLE owner_type_translations (
    owner_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    owner_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_owner_type_lang (owner_type_id, language_id),
    FOREIGN KEY (owner_type_id) REFERENCES owner_type(owner_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* housing_type_translations */
CREATE TABLE housing_type_translations (
    housing_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    housing_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_housing_type_lang (housing_type_id, language_id),
    FOREIGN KEY (housing_type_id) REFERENCES housing_type(housing_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* peb_rating_translations */
CREATE TABLE peb_rating_translations (
    peb_rating_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    peb_rating_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(10) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_peb_rating_lang (peb_rating_id, language_id),
    FOREIGN KEY (peb_rating_id) REFERENCES peb_rating(peb_rating_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* application_status_translations */
CREATE TABLE application_status_translations (
    application_status_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    application_status_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_application_status_lang (application_status_id, language_id),
    FOREIGN KEY (application_status_id) REFERENCES application_status(application_status_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* offer_type_translations */
CREATE TABLE offer_type_translations (
    offer_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    offer_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_offer_type_lang (offer_type_id, language_id),
    FOREIGN KEY (offer_type_id) REFERENCES offer_type(offer_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* contract_type_translations */
CREATE TABLE contract_type_translations (
    contract_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    contract_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_contract_type_lang (contract_type_id, language_id),
    FOREIGN KEY (contract_type_id) REFERENCES contract_type(contract_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* schedule_type_translations */
CREATE TABLE schedule_type_translations (
    schedule_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    schedule_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_schedule_type_lang (schedule_type_id, language_id),
    FOREIGN KEY (schedule_type_id) REFERENCES schedule_type(schedule_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;


/* duration_unit_translations */
CREATE TABLE duration_unit_translations (
    duration_unit_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    duration_unit_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(20) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_duration_unit_lang (duration_unit_id, language_id),
    FOREIGN KEY (duration_unit_id) REFERENCES duration_unit(duration_unit_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* facility_type_translations */
CREATE TABLE facility_type_translations (
    facility_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    facility_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_facility_type_lang (facility_type_id, language_id),
    FOREIGN KEY (facility_type_id) REFERENCES facility_type(facility_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* degree_category_translations */
CREATE TABLE degree_category_translations (
    degree_category_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    degree_category_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_degree_category_lang (degree_category_id, language_id),
    FOREIGN KEY (degree_category_id) REFERENCES degree_category(degree_category_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* tuition_type_translations */
CREATE TABLE tuition_type_translations (
    tuition_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    tuition_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_tuition_type_lang (tuition_type_id, language_id),
    FOREIGN KEY (tuition_type_id) REFERENCES tuition_type(tuition_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* cycle_translations */
CREATE TABLE cycle_translations (
    cycle_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    cycle_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_cycle_lang (cycle_id, language_id),
    FOREIGN KEY (cycle_id) REFERENCES cycle(cycle_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* certification_type_translations */
CREATE TABLE certification_type_translations (
    certification_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    certification_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_certification_type_lang (certification_type_id, language_id),
    FOREIGN KEY (certification_type_id) REFERENCES certification_type(certification_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* delivery_mode_translations */
CREATE TABLE delivery_mode_translations (
    delivery_mode_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    delivery_mode_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_delivery_mode_lang (delivery_mode_id, language_id),
    FOREIGN KEY (delivery_mode_id) REFERENCES delivery_mode(delivery_mode_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* mime_type_translations */
CREATE TABLE mime_type_translations (
    mime_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    mime_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_mime_type_lang (mime_type_id, language_id),
    FOREIGN KEY (mime_type_id) REFERENCES mime_type(mime_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* amenity_translations */
CREATE TABLE amenity_translations (
    amenity_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    amenity_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_amenity_lang (amenity_id, language_id),
    FOREIGN KEY (amenity_id) REFERENCES amenity(amenity_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* notification_type_translations */
CREATE TABLE notification_type_translations (
    notification_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    notification_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_notification_type_lang (notification_type_id, language_id),
    FOREIGN KEY (notification_type_id) REFERENCES notification_type(notification_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* study_domain_translations */
CREATE TABLE study_domain_translations (
    study_domain_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    domain_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_study_domain_lang (domain_id, language_id),
    FOREIGN KEY (domain_id) REFERENCES study_domain(domain_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* semester_translations */
CREATE TABLE semester_translations (
    semester_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    semester_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_semester_lang (semester_id, language_id),
    FOREIGN KEY (semester_id) REFERENCES semester(semester_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* activity_type_translations */
CREATE TABLE activity_type_translations (
    activity_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    activity_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_activity_type_lang (activity_type_id, language_id),
    FOREIGN KEY (activity_type_id) REFERENCES activity_type(activity_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* prerequisite_type_translations */
CREATE TABLE prerequisite_type_translations (
    prerequisite_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    prerequisite_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_prerequisite_type_lang (prerequisite_type_id, language_id),
    FOREIGN KEY (prerequisite_type_id) REFERENCES prerequisite_type(prerequisite_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* prerequisite_source_translations */
CREATE TABLE prerequisite_source_translations (
    prerequisite_source_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    prerequisite_source_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description VARCHAR(191),
    UNIQUE KEY uk_prerequisite_source_lang (prerequisite_source_id, language_id),
    FOREIGN KEY (prerequisite_source_id) REFERENCES prerequisite_source(prerequisite_source_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* currency_translations */
CREATE TABLE currency_translations (
    currency_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    currency_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_currency_lang (currency_id, language_id),
    FOREIGN KEY (currency_id) REFERENCES currency(currency_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* payment_method_translations */
CREATE TABLE payment_method_translations (
    payment_method_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    payment_method_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_payment_method_lang (payment_method_id, language_id),
    FOREIGN KEY (payment_method_id) REFERENCES payment_method(payment_method_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* tax_translations */
CREATE TABLE tax_translations (
    tax_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    tax_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_tax_lang (tax_id, language_id),
    FOREIGN KEY (tax_id) REFERENCES tax(tax_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* payment_status_translations */
CREATE TABLE payment_status_translations (
    payment_status_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    payment_status_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_payment_status_lang (payment_status_id, language_id),
    FOREIGN KEY (payment_status_id) REFERENCES payment_status(payment_status_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* degree_type_translations */
CREATE TABLE degree_type_translations (
    degree_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    degree_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_degree_type_lang (degree_type_id, language_id),
    FOREIGN KEY (degree_type_id) REFERENCES degree_type(degree_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* token_type_translations */
CREATE TABLE token_type_translations (
    token_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    token_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_token_type_lang (token_type_id, language_id),
    FOREIGN KEY (token_type_id) REFERENCES token_type(token_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* invitation_type_translations */
CREATE TABLE invitation_type_translations (
    invitation_type_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    invitation_type_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_invitation_type_lang (invitation_type_id, language_id),
    FOREIGN KEY (invitation_type_id) REFERENCES invitation_type(invitation_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/************************************************************************************
    Translation - Tables Dépendantes
************************************************************************************/

/* entities_translations */
CREATE TABLE entities_translations (
    entities_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    entity_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(191) NOT NULL,
    UNIQUE KEY uk_entities_lang (entity_id, language_id),
    FOREIGN KEY (entity_id) REFERENCES entities(entity_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* institution_translations */
CREATE TABLE institution_translations (
    institution_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    institution_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_acronym VARCHAR(10),
    translated_description TEXT,
    translated_target_audience VARCHAR(50),
    UNIQUE KEY uk_institution_lang (institution_id, language_id),
    FOREIGN KEY (institution_id) REFERENCES institution(institution_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* campus_translations */
CREATE TABLE campus_translations (
    campus_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    campus_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_acronym VARCHAR(10),
    translated_description TEXT,
    UNIQUE KEY uk_campus_lang (campus_id, language_id),
    FOREIGN KEY (campus_id) REFERENCES campus(campus_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* company_translations */
CREATE TABLE company_translations (
    company_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    company_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(100) NOT NULL,
    translated_acronym VARCHAR(10),
    translated_description TEXT,
    translated_sector VARCHAR(50),
    UNIQUE KEY uk_company_lang (company_id, language_id),
    FOREIGN KEY (company_id) REFERENCES company(company_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* student_translations */
CREATE TABLE student_translations (
    student_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    student_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_bio TEXT,
    translated_study_field VARCHAR(100),
    UNIQUE KEY uk_student_lang (student_id, language_id),
    FOREIGN KEY (student_id) REFERENCES student(student_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* payment_plan_translations */
CREATE TABLE payment_plan_translations (
    payment_plan_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    payment_plan_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_payment_plan_lang (payment_plan_id, language_id),
    FOREIGN KEY (payment_plan_id) REFERENCES payment_plan(payment_plan_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* housing_translations */
CREATE TABLE housing_translations (
    housing_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    housing_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_title VARCHAR(100) NOT NULL,
    translated_description TEXT NOT NULL,
    translated_preferences TEXT,
    UNIQUE KEY uk_housing_lang (housing_id, language_id),
    FOREIGN KEY (housing_id) REFERENCES housing(housing_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* offer_translations */
CREATE TABLE offer_translations (
    offer_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    offer_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_title VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_offer_lang (offer_id, language_id),
    FOREIGN KEY (offer_id) REFERENCES offer(offer_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* cv_translations */
CREATE TABLE cv_translations (
    cv_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    cv_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_title VARCHAR(100) NOT NULL,
    translated_objective TEXT,
    UNIQUE KEY uk_cv_lang (cv_id, language_id),
    FOREIGN KEY (cv_id) REFERENCES cv(cv_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* experience_translations */
CREATE TABLE experience_translations (
    experience_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    experience_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_job_title VARCHAR(100) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_experience_lang (experience_id, language_id),
    FOREIGN KEY (experience_id) REFERENCES experience(experience_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* education_translations */
CREATE TABLE education_translations (
    education_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    education_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_degree VARCHAR(100) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_education_lang (education_id, language_id),
    FOREIGN KEY (education_id) REFERENCES education(education_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* cv_skill_translations */
CREATE TABLE cv_skill_translations (
    cv_skill_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    cv_skill_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    UNIQUE KEY uk_cv_skill_lang (cv_skill_id, language_id),
    FOREIGN KEY (cv_skill_id) REFERENCES cv_skill(cv_skill_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* degree_translations */
CREATE TABLE degree_translations (
    degree_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    degree_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_degree_lang (degree_id, language_id),
    FOREIGN KEY (degree_id) REFERENCES degree(degree_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* specialty_translations */
CREATE TABLE specialty_translations (
    specialty_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    specialty_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(50) NOT NULL,
    translated_description TEXT,
    translated_outcomes TEXT,
    UNIQUE KEY uk_specialty_lang (specialty_id, language_id),
    FOREIGN KEY (specialty_id) REFERENCES specialty(specialty_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* ue_translations */
CREATE TABLE ue_translations (
    ue_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    ue_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(100) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_ue_lang (ue_id, language_id),
    FOREIGN KEY (ue_id) REFERENCES ue(ue_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* ua_translations */
CREATE TABLE ua_translations (
    ua_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    ua_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_name VARCHAR(100) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_ua_lang (ua_id, language_id),
    FOREIGN KEY (ua_id) REFERENCES ua(ua_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* bridge_translations */
CREATE TABLE bridge_translations (
    bridge_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    bridge_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_bridge_lang (bridge_id, language_id),
    FOREIGN KEY (bridge_id) REFERENCES bridge(bridge_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* degree_prerequisite_translations */
CREATE TABLE degree_prerequisite_translations (
    degree_prerequisite_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    degree_prerequisite_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_description TEXT NOT NULL,
    UNIQUE KEY uk_degree_prerequisite_lang (degree_prerequisite_id, language_id),
    FOREIGN KEY (degree_prerequisite_id) REFERENCES degree_prerequisite(degree_prerequisite_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* event_translations */
CREATE TABLE event_translations (
    event_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    event_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_title VARCHAR(100) NOT NULL,
    translated_description TEXT,
    UNIQUE KEY uk_event_lang (event_id, language_id),
    FOREIGN KEY (event_id) REFERENCES event(event_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* notification_translations */
CREATE TABLE notification_translations (
    notification_translation_id INT AUTO_INCREMENT PRIMARY KEY,
    notification_id INT NOT NULL,
    language_id INT NOT NULL,
    translated_message TEXT NOT NULL,
    UNIQUE KEY uk_notification_lang (notification_id, language_id),
    FOREIGN KEY (notification_id) REFERENCES notification(notification_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB;


/*************************************************************************************************
    5. Index Supplémentaires
**************************************************************************************************/

/* Index pour optimiser les requêtes fréquentes */
CREATE INDEX idx_payment_owner ON payment (owner_type_id, owner_id); -- Index sur les paiements par propriétaire
CREATE INDEX idx_payment_status ON payment (payment_status_id); -- Index sur le statut des paiements
CREATE INDEX idx_housing_price ON housing (price); -- Index sur le prix des logements
CREATE INDEX idx_housing_size ON housing (size); -- Index sur la taille des logements
CREATE INDEX idx_users_email ON users (email); -- Index sur l’email des utilisateurs
CREATE INDEX idx_entity_user_user ON entity_user (user_id); -- Index sur les utilisateurs dans entity_user
CREATE INDEX idx_login_attempt_email ON login_attempt (email); -- Index sur l’email des tentatives de connexion
CREATE INDEX idx_login_attempt_ip_address ON login_attempt (ip_address); -- Index sur l’IP des tentatives
CREATE INDEX idx_login_attempt_time ON login_attempt (attempt_time); -- Index sur la date des tentatives
CREATE INDEX idx_student_friendship_student ON student_friendship (student_id_1, student_id_2); -- Index sur les relations d’amitié
CREATE INDEX idx_housing_visit_housing ON housing_visit (housing_id); -- Index sur les visites par logement
CREATE INDEX idx_housing_status ON housing (status); -- Index sur le statut des logements
CREATE INDEX idx_offer_deadline ON offer (deadline); -- Index sur la date limite des offres
CREATE INDEX idx_offer_company ON offer (company_id); -- Index sur les offres par entreprise
CREATE INDEX idx_application_student ON application (student_id); -- Index sur les candidatures par étudiant
CREATE INDEX idx_degree_campus ON degree (campus_id); -- Index sur les diplômes par campus
CREATE INDEX idx_specialty_degree ON specialty (degree_id); -- Index sur les spécialités par diplôme
CREATE INDEX idx_event_owner ON event (event_owner_id); -- Index sur les événements par propriétaire
CREATE INDEX idx_notification_user ON notification (user_id, is_read); -- Index sur les notifications par utilisateur et état
CREATE INDEX idx_location_postal_code ON location (postal_code); -- Index sur le code postal des localisations
CREATE INDEX idx_location_city ON location (city); -- Index sur la ville des localisations
CREATE INDEX idx_location_geo ON location (latitude, longitude); -- Index sur les coordonnées géographiques

/* Index recommandés pour login_attempt et sessionEvents */
CREATE INDEX IX_LoginAttempts_Email_AttemptTime ON login_attempt (email, attempt_time); -- Index combiné pour email et heure des tentatives
CREATE INDEX IX_SessionEvents_UserId_AttemptTime ON sessionEvents (user_id, eventTime); -- Index combiné pour utilisateur et heure des événements

/* Index pour accélérer les recherches par entité dans entity_user */
CREATE INDEX idx_entity_user_entity ON entity_user (entity_id);

/* Index pour filtrer les notifications par type */
CREATE INDEX idx_notification_type ON notification (notification_type_id);

/* Index pour les recherches de logements par propriétaire */
CREATE INDEX idx_housing_owner ON housing (housing_owner_id);

/* Index pour les recherches d'offres par type d’offre */
CREATE INDEX idx_offer_type ON offer (offer_type_id);

/* Index pour accélérer les recherches par langue dans entity_type_translations */
CREATE INDEX idx_entity_type_translations_language ON entity_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans institution_type_translations */
CREATE INDEX idx_institution_type_translations_language ON institution_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans role_translations */
CREATE INDEX idx_role_translations_language ON role_translations (language_id);

/* Index pour accélérer les recherches par langue dans super_role_translations */
CREATE INDEX idx_super_role_translations_language ON super_role_translations (language_id);

/* Index pour accélérer les recherches par langue dans community_translations */
CREATE INDEX idx_community_translations_language ON community_translations (language_id);

/* Index pour accelérer les recherches par langue dans legal_status_translations */
CREATE INDEX idx_legal_status_translations_language ON legal_status_translations (language_id);

/* Index pour accélérer les recherches par langue dans education_level_translations */
CREATE INDEX idx_education_level_translations_language ON education_level_translations (language_id);

/* Index pour accélérer les recherches par langue dans network_translations */
CREATE INDEX idx_network_translations_language ON network_translations (language_id);

/* Index pour accélérer les recherches par langue dans authority_translations */
CREATE INDEX idx_authority_translations_language ON authority_translations (language_id);

/* Index pour accélérer les recherches par langue dans campus_type_translations */
CREATE INDEX idx_campus_type_translations_language ON campus_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans company_type_translations */
CREATE INDEX idx_company_type_translations_language ON company_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans study_level_translations */
CREATE INDEX idx_study_level_translations_language ON study_level_translations (language_id);

/* Index pour accélérer les recherches par langue dans owner_type_translations */
CREATE INDEX idx_owner_type_translations_language ON owner_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans housing_type_translations */
CREATE INDEX idx_housing_type_translations_language ON housing_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans peb_rating_translations */
CREATE INDEX idx_peb_rating_translations_language ON peb_rating_translations (language_id);

/* Index pour accélérer les recherches par langue dans application_status_translations */
CREATE INDEX idx_application_status_translations_language ON application_status_translations (language_id);

/* Index pour accélérer les recherches par langue dans offer_type_translations */
CREATE INDEX idx_offer_type_translations_language ON offer_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans contract_type_translations */
CREATE INDEX idx_contract_type_translations_language ON contract_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans schedule_type_translations */
CREATE INDEX idx_schedule_type_translations_language ON schedule_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans duration_unit_translations */
CREATE INDEX idx_duration_unit_translations_language ON duration_unit_translations (language_id);

/* Index pour accélérer les recherches par langue dans facility_type_translations */
CREATE INDEX idx_facility_type_translations_language ON facility_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans degree_category_translations */
CREATE INDEX idx_degree_category_translations_language ON degree_category_translations (language_id);

/* Index pour accélérer les recherches par langue dans tuition_type_translations */
CREATE INDEX idx_tuition_type_translations_language ON tuition_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans cycle_translations */
CREATE INDEX idx_cycle_translations_language ON cycle_translations (language_id);

/* Index pour accélérer les recherches par langue dans certification_type_translations */
CREATE INDEX idx_certification_type_translations_language ON certification_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans delivery_mode_translations */
CREATE INDEX idx_delivery_mode_translations_language ON delivery_mode_translations (language_id);

/* Index pour accélérer les recherches par langue dans mime_type_translations */
CREATE INDEX idx_mime_type_translations_language ON mime_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans amenity_translations */
CREATE INDEX idx_amenity_translations_language ON amenity_translations (language_id);

/* Index pour accélérer les recherches par langue dans notification_type_translations */
CREATE INDEX idx_notification_type_translations_language ON notification_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans study_domain_translations */
CREATE INDEX idx_study_domain_translations_language ON study_domain_translations (language_id);

/* Index pour accélérer les recherches par langue dans semester_translations */
CREATE INDEX idx_semester_translations_language ON semester_translations (language_id);

/* Index pour accélérer les recherches par langue dans activity_type_translations */
CREATE INDEX idx_activity_type_translations_language ON activity_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans prerequisite_type_translations */
CREATE INDEX idx_prerequisite_type_translations_language ON prerequisite_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans prerequisite_source_translations */
CREATE INDEX idx_prerequisite_source_translations_language ON prerequisite_source_translations (language_id);

/* Index pour accélérer les recherches par langue dans currency_translations */
CREATE INDEX idx_currency_translations_language ON currency_translations (language_id);

/* Index pour accélérer les recherches par langue dans payment_method_translations */
CREATE INDEX idx_payment_method_translations_language ON payment_method_translations (language_id);

/* Index pour accélérer les recherches par langue dans tax_translations */
CREATE INDEX idx_tax_translations_language ON tax_translations (language_id);

/* Index pour accélérer les recherches par langue dans payment_status_translations */
CREATE INDEX idx_payment_status_translations_language ON payment_status_translations (language_id);

/* Index pour accélérer les recherches par langue dans degree_type_translations */
CREATE INDEX idx_degree_type_translations_language ON degree_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans token_type_translations */
CREATE INDEX idx_token_type_translations_language ON token_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans invitation_type_translations */
CREATE INDEX idx_invitation_type_translations_language ON invitation_type_translations (language_id);

/* Index pour accélérer les recherches par langue dans entities_translations */
CREATE INDEX idx_entities_translations_language ON entities_translations (language_id);

/* Index pour accélérer les recherches par langue dans institution_translations */
CREATE INDEX idx_institution_translations_language ON institution_translations (language_id);

/* Index pour accélérer les recherches par langue dans campus_translations */
CREATE INDEX idx_campus_translations_language ON campus_translations (language_id);

/* Index pour accélérer les recherches par langue dans company_translations */
CREATE INDEX idx_company_translations_language ON company_translations (language_id);

/* Index pour accélérer les recherches par langue dans student_translations */
CREATE INDEX idx_student_translations_language ON student_translations (language_id);

/* Index pour accélérer les recherches par langue dans payment_plan_translations */
CREATE INDEX idx_payment_plan_translations_language ON payment_plan_translations (language_id);

/* Index pour accélérer les recherches par langue dans housing_translations */
CREATE INDEX idx_housing_translations_language ON housing_translations (language_id);

/* Index pour accélérer les recherches par langue dans offer_translations */
CREATE INDEX idx_offer_translations_language ON offer_translations (language_id);

/* Index pour accélérer les recherches par langue dans cv_translations */
CREATE INDEX idx_cv_translations_language ON cv_translations (language_id);

/* Index pour accélérer les recherches par langue dans experience_translations */
CREATE INDEX idx_experience_translations_language ON experience_translations (language_id);

/* Index pour accélérer les recherches par langue dans education_translations */
CREATE INDEX idx_education_translations_language ON education_translations (language_id);

/* Index pour accélérer les recherches par langue dans cv_skill_translations */
CREATE INDEX idx_cv_skill_translations_language ON cv_skill_translations (language_id);

/* Index pour accélérer les recherches par langue dans degree_translations */
CREATE INDEX idx_degree_translations_language ON degree_translations (language_id);

/* Index pour accélérer les recherches par langue dans specialty_translations */
CREATE INDEX idx_specialty_translations_language ON specialty_translations (language_id);

/* Index pour accélérer les recherches par langue dans ue_translations */
CREATE INDEX idx_ue_translations_language ON ue_translations (language_id);

/* Index pour accélérer les recherches par langue dans ua_translations */
CREATE INDEX idx_ua_translations_language ON ua_translations (language_id);

/* Index pour accélérer les recherches par langue dans bridge_translations */
CREATE INDEX idx_bridge_translations_language ON bridge_translations (language_id);

/* Index pour accélérer les recherches par langue dans degree_prerequisite_translations */
CREATE INDEX idx_degree_prerequisite_translations_language ON degree_prerequisite_translations (language_id);

/* Index pour accélérer les recherches par langue dans event_translations */
CREATE INDEX idx_event_translations_language ON event_translations (language_id);

/* Index pour accélérer les recherches par langue dans notification_translations */
CREATE INDEX idx_notification_translations_language ON notification_translations (language_id);






-- ✅ Étape 1 : Renommer la colonne d'expiration du token
ALTER TABLE token_type 
CHANGE COLUMN default_expiration_minutes default_token_expiration_minutes INT;

-- ✅ Étape 2 : Ajouter la durée d’expiration OTP par défaut
ALTER TABLE token_type 
ADD COLUMN default_otp_expiration_minutes INT NOT NULL DEFAULT 120;

-- ✅ Étape 3 : Ajouter les paramètres de limitation
UPDATE token_type
SET 
    token_required = 1,
    code_otp_required = 1,
    max_otp_attempts = 5,
    default_token_expiration_minutes = 1440, -- 24h
    default_otp_expiration_minutes = 120,    -- 2h
    min_delay_minutes = 0,
    is_rate_limited = 1,
    max_requests_per_window = 3,
    rate_limit_window_minutes = 60
WHERE name = 'EMAIL_CONFIRMATION';

UPDATE token_type
SET default_otp_expiration_minutes = 120
WHERE name = 'PASSWORD_RESET';

-- ✅ Étape 4 : Renommer la colonne expires_at dans security_token
ALTER TABLE security_token 
CHANGE COLUMN expires_at token_expires_at DATETIME;

-- ✅ Étape 5 : Ajouter otp_expires_at et revoke_reason
ALTER TABLE security_token 
ADD COLUMN otp_expires_at DATETIME NULL AFTER token_expires_at;

ALTER TABLE security_token 
ADD COLUMN revoke_reason VARCHAR(100) NULL AFTER revoked_at;

ALTER TABLE security_token 
ADD COLUMN otp_revoked_at DATETIME NULL;

-- ✅ Étape 6 : Exemple de réinitialisation d’un token
UPDATE security_token
SET 
  otp_revoked_at = NOW(),
  revoked = 0,
  revoked_at = NULL,
  revoke_reason = NULL
WHERE security_token_id = 45;

-- ✅ Étape 7 : Créer la table otp_send_log
CREATE TABLE otp_send_log (
    otp_send_log_id INT AUTO_INCREMENT PRIMARY KEY,
    security_token_id INT NOT NULL,
    sent_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    user_agent_id INT,
    ip_address VARCHAR(45),
    FOREIGN KEY (security_token_id) REFERENCES security_token(security_token_id) ON DELETE CASCADE,
    FOREIGN KEY (user_agent_id) REFERENCES user_agents(user_agent_id) ON DELETE SET NULL
) ENGINE=InnoDB;

-- Table pour les types de documents légaux (ex. : CGU, politique de confidentialité)
CREATE TABLE legal_document_type (
    document_type_id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Identifiant unique du type de document',
    name VARCHAR(100) NOT NULL UNIQUE COMMENT 'Nom du type de document (ex. : TermsOfService)',
    description TEXT COMMENT 'Description du type de document',
    INDEX idx_name (name) COMMENT 'Index pour recherches par nom'
) ENGINE=InnoDB COMMENT 'Types de documents légaux';

-- Table pour les traductions des types de documents
CREATE TABLE legal_document_type_translation (
    translation_id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Identifiant unique de la traduction',
    document_type_id INT NOT NULL COMMENT 'Type de document associé',
    language_id INT NOT NULL COMMENT 'Langue de la traduction',
    name VARCHAR(100) NOT NULL COMMENT 'Nom traduit du type de document',
    description TEXT COMMENT 'Description traduite',
    UNIQUE (document_type_id, language_id) COMMENT 'Garantit une seule traduction par langue',
    FOREIGN KEY (document_type_id) REFERENCES legal_document_type(document_type_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE CASCADE
) ENGINE=InnoDB COMMENT 'Traductions des types de documents';

-- Table pour les versions des documents légaux
CREATE TABLE legal_document (
    document_id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Identifiant unique du document',
    document_type_id INT NOT NULL COMMENT 'Type de document associé',
    version VARCHAR(20) NOT NULL COMMENT 'Version du document (ex. : 1.0.0)',
    published_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Date de publication',
    valid_until DATETIME COMMENT 'Date de fin de validité (NULL si actif)',
    is_active BOOLEAN DEFAULT TRUE COMMENT 'Indique si le document est actif',
    FOREIGN KEY (document_type_id) REFERENCES legal_document_type(document_type_id) ON DELETE CASCADE,
    INDEX idx_is_active (is_active) COMMENT 'Index pour recherches par statut actif',
    INDEX idx_document_type_id (document_type_id) COMMENT 'Index pour recherches par type'
) ENGINE=InnoDB COMMENT 'Versions des documents légaux';

-- Table des clauses associées à un document légal (ex. CGU, Politique, Mentions)
CREATE TABLE legal_document_clause (
    clause_id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Identifiant unique de la clause',
    document_id INT NOT NULL COMMENT 'Document légal auquel cette clause appartient',
    order_index INT NOT NULL COMMENT 'Ordre d’affichage de la clause dans le document',
    FOREIGN KEY (document_id) REFERENCES legal_document(document_id) ON DELETE CASCADE,
    INDEX idx_document_order (document_id, order_index) COMMENT 'Index pour trier les clauses dans un document'
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci
  COMMENT='Clauses structurées associées à un document légal';

-- Table des traductions multilingues des clauses
CREATE TABLE legal_document_clause_translation (
    translation_id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Identifiant unique de la traduction de clause',
    clause_id INT NOT NULL COMMENT 'Clause à laquelle cette traduction est liée',
    language_id INT NOT NULL COMMENT 'Langue de la traduction',
    title VARCHAR(255) COMMENT 'Titre de la clause (traduit)',
    content TEXT NOT NULL COMMENT 'Contenu traduit de la clause',
    UNIQUE (clause_id, language_id) COMMENT 'Une seule traduction par clause et par langue',
    FOREIGN KEY (clause_id) REFERENCES legal_document_clause(clause_id) ON DELETE CASCADE,
    FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE CASCADE
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci
  COMMENT='Traductions multilingues des clauses d’un document légal';

-- Table pour les consentements des utilisateurs aux documents légaux
CREATE TABLE user_legal_consent (
    consent_id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Identifiant unique du consentement',
    user_id INT NOT NULL COMMENT 'Utilisateur ayant donné le consentement',
    document_id INT NOT NULL COMMENT 'Document accepté',
    document_version VARCHAR(20) NOT NULL COMMENT 'Version du document acceptée',
    accepted_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Date d’acceptation',
    ip_address VARCHAR(45) COMMENT 'Adresse IP lors de l’acceptation',
    user_agent_id INT COMMENT 'Agent utilisateur associé',
    revoked BOOLEAN DEFAULT FALSE COMMENT 'Indique si le consentement est révoqué',
    revoked_at DATETIME COMMENT 'Date de révocation (NULL si non révoqué)',
    revoke_reason VARCHAR(100) COMMENT 'Raison de la révocation',
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (document_id) REFERENCES legal_document(document_id) ON DELETE CASCADE,
    FOREIGN KEY (user_agent_id) REFERENCES user_agents(user_agent_id) ON DELETE SET NULL,
    INDEX idx_user_document (user_id, document_id, revoked) COMMENT 'Index pour recherches de consentements actifs'
) ENGINE=InnoDB COMMENT 'Consentements des utilisateurs aux documents légaux';

CREATE TABLE contact_message (
  message_id INT NOT NULL AUTO_INCREMENT COMMENT 'Identifiant unique du message de contact',
  full_name VARCHAR(100) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT 'Nom complet de l’expéditeur',
  email VARCHAR(255) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT 'Adresse email de contact',
  subject VARCHAR(255) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT 'Sujet du message',
  message TEXT COLLATE utf8mb4_unicode_ci NOT NULL COMMENT 'Contenu du message',
  language_id INT NOT NULL COMMENT 'Langue du message (référence à spoken_language)',
  user_id INT DEFAULT NULL COMMENT 'Utilisateur connecté (null si anonyme)',
  ip_address VARCHAR(45) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT 'Adresse IP de l’expéditeur',
  user_agent_id INT DEFAULT NULL COMMENT 'Identifiant du user agent utilisé',
  received_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Date et heure de réception du message',

  PRIMARY KEY (message_id),
  KEY idx_user_id (user_id),
  KEY idx_user_agent_id (user_agent_id),
  KEY idx_language_id (language_id),
  KEY idx_received_at (received_at),

  CONSTRAINT contact_message_ibfk_1 FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL,
  CONSTRAINT contact_message_ibfk_2 FOREIGN KEY (user_agent_id) REFERENCES user_agents(user_agent_id) ON DELETE SET NULL,
  CONSTRAINT fk_contact_message_language FOREIGN KEY (language_id) REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci
  COMMENT='Messages reçus via le formulaire de contact';

CREATE TABLE contact_message_type (
    contact_message_type_id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Identifiant unique du type de message',
    priority INT NOT NULL DEFAULT 0 COMMENT 'Niveau de priorité du type de message',
    is_active BOOLEAN NOT NULL DEFAULT TRUE COMMENT 'Le type est-il actif ?',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Date de création',
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Date de mise à jour',
    created_by INT DEFAULT NULL COMMENT 'Référence à l’utilisateur créateur',
    updated_by INT DEFAULT NULL COMMENT 'Référence à l’utilisateur ayant mis à jour'
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci
  COMMENT='Types de messages de contact supportés';

CREATE TABLE contact_message_type_translation (
    contact_message_type_translation_id INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Identifiant unique de la traduction',
    contact_message_type_id INT NOT NULL COMMENT 'Référence au type de message',
    language_id INT NOT NULL COMMENT 'Référence à la langue (spoken_language)',
    name VARCHAR(100) NOT NULL COMMENT 'Nom traduit du type de message',
    description TEXT NULL COMMENT 'Description traduite (optionnelle)',

    UNIQUE KEY uniq_type_language (contact_message_type_id, language_id),

    CONSTRAINT fk_msg_type_translation_type FOREIGN KEY (contact_message_type_id)
        REFERENCES contact_message_type(contact_message_type_id) ON DELETE CASCADE,
    CONSTRAINT fk_msg_type_translation_lang FOREIGN KEY (language_id)
        REFERENCES spoken_language(language_id) ON DELETE RESTRICT
) ENGINE=InnoDB
  DEFAULT CHARSET=utf8mb4
  COLLATE=utf8mb4_unicode_ci
  COMMENT='Traductions des types de messages de contact';

ALTER TABLE contact_message
ADD contact_message_type_id INT NULL,
ADD CONSTRAINT fk_contact_message_type FOREIGN KEY (contact_message_type_id) REFERENCES contact_message_type(contact_message_type_id);

ALTER TABLE contact_message_type ADD key_name VARCHAR(50) UNIQUE COMMENT 'Clé technique temporaire pour les inserts';

ALTER TABLE contact_message ADD phone VARCHAR(20) COMMENT 'Numéro de téléphone de l expediteur';
