USE VeloMax;

DELETE FROM assemblage;
DELETE FROM commande;
DELETE FROM stock_magasin;
DELETE FROM stock_fournisseur;
DELETE FROM vendeur;
DELETE FROM boutique;
DELETE FROM client;
DELETE FROM fidélio;
DELETE FROM pièce;
DELETE FROM modele;
DELETE FROM fournisseur;

INSERT INTO fournisseur (siret, contact, adresse, libellé) VALUES
(100001, 'Jean Dupont', '12 Rue des Entrepreneurs 75012 Paris, France', 1),
(100002, 'Marie Curie', '33 Avenue de la Liberté 67000 Strasbourg, France', 2),
(100003, 'Lucas Martin', '88 Boulevard des Alliés 31000 Toulouse, France', 3),
(100004, 'Sophie Mercier', '150 Rue de la Loire 44000 Nantes, France', 4),
(100005, 'Olivier Giroud', '23 Rue du Commerce 69000 Lyon, France', 1);

INSERT INTO modele (nprod, nom, grandeur, prix_u, ligne_produit, date_intro, date_disco, stock) VALUES
(101, 'Kilimandjaro', 'Adultes', 569, 'VTT', '2022-05-12', '2023-12-31', 2),
(102, 'NorthPole', 'Adultes', 329, 'VTT', '2022-01-01', NULL, 7),
(103, 'MontBlanc', 'Jeunes', 399, 'VTT', '2022-01-01', '2024-06-30', 5),
(104, 'Hooligan', 'Jeunes', 199, 'VTT', '2022-06-08', NULL, 1),
(105, 'Orléans', 'Hommes', 229, 'Vélo de course', '2022-01-01', '2024-03-15', 2),
(106, 'Orléans', 'Dames', 229, 'Vélo de course', '2022-01-01', NULL, 8),
(107, 'BlueJay', 'Hommes', 349, 'Vélo de course', '2022-01-01', NULL, 7),
(108, 'BlueJay', 'Dames', 349, 'Vélo de course', '2022-01-01', '2023-10-01', 0),
(109, 'Trail Explorer', 'Filles', 129, 'Classique', '2022-01-01', '2023-12-31', 4),
(110, 'Trail Explorer', 'Garçons', 129, 'Classique', '2022-04-02', NULL, 5),
(111, 'Night Hawk', 'Jeunes', 189, 'Classique', '2022-01-01', NULL, 3),
(112, 'Tierra Verde', 'Hommes', 199, 'Classique', '2022-01-01', '2023-08-20', 0),
(113, 'Tierra Verde', 'Dames', 199, 'Classique', '2022-01-01', NULL, 7),
(114, 'Mud Zinger I', 'Jeunes', 279, 'BMX', '2022-08-04', NULL, 5),
(115, 'Mud Zinger II', 'Adultes', 359, 'BMX', '2022-11-11', '2023-11-30', 2);

INSERT INTO pièce (nprod_p, desc_p, nom_f, nprod_f, prix_u_p, date_intro, date_disco, delai, quantité) VALUES
(1, 'C32', 'Jean Dupont', 100001, 12.50, '2023-05-15', NULL, 3, 50),
(2, 'C34', 'Marie Curie', 100002, 8.75, '2023-07-22', NULL, 2, 100),
(3, 'C76', 'Lucas Martin', 100003, 18.20, '2023-06-10', NULL, 5, 30),
(4, 'C43', 'Sophie Mercier', 100004, 6.90, '2023-08-29', NULL, 4, 80),
(5, 'C44f', 'Jean Dupont', 100001, 14.00, '2023-09-17', NULL, 3, 60),
(6, 'C01', 'Marie Curie', 100002, 9.30, '2023-04-08', NULL, 2, 120),
(7, 'C02', 'Lucas Martin', 100003, 15.80, '2023-03-25', NULL, 5, 40),
(8, 'C15', 'Sophie Mercier', 100004, 7.60, '2023-11-11', NULL, 4, 90),
(9, 'C87', 'Jean Dupont', 100001, 11.20, '2023-10-30', NULL, 3, 70),
(10, 'C87f', 'Marie Curie', 100002, 10.50, '2023-01-19', NULL, 2, 140);

INSERT INTO boutique (nom_compagnie, adresse, telephone, courriel, nom_b, chiffre) VALUES
('Cycles Paris', '25 Rue du Faubourg Saint-Antoine, 75011 Paris, France', '+33 1 40 09 93 94', 'contact@cyclesparis.com', 'Toto Titi', 1000000),
('Vélos Lyon', '10 Rue de la République, 69002 Lyon, France', '+33 4 72 77 07 08', 'contact@veloslyon.com', 'Song Mingi', 500000),
('Bike Center Marseille', '15 Rue de la République, 13001 Marseille, France', '+33 4 91 42 61 24', 'contact@bikecentermarseille.com', 'Lee Hyejin', 800000),
('Vélo Passion Bordeaux', '30 Rue Sainte-Catherine, 33000 Bordeaux, France', '+33 5 56 48 28 62', 'contact@velopassionbordeaux.com', 'Garcia Lopez', 3000000),
('Wheels & Pedals Nice', '5 Avenue Jean Médecin, 06000 Nice, France', '+33 4 93 62 29 55', 'contact@wheelsandpedalsnice.com', 'Zhang Wei', 4000000);

