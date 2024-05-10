using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public void NombreProduitsParFournisseur()
        {
            try
            {
                connection.Open();
                string Requete = @"
            SELECT f.contact AS Fournisseur,
                   (SELECT COUNT(*) FROM modele WHERE fournisseur = f.siret) AS NombreVélos,
                   (SELECT COUNT(*) FROM pièce WHERE nprod_f = f.siret) AS NombrePièces
            FROM fournisseur f;
        ";

                MySqlCommand cmd = new MySqlCommand(Requete, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Fournisseur | Nombre de Vélos | Nombre de Pièces");
                while (reader.Read())
                {
                    string fournisseur = reader["Fournisseur"].ToString();
                    int nombreVélos = reader.IsDBNull(reader.GetOrdinal("NombreVélos")) ? 0 : reader.GetInt32("NombreVélos");
                    int nombrePièces = reader.IsDBNull(reader.GetOrdinal("NombrePièces")) ? 0 : reader.GetInt32("NombrePièces");
                    Console.WriteLine($"{fournisseur} | {nombreVélos} | {nombrePièces}");
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

        // Méthode pour trouver le meilleur client en fonction du montant total des ventes en euros
        public void MeilleurClientParMontantVente()
        {
            try
            {
                connection.Open();
                string query = @"
                    SELECT c.nom, c.prenom, SUM(m.prix_u * cmd.quantite) AS MontantTotalVentes
                    FROM commande cmd
                    JOIN client c ON cmd.nclient = c.nclient
                    JOIN modele m ON cmd.nprod = m.nprod
                    GROUP BY c.nom, c.prenom
                    ORDER BY SUM(m.prix_u * cmd.quantite) DESC
                    LIMIT 1;
                ";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Meilleur client par montant total des ventes (en euros) :");
                while (reader.Read())
                {
                    Console.WriteLine($"Nom: {reader["nom"]} | Prénom: {reader["prenom"]} | Montant total des ventes: {reader["MontantTotalVentes"]} euros");
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
        public void ListerProduitsFaibleQuantite()
        {
            try
            {
                connection.Open();
                string Requete = @"
            SELECT nprod, nom, stock 
            FROM modele
            WHERE stock <= 2
            UNION
            SELECT nprod_p, desc_p, quantité
            FROM pièce
            WHERE quantité <= 2;
        ";

                MySqlCommand cmd = new MySqlCommand(Requete, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("ID Produit | Description | Quantité en Stock");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} | {reader[1]} | {reader[2]}");
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
        public void ChiffreAffairesParMagasinEtVentesParVendeur()
        {
            try
            {
                connection.Open();
                string Requete = @"
            SELECT b.nom_compagnie AS Magasin, v.nomv AS Vendeur, SUM(m.prix_u * cmd.quantite) AS TotalVentes
            FROM commande cmd
            JOIN vendeur v ON cmd.nvendeur = v.nvendeur
            JOIN boutique b ON v.boutique = b.nom_compagnie
            JOIN modele m ON cmd.nprod = m.nprod
            GROUP BY b.nom_compagnie, v.nomv
            ORDER BY b.nom_compagnie, SUM(m.prix_u * cmd.quantite) DESC;
        ";

                MySqlCommand cmd = new MySqlCommand(Requete, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Magasin | Vendeur | Total des Ventes");
                while (reader.Read())
                {
                    decimal totalVentes = reader.GetDecimal("TotalVentes");
                    string formattedTotal = totalVentes.ToString("N2", CultureInfo.CreateSpecificCulture("fr-FR")) + " €";
                    Console.WriteLine($"{reader["Magasin"]} | {reader["Vendeur"]} | {formattedTotal}");
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
