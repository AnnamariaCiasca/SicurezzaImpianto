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
    public class TemperaturaRepository : ITemperaturaRepository
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                       "Initial Catalog = AcademyI;" +
                                       "Integrated Security = true";
        public List<Temperatura> GetItemsWithOutState()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM dbo.Temperatura WHERE Stato IS NULL";

                    SqlDataReader reader = command.ExecuteReader();

                    List<Temperatura> temperature = new List<Temperatura>();

                    while (reader.Read())
                    {
                        Temperatura temperatura = new Temperatura();
                        temperatura.Id = (int)reader["Id"];
                        temperatura.DataMisurazione = Convert.ToDateTime(reader["DataMisurazione"]);
                        temperatura.OraMisurazione = (TimeSpan)reader["OraMisurazione"];
                        temperatura.ValoreTemperatura = Convert.ToDouble(reader["ValoreTemperatura"]);

                        temperature.Add(temperatura);
                    }
                    return temperature;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

    }

        public void Update(Temperatura item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "UPDATE dbo.Temperatura SET Stato = @stato WHERE Id=@id";
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
    }
}
