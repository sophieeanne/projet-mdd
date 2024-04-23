using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
class Program
{
    static void Main()
    {
        string CS = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=root;"; //CS = Connection String
        MySqlConnection maConnexion;
        try
        {
            maConnexion = new MySqlConnection(CS);
            maConnexion.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }
        Creation_Tables(maConnexion);
    }

    static void Creation_Tables(MySqlConnection maConnexion)
    {
        string createTable = "CREATE TABLE IF NOT EXISTS modèle (nprod INT, nom VARCHAR(255), grandeur VARCHAR(255), prix_u INT, ligne_produit VARCHAR(255), date_intro DATE, date_disco DATE, PRIMARY KEY (nprod));";
        MySqlCommand maCommande = maConnexion.CreateCommand();
        maCommande.CommandText = createTable;
        string createTable_pièce = "CREATE TABLE IF NOT EXISTS pièce (nprod_p INT, description VARCHAR(255), nom_f VARCHAR(255), nprod_f INT, prix_u_p INT, date_commande DATE, adresse VARCHAR(255), date_livraison DATE, quantité INT, PRIMARY KEY (nprod_p));";
        MySqlCommand maCommande2 = maConnexion.CreateCommand();
        maCommande2.CommandText = createTable_pièce;
        string createTable_fournisseur = "CREATE TABLE IF NOT EXISTS fournisseur (siret INT, contact VARCHAR(255), adresse VARCHAR(255), libellé INT, PRIMARY KEY (siret));";
        MySqlCommand maCommande3 = maConnexion.CreateCommand();
        maCommande3.CommandText = createTable_fournisseur;
        string createTable_client = "CREATE TABLE IF NOT EXISTS client (nclient INT, nom VARCHAR(255), prenom VARCHAR(255), adresse VARCHAR(255), telephone INT, courriel VARCHAR(255), PRIMARY KEY (nclient));";
        MySqlCommand maCommande4 = maConnexion.CreateCommand();
        maCommande4.CommandText = createTable_client;
        string createTable_boutique = "CREATE TABLE IF NOT EXISTS boutique (nom_compagnie VARCHAR(255), adresse VARCHAR(255), telephone INT, courriel VARCHAR(255), nom_b VARCHAR(255), PRIMARY KEY (nom_compagnie))";
        MySqlCommand maCommande5 = maConnexion.CreateCommand();
        maCommande5.CommandText = createTable_boutique;
        string createTable_Fidélio = "CREATE TABLE IF NOT EXISTS fidélio (nprogramme INT, description VARCHAR(255), cout INT, durée VARCHAR(255), rabais INT, PRIMARY KEY(nprogramme));";
        MySqlCommand maCommande6 = maConnexion.CreateCommand();
        maCommande6.CommandText = createTable_Fidélio;
        string createTable_assemblage = "CREATE TABLE IF NOT EXISTS assemblage (nom VARCHAR(255), FOREIGN KEY REFERENCES modèle(nom), ;"; 

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
