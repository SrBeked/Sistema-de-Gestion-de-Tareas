using BusinessLogic;
using DataAccess;
using System;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=Gtareas;User=root;Password=20231886;";
            var taskRepository = new TaskRepository(connectionString);
            var taskService = new TaskService(taskRepository);

            MostrarBienvenida();

            string opcion;

            do
            {
                MostrarMenu();
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CrearTarea(taskService);
                        break;
                    case "2":
                        VerTareas(taskService);
                        break;
                    case "3":
                        MarcarTareaCompletada(taskService);
                        break;
                    case "4":
                        EliminarTarea(taskService);
                        break;
                }

            } while (opcion != "5");

            MostrarDespedida();
        }

        static void CrearTarea(TaskService taskService)
        {
            Console.Write("Descripción de la tarea: ");
            string descripcion = Console.ReadLine();
            taskService.CreateTask(descripcion);
            Console.WriteLine("Tarea creada correctamente. Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void VerTareas(TaskService taskService)
        {
            var tasks = taskService.GetAllTasks();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("     \nID   |   Descripción   |     Estado");
            Console.WriteLine("------------------------------------------");

            foreach (var task in tasks)
            {
                Console.Write($"{task.Id,-4} | ");
                Console.Write($"{task.Descripcion,-20} | ");
                Console.WriteLine(task.Completada ? "Completada" : "Pendiente");
            }
            Console.ResetColor();

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void MarcarTareaCompletada(TaskService taskService)
        {
            Console.Write("ID de la tarea a marcar como completada: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                taskService.MarkTaskAsCompleted(id);
                Console.WriteLine("Tarea marcada como completada. Presiona cualquier tecla para continuar...");
            }
            else
            {
                Console.WriteLine("ID inválido. Presiona cualquier tecla para continuar...");
            }
            Console.ReadKey();
        }

        static void EliminarTarea(TaskService taskService)
        {
            Console.Write("ID de la tarea a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                taskService.DeleteTask(id);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tarea eliminada. Presiona cualquier tecla para continuar...");
                Console.ResetColor();

            }
            else
            {
                Console.WriteLine("ID inválido. Presiona cualquier tecla para continuar...");
            }
            Console.ReadKey();
        }

        static void MostrarBienvenida()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==============================================================");
            Console.WriteLine("||        Bienvenido al Sistema de Gestión de Tareas        ||");
            Console.WriteLine("==============================================================");
            Console.ResetColor();
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=========================================================");
            Console.WriteLine("||              Sistema de Gestión de Tareas           ||");
            Console.WriteLine("=========================================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("======================================");
            Console.WriteLine("||  1. Crear tarea                  ||");
            Console.WriteLine("||  2. Ver tareas                   ||");
            Console.WriteLine("||  3. Marcar tarea como completada ||");
            Console.WriteLine("||  4. Eliminar tarea               ||");
            Console.WriteLine("||  5. Salir                        ||");
            Console.WriteLine("======================================");
            Console.ResetColor();


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("====||Selecciona una opción:");
            Console.ResetColor();
        }

        static void MostrarDespedida()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Gracias por usar el Sistema de Gestión de Tareas.");
            Console.WriteLine("¡Hasta la próxima!");
            Console.ResetColor();
            Console.WriteLine(" Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
