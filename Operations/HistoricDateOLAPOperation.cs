using System;
using System.Data.SqlClient;
using APPCORE.BDCore.Abstracts;
using BusinessLogic.Connection;

namespace Operations
{
    public class HistoricDateOLAPOperation
    {
        private static readonly WDataMapper? _dataMapper = new BDConnection().DBDestino;
        private static readonly string _connectionString = _dataMapper.GDatos.ConexionString;
        private const string _processName = "CargaGeneralDW";

        // Obtiene la última fecha en que se ejecutó el proceso ETL.
        public static DateTime GetLastUpdatedate()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT TOP 1 FechaFin 
                    FROM ETL_Historial_Cargas
                    WHERE Proceso = @Proceso
                    ORDER BY FechaFin DESC";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Proceso", _processName);
                    object result = cmd.ExecuteScalar();

                    if (result != null && DateTime.TryParse(result.ToString(), out DateTime lastDate))
                        return lastDate;
                }
            }

            // Si no hay registro, retornamos una fecha por defecto
            return DateTime.Parse("2025-01-01");
        }

        // Inserta un nuevo registro de ejecución ETL en la tabla de historial.
        // Mantiene el historial completo de todas las ejecuciones.
        public static void UpdateLastUpdateDate(DateTime startTime, DateTime endTime, int registeredRows)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = @"
        INSERT INTO ETL_Historial_Cargas
            (Proceso, FechaInicio, FechaFin, RegistrosProcesados, Estado)
        VALUES
            (@Proceso, @FechaInicio, @FechaFin, @RegistrosProcesados, @Estado)";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                {
                    insertCmd.Parameters.AddWithValue("@Proceso", _processName);
                    insertCmd.Parameters.AddWithValue("@FechaInicio", startTime);
                    insertCmd.Parameters.AddWithValue("@FechaFin", endTime);
                    insertCmd.Parameters.AddWithValue("@RegistrosProcesados", registeredRows);
                    insertCmd.Parameters.AddWithValue("@Estado", "Completado");

                    insertCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
