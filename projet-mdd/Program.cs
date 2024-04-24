using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
class Program
{
    static void Main()
    {
        Console.WriteLine("Bienvenue sur VeloMax");
        string CS = "SERVER=localhost;PORT=3306;DATABASE=VeloMax;UID=root;PASSWORD=root;"; //CS = Connection String
        MySqlConnection maConnexion;
        try
        {
            maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            Console.WriteLine("Connexion à la base de données réussie");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
        Creation_Tables(CS);
    }

    static void Demo(string CS)
    {
        Console.WriteLine("Bienvenue sur VeloMax. Appuyez sur une touche du clavier pour continuer");
        Console.ReadKey();
        Console.WriteLine("Voici les différentes fonctionnalités de VeloMax");
        Console.WriteLine("1. Création d'un client");
        Console.Clear();
        Creation_Client(CS);

    }

    static void Creation_Client(string CS)
    {
        try
        {
            MySqlConnection maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
            Console.WriteLine("Veuillez entrer le numéro de client");
            int numClient = Convert.ToInt32(Console.ReadLine());
            MySqlParameter numClientParam = new MySqlParameter("@numClient", MySqlDbType.Int32);
            Console.WriteLine("Veuillez entrer le nom du client");
            string nomClient = Console.ReadLine();
            MySqlParameter nomClientParam = new MySqlParameter("@nomClient", MySqlDbType.VarChar);
            Console.WriteLine("Veuillez entrer le prénom du client");
            string prenomClient = Console.ReadLine();
            MySqlParameter prenomClientParam = new MySqlParameter("@prenomClient", MySqlDbType.VarChar);
            Console.WriteLine("Veuillez entrer l'adresse du client");
            string adresseClient = Console.ReadLine();
            MySqlParameter adresseClientParam = new MySqlParameter("@adresseClient", MySqlDbType.VarChar);
            Console.WriteLine("Veuillez entrer le numéro de téléphone du client");
            int telClient = Convert.ToInt32(Console.ReadLine());
            MySqlParameter telClientParam = new MySqlParameter("@telClient", MySqlDbType.Int32);
            Console.WriteLine("Veuillez entrer l'adresse mail du client");
            string mailClient = Console.ReadLine();
            MySqlParameter mailClientParam = new MySqlParameter("@mailClient", MySqlDbType.VarChar);
            MySqlCommand creation= maConnexion.CreateCommand();
            creation.CommandText = "INSERT INTO client (nclient, nom, prenom, adresse, telephone, courriel) VALUES (@numClient, @nomClient, @prenomClient, @adresseClient, @telClient, @mailClient)";
            MySqlDataReader reader = creation.ExecuteReader();
            reader = creation.ExecuteReader();
            reader.Close();
            Console.WriteLine("Client créé");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
    }


    static void Creation_Tables(string CS)
    {
        MySqlConnection maConnexion = new MySqlConnection(CS);
        string createTable = "CREATE TABLE IF NOT EXISTS modele(\r\n\tnprod INT, \r\n\tnom VARCHAR(255),\r\n\tgrandeur VARCHAR(255), \r\n\tprix_u INT, \r\n\tligne_produit VARCHAR(255), \r\n\tdate_intro DATE, \r\n\tdate_disco DATE, \r\n\tPRIMARY KEY (nprod)\r\n);";
        MySqlCommand maCommande = maConnexion.CreateCommand();
        maCommande.CommandText = createTable;
        string createTable_pièce = "CREATE TABLE IF NOT EXISTS pièce (\r\n\tnprod_p INT, \r\n\tdesc_p VARCHAR(255), \r\n\tnom_f VARCHAR(255), \r\n\tnprod_f INT, \r\n\tprix_u_p INT, \r\n\tdate_commande DATE, \r\n\tadresse VARCHAR(255), \r\n\tdate_livraison DATE, \r\n\tquantité INT, \r\n    FOREIGN KEY (nprod_f) REFERENCES fournisseur(siret),\r\n\tPRIMARY KEY (nprod_p)\r\n);";
        MySqlCommand maCommande2 = maConnexion.CreateCommand();
        maCommande2.CommandText = createTable_pièce;
        string createTable_fournisseur = "CREATE TABLE IF NOT EXISTS fournisseur (\r\n\tsiret INT, \r\n\tcontact VARCHAR(255), \r\n\tadresse VARCHAR(255), \r\n\tlibellé INT, \r\n\tPRIMARY KEY (siret)\r\n);";
        MySqlCommand maCommande3 = maConnexion.CreateCommand();
        maCommande3.CommandText = createTable_fournisseur;
        string createTable_client = "CREATE TABLE IF NOT EXISTS client (nclient INT, \r\n\tnom VARCHAR(255), \r\n\tprenom VARCHAR(255), \r\n\tadresse VARCHAR(255), \r\n\ttelephone INT, \r\n\tcourriel VARCHAR(255), PRIMARY KEY (nclient)\r\n);";
        MySqlCommand maCommande4 = maConnexion.CreateCommand();
        maCommande4.CommandText = createTable_client;
        string createTable_boutique = "CREATE TABLE IF NOT EXISTS boutique (nom_compagnie VARCHAR(255), \r\n\tadresse VARCHAR(255), \r\n\ttelephone INT, \r\n\tcourriel VARCHAR(255),\r\n\tnom_b VARCHAR(255), \r\n\tPRIMARY KEY (nom_compagnie)\r\n );";
        MySqlCommand maCommande5 = maConnexion.CreateCommand();
        maCommande5.CommandText = createTable_boutique;
        string createTable_Fidélio = " CREATE TABLE IF NOT EXISTS fidélio (\r\n\t nprogramme INT, \r\n\t description VARCHAR(255), \r\n\t cout INT, \r\n\t durée VARCHAR(255), \r\n\t rabais INT, \r\n\t PRIMARY KEY(nprogramme)\r\n );\r\n";
        MySqlCommand maCommande6 = maConnexion.CreateCommand();
        maCommande6.CommandText = createTable_Fidélio;
        string createTable_commande = "CREATE TABLE IF NOT EXISTS commande(\r\n    ncommande INT PRIMARY KEY,\r\n    nprod INT,\r\n    nprod_p INT,\r\n    date_commande DATE,\r\n    adresse_livraison VARCHAR(255),\r\n    date_livraison DATE,\r\n    quantite INT,\r\n    FOREIGN KEY (nprod) REFERENCES modele(nprod),\r\n    FOREIGN KEY (nprod_p) REFERENCES piece(nprod_p));\r\n";
        MySqlCommand maCommande7 = maConnexion.CreateCommand();
        maCommande7.CommandText = createTable_commande;
        string createTable_assemblage = "CREATE TABLE IF NOT EXISTS assemblage(\r\n\tnprod INT,\r\n\tnprod_p INT,\r\n\tnom VARCHAR(255),\r\n\tgrandeur VARCHAR(255),\r\n\tcadre VARCHAR(255),\r\n\tguidon VARCHAR(255),\r\n\tfreins VARCHAR(255),\r\n\tselle VARCHAR(255),\r\n\tdérailleur_avant VARCHAR(255),\r\n\tdérailleur_arrière VARCHAR(255),\r\n\troue_avant VARCHAR(255),\r\n\troue_arrière VARCHAR(255),\r\n\treflecteurs VARCHAR(255),\r\n\tpedalier VARCHAR(255),\r\n\tordinateur VARCHAR(255),\r\n\tpanier VARCHAR(255),\r\n\tFOREIGN KEY (nprod) REFERENCES modele(nprod),\r\n\tFOREIGN KEY (nprod_p) REFERENCES piece(nprod_p)\r\n);\r\n";
        MySqlCommand maCommande8 = maConnexion.CreateCommand();
        maCommande8.CommandText = createTable_assemblage;
        try
        {
            maCommande.ExecuteNonQuery();
            maCommande2.ExecuteNonQuery();
            maCommande3.ExecuteNonQuery();
            maCommande4.ExecuteNonQuery();
            maCommande5.ExecuteNonQuery();
            Console.WriteLine("Tables créées");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }   
        maCommande.Dispose();
        maCommande2.Dispose();
        maCommande3.Dispose();
        maCommande4.Dispose();
        maCommande5.Dispose();
        
    }
}
