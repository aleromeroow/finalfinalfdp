using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalFDP
{
    internal class Program
    {
       
            // [usuario, contraseña]
            string[,] matrizCajeros = new string[15, 2];
            int totalCajeros = 0;

            // [nombre, cédula]
            string[,] matrizConductores = new string[15, 2];
            int totalConductores = 0;

            // [nombre, carro, horario, cédula conductor]
            string[,] matrizRutas = new string[15, 4];
            int totalRutas = 0;

            static void Main(string[] args)
            {
                Program miPrograma = new Program();
                miPrograma.EjecutarSistema();
            }

            public void EjecutarSistema()
            {
                matrizCajeros[0, 0] = "cajero1";
                matrizCajeros[0, 1] = "1234";
                totalCajeros = 1;

                bool salirSistema = false;

                while (!salirSistema)
                {
                    string[] datosAutenticacion = AutenticarUsuario();
                    bool accesoConcedido = (datosAutenticacion[0] != "");

                    if (accesoConcedido)
                    {
                        string usuarioLogueado = datosAutenticacion[0];
                        string rolLogueado = datosAutenticacion[1];

                        Console.Clear();
                        Console.WriteLine("==================================================");
                        Console.WriteLine(" ¡Bienvenido/a " + usuarioLogueado + "! Rol: " + rolLogueado + " ");
                        Console.WriteLine("==================================================");
                        Console.WriteLine("\nPresiona cualquier tecla para continuar al menú... :D");
                        Console.ReadKey();

                        if (rolLogueado.ToLower() == "administrador")
                        {
                            MenuAdministracion();
                        }
                        else if (rolLogueado.ToLower() == "cajero")
                        {
                            MenuCajero(usuarioLogueado);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nPrograma bloqueado por seguridad. Contacte a soporte técnico. T_T");
                        salirSistema = true;
                    }
                }
            }

            public string[] AutenticarUsuario()
            {
                string[] resultado = new string[2];
                int intentos = 3;

                string adminUser = "admin";
                string adminPass = "admin123";

                while (intentos > 0)
                {
                    Console.Clear();
                    Console.WriteLine("==========================================");
                    Console.WriteLine("       SISTEMA DE ACCESO - TIKETS         ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine("Intentos restantes: " + intentos);
                    Console.Write("Ingrese su usuario: ");
                    string ingresoUser = Console.ReadLine();

                    Console.Write("Ingrese su contraseña: ");
                    string ingresoPass = Console.ReadLine();

                    Console.Write("Ingrese el rol (Administrador / Cajero): ");
                    string ingresoRol = Console.ReadLine().Trim();

                    if (ingresoRol.ToLower() == "administrador" && ingresoUser == adminUser && ingresoPass == adminPass)
                    {
                        resultado[0] = ingresoUser;
                        resultado[1] = "Administrador";
                        return resultado;
                    }
                    else if (ingresoRol.ToLower() == "cajero")
                    {
                        bool cajeroEncontrado = false;
                        for (int i = 0; i < totalCajeros; i++)
                        {
                            if (matrizCajeros[i, 0] == ingresoUser && matrizCajeros[i, 1] == ingresoPass)
                            {
                                cajeroEncontrado = true;
                                break;
                            }
                        }

                        if (cajeroEncontrado)
                        {
                            resultado[0] = ingresoUser;
                            resultado[1] = "Cajero";
                            return resultado;
                        }
                    }

                    intentos--;
                    Console.WriteLine("\n> Datos incorrectos. Verifique usuario, contraseña o rol. O_O");
                    Console.WriteLine("Presione una tecla para intentar de nuevo...");
                    Console.ReadKey();
                }

                resultado[0] = "";
                resultado[1] = "";
                return resultado;
            }

            public void MenuAdministracion()
            {
                bool volverLogin = false;
                string userAdminActual = "admin";
                string passAdminActual = "admin123";

                while (!volverLogin)
                {
                    Console.Clear();
                    Console.WriteLine("==========================================");
                    Console.WriteLine("           MENÚ ADMINISTRACIÓN            ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine("1. Gestión de cuenta administrador");
                    Console.WriteLine("2. Gestión de cajeros");
                    Console.WriteLine("3. Gestión de conductores");
                    Console.WriteLine("4. Salir (Regresa a la pantalla de autenticación)");
                    Console.Write("\nSeleccione una opción: ");
                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            string[] datosAdminActualizados = GestionCuentaAdmin(userAdminActual, passAdminActual);
                            userAdminActual = datosAdminActualizados[0];
                            passAdminActual = datosAdminActualizados[1];
                            break;
                        case "2":
                            GestionCajeros();
                            break;
                        case "3":
                            GestionConductores();
                            break;
                        case "4":
                            volverLogin = true;
                            break;
                        default:
                            Console.WriteLine("\n> Opción no válida. Presione una tecla... ;)");
                            Console.ReadKey();
                            break;
                    }
                }
            }

            public string[] GestionCuentaAdmin(string usuario, string password)
            {
                string[] credenciales = { usuario, password };
                bool salir = false;

                while (!salir)
                {
                    Console.Clear();
                    Console.WriteLine("--- GESTIÓN CUENTA ADMINISTRADOR ---");
                    Console.WriteLine("1. Editar usuario");
                    Console.WriteLine("2. Editar contraseña");
                    Console.WriteLine("3. Salir");
                    Console.Write("Seleccione una opción: ");
                    string op = Console.ReadLine();

                    if (op == "1")
                    {
                        Console.Write("Ingrese el nuevo nombre de usuario: ");
                        credenciales[0] = Console.ReadLine();
                        Console.WriteLine("Usuario actualizado con éxito. ^^");
                        Console.ReadKey();
                    }
                    else if (op == "2")
                    {
                        Console.Write("Ingrese la nueva contraseña: ");
                        credenciales[1] = Console.ReadLine();
                        Console.WriteLine("Contraseña actualizada con éxito. ^^");
                        Console.ReadKey();
                    }
                    else if (op == "3")
                    {
                        salir = true;
                    }
                }
                return credenciales;
            }

            public void GestionCajeros()
            {
                bool salir = false;
                while (!salir)
                {
                    Console.Clear();
                    Console.WriteLine("--- GESTIÓN DE CAJEROS ---");
                    Console.WriteLine("1. Crear cajero");
                    Console.WriteLine("2. Editar cajero");
                    Console.WriteLine("3. Ver lista de cajeros");
                    Console.WriteLine("4. Salir");
                    Console.Write("Seleccione una opción: ");
                    string op = Console.ReadLine();

                    if (op == "1")
                    {
                        if (totalCajeros < 15)
                        {
                            Console.Write("Ingrese el usuario del nuevo cajero: ");
                            matrizCajeros[totalCajeros, 0] = Console.ReadLine();
                            Console.Write("Ingrese la contraseña: ");
                            matrizCajeros[totalCajeros, 1] = Console.ReadLine();
                            totalCajeros++;
                            Console.WriteLine("Cajero creado exitosamente. :P");
                        }
                        else
                        {
                            Console.WriteLine("Límite máximo de 15 cajeros alcanzado. O_O");
                        }
                        Console.ReadKey();
                    }
                    else if (op == "2")
                    {
                        Console.Write("Ingrese el usuario exacto del cajero a editar: ");
                        string buscarUser = Console.ReadLine();
                        bool encontrado = false;
                        for (int i = 0; i < totalCajeros; i++)
                        {
                            if (matrizCajeros[i, 0] == buscarUser)
                            {
                                Console.Write("Nuevo usuario: ");
                                matrizCajeros[i, 0] = Console.ReadLine();
                                Console.Write("Nueva contraseña: ");
                                matrizCajeros[i, 1] = Console.ReadLine();
                                Console.WriteLine("Cajero modificado. ^^");
                                encontrado = true;
                                break;
                            }
                        }
                        if (!encontrado) Console.WriteLine("Cajero no encontrado. T_T");
                        Console.ReadKey();
                    }
                    else if (op == "3")
                    {
                        Console.WriteLine("\n--- LISTA DE CAJEROS ---");
                        for (int i = 0; i < totalCajeros; i++)
                        {
                            Console.WriteLine("Índice " + (i + 1) + " | Usuario: " + matrizCajeros[i, 0]);
                        }
                        Console.ReadKey();
                    }
                    else if (op == "4")
                    {
                        salir = true;
                    }
                }
            }

            public void GestionConductores()
            {
                bool salir = false;
                while (!salir)
                {
                    Console.Clear();
                    Console.WriteLine("--- GESTIÓN DE CONDUCTORES ---");
                    Console.WriteLine("1. Crear conductor");
                    Console.WriteLine("2. Editar conductor");
                    Console.WriteLine("3. Asignar ruta a conductor (Auto, ruta, horario)");
                    Console.WriteLine("4. Ver lista de rutas de un conductor");
                    Console.WriteLine("5. Ver lista de conductores");
                    Console.WriteLine("6. Salir");
                    Console.Write("Seleccione una opción: ");
                    string op = Console.ReadLine();

                    if (op == "1")
                    {
                        if (totalConductores < 15)
                        {
                            Console.Write("Nombre del conductor: ");
                            matrizConductores[totalConductores, 0] = Console.ReadLine();
                            Console.Write("Cédula del conductor: ");
                            matrizConductores[totalConductores, 1] = Console.ReadLine();
                            totalConductores++;
                            Console.WriteLine("Conductor registrado. :D");
                        }
                        else
                        {
                            Console.WriteLine("Límite máximo de 15 conductores alcanzado. O_O");
                        }
                        Console.ReadKey();
                    }
                    else if (op == "2")
                    {
                        Console.Write("Ingrese la cédula del conductor a editar: ");
                        string cedulaBuscar = Console.ReadLine();
                        bool encontrado = false;
                        for (int i = 0; i < totalConductores; i++)
                        {
                            if (matrizConductores[i, 1] == cedulaBuscar)
                            {
                                Console.Write("Nuevo Nombre: ");
                                matrizConductores[i, 0] = Console.ReadLine();
                                Console.Write("Nueva Cédula: ");
                                matrizConductores[i, 1] = Console.ReadLine();
                                Console.WriteLine("Conductor actualizado. ^^");
                                encontrado = true;
                                break;
                            }
                        }
                        if (!encontrado) Console.WriteLine("Conductor no encontrado. T_T");
                        Console.ReadKey();
                    }
                    else if (op == "3")
                    {
                        if (totalRutas < 15)
                        {
                            Console.Write("Nombre/Destino de la ruta: ");
                            matrizRutas[totalRutas, 0] = Console.ReadLine();
                            Console.Write("Automóvil asignado (Placa/Modelo): ");
                            matrizRutas[totalRutas, 1] = Console.ReadLine();
                            Console.Write("Horario: ");
                            matrizRutas[totalRutas, 2] = Console.ReadLine();

                            Console.Write("Cédula del conductor al que se le asigna: ");
                            string cedCond = Console.ReadLine();
                            matrizRutas[totalRutas, 3] = cedCond;

                            totalRutas++;
                            Console.WriteLine("Ruta y detalles asignados correctamente al conductor. :P");
                        }
                        else
                        {
                            Console.WriteLine("Límite máximo de 15 rutas alcanzado. O_O");
                        }
                        Console.ReadKey();
                    }
                    else if (op == "4")
                    {
                        Console.Write("Ingrese la cédula del conductor para ver sus rutas: ");
                        string cedBuscar = Console.ReadLine();
                        Console.WriteLine("\n--- RUTAS DEL CONDUCTOR (" + cedBuscar + ") ---");
                        for (int i = 0; i < totalRutas; i++)
                        {
                            if (matrizRutas[i, 3] == cedBuscar)
                            {
                                Console.WriteLine("Ruta: " + matrizRutas[i, 0] + " | Auto: " + matrizRutas[i, 1] + " | Horario: " + matrizRutas[i, 2]);
                            }
                        }
                        Console.ReadKey();
                    }
                    else if (op == "5")
                    {
                        Console.WriteLine("\n--- LISTA DE CONDUCTORES ---");
                        for (int i = 0; i < totalConductores; i++)
                        {
                            Console.WriteLine((i + 1) + ". Nombre: " + matrizConductores[i, 0] + " | Cédula: " + matrizConductores[i, 1]);
                        }
                        Console.ReadKey();
                    }
                    else if (op == "6")
                    {
                        salir = true;
                    }
                }
            }

            public void MenuCajero(string usuarioActual)
            {
                bool volverLogin = false;

                while (!volverLogin)
                {
                    Console.Clear();
                    Console.WriteLine("==========================================");
                    Console.WriteLine("              MENÚ CAJERO                 ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine("1. Gestión cuenta cajero");
                    Console.WriteLine("2. Gestión de rutas");
                    Console.WriteLine("3. Realizar venta");
                    Console.WriteLine("4. Salir (Regresar al login)");
                    Console.Write("\nSeleccione una opción: ");
                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            GestionCuentaCajero(usuarioActual);
                            break;
                        case "2":
                            GestionRutasCajero();
                            break;
                        case "3":
                            RealizarVentaTiquete();
                            break;
                        case "4":
                            volverLogin = true;
                            break;
                        default:
                            Console.WriteLine("\n> Opción no válida. Presione una tecla... ;)");
                            Console.ReadKey();
                            break;
                    }
                }
            }

            public void GestionCuentaCajero(string usuarioActual)
            {
                bool salir = false;
                while (!salir)
                {
                    Console.Clear();
                    Console.WriteLine("--- GESTIÓN CUENTA CAJERO ---");
                    Console.WriteLine("1. Editar usuario");
                    Console.WriteLine("2. Editar contraseña");
                    Console.WriteLine("3. Salir");
                    Console.Write("Seleccione opción: ");
                    string op = Console.ReadLine();

                    for (int i = 0; i < totalCajeros; i++)
                    {
                        if (matrizCajeros[i, 0] == usuarioActual)
                        {
                            if (op == "1")
                            {
                                Console.Write("Nuevo usuario: ");
                                matrizCajeros[i, 0] = Console.ReadLine();
                                Console.WriteLine("Modificado con éxito. ^^");
                            }
                            else if (op == "2")
                            {
                                Console.Write("Nueva contraseña: ");
                                matrizCajeros[i, 1] = Console.ReadLine();
                                Console.WriteLine("Contraseña actualizada. ^^");
                            }
                            break;
                        }
                    }
                    if (op == "3") salir = true;
                    Console.ReadKey();
                }
            }

            public void GestionRutasCajero()
            {
                bool salir = false;
                while (!salir)
                {
                    Console.Clear();
                    Console.WriteLine("--- GESTIÓN DE RUTAS (CAJERO) ---");
                    Console.WriteLine("1. Crear ruta");
                    Console.WriteLine("2. Ver lista de rutas");
                    Console.WriteLine("3. Asignar conductor a ruta");
                    Console.WriteLine("4. Salir");
                    Console.Write("Seleccione opción: ");
                    string op = Console.ReadLine();

                    if (op == "1" && totalRutas < 15)
                    {
                        Console.Write("Nombre de la ruta: ");
                        matrizRutas[totalRutas, 0] = Console.ReadLine();
                        Console.Write("Automóvil: ");
                        matrizRutas[totalRutas, 1] = Console.ReadLine();
                        Console.Write("Horario: ");
                        matrizRutas[totalRutas, 2] = Console.ReadLine();
                        totalRutas++;
                        Console.WriteLine("Ruta creada. :D");
                        Console.ReadKey();
                    }
                    else if (op == "2")
                    {
                        Console.WriteLine("\n--- LISTA DE RUTAS DISPONIBLES ---");
                        for (int i = 0; i < totalRutas; i++)
                        {
                            Console.WriteLine("[" + (i + 1) + "] Ruta: " + matrizRutas[i, 0] + " | Auto: " + matrizRutas[i, 1] + " | Horario: " + matrizRutas[i, 2] + " | Conductor CC: " + matrizRutas[i, 3]);
                        }
                        Console.ReadKey();
                    }
                    else if (op == "3")
                    {
                        Console.Write("Número de índice de la ruta a la que desea asignar conductor (Ej: 0, 1...): ");
                        if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 0 && idx < totalRutas)
                        {
                            Console.Write("Ingrese la cédula del conductor: ");
                            matrizRutas[idx, 3] = Console.ReadLine();
                            Console.WriteLine("Conductor asignado a la ruta. ^^");
                        }
                        else
                        {
                            Console.WriteLine("Índice no válido. T_T");
                        }
                        Console.ReadKey();
                    }
                    else if (op == "4")
                    {
                        salir = true;
                    }
                }
            }

            public void RealizarVentaTiquete()
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("            PROCESO DE VENTA              ");
                Console.WriteLine("==========================================");

                Console.Write("1. Ingrese el nombre del comprador: ");
                string nombreComprador = Console.ReadLine();

                Console.WriteLine("\n2. Seleccione una ruta disponible:");
                if (totalRutas == 0)
                {
                    Console.WriteLine("No hay rutas registradas actualmente. O_O");
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < totalRutas; i++)
                {
                    Console.WriteLine("[" + (i + 1) + "] Ruta: " + matrizRutas[i, 0] + " - Auto: " + matrizRutas[i, 1] + " - Horario: " + matrizRutas[i, 2]);
                }

                Console.Write("Seleccione el número de la ruta: ");
                if (int.TryParse(Console.ReadLine(), out int seleccionRuta) && seleccionRuta >= 1 && seleccionRuta <= totalRutas)
                {
                    string rutaElegida = matrizRutas[seleccionRuta - 1, 0];
                    string autoElegido = matrizRutas[seleccionRuta - 1, 1];
                    string horarioElegido = matrizRutas[seleccionRuta - 1, 2];

                    Console.Write("3. Ingrese la cantidad de tiquetes: ");
                    int cantidadTiquetes = int.Parse(Console.ReadLine());
                    double precioUnitario = 15000.0;
                    double totalPagar = cantidadTiquetes * precioUnitario;

                    Console.Clear();
                    Console.WriteLine("==========================================");
                    Console.WriteLine("             IMPRESIÓN DE TIQUETE         ");
                    Console.WriteLine("==========================================");
                    Console.WriteLine(" Comprador: " + nombreComprador);
                    Console.WriteLine(" Ruta: " + rutaElegida);
                    Console.WriteLine(" Automóvil: " + autoElegido);
                    Console.WriteLine(" Horario: " + horarioElegido);
                    Console.WriteLine(" Cantidad de tiquetes: " + cantidadTiquetes);
                    Console.WriteLine(" Total Pagado: $" + totalPagar);
                    Console.WriteLine("==========================================");
                    Console.WriteLine(" ¡Gracias por su compra! Buen viaje. ^_^");
                }
                else
                {
                    Console.WriteLine("Selección de ruta inválida. T_T");
                }

                Console.WriteLine("\nPresione cualquier tecla para volver al Menú Cajero...");
                Console.ReadKey();
            }
        }
    }