INSERT INTO stock_magasin (nom, nprod, nom_vélo, stock_v, nprod_p, nom_piece, stock_p) VALUES
('Cycles Paris', 103, 'MontBlanc', 0, 1, 'C32', 100),
('Cycles Paris', 104, 'Hooligan', 7, 2, 'C34', 200),
('Vélo Passion Bordeaux', 105, 'Orléans', 3, 3, 'C76', 150),
('Vélos Lyon', 107, 'BlueJay', 2, 4, 'C43', 50),
('Wheels & Pedals Nice', 109, 'Trail Explorer', 50, 5, 'C44f', 250),
('Bike Center Marseille', 111, 'Night Hawk', 78, 6, 'C01', 300),
('Cycles Paris', 113, 'Tierra Verde', 95, 7, 'C02', 400),
('Vélo Passion Bordeaux', 115, 'Mud Zinger II', 76, 8, 'C15', 500),
('Vélos Lyon', 112, 'Tierra Verde', 32, 9, 'C87', 600),
('Wheels & Pedals Nice', 110, 'Trail Explorer', 21, 10, 'C87f', 700);

INSERT INTO client (nclient, nom, prenom, adresse, telephone, courriel, fidelio, expiration_fidélio) VALUES
(101, 'Dupont', 'Jean', '5 Rue de la Paix, 75002 Paris, France', '+33 1 40 20 00 00', 'jean.dupont@example.com', 1, '2025-01-01'),
(102, 'Martin', 'Sophie', '15 Avenue des Champs-Élysées, 75008 Paris, France', '+33 1 45 60 70 80', 'sophie.martin@example.com', 2, '2026-01-01'),
(103, 'Dubois', 'Pierre', '25 Rue de Rivoli, 75004 Paris, France', '+33 1 55 30 40 50', 'pierre.dubois@example.com', 3, '2026-01-01'),
(104, 'Bernard', 'Anne', '35 Rue du Faubourg Saint-Honoré, 75008 Paris, France', '+33 1 67 80 90 00', 'anne.bernard@example.com', 4, '2027-01-01'),
(105, 'Lefevre', 'Marie', '45 Boulevard Haussmann, 75009 Paris, France', '+33 1 75 85 95 10', 'marie.lefevre@example.com', NULL, NULL);

INSERT INTO fidélio (nprogramme, description, cout, durée, rabais) VALUES
(1, 'Fidélio', 15, '1 an', 5),
(2, 'Fidélio Or', 25, '2 ans', 8),
(3, 'Fidélio Platine', 60, '2 ans', 10),
(4, 'Fidélio Max', 100, '3 ans', 12);

INSERT INTO vendeur (nvendeur, nomv, prenomv, numerov, emailv, boutique, ventes, satisfaction_client) VALUES
(1, 'Dupont', 'Jean', '0123456789', 'jean.dupont@example.com', 'Cycles Paris', 20, 4),
(2, 'Durand', 'Marie', '0987654321', 'marie.durand@example.com', 'Cycles Paris', 15, 5),
(3, 'Martin', 'Lucas', '0654321098', 'lucas.martin@example.com', 'Vélos Lyon', 25, 1),
(4, 'Leclerc', 'Sophie', '0789456123', 'sophie.leclerc@example.com', 'Vélos Lyon', 18, 2),
(5, 'Toto', 'Aurélie', '0134678901', 'aurelie.toto@example.com', 'Bike Center Marseille', 12, 1),
(6, 'Gagnon', 'Luc', '0678451236', 'luc.gagnon@example.com', 'Vélo Passion Bordeaux', 17, 2),
(7, 'Chen', 'Ling', '0456123789', 'ling.chen@example.com', 'Wheels & Pedals Nice', 22, 3);

