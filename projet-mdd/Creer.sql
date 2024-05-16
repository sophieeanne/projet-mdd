CREATE DATABASE IF NOT EXISTS VeloMax;
USE VeloMax;

CREATE TABLE IF NOT EXISTS fournisseur (
    siret INT, 
    contact VARCHAR(255), 
    adresse VARCHAR(255), 
    libellé INT, 
    PRIMARY KEY (siret)
);

CREATE TABLE IF NOT EXISTS boutique (
    nom_compagnie VARCHAR(255), 
    adresse VARCHAR(255), 
    telephone VARCHAR(255), 
    courriel VARCHAR(255),
    nom_b VARCHAR(255),
    chiffre INT,
    PRIMARY KEY (nom_compagnie)
);

CREATE TABLE IF NOT EXISTS client (
    nclient INT, 
    nom VARCHAR(255), 
    prenom VARCHAR(255), 
    adresse VARCHAR(255), 
    telephone VARCHAR(255), 
    courriel VARCHAR(255), 
    fidelio INT,
    expiration_fidélio DATE,
    PRIMARY KEY (nclient),
    CONSTRAINT fidelio_check CHECK (fidelio IN (1, 2, 3, 4) OR fidelio IS NULL)
);

CREATE TABLE IF NOT EXISTS fidélio (
    nprogramme INT, 
    description VARCHAR(255), 
    cout INT, 
    durée VARCHAR(255), 
    rabais INT, 
    PRIMARY KEY(nprogramme)
);

CREATE TABLE IF NOT EXISTS modele(
    nprod INT, 
    nom VARCHAR(255),
    grandeur VARCHAR(255), 
    prix_u INT, 
    ligne_produit VARCHAR(255), 
    date_intro DATE, 
    date_disco DATE, 
    stock INT,
    PRIMARY KEY (nprod)
);

CREATE TABLE IF NOT EXISTS pièce (
    nprod_p INT, 
    desc_p VARCHAR(255), 
    nom_f VARCHAR(255), 
    nprod_f INT, 
    prix_u_p INT, 
    date_intro DATE,
    date_disco DATE,
    delai INT,
    quantité INT, 
    PRIMARY KEY (nprod_p),
    FOREIGN KEY (nprod_f) REFERENCES fournisseur(siret)
);

CREATE TABLE IF NOT EXISTS vendeur(
    nvendeur INT PRIMARY KEY,
    nomv VARCHAR(255),
    prenomv varchar(255),
    numerov varchar(255),
    emailv varchar(255),
    boutique varchar(255),
    ventes INT,
    satisfaction_client INT,
    CONSTRAINT s_check CHECK (satisfaction_client IN (1,2,3,4,5))
);

CREATE TABLE IF NOT EXISTS assemblage(
    nprod INT,
    nom VARCHAR(255),
    grandeur VARCHAR(255),
    cadre VARCHAR(255),
    guidon VARCHAR(255),
    freins VARCHAR(255),
    selle VARCHAR(255),
    dérailleur_avant VARCHAR(255),
    dérailleur_arrière VARCHAR(255),
    roue_avant VARCHAR(255),
    roue_arrière VARCHAR(255),
    reflecteurs VARCHAR(255),
    pedalier VARCHAR(255),
    ordinateur VARCHAR(255),
    panier VARCHAR(255),
    FOREIGN KEY (nprod) REFERENCES modele(nprod)
);

CREATE TABLE IF NOT EXISTS commande(
    ncommande INT PRIMARY KEY,
    nprod INT,
    nprod_p INT,
    nclient INT,
    date_commande DATE,
    adresse_livraison VARCHAR(255),
    date_livraison DATE,
    quantite INT,
    quantite_p INT,
    nom_magasin VARCHAR(255),
    nvendeur INT,
    FOREIGN KEY (nprod) REFERENCES modele(nprod),
    FOREIGN KEY (nclient) REFERENCES client(nclient),
    FOREIGN KEY (nprod_p) REFERENCES pièce(nprod_p),
    FOREIGN KEY (nom_magasin) REFERENCES boutique(nom_compagnie),
    FOREIGN KEY (nvendeur) REFERENCES vendeur(nvendeur)
);

CREATE TABLE IF NOT EXISTS stock_magasin(
    nom VARCHAR(255),
    nprod INT,
    nom_vélo VARCHAR(255),
    stock_v INT,
    nprod_p INT,
    nom_piece VARCHAR(255),
    stock_p INT,
    FOREIGN KEY (nom) REFERENCES boutique(nom_compagnie),
    FOREIGN KEY (nprod) REFERENCES modele(nprod),
    FOREIGN KEY (nprod_p) REFERENCES pièce(nprod_p)
);

CREATE TABLE IF NOT EXISTS stock_fournisseur(
    siret INT,
    nprod INT,
    nom_vélo VARCHAR(255),
    stock_v INT,
    nprod_p INT,
    nom_piece VARCHAR(255),
    stock_p INT,
    FOREIGN KEY (siret) REFERENCES fournisseur(siret),
    FOREIGN KEY (nprod) REFERENCES modele(nprod),
    FOREIGN KEY (nprod_p) REFERENCES pièce(nprod_p)
);

DELETE FROM boutique WHERE nom_compagnie = 'toto';
