/* Insertion dans spoken_language (table de base pour les traductions) */
INSERT INTO spoken_language (name, code, created_at, updated_at, created_by, updated_by) VALUES
    ('Français', 'fr', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL),
    ('Nederlands', 'nl', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL),
    ('Deutsch', 'de', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL),
    ('English', 'en', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL);

/* Insertion dans institution_type */
INSERT INTO institution_type (name, description) VALUES
    ('Université', 'Établissement d’enseignement supérieur universitaire (ex. UCLouvain, UGent)'),
    ('Haute École', 'Établissement d’enseignement supérieur non universitaire (ex. HE Vinci)'),
    ('École Secondaire', 'Établissement d’enseignement secondaire général ou technique'),
    ('Centre PMS', 'Centre psycho-médico-social, lié au soutien scolaire en Belgique'),
    ('Institut de Promotion Sociale', 'Établissement offrant des formations pour adultes');

/* Insertion dans institution_type_translations */
INSERT INTO institution_type_translations (institution_type_id, language_id, translated_name, translated_description) VALUES
    -- Université
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Université'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Université', 'Établissement d’enseignement supérieur universitaire (ex. UCLouvain, UGent)'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Université'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Universiteit', 'Hogeronderwijsinstelling van universitair niveau (bijv. UCLouvain, UGent)'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Université'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Universität', 'Hochschuleinrichtung auf Universitätsniveau (z. B. UCLouvain, UGent)'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Université'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'University', 'Higher education institution at university level (e.g., UCLouvain, UGent)'),
    -- Haute École
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Haute École'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Haute École', 'Établissement d’enseignement supérieur non universitaire (ex. HE Vinci)'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Haute École'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Hogeschool', 'Hogeronderwijsinstelling niet-universitair (bijv. HE Vinci)'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Haute École'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Fachhochschule', 'Hochschuleinrichtung nicht-universitär (z. B. HE Vinci)'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Haute École'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'University of Applied Sciences', 'Non-university higher education institution (e.g., HE Vinci)'),
    -- École Secondaire
    ((SELECT institution_type_id FROM institution_type WHERE name = 'École Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'École Secondaire', 'Établissement d’enseignement secondaire général ou technique'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'École Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Secundaire School', 'Instelling voor algemeen of technisch secundair onderwijs'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'École Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Sekundarschule', 'Einrichtung für allgemeine oder technische Sekundarbildung'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'École Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Secondary School', 'Institution for general or technical secondary education'),
    -- Centre PMS
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Centre PMS'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Centre PMS', 'Centre psycho-médico-social, lié au soutien scolaire en Belgique'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Centre PMS'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'PMS-Centrum', 'Psycho-medisch-sociaal centrum, verbonden met schoolondersteuning in België'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Centre PMS'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'PMS-Zentrum', 'Psycho-medizinisch-soziales Zentrum, verbunden mit schulischer Unterstützung in Belgien'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Centre PMS'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'PMS Center', 'Psycho-medical-social center linked to school support in Belgium'),
    -- Institut de Promotion Sociale
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Institut de Promotion Sociale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Institut de Promotion Sociale', 'Établissement offrant des formations pour adultes'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Institut de Promotion Sociale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Instituut voor Sociale Promotie', 'Instelling die opleidingen voor volwassenen aanbiedt'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Institut de Promotion Sociale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Institut für soziale Förderung', 'Einrichtung, die Erwachsenenbildung anbietet'),
    ((SELECT institution_type_id FROM institution_type WHERE name = 'Institut de Promotion Sociale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Institute of Social Promotion', 'Institution offering adult education');

/* Insertion dans community */
INSERT INTO community (name, description) VALUES
    ('Française', 'Communauté française de Belgique, gérant l’enseignement francophone'),
    ('Flamande', 'Communauté flamande de Belgique, gérant l’enseignement néerlandophone'),
    ('Germanophone', 'Communauté germanophone de Belgique, gérant l’enseignement en allemand'),
    ('Internationale', 'Communautés internationales présentes en Belgique (ex. écoles européennes)');

/* Insertion dans community_translations */
INSERT INTO community_translations (community_id, language_id, translated_name, translated_description) VALUES
    -- Française
    ((SELECT community_id FROM community WHERE name = 'Française'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Française', 'Communauté française de Belgique, gérant l’enseignement francophone'),
    ((SELECT community_id FROM community WHERE name = 'Française'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Franse', 'Franse Gemeenschap van België, verantwoordelijk voor Franstalig onderwijs'),
    ((SELECT community_id FROM community WHERE name = 'Française'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Französisch', 'Französische Gemeinschaft Belgiens, zuständig für französischsprachigen Unterricht'),
    ((SELECT community_id FROM community WHERE name = 'Française'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'French', 'French Community of Belgium, managing French-speaking education'),
    -- Flamande
    ((SELECT community_id FROM community WHERE name = 'Flamande'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Flamande', 'Communauté flamande de Belgique, gérant l’enseignement néerlandophone'),
    ((SELECT community_id FROM community WHERE name = 'Flamande'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Vlaamse', 'Vlaamse Gemeenschap van België, verantwoordelijk voor Nederlandstalig onderwijs'),
    ((SELECT community_id FROM community WHERE name = 'Flamande'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Flämisch', 'Flämische Gemeinschaft Belgiens, zuständig für niederländischsprachigen Unterricht'),
    ((SELECT community_id FROM community WHERE name = 'Flamande'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Flemish', 'Flemish Community of Belgium, managing Dutch-speaking education'),
    -- Germanophone
    ((SELECT community_id FROM community WHERE name = 'Germanophone'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Germanophone', 'Communauté germanophone de Belgique, gérant l’enseignement en allemand'),
    ((SELECT community_id FROM community WHERE name = 'Germanophone'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Duitstalige', 'Duitstalige Gemeenschap van België, verantwoordelijk voor Duitstalig onderwijs'),
    ((SELECT community_id FROM community WHERE name = 'Germanophone'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Deutschsprachig', 'Deutschsprachige Gemeinschaft Belgiens, zuständig für deutschsprachigen Unterricht'),
    ((SELECT community_id FROM community WHERE name = 'Germanophone'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'German-speaking', 'German-speaking Community of Belgium, managing German-speaking education'),
    -- Internationale
    ((SELECT community_id FROM community WHERE name = 'Internationale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Internationale', 'Communautés internationales présentes en Belgique (ex. écoles européennes)'),
    ((SELECT community_id FROM community WHERE name = 'Internationale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Internationaal', 'Internationale gemeenschappen aanwezig in België (bijv. Europese scholen)'),
    ((SELECT community_id FROM community WHERE name = 'Internationale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'International', 'Internationale Gemeinschaften in Belgien (z. B. Europäische Schulen)'),
    ((SELECT community_id FROM community WHERE name = 'Internationale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'International', 'International communities present in Belgium (e.g., European schools)');

/* Insertion dans legal_status */
INSERT INTO legal_status (name, description) VALUES
    ('Public', 'Entité financée et gérée par l’État ou une communauté'),
    ('Privé', 'Entité privée, indépendante des fonds publics'),
    ('ASBL', 'Association Sans But Lucratif, forme courante pour les institutions éducatives'),
    ('SA', 'Société Anonyme, utilisée par certaines entreprises belges'),
    ('SPRL', 'Société Privée à Responsabilité Limitée, forme fréquente pour les PME');

/* Insertion dans legal_status_translations */
INSERT INTO legal_status_translations (legal_status_id, language_id, translated_name, translated_description) VALUES
    -- Public
    ((SELECT legal_status_id FROM legal_status WHERE name = 'Public'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Public', 'Entité financée et gérée par l’État ou une communauté'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'Public'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Openbaar', 'Entiteit gefinancierd en beheerd door de staat of een gemeenschap'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'Public'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Öffentlich', 'Einrichtung, die vom Staat oder einer Gemeinschaft finanziert und verwaltet wird'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'Public'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Public', 'Entity funded and managed by the state or a community'),
    -- Privé
    ((SELECT legal_status_id FROM legal_status WHERE name = 'Privé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Privé', 'Entité privée, indépendante des fonds publics'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'Privé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Privé', 'Private entiteit, onafhankelijk van publieke fondsen'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'Privé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Privat', 'Private Einrichtung, unabhängig von öffentlichen Mitteln'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'Privé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Private', 'Private entity, independent of public funds'),
    -- ASBL
    ((SELECT legal_status_id FROM legal_status WHERE name = 'ASBL'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'ASBL', 'Association Sans But Lucratif, forme courante pour les institutions éducatives'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'ASBL'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'VZW', 'Vereniging Zonder Winstoogmerk, veelvoorkomende vorm voor onderwijsinstellingen'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'ASBL'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'ASBL', 'Vereinigung ohne Gewinnzweck, häufige Form für Bildungseinrichtungen'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'ASBL'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'NPO', 'Non-Profit Association, common form for educational institutions'),
    -- SA
    ((SELECT legal_status_id FROM legal_status WHERE name = 'SA'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'SA', 'Société Anonyme, utilisée par certaines entreprises belges'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'SA'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'NV', 'Naamloze Vennootschap, gebruikt door sommige Belgische bedrijven'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'SA'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'AG', 'Aktiengesellschaft, genutzt von einigen belgischen Unternehmen'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'SA'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'PLC', 'Public Limited Company, used by some Belgian companies'),
    -- SPRL
    ((SELECT legal_status_id FROM legal_status WHERE name = 'SPRL'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'SPRL', 'Société Privée à Responsabilité Limitée, forme fréquente pour les PME'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'SPRL'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'BVBA', 'Besloten Vennootschap met Beperkte Aansprakelijkheid, veelvoorkomend bij KMO’s'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'SPRL'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'GmbH', 'Gesellschaft mit beschränkter Haftung, häufige Form für KMU'),
    ((SELECT legal_status_id FROM legal_status WHERE name = 'SPRL'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'LLC', 'Limited Liability Company, common form for SMEs');

/* Insertion dans education_level */
INSERT INTO education_level (name, description) VALUES
    ('Maternelle', 'Enseignement préscolaire (2,5 à 6 ans)'),
    ('Primaire', 'Enseignement primaire (6 à 12 ans)'),
    ('Secondaire', 'Enseignement secondaire général, technique ou professionnel (12 à 18 ans)'),
    ('Supérieur', 'Enseignement supérieur (bachelier, master, doctorat)'),
    ('Promotion Sociale', 'Formation continue pour adultes');

/* Insertion dans education_level_translations */
INSERT INTO education_level_translations (education_level_id, language_id, translated_name, translated_description) VALUES
    -- Maternelle
    ((SELECT education_level_id FROM education_level WHERE name = 'Maternelle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Maternelle', 'Enseignement préscolaire (2,5 à 6 ans)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Maternelle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Kleuteronderwijs', 'Voorschoolse opvoeding (2,5 tot 6 jaar)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Maternelle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Kindergarten', 'Vorschulunterricht (2,5 bis 6 Jahre)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Maternelle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Preschool', 'Preschool education (2.5 to 6 years)'),
    -- Primaire
    ((SELECT education_level_id FROM education_level WHERE name = 'Primaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Primaire', 'Enseignement primaire (6 à 12 ans)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Primaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Lager Onderwijs', 'Basisonderwijs (6 tot 12 jaar)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Primaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Primarschule', 'Primarunterricht (6 bis 12 Jahre)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Primaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Primary', 'Primary education (6 to 12 years)'),
    -- Secondaire
    ((SELECT education_level_id FROM education_level WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Secondaire', 'Enseignement secondaire général, technique ou professionnel (12 à 18 ans)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Secundair Onderwijs', 'Secundair onderwijs algemeen, technisch of beroeps (12 tot 18 jaar)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Sekundarschule', 'Sekundarunterricht allgemein, technisch oder beruflich (12 bis 18 Jahre)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Secondary', 'Secondary education general, technical, or vocational (12 to 18 years)'),
    -- Supérieur
    ((SELECT education_level_id FROM education_level WHERE name = 'Supérieur'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Supérieur', 'Enseignement supérieur (bachelier, master, doctorat)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Supérieur'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Hoger Onderwijs', 'Hoger onderwijs (bachelor, master, doctoraat)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Supérieur'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Höhere Bildung', 'Höhere Bildung (Bachelor, Master, Doktorat)'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Supérieur'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Higher Education', 'Higher education (bachelor, master, doctorate)'),
    -- Promotion Sociale
    ((SELECT education_level_id FROM education_level WHERE name = 'Promotion Sociale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Promotion Sociale', 'Formation continue pour adultes'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Promotion Sociale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Sociale Promotie', 'Voortgezette opleiding voor volwassenen'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Promotion Sociale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Soziale Förderung', 'Weiterbildung für Erwachsene'),
    ((SELECT education_level_id FROM education_level WHERE name = 'Promotion Sociale'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Social Promotion', 'Continuing education for adults');

/* Insertion dans network */
INSERT INTO network (name, description) VALUES
    ('Officiel', 'Réseau public organisé par les communautés ou les provinces'),
    ('Libre', 'Réseau d’enseignement libre, souvent confessionnel (ex. catholique)'),
    ('Communal', 'Réseau géré par les communes'),
    ('Wallonie-Bruxelles Enseignement', 'Réseau officiel de la Fédération Wallonie-Bruxelles'),
    ('GO!', 'Réseau officiel de la Communauté flamande');

/* Insertion dans network_translations */
INSERT INTO network_translations (network_id, language_id, translated_name, translated_description) VALUES
    -- Officiel
    ((SELECT network_id FROM network WHERE name = 'Officiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Officiel', 'Réseau public organisé par les communautés ou les provinces'),
    ((SELECT network_id FROM network WHERE name = 'Officiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Officieel', 'Openbaar netwerk georganiseerd door gemeenschappen of provincies'),
    ((SELECT network_id FROM network WHERE name = 'Officiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Offiziell', 'Öffentliches Netzwerk, organisiert von Gemeinschaften oder Provinzen'),
    ((SELECT network_id FROM network WHERE name = 'Officiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Official', 'Public network organized by communities or provinces'),
    -- Libre
    ((SELECT network_id FROM network WHERE name = 'Libre'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Libre', 'Réseau d’enseignement libre, souvent confessionnel (ex. catholique)'),
    ((SELECT network_id FROM network WHERE name = 'Libre'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Vrij', 'Vrij onderwijsnetwerk, vaak confessioneel (bijv. katholiek)'),
    ((SELECT network_id FROM network WHERE name = 'Libre'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Frei', 'Freies Bildungsnetzwerk, oft konfessionell (z. B. katholisch)'),
    ((SELECT network_id FROM network WHERE name = 'Libre'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Free', 'Free education network, often confessional (e.g., Catholic)'),
    -- Communal
    ((SELECT network_id FROM network WHERE name = 'Communal'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Communal', 'Réseau géré par les communes'),
    ((SELECT network_id FROM network WHERE name = 'Communal'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Gemeentelijk', 'Netwerk beheerd door gemeenten'),
    ((SELECT network_id FROM network WHERE name = 'Communal'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Kommunal', 'Netzwerk, das von Gemeinden verwaltet wird'),
    ((SELECT network_id FROM network WHERE name = 'Communal'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Municipal', 'Network managed by municipalities'),
    -- Wallonie-Bruxelles Enseignement
    ((SELECT network_id FROM network WHERE name = 'Wallonie-Bruxelles Enseignement'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Wallonie-Bruxelles Enseignement', 'Réseau officiel de la Fédération Wallonie-Bruxelles'),
    ((SELECT network_id FROM network WHERE name = 'Wallonie-Bruxelles Enseignement'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Wallonië-Brussel Onderwijs', 'Officieel netwerk van de Federatie Wallonië-Brussel'),
    ((SELECT network_id FROM network WHERE name = 'Wallonie-Bruxelles Enseignement'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Wallonien-Brüssel Bildung', 'Offizielles Netzwerk der Föderation Wallonien-Brüssel'),
    ((SELECT network_id FROM network WHERE name = 'Wallonie-Bruxelles Enseignement'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Wallonia-Brussels Education', 'Official network of the Wallonia-Brussels Federation'),
    -- GO!
    ((SELECT network_id FROM network WHERE name = 'GO!'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'GO!', 'Réseau officiel de la Communauté flamande'),
    ((SELECT network_id FROM network WHERE name = 'GO!'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'GO!', 'Officieel netwerk van de Vlaamse Gemeenschap'),
    ((SELECT network_id FROM network WHERE name = 'GO!'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'GO!', 'Offizielles Netzwerk der Flämischen Gemeinschaft'),
    ((SELECT network_id FROM network WHERE name = 'GO!'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'GO!', 'Official network of the Flemish Community');

/* Insertion dans authority */
INSERT INTO authority (name, description) VALUES
    ('Fédération Wallonie-Bruxelles', 'Autorité pour l’enseignement francophone'),
    ('Vlaamse Overheid', 'Autorité pour l’enseignement néerlandophone'),
    ('Ostbelgien', 'Autorité pour l’enseignement germanophone'),
    ('Ministère de l’Éducation', 'Autorité fédérale pour certains aspects éducatifs');

/* Insertion dans authority_translations */
INSERT INTO authority_translations (authority_id, language_id, translated_name, translated_description) VALUES
    -- Fédération Wallonie-Bruxelles
    ((SELECT authority_id FROM authority WHERE name = 'Fédération Wallonie-Bruxelles'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Fédération Wallonie-Bruxelles', 'Autorité pour l’enseignement francophone'),
    ((SELECT authority_id FROM authority WHERE name = 'Fédération Wallonie-Bruxelles'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Federatie Wallonië-Brussel', 'Autoriteit voor Franstalig onderwijs'),
    ((SELECT authority_id FROM authority WHERE name = 'Fédération Wallonie-Bruxelles'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Föderation Wallonien-Brüssel', 'Autorität für französischsprachigen Unterricht'),
    ((SELECT authority_id FROM authority WHERE name = 'Fédération Wallonie-Bruxelles'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Wallonia-Brussels Federation', 'Authority for French-speaking education'),
    -- Vlaamse Overheid
    ((SELECT authority_id FROM authority WHERE name = 'Vlaamse Overheid'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Vlaamse Overheid', 'Autorité pour l’enseignement néerlandophone'),
    ((SELECT authority_id FROM authority WHERE name = 'Vlaamse Overheid'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Vlaamse Overheid', 'Autoriteit voor Nederlandstalig onderwijs'),
    ((SELECT authority_id FROM authority WHERE name = 'Vlaamse Overheid'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Flämische Regierung', 'Autorität für niederländischsprachigen Unterricht'),
    ((SELECT authority_id FROM authority WHERE name = 'Vlaamse Overheid'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Flemish Government', 'Authority for Dutch-speaking education'),
    -- Ostbelgien
    ((SELECT authority_id FROM authority WHERE name = 'Ostbelgien'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Ostbelgien', 'Autorité pour l’enseignement germanophone'),
    ((SELECT authority_id FROM authority WHERE name = 'Ostbelgien'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Oostbelgië', 'Autoriteit voor Duitstalig onderwijs'),
    ((SELECT authority_id FROM authority WHERE name = 'Ostbelgien'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Ostbelgien', 'Autorität für deutschsprachigen Unterricht'),
    ((SELECT authority_id FROM authority WHERE name = 'Ostbelgien'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'East Belgium', 'Authority for German-speaking education'),
    -- Ministère de l’Éducation
    ((SELECT authority_id FROM authority WHERE name = 'Ministère de l’Éducation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Ministère de l’Éducation', 'Autorité fédérale pour certains aspects éducatifs'),
    ((SELECT authority_id FROM authority WHERE name = 'Ministère de l’Éducation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Ministerie van Onderwijs', 'Federale autoriteit voor bepaalde onderwijskwesties'),
    ((SELECT authority_id FROM authority WHERE name = 'Ministère de l’Éducation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Bildungsministerium', 'Föderale Autorität für bestimmte Bildungsfragen'),
    ((SELECT authority_id FROM authority WHERE name = 'Ministère de l’Éducation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Ministry of Education', 'Federal authority for certain educational aspects');

/* Insertion dans campus_type */
INSERT INTO campus_type (name, description) VALUES
    ('Principal', 'Campus principal d’une institution (ex. Louvain-la-Neuve)'),
    ('Secondaire', 'Campus secondaire ou annexe (ex. UCLouvain à Mons)'),
    ('Spécialisé', 'Campus dédié à une discipline (ex. médecine, ingénierie)'),
    ('International', 'Campus accueillant des étudiants internationaux');

/* Insertion dans campus_type_translations */
INSERT INTO campus_type_translations (campus_type_id, language_id, translated_name, translated_description) VALUES
    -- Principal
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Principal'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Principal', 'Campus principal d’une institution (ex. Louvain-la-Neuve)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Principal'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Hoofd', 'Hoofdcampus van een instelling (bijv. Louvain-la-Neuve)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Principal'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Haupt', 'Hauptcampus einer Institution (z. B. Louvain-la-Neuve)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Principal'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Main', 'Main campus of an institution (e.g., Louvain-la-Neuve)'),
    -- Secondaire
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Secondaire', 'Campus secondaire ou annexe (ex. UCLouvain à Mons)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Secundair', 'Secundaire campus of bijgebouw (bijv. UCLouvain in Mons)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Sekundär', 'Sekundärcampus oder Nebenstelle (z. B. UCLouvain in Mons)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Secondary', 'Secondary campus or annex (e.g., UCLouvain in Mons)'),
    -- Spécialisé
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Spécialisé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Spécialisé', 'Campus dédié à une discipline (ex. médecine, ingénierie)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Spécialisé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Gespecialiseerd', 'Campus gewijd aan een discipline (bijv. geneeskunde, ingenieurswetenschappen)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Spécialisé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Spezialisiert', 'Campus, der einer Disziplin gewidmet ist (z. B. Medizin, Ingenieurwesen)'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'Spécialisé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Specialized', 'Campus dedicated to a discipline (e.g., medicine, engineering)'),
    -- International
    ((SELECT campus_type_id FROM campus_type WHERE name = 'International'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'International', 'Campus accueillant des étudiants internationaux'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'International'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Internationaal', 'Campus die internationale studenten verwelkomt'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'International'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'International', 'Campus, der internationale Studenten aufnimmt'),
    ((SELECT campus_type_id FROM campus_type WHERE name = 'International'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'International', 'Campus welcoming international students');

/* Insertion dans company_type */
INSERT INTO company_type (name, description) VALUES
    ('PME', 'Petite ou Moyenne Entreprise, majoritaire en Belgique'),
    ('Grande Entreprise', 'Entreprise de plus grande échelle (ex. AB InBev)'),
    ('Start-up', 'Jeune entreprise innovante (ex. secteur tech à Bruxelles)'),
    ('Indépendant', 'Travailleur indépendant ou freelance');

/* Insertion dans company_type_translations */
INSERT INTO company_type_translations (company_type_id, language_id, translated_name, translated_description) VALUES
    -- PME
    ((SELECT company_type_id FROM company_type WHERE name = 'PME'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'PME', 'Petite ou Moyenne Entreprise, majoritaire en Belgique'),
    ((SELECT company_type_id FROM company_type WHERE name = 'PME'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'KMO', 'Kleine of Middelgrote Onderneming, overheersend in België'),
    ((SELECT company_type_id FROM company_type WHERE name = 'PME'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'KMU', 'Kleine oder mittlere Unternehmen, vorherrschend in Belgien'),
    ((SELECT company_type_id FROM company_type WHERE name = 'PME'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'SME', 'Small or Medium Enterprise, predominant in Belgium'),
    -- Grande Entreprise
    ((SELECT company_type_id FROM company_type WHERE name = 'Grande Entreprise'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Grande Entreprise', 'Entreprise de plus grande échelle (ex. AB InBev)'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Grande Entreprise'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Grote Onderneming', 'Onderneming van grotere schaal (bijv. AB InBev)'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Grande Entreprise'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Großunternehmen', 'Unternehmen von größerem Maßstab (z. B. AB InBev)'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Grande Entreprise'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Large Enterprise', 'Larger-scale enterprise (e.g., AB InBev)'),
    -- Start-up
    ((SELECT company_type_id FROM company_type WHERE name = 'Start-up'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Start-up', 'Jeune entreprise innovante (ex. secteur tech à Bruxelles)'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Start-up'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Start-up', 'Jonge innovatieve onderneming (bijv. technologiesector in Brussel)'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Start-up'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Start-up', 'Junges innovatives Unternehmen (z. B. Technologiesektor in Brüssel)'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Start-up'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Start-up', 'Young innovative company (e.g., tech sector in Brussels)'),
    -- Indépendant
    ((SELECT company_type_id FROM company_type WHERE name = 'Indépendant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Indépendant', 'Travailleur indépendant ou freelance'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Indépendant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Zelfstandige', 'Zelfstandige werker of freelancer'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Indépendant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Selbstständig', 'Selbstständiger Arbeiter oder Freelancer'),
    ((SELECT company_type_id FROM company_type WHERE name = 'Indépendant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Independent', 'Independent worker or freelancer');

/* Insertion dans study_level */
INSERT INTO study_level (name, description) VALUES
    ('Bachelier', 'Premier cycle universitaire ou supérieur (3 ans)'),
    ('Master', 'Deuxième cycle universitaire (1 ou 2 ans)'),
    ('Doctorat', 'Troisième cycle universitaire (recherche)'),
    ('Secondaire', 'Niveau atteint après l’enseignement secondaire'),
    ('Certificat', 'Formation courte ou professionnelle');

/* Insertion dans study_level_translations */
INSERT INTO study_level_translations (study_level_id, language_id, translated_name, translated_description) VALUES
    -- Bachelier
    ((SELECT study_level_id FROM study_level WHERE name = 'Bachelier'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Bachelier', 'Premier cycle universitaire ou supérieur (3 ans)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Bachelier'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bachelor', 'Eerste universitaire of hogere cyclus (3 jaar)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Bachelier'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Bachelor', 'Erster universitätrer oder höherer Zyklus (3 Jahre)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Bachelier'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Bachelor', 'First university or higher cycle (3 years)'),
    -- Master
    ((SELECT study_level_id FROM study_level WHERE name = 'Master'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Master', 'Deuxième cycle universitaire (1 ou 2 ans)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Master'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Master', 'Tweede universitaire cyclus (1 of 2 jaar)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Master'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Master', 'Zweiter universitätrer Zyklus (1 oder 2 Jahre)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Master'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Master', 'Second university cycle (1 or 2 years)'),
    -- Doctorat
    ((SELECT study_level_id FROM study_level WHERE name = 'Doctorat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Doctorat', 'Troisième cycle universitaire (recherche)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Doctorat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Doctoraat', 'Derde universitaire cyclus (onderzoek)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Doctorat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Doktorat', 'Dritter universitätrer Zyklus (Forschung)'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Doctorat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Doctorate', 'Third university cycle (research)'),
    -- Secondaire
    ((SELECT study_level_id FROM study_level WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Secondaire', 'Niveau atteint après l’enseignement secondaire'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Secundair', 'Niveau bereikt na secundair onderwijs'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Sekundar', 'Niveau erreicht nach der Sekundarbildung'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Secondaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Secondary', 'Level achieved after secondary education'),
    -- Certificat
    ((SELECT study_level_id FROM study_level WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Certificat', 'Formation courte ou professionnelle'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Certificaat', 'Korte of beroepsopleiding'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Zertifikat', 'Kurze oder berufliche Ausbildung'),
    ((SELECT study_level_id FROM study_level WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Certificate', 'Short or vocational training');

/* Insertion dans owner_type */
INSERT INTO owner_type (name, description) VALUES
    ('Campus', 'Logements gérés par un campus universitaire'),
    ('Landlord', 'Bailleur privé individuel'),
    ('Société Immobilière', 'Entreprise gérant des logements (ex. agence immobilière)'),
    ('Commune', 'Logements publics gérés par une commune');

/* Insertion dans owner_type_translations */
INSERT INTO owner_type_translations (owner_type_id, language_id, translated_name, translated_description) VALUES
    -- Campus
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Campus'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Campus', 'Logements gérés par un campus universitaire'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Campus'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Campus', 'Woningen beheerd door een universitaire campus'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Campus'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Campus', 'Wohnungen, die von einem Universitätsgelände verwaltet werden'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Campus'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Campus', 'Housing managed by a university campus'),
    -- Landlord
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Landlord'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Landlord', 'Bailleur privé individuel'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Landlord'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Verhuurder', 'Individuele privéverhuurder'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Landlord'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Vermieter', 'Individueller privater Vermieter'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Landlord'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Landlord', 'Individual private landlord'),
    -- Société Immobilière
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Société Immobilière'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Société Immobilière', 'Entreprise gérant des logements (ex. agence immobilière)'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Société Immobilière'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Vastgoedmaatschappij', 'Bedrijf dat woningen beheert (bijv. vastgoedkantoor)'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Société Immobilière'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Immobiliengesellschaft', 'Unternehmen, das Wohnungen verwaltet (z. B. Immobilienagentur)'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Société Immobilière'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Real Estate Company', 'Company managing housing (e.g., real estate agency)'),
    -- Commune
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Commune'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Commune', 'Logements publics gérés par une commune'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Commune'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Gemeente', 'Openbare woningen beheerd door een gemeente'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Commune'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Gemeinde', 'Öffentliche Wohnungen, die von einer Gemeinde verwaltet werden'),
    ((SELECT owner_type_id FROM owner_type WHERE name = 'Commune'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Municipality', 'Public housing managed by a municipality');

/* Insertion dans housing_type */
INSERT INTO housing_type (name, description) VALUES
    ('Kot', 'Logement étudiant typique en Belgique'),
    ('Appartement', 'Appartement classique pour étudiants ou familles'),
    ('Maison', 'Maison individuelle ou mitoyenne'),
    ('Résidence Étudiante', 'Résidence gérée par une institution ou privée');

/* Insertion dans housing_type_translations */
INSERT INTO housing_type_translations (housing_type_id, language_id, translated_name, translated_description) VALUES
    -- Kot
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Kot'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Kot', 'Logement étudiant typique en Belgique'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Kot'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Kot', 'Typische studentenhuisvesting in België'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Kot'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Kot', 'Typische Studentenunterkunft in Belgien'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Kot'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Kot', 'Typical student housing in Belgium'),
    -- Appartement
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Appartement'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Appartement', 'Appartement classique pour étudiants ou familles'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Appartement'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Appartement', 'Klassiek appartement voor studenten of gezinnen'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Appartement'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Wohnung', 'Klassische Wohnung für Studenten oder Familien'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Appartement'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Apartment', 'Classic apartment for students or families'),
    -- Maison
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Maison'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Maison', 'Maison individuelle ou mitoyenne'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Maison'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Huis', 'Eengezinswoning of rijhuis'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Maison'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Haus', 'Einfamilienhaus oder Reihenhaus'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Maison'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'House', 'Single-family house or terraced house'),
    -- Résidence Étudiante
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Résidence Étudiante'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Résidence Étudiante', 'Résidence gérée par une institution ou privée'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Résidence Étudiante'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Studentenresidentie', 'Residentie beheerd door een instelling of privé'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Résidence Étudiante'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Studentenresidenz', 'Residenz, die von einer Institution oder privat verwaltet wird'),
    ((SELECT housing_type_id FROM housing_type WHERE name = 'Résidence Étudiante'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Student Residence', 'Residence managed by an institution or private entity');

/* Insertion dans peb_rating */
INSERT INTO peb_rating (name, description) VALUES
    ('A', 'Très économe en énergie'),
    ('B', 'Économe en énergie'),
    ('C', 'Consommation modérée'),
    ('D', 'Consommation moyenne'),
    ('E', 'Peu économe'),
    ('F', 'Énergivore'),
    ('G', 'Très énergivore');

/* Insertion dans peb_rating_translations */
INSERT INTO peb_rating_translations (peb_rating_id, language_id, translated_name, translated_description) VALUES
    -- A
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'A'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'A', 'Très économe en énergie'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'A'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'A', 'Zeer energiezuinig'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'A'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'A', 'Sehr energieeffizient'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'A'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'A', 'Very energy-efficient'),
    -- B
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'B'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'B', 'Économe en énergie'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'B'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'B', 'Energiezuinig'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'B'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'B', 'Energieeffizient'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'B'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'B', 'Energy-efficient'),
    -- C
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'C'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'C', 'Consommation modérée'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'C'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'C', 'Matig verbruik'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'C'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'C', 'Moderater Verbrauch'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'C'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'C', 'Moderate consumption'),
    -- D
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'D'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'D', 'Consommation moyenne'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'D'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'D', 'Gemiddeld verbruik'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'D'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'D', 'Durchschnittlicher Verbrauch'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'D'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'D', 'Average consumption'),
    -- E
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'E'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'E', 'Peu économe'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'E'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'E', 'Weinig energiezuinig'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'E'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'E', 'Wenig energieeffizient'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'E'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'E', 'Low energy efficiency'),
    -- F
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'F'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'F', 'Énergivore'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'F'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'F', 'Energieverslindend'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'F'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'F', 'Energieintensiv'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'F'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'F', 'Energy-intensive'),
    -- G
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'G'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'G', 'Très énergivore'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'G'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'G', 'Zeer energieverslindend'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'G'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'G', 'Sehr energieintensiv'),
    ((SELECT peb_rating_id FROM peb_rating WHERE name = 'G'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'G', 'Very energy-intensive');

/* Insertion dans application_status */
INSERT INTO application_status (name, description) VALUES
    ('En attente', 'Candidature soumise, en cours d’examen'),
    ('Acceptée', 'Candidature retenue'),
    ('Refusée', 'Candidature non retenue'),
    ('En négociation', 'Discussion en cours avec le candidat');

/* Insertion dans application_status_translations */
INSERT INTO application_status_translations (application_status_id, language_id, translated_name, translated_description) VALUES
    -- En attente
    ((SELECT application_status_id FROM application_status WHERE name = 'En attente'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'En attente', 'Candidature soumise, en cours d’examen'),
    ((SELECT application_status_id FROM application_status WHERE name = 'En attente'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'In afwachting', 'Sollicitatie ingediend, in behandeling'),
    ((SELECT application_status_id FROM application_status WHERE name = 'En attente'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'In Bearbeitung', 'Bewerbung eingereicht, in Prüfung'),
    ((SELECT application_status_id FROM application_status WHERE name = 'En attente'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Pending', 'Application submitted, under review'),
    -- Acceptée
    ((SELECT application_status_id FROM application_status WHERE name = 'Acceptée'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Acceptée', 'Candidature retenue'),
    ((SELECT application_status_id FROM application_status WHERE name = 'Acceptée'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Geaccepteerd', 'Sollicitatie aanvaard'),
    ((SELECT application_status_id FROM application_status WHERE name = 'Acceptée'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Angenommen', 'Bewerbung akzeptiert'),
    ((SELECT application_status_id FROM application_status WHERE name = 'Acceptée'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Accepted', 'Application accepted'),
    -- Refusée
    ((SELECT application_status_id FROM application_status WHERE name = 'Refusée'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Refusée', 'Candidature non retenue'),
    ((SELECT application_status_id FROM application_status WHERE name = 'Refusée'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Afgewezen', 'Sollicitatie niet aanvaard'),
    ((SELECT application_status_id FROM application_status WHERE name = 'Refusée'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Abgelehnt', 'Bewerbung abgelehnt'),
    ((SELECT application_status_id FROM application_status WHERE name = 'Refusée'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Rejected', 'Application rejected'),
    -- En négociation
    ((SELECT application_status_id FROM application_status WHERE name = 'En négociation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'En négociation', 'Discussion en cours avec le candidat'),
    ((SELECT application_status_id FROM application_status WHERE name = 'En négociation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'In onderhandeling', 'Gesprek gaande met de kandidaat'),
    ((SELECT application_status_id FROM application_status WHERE name = 'En négociation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'In Verhandlung', 'Gespräch mit dem Kandidaten läuft'),
    ((SELECT application_status_id FROM application_status WHERE name = 'En négociation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'In negotiation', 'Discussion ongoing with the candidate');

/* Insertion dans offer_type */
INSERT INTO offer_type (name, description) VALUES
    ('Stage', 'Stage étudiant, souvent intégré dans un cursus'),
    ('Job étudiant', 'Emploi temporaire limité à 475 heures/an'),
    ('Alternance', 'Formation en alternance école-entreprise'),
    ('CDI', 'Contrat à durée indéterminée');

/* Insertion dans offer_type_translations */
INSERT INTO offer_type_translations (offer_type_id, language_id, translated_name, translated_description) VALUES
    -- Stage
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Stage'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Stage', 'Stage étudiant, souvent intégré dans un cursus'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Stage'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Stage', 'Studentenstage, vaak geïntegreerd in een curriculum'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Stage'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Praktikum', 'Studentenpraktikum, oft in ein Curriculum integriert'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Stage'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Internship', 'Student internship, often integrated into a curriculum'),
    -- Job étudiant
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Job étudiant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Job étudiant', 'Emploi temporaire limité à 475 heures/an'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Job étudiant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Studentenjob', 'Tijdelijke baan beperkt tot 475 uur/jaar'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Job étudiant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Studentenjob', 'Vorübergehender Job, begrenzt auf 475 Stunden/Jahr'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Job étudiant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Student Job', 'Temporary job limited to 475 hours/year'),
    -- Alternance
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Alternance'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Alternance', 'Formation en alternance école-entreprise'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Alternance'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Alternatie', 'Opleiding in afwisseling school-bedrijf'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Alternance'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Alternanzausbildung', 'Ausbildung im Wechsel Schule-Unternehmen'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'Alternance'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Apprenticeship', 'Training alternating between school and company'),
    -- CDI
    ((SELECT offer_type_id FROM offer_type WHERE name = 'CDI'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'CDI', 'Contrat à durée indéterminée'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'CDI'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Vast Contract', 'Contract van onbepaalde duur'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'CDI'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Unbefristeter Vertrag', 'Vertrag von unbestimmter Dauer'),
    ((SELECT offer_type_id FROM offer_type WHERE name = 'CDI'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Permanent Contract', 'Permanent contract');

/* Insertion dans contract_type */
INSERT INTO contract_type (name, description) VALUES
    ('CDD', 'Contrat à durée déterminée'),
    ('CDI', 'Contrat à durée indéterminée'),
    ('Intérim', 'Contrat via une agence d’intérim'),
    ('Étudiant', 'Contrat spécifique pour jobs étudiants');

/* Insertion dans contract_type_translations */
INSERT INTO contract_type_translations (contract_type_id, language_id, translated_name, translated_description) VALUES
    -- CDD
    ((SELECT contract_type_id FROM contract_type WHERE name = 'CDD'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'CDD', 'Contrat à durée déterminée'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'CDD'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bepaald Contract', 'Contract van bepaalde duur'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'CDD'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Befristeter Vertrag', 'Vertrag von bestimmter Dauer'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'CDD'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Fixed-term Contract', 'Fixed-term contract'),
    -- CDI
    ((SELECT contract_type_id FROM contract_type WHERE name = 'CDI'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'CDI', 'Contrat à durée indéterminée'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'CDI'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Vast Contract', 'Contract van onbepaalde duur'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'CDI'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Unbefristeter Vertrag', 'Vertrag von unbestimmter Dauer'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'CDI'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Permanent Contract', 'Permanent contract'),
    -- Intérim
    ((SELECT contract_type_id FROM contract_type WHERE name = 'Intérim'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Intérim', 'Contrat via une agence d’intérim'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'Intérim'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Interim', 'Contract via een uitzendbureau'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'Intérim'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Zeitarbeit', 'Vertrag über eine Zeitarbeitsagentur'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'Intérim'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Temporary Agency Contract', 'Contract via a temporary agency'),
    -- Étudiant
    ((SELECT contract_type_id FROM contract_type WHERE name = 'Étudiant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Étudiant', 'Contrat spécifique pour jobs étudiants'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'Étudiant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Student', 'Specifiek contract voor studentenjobs'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'Étudiant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Student', 'Spezifischer Vertrag für Studentenjobs'),
    ((SELECT contract_type_id FROM contract_type WHERE name = 'Étudiant'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Student', 'Specific contract for student jobs');

/* Insertion dans schedule_type */
INSERT INTO schedule_type (name, description) VALUES
    ('Temps plein', '38 heures/semaine, standard en Belgique'),
    ('Temps partiel', 'Moins de 38 heures/semaine'),
    ('Horaire flexible', 'Horaires adaptés aux besoins'),
    ('Soirée/Weekend', 'Travail en dehors des heures classiques');

/* Insertion dans schedule_type_translations */
INSERT INTO schedule_type_translations (schedule_type_id, language_id, translated_name, translated_description) VALUES
    -- Temps plein
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Temps plein'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Temps plein', '38 heures/semaine, standard en Belgique'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Temps plein'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Voltijds', '38 uur/week, standaard in België'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Temps plein'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Vollzeit', '38 Stunden/Woche, Standard in Belgien'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Temps plein'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Full-time', '38 hours/week, standard in Belgium'),
    -- Temps partiel
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Temps partiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Temps partiel', 'Moins de 38 heures/semaine'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Temps partiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Deeltijds', 'Minder dan 38 uur/week'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Temps partiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Teilzeit', 'Weniger als 38 Stunden/Woche'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Temps partiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Part-time', 'Less than 38 hours/week'),
    -- Horaire flexible
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Horaire flexible'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Horaire flexible', 'Horaires adaptés aux besoins'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Horaire flexible'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Flexibel uurrooster', 'Uurroosters aangepast aan de behoeften'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Horaire flexible'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Flexibler Zeitplan', 'Zeitpläne, die an die Bedürfnisse angepasst sind'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Horaire flexible'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Flexible schedule', 'Schedules adapted to needs'),
    -- Soirée/Weekend
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Soirée/Weekend'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Soirée/Weekend', 'Travail en dehors des heures classiques'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Soirée/Weekend'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Avond/Weekend', 'Werk buiten de klassieke uren'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Soirée/Weekend'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Abend/Wochenende', 'Arbeit außerhalb der üblichen Stunden'),
    ((SELECT schedule_type_id FROM schedule_type WHERE name = 'Soirée/Weekend'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Evening/Weekend', 'Work outside regular hours');

/* Insertion dans duration_unit */
INSERT INTO duration_unit (name, description) VALUES
    ('mois', 'Durée en mois, souvent utilisée pour les baux'),
    ('années', 'Durée en années, pour les cursus ou contrats'),
    ('semaines', 'Durée en semaines, pour stages courts'),
    ('jours', 'Durée en jours, pour événements ou formations');

/* Insertion dans duration_unit_translations */
INSERT INTO duration_unit_translations (duration_unit_id, language_id, translated_name, translated_description) VALUES
    -- mois
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'mois'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'mois', 'Durée en mois, souvent utilisée pour les baux'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'mois'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'maanden', 'Duur in maanden, vaak gebruikt voor huurovereenkomsten'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'mois'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Monate', 'Dauer in Monaten, oft für Mietverträge verwendet'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'mois'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'months', 'Duration in months, often used for leases'),
    -- années
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'années'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'années', 'Durée en années, pour les cursus ou contrats'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'années'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'jaren', 'Duur in jaren, voor cursussen of contracten'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'années'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Jahre', 'Dauer in Jahren, für Studiengänge oder Verträge'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'années'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'years', 'Duration in years, for courses or contracts'),
    -- semaines
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'semaines'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'semaines', 'Durée en semaines, pour stages courts'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'semaines'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'weken', 'Duur in weken, voor korte stages'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'semaines'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Wochen', 'Dauer in Wochen, für kurze Praktika'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'semaines'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'weeks', 'Duration in weeks, for short internships'),
    -- jours
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'jours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'jours', 'Durée en jours, pour événements ou formations'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'jours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'dagen', 'Duur in dagen, voor evenementen of opleidingen'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'jours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Tage', 'Dauer in Tagen, für Veranstaltungen oder Schulungen'),
    ((SELECT duration_unit_id FROM duration_unit WHERE name = 'jours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'days', 'Duration in days, for events or training');

/* Insertion dans currency */
INSERT INTO currency (code, name, description) VALUES
    ('EUR', 'Euro', 'Monnaie officielle en Belgique'),
    ('USD', 'Dollar américain', 'Utilisé pour transactions internationales');

/* Insertion dans currency_translations */
INSERT INTO currency_translations (currency_id, language_id, translated_name, translated_description) VALUES
    -- EUR
    ((SELECT currency_id FROM currency WHERE code = 'EUR'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Euro', 'Monnaie officielle en Belgique'),
    ((SELECT currency_id FROM currency WHERE code = 'EUR'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Euro', 'Officiële munt in België'),
    ((SELECT currency_id FROM currency WHERE code = 'EUR'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Euro', 'Offizielle Währung in Belgien'),
    ((SELECT currency_id FROM currency WHERE code = 'EUR'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Euro', 'Official currency in Belgium'),
    -- USD
    ((SELECT currency_id FROM currency WHERE code = 'USD'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Dollar américain', 'Utilisé pour transactions internationales'),
    ((SELECT currency_id FROM currency WHERE code = 'USD'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Amerikaanse Dollar', 'Gebruikt voor internationale transacties'),
    ((SELECT currency_id FROM currency WHERE code = 'USD'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'US-Dollar', 'Verwendet für internationale Transaktionen'),
    ((SELECT currency_id FROM currency WHERE code = 'USD'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'US Dollar', 'Used for international transactions');

/* Insertion dans payment_method */
INSERT INTO payment_method (name, description) VALUES
    ('Carte bancaire', 'Paiement par Bancontact ou carte de crédit'),
    ('Virement bancaire', 'Paiement par virement SEPA'),
    ('Payconiq', 'Paiement mobile populaire en Belgique'),
    ('Espèces', 'Paiement en liquide, moins fréquent');

/* Insertion dans payment_method_translations */
INSERT INTO payment_method_translations (payment_method_id, language_id, translated_name, translated_description) VALUES
    -- Carte bancaire
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Carte bancaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Carte bancaire', 'Paiement par Bancontact ou carte de crédit'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Carte bancaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bankkaart', 'Betaling via Bancontact of kredietkaart'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Carte bancaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Bankkarte', 'Zahlung per Bancontact oder Kreditkarte'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Carte bancaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Bank Card', 'Payment via Bancontact or credit card'),
    -- Virement bancaire
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Virement bancaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Virement bancaire', 'Paiement par virement SEPA'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Virement bancaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bankoverschrijving', 'Betaling via SEPA-overschrijving'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Virement bancaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Banküberweisung', 'Zahlung per SEPA-Überweisung'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Virement bancaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Bank Transfer', 'Payment via SEPA transfer'),
    -- Payconiq
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Payconiq'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Payconiq', 'Paiement mobile populaire en Belgique'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Payconiq'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Payconiq', 'Populaire mobiele betaling in België'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Payconiq'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Payconiq', 'Beliebte mobile Zahlung in Belgien'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Payconiq'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Payconiq', 'Popular mobile payment in Belgium'),
    -- Espèces
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Espèces'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Espèces', 'Paiement en liquide, moins fréquent'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Espèces'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Contant', 'Betaling in contanten, minder frequent'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Espèces'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Barzahlung', 'Zahlung in bar, weniger häufig'),
    ((SELECT payment_method_id FROM payment_method WHERE name = 'Espèces'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Cash', 'Cash payment, less frequent');

/* Insertion dans facility_type */
INSERT INTO facility_type (name, description) VALUES
    ('Bibliothèque', 'Bibliothèque universitaire ou publique'),
    ('Cafétéria', 'Espace de restauration pour étudiants'),
    ('Laboratoire', 'Laboratoire pour recherches ou cours pratiques'),
    ('Salle de sport', 'Infrastructure sportive sur le campus');

/* Insertion dans facility_type_translations */
INSERT INTO facility_type_translations (facility_type_id, language_id, translated_name, translated_description) VALUES
    -- Bibliothèque
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Bibliothèque'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Bibliothèque', 'Bibliothèque universitaire ou publique'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Bibliothèque'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bibliotheek', 'Universiteits- of openbare bibliotheek'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Bibliothèque'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Bibliothek', 'Universitäts- oder öffentliche Bibliothek'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Bibliothèque'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Library', 'University or public library'),
    -- Cafétéria
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Cafétéria'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Cafétéria', 'Espace de restauration pour étudiants'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Cafétéria'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Cafeteria', 'Eetruimte voor studenten'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Cafétéria'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Kantine', 'Essensraum für Studenten'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Cafétéria'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Cafeteria', 'Dining space for students'),
    -- Laboratoire
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Laboratoire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Laboratoire', 'Laboratoire pour recherches ou cours pratiques'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Laboratoire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Laboratorium', 'Laboratorium voor onderzoek of praktische lessen'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Laboratoire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Labor', 'Laboratorium für Forschung oder praktische Kurse'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Laboratoire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Laboratory', 'Laboratory for research or practical courses'),
    -- Salle de sport
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Salle de sport'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Salle de sport', 'Infrastructure sportive sur le campus'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Salle de sport'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Sporthal', 'Sportinfrastructuur op de campus'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Salle de sport'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Sporthalle', 'Sportinfrastruktur auf dem Campus'),
    ((SELECT facility_type_id FROM facility_type WHERE name = 'Salle de sport'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Sports Hall', 'Sports infrastructure on campus');

/* Insertion dans degree_category */
INSERT INTO degree_category (name, description) VALUES
    ('Sciences', 'Domaines scientifiques (physique, chimie, biologie)'),
    ('Arts', 'Domaines artistiques (musique, théâtre, arts visuels)'),
    ('Économie', 'Domaines économiques et de gestion'),
    ('Droit', 'Études juridiques');

/* Insertion dans degree_category_translations */
INSERT INTO degree_category_translations (degree_category_id, language_id, translated_name, translated_description) VALUES
    -- Sciences
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Sciences'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Sciences', 'Domaines scientifiques (physique, chimie, biologie)'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Sciences'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Wetenschappen', 'Wetenschappelijke domeinen (fysica, chemie, biologie)'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Sciences'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Wissenschaften', 'Wissenschaftliche Bereiche (Physik, Chemie, Biologie)'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Sciences'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Sciences', 'Scientific fields (physics, chemistry, biology)'),
    -- Arts
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Arts'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Arts', 'Domaines artistiques (musique, théâtre, arts visuels)'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Arts'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Kunsten', 'Artistieke domeinen (muziek, theater, beeldende kunsten)'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Arts'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Künste', 'Künstlerische Bereiche (Musik, Theater, bildende Künste)'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Arts'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Arts', 'Artistic fields (music, theater, visual arts)'),
    -- Économie
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Économie'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Économie', 'Domaines économiques et de gestion'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Économie'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Economie', 'Economische en managementdomeinen'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Économie'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Wirtschaft', 'Wirtschaftliche und Managementbereiche'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Économie'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Economics', 'Economic and management fields'),
    -- Droit
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Droit'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Droit', 'Études juridiques'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Droit'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Rechten', 'Juridische studies'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Droit'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Recht', 'Juristische Studien'),
    ((SELECT degree_category_id FROM degree_category WHERE name = 'Droit'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Law', 'Legal studies');

/* Insertion dans tuition_type */
INSERT INTO tuition_type (name, description) VALUES
    ('Annuel', 'Frais d’inscription annuels'),
    ('Semestriel', 'Frais d’inscription par semestre'),
    ('Mensuel', 'Frais d’inscription par mois'),
    ('Par cours', 'Frais selon les cours suivis');

/* Insertion dans tuition_type_translations */
INSERT INTO tuition_type_translations (tuition_type_id, language_id, translated_name, translated_description) VALUES
    -- Annuel
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Annuel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Annuel', 'Frais d’inscription annuels'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Annuel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Jaarlijks', 'Jaarlijkse inschrijvingskosten'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Annuel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Jährlich', 'Jährliche Einschreibungsgebühren'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Annuel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Annual', 'Annual tuition fees'),
    -- Semestriel
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Semestriel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Semestriel', 'Frais d’inscription par semestre'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Semestriel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Per semester', 'Inschrijvingskosten per semester'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Semestriel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Semesterweise', 'Einschreibungsgebühren pro Semester'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Semestriel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Per semester', 'Tuition fees per semester'),
    -- Mensuel
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Mensuel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Mensuel', 'Frais d’inscription par mois'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Mensuel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Maandelijks', 'Inschrijvingskosten per maand'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Mensuel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Monatlich', 'Einschreibungsgebühren pro Monat'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Mensuel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Monthly', 'Tuition fees per month'),
    -- Par cours
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Par cours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Par cours', 'Frais selon les cours suivis'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Par cours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Per cursus', 'Kosten afhankelijk van de gevolgde cursussen'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Par cours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Pro Kurs', 'Gebühren je nach belegten Kursen'),
    ((SELECT tuition_type_id FROM tuition_type WHERE name = 'Par cours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Per course', 'Fees based on courses taken');

/* Insertion dans cycle */
INSERT INTO cycle (name, description) VALUES
    ('Premier cycle', 'Niveau bachelier en Belgique'),
    ('Deuxième cycle', 'Niveau master en Belgique'),
    ('Troisième cycle', 'Niveau doctorat en Belgique');

/* Insertion dans cycle_translations */
INSERT INTO cycle_translations (cycle_id, language_id, translated_name, translated_description) VALUES
    -- Premier cycle
    ((SELECT cycle_id FROM cycle WHERE name = 'Premier cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Premier cycle', 'Niveau bachelier en Belgique'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Premier cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Eerste cyclus', 'Bachelorniveau in België'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Premier cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Erster Zyklus', 'Bachelorniveau in Belgien'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Premier cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'First cycle', 'Bachelor level in Belgium'),
    -- Deuxième cycle
    ((SELECT cycle_id FROM cycle WHERE name = 'Deuxième cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Deuxième cycle', 'Niveau master en Belgique'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Deuxième cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Tweede cyclus', 'Masterniveau in België'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Deuxième cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Zweiter Zyklus', 'Masterniveau in Belgien'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Deuxième cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Second cycle', 'Master level in Belgium'),
    -- Troisième cycle
    ((SELECT cycle_id FROM cycle WHERE name = 'Troisième cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Troisième cycle', 'Niveau doctorat en Belgique'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Troisième cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Derde cyclus', 'Doctoraatsniveau in België'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Troisième cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Dritter Zyklus', 'Doktorniveau in Belgien'),
    ((SELECT cycle_id FROM cycle WHERE name = 'Troisième cycle'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Third cycle', 'Doctorate level in Belgium');

/* Insertion dans certification_type */
INSERT INTO certification_type (name, description) VALUES
    ('Certificat', 'Certificat de formation courte'),
    ('Diplôme', 'Diplôme officiel d’études supérieures'),
    ('Attestation', 'Attestation de participation ou de réussite');

/* Insertion dans certification_type_translations */
INSERT INTO certification_type_translations (certification_type_id, language_id, translated_name, translated_description) VALUES
    -- Certificat
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Certificat', 'Certificat de formation courte'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Certificaat', 'Certificaat van korte opleiding'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Zertifikat', 'Zertifikat für Kurzschulung'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Certificate', 'Certificate of short training'),
    -- Diplôme
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Diplôme'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Diplôme', 'Diplôme officiel d’études supérieures'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Diplôme'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Diploma', 'Officieel diploma van hoger onderwijs'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Diplôme'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Diplom', 'Offizielles Diplom des Hochschulwesens'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Diplôme'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Diploma', 'Official higher education diploma'),
    -- Attestation
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Attestation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Attestation', 'Attestation de participation ou de réussite'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Attestation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Attest', 'Attest van deelname of slagen'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Attestation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Bescheinigung', 'Bescheinigung über Teilnahme oder Erfolg'),
    ((SELECT certification_type_id FROM certification_type WHERE name = 'Attestation'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Certificate', 'Certificate of participation or completion');

/* Insertion dans delivery_mode */
INSERT INTO delivery_mode (name, description) VALUES
    ('Présentiel', 'Cours en face-à-face'),
    ('À distance', 'Cours en ligne'),
    ('Hybride', 'Combinaison de présentiel et à distance');

/* Insertion dans delivery_mode_translations */
INSERT INTO delivery_mode_translations (delivery_mode_id, language_id, translated_name, translated_description) VALUES
    -- Présentiel
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'Présentiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Présentiel', 'Cours en face-à-face'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'Présentiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'In persoon', 'Lessen face-to-face'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'Présentiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Präsenz', 'Kurse von Angesicht zu Angesicht'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'Présentiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'In-person', 'Face-to-face courses'),
    -- À distance
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'À distance'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'À distance', 'Cours en ligne'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'À distance'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Op afstand', 'Online lessen'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'À distance'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Fernunterricht', 'Online-Kurse'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'À distance'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Remote', 'Online courses'),
    -- Hybride
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'Hybride'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Hybride', 'Combinaison de présentiel et à distance'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'Hybride'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Hybride', 'Combinatie van in persoon en op afstand'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'Hybride'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Hybrid', 'Kombination aus Präsenz und Fernunterricht'),
    ((SELECT delivery_mode_id FROM delivery_mode WHERE name = 'Hybride'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Hybrid', 'Combination of in-person and remote');

/* Insertion dans mime_type */
INSERT INTO mime_type (name, description) VALUES
    ('image/jpeg', 'Format d’image JPEG'),
    ('application/pdf', 'Document PDF'),
    ('text/plain', 'Fichier texte brut');

/* Insertion dans mime_type_translations */
INSERT INTO mime_type_translations (mime_type_id, language_id, translated_name, translated_description) VALUES
    -- image/jpeg
    ((SELECT mime_type_id FROM mime_type WHERE name = 'image/jpeg'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'image/jpeg', 'Format d’image JPEG'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'image/jpeg'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'image/jpeg', 'JPEG-beeldformaat'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'image/jpeg'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'image/jpeg', 'JPEG-Bildformat'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'image/jpeg'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'image/jpeg', 'JPEG image format'),
    -- application/pdf
    ((SELECT mime_type_id FROM mime_type WHERE name = 'application/pdf'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'application/pdf', 'Document PDF'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'application/pdf'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'application/pdf', 'PDF-document'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'application/pdf'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'application/pdf', 'PDF-Dokument'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'application/pdf'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'application/pdf', 'PDF document'),
    -- text/plain
    ((SELECT mime_type_id FROM mime_type WHERE name = 'text/plain'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'text/plain', 'Fichier texte brut'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'text/plain'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'text/plain', 'Platte tekstbestand'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'text/plain'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'text/plain', 'Nur-Text-Datei'),
    ((SELECT mime_type_id FROM mime_type WHERE name = 'text/plain'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'text/plain', 'Plain text file');

/* Insertion dans amenity */
INSERT INTO amenity (name, description) VALUES
    ('Wi-Fi', 'Connexion internet sans fil'),
    ('Cuisine', 'Cuisine équipée dans le logement'),
    ('Lave-linge', 'Machine à laver disponible'),
    ('Parking', 'Place de parking pour véhicules');

/* Insertion dans amenity_translations */
INSERT INTO amenity_translations (amenity_id, language_id, translated_name, translated_description) VALUES
    -- Wi-Fi
    ((SELECT amenity_id FROM amenity WHERE name = 'Wi-Fi'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Wi-Fi', 'Connexion internet sans fil'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Wi-Fi'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Wi-Fi', 'Draadloze internetverbinding'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Wi-Fi'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Wi-Fi', 'Drahtlose Internetverbindung'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Wi-Fi'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Wi-Fi', 'Wireless internet connection'),
    -- Cuisine
    ((SELECT amenity_id FROM amenity WHERE name = 'Cuisine'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Cuisine', 'Cuisine équipée dans le logement'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Cuisine'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Keuken', 'Uitgeruste keuken in de woning'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Cuisine'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Küche', 'Ausgestattete Küche in der Unterkunft'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Cuisine'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Kitchen', 'Equipped kitchen in the housing'),
    -- Lave-linge
    ((SELECT amenity_id FROM amenity WHERE name = 'Lave-linge'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Lave-linge', 'Machine à laver disponible'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Lave-linge'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Wasmachine', 'Beschikbare wasmachine'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Lave-linge'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Waschmaschine', 'Verfügbare Waschmaschine'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Lave-linge'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Washing machine', 'Available washing machine'),
    -- Parking
    ((SELECT amenity_id FROM amenity WHERE name = 'Parking'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Parking', 'Place de parking pour véhicules'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Parking'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Parkeerplaats', 'Parkeerplaats voor voertuigen'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Parking'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Parkplatz', 'Parkplatz für Fahrzeuge'),
    ((SELECT amenity_id FROM amenity WHERE name = 'Parking'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Parking', 'Parking space for vehicles');

/* Insertion dans notification_type */
INSERT INTO notification_type (name, description) VALUES
    ('Alerte', 'Notification urgente'),
    ('Information', 'Mise à jour ou annonce'),
    ('Rappel', 'Rappel d’échéance ou d’événement');

/* Insertion dans notification_type_translations */
INSERT INTO notification_type_translations (notification_type_id, language_id, translated_name, translated_description) VALUES
    -- Alerte
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Alerte'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Alerte', 'Notification urgente'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Alerte'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Alarm', 'Dringende melding'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Alerte'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Alarm', 'Dringende Benachrichtigung'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Alerte'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Alert', 'Urgent notification'),
    -- Information
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Information'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Information', 'Mise à jour ou annonce'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Information'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Informatie', 'Update of aankondiging'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Information'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Information', 'Aktualisierung oder Ankündigung'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Information'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Information', 'Update or announcement'),
    -- Rappel
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Rappel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Rappel', 'Rappel d’échéance ou d’événement'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Rappel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Herinnering', 'Herinnering aan een deadline of evenement'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Rappel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Erinnerung', 'Erinnerung an eine Frist oder ein Ereignis'),
    ((SELECT notification_type_id FROM notification_type WHERE name = 'Rappel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Reminder', 'Reminder of a deadline or event');

/* Insertion dans study_domain */
INSERT INTO study_domain (name, description) VALUES
    ('Informatique', 'Études en technologies de l’information'),
    ('Médecine', 'Études médicales et santé'),
    ('Ingénierie', 'Études en ingénierie et techniques'),
    ('Sciences humaines', 'Études en sciences sociales et humaines');

/* Insertion dans study_domain_translations */
INSERT INTO study_domain_translations (domain_id, language_id, translated_name, translated_description) VALUES
    -- Informatique
    ((SELECT domain_id FROM study_domain WHERE name = 'Informatique'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Informatique', 'Études en technologies de l’information'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Informatique'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Informatica', 'Studies in informatietechnologie'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Informatique'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Informatik', 'Studien in Informationstechnologie'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Informatique'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Computer Science', 'Studies in information technology'),
    -- Médecine
    ((SELECT domain_id FROM study_domain WHERE name = 'Médecine'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Médecine', 'Études médicales et santé'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Médecine'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Geneeskunde', 'Medische studies en gezondheid'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Médecine'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Medizin', 'Medizinische Studien und Gesundheit'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Médecine'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Medicine', 'Medical and health studies'),
    -- Ingénierie
    ((SELECT domain_id FROM study_domain WHERE name = 'Ingénierie'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Ingénierie', 'Études en ingénierie et techniques'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Ingénierie'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Ingenieurswetenschappen', 'Studies in ingenieurswetenschappen en technieken'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Ingénierie'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Ingenieurwesen', 'Studien in Ingenieurwesen und Technik'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Ingénierie'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Engineering', 'Studies in engineering and technology'),
    -- Sciences humaines
    ((SELECT domain_id FROM study_domain WHERE name = 'Sciences humaines'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Sciences humaines', 'Études en sciences sociales et humaines'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Sciences humaines'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Menswetenschappen', 'Studies in sociale en humane wetenschappen'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Sciences humaines'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Geisteswissenschaften', 'Studien in Sozial- und Geisteswissenschaften'),
    ((SELECT domain_id FROM study_domain WHERE name = 'Sciences humaines'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Human Sciences', 'Studies in social and human sciences');

/* Insertion dans semester */
INSERT INTO semester (name, description) VALUES
    ('Semestre 1', 'Premier semestre de l’année académique'),
    ('Semestre 2', 'Deuxième semestre de l’année académique');

/* Insertion dans semester_translations */
INSERT INTO semester_translations (semester_id, language_id, translated_name, translated_description) VALUES
    -- Semestre 1
    ((SELECT semester_id FROM semester WHERE name = 'Semestre 1'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Semestre 1', 'Premier semestre de l’année académique'),
    ((SELECT semester_id FROM semester WHERE name = 'Semestre 1'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Semester 1', 'Eerste semester van het academiejaar'),
    ((SELECT semester_id FROM semester WHERE name = 'Semestre 1'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Semester 1', 'Erstes Semester des akademischen Jahres'),
    ((SELECT semester_id FROM semester WHERE name = 'Semestre 1'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Semester 1', 'First semester of the academic year'),
    -- Semestre 2
    ((SELECT semester_id FROM semester WHERE name = 'Semestre 2'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Semestre 2', 'Deuxième semestre de l’année académique'),
    ((SELECT semester_id FROM semester WHERE name = 'Semestre 2'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Semester 2', 'Tweede semester van het academiejaar'),
    ((SELECT semester_id FROM semester WHERE name = 'Semestre 2'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Semester 2', 'Zweites Semester des akademischen Jahres'),
    ((SELECT semester_id FROM semester WHERE name = 'Semestre 2'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Semester 2', 'Second semester of the academic year');

/* Insertion dans activity_type */
INSERT INTO activity_type (name, description) VALUES
    ('Cours magistral', 'Cours théorique en amphithéâtre'),
    ('Travaux pratiques', 'Séances pratiques en laboratoire ou atelier'),
    ('Séminaire', 'Session interactive avec discussions'),
    ('Projet', 'Travail de groupe ou individuel sur un projet');

/* Insertion dans activity_type_translations */
INSERT INTO activity_type_translations (activity_type_id, language_id, translated_name, translated_description) VALUES
    -- Cours magistral
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Cours magistral'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Cours magistral', 'Cours théorique en amphithéâtre'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Cours magistral'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Hoorcollege', 'Theoretische les in een auditorium'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Cours magistral'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Vorlesung', 'Theoretischer Kurs im Hörsaal'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Cours magistral'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Lecture', 'Theoretical course in an auditorium'),
    -- Travaux pratiques
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Travaux pratiques'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Travaux pratiques', 'Séances pratiques en laboratoire ou atelier'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Travaux pratiques'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Practicum', 'Praktische sessies in laboratorium of werkplaats'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Travaux pratiques'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Praktikum', 'Praktische Sitzungen im Labor oder Werkstatt'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Travaux pratiques'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Practical Work', 'Practical sessions in a laboratory or workshop'),
    -- Séminaire
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Séminaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Séminaire', 'Session interactive avec discussions'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Séminaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Seminarie', 'Interactieve sessie met discussies'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Séminaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Seminar', 'Interaktive Sitzung mit Diskussionen'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Séminaire'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Seminar', 'Interactive session with discussions'),
    -- Projet
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Projet'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Projet', 'Travail de groupe ou individuel sur un projet'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Projet'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Project', 'Groeps- of individueel werk aan een project'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Projet'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Projekt', 'Gruppen- oder Einzelarbeit an einem Projekt'),
    ((SELECT activity_type_id FROM activity_type WHERE name = 'Projet'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Project', 'Group or individual work on a project');

/* Insertion dans prerequisite_type */
INSERT INTO prerequisite_type (name, description) VALUES
    ('Diplôme', 'Diplôme requis pour l’admission'),
    ('Cours', 'Cours préalable obligatoire'),
    ('Expérience', 'Expérience professionnelle exigée');

/* Insertion dans prerequisite_type_translations */
INSERT INTO prerequisite_type_translations (prerequisite_type_id, language_id, translated_name, translated_description) VALUES
    -- Diplôme
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Diplôme'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Diplôme', 'Diplôme requis pour l’admission'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Diplôme'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Diploma', 'Diploma vereist voor toelating'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Diplôme'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Diplom', 'Diplom erforderlich für die Zulassung'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Diplôme'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Diploma', 'Diploma required for admission'),
    -- Cours
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Cours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Cours', 'Cours préalable obligatoire'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Cours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Cursus', 'Verplichte voorafgaande cursus'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Cours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Kurs', 'Verpflichtender Vorkurs'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Cours'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Course', 'Mandatory prerequisite course'),
    -- Expérience
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Expérience'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Expérience', 'Expérience professionnelle exigée'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Expérience'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Ervaring', 'Vereiste professionele ervaring'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Expérience'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Erfahrung', 'Erforderliche Berufserfahrung'),
    ((SELECT prerequisite_type_id FROM prerequisite_type WHERE name = 'Expérience'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Experience', 'Required professional experience');

/* Insertion dans prerequisite_source */
INSERT INTO prerequisite_source (name, description) VALUES
    ('Institution', 'Prérequis défini par une institution'),
    ('Entreprise', 'Prérequis exigé par une entreprise'),
    ('Officiel', 'Prérequis réglementaire officiel');

/* Insertion dans prerequisite_source_translations */
INSERT INTO prerequisite_source_translations (prerequisite_source_id, language_id, translated_name, translated_description) VALUES
    -- Institution
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Institution'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Institution', 'Prérequis défini par une institution'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Institution'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Instelling', 'Voorwaarde vastgelegd door een instelling'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Institution'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Institution', 'Voraussetzung, die von einer Institution festgelegt ist'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Institution'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Institution', 'Prerequisite set by an institution'),
    -- Entreprise
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Entreprise'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Entreprise', 'Prérequis exigé par une entreprise'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Entreprise'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bedrijf', 'Voorwaarde vereist door een bedrijf'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Entreprise'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Unternehmen', 'Voraussetzung, die von einem Unternehmen verlangt wird'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Entreprise'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Company', 'Prerequisite required by a company'),
    -- Officiel
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Officiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Officiel', 'Prérequis réglementaire officiel'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Officiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Officieel', 'Officiële regelgevende voorwaarde'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Officiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Offiziell', 'Offizielle regulatorische Voraussetzung'),
    ((SELECT prerequisite_source_id FROM prerequisite_source WHERE name = 'Officiel'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Official', 'Official regulatory prerequisite');

/* Insertion dans tax */
INSERT INTO tax (name, rate, description) VALUES
    ('TVA 21%', 21.00, 'Taxe sur la valeur ajoutée standard en Belgique'),
    ('TVA 6%', 6.00, 'Taxe réduite pour certains services'),
    ('Exempt', 0.00, 'Exemption de taxe');

/* Insertion dans tax_translations */
INSERT INTO tax_translations (tax_id, language_id, translated_name, translated_description) VALUES
    -- TVA 21%
    ((SELECT tax_id FROM tax WHERE name = 'TVA 21%'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'TVA 21%', 'Taxe sur la valeur ajoutée standard en Belgique'),
    ((SELECT tax_id FROM tax WHERE name = 'TVA 21%'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'BTW 21%', 'Standaard belasting over de toegevoegde waarde in België'),
    ((SELECT tax_id FROM tax WHERE name = 'TVA 21%'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'MwSt 21%', 'Standard-Mehrwertsteuer in Belgien'),
    ((SELECT tax_id FROM tax WHERE name = 'TVA 21%'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'VAT 21%', 'Standard value-added tax in Belgium'),
    -- TVA 6%
    ((SELECT tax_id FROM tax WHERE name = 'TVA 6%'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'TVA 6%', 'Taxe réduite pour certains services'),
    ((SELECT tax_id FROM tax WHERE name = 'TVA 6%'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'BTW 6%', 'Verlaagde belasting voor bepaalde diensten'),
    ((SELECT tax_id FROM tax WHERE name = 'TVA 6%'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'MwSt 6%', 'Reduzierte Steuer für bestimmte Dienstleistungen'),
    ((SELECT tax_id FROM tax WHERE name = 'TVA 6%'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'VAT 6%', 'Reduced tax for certain services'),
    -- Exempt
    ((SELECT tax_id FROM tax WHERE name = 'Exempt'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Exempt', 'Exemption de taxe'),
    ((SELECT tax_id FROM tax WHERE name = 'Exempt'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Vrijgesteld', 'Vrijstelling van belasting'),
    ((SELECT tax_id FROM tax WHERE name = 'Exempt'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Befreit', 'Steuerbefreiung'),
    ((SELECT tax_id FROM tax WHERE name = 'Exempt'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Exempt', 'Tax exemption');

/* Insertion dans payment_status */
INSERT INTO payment_status (name, description) VALUES
    ('En attente', 'Paiement non encore effectué'),
    ('Confirmé', 'Paiement validé'),
    ('Échoué', 'Paiement non réussi'),
    ('Remboursé', 'Paiement remboursé');

/* Insertion dans payment_status_translations */
INSERT INTO payment_status_translations (payment_status_id, language_id, translated_name, translated_description) VALUES
    -- En attente
    ((SELECT payment_status_id FROM payment_status WHERE name = 'En attente'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'En attente', 'Paiement non encore effectué'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'En attente'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'In afwachting', 'Betaling nog niet uitgevoerd'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'En attente'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Ausstehend', 'Zahlung noch nicht durchgeführt'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'En attente'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Pending', 'Payment not yet completed'),
    -- Confirmé
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Confirmé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Confirmé', 'Paiement validé'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Confirmé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bevestigd', 'Betaling gevalideerd'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Confirmé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Bestätigt', 'Zahlung bestätigt'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Confirmé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Confirmed', 'Payment validated'),
    -- Échoué
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Échoué'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Échoué', 'Paiement non réussi'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Échoué'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Mislukt', 'Betaling niet gelukt'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Échoué'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Fehlgeschlagen', 'Zahlung nicht erfolgreich'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Échoué'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Failed', 'Payment failed'),
    -- Remboursé
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Remboursé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Remboursé', 'Paiement remboursé'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Remboursé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Terugbetaald', 'Betaling terugbetaald'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Remboursé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Erstattet', 'Zahlung erstattet'),
    ((SELECT payment_status_id FROM payment_status WHERE name = 'Remboursé'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Refunded', 'Payment refunded');

/* Insertion dans degree_type */
INSERT INTO degree_type (name, description, cycle_id) VALUES
    ('Bachelier', 'Diplôme de premier cycle', (SELECT cycle_id FROM cycle WHERE name = 'Premier cycle')),
    ('Master', 'Diplôme de deuxième cycle', (SELECT cycle_id FROM cycle WHERE name = 'Deuxième cycle')),
    ('Doctorat', 'Diplôme de troisième cycle', (SELECT cycle_id FROM cycle WHERE name = 'Troisième cycle')),
    ('Certificat', 'Certificat de formation spécifique', NULL);

/* Insertion dans degree_type_translations */
INSERT INTO degree_type_translations (degree_type_id, language_id, translated_name, translated_description) VALUES
    -- Bachelier
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Bachelier'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Bachelier', 'Diplôme de premier cycle'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Bachelier'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bachelor', 'Diploma van de eerste cyclus'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Bachelier'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Bachelor', 'Diplom des ersten Zyklus'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Bachelier'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Bachelor', 'First cycle diploma'),
    -- Master
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Master'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Master', 'Diplôme de deuxième cycle'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Master'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Master', 'Diploma van de tweede cyclus'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Master'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Master', 'Diplom des zweiten Zyklus'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Master'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Master', 'Second cycle diploma'),
    -- Doctorat
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Doctorat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Doctorat', 'Diplôme de troisième cycle'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Doctorat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Doctoraat', 'Diploma van de derde cyclus'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Doctorat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Doktorat', 'Diplom des dritten Zyklus'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Doctorat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Doctorate', 'Third cycle diploma'),
    -- Certificat
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Certificat', 'Certificat de formation spécifique'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Certificaat', 'Certificaat van specifieke opleiding'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Zertifikat', 'Zertifikat für spezifische Ausbildung'),
    ((SELECT degree_type_id FROM degree_type WHERE name = 'Certificat'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Certificate', 'Certificate of specific training');


/*************************************************************************************************
    4. Insertions des données initiales (adaptées pour une nouvelle DB)
**************************************************************************************************/

/* Insertion dans super_role */
INSERT INTO super_role (name, description, created_at, updated_at)
VALUES 
    ('SuperAdmin', 'Rôle global pour les administrateurs système avec tous les privilèges', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Others', 'Rôle global pour les autres utilisateurs système', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

/* Insertion dans entity_types */
INSERT INTO entity_types (name, description, created_at, updated_at) VALUES 
    ('Institution', 'Établissements éducatifs belges', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Company', 'Entreprises enregistrées en Belgique', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Public', 'Entités publiques ou accessibles à tous', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
	
/* Insertion dans entity_types_translations */
INSERT INTO entity_type_translations (entity_type_id, language_id, translated_name, translated_description) VALUES
    -- Institution
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Institution'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Institution', 'Établissements éducatifs belges'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Institution'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Instelling', 'Belgische onderwijsinstellingen'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Institution'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Institution', 'Belgische Bildungseinrichtungen'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Institution'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Institution', 'Belgian educational institutions'),
    -- Company
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Company'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Entreprise', 'Entreprises enregistrées en Belgique'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Company'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Bedrijf', 'In België geregistreerde bedrijven'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Company'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Unternehmen', 'In Belgien registrierte Unternehmen'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Company'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Company', 'Companies registered in Belgium'),
    -- Public
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Public'), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Public', 'Entités publiques ou accessibles à tous'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Public'), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Openbaar', 'Openbare entiteiten of toegankelijk voor iedereen'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Public'), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Öffentlich', 'Öffentliche Einrichtungen oder für alle zugänglich'),
    ((SELECT entity_type_id FROM entity_types WHERE name = 'Public'), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Public', 'Public entities or accessible to all');

/* Insertion dans role */
INSERT INTO role (name, entity_type_id, description, created_at, updated_at) VALUES
    ('Admin', (SELECT entity_type_id FROM entity_types WHERE name = 'Institution'), 'Administrateur d’une institution', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Admin', (SELECT entity_type_id FROM entity_types WHERE name = 'Company'), 'Administrateur d’une entreprise', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Recruiter', (SELECT entity_type_id FROM entity_types WHERE name = 'Company'), 'Recruteur dans une entreprise', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Viewer', (SELECT entity_type_id FROM entity_types WHERE name = 'Company'), 'Utilisateur en lecture seule', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Editor', (SELECT entity_type_id FROM entity_types WHERE name = 'Institution'), 'Éditeur de contenus pédagogiques', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Viewer', (SELECT entity_type_id FROM entity_types WHERE name = 'Institution'), 'Utilisateur en lecture seule', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('Student', (SELECT entity_type_id FROM entity_types WHERE name = 'Public'), 'Étudiant avec accès public', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    ('PrivateLandlord', (SELECT entity_type_id FROM entity_types WHERE name = 'Public'), 'Bailleur individuel public', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


/* Insertion dans invitation_type */
INSERT INTO invitation_type (name, description) VALUES
    ('EntityAdmin', 'Invitation pour administrer une nouvelle entité créée par un superadmin'),
    ('Colleague', 'Invitation pour rejoindre une entité existante en tant que collègue');

INSERT INTO token_type (
    name, 
    description, 
    default_token_expiration_minutes, 
    min_delay_minutes, 
    max_requests_per_window, 
    is_rate_limited, 
    rate_limit_window_minutes, 
    token_required, 
    code_otp_required, 
    max_otp_attempts, 
    default_otp_expiration_minutes
) VALUES
    ('EMAIL_CONFIRMATION', 'Token utilisé pour confirmer l’adresse email des utilisateurs', 1440, 0, 0, 0, 60, 1, 0, 5, 120),
    ('PASSWORD_RESET', 'Token utilisé pour réinitialiser le mot de passe des utilisateurs', 60, 5, 1, 1, 60, 1, 0, 5, 120);

/* Insertion d’un superadmin dans users */
INSERT INTO users (email, password_hash, first_name, last_name, super_role_id, is_verified, last_login_at, created_at, updated_at, emailConfirmationToken, created_by, updated_by) VALUES
    ('user@example.com', '$2b$12$87U/DnX6jE/KCYPSleGaPuqCQtrquO7mBJ77/LSFU2yoJ2i4qEe.6', 'Super', 'Admin', 
     (SELECT super_role_id FROM super_role WHERE name = 'SuperAdmin'), TRUE, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, NULL, NULL, NULL);


/* Insertion dans role_translations */
INSERT INTO role_translations (role_id, language_id, translated_name, translated_description) VALUES
    -- Admin pour Institution
    ((SELECT role_id FROM role WHERE name = 'Admin' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Administrateur', 'Administrateur d’une institution'),
    ((SELECT role_id FROM role WHERE name = 'Admin' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Beheerder', 'Beheerder van een instelling'),
    ((SELECT role_id FROM role WHERE name = 'Admin' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Administrator', 'Administrator einer Institution'),
    ((SELECT role_id FROM role WHERE name = 'Admin' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Admin', 'Administrator of an institution'),
    -- Admin pour Company
    ((SELECT role_id FROM role WHERE name = 'Admin' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Administrateur', 'Administrateur de l’entreprise avec tous les privilèges'),
    ((SELECT role_id FROM role WHERE name = 'Admin' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Beheerder', 'Bedrijfsbeheerder met alle privileges'),
    ((SELECT role_id FROM role WHERE name = 'Admin' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Administrator', 'Unternehmensadministrator mit allen Berechtigungen'),
    ((SELECT role_id FROM role WHERE name = 'Admin' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Admin', 'Company administrator with all privileges'),
    -- Recruiter pour Company
    ((SELECT role_id FROM role WHERE name = 'Recruiter' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Recruteur', 'Recruteur gérant les offres d’emploi et les candidatures'),
    ((SELECT role_id FROM role WHERE name = 'Recruiter' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Rekruteerder', 'Rekruteerder die vacatures en sollicitaties beheert'),
    ((SELECT role_id FROM role WHERE name = 'Recruiter' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Rekrutierer', 'Rekrutierer, der Stellenangebote und Bewerbungen verwaltet'),
    ((SELECT role_id FROM role WHERE name = 'Recruiter' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Recruiter', 'Recruiter managing job offers and applications'),
    -- Viewer pour Company
    ((SELECT role_id FROM role WHERE name = 'Viewer' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Visionneur', 'Utilisateur en lecture seule'),
    ((SELECT role_id FROM role WHERE name = 'Viewer' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Kijker', 'Gebruiker met alleen-lezen toegang'),
    ((SELECT role_id FROM role WHERE name = 'Viewer' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Betrachter', 'Benutzer mit Lesezugriff'),
    ((SELECT role_id FROM role WHERE name = 'Viewer' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Company') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Viewer', 'Read-only user'),
    -- Editor pour Institution
    ((SELECT role_id FROM role WHERE name = 'Editor' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Éditeur', 'Éditeur de contenus pédagogiques'),
    ((SELECT role_id FROM role WHERE name = 'Editor' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Redacteur', 'Redacteur van pedagogische inhoud'),
    ((SELECT role_id FROM role WHERE name = 'Editor' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Editor', 'Editor von pädagogischen Inhalten'),
    ((SELECT role_id FROM role WHERE name = 'Editor' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Editor', 'Editor of educational content'),
    -- Viewer pour Institution
    ((SELECT role_id FROM role WHERE name = 'Viewer' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'fr'), 'Visionneur', 'Utilisateur en lecture seule'),
    ((SELECT role_id FROM role WHERE name = 'Viewer' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'nl'), 'Kijker', 'Gebruiker met alleen-lezen toegang'),
    ((SELECT role_id FROM role WHERE name = 'Viewer' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'de'), 'Betrachter', 'Benutzer mit Lesezugriff'),
    ((SELECT role_id FROM role WHERE name = 'Viewer' AND entity_type_id = (SELECT entity_type_id FROM entity_types WHERE name = 'Institution') LIMIT 1), 
     (SELECT language_id FROM spoken_language WHERE code = 'en'), 'Viewer', 'Read-only user');


	
	
	
-- Types de documents légaux avec descriptions en français
INSERT INTO legal_document_type (name, description) VALUES 
('TermsOfService', 'Conditions générales d’utilisation du site'),
('PrivacyPolicy', 'Politique de protection des données personnelles'),
('LegalNotice', 'Informations légales sur l’éditeur du site');

-- ✅ TermsOfService
INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService'),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Conditions Générales d\'Utilisation',
 'En cochant cette case, vous acceptez nos conditions générales d’utilisation.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService'),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Terms of Service',
 'By checking this box, you agree to our terms of service.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService'),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Gebruiksvoorwaarden',
 'Door dit vakje aan te vinken, gaat u akkoord met onze gebruiksvoorwaarden.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService'),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Nutzungsbedingungen',
 'Durch Ankreuzen dieses Feldes akzeptieren Sie unsere Nutzungsbedingungen.');
 
 -- ✅ PrivacyPolicy
INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy'),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Politique de Confidentialité',
 'En cochant cette case, vous consentez à notre politique de confidentialité concernant la gestion de vos données.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy'),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Privacy Policy',
 'By checking this box, you consent to our privacy policy regarding your data.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy'),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Privacybeleid',
 'Door dit vakje aan te vinken, stemt u in met ons privacybeleid met betrekking tot uw gegevens.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy'),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Datenschutzrichtlinie',
 'Durch Ankreuzen dieses Feldes stimmen Sie unserer Datenschutzrichtlinie zu.');
 
 -- ✅ LegalNotice
INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice'),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Mentions Légales',
 'En cochant cette case, vous reconnaissez avoir pris connaissance de nos mentions légales.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice'),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Legal Notice',
 'By checking this box, you acknowledge having read our legal notice.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice'),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Juridische kennisgeving',
 'Door dit vakje aan te vinken, erkent u dat u kennis heeft genomen van onze juridische kennisgeving.');

INSERT INTO legal_document_type_translation (document_type_id, language_id, name, description) VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice'),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Impressum',
 'Durch Ankreuzen dieses Feldes erkennen Sie an, dass Sie unser Impressum gelesen haben.');


-- Versions des documents
INSERT INTO legal_document (document_type_id, version, published_at, is_active)
VALUES
((SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService'), '1.0.0', '2025-04-17 00:00:00', TRUE),
((SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy'), '1.0.0', '2025-04-17 00:00:00', TRUE),
((SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice'), '1.0.0', '2025-04-17 00:00:00', TRUE);

-- Clause 1 : Acceptation des conditions
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  1
);

-- Traductions clause 1
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Acceptation des conditions',
 'En utilisant ce site, vous acceptez l’ensemble des présentes conditions générales d’utilisation.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Acceptance of Terms',
 'By using this website, you agree to all of these terms of use.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Aanvaarding van voorwaarden',
 'Door deze website te gebruiken, gaat u akkoord met alle gebruiksvoorwaarden.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Akzeptanz der Bedingungen',
 'Durch die Nutzung dieser Website akzeptieren Sie alle Nutzungsbedingungen.');

-- Clause 2 : Utilisation du service
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  2
);

-- Traductions clause 2
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Utilisation du service',
 'Le site doit être utilisé de manière légale, responsable et respectueuse.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Use of the Service',
 'The website must be used in a legal, responsible, and respectful manner.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Gebruik van de dienst',
 'De website moet op een legale, verantwoorde en respectvolle manier worden gebruikt.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Nutzung des Dienstes',
 'Die Website muss auf legale, verantwortungsvolle und respektvolle Weise genutzt werden.');


-- Clause 3: Propriété intellectuelle
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  3
);

-- Traductions clause 3
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Propriété intellectuelle',
 'Tout contenu publié sur ce site, incluant textes, images et logos, est protégé par les droits d’auteur et ne peut être utilisé sans autorisation.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Intellectual Property',
 'All content published on this website, including texts, images, and logos, is protected by copyright and may not be used without permission.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Intellectueel eigendom',
 'Alle inhoud die op deze website wordt gepubliceerd, inclusief teksten, afbeeldingen en logo’s, is beschermd door auteursrecht en mag niet zonder toestemming worden gebruikt.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Geistiges Eigentum',
 'Alle auf dieser Website veröffentlichten Inhalte, einschließlich Texte, Bilder und Logos, sind urheberrechtlich geschützt und dürfen ohne Genehmigung nicht verwendet werden.');

-- Clause 4: Responsabilité des utilisateurs
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  4
);

-- Traductions clause 4
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Responsabilité des utilisateurs',
 'Vous êtes responsable de tout contenu que vous publiez et de toute activité effectuée via votre compte.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'User Responsibilities',
 'You are responsible for any content you post and any activity conducted through your account.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Verantwoordelijkheid van gebruikers',
 'U bent verantwoordelijk voor alle inhoud die u plaatst en voor alle activiteiten die via uw account worden uitgevoerd.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Verantwortung der Nutzer',
 'Sie sind für alle Inhalte, die Sie veröffentlichen, und für alle Aktivitäten, die über Ihr Konto durchgeführt werden, verantwortlich.');

-- Clause 5: Résiliation du service
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  5
);

-- Traductions clause 5
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Résiliation du service',
 'Nous nous réservons le droit de suspendre ou de résilier votre accès au site en cas de violation des présentes conditions.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Termination of Service',
 'We reserve the right to suspend or terminate your access to the website in case of violation of these terms.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Beëindiging van de dienst',
 'Wij behouden ons het recht voor om uw toegang tot de website te schorsen of te beëindigen bij schending van deze voorwaarden.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Beendigung des Dienstes',
 'Wir behalten uns das Recht vor, Ihren Zugang zur Website bei Verstoß gegen diese Bedingungen zu sperren oder zu beenden.');

-- Clause 6: Limitation de responsabilité
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  6
);

-- Traductions clause 6
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Limitation de responsabilité',
 'Nous ne sommes pas responsables des dommages directs ou indirects découlant de l’utilisation de ce site.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Limitation of Liability',
 'We are not liable for any direct or indirect damages arising from the use of this website.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Beperking van aansprakelijkheid',
 'Wij zijn niet aansprakelijk voor directe of indirecte schade die voortvloeit uit het gebruik van deze website.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Haftungsbeschränkung',
 'Wir haften nicht für direkte oder indirekte Schäden, die aus der Nutzung dieser Website entstehen.');

-- Clause 7: Protection des données personnelles
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  7
);

-- Traductions clause 7
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Protection des données personnelles',
 'Vos données personnelles sont collectées et traitées conformément à notre politique de confidentialité.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Protection of Personal Data',
 'Your personal data is collected and processed in accordance with our privacy policy.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Bescherming van persoonsgegevens',
 'Uw persoonsgegevens worden verzameld en verwerkt in overeenstemming met ons privacybeleid.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Schutz personenbezogener Daten',
 'Ihre personenbezogenen Daten werden gemäß unserer Datenschutzrichtlinie erfasst und verarbeitet.');

-- Clause 8: Modifications des conditions
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  8
);

-- Traductions clause 8
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Modifications des conditions',
 'Nous pouvons modifier ces conditions à tout moment. Les modifications seront publiées sur ce site.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Changes to the Terms',
 'We may modify these terms at any time. Changes will be posted on this website.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Wijzigingen in de voorwaarden',
 'Wij kunnen deze voorwaarden op elk moment wijzigen. Wijzigingen worden op deze website gepubliceerd.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Änderungen der Bedingungen',
 'Wir können diese Bedingungen jederzeit ändern. Änderungen werden auf dieser Website veröffentlicht.');

-- Clause 9: Résolution des litiges
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  9
);

-- Traductions clause 9
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Résolution des litiges',
 'Tout litige relatif à ces conditions sera soumis à la juridiction exclusive des tribunaux compétents.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Dispute Resolution',
 'Any dispute related to these terms will be subject to the exclusive jurisdiction of the competent courts.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Geschillenbeslechting',
 'Elk geschil met betrekking tot deze voorwaarden valt onder de exclusieve jurisdictie van de bevoegde rechtbanken.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Streitbeilegung',
 'Jeder Streit im Zusammenhang mit diesen Bedingungen unterliegt der ausschließlichen Zuständigkeit der zuständigen Gerichte.');

-- Clause 10: Force majeure
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'TermsOfService')),
  10
);

-- Traductions clause 10
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Force majeure',
 'Nous ne sommes pas responsables des retards ou manquements dus à des événements indépendants de notre volonté.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Force Majeure',
 'We are not responsible for delays or failures due to events beyond our control.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Overmacht',
 'Wij zijn niet verantwoordelijk voor vertragingen of tekortkomingen als gevolg van gebeurtenissen buiten onze controle.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Höhere Gewalt',
 'Wir sind nicht verantwortlich für Verzögerungen oder Ausfälle aufgrund von Ereignissen, die außerhalb unserer Kontrolle liegen.');
 
 -- Clause 1: Collecte des données
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  1
);

-- Traductions clause 1
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Collecte des données',
 'Nous collectons des données personnelles telles que votre nom, adresse e-mail et informations de navigation lorsque vous utilisez notre site.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Data Collection',
 'We collect personal data such as your name, email address, and browsing information when you use our website.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Gegevensverzameling',
 'Wij verzamelen persoonlijke gegevens zoals uw naam, e-mailadres en surfgedrag wanneer u onze website gebruikt.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Datenerhebung',
 'Wir erheben personenbezogene Daten wie Ihren Namen, Ihre E-Mail-Adresse und Browsing-Informationen, wenn Sie unsere Website nutzen.');

-- Clause 2: Utilisation des données
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  2
);

-- Traductions clause 2
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Utilisation des données',
 'Vos données sont utilisées pour fournir, améliorer nos services et communiquer avec vous.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Use of Data',
 'Your data is used to provide, improve our services, and communicate with you.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Gebruik van gegevens',
 'Uw gegevens worden gebruikt om onze diensten te leveren, te verbeteren en met u te communiceren.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Verwendung der Daten',
 'Ihre Daten werden verwendet, um unsere Dienste bereitzustellen, zu verbessern und mit Ihnen zu kommunizieren.');

-- Clause 3: Partage des données
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  3
);

-- Traductions clause 3
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Partage des données',
 'Nous ne partageons vos données qu’avec des partenaires de confiance ou si la loi l’exige.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Data Sharing',
 'We only share your data with trusted partners or when required by law.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Delen van gegevens',
 'Wij delen uw gegevens alleen met vertrouwde partners of wanneer dit wettelijk vereist is.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Datenweitergabe',
 'Wir geben Ihre Daten nur an vertrauenswürdige Partner oder bei gesetzlicher Verpflichtung weiter.');

-- Clause 4: Droits des utilisateurs
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  4
);

-- Traductions clause 4
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Droits des utilisateurs',
 'Vous avez le droit d’accéder, de corriger ou de supprimer vos données personnelles.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'User Rights',
 'You have the right to access, correct, or delete your personal data.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Rechten van gebruikers',
 'U heeft het recht om uw persoonlijke gegevens in te zien, te corrigeren of te verwijderen.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Nutzerrechte',
 'Sie haben das Recht, auf Ihre personenbezogenen Daten zuzugreifen, sie zu korrigieren oder zu löschen.');

-- Clause 5: Sécurité des données
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  5
);

-- Traductions clause 5
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Sécurité des données',
 'Nous mettons en place des mesures techniques et organisationnelles pour protéger vos données.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Data Security',
 'We implement technical and organizational measures to protect your data.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Gegevensbeveiliging',
 'Wij nemen technische en organisatorische maatregelen om uw gegevens te beschermen.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Datensicherheit',
 'Wir setzen technische und organisatorische Maßnahmen ein, um Ihre Daten zu schützen.');

-- Clause 6: Cookies et technologies similaires
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  6
);

-- Traductions clause 6
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Cookies et technologies similaires',
 'Nous utilisons des cookies pour améliorer votre expérience utilisateur. Vous pouvez les désactiver dans vos paramètres.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Cookies and Similar Technologies',
 'We use cookies to enhance your user experience. You can disable them in your settings.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Cookies en vergelijkbare technologieën',
 'Wij gebruiken cookies om uw gebruikerservaring te verbeteren. U kunt deze uitschakelen in uw instellingen.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Cookies und ähnliche Technologien',
 'Wir verwenden Cookies, um Ihre Nutzererfahrung zu verbessern. Sie können diese in Ihren Einstellungen deaktivieren.');

-- Clause 7: Transferts internationaux de données
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  7
);

-- Traductions clause 7
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Transferts internationaux de données',
 'Vos données peuvent être transférées vers des pays hors de l’UE, avec des garanties appropriées.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'International Data Transfers',
 'Your data may be transferred to countries outside the EU, with appropriate safeguards.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Internationale gegevensoverdracht',
 'Uw gegevens kunnen worden overgedragen naar landen buiten de EU, met passende waarborgen.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Internationale Datenübermittlung',
 'Ihre Daten können in Länder außerhalb der EU übermittelt werden, mit angemessenen Schutzmaßnahmen.');

-- Clause 8: Modifications de la politique
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  8
);

-- Traductions clause 8
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Modifications de la politique',
 'Nous pouvons mettre à jour cette politique. Les modifications seront publiées sur notre site.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Changes to the Policy',
 'We may update this policy. Changes will be posted on our website.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Wijzigingen in het beleid',
 'Wij kunnen dit beleid bijwerken. Wijzigingen worden op onze website gepubliceerd.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Änderungen der Richtlinie',
 'Wir können diese Richtlinie aktualisieren. Änderungen werden auf unserer Website veröffentlicht.');

-- Clause 9: Contact
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'PrivacyPolicy')),
  9
);

-- Traductions clause 9
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Contact',
 'Pour toute question concernant cette politique, contactez-nous via notre formulaire de contact.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Contact',
 'For any questions about this policy, contact us via our contact form.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Contact',
 'Voor vragen over dit beleid kunt u contact met ons opnemen via ons contactformulier.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Kontakt',
 'Bei Fragen zu dieser Richtlinie kontaktieren Sie uns über unser Kontaktformular.');
 
 -- Clause 1: Identification de l'éditeur
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice')),
  1
);

-- Traductions clause 1
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Identification de l’éditeur',
 'Ce site est édité par Ali Jallouli, personne physique, domiciliée à Avenue de Jemappes 51, 7000 Mons, Belgique.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Publisher Identification',
 'This website is published by Ali Jallouli, a natural person, residing at Avenue de Jemappes 51, 7000 Mons, Belgium.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Identificatie van de uitgever',
 'Deze website wordt uitgegeven door Ali Jallouli, een natuurlijke persoon, woonachtig te Avenue de Jemappes 51, 7000 Mons, België.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Identifikation des Herausgebers',
 'Diese Website wird herausgegeben von Ali Jallouli, einer natürlichen Person, wohnhaft in Avenue de Jemappes 51, 7000 Mons, Belgien.');

-- Clause 2: Contact
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice')),
  2
);

-- Traductions clause 2
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Contact',
 'Pour nous contacter : contact@doorz.be (général) ou support@doorz.be (support technique).'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Contact',
 'To contact us: contact@doorz.be (general) or support@doorz.be (technical support).'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Contact',
 'Om contact met ons op te nemen: contact@doorz.be (algemeen) of support@doorz.be (technische ondersteuning).'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Kontakt',
 'Um uns zu kontaktieren: contact@doorz.be (allgemein) oder support@doorz.be (technischer Support).');

-- Clause 3: Représentation légale
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice')),
  3
);

-- Traductions clause 3
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Représentation légale',
 'La personne responsable de la publication est Ali Jallouli.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Legal Representation',
 'The person responsible for publication is Ali Jallouli.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Wettelijke vertegenwoordiging',
 'De persoon verantwoordelijk voor de publicatie is Ali Jallouli.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Gesetzliche Vertretung',
 'Die für die Veröffentlichung verantwortliche Person ist Ali Jallouli.');

-- Clause 4: Hébergeur
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice')),
  4
);

-- Traductions clause 4
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Hébergeur',
 'Le site est hébergé par OVH, 2 rue Kellermann, 59100 Roubaix, France, téléphone : +33 9 72 10 10 07.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Hosting Provider',
 'The website is hosted by OVH, 2 rue Kellermann, 59100 Roubaix, France, phone: +33 9 72 10 10 07.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Hostingprovider',
 'De website wordt gehost door OVH, 2 rue Kellermann, 59100 Roubaix, Frankrijk, telefoon: +33 9 72 10 10 07.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Hosting-Anbieter',
 'Die Website wird gehostet von OVH, 2 rue Kellermann, 59100 Roubaix, Frankreich, Telefon: +33 9 72 10 10 07.');

-- Clause 5: Responsabilité
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice')),
  5
);

-- Traductions clause 5
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Responsabilité',
 'L’éditeur n’est pas responsable du contenu des sites liés ou des erreurs présentes sur ce site.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Liability',
 'The publisher is not responsible for the content of linked websites or errors on this website.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Aansprakelijkheid',
 'De uitgever is niet verantwoordelijk voor de inhoud van gelinkte websites of fouten op deze website.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Haftung',
 'Der Herausgeber ist nicht verantwortlich für die Inhalte verlinkter Websites oder Fehler auf dieser Website.');

-- Clause 6: Propriété intellectuelle
INSERT INTO legal_document_clause (document_id, order_index)
VALUES (
  (SELECT document_id FROM legal_document WHERE version = '1.0.0' AND document_type_id = (SELECT document_type_id FROM legal_document_type WHERE name = 'LegalNotice')),
  6
);

-- Traductions clause 6
INSERT INTO legal_document_clause_translation (clause_id, language_id, title, content)
VALUES
-- FR
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'fr'),
 'Propriété intellectuelle',
 'Tous les contenus de ce site sont protégés par le droit d’auteur et ne peuvent être reproduits sans autorisation.'),
-- EN
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'en'),
 'Intellectual Property',
 'All content on this website is protected by copyright and may not be reproduced without permission.'),
-- NL
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'nl'),
 'Intellectueel eigendom',
 'Alle inhoud op deze website is beschermd door auteursrecht en mag niet worden gereproduceerd zonder toestemming.'),
-- DE
((SELECT clause_id FROM legal_document_clause ORDER BY clause_id DESC LIMIT 1),
 (SELECT language_id FROM spoken_language WHERE code = 'de'),
 'Geistiges Eigentum',
 'Alle Inhalte dieser Website sind urheberrechtlich geschützt und dürfen ohne Genehmigung nicht vervielfältigt werden.');
 
 
 
 
 
 
 
 
 
 
 
 INSERT INTO contact_message_type (priority, is_active, key_name)
VALUES 
(3, TRUE, 'technical_issue'),         -- Problème technique urgent
(2, TRUE, 'billing_request'),         -- Demande de facturation ou paiement
(2, TRUE, 'commercial_request'),      -- Partenariat ou offre commerciale
(1, TRUE, 'suggestion'),              -- Suggestion ou idée
(1, TRUE, 'general_question'),        -- Question générale
(1, TRUE, 'other');                   -- Autre type
 
 
 
 
 
 -- Traductions sécurisées via key_name + code langue
INSERT INTO contact_message_type_translation (contact_message_type_id, language_id, name, description)
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'technical_issue'),
    language_id, 'Problème technique', 'Signaler un bug ou un dysfonctionnement technique.'
FROM spoken_language WHERE code = 'fr'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'technical_issue'),
    language_id, 'Technical Issue', 'Report a bug or technical problem.'
FROM spoken_language WHERE code = 'en'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'technical_issue'),
    language_id, 'Technisches Problem', 'Ein technisches Problem melden.'
FROM spoken_language WHERE code = 'de'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'technical_issue'),
    language_id, 'Technisch probleem', 'Meld een technisch probleem.'
FROM spoken_language WHERE code = 'nl'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'billing_request'),
    language_id, 'Demande de facturation', 'Questions ou demandes liées à la facturation ou au paiement.'
FROM spoken_language WHERE code = 'fr'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'billing_request'),
    language_id, 'Billing Request', 'Questions or requests related to billing or payment.'
FROM spoken_language WHERE code = 'en'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'billing_request'),
    language_id, 'Rechnungsanfrage', 'Fragen oder Anforderungen im Zusammenhang mit Rechnungen oder Zahlungen.'
FROM spoken_language WHERE code = 'de'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'billing_request'),
    language_id, 'Facturatieverzoek', 'Vragen of verzoeken met betrekking tot facturatie of betaling.'
FROM spoken_language WHERE code = 'nl'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'commercial_request'),
    language_id, 'Demande commerciale', 'Proposition de partenariat ou offre commerciale.'
FROM spoken_language WHERE code = 'fr'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'commercial_request'),
    language_id, 'Commercial Request', 'Proposal for partnership or commercial offer.'
FROM spoken_language WHERE code = 'en'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'commercial_request'),
    language_id, 'Kommerzielle Anfrage', 'Vorschlag für eine Partnerschaft oder ein kommerzielles Angebot.'
FROM spoken_language WHERE code = 'de'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'commercial_request'),
    language_id, 'Commercieel verzoek', 'Voorstel voor partnerschap of commercieel aanbod.'
FROM spoken_language WHERE code = 'nl'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'suggestion'),
    language_id, 'Suggestion', 'Proposer une idée ou une amélioration.'
FROM spoken_language WHERE code = 'fr'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'suggestion'),
    language_id, 'Suggestion', 'Suggest an idea or improvement.'
FROM spoken_language WHERE code = 'en'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'suggestion'),
    language_id, 'Vorschlag', 'Eine Idee oder Verbesserung vorschlagen.'
FROM spoken_language WHERE code = 'de'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'suggestion'),
    language_id, 'Suggestie', 'Een idee of verbetering voorstellen.'
FROM spoken_language WHERE code = 'nl'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'general_question'),
    language_id, 'Question générale', 'Poser une question générale sur nos services.'
FROM spoken_language WHERE code = 'fr'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'general_question'),
    language_id, 'General Question', 'Ask a general question about our services.'
FROM spoken_language WHERE code = 'en'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'general_question'),
    language_id, 'Allgemeine Frage', 'Eine allgemeine Frage zu unseren Dienstleistungen stellen.'
FROM spoken_language WHERE code = 'de'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'general_question'),
    language_id, 'Algemene vraag', 'Een algemene vraag stellen over onze diensten.'
FROM spoken_language WHERE code = 'nl'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'other'),
    language_id, 'Autre', 'Tout autre type de demande ou commentaire.'
FROM spoken_language WHERE code = 'fr'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'other'),
    language_id, 'Other', 'Any other type of request or comment.'
FROM spoken_language WHERE code = 'en'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'other'),
    language_id, 'Sonstiges', 'Jede andere Art von Anfrage oder Kommentar.'
FROM spoken_language WHERE code = 'de'
UNION ALL
SELECT
    (SELECT contact_message_type_id FROM contact_message_type WHERE key_name = 'other'),
    language_id, 'Overig', 'Elk ander type verzoek of opmerking.'
FROM spoken_language WHERE code = 'nl';