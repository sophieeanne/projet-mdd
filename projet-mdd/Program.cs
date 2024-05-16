using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using projet_mdd;
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string CS = "SERVER=localhost;PORT=3306;DATABASE=VeloMax;UID=root;PASSWORD=root;"; //CS = Connection String
        Console.WriteLine("Bienvenue sur VeloMax. Appuyez sur une touche du clavier pour continuer");
        Console.ReadKey();
        string continuer;
        Statistiques stats = new Statistiques(CS);


        do
        {
            Console.Clear();
            Console.WriteLine("Voulez-vous accéder à : \n1. La démo évaluateur de VeloMax \n2. La gestion des tables \n3. La gestion des stocks \n4. Module Statistiques");
            Console.WriteLine("Veuillez entrer le numéro de la démo ou gestion que vous souhaitez utiliser");
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    Demo(CS);
                    break;
                case 2:
                    Gestion_Tables(CS);
                    break;
                case 3:
                    Gestion_Stocks(CS);
                    break;
                case 4:
                    Module_Statistique(CS);
                    break;

                default:
                    Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                    break;
            }
            Console.WriteLine("Voulez-vous tester une autre fonctionnalité ? (O/N)");
            continuer = Console.ReadLine();
        }while(continuer == "O" || continuer == "o");
        Console.Clear();
        Console.WriteLine("Merci d'avoir utilisé VeloMax ! Appuyez sur une touche pour quitter");
        Console.ReadKey();
    }

    static void Module_Statistique(string CS)
    {
        Statistiques stats = new Statistiques(CS);
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Module Statistiques");
        Console.WriteLine("Choisissez une option :");
        Console.WriteLine("1. Rapport des quantités vendues");
        Console.WriteLine("2. Afficher membres et expiration");
        Console.WriteLine("3. Meilleur client par montant total des ventes");
        Console.WriteLine("4. Analyse des commandes");
        Console.WriteLine("5. Calcul des bonus des salariés");
        Console.WriteLine("Veuillez entrer le numéro de l'option que vous souhaitez utiliser");
        int choixStat = Convert.ToInt32(Console.ReadLine());
        switch (choixStat)
        {
            case 1:
                stats.RapportQuantitesVendues();
                break;
            case 2:
                stats.AfficherMembresEtExpiration();
                break;
            case 3:
                stats.MeilleurClientParMontantVente();
                break;
            case 4:
                stats.AnalyseCommandes();
                break;
            case 5:
                Console.WriteLine("Veuillez entrer le coefficient global :");
                double coeffglobal = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Veuillez entrer le coefficient 1 (pour la satisfaction) :");
                double coeff1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Veuillez entrer le coefficient 2 (pour le chiffre d'affaires) :");
                double coeff2 = Convert.ToDouble(Console.ReadLine());
                stats.CalculBonusSalaries(coeffglobal, coeff1, coeff2);
                break;
            default:
                Console.WriteLine("Option non valide. Veuillez choisir une option entre 1 et 5.");
                break;
        }
        Console.ResetColor();
    }


    // Menu Gestion des tables
    static void Gestion_Tables(string CS)
    {
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        string continuer;
        do
        {
            Console.Clear();
            Console.WriteLine("Bienvenue sur la gestion des tables de VeloMax");
            Console.WriteLine("Voici les différentes tables de VeloMax : \n1. Client \n2. Commande \n3. Fournisseur \n4. Boutique \n5. Modèle \n6. Pièce \n7. Vendeur");
            Console.WriteLine("Veuillez entrer le numéro de la table que vous souhaitez gérer");
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    Gestion_Client(CS);
                    break;
                case 2:
                    Gestion_Commande(CS);
                    break;
                case 3:
                    Gestion_Fournisseur(CS);
                    break;
                case 4:
                    Gestion_Boutique(CS);
                    break;
                case 5:
                    Gestion_Modele(CS);
                    break;
                case 6:
                    Gestion_Piece(CS);
                    break;
                case 7:
                    Gestion_Vendeur(CS);
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                    break;
            }
            Console.WriteLine("Voulez-vous continuer ? (O/N)");
            continuer = Console.ReadLine();
        } while (continuer == "O" || continuer == "o");
    }


    //Gestion de client (les fonctions sont dans la démo évaluateur)
    static void Gestion_Client(string CS)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Bienvenue sur la gestion de la table client");
        Console.WriteLine("Voici les différentes fonctionnalités de la table client : \n1. Création d'un client \n2. Modification d'un client \n3. Suppression d'un client");
        Console.WriteLine("Veuillez entrer le numéro de la fonctionnalité que vous souhaitez utiliser");
        int choix = Convert.ToInt32(Console.ReadLine());
        switch (choix)
        {
            case 1:
                Console.Clear();
                Creation_Client(CS);
                break;
            case 2:
                Console.Clear();
                Modification_Client(CS);
                break;
            case 3:
                Console.Clear();
                Suppression_Client(CS);
                break;
            default:
                Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                break;
        }
    }   



    //Gestion de commande (fonctionnelle)
    static void Gestion_Commande(string CS)
    {
        Console.Clear();
        Console.WriteLine("Bienvenue sur la gestion de la table commande");
        Console.WriteLine("Voici les différentes fonctionnalités de la table commande : \n1. Création d'une commande \n2. Modification d'une commande \n3. Suppression d'une commande");
        Console.WriteLine("Veuillez entrer le numéro de la fonctionnalité que vous souhaitez utiliser");
        int choix = Convert.ToInt32(Console.ReadLine());
        switch (choix)
        {
            case 1:
                Console.Clear();
                Creation_Commande(CS);
                break;
            case 2:
                Console.Clear();
                Modification_Commande(CS);
                break;
            case 3:
                Console.Clear();
                Suppression_Commande(CS);
                break;
            default:
                Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                break;
        }
    }
    static void Creation_Commande(string CS)
    {
        Console.WriteLine("Création d'une commande");
        try
        {
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();

            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM commande";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table commande : \n");
            Console.WriteLine("ncommande | numéro vélo | numéro pièce | numéro client | date commande | adresse livraison | date livraison | quantité de vélo | nom du magasin | numéro du vendeur | quantité pièces");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();

            int count = 0;
            int numCommande;
            do
            {
                Console.WriteLine("Veuillez entrer le numéro de commande");
                numCommande = Convert.ToInt32(Console.ReadLine());

                MySqlCommand verif_numCommande = maConnexion.CreateCommand();
                verif_numCommande.CommandText = "SELECT COUNT(*) FROM commande WHERE ncommande = @numCommande";
                verif_numCommande.Parameters.AddWithValue("@numCommande", numCommande);
                count = Convert.ToInt32(verif_numCommande.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("Erreur, ce numéro de commande existe déjà");
                }
            } while (count > 0);
            Console.WriteLine("Voulez vous commander un vélo ou une pièce ou les deux? (vélo/pièce/2)");
            string choix = Console.ReadLine();
            while (choix != "vélo" && choix != "pièce" && choix !="2")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }

            int? numProd = null;
            int? numProd_p = null;
            int? quantite = null;
            int? quantite_p = null;
            if (choix == "vélo")
            {
                Console.WriteLine("Pour réference, voici les modèles de vélos disponibles : \n");   
                string v = "SELECT nprod, nom FROM modele WHERE stock > 0";
                MySqlCommand voir_table_modele = maConnexion.CreateCommand();
                voir_table_modele.CommandText = v;
                MySqlDataReader reader_modele = voir_table_modele.ExecuteReader();
                Console.WriteLine("nprod | nom");
                while (reader_modele.Read())
                {
                    string currentRowAsString = "";
                    for (int i = 0; i < reader_modele.FieldCount; i++)
                    {
                        string valueAsString = reader_modele.GetValue(i).ToString();
                        currentRowAsString += valueAsString + "  ";
                    }
                    Console.WriteLine(currentRowAsString);
                }
                reader_modele.Close();
                voir_table_modele.Dispose();

                Console.WriteLine("Veuillez entrer le numéro du vélo");
                numProd = Convert.ToInt32(Console.ReadLine());
                numProd_p = null;
                Console.WriteLine("Veuillez entrer la quantité de vélos");
                quantite = Convert.ToInt32(Console.ReadLine());
            }
            else if(choix=="pièce")
            {
                Console.WriteLine("Pour réference, voici les pièces disponibles : \n");
                string p = "SELECT nprod_p, desc_p FROM pièce WHERE quantité >0;";
                MySqlCommand voir_table_piece = maConnexion.CreateCommand();
                voir_table_piece.CommandText = p;
                MySqlDataReader reader_piece = voir_table_piece.ExecuteReader();
                Console.WriteLine("nprod_p | nom");
                while (reader_piece.Read())
                {
                    string currentRowAsString = "";
                    for (int i = 0; i < reader_piece.FieldCount; i++)
                    {
                        string valueAsString = reader_piece.GetValue(i).ToString();
                        currentRowAsString += valueAsString + "  ";
                    }
                    Console.WriteLine(currentRowAsString);
                }
                reader_piece.Close();
                voir_table_piece.Dispose();
                Console.WriteLine("Veuillez entrer le numéro de la pièce");
                numProd_p = Convert.ToInt32(Console.ReadLine());
                numProd = null;
                Console.WriteLine("Veuillez entrer la quantité de pièces");
                quantite_p = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Pour réference, voici les modèles de vélos disponibles : \n");
                string v = "SELECT nprod, nom FROM modele WHERE stock >0";
                MySqlCommand voir_table_modele = maConnexion.CreateCommand();
                voir_table_modele.CommandText = v;
                MySqlDataReader reader_modele = voir_table_modele.ExecuteReader();
                Console.WriteLine("nprod | nom");
                while (reader_modele.Read())
                {
                    string currentRowAsString = "";
                    for (int i = 0; i < reader_modele.FieldCount; i++)
                    {
                        string valueAsString = reader_modele.GetValue(i).ToString();
                        currentRowAsString += valueAsString + "  ";
                    }
                    Console.WriteLine(currentRowAsString);
                }
                reader_modele.Close();
                voir_table_modele.Dispose();
                Console.WriteLine("Veuillez entrer le numéro du vélo");
                numProd = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Pour réference, voici les pièces disponibles : \n");
                string p = "SELECT nprod_p, desc_p FROM pièce WHERE quantité >0;";
                MySqlCommand voir_table_piece = maConnexion.CreateCommand();
                voir_table_piece.CommandText = p;
                MySqlDataReader reader_piece = voir_table_piece.ExecuteReader();
                Console.WriteLine("nprod_p | nom");
                while (reader_piece.Read())
                {
                    string currentRowAsString = "";
                    for (int i = 0; i < reader_piece.FieldCount; i++)
                    {
                        string valueAsString = reader_piece.GetValue(i).ToString();
                        currentRowAsString += valueAsString + "  ";
                    }
                    Console.WriteLine(currentRowAsString);
                }
                reader_piece.Close();
                voir_table_piece.Dispose();

                Console.WriteLine("Veuillez entrer le numéro de la pièce");
                numProd_p = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Veuillez entrer la quantité de pièces");
                quantite_p = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Veuillez entrer la quantité de vélos");
                quantite = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("\nPour réference, voici les numéros des clients : \n");
            string voir_client = "SELECT nclient, nom FROM client";
            MySqlCommand voir_table_client = maConnexion.CreateCommand();
            voir_table_client.CommandText = voir_client;
            MySqlDataReader reader_client = voir_table_client.ExecuteReader();
            Console.WriteLine("nclient | nom");
            while (reader_client.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader_client.FieldCount; i++)
                {
                    string valueAsString = reader_client.GetValue(i).ToString();
                    currentRowAsString += valueAsString + "  ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader_client.Close();
            voir_table_client.Dispose();

            Console.WriteLine("Veuillez entrer le numéro du client");
            int numClient = Convert.ToInt32(Console.ReadLine());

            DateTime dateCommande = DateTime.Now;

            Console.WriteLine("Veuillez entrer l'adresse de livraison");
            string adresseLivraison = Console.ReadLine();

            DateTime dateLivraison = dateCommande.AddDays(7);

            Console.WriteLine("Pour réference, voici les numéros des vendeurs et leurs boutiques correspondantes : \n");

            string voir_vendeur = "SELECT nvendeur, boutique FROM vendeur";
            MySqlCommand voir_table_vendeur = maConnexion.CreateCommand();
            voir_table_vendeur.CommandText = voir_vendeur;
            MySqlDataReader reader_vendeur = voir_table_vendeur.ExecuteReader();
            Console.WriteLine("nvendeur | boutique");
            while (reader_vendeur.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader_vendeur.FieldCount; i++)
                {
                    string valueAsString = reader_vendeur.GetValue(i).ToString();
                    currentRowAsString += valueAsString + "  ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader_vendeur.Close();
            voir_table_vendeur.Dispose();
            Console.WriteLine("Veuillez entrer le nom de la boutique");
            string nomBoutique = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le numéro du vendeur");
            int numVendeur = Convert.ToInt32(Console.ReadLine());
            MySqlCommand creation = maConnexion.CreateCommand();
            creation.CommandText = "INSERT INTO commande (ncommande, nprod, nprod_p, nclient, date_commande, adresse_livraison, date_livraison, quantite, nom_magasin, nvendeur, quantite_p) VALUES " +
                "(@numCommande, @numProd, @numProd_p, @numClient, @dateCommande, @adresseLivraison, @dateLivraison, @quantite, @nomBoutique, @numVendeur, @quantite_p)";
            creation.Parameters.AddWithValue("@numCommande", numCommande);
            creation.Parameters.AddWithValue("@numProd", numProd);
            creation.Parameters.AddWithValue("@numProd_p", numProd_p);
            creation.Parameters.AddWithValue("@numClient", numClient);
            creation.Parameters.AddWithValue("@dateCommande", dateCommande);
            creation.Parameters.AddWithValue("@adresseLivraison", adresseLivraison);
            creation.Parameters.AddWithValue("@dateLivraison", dateLivraison);
            creation.Parameters.AddWithValue("@quantite", quantite);
            creation.Parameters.AddWithValue("@nomBoutique", nomBoutique);
            creation.Parameters.AddWithValue("@numVendeur", numVendeur);
            creation.Parameters.AddWithValue("@quantite_p", quantite_p);
            int lignes = creation.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Commande créée");
            }
            else
            {
                Console.WriteLine("Erreur lors de la création de la commande");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Modification_Commande(string CS)
    {
        try
        {
            Console.WriteLine("Modification d'une commande");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();

            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM commande";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table commande : \n");
            Console.WriteLine("ncommande | numéro vélo | numéro pièce | numéro client | date commande | adresse livraison | date livraison | quantité de vélo | nom du magasin | numéro du vendeur | quantité pièces");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();

            int numCommande;
            Console.WriteLine("Veuillez rentrer le numéro de la commande à modifier");
            while (!int.TryParse(Console.ReadLine(), out numCommande)) //si l'input n'est pas un nombre
            {
                Console.WriteLine("Veuillez entrer un numéro valide.");
            }

            Console.WriteLine("Quelle information voulez-vous modifier ? (numéro vélo, numéro pièce, numéro client, adresse livraison, quantité de pièces, quantité de vélos)");
            string choix = Console.ReadLine();

            while (choix != "numéro vélo" && choix != "numéro pièce" && choix != "numéro client" && choix != "adresse livraison" && choix != "quantité de pièces" && choix !="quantités de vélos")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }
            MySqlCommand modification = maConnexion.CreateCommand();
            switch (choix)
            {
                case "numéro vélo":
                    Console.WriteLine("Pour réference, voici les modèles de vélos disponibles : \n");
                    string v = "SELECT nprod, nom FROM modele WHERE stock > 0";
                    MySqlCommand voir_table_modele = maConnexion.CreateCommand();
                    voir_table_modele.CommandText = v;
                    MySqlDataReader reader_modele = voir_table_modele.ExecuteReader();
                    Console.WriteLine("nprod | nom");
                    while (reader_modele.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader_modele.FieldCount; i++)
                        {
                            string valueAsString = reader_modele.GetValue(i).ToString();
                            currentRowAsString += valueAsString + "  ";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    reader_modele.Close();
                    voir_table_modele.Dispose();
                    Console.WriteLine("Veuillez entrer le nouveau numéro de vélo");
                    int numProd = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE commande SET nprod = @numProd WHERE ncommande = @numCommande";
                    modification.Parameters.AddWithValue("@numProd", numProd);
                    Console.WriteLine("Numéro de vélo modifié");
                    break;
                    case "numéro pièce":
                    Console.WriteLine("Pour réference, voici les pièces disponibles : \n");
                    string p = "SELECT nprod_p, desc_p FROM pièce WHERE quantité >0;";
                    MySqlCommand voir_table_piece = maConnexion.CreateCommand();
                    voir_table_piece.CommandText = p;
                    MySqlDataReader reader_piece = voir_table_piece.ExecuteReader();
                    Console.WriteLine("nprod_p | nom");
                    while (reader_piece.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader_piece.FieldCount; i++)
                        {
                            string valueAsString = reader_piece.GetValue(i).ToString();
                            currentRowAsString += valueAsString + "  ";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    reader_piece.Close();
                    voir_table_piece.Dispose();
                    Console.WriteLine("Veuillez entrer le nouveau numéro de pièce");
                    int numProd_p = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE commande SET nprod_p = @numProd_p WHERE ncommande = @numCommande";
                    modification.Parameters.AddWithValue("@numProd_p", numProd_p);
                    Console.WriteLine("Numéro de pièce modifié");
                    break;
                    case "numéro client":
                    Console.WriteLine("Pour réference, voici les numéros des clients : \n");
                    string voir_client = "SELECT nclient, nom FROM client";
                    MySqlCommand voir_table_client = maConnexion.CreateCommand();
                    voir_table_client.CommandText = voir_client;
                    MySqlDataReader reader_client = voir_table_client.ExecuteReader();
                    Console.WriteLine("nclient | nom");
                    while (reader_client.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader_client.FieldCount; i++)
                        {
                            string valueAsString = reader_client.GetValue(i).ToString();
                            currentRowAsString += valueAsString + "  ";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    reader_client.Close();
                    voir_table_client.Dispose();
                    Console.WriteLine("Veuillez entrer le nouveau numéro de client");
                    int numClient = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE commande SET nclient = @numClient WHERE ncommande = @numCommande";
                    modification.Parameters.AddWithValue("@numClient", numClient);
                    Console.WriteLine("Numéro de client modifié");
                    break;
                    case "adresse livraison":
                    Console.WriteLine("Veuillez entrer la nouvelle adresse de livraison");
                    string adresseLivraison = Console.ReadLine();
                    modification.CommandText = "UPDATE commande SET adresse_livraison = @adresseLivraison WHERE ncommande = @numCommande";
                    modification.Parameters.AddWithValue("@adresseLivraison", adresseLivraison);
                    Console.WriteLine("Adresse de livraison modifiée");
                    break;
                    case "quantité de vélos":
                    Console.WriteLine("Veuillez entrer la nouvelle quantité");
                    int quantite = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE commande SET quantite = @quantite WHERE ncommande = @numCommande";
                    modification.Parameters.AddWithValue("@quantite", quantite);
                    Console.WriteLine("Quantité modifiée");
                    break;
                    case "quantité de pièces":
                    Console.WriteLine("Veuillez entrer la nouvelle quantité");
                    int quantite_p = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE commande SET quantite_p = @quantite_p WHERE ncommande = @numCommande";
                    modification.Parameters.AddWithValue("@quantite_p", quantite_p);
                    Console.WriteLine("Quantité modifiée");
                    break;
                    default:
                    Console.WriteLine("Erreur, veuillez entrer une information valide");
                    break;
                    
            }      
            maConnexion.Close();
        }

        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Suppression_Commande(string CS)
    {
        try
        {
            Console.WriteLine("Suppression d'une commande");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM commande";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table commande : \n");
            Console.WriteLine("ncommande | numéro vélo | numéro pièce | numéro client | date commande | adresse livraison | date livraison | quantité");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            MySqlCommand suppression = maConnexion.CreateCommand();
            Console.WriteLine("Veuillez entrer le numéro de la commande à supprimer");
            int numCommande = Convert.ToInt32(Console.ReadLine());
            suppression.CommandText = "DELETE FROM commande WHERE ncommande = @numCommande";
            suppression.Parameters.AddWithValue("@numCommande", numCommande);
            int lignes = suppression.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Commande supprimée");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression de la commande");
            }
            suppression.Dispose();
            maConnexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }



    //Gestion de fournisseur
    static void Gestion_Fournisseur(string CS)
    {
        Console.Clear();
        Console.WriteLine("Bienvenue sur la gestion de la table fournisseur");
        Console.WriteLine("Voici les différentes fonctionnalités de la table fournisseur : \n1. Création d'un fournisseur \n2. Modification d'un fournisseur \n3. Suppression d'un fournisseur");
        Console.WriteLine("Veuillez entrer le numéro de la fonctionnalité que vous souhaitez utiliser");
        int choix = Convert.ToInt32(Console.ReadLine());
        switch (choix)
        {
            case 1:
                Console.Clear();
                Creation_Fournisseur(CS);
                break;
            case 2:
                Console.Clear();
                Modification_Fournisseur(CS);
                break;
            case 3:
                Console.Clear();
                Suppression_Fournisseur(CS);
                break;
            default:
                Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                break;
        }
    }
    static void Creation_Fournisseur(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Création d'un fournisseur");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM fournisseur";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table fournisseur : \n");
            Console.WriteLine("siret | contact | adresse | libellé");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            int count = 0;
            int siret;
            do
            {
                Console.WriteLine("Veuillez entrer le numéro de siret du fournisseur");
                siret = Convert.ToInt32(Console.ReadLine());
                MySqlCommand verif_siret = maConnexion.CreateCommand();
                verif_siret.CommandText = "SELECT COUNT(*) FROM fournisseur WHERE siret = @siret";
                verif_siret.Parameters.AddWithValue("@siret", siret);
                count = Convert.ToInt32(verif_siret.ExecuteScalar());
                if (count > 0)
                {
                    Console.WriteLine("Erreur, ce numéro de siret existe déjà");
                }
            } while (count > 0);
            Console.WriteLine("Veuillez entrer le contact du fournisseur");
            string contact = Console.ReadLine();
            Console.WriteLine("Veuillez entrer l'adresse du fournisseur");
            string adresse = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le libellé du fournisseur");
            int libelle = Convert.ToInt32(Console.ReadLine());
            MySqlCommand creation = maConnexion.CreateCommand();
            creation.CommandText = "INSERT INTO fournisseur (siret, contact, adresse, libellé) VALUES (@siret, @contact, @adresse, @libelle)";
            creation.Parameters.AddWithValue("@siret", siret);
            creation.Parameters.AddWithValue("@contact", contact);
            creation.Parameters.AddWithValue("@adresse", adresse);
            creation.Parameters.AddWithValue("@libelle", libelle);
            int lignes = creation.ExecuteNonQuery();
                
            if (lignes > 0)
            {
                Console.WriteLine("Fournisseur créé");
            }
            else
            {
                Console.WriteLine("Erreur lors de la création du fournisseur");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
    static void Modification_Fournisseur(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Modification d'un fournisseur");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM fournisseur";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table fournisseur : \n");
            Console.WriteLine("siret | contact | adresse | libellé");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            MySqlCommand modification = maConnexion.CreateCommand();
            Console.WriteLine("Quelle information voulez-vous modifier ? (contact, adresse, libellé)"); 
            string choix = Console.ReadLine();
            while (choix != "contact" && choix != "adresse" && choix != "libellé")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }
            switch (choix)
            {
                case "contact":
                    Console.WriteLine("Veuillez entrer le nouveau contact");
                    string contact = Console.ReadLine();
                    modification.CommandText = "UPDATE fournisseur SET contact = @contact WHERE siret = @siret";
                    modification.Parameters.AddWithValue("@contact", contact);
                    Console.WriteLine("Contact modifié");
                    break;
                case "adresse":
                    Console.WriteLine("Veuillez entrer la nouvelle adresse");
                    string adresse = Console.ReadLine();
                    modification.CommandText = "UPDATE fournisseur SET adresse = @adresse WHERE siret = @siret";
                    modification.Parameters.AddWithValue("@adresse", adresse);
                    Console.WriteLine("Adresse modifiée");
                    break;
                case "libellé":
                    Console.WriteLine("Veuillez entrer le nouveau libellé");
                    int libelle = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE fournisseur SET libellé = @libelle WHERE siret = @siret";
                    modification.Parameters.AddWithValue("@libelle", libelle);
                    Console.WriteLine("Libellé modifié");
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer une information valide");
                    break;
            }   

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Suppression_Fournisseur(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Suppression d'un fournisseur");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM fournisseur";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table fournisseur : \n");
            Console.WriteLine("siret | contact | adresse | libellé");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            MySqlCommand suppression = maConnexion.CreateCommand(); 
            Console.WriteLine("Veuillez entrer le numéro de siret du fournisseur à supprimer");
            int siret = Convert.ToInt32(Console.ReadLine());

           
            suppression.CommandText = "DELETE FROM fournisseur WHERE siret = @siret";
            suppression.Parameters.AddWithValue("@siret", siret);
            int lignes = suppression.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Fournisseur supprimé");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression du fournisseur");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ce fournisseur apparait dans une autre table, il ne peut donc pas être supprimé");
            return;
        }
    }



    //Gestion de boutique

    static void Gestion_Boutique(string CS)
    {
        Console.Clear();
        Console.WriteLine("Bienvenue sur la gestion de la table boutique");
        Console.WriteLine("Voici les différentes fonctionnalités de la table boutique : \n1. Création d'une boutique \n2. Modification d'une boutique \n3. Suppression d'une boutique");
        Console.WriteLine("Veuillez entrer le numéro de la fonctionnalité que vous souhaitez utiliser");
        int choix = Convert.ToInt32(Console.ReadLine());
        switch (choix)
        {
            case 1:
                Console.Clear();
                Creation_Boutique(CS);
                break;
            case 2:
                Console.Clear();
                Modification_Boutique(CS);
                break;
            case 3:
                Console.Clear();
                Suppression_Boutique(CS);
                break;
            default:
                Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                break;
        }
    }
    static void Creation_Boutique(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Création d'une boutique");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM boutique";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table boutique : \n");
            Console.WriteLine("nom de la compagnie | adresse | telephone | courriel | responsable | chiffre d'affaires");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            int count = 0;
            string nomCompagnie;
            do
            {
                Console.WriteLine("Veuillez entrer le nom de la compagnie");
                nomCompagnie = Console.ReadLine();
                MySqlCommand verif_nomCompagnie = maConnexion.CreateCommand();
                verif_nomCompagnie.CommandText = "SELECT COUNT(*) FROM boutique WHERE nom_compagnie = @nomCompagnie";
                verif_nomCompagnie.Parameters.AddWithValue("@nomCompagnie", nomCompagnie);
                count = Convert.ToInt32(verif_nomCompagnie.ExecuteScalar());
                if (count > 0)
                {
                    Console.WriteLine("Erreur, ce nom de compagnie existe déjà");
                }
            } while (count > 0);
            Console.WriteLine("Veuillez entrer l'adresse de la boutique");
            string adresse = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le numéro de téléphone de la boutique");
            string telephone = Console.ReadLine();
            Console.WriteLine("Veuillez entrer l'adresse mail de la boutique");
            string courriel = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le nom du responsable de la boutique");
            string responsable = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le chiffre d'affaires de la boutique");
            int chiffreAffaires = int.Parse(Console.ReadLine());
            MySqlCommand creation = maConnexion.CreateCommand();
            creation.CommandText = "INSERT INTO boutique (nom_compagnie, adresse, telephone, courriel, nom_b, chiffre) VALUES (@nomCompagnie, @adresse, @telephone, @courriel, @responsable, @chiffreAffaires)";
            creation.Parameters.AddWithValue("@nomCompagnie", nomCompagnie);
            creation.Parameters.AddWithValue("@adresse", adresse);
            creation.Parameters.AddWithValue("@telephone", telephone);
            creation.Parameters.AddWithValue("@courriel", courriel);
            creation.Parameters.AddWithValue("@responsable", responsable);
            creation.Parameters.AddWithValue("@chiffreAffaires", chiffreAffaires);
            int lignes = creation.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Boutique créée");
            }
            else
            {
                Console.WriteLine("Erreur lors de la création de la boutique");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
        
    }
    static void Modification_Boutique(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Modification d'une boutique");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM boutique";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table boutique : \n");
            Console.WriteLine("nom de la compagnie | adresse | telephone | courriel | responsable | chiffre d'affaires");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            MySqlCommand modification = maConnexion.CreateCommand();
            Console.WriteLine("Quelle information voulez-vous modifier ? (adresse, téléphone, courriel, responsable, chiffre d'affaires)");
            string choix = Console.ReadLine();
            while (choix != "adresse" && choix != "téléphone" && choix != "courriel" && choix != "responsable" && choix != "chiffre d'affaires")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }
            switch (choix)
            {
                case "adresse":
                    Console.WriteLine("Veuillez entrer la nouvelle adresse");
                    string adresse = Console.ReadLine();
                    modification.CommandText = "UPDATE boutique SET adresse = @adresse WHERE nom_compagnie = @nomCompagnie";
                    modification.Parameters.AddWithValue("@adresse", adresse);
                    Console.WriteLine("Adresse modifiée");
                    break;
                case "téléphone":
                    Console.WriteLine("Veuillez entrer le nouveau numéro de téléphone");
                    string telephone = Console.ReadLine();
                    modification.CommandText = "UPDATE boutique SET telephone = @telephone WHERE nom_compagnie = @nomCompagnie";
                    modification.Parameters.AddWithValue("@telephone", telephone);
                    Console.WriteLine("Téléphone modifié");
                    break;
                case "courriel":
                    Console.WriteLine("Veuillez entrer le nouveau courriel");
                    string courriel = Console.ReadLine();
                    modification.CommandText = "UPDATE boutique SET courriel = @courriel WHERE nom_compagnie = @nomCompagnie";
                    modification.Parameters.AddWithValue("@courriel", courriel);
                    Console.WriteLine("Courriel modifié");
                    break;
                case "responsable":
                    Console.WriteLine("Veuillez entrer le nouveau responsable");
                    string responsable = Console.ReadLine();
                    modification.CommandText = "UPDATE boutique SET nom_b = @responsable WHERE nom_compagnie = @nomCompagnie";
                    modification.Parameters.AddWithValue("@responsable", responsable);
                    Console.WriteLine("Responsable modifié");
                    break;
                case "chiffre d'affaires":
                    Console.WriteLine("Veuillez entrer le nouveau chiffre d'affaires");
                    int chiffreAffaires = int.Parse(Console.ReadLine());
                    modification.CommandText = "UPDATE boutique SET chiffre = @chiffreAffaires WHERE nom_compagnie = @nomCompagnie";
                    modification.Parameters.AddWithValue("@chiffreAffaires", chiffreAffaires);
                    Console.WriteLine("Chiffre d'affaires modifié");
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer une information valide");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Suppression_Boutique(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Suppression d'une boutique");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM boutique";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table boutique : \n");
            Console.WriteLine("nom de la compagnie | adresse | telephone | courriel | responsable | chiffre d'affaires");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            MySqlCommand suppression = maConnexion.CreateCommand();
            Console.WriteLine("Veuillez entrer le nom de la compagnie de la boutique à supprimer");
            string nomCompagnie = Console.ReadLine();
            suppression.CommandText = "DELETE FROM boutique WHERE nom_compagnie = @nomCompagnie";
            suppression.Parameters.AddWithValue("@nomCompagnie", nomCompagnie);
            int lignes = suppression.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Boutique supprimée");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression de la boutique");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Cette boutique apparait dans une table (stocks_magasin), elle ne peut donc pas être supprimée");
            return;
        }
    }



    //Gestion de modèle
    static void Gestion_Modele(string CS)
    {
        Console.Clear();
        Console.WriteLine("Bienvenue sur la gestion de la table modèle");
        Console.WriteLine("Voici les différentes fonctionnalités de la table modèle : \n1. Création d'un modèle \n2. Modification d'un modèle \n3. Suppression d'un modèle");
        Console.WriteLine("Veuillez entrer le numéro de la fonctionnalité que vous souhaitez utiliser");
        int choix = Convert.ToInt32(Console.ReadLine());
        switch (choix)
        {
            case 1:
                Creation_Modele(CS);
                break;
            case 2:
                Modification_Modele(CS);
                break;
            case 3:
                Suppression_Modele(CS);
                break;
            default:
                Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                break;
        }
    }
    static void Creation_Modele(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Création d'un modèle");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM modele";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table modèle : \n");
            Console.WriteLine("nprod nom grandeur prix_u ligne_produit date_intro date_disco stock");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            int count = 0;
            int nprod;
            do
            {
                Console.WriteLine("Veuillez entrer le numéro de produit");
                nprod = Convert.ToInt32(Console.ReadLine());
                MySqlCommand verif_nprod = maConnexion.CreateCommand();
                verif_nprod.CommandText = "SELECT COUNT(*) FROM modele WHERE nprod = @nprod";
                verif_nprod.Parameters.AddWithValue("@nprod", nprod);
                count = Convert.ToInt32(verif_nprod.ExecuteScalar());
                if (count > 0)
                {
                    Console.WriteLine("Erreur, ce numéro de produit existe déjà");
                }
            } while (count > 0);
            Console.WriteLine("Veuillez entrer le nom du modèle");
            string nom = Console.ReadLine();
            Console.WriteLine("Veuillez entrer la grandeur du modèle");
            string grandeur = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le prix unitaire du modèle");
            int prix_u = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez entrer la ligne de produit du modèle");
            string ligne_produit = Console.ReadLine();
            DateTime date_intro = DateTime.Now;
            Console.WriteLine("Veuillez entrer la date de discontinuation du modèle dans le format AAAA-MM-JJ ");
            DateTime date_disco = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Veuillez entrer le stock du modèle");
            int stock = Convert.ToInt32(Console.ReadLine());
            MySqlCommand creation = maConnexion.CreateCommand();
            creation.CommandText = "INSERT INTO modele (nprod, nom, grandeur, prix_u, ligne_produit, date_intro, date_disco, stock) VALUES (@nprod, @nom, @grandeur, @prix_u, @ligne_produit, @date_intro, @date_disco, @stock)";
            creation.Parameters.AddWithValue("@nprod", nprod);
            creation.Parameters.AddWithValue("@nom", nom);
            creation.Parameters.AddWithValue("@grandeur", grandeur);
            creation.Parameters.AddWithValue("@prix_u", prix_u);
            creation.Parameters.AddWithValue("@ligne_produit", ligne_produit);
            creation.Parameters.AddWithValue("@date_intro", date_intro);
            creation.Parameters.AddWithValue("@date_disco", date_disco);
            creation.Parameters.AddWithValue("@stock", stock);
            int lignes = creation.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Modèle créé");
            }
            else
            {
                Console.WriteLine("Erreur lors de la création du modèle");
            }
            creation.Dispose();
            maConnexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Modification_Modele(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Modification d'un modèle");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM modele";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table modèle : \n");
            Console.WriteLine("nprod nom grandeur prix_u ligne_produit date_intro date_disco stock");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            MySqlCommand modification = maConnexion.CreateCommand();
            Console.WriteLine("Quelle information voulez-vous modifier ? (nom, grandeur, prix unitaire, ligne de produit, date d'introduction, date de discontinuation, stock)");
            string choix = Console.ReadLine();
            while (choix != "nom" && choix != "grandeur" && choix != "prix unitaire" && choix != "ligne de produit" && choix != "date d'introduction" && choix != "date de discontinuation" && choix != "stock")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }
            switch (choix)
            {
                case "nom":
                    Console.WriteLine("Veuillez entrer le nouveau nom");
                    string nom = Console.ReadLine();
                    modification.CommandText = "UPDATE modele SET nom = @nom WHERE nprod = @nprod";
                    modification.Parameters.AddWithValue("@nom", nom);
                    Console.WriteLine("Nom modifié");
                    break;
                case "grandeur":
                    Console.WriteLine("Veuillez entrer la nouvelle grandeur");
                    string grandeur = Console.ReadLine();
                    modification.CommandText = "UPDATE modele SET grandeur = @grandeur WHERE nprod = @nprod";
                    modification.Parameters.AddWithValue("@grandeur", grandeur);
                    Console.WriteLine("Grandeur modifiée");
                    break;
                case "prix unitaire":
                    Console.WriteLine("Veuillez entrer le nouveau prix unitaire");
                    int prix_u = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE modele SET prix_u = @prix_u WHERE nprod = @nprod";
                    modification.Parameters.AddWithValue("@prix_u", prix_u);
                    Console.WriteLine("Prix unitaire modifié");
                    break;
                case "ligne de produit":
                    Console.WriteLine("Veuillez entrer la nouvelle ligne de produit");
                    string ligne_produit = Console.ReadLine();
                    modification.CommandText = "UPDATE modele SET ligne_produit = @ligne_produit WHERE nprod = @nprod";
                    modification.Parameters.AddWithValue("@ligne_produit", ligne_produit);
                    Console.WriteLine("Ligne de produit modifiée");
                    break;
                case "date d'introduction":
                    Console.WriteLine("Veuillez entrer la nouvelle date d'introduction");
                    DateTime date_intro = Convert.ToDateTime(Console.ReadLine());
                    modification.CommandText = "UPDATE modele SET date_intro = @date_intro WHERE nprod = @nprod";
                    modification.Parameters.AddWithValue("@date_intro", date_intro);
                    Console.WriteLine("Date d'introduction modifiée");
                    break;
                case "date de discontinuation":
                    Console.WriteLine("Veuillez entrer la nouvelle date de discontinuation");
                    DateTime date_disco = Convert.ToDateTime(Console.ReadLine());
                    modification.CommandText = "UPDATE modele SET date_disco = @date_disco WHERE nprod = @nprod";
                    modification.Parameters.AddWithValue("@date_disco", date_disco);
                    Console.WriteLine("Date de discontinuation modifiée");
                    break;
                case "stock":
                    Console.WriteLine("Veuillez entrer le nouveau stock");
                    int stock = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE modele SET stock = @stock WHERE nprod = @nprod";
                    modification.Parameters.AddWithValue("@stock", stock);
                    Console.WriteLine("Stock modifié");
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer une information valide");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Suppression_Modele(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Suppression d'un modèle");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM modele";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table modèle : \n");
            Console.WriteLine("nprod nom grandeur prix_u ligne_produit date_intro date_disco stock");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            MySqlCommand suppression = maConnexion.CreateCommand();
            Console.WriteLine("Veuillez entrer le numéro de produit du modèle à supprimer");
            int nprod = Convert.ToInt32(Console.ReadLine());

            suppression.CommandText = "DELETE FROM modele WHERE nprod = @nprod";
            suppression.Parameters.AddWithValue("@nprod", nprod);
            int lignes = suppression.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Modèle supprimé");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression du modèle");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ce modèle de vélo apparait dans une table, il ne peut donc pas être supprimé");
            return;
        }
    }



    //Gestion de pièce
    static void Gestion_Piece(string CS)
    {
        Console.Clear();
        Console.WriteLine("Bienvenue sur la gestion de la table pièce");
        Console.WriteLine("Voici les différentes fonctionnalités de la table pièce : \n1. Création d'une pièce \n2. Modification d'une pièce \n3. Suppression d'une pièce");
        Console.WriteLine("Veuillez entrer le numéro de la fonctionnalité que vous souhaitez utiliser");
        int choix = Convert.ToInt32(Console.ReadLine());
        switch (choix)
        {
            case 1:
                Creation_Piece(CS);
                break;
            case 2:
                Modification_Piece(CS);
                break;
            case 3:
                Suppression_Piece(CS);
                break;
            default:
                Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                break;
        }
    }
    static void Creation_Piece(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Création d'une pièce");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM piece";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table pièce : \n");
            Console.WriteLine("nprod_p desc_p nom_f nprod_f prix_u_p date_intro date_disco delai quantité");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();

            MySqlCommand voirtablefournisseur = maConnexion.CreateCommand();
            voirtablefournisseur.CommandText = "SELECT siret, contact  FROM fournisseur";
            MySqlDataReader readerfournisseur = voirtablefournisseur.ExecuteReader();
            Console.WriteLine("Pour réference, voici les numéros sirets et les nom des fournisseurs existants \n");
            while(readerfournisseur.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < readerfournisseur.FieldCount; i++)
                {
                    string valueAsString = readerfournisseur.GetValue(i).ToString();
                    currentRowAsString += valueAsString + "  ";
                }
                Console.WriteLine(currentRowAsString);
            }
            readerfournisseur.Close();
            voirtablefournisseur.Dispose();

            Console.WriteLine("Veuillez entrer le numéro de produit de la pièce");
            int nprod_p = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez entrer la description de la pièce");
            string desc_p = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le nom du fournisseur de la pièce");
            string nom_f = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le numéro siret du fournisseur de la pièce");
            int nprod_f = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez entrer le prix unitaire de la pièce");
            int prix_u_p = Convert.ToInt32(Console.ReadLine());
            DateTime date_intro = DateTime.Now;
            Console.WriteLine("Veuillez entrer la date de discontinuation de la pièce dans le format AAAA-MM-JJ ");
            DateTime date_disco = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Veuillez entrer le délai de livraison de la pièce");
            int delai = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez entrer la quantité de la pièce");
            int quantite = Convert.ToInt32(Console.ReadLine());
            MySqlCommand creation = maConnexion.CreateCommand();
            creation.CommandText = "INSERT INTO piece (nprod_p, desc_p, nom_f, nprod_f, prix_u_p, date_intro, date_disco, delai, quantité) VALUES (@nprod_p, @desc_p, @nom_f, @nprod_f, @prix_u_p, @date_intro, @date_disco, @delai, @quantite)";
            creation.Parameters.AddWithValue("@nprod_p", nprod_p);
            creation.Parameters.AddWithValue("@desc_p", desc_p);
            creation.Parameters.AddWithValue("@nom_f", nom_f);
            creation.Parameters.AddWithValue("@nprod_f", nprod_f);
            creation.Parameters.AddWithValue("@prix_u_p", prix_u_p);
            creation.Parameters.AddWithValue("@date_intro", date_intro);
            creation.Parameters.AddWithValue("@date_disco", date_disco);
            creation.Parameters.AddWithValue("@delai", delai);
            creation.Parameters.AddWithValue("@quantite", quantite);
            int lignes = creation.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Pièce créée");
            }
            else
            {
                Console.WriteLine("Erreur lors de la création de la pièce");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Modification_Piece(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Modification d'une pièce");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM piece";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table pièce : \n");
            Console.WriteLine("nprod_p desc_p nom_f nprod_f prix_u_p date_intro date_disco delai quantité");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            MySqlCommand modification = maConnexion.CreateCommand();
            Console.WriteLine("Quelle information voulez-vous modifier ? (description, nom du fournisseur, prix unitaire, date d'introduction, date de discontinuation, delai, quantité)");
            string choix = Console.ReadLine();
            while (choix != "description" && choix != "nom du fournisseur" && choix != "prix unitaire" && choix != "date d'introduction" && choix != "date de discontinuation" && choix != "delai" && choix != "quantité")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }
            switch (choix)
            {
                case "description":
                    Console.WriteLine("Veuillez entrer la nouvelle description");
                    string desc_p = Console.ReadLine();
                    modification.CommandText = "UPDATE piece SET desc_p = @desc_p WHERE nprod_p = @nprod_p";
                    modification.Parameters.AddWithValue("@desc_p", desc_p);
                    Console.WriteLine("Description modifiée");
                    break;
                case "nom du fournisseur":
                    Console.WriteLine("Veuillez entrer le nouveau nom du fournisseur");
                    string nom_f = Console.ReadLine();
                    modification.CommandText = "UPDATE piece SET nom_f = @nom_f WHERE nprod_p = @nprod_p";
                    modification.Parameters.AddWithValue("@nom_f", nom_f);
                    Console.WriteLine("Nom du fournisseur modifié");
                    break;
                case "prix unitaire":
                    Console.WriteLine("Veuillez entrer le nouveau prix unitaire");
                    int prix_u_p = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE piece SET prix_u_p = @prix_u_p WHERE nprod_p = @nprod_p";
                    modification.Parameters.AddWithValue("@prix_u_p", prix_u_p);
                    Console.WriteLine("Prix unitaire modifié");
                    break;
                case "date d'introduction":
                    Console.WriteLine("Veuillez entrer la nouvelle date d'introduction");
                    DateTime date_intro = Convert.ToDateTime(Console.ReadLine());
                    modification.CommandText = "UPDATE piece SET date_intro = @date_intro WHERE nprod_p = @nprod_p";
                    modification.Parameters.AddWithValue("@date_intro", date_intro);
                    Console.WriteLine("Date d'introduction modifiée");
                    break;
                case "date de discontinuation":
                    Console.WriteLine("Veuillez entrer la nouvelle date de discontinuation");
                    DateTime date_disco = Convert.ToDateTime(Console.ReadLine());
                    modification.CommandText = "UPDATE piece SET date_disco = @date_disco WHERE nprod_p = @nprod_p";
                    modification.Parameters.AddWithValue("@date_disco", date_disco);
                    Console.WriteLine("Date de discontinuation modifiée");
                    break;
                case "delai":
                    Console.WriteLine("Veuillez entrer le nouveau délai");
                    int delai = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE piece SET delai = @delai WHERE nprod_p = @nprod_p";
                    modification.Parameters.AddWithValue("@delai", delai);
                    Console.WriteLine("Délai modifié");
                    break;
                case "quantité":
                    Console.WriteLine("Veuillez entrer la nouvelle quantité");
                    int quantite = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE piece SET quantité = @quantite WHERE nprod_p = @nprod_p";
                    modification.Parameters.AddWithValue("@quantite", quantite);
                    Console.WriteLine("Quantité modifiée");
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer une information valide");
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Suppression_Piece(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Suppression d'une pièce");   
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM piece";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table pièce : \n");
            Console.WriteLine("nprod_p desc_p nom_f nprod_f prix_u_p date_intro date_disco delai quantité");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            MySqlCommand suppression = maConnexion.CreateCommand();
            Console.WriteLine("Veuillez entrer le numéro de produit de la pièce à supprimer");
            int nprod_p = Convert.ToInt32(Console.ReadLine());
            suppression.CommandText = "DELETE FROM piece WHERE nprod_p = @nprod_p";
            suppression.Parameters.AddWithValue("@nprod_p", nprod_p);
            int lignes = suppression.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Pièce supprimée");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression de la pièce");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Cette pièce apparait dans une table, elle ne peut donc pas être supprimée");
            return;
        }
    }


    //Gestion vendeur
    static void Gestion_Vendeur(string CS)
    {
        Console.Clear();
        Console.WriteLine("Bienvenue sur la gestion de la table vendeur");
        Console.WriteLine("Voici les différentes fonctionnalités de la table vendeur : \n1. Création d'un vendeur \n2. Modification d'un vendeur \n3. Suppression d'un vendeur");
        Console.WriteLine("Veuillez entrer le numéro de la fonctionnalité que vous souhaitez utiliser");
        int choix = Convert.ToInt32(Console.ReadLine());
        switch (choix)
        {
            case 1:
                Creation_Vendeur(CS);
                break;
            case 2:
                Modification_Vendeur(CS);
                break;
            case 3:
                Suppression_Vendeur(CS);
                break;
            default:
                Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                break;
        }

    }
    static void Creation_Vendeur(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Création d'un vendeur");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM vendeur";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table vendeur : \n");
            Console.WriteLine("nvendeur nomv prenomv numerov emailv boutique ventes (en K euros) satisfaction_client");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            int count = 0;
            int nvendeur;
            do
            {
                Console.WriteLine("Veuillez entrer le numéro de vendeur");
                nvendeur = Convert.ToInt32(Console.ReadLine());
                MySqlCommand verif_nvendeur = maConnexion.CreateCommand();
                verif_nvendeur.CommandText = "SELECT COUNT(*) FROM vendeur WHERE nvendeur = @nvendeur";
                verif_nvendeur.Parameters.AddWithValue("@nvendeur", nvendeur);
                count = Convert.ToInt32(verif_nvendeur.ExecuteScalar());
                if (count > 0)
                {
                    Console.WriteLine("Erreur, ce numéro de vendeur existe déjà");
                }
            } while (count > 0);
            Console.WriteLine("Veuillez entrer le nom du vendeur");
            string nomv = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le prénom du vendeur");
            string prenomv = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le numéro de téléphone du vendeur");
            string numerov = Console.ReadLine();
            Console.WriteLine("Veuillez entrer l'adresse mail du vendeur");
            string emailv = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le nom de la boutique du vendeur");
            Console.WriteLine("Pour réference, voici les noms des boutiques\n");
            MySqlCommand voir_table_boutique = maConnexion.CreateCommand();
            voir_table_boutique.CommandText = "SELECT nom_compagnie FROM boutique";
            MySqlDataReader reader_boutique = voir_table_boutique.ExecuteReader();
            while (reader_boutique.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader_boutique.FieldCount; i++)
                {
                    string valueAsString = reader_boutique.GetValue(i).ToString();
                    currentRowAsString += valueAsString + "  ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader_boutique.Close();
            voir_table_boutique.Dispose();
            string boutique = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le nombre de ventes du vendeur");
            int ventes = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez entrer la satisfaction client du vendeur");
            int satisfaction_client = Convert.ToInt32(Console.ReadLine());
            while(satisfaction_client < 1 || satisfaction_client > 5)
            {
                Console.WriteLine("Erreur, veuillez entrer une satisfaction client entre 0 et 10");
                satisfaction_client = Convert.ToInt32(Console.ReadLine());
            }
            MySqlCommand creation = maConnexion.CreateCommand();

            creation.CommandText = "INSERT INTO vendeur (nvendeur, nomv, prenomv, numerov, emailv, boutique, ventes, satisfaction_client) VALUES (@nvendeur, @nomv, @prenomv, @numerov, @emailv, @boutique, @ventes, @satisfaction_client)";
            creation.Parameters.AddWithValue("@nvendeur", nvendeur);
            creation.Parameters.AddWithValue("@nomv", nomv);
            creation.Parameters.AddWithValue("@prenomv", prenomv);
            creation.Parameters.AddWithValue("@numerov", numerov);
            creation.Parameters.AddWithValue("@emailv", emailv);
            creation.Parameters.AddWithValue("@boutique", boutique);
            creation.Parameters.AddWithValue("@ventes", ventes);
            creation.Parameters.AddWithValue("@satisfaction_client", satisfaction_client);
            int lignes = creation.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Vendeur créé");
            }
            else
            {
                Console.WriteLine("Erreur lors de la création du vendeur");
            }
            creation.Dispose();
            maConnexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Modification_Vendeur(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Modification d'un vendeur");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM vendeur";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table vendeur : \n");
            Console.WriteLine("nvendeur nomv prenomv numerov emailv boutique ventes (en K euros) satisfaction_client");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            MySqlCommand modification = maConnexion.CreateCommand();
            Console.WriteLine("Quelle information voulez-vous modifier ? (nom, prénom, numéro de téléphone, email, boutique, ventes, satisfaction client)");
            string choix = Console.ReadLine();
            while (choix != "nom" && choix != "prénom" && choix != "numéro de téléphone" && choix != "email" && choix != "boutique" && choix != "ventes" && choix != "satisfaction client")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }
            switch (choix)
            {
                case "nom":
                    Console.WriteLine("Veuillez entrer le nouveau nom");
                    string nomv = Console.ReadLine();
                    modification.CommandText = "UPDATE vendeur SET nomv = @nomv WHERE nvendeur = @nvendeur";
                    modification.Parameters.AddWithValue("@nomv", nomv);
                    Console.WriteLine("Nom modifié");
                    break;
                case "prénom":
                    Console.WriteLine("Veuillez entrer le nouveau prénom");
                    string prenomv = Console.ReadLine();
                    modification.CommandText = "UPDATE vendeur SET prenomv = @prenomv WHERE nvendeur = @nvendeur";
                    modification.Parameters.AddWithValue("@prenomv", prenomv);
                    Console.WriteLine("Prénom modifié");
                    break;
                case "numéro de téléphone":
                    Console.WriteLine("Veuillez entrer le nouveau numéro de téléphone");
                    string numerov = Console.ReadLine();
                    modification.CommandText = "UPDATE vendeur SET numerov = @numerov WHERE nvendeur = @nvendeur";
                    modification.Parameters.AddWithValue("@numerov", numerov);
                    Console.WriteLine("Numéro de téléphone modifié");
                    break;
                case "email":
                    Console.WriteLine("Veuillez entrer le nouvel email");
                    string emailv = Console.ReadLine();
                    modification.CommandText = "UPDATE vendeur SET emailv = @emailv WHERE nvendeur = @nvendeur";
                    modification.Parameters.AddWithValue("@emailv", emailv);
                    Console.WriteLine("Email modifié");
                    break;
                case "boutique":
                    Console.WriteLine("Veuillez entrer la nouvelle boutique");
                    Console.WriteLine("Pour réference, voici les noms des boutiques\n");
                    MySqlCommand voir_table_boutique = maConnexion.CreateCommand();
                    voir_table_boutique.CommandText = "SELECT nom_compagnie FROM boutique";
                    MySqlDataReader reader_boutique = voir_table_boutique.ExecuteReader();
                    while (reader_boutique.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader_boutique.FieldCount; i++)
                        {
                            string valueAsString = reader_boutique.GetValue(i).ToString();
                            currentRowAsString += valueAsString + "  ";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    reader_boutique.Close();
                    voir_table_boutique.Dispose();
                    string boutique = Console.ReadLine();
                    modification.CommandText = "UPDATE vendeur SET boutique = @boutique WHERE nvendeur = @nvendeur";
                    modification.Parameters.AddWithValue("@boutique", boutique);
                    Console.WriteLine("Boutique modifiée");
                    break;
                case "ventes":
                    Console.WriteLine("Veuillez entrer le nouveau nombre de ventes");
                    int ventes = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE vendeur SET ventes = @ventes WHERE nvendeur = @nvendeur";
                    modification.Parameters.AddWithValue("@ventes", ventes);
                    Console.WriteLine("Nombre de ventes modifié");
                    break;
                case "satisfaction client":
                    Console.WriteLine("Veuillez entrer la nouvelle satisfaction client");
                    int satisfaction_client = Convert.ToInt32(Console.ReadLine());
                    while (satisfaction_client < 1 || satisfaction_client > 5)
                    {
                        Console.WriteLine("Erreur, veuillez entrer une satisfaction client entre 0 et 10");
                        satisfaction_client = Convert.ToInt32(Console.ReadLine());
                    }
                    modification.CommandText = "UPDATE vendeur SET satisfaction_client = @satisfaction_client WHERE nvendeur = @nvendeur";
                    modification.Parameters.AddWithValue("@satisfaction_client", satisfaction_client);
                    Console.WriteLine("Satisfaction client modifiée");
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer une information valide");
                    break;
                modification.Dispose();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Suppression_Vendeur(string CS)
    {
        Console.Clear();
        try
        {
            Console.WriteLine("Suppression d'un vendeur");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM vendeur";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table vendeur : \n");
            Console.WriteLine("nvendeur nomv prenomv numerov emailv boutique ventes (en K euros) satisfaction_client");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            MySqlCommand suppression = maConnexion.CreateCommand();
            Console.WriteLine("Veuillez entrer le numéro de vendeur à supprimer");
            int nvendeur = Convert.ToInt32(Console.ReadLine());
            suppression.CommandText = "DELETE FROM vendeur WHERE nvendeur = @nvendeur";
            suppression.Parameters.AddWithValue("@nvendeur", nvendeur);
            int lignes = suppression.ExecuteNonQuery();
            if (lignes > 0)
            {
                Console.WriteLine("Vendeur supprimé");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression du vendeur");
            }
            suppression.Dispose();
            maConnexion.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }   
    }






    //Demo évaluateur (finie et fonctionnelle)
    static void Demo(string CS)
    {
        string continuer;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Bienvenue sur la démo évaluateur de VeloMax");
        Console.WriteLine("Voici les différentes fonctionnalités de cette démo");
        do
        {
            Console.Clear();
            Console.WriteLine("1. Création d'un client, modification et suppresion d'un client" +
                           "\n2. Nombre de Clients \n3. Nombre de clients avec le cumul de toutes ses commandes en euros" +
                           "\n4 .Listes des produits ayant une quantité en stock <=2 \n5. Nombres de pièces et/ou vélos fournis par fournisseur" +
                           "\n6. Le chiffre d'affaires par magasin et les ventes générées par le vendeur");
            Console.WriteLine("Veuillez entrer le numéro de la fonctionnalité que vous souhaitez utiliser");

            int choix = Convert.ToInt32(Console.ReadLine());

            switch (choix)
            {
                case 1:
                    Console.Clear();
                    Creation_Client(CS);
                    Console.WriteLine("Appuyez sur une touche pour continuer");
                    Console.ReadKey();  
                    Console.Clear();
                    Modification_Client(CS);
                    Console.WriteLine("Appuyez sur une touche pour continuer");
                    Console.ReadKey();
                    Console.Clear();
                    Suppression_Client(CS);
                    break;
                case 2:
                    Console.Clear();
                    Nombre_Clients(CS);
                    break;
                case 3:
                    Console.Clear();
                    Nombre_Clients_Commandes(CS);
                    break;
                case 4:
                    Console.Clear();
                    Produits_Stock(CS);
                    Console.WriteLine("Appuyez sur une touche pour continuer");
                    Console.ReadKey();
                    Console.Clear();
                    Produits_Stock2(CS);
                    break;
                case 5:
                    Console.Clear();
                    Pieces_Velos_Fournisseur(CS);
                    break;
                case 6:
                    Console.Clear();
                    Chiffre_Affaires(CS);
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                    break;
            }
            Console.WriteLine("\nVoulez-vous continuer ? (O/N)");
            continuer = Console.ReadLine();
        }while(continuer == "O" || continuer == "o");
      
    }
    static void Creation_Client(string CS)
    {
        
        Console.WriteLine("Création d'un client");
        try
        {
            //Connexion à la base de données
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();

            //L'état de la table client
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM client";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table client : \n");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();
            int count = 0;
            int numClient;
            //On demande à l'utilisateur de rentrer les informations du client
            do
            {
                Console.WriteLine("Veuillez entrer le numéro de client");
                numClient = Convert.ToInt32(Console.ReadLine());

                //On vérifie si le numéro de client existe déjà
                MySqlCommand verif_numClient = maConnexion.CreateCommand();
                verif_numClient.CommandText = "SELECT COUNT(*) FROM client WHERE nclient = @numClient";
                verif_numClient.Parameters.AddWithValue("@numClient", numClient);
                count = Convert.ToInt32(verif_numClient.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("Erreur, ce numéro de client existe déjà");
                }
            }while (count > 0);

            Console.WriteLine("Veuillez entrer le nom du client");
            string nomClient = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le prénom du client");
            string prenomClient = Console.ReadLine();
            Console.WriteLine("Veuillez entrer l'adresse du client");
            string adresseClient = Console.ReadLine();
            Console.WriteLine("Veuillez entrer le numéro de téléphone du client");
            string telClient = Console.ReadLine();
            Console.WriteLine("Veuillez entrer l'adresse mail du client");
            string mailClient = Console.ReadLine();
            Console.WriteLine("A quel numéro de Fidélio est-il abonné ? (1, 2, 3, 4 ou null si aucun)");
            int? fidelioClient;
            string f = Console.ReadLine();

            if (f.ToLower() == "null")
            {
                fidelioClient = null;
            }
            else
            {
                fidelioClient = Convert.ToInt32(f);
            }

            DateTime? dateexpiration = DateTime.Now;
            if (fidelioClient == 1)
            {
                dateexpiration = dateexpiration.Value.AddYears(1);
            }
            else if (fidelioClient == 2 || fidelioClient == 3)
            {
                dateexpiration = dateexpiration.Value.AddYears(2);
            }
            else if (fidelioClient == 4)
            {
                dateexpiration = dateexpiration.Value.AddYears(3);
            }
            else
            {
                dateexpiration = null;
            }



            //On crée les paramètres pour la requête
            MySqlParameter numClientParam = new MySqlParameter("@numClient", MySqlDbType.Int32);
            numClientParam.Value = numClient;
            MySqlParameter nomClientParam = new MySqlParameter("@nomClient", MySqlDbType.String);
            nomClientParam.Value = nomClient;
            MySqlParameter prenomClientParam = new MySqlParameter("@prenomClient", MySqlDbType.String);
            prenomClientParam.Value = prenomClient;
            MySqlParameter adresseClientParam = new MySqlParameter("@adresseClient", MySqlDbType.String);
            adresseClientParam.Value = adresseClient;
            MySqlParameter telClientParam = new MySqlParameter("@telClient", MySqlDbType.String);
            telClientParam.Value = telClient;
            MySqlParameter mailClientParam = new MySqlParameter("@mailClient", MySqlDbType.String);
            mailClientParam.Value = mailClient;
            MySqlParameter fidelioClientParam = new MySqlParameter("@fidelioClient",MySqlDbType.Int32);
            MySqlParameter dateexpirationParam = new MySqlParameter("@dateexpiration", MySqlDbType.DateTime);
            dateexpirationParam.Value = dateexpiration;

            //On crée la commande
            MySqlCommand creation = maConnexion.CreateCommand();
            creation.Parameters.Add(numClientParam);
            creation.Parameters.Add(nomClientParam);
            creation.Parameters.Add(prenomClientParam);
            creation.Parameters.Add(adresseClientParam);
            creation.Parameters.Add(telClientParam);
            creation.Parameters.Add(mailClientParam);
            creation.Parameters.Add(fidelioClientParam);
            creation.Parameters.Add(dateexpirationParam);

            creation.CommandText = "INSERT INTO client (nclient, nom, prenom, adresse, telephone, courriel, fidelio, expiration_fidélio) VALUES (@numClient, @nomClient, @prenomClient, @adresseClient, @telClient, @mailClient, @fidelioClient, @dateexpiration)";

            //On exécute la commande
            int lignes = creation.ExecuteNonQuery();

            if (lignes > 0)
            {
                Console.WriteLine("Client créé");
            }
            else
            {
                Console.WriteLine("Erreur lors de la création du client");
            }

            maConnexion.Close();
            creation.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }
    static void Modification_Client(string CS)
    {
       
        try
        {
            Console.WriteLine("Modification d'un client");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();

            //Montrer la table client
            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM client";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table client : \n");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            voir_table.Dispose();

            //On demande à l'utilisateur de rentrer le numéro du client à modifier
            int numClient;
            Console.WriteLine("Veuillez rentrer le numéro du client à modifier");
            while (!int.TryParse(Console.ReadLine(), out numClient)) //si l'input n'est pas un nombre
            {
                Console.WriteLine("Veuillez entrer un numéro valide.");
            }

            Console.WriteLine("Quelle information voulez-vous modifier ? (nom, prénom, adresse, téléphone, mail, fidélio)");
            string choix = Console.ReadLine();
            while (choix != "nom" && choix != "prénom" && choix != "adresse" && choix != "téléphone" && choix != "mail" && choix != "fidélio")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }

          
                MySqlCommand modification = maConnexion.CreateCommand();
                switch (choix)
                {
                    case "nom":
                        Console.WriteLine("Veuillez entrer le nouveau nom");
                        string nomClient = Console.ReadLine();
                        modification.CommandText = "UPDATE client SET nom = @nomClient WHERE nclient = @numClient";
                        modification.Parameters.AddWithValue("@nomClient", nomClient);
                        break;
                    case "prénom":
                        Console.WriteLine("Veuillez entrer le nouveau prénom");
                        string prenomClient = Console.ReadLine();
                        modification.CommandText = "UPDATE client SET prenom = @prenomClient WHERE nclient = @numClient";
                        modification.Parameters.AddWithValue("@prenomClient", prenomClient);
                        break;
                    case "adresse":
                        Console.WriteLine("Veuillez entrer la nouvelle adresse");
                        string adresseClient = Console.ReadLine();
                        modification.CommandText = "UPDATE client SET adresse = @adresseClient WHERE nclient = @numClient";
                        modification.Parameters.AddWithValue("@adresseClient", adresseClient);
                        break;
                    case "téléphone":
                        Console.WriteLine("Veuillez entrer le nouveau numéro de téléphone");
                        int telClient = Convert.ToInt32(Console.ReadLine());
                        modification.CommandText = "UPDATE client SET telephone = @telClient WHERE nclient = @numClient";
                        modification.Parameters.AddWithValue("@telClient", telClient);
                        break;
                    case "mail":
                        Console.WriteLine("Veuillez entrer le nouveau mail");
                        string mailClient = Console.ReadLine();
                        modification.CommandText = "UPDATE client SET courriel = @mailClient WHERE nclient = @numClient";
                        modification.Parameters.AddWithValue("@mailClient", mailClient);
                        break;
                    case "fidélio":
                        Console.WriteLine("Veuillez entrer le numéro de souscription fidélio");
                        int? fidelioClient;
                        string f = Console.ReadLine();
                        if (f.ToLower() == "null")
                        {
                            fidelioClient = null;
                        }
                        else
                        {
                            fidelioClient = Convert.ToInt32(f);
                        }
                        DateTime? dateexpiration = DateTime.Now;
                        if (fidelioClient == 1)
                        {
                            dateexpiration = dateexpiration.Value.AddYears(1);
                        }
                        else if (fidelioClient == 2 || fidelioClient == 3)
                        {
                            dateexpiration = dateexpiration.Value.AddYears(2);
                        }
                        else if (fidelioClient == 4)
                        {
                            dateexpiration = dateexpiration.Value.AddYears(3);
                        }
                        else
                        {
                            dateexpiration = null;
                        }
        
                        modification.CommandText = "UPDATE client SET fidelio = @fidelioClient, expiration_fidélio = @dateexpiration WHERE nclient = @numClient";
                        
                        modification.Parameters.AddWithValue("@fidelioClient", fidelioClient);
                        modification.Parameters.AddWithValue("@dateexpiration", dateexpiration);
                        break;
                    default:
                        Console.WriteLine("Erreur, veuillez entrer une information valide");
                        break;
                }
            modification.Parameters.AddWithValue("@numClient", numClient);

            int lignes = modification.ExecuteNonQuery();

            if (lignes > 0)
            {
                Console.WriteLine("L'information du client a été modifiée !");
            }
            else
            {
                Console.WriteLine("Erreur lors de la modification du client");
            }

            maConnexion.Close();
            modification.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }

    }
    static void Suppression_Client(string CS)
    {
     
        Console.WriteLine("Suppression d'un client");
        try
        {
            //Connexion à la base de données
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();

            MySqlCommand voir_table = maConnexion.CreateCommand();
            voir_table.CommandText = "SELECT * FROM client";
            MySqlDataReader reader = voir_table.ExecuteReader();
            Console.WriteLine("Voici la table client : \n");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();

            //On demande à l'utilisateur de rentrer le numéro du client à supprimer
            Console.WriteLine("Veuillez entrer le numéro du client à supprimer");
            int numClient = Convert.ToInt32(Console.ReadLine());

            //On crée le paramètre pour la requête
            MySqlParameter numClientParam = new MySqlParameter("@numClient", MySqlDbType.Int32);
            numClientParam.Value = numClient;

            //On crée la commande
            MySqlCommand suppression = maConnexion.CreateCommand();
            suppression.Parameters.Add(numClientParam);

            suppression.CommandText = "DELETE FROM client WHERE nclient = @numClient";

            //On exécute la commande
            int lignes = suppression.ExecuteNonQuery();

            if (lignes > 0)
            {
                Console.WriteLine("Client supprimé");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression du client");
            }

            maConnexion.Close();
            suppression.Dispose();
            voir_table.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine("Ce client est lié à une commande, il ne peut donc pas être supprimé");
            return;
        }
    }
    static void Nombre_Clients(string CS)
    {
        
        Console.WriteLine("Afficher le nombre de clients");
        try
        {
            //Connexion à la base de données
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();

            //Création de la commande
            int nbclients;
            string nb_clients = "SELECT COUNT(*) nclient FROM client";
            MySqlCommand commande = maConnexion.CreateCommand();
            commande.CommandText = nb_clients;
            object n = commande.ExecuteScalar();
            if(n!=null)
            {
                nbclients = Convert.ToInt32(n);
                Console.WriteLine("Il y'a " + nbclients + " clients en tout");
            }
            commande.Dispose();
            maConnexion.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Erreur : "+e.Message);
        }
            
    }
    static void Nombre_Clients_Commandes(string CS)
    {
     
        Console.WriteLine("Nombre des clients avec le cumul de toutes ses commandes en euros");
        try
        {
            //Connexion à la base de données
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            string requete = "SELECT c.nom, SUM(total_commande) AS total_commande_euros\r\nFROM (\r\n    SELECT \r\n        cmd.ncommande, \r\n        cmd.nclient,\r\n        SUM(m.prix_u * cmd.quantite) AS total_commande\r\n    FROM commande cmd\r\n    JOIN modele m ON cmd.nprod = m.nprod\r\n    GROUP BY cmd.ncommande, cmd.nclient\r\n\r\n    UNION ALL\r\n    \r\n    SELECT \r\n        cmd.ncommande, \r\n        cmd.nclient,\r\n        SUM(p.prix_u_p * cmd.quantite) AS total_commande\r\n    FROM commande cmd\r\n    JOIN pièce p ON cmd.nprod_p = p.nprod_p\r\n    GROUP BY cmd.ncommande, cmd.nclient\r\n) AS total_commandes\r\nJOIN client c ON total_commandes.nclient = c.nclient\r\nGROUP BY c.nom;";
            MySqlCommand commande = maConnexion.CreateCommand();
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            Console.WriteLine("Nom      Cumul des commandes (en euros)");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            commande.Dispose ();
            maConnexion.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Erreur : " +e.Message);
        }
    }
    static void Produits_Stock(string CS)
    {
        try
        {
            Console.WriteLine("Liste des produits ayant une quantité en stock <=2");
            MySqlConnection c = new MySqlConnection(CS);
            c.Open();
            string r = "SELECT desc_p, quantité FROM pièce WHERE quantité <=2;";
            MySqlCommand com = c.CreateCommand();
            com.CommandText = r;
            MySqlDataReader reader = com.ExecuteReader();
            Console.WriteLine("\nListe des pièces ayant une quantité en stock <=2");
            Console.WriteLine("Nom de la pièce | Quantité en stock");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            com.Dispose();
            c.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : " + e.Message);
        }
    }
    static void Produits_Stock2(string CS)
    {
        try
        {
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            string requete = "SELECT nom, stock FROM modele WHERE stock <=2;";
            MySqlCommand commande = maConnexion.CreateCommand();
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            Console.WriteLine("Liste des vélos ayant une quantité en stock <=2");
            Console.WriteLine("Nom du vélo | Quantité en stock");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            commande.Dispose();
            maConnexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : " + e.Message);
        }
    }
    static void Pieces_Velos_Fournisseur(string CS)
    {
        try
        {
            MySqlConnection c = new MySqlConnection(CS);
            c.Open();
            string r = "SELECT contact, COUNT(*) nprod_f\r\nFROM pièce JOIN fournisseur\r\nON nprod_f = siret\r\nGROUP BY contact;";
            MySqlCommand commande = c.CreateCommand();
            commande.CommandText = r;
            MySqlDataReader reader = commande.ExecuteReader();
            Console.WriteLine("Nombre de pièces fournies par chaque fournisseur");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + "  ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            commande.Dispose();
            c.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : " + e.Message);
        }

    }
    static void Chiffre_Affaires(string CS)
    {
        try
        {
            Console.WriteLine("Le chiffres d'affaires par magasin et les ventes générées par vendeur");
            MySqlConnection c = new MySqlConnection(CS);
            c.Open();
            string r1 = "SELECT nom_compagnie, chiffre FROM boutique;";
            string r2 = "SELECT nomv, ventes FROM vendeur;";
            MySqlCommand com1 = c.CreateCommand();
            MySqlCommand com2 = c.CreateCommand();
            com1.CommandText = r1;
            com2.CommandText = r2;
            MySqlDataReader reader = com1.ExecuteReader();
            Console.WriteLine("Chiffre d'affaires par magasin (en euros)");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + "  ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            com1.Dispose();
            c.Close();
            c.Open();
            MySqlDataReader reader2 = com2.ExecuteReader();
            Console.WriteLine("Ventes générées par vendeur (en milleur d'euros)");
            while (reader2.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    string valueAsString = reader2.GetValue(i).ToString();
                    currentRowAsString += valueAsString + "  ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader2.Close();
            com2.Dispose();
            c.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : " + e.Message);
        }
    }




    //Gestion des stocks
    static void Gestion_Stocks (string CS)
    {
        string continuer;
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Magenta;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Bienvenue sur la gestion des stocks de VeloMax");
        do
        {
            Console.Clear();
            Console.WriteLine("Quel est le stock dont vous voulez avoir la connaissance ? \n1. Stocks des pièces \n2. Stocks des vélos \n3. Stocks des fournisseurs \n4. Stocks des boutiques");
            Console.WriteLine("Entrez le numéro correspondant");
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    Console.Clear();
                    Stocks_Pieces(CS);
                    break;
                case 2:
                    Console.Clear();
                    Stocks_Modele(CS);
                    break;
                case 3:
                    Console.Clear();
                    Stocks_Fournisseurs(CS);
                    break;
                case 4:
                    Console.Clear();
                    Stocks_Boutiques(CS);
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                    break;
            }
            Console.WriteLine("\nVoulez-vous continuer ? (O/N)");
            continuer = Console.ReadLine();
        }while(continuer == "O" || continuer == "o");
      
    }
    static void Stocks_Pieces(string CS)
    {
        try
        {
            Console.WriteLine("Stocks des pièces : \n");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            string requete = "SELECT nprod_p, desc_p, quantité FROM pièce";
            MySqlCommand commande = maConnexion.CreateCommand();
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            Console.WriteLine("Numéro de la pièce | Description de la pièce | Quantité en stock");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            commande.Dispose();
            maConnexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : " + e.Message);
        }
    }
    static void Stocks_Modele(string CS)
    {
        try
        {
            Console.WriteLine("Stocks des vélos : \n");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            string requete = "SELECT nprod, nom, stock FROM modele";
            MySqlCommand commande = maConnexion.CreateCommand();
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            Console.WriteLine("Numéro du vélo | Nom du vélo | Stock");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
            commande.Dispose();
            maConnexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : " + e.Message);
        }
    }
    static void Stocks_Fournisseurs(string CS)
    {
        try
        {
            Console.WriteLine("Stocks des pièces par fournisseur : \n");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            string requete = "SELECT siret, nom_piece, stock_p FROM stock_fournisseur WHERE nom_piece IS NOT NULL;";
            MySqlCommand commande = maConnexion.CreateCommand();
            commande.CommandText = requete;
            MySqlDataReader reader = commande.ExecuteReader();
            Console.WriteLine("Numéro de siret | Nom de la pièce | Stock");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();

            Console.WriteLine("Appuyer sur une touche pour continuer"); 
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Stocks des vélos par fournisseur : \n");
            string requete2 = "SELECT siret, nom_vélo, stock_v FROM stock_fournisseur WHERE nom_vélo IS NOT NULL;";
            MySqlCommand commande2 = maConnexion.CreateCommand();
            commande2.CommandText = requete2;
            MySqlDataReader reader2 = commande2.ExecuteReader();
            Console.WriteLine("Numéro de siret | Nom du vélo | Stock");
            while (reader2.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    string valueAsString = reader2.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader2.Close();
            commande.Dispose();
            commande2.Dispose();
            maConnexion.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : " + e.Message);
        }
    }
    static void Stocks_Boutiques(string CS)
    {
        try
        {
            Console.WriteLine("Stocks des vélos par boutique : \n");
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            string requete1 = "SELECT nom, nom_vélo, stock_v FROM stock_magasin WHERE nom_vélo IS NOT NULL;";
            MySqlCommand commande1 = maConnexion.CreateCommand();
            commande1.CommandText = requete1;
            MySqlDataReader reader1 = commande1.ExecuteReader();
            Console.WriteLine("Nom de la boutique | Nom du vélo | Stock");
            while (reader1.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader1.FieldCount; i++)
                {
                    string valueAsString = reader1.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader1.Close();
            commande1.Dispose();

            Console.WriteLine("Appuyer sur une touche pour continuer");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Stocks des pièces par boutique : \n");
            string requete2 = "SELECT nom, nom_piece, stock_p FROM stock_magasin WHERE nom_piece IS NOT NULL;";
            MySqlCommand commande2 = maConnexion.CreateCommand();
            commande2.CommandText = requete2;
            MySqlDataReader reader2 = commande2.ExecuteReader();
            Console.WriteLine("Nom de la boutique | Nom de la pièce | Stock");
            while (reader2.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    string valueAsString = reader2.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " | ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader2.Close();
            commande2.Dispose();
            maConnexion.Close();

        }
        catch(Exception e)
        {
            Console.WriteLine("Erreur : " + e.Message);
        }

    }



}
