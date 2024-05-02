using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_mdd
{
    internal class Statistiques
    {
        private MySqlConnection connection;
        string CS = "SERVER=localhost;PORT=3306;DATABASE=VeloMax;UID=root;PASSWORD=root;"; //CS = Connection String
        public Statistiques(string CS)
        {
            connection = new MySqlConnection(CS);
        }

        public void RapportQuantitesVendues()
        {
            try
            {
                connection.Open();
                string Requete = @"
                SELECT 
                    p.nom, 
                    IFNULL(SUM(cmd.quantite), 0) as QuantiteTotale, 
                    b.nom_compagnie as Magasin,
                    v.nomv as Vendeur
                FROM commande cmd
                LEFT JOIN modele p ON cmd.nprod = p.nprod
                LEFT JOIN boutique b ON b.nom_compagnie = cmd.adresse_livraison
                LEFT JOIN vendeur v ON v.boutique = b.nom_compagnie
                GROUP BY p.nom, b.nom_compagnie, v.nomv
                UNION
                SELECT 
                    pc.desc_p, 
                    IFNULL(SUM(cmd.quantite), 0) as QuantiteTotale,
                    b.nom_compagnie as Magasin,
                    v.nomv as Vendeur
                FROM commande cmd
                LEFT JOIN pièce pc ON cmd.nprod_p = pc.nprod_p
                LEFT JOIN boutique b ON b.nom_compagnie = cmd.adresse_livraison
                LEFT JOIN vendeur v ON v.boutique = b.nom_compagnie
                GROUP BY pc.desc_p, b.nom_compagnie, v.nomv;
            ";

                MySqlCommand cmd = new MySqlCommand(Requete, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Produit | Quantité Totale | Magasin | Vendeur");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["nom"]} | {reader["QuantiteTotale"]} | {reader["Magasin"]} | {reader["Vendeur"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public void ListeMembresProgrammes()
        {
            try
            {
                connection.Open();
                string Requete = @"
            SELECT 
                f.description AS Programme,
                c.nom AS NomClient,
                c.prenom AS PrenomClient,
                c.fidelio AS NumeroFidelio,
                c.expiration_fidélio AS DateExpiration
            FROM client c
            JOIN fidélio f ON c.fidelio = f.nprogramme
            ORDER BY f.description, c.nom, c.prenom;
        ";

                MySqlCommand cmd = new MySqlCommand(Requete, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Programme | Nom Client | Prénom Client | Numéro Fidélio | Date d'Expiration");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Programme"]} | {reader["NomClient"]} | {reader["PrenomClient"]} | {reader["NumeroFidelio"]} | {reader["DateExpiration"]:yyyy-MM-dd}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public void AfficherMembresEtExpiration()
        {
            try
            {
                connection.Open();
                string query = @"
                SELECT f.description AS Programme, c.nom, c.prenom, c.courriel, c.expiration_fidélio AS DateExpiration
                FROM client c
                JOIN fidélio f ON c.fidelio = f.nprogramme
                ORDER BY f.description, c.nom, c.prenom;
            ";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Programme | Nom | Prénom | Courriel | Date d'Expiration");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Programme"]} | {reader["nom"]} | {reader["prenom"]} | {reader["courriel"]} | {reader.GetDateTime("DateExpiration").ToString("yyyy-MM-dd")}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
