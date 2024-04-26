using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.PortableExecutable;
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
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
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
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
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
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Liste des produits ayant une quantité en stock <=2");
        try
        {
            MySqlConnection c = new MySqlConnection(CS);
            c.Open();
            string r = "SELECT desc_p, quantité FROM pièce WHERE quantité <=2;";
            MySqlCommand com = c.CreateCommand();
            com.CommandText = r;
            MySqlDataReader reader = com.ExecuteReader();
            //Console.WriteLine("Liste des pièces ayant une quantité en stock <=2");
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
            reader.Close();
            com.Dispose();
            c.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : "+e.Message);
        }
    }


}
