using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
class Program
{
    static void Main()
    {
        string CS = "SERVER=localhost;PORT=3306;DATABASE=VeloMax;UID=root;PASSWORD=root;"; //CS = Connection String
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Bienvenue sur VeloMax. Appuyez sur une touche du clavier pour continuer");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("Voulez-vous accéder à la démo évaluateur de VeloMax, la gestion des tables ou la gestion des stocks ? (1,2,3)");
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
                //Gestion_Stocks(CS);
                break;
            default:
                Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                break;
        } 
    }

    static void Gestion_Tables(string CS)
    {
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
                    //Gestion_Modele(CS);
                    break;
                case 6:
                    //Gestion_Piece(CS);
                    break;
                case 7:
                    //Gestion_Vendeur(CS);
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                    break;
            }
            Console.WriteLine("Voulez-vous continuer ? (O/N)");
            continuer = Console.ReadLine();
        } while (continuer == "O" || continuer == "o");
    }
    static void Gestion_Client(string CS)
    {
        Console.Clear();
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
            Console.WriteLine("Voulez vous commander un vélo ou une pièce ? (vélo/pièce)");
            string choix = Console.ReadLine();
            while (choix != "vélo" && choix != "pièce")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }
            int? numProd = 0;
            int? numProd_p = 0;
            if (choix == "vélo")
            {
                Console.WriteLine("Veuillez entrer le numéro du vélo");
                numProd = Convert.ToInt32(Console.ReadLine());
                numProd_p = null;
            }
            else
            {
                Console.WriteLine("Veuillez entrer le numéro de la pièce");
                numProd_p = Convert.ToInt32(Console.ReadLine());
                numProd = null;
            }
            Console.WriteLine("Veuillez entrer le numéro du client");
            int numClient = Convert.ToInt32(Console.ReadLine());
            DateTime dateCommande = DateTime.Now;
            Console.WriteLine("Veuillez entrer l'adresse de livraison");
            string adresseLivraison = Console.ReadLine();
            DateTime dateLivraison = dateCommande.AddDays(7);
            Console.WriteLine("Veuillez entrer la quantité");
            int quantite = Convert.ToInt32(Console.ReadLine());
            if(choix == "vélo")
            {
                MySqlCommand creation = maConnexion.CreateCommand();
                creation.CommandText = "INSERT INTO commande (ncommande, nprod, nclient, date_commande, adresse_livraison, date_livraison, quantite) VALUES (@numCommande, @numProd, @numClient, @dateCommande, @adresseLivraison, @dateLivraison, @quantite)";
                creation.Parameters.AddWithValue("@numProd", numProd);
                creation.Parameters.AddWithValue("@numClient", numClient);
                creation.Parameters.AddWithValue("@dateCommande", dateCommande);
                creation.Parameters.AddWithValue("@adresseLivraison", adresseLivraison);
                creation.Parameters.AddWithValue("@dateLivraison", dateLivraison);
                creation.Parameters.AddWithValue("@quantite", quantite);
                creation.Parameters.AddWithValue("@numCommande", numCommande);
                int rowsAffected = creation.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Commande créée");
                }
                else
                {
                    Console.WriteLine("Erreur lors de la création de la commande");
                }
                creation.Dispose();
            }
            else
            {
                MySqlCommand creation = maConnexion.CreateCommand();
                creation.CommandText = "INSERT INTO commande (ncommande, nprod_p, nclient, date_commande, adresse_livraison, date_livraison, quantite) VALUES (@numCommande, @numProd_p, @numClient, @dateCommande, @adresseLivraison, @dateLivraison, @quantite)";
                creation.Parameters.AddWithValue("@numProd_p", numProd_p);
                creation.Parameters.AddWithValue("@numClient", numClient);
                creation.Parameters.AddWithValue("@dateCommande", dateCommande);
                creation.Parameters.AddWithValue("@adresseLivraison", adresseLivraison);
                creation.Parameters.AddWithValue("@dateLivraison", dateLivraison);
                creation.Parameters.AddWithValue("@quantite", quantite);
                creation.Parameters.AddWithValue("@numCommande", numCommande);
                int rowsAffected = creation.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Commande créée");
                }
                else
                {
                    Console.WriteLine("Erreur lors de la création de la commande");
                }
                creation.Dispose();
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
            voir_table.Dispose();
            int numCommande;
            Console.WriteLine("Veuillez rentrer le numéro de la commande à modifier");
            while (!int.TryParse(Console.ReadLine(), out numCommande)) //si l'input n'est pas un nombre
            {
                Console.WriteLine("Veuillez entrer un numéro valide.");
            }
            Console.WriteLine("Quelle information voulez-vous modifier ? (numéro vélo, numéro pièce, numéro client, adresse livraison, quantité)");
            string choix = Console.ReadLine();
            while (choix != "numéro vélo" && choix != "numéro pièce" && choix != "numéro client" && choix != "adresse livraison" && choix != "quantité")
            {
                Console.WriteLine("Erreur de frappe.Veuillez rentrer une information valide");
                choix = Console.ReadLine().ToLower();
            }
            MySqlCommand modification = maConnexion.CreateCommand();
            switch (choix)
            {
                case "numéro vélo":
                    Console.WriteLine("Veuillez entrer le nouveau numéro de vélo");
                    int numProd = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE commande SET nprod = @numProd WHERE ncommande = @numCommande";
                    modification.Parameters.AddWithValue("@numProd", numProd);
                    Console.WriteLine("Numéro de vélo modifié");
                    break;
                    case "numéro pièce":
                    Console.WriteLine("Veuillez entrer le nouveau numéro de pièce");
                    int numProd_p = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE commande SET nprod_p = @numProd_p WHERE ncommande = @numCommande";
                    modification.Parameters.AddWithValue("@numProd_p", numProd_p);
                    Console.WriteLine("Numéro de pièce modifié");
                    break;
                    case "numéro client":
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
                    case "quantité":
                    Console.WriteLine("Veuillez entrer la nouvelle quantité");
                    int quantite = Convert.ToInt32(Console.ReadLine());
                    modification.CommandText = "UPDATE commande SET quantite = @quantite WHERE ncommande = @numCommande";
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
            int rowsAffected = suppression.ExecuteNonQuery();
            if (rowsAffected > 0)
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
            int rowsAffected = creation.ExecuteNonQuery();
                
            if (rowsAffected > 0)
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
            int rowsAffected = suppression.ExecuteNonQuery();
            if (rowsAffected > 0)
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
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }

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
            int rowsAffected = creation.ExecuteNonQuery();
            if (rowsAffected > 0)
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
            int rowsAffected = suppression.ExecuteNonQuery();
            if (rowsAffected > 0)
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
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }

    static void Demo(string CS)
    {
        string continuer;
        Console.WriteLine("Bienvenue sur la démo évaluateur de VeloMax");
        Console.WriteLine("Voici les différentes fonctionnalités de VeloMax");
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
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Creation_Client(CS);
                    Console.WriteLine("Appuyez sur une touche pour continuer");
                    Console.ReadKey();  
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Modification_Client(CS);
                    Console.WriteLine("Appuyez sur une touche pour continuer");
                    Console.ReadKey();
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.White;
                    Suppression_Client(CS);
                    break;
                case 2:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Nombre_Clients(CS);
                    break;
                case 3:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Nombre_Clients_Commandes(CS);
                    break;
                case 4:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Produits_Stock(CS);
                    break;
                case 5:
                    Console.Clear();
                    Pieces_Velos_Fournisseur(CS);
                    break;
                case 6:
                    //Chiffre_Affaires(CS);
                    break;
                default:
                    Console.WriteLine("Erreur, veuillez entrer un numéro valide");
                    break;
            }
            Console.WriteLine("Voulez-vous continuer ? (O/N)");
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

            //On crée la commande
            MySqlCommand creation = maConnexion.CreateCommand();
            creation.Parameters.Add(numClientParam);
            creation.Parameters.Add(nomClientParam);
            creation.Parameters.Add(prenomClientParam);
            creation.Parameters.Add(adresseClientParam);
            creation.Parameters.Add(telClientParam);
            creation.Parameters.Add(mailClientParam);
            creation.Parameters.Add(fidelioClientParam);

            creation.CommandText = "INSERT INTO client (nclient, nom, prenom, adresse, telephone, courriel, fidelio) VALUES (@numClient, @nomClient, @prenomClient, @adresseClient, @telClient, @mailClient, @fidelioClient)";

            //On exécute la commande
            int rowsAffected = creation.ExecuteNonQuery();

            if (rowsAffected > 0)
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
                    currentRowAsString += valueAsString + " , ";
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
                        modification.CommandText = "UPDATE client SET fidelio = @fidelioClient WHERE nclient = @numClient";
                        modification.Parameters.AddWithValue("@fidelioClient", fidelioClient);
                        break;
                    default:
                        Console.WriteLine("Erreur, veuillez entrer une information valide");
                        break;
                }
            modification.Parameters.AddWithValue("@numClient", numClient);

            int rowsAffected = modification.ExecuteNonQuery();

            if (rowsAffected > 0)
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
            int rowsAffected = suppression.ExecuteNonQuery();

            if (rowsAffected > 0)
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
            Console.WriteLine("Error: " + e.Message);
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
            MySqlConnection c = new MySqlConnection(CS);
            c.Open();
            string r = "SELECT desc_p, quantité FROM pièce WHERE quantité <=2;";
            string r1 = "SELECT nom, stock FROM modele WHERE stock <=2;";
            MySqlCommand com = c.CreateCommand();
            MySqlCommand com2 = c.CreateCommand();
            com.CommandText = r;
            com2.CommandText = r1;
            MySqlDataReader reader = com.ExecuteReader();
            Console.WriteLine("Liste des pièces ayant une quantité en stock <=2");
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " ";
                }
                Console.WriteLine(currentRowAsString);
            }
            Console.WriteLine("\nListe des vélos ayant une quantité en stock <=2");
            reader.Close();
            com.Dispose();
            c.Close();
            c.Open();
            MySqlDataReader r3 = com2.ExecuteReader();
            while (r3.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = r3.GetValue(i).ToString();
                    currentRowAsString += valueAsString + " ";
                }
                Console.WriteLine(currentRowAsString);
            }
            r3.Close();
            com2.Dispose();
            c.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : "+e.Message);
        }
    }
    static void Pieces_Velos_Fournisseur(string CS)
    {

    }


}
