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
        string createTable = "";
        MySqlCommand maCommande = maConnexion.CreateCommand();
        maCommande.CommandText = createTable;
        string createTable_pièce = "";
        MySqlCommand maCommande2 = maConnexion.CreateCommand();
        maCommande2.CommandText = createTable_pièce;
        string createTable_fournisseur = "";
        MySqlCommand maCommande3 = maConnexion.CreateCommand();
        maCommande3.CommandText = createTable_fournisseur;
        string createTable_client = "";
        MySqlCommand maCommande4 = maConnexion.CreateCommand();
        maCommande4.CommandText = createTable_client;
        string createTable_boutique = "";
        MySqlCommand maCommande5 = maConnexion.CreateCommand();
        maCommande5.CommandText = createTable_boutique;
        string createTable_Fidélio = ";";
        MySqlCommand maCommande6 = maConnexion.CreateCommand();
        maCommande6.CommandText = createTable_Fidélio;


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
