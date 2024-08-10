using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIstema_Gestion_De_Tareas.Cap._1DataAccess
{
    internal class TaskRepository
    {
    }
}

namespace DataAccess
{
    public class TaskRepository
    {
        private string _connectionString;

        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTask(string description)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Tareas (Descripcion, Completada) VALUES (@Descripcion, @Completada)";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Descripcion", description);
                cmd.Parameters.AddWithValue("@Completada", false);
                cmd.ExecuteNonQuery();
            }
        }

        public List<(int Id, string Descripcion, bool Completada)> GetTasks()
        {
            var tasks = new List<(int Id, string Descripcion, bool Completada)>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Id, Descripcion, Completada FROM Tareas";
                var cmd = new MySqlCommand(query, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add((reader.GetInt32("Id"), reader.GetString("Descripcion"), reader.GetBoolean("Completada")));
                    }
                }
            }

            return tasks;
        }

        public void CompleteTask(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Tareas SET Completada = @Completada WHERE Id = @Id";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Completada", true);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Tareas WHERE Id = @Id";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