INSERT INTO assemblage (nprod, nom, grandeur, cadre, guidon, freins, selle, dérailleur_avant, dérailleur_arrière, roue_avant, roue_arrière, reflecteurs, pedalier, ordinateur, panier) VALUES
(101, 'Kilimandjaro', 'Adultes', 'C32', 'G7', 'F3', 'S88', 'DV133', 'DR56', 'R45', 'R46', NULL, 'P12', 'O2', NULL),
(102, 'NorthPole', 'Adultes', 'C34', 'G9', 'F3', 'S37', 'DV17', 'DR87', 'R48', 'R47', NULL, 'P34', NULL, NULL),
(103, 'MontBlanc', 'Jeunes', 'C76', 'G12', 'F3', 'S35', 'DV17', 'DR87', 'R48', 'R47', NULL, 'P15', 'O2', NULL),
(104, 'Hooligan', 'Jeunes', 'C43', 'G9', 'F9', 'S37', 'DV87', 'DR86', 'R12', 'R32', NULL, 'P34', NULL, NULL),
(105, 'Orléans', 'Hommes', 'C44f', 'G9', 'F9', 'S35', 'DV57', 'DR86', 'R19', 'R18', 'R02', 'P34', NULL, NULL),
(106, 'Orléans', 'Dames', 'C43', 'G12', 'S02', NULL, 'DV57', 'DR86', 'R19', 'R18', 'R02', 'P15', NULL, 'S01'),
(107, 'BlueJay', 'Hommes', 'C15', 'G7', 'F3', 'S87', 'DV57', 'DR87', 'R44', 'R47', NULL, 'P12', NULL, NULL),
(108, 'BlueJay', 'Dames', 'C87', 'G12', 'F9', 'S87', 'DV57', 'DR87', 'R44', 'R47', NULL, 'P15', NULL, NULL),
(109, 'Trail Explorer', 'Filles', 'C87f', 'G7', 'F3', 'S87', 'DV15', 'DR23', 'R11', 'R12', 'R10', 'P15', NULL, 'S74'),
(110, 'Trail Explorer', 'Garçons', 'C25', 'G12', 'F9', 'S87', 'DV132', 'DR52', 'R44', 'R47', NULL, 'P12', NULL, NULL),
(111, 'Night Hawk', 'Jeunes', 'C26', 'G7', 'F3', 'S87', 'DV133', 'DR52', 'R44', 'R47', NULL, 'P12', NULL, NULL),
(112, 'Tierra Verde', 'Hommes', 'C87', 'G12', 'F9', 'S87', 'DV41', 'DR76', 'R11', 'R12', 'R10', 'P15', NULL, 'S74'),
(113, 'Tierra Verde', 'Dames', 'C87f', 'G12', 'F9', 'S87', 'DV41', 'DR76', 'R11', 'R12', 'R10', 'P15', NULL, 'S73'),
(114, 'Mud Zinger I', 'Jeunes', 'C15', 'G7', 'F3', 'S87', 'DV132', 'DR52', 'R44', 'R47', NULL, 'P12', NULL, NULL),
(115, 'Mud Zinger II', 'Adultes', 'C26', 'G7', 'F3', 'S87', 'DV133', 'DR52', 'R44', 'R47', NULL, 'P12', NULL, NULL);

INSERT INTO commande (ncommande, nprod, nprod_p, nclient, date_commande, adresse_livraison, date_livraison, quantite, nom_magasin, nvendeur, quantite_p) VALUES
(1, 101, 1, 101, '2024-04-01', '10 Rue de la Liberté, 75001 Paris, France', '2024-04-10', 2, 'Cycles Paris', 1, 3),
(2, 105, 2, 102, '2024-04-02', '20 Avenue des Champs-Élysées, 75008 Paris, France', '2024-04-12', 1, 'Cycles Paris', 2, 8),
(3, 109, 3, 103, '2024-04-03', '30 Rue du Faubourg Saint-Honoré, 75008 Paris, France', '2024-04-15', 3, 'Vélos Lyon', 3, 5),
(4, 102, 4, 104, '2024-04-04', '40 Avenue Montaigne, 75008 Paris, France', '2024-04-14', 2, 'Vélos Lyon', 4, 3),
(5, 107, 5, 105, '2024-04-05', '50 Rue de Rivoli, 75004 Paris, France', '2024-04-16', 1, 'Bike Center Marseille', 5, 4),
(6, 111, 6, 101, '2024-04-06', '60 Avenue de lOpéra, 75002 Paris, France', '2024-04-18', 2, 'Vélo Passion Bordeaux', 6, 2),
(7, 114, 7, 102, '2024-04-07', '70 Rue Saint-Honoré, 75001 Paris, France', '2024-04-20', 1, 'Wheels & Pedals Nice', 7, 2),
(8, 112, 8, 103, '2024-04-08', '80 Rue de la Paix, 75002 Paris, France', '2024-04-22', 2, 'Cycles Paris', 1, 1),
(9, 106, 9, 104, '2024-04-09', '90 Avenue Marceau, 75008 Paris, France', '2024-04-24', 1, 'Cycles Paris', 2, 5),
(10, 115, 10, 105, '2024-04-10', '100 Rue du Faubourg Saint-Honoré, 75008 Paris, France', '2024-04-25', 3, 'Vélos Lyon', 3, 7);

