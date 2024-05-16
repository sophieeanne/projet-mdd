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
        public void AnalyseCommandes()
        {
            try
            {
                connection.Open();
                string query = @"
            SELECT 
                AVG(cmd.quantite * m.prix_u) AS MoyenneMontant,
                AVG(cmd.quantite) AS MoyenneQuantite,
                AVG(CASE WHEN cmd.nprod IS NOT NULL THEN cmd.quantite ELSE 0 END) AS MoyenneVelos,
                AVG(CASE WHEN cmd.nprod_p IS NOT NULL THEN cmd.quantite ELSE 0 END) AS MoyennePieces
            FROM commande cmd
            LEFT JOIN modele m ON cmd.nprod = m.nprod
            LEFT JOIN pièce pc ON cmd.nprod_p = pc.nprod_p
        ";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Moyenne des montants des commandes | Moyenne du nombre de pièces par commande | Moyenne de vélos par commande | Moyenne de pièces par commande");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["MoyenneMontant"]} | {reader["MoyenneQuantite"]} | {reader["MoyenneVelos"]} | {reader["MoyennePieces"]}");
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
        public void CalculBonusSalaries(double coeffglobal, double coeff1, double coeff2)
        {
            try
            {
                connection.Open();
                string query = @"
            SELECT 
                v.nomv AS Vendeur,
                IFNULL(SUM(CASE 
                    WHEN cmd.nprod IS NOT NULL THEN m.prix_u * cmd.quantite 
                    WHEN cmd.nprod_p IS NOT NULL THEN pc.prix_u_p * cmd.quantite 
                    ELSE 0 
                END), 0) AS ChiffreAffaires,
                IFNULL(AVG(v.satisfaction_client), 0) AS MoyenneSatisfaction
            FROM commande cmd
            JOIN vendeur v ON cmd.nvendeur = v.nvendeur
            LEFT JOIN modele m ON cmd.nprod = m.nprod
            LEFT JOIN pièce pc ON cmd.nprod_p = pc.nprod_p
            GROUP BY v.nomv;
        ";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("Vendeur | Chiffre d'affaires | Moyenne Satisfaction | Bonus");
                double totalBonus = 0;
                int count = 0;
                while (reader.Read())
                {
                    string vendeur = reader["Vendeur"].ToString();
                    double chiffreAffaires = Convert.ToDouble(reader["ChiffreAffaires"]);
                    double satisfaction = Convert.ToDouble(reader["MoyenneSatisfaction"]);
                    double bonus = coeffglobal * Math.Pow(satisfaction, coeff1) * Math.Pow(chiffreAffaires, coeff2);
                    totalBonus += bonus;
                    count++;
                    Console.WriteLine($"{vendeur} | {chiffreAffaires.ToString("N2", CultureInfo.CreateSpecificCulture("fr-FR"))} € | {satisfaction.ToString("N2", CultureInfo.CreateSpecificCulture("fr-FR"))} | {bonus.ToString("N2", CultureInfo.CreateSpecificCulture("fr-FR"))} €");
                }
                reader.Close();

                if (count > 0)
                {
                    double bonusMoyen = totalBonus / count;
                    Console.WriteLine($"Bonus moyen : {bonusMoyen.ToString("N2", CultureInfo.CreateSpecificCulture("fr-FR"))} €");
                }
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
