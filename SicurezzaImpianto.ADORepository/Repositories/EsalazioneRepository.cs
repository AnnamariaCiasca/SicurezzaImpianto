using SicurezzaImpianto.Core.Entities;
using SicurezzaImpianto.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicurezzaImpianto.ADORepository
{
    public class EsalazioneRepository : IEsalazioneRepository
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                         "Initial Catalog = AcademyI;" +
                                         "Integrated Security = true";
        public List<Esalazione> GetItemsWithOutState()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM dbo.EsalazioniTossiche WHERE Stato IS NULL";

                    SqlDataReader reader = command.ExecuteReader();

                    List<Esalazione> esalazioni = new List<Esalazione>();

                    while (reader.Read())
                    {
                        Esalazione esalazione = new Esalazione();
                        esalazione.Id = (int)reader["Id"];
                        esalazione.DataMisurazione = Convert.ToDateTime(reader["DataMisurazione"]);
                        esalazione.OraMisurazione = (TimeSpan)reader["OraMisurazione"];
                        esalazione.ConcentrazionePpm = Convert.ToDouble(reader["ConcentrazionePPm"]);

                        esalazioni.Add(esalazione);
                    }
                    return esalazioni;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

     
        public void Update(Esalazione item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "UPDATE dbo.EsalazioniTossiche SET Stato=@stato WHERE Id=@id";
                    command.Parameters.AddWithValue("@stato", item.Stato);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.ExecuteNonQuery();





                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Insert(Esalazione es)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO dbo.EsalazioniTossiche VALUES (@concentrazionePpm, @dataMisurazione, @oraMisurazione, @stato)";
                command.Parameters.AddWithValue("@concentrazionePpm", es.ConcentrazionePpm);
                command.Parameters.AddWithValue("@dataMisurazione", es.DataMisurazione);
                command.Parameters.AddWithValue("@oraMisurazione", es.OraMisurazione);
                command.Parameters.AddWithValue("@stato", DBNull.Value);



                command.ExecuteNonQuery();
            }
        }
    }
}