using System;
using System.Data.SqlClient;
using APPCORE.BDCore.Abstracts;
using BusinessLogic.Connection;

namespace Operations
{
    public class DateOLAPOperation
    {
        private static readonly WDataMapper? _dataMapper = new BDConnection().DBDestino;
        private static readonly string _connectionString = _dataMapper.GDatos.ConexionString;
        private const string _processName = "CargaGeneralDW";

        /// <summary>
        /// Obtiene la última fecha en que se ejecutó el proceso ETL.
        /// </summary>
        public static DateTime GetLastUpdatedate()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT TOP 1 FechaUltimaEjecucion FROM ETL_Control WHERE NombreProceso = @NombreProceso";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NombreProceso", _processName);
                    object result = cmd.ExecuteScalar();

                    if (result != null && DateTime.TryParse(result.ToString(), out DateTime lastDate))
                    {
                        return lastDate;
                    }
                }
            }

            // Si no hay registro, retornamos una fecha por defecto
            return DateTime.Parse("2025-01-01");
        }

        /// <summary>
        /// Actualiza la fecha de última ejecución al finalizar el ETL.
        /// </summary>
        public static void UpdateLastUpdateDate(DateTime endTime)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                UPDATE ETL_Control
                SET FechaUltimaEjecucion = @Fecha
                WHERE NombreProceso = @NombreProceso";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Fecha", endTime);
                    cmd.Parameters.AddWithValue("@NombreProceso", _processName);
                    int rows = cmd.ExecuteNonQuery();

                    // Si no existe, lo insertamos
                    if (rows == 0)
                    {
                        string insertQuery = "INSERT INTO ETL_Control (NombreProceso, FechaUltimaEjecucion) VALUES (@NombreProceso, @Fecha)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@NombreProceso", _processName);
                            insertCmd.Parameters.AddWithValue("@Fecha", endTime);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
