using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
class Program
{
    static void Main()
    {
        string CS = "SERVER=localhost;PORT=3306;DATABASE=VeloMax;UID=root;PASSWORD=root;"; //CS = Connection String
        Demo(CS);
    }

    static void Demo(string CS)
    {
        string continuer;
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Bienvenue sur VeloMax. Appuyez sur une touche du clavier pour continuer");
        Console.ReadKey();
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
                    //Nombre_Clients(CS);
                    break;
                case 3:
                    //Nombre_Clients_Commandes(CS);
                    break;
                case 4:
                    //Produits_Stock(CS);
                    break;
                case 5:
                    //Pieces_Velos_Fournisseur(CS);
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
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
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
                    currentRowAsString += valueAsString + " , ";
                }
                Console.WriteLine(currentRowAsString);
            }
            reader.Close();
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
            int telClient = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Veuillez entrer l'adresse mail du client");
            string mailClient = Console.ReadLine();

            //On crée les paramètres pour la requête
            MySqlParameter numClientParam = new MySqlParameter("@numClient", MySqlDbType.Int32);
            numClientParam.Value = numClient;
            MySqlParameter nomClientParam = new MySqlParameter("@nomClient", MySqlDbType.String);
            nomClientParam.Value = nomClient;
            MySqlParameter prenomClientParam = new MySqlParameter("@prenomClient", MySqlDbType.String);
            prenomClientParam.Value = prenomClient;
            MySqlParameter adresseClientParam = new MySqlParameter("@adresseClient", MySqlDbType.String);
            adresseClientParam.Value = adresseClient;
            MySqlParameter telClientParam = new MySqlParameter("@telClient", MySqlDbType.Int32);
            telClientParam.Value = telClient;
            MySqlParameter mailClientParam = new MySqlParameter("@mailClient", MySqlDbType.String);
            mailClientParam.Value = mailClient;

            //On crée la commande
            MySqlCommand creation = maConnexion.CreateCommand();
            creation.Parameters.Add(numClientParam);
            creation.Parameters.Add(nomClientParam);
            creation.Parameters.Add(prenomClientParam);
            creation.Parameters.Add(adresseClientParam);
            creation.Parameters.Add(telClientParam);
            creation.Parameters.Add(mailClientParam);

            creation.CommandText = "INSERT INTO client (nclient, nom, prenom, adresse, telephone, courriel) VALUES (@numClient, @nomClient, @prenomClient, @adresseClient, @telClient, @mailClient)";

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
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
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

            //On demande à l'utilisateur de rentrer le numéro du client à modifier
            Console.WriteLine("Veuillez rentrer le numéro du client à modifier");
            int numClient = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Quelle information voulez-vous modifier ? (nom, prénom, adresse, téléphone, mail)");
            string choix = Console.ReadLine();
            MySqlCommand modification = maConnexion.CreateCommand();

            do
            {
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
                    default:
                        Console.WriteLine("Erreur, veuillez entrer une information valide");
                        break;
                }
                
            }while(choix != "nom" && choix != "prénom" && choix != "adresse" && choix != "téléphone" && choix != "mail");

            modification.Parameters.AddWithValue("@numClient", numClient);

            int rowsAffected = modification.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"L'information du client a été modifiée !");
            }
            else
            {
                Console.WriteLine("Erreur lors de la modification du client");
            }

            maConnexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return;
        }

    }
    static void Suppression_Client(string CS)
    {
        Console.BackgroundColor = ConsoleColor.Magenta;
        Console.ForegroundColor = ConsoleColor.White;
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
                    currentRowAsString += valueAsString + " , ";
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
}
