using System;
using System.Collections.Generic;

namespace NeuroHealth
{
    internal class Program
    {
        /*
         * ============================================================
         * PROYECTO: NeuroHealth - Sistema de Triaje de Emergencias
         * ============================================================
         * Integrantes: Domé Florencia, Micaela Bahurlet, Spinetta Carlos
         * -
         * -
         * -
         * -
         *
         * Explicación general del programa:
         * TODO: explicar brevemente qué hace el sistema.
         *
         * Organización de datos:
         * TODO: explicar cómo organizaron pacientes, cola de espera,
         * pacientes admitidos y observaciones.
         *
         * Justificación de estructuras:
         * TODO: explicar por qué usaron List<T>, Queue<T> y Stack<T>.
         *
         * Algoritmo de triaje:
         * TODO: explicar cómo se asignan los niveles Verde, Amarillo y Rojo.
         *
         * Recursividad:
         * TODO: explicar qué función recursiva implementaron.
         *
         * Búsquedas:
         * TODO: explicar la búsqueda lineal y la búsqueda binaria recursiva.
         */

        #region TIPOS DEL SISTEMA

        // Los motivos de consulta son valores cerrados definidos por la consigna.
        enum MotivoConsulta
        {
            DolorToracico = 1,
            DificultadRespiratoria = 2,
            Fiebre = 3,
            DolorAbdominal = 4,
            Traumatismo = 5,
            PerdidaConocimiento = 6,
            Cefalea = 7,
            ControlGeneral = 8
        }

        // Los niveles de urgencia son valores cerrados definidos por la consigna.
        enum NivelUrgencia
        {
            SinEvaluar = 0,
            Verde = 1,
            Amarillo = 2,
            Rojo = 3
        }

        // Sugerencia de modelado: agrupar signos vitales.
        // El grupo puede adaptar este modelo si lo justifica correctamente.
        struct SignosVitales
        {
            public int Pulso;
            public double Temperatura;
            public string Presion;
            public int Saturacion;
            public int Dolor;
        }

        // Sugerencia de modelado: representar al paciente como un registro de datos.
        // El grupo puede modificar esta representación si lo justifica correctamente.
        record Paciente(
            long Dni,
            string NombreApellido,
            int Edad,
            MotivoConsulta Motivo,
            SignosVitales Signos,
            DateTime FechaIngreso,
            NivelUrgencia Nivel
        );

        // Sugerencia de modelado: observación asociada a un DNI.
        struct Observacion
        {
            public long DniPaciente;
            public string Texto;
            public DateTime Fecha;
        }

        #endregion

        #region ESTRUCTURAS PRINCIPALES

        // TODO: declarar las estructuras principales del sistema.
        // Sugerencias según la consigna:
        // - Cola de espera: Queue<Paciente>
        // - Lista de pacientes admitidos: List<Paciente>
        // - Pila de observaciones: Stack<Observacion>

        // Ejemplo de declaración posible:
        static Queue<Paciente> colaEspera = new Queue<Paciente>();
        static List<Paciente> pacientesAdmitidos = new List<Paciente>();
        static Stack<Observacion> observaciones = new Stack<Observacion>();

        #endregion

        #region PROGRAMA PRINCIPAL

        static void Main(string[] args)
        {
            // TODO: inicializar estructuras si corresponde.
            // TODO: cargar casos de prueba si el grupo decide incluirlos.
            CargarCasosDePrueba();
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();
                int opcion = LeerEntero("Seleccione una opción: ");

                switch (opcion)
                {
                    case 1:
                        RegistrarPaciente();
                        break;
                    case 2:
                        MostrarColaEspera();
                        break;
                    case 3:
                        EvaluarPaciente();
                        break;
                    case 4:
                        RegistrarObservacion();
                        break;
                    case 5:
                        MostrarObservaciones();
                        break;
                    case 6:
                        BuscarPacientePorDni();
                        break;
                    case 7:
                        CalcularPuntajeRiesgo();
                        break;
                    case 8:
                        ListarPacientesAdmitidos();
                        break;
                    case 9:
                        FiltrarPorUrgencia();
                        break;
                    case 10:
                        MostrarEstadisticas();
                        break;
                    case 0:
                        salir = true;
                        Console.WriteLine("Gracias por usar NeuroHealth.");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        #endregion

        #region MENÚ

        static void MostrarMenu()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("     NEUROHEALTH - SISTEMA DE TRIAJE   ");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Registrar paciente");
            Console.WriteLine("2. Mostrar cola de espera");
            Console.WriteLine("3. Evaluar paciente (triaje automático)");
            Console.WriteLine("4. Registrar observación médica");
            Console.WriteLine("5. Mostrar observaciones de un paciente");
            Console.WriteLine("6. Buscar paciente por DNI");
            Console.WriteLine("7. Calcular puntaje de riesgo recursivo");
            Console.WriteLine("8. Listar pacientes admitidos");
            Console.WriteLine("9. Filtrar pacientes por nivel de urgencia");
            Console.WriteLine("10. Mostrar estadísticas generales");
            Console.WriteLine("0. Salir");
            Console.WriteLine("=======================================");
        }

        #endregion

        #region CARGA DE DATOS DE PRUEBA

        static void CargarCasosDePrueba()
        {
            // 1. Pacientes en Cola de Espera (NivelUrgencia.SinEvaluar)
            Paciente p1 = new Paciente(
                11111111,
                "Carlos Gómez",
                45,
                MotivoConsulta.Fiebre,
                new SignosVitales { Pulso = 90, Temperatura = 38.5, Presion = "120/80", Saturacion = 96, Dolor = 4 },
                DateTime.Now.AddMinutes(-30),
                NivelUrgencia.SinEvaluar
            );

            Paciente p2 = new Paciente(
                22222222,
                "María López",
                30,
                MotivoConsulta.DolorAbdominal,
                new SignosVitales { Pulso = 85, Temperatura = 37.0, Presion = "110/70", Saturacion = 98, Dolor = 6 },
                DateTime.Now.AddMinutes(-15),
                NivelUrgencia.SinEvaluar
            );

            colaEspera.Enqueue(p1);
            colaEspera.Enqueue(p2);

            // 2. Pacientes Admitidos (Ya evaluados por el sistema)
            Paciente p3 = new Paciente(
                30111222,
                "Ana Pérez",
                55,
                MotivoConsulta.DolorToracico,
                new SignosVitales { Pulso = 125, Temperatura = 38.7, Presion = "140/90", Saturacion = 89, Dolor = 9 },
                DateTime.Now.AddHours(-2),
                NivelUrgencia.Rojo
            );

            Paciente p4 = new Paciente(
                44444444,
                "Luis Fernández",
                22,
                MotivoConsulta.ControlGeneral,
                new SignosVitales { Pulso = 70, Temperatura = 36.5, Presion = "120/80", Saturacion = 99, Dolor = 0 },
                DateTime.Now.AddHours(-3),
                NivelUrgencia.Verde
            );

            pacientesAdmitidos.Add(p3);
            pacientesAdmitidos.Add(p4);

            // Observaciones del caso de ejemplo de Ana Pérez
            observaciones.Push(new Observacion { DniPaciente = 30111222, Texto = "Paciente con dolor persistente", Fecha = DateTime.Now.AddHours(-1) });
            observaciones.Push(new Observacion { DniPaciente = 30111222, Texto = "Se administra oxígeno", Fecha = DateTime.Now.AddMinutes(-30) });

            // Imprimimos un mensaje silencioso por consola solo para nosotros los mamejores
            Console.WriteLine("[DEV] Datos de prueba inyectados en memoria.");
        }

        #endregion

        #region REGISTRO DE PACIENTES

        static void RegistrarPaciente()
        {
            Console.WriteLine("\n--- REGISTRO DE PACIENTE ---");
            //pedimos el DNI.


            long dni;
            do
            {
                dni = LeerLong("Ingrese DNI: ");
             //validamos que sea positivo y no esté repetido en sistema.
                if (dni <= 0) Console.WriteLine("El DNI debe ser mayor a cero.");
                else if (ExisteDniEnSistema(dni)) Console.WriteLine("Error: El DNI ya se encuentra registrado en el sistema.");
            } while (dni <= 0 || ExisteDniEnSistema(dni));

            // TODO: pedir apellido y nombre.
            string nombre = LeerTextoObligatorio("Ingrese Apellido y Nombre: ");
            // TODO: pedir edad.
            int edad = LeerEnteroEnRango("Ingrese Edad (0-120): ", 0, 120);

            // TODO: pedir motivo de consulta.
            MotivoConsulta motivo = LeerMotivoConsulta();
            // TODO: pedir signos vitales.
            Console.WriteLine("\n-- Signos Vitales --");
            SignosVitales signos = new SignosVitales
            {
                Pulso = LeerEnteroEnRango("Pulso (30-200 lpm): ", 30, 200),
                Temperatura = LeerDoubleEnRango("Temperatura (34.0-42.0 °C): ", 34.0, 42.0),
                Presion = LeerTextoObligatorio("Presión arterial (Ej: 120/80): "),
                Saturacion = LeerEnteroEnRango("Saturación de oxígeno (70-100 %): ", 70, 100),
                Dolor = LeerEnteroEnRango("Nivel de dolor (0-10): ", 0, 10)
            };
            Paciente nuevoPaciente = new Paciente(dni, nombre, edad, motivo, signos, DateTime.Now, NivelUrgencia.SinEvaluar);
            colaEspera.Enqueue(nuevoPaciente);
            Console.WriteLine($"\nPaciente {nombre} agregado a la cola de espera exitosamente.");
        }

        static bool ExisteDniEnSistema(long dni)
        {
            foreach (var p in colaEspera) if (p.Dni == dni) return true;
            foreach (var p in pacientesAdmitidos) if (p.Dni == dni) return true;
            return false;
        }
        

        #endregion

        #region COLA DE ESPERA Y TRIAJE

        static void MostrarColaEspera()
        {
            Console.WriteLine("\n--- COLA DE ESPERA ---");
            if (colaEspera.Count == 0)
            {
                Console.WriteLine("No hay pacientes en espera.");
                return;
            }

            int posicion = 1;
            foreach (var paciente in colaEspera)
            {
                Console.WriteLine($"{posicion}. DNI: {paciente.Dni} - {paciente.NombreApellido} - Motivo: {paciente.Motivo}");
                posicion++;
            }
        }

        static void EvaluarPaciente()
        {
            if (colaEspera.Count == 0)
            {
                Console.WriteLine("No hay pacientes en la cola para evaluar.");
                return;
            }

            Paciente pacienteAEvaluar = colaEspera.Dequeue();
            NivelUrgencia nivelAsignado = ClasificarTriaje(pacienteAEvaluar.Signos);

            // Al ser un record, usamos 'with' para crear una copia con el Nivel actualizado.
            Paciente pacienteAdmitido = pacienteAEvaluar with { Nivel = nivelAsignado };
            pacientesAdmitidos.Add(pacienteAdmitido);

            Console.WriteLine($"\nPaciente {pacienteAdmitido.NombreApellido} evaluado.");
            Console.WriteLine($"Nivel asignado: {pacienteAdmitido.Nivel}");
        }

        static NivelUrgencia ClasificarTriaje(SignosVitales s)
        {
            // Lógica OR: Con que cumpla UNA condición de riesgo, entra en esa categoría.
            if (s.Saturacion < 90 || s.Pulso > 120 || s.Temperatura >= 39.0 || s.Dolor >= 9)
            {
                return NivelUrgencia.Rojo;
            }

            if ((s.Saturacion >= 90 && s.Saturacion <= 94) || (s.Pulso >= 100 && s.Pulso <= 120) || (s.Temperatura >= 38.0 && s.Temperatura <= 38.9) || (s.Dolor >= 6 && s.Dolor <= 8))
            {
                return NivelUrgencia.Amarillo;
            }

            return NivelUrgencia.Verde;
        }

        #endregion

        #region OBSERVACIONES MÉDICAS
        //para probar la opción 4 del menú, DNI válidos 30111222 o 44444444 
        static void RegistrarObservacion()
        {
            Console.WriteLine("\n--- REGISTRAR OBSERVACIÓN MÉDICA ---");
            // pedir DNI del paciente admitido.
            long dni = LeerDniOCancelar("Ingrese DNI del paciente admitido (-1 para volver): ");
            
            // permitir -1 para volver.
            if (dni == -1)
            {
                return;
            }
            
            // validar que el paciente exista en admitidos.
            bool pacienteEncontrado = false;
            string nombrePaciente = "";
            foreach (var p in pacientesAdmitidos)
            {
                if (p.Dni == dni)
                {
                    pacienteEncontrado = true;
                    nombrePaciente = p.NombreApellido;
                    break;
                }
            }
            
            if (!pacienteEncontrado)
            {
                Console.WriteLine("Error: El paciente no está en la lista de admitidos.");
                return;
            }
            
            Console.WriteLine($"Paciente encontrado: {nombrePaciente}");
            
            // pedir texto de observación.
            string texto = LeerTextoObligatorio("Ingrese la observación médica: ");
            
            // agregar observación a la pila.
            Observacion nuevaObservacion = new Observacion
            {
                DniPaciente = dni,
                Texto = texto,
                Fecha = DateTime.Now
            };
            observaciones.Push(nuevaObservacion);
            
            Console.WriteLine("Observación registrada exitosamente.");
        }

        static void MostrarObservaciones()
        {
            Console.WriteLine("\n--- MOSTRAR OBSERVACIONES DE PACIENTE ---");
            // pedir DNI del paciente.
            long dni = LeerDniOCancelar("Ingrese DNI del paciente (-1 para volver): ");
            
            // permitir -1 para volver.
            if (dni == -1)
            {
                return;
            }
            
            // mostrar observaciones desde la más reciente a la más antigua.
            bool hayObservaciones = false;
            foreach (var obs in observaciones)
            {
                if (obs.DniPaciente == dni)
                {
                    Console.WriteLine($"[{obs.Fecha}] {obs.Texto}");
                    hayObservaciones = true;
                }
            }
            
            if (!hayObservaciones)
            {
                Console.WriteLine("No hay observaciones para este paciente.");
            }
        }

        #endregion

        #region LISTADOS Y FILTROS

        static void ListarPacientesAdmitidos()
        {
            Console.WriteLine("\n--- LISTA DE PACIENTES ADMITIDOS ---");
            // mostrar DNI, nombre, edad, motivo y nivel de urgencia.
            if (pacientesAdmitidos.Count == 0)
            {
                Console.WriteLine("No hay pacientes admitidos.");
                return;
            }
            
            Console.WriteLine("DNI\t\tNombre\t\t\tEdad\tMotivo\t\t\tNivel");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (var p in pacientesAdmitidos)
            {
                Console.WriteLine($"{p.Dni}\t{p.NombreApellido}\t{p.Edad}\t{p.Motivo}\t\t{p.Nivel}");
            }
        }

        static void MostrarDatosPaciente(Paciente paciente)
        {
            // mostrar los datos de un paciente de manera clara.
            Console.WriteLine("\n--- DATOS DEL PACIENTE ---");
            Console.WriteLine($"DNI: {paciente.Dni}");
            Console.WriteLine($"Nombre: {paciente.NombreApellido}");
            Console.WriteLine($"Edad: {paciente.Edad}");
            Console.WriteLine($"Motivo de consulta: {paciente.Motivo}");
            Console.WriteLine($"Nivel de urgencia: {paciente.Nivel}");
            Console.WriteLine($"Fecha de ingreso: {paciente.FechaIngreso}");
            Console.WriteLine($"Pulso: {paciente.Signos.Pulso} lpm");
            Console.WriteLine($"Temperatura: {paciente.Signos.Temperatura} °C");
            Console.WriteLine($"Presión arterial: {paciente.Signos.Presion}");
            Console.WriteLine($"Saturación de oxígeno: {paciente.Signos.Saturacion} %");
            Console.WriteLine($"Nivel de dolor: {paciente.Signos.Dolor}");
        }

        static void FiltrarPorUrgencia()
        {
            Console.WriteLine("\n--- FILTRAR POR NIVEL DE URGENCIA ---");
            // pedir nivel de urgencia.
            NivelUrgencia nivel = LeerNivelUrgencia();
            
            // permitir -1 para volver.
            if (nivel == NivelUrgencia.SinEvaluar)
            {
                return;
            }
            
            // mostrar pacientes admitidos que coincidan con el nivel seleccionado.
            Console.WriteLine($"\n--- PACIENTES CON NIVEL {nivel} ---");
            bool hayPacientes = false;
            foreach (var p in pacientesAdmitidos)
            {
                if (p.Nivel == nivel)
                {
                    Console.WriteLine($"DNI: {p.Dni} - Nombre: {p.NombreApellido} - Edad: {p.Edad} - Nivel: {p.Nivel}");
                    hayPacientes = true;
                }
            }
            
            if (!hayPacientes)
            {
                Console.WriteLine("No hay pacientes con ese nivel de urgencia.");
            }
        }

        #endregion

        #region BÚSQUEDAS

        // aqui llamamos funciones auxiliares
        static void BuscarPacientePorDni()
        {
            Console.WriteLine("\n--- BUSCAR PACIENTE POR DNI ---");
            // pedir DNI a buscar.
            long dni = LeerDniOCancelar("Ingrese DNI a buscar (-1 para volver): ");
            
            // permitir -1 para volver.
            if (dni == -1)
            {
                return;
            }
            
            // buscar en pacientes admitidos con búsqueda lineal, recorre uno por unon
            int pasosLineal = 0;
            int indiceLineal = BuscarLineal(dni, ref pasosLineal);
            
            // ordenar una copia por DNI.
            List<Paciente> copiaOrdenada = CopiarListaPacientes();
            OrdenarPacientesPorDni(copiaOrdenada);
            
            // buscar con búsqueda binaria recursiva, divide por la mitad
            int pasosBinaria = 0;
            //para que busque en la lista ordenada de paccientes, divide a la mitad hasta encontrar el paciente o ver que no existe. LOGICA: si el DNI buscado es menor al del medio, llama a la funncion con la mitad izquierda, si es mayor con la derecha, esto lo repetimos hasta encontrarlo o nno.
            int indiceBinaria = BuscarBinariaRecursiva(copiaOrdenada, dni, 0, copiaOrdenada.Count - 1, ref pasosBinaria);
            
            // mostrar cantidad de pasos de cada búsqueda, asi muestra comparaciones de lo que se hizo hasta encontrar. 
            Console.WriteLine($"\nResultados de búsqueda para DNI {dni}:");
            Console.WriteLine($"Búsqueda lineal: {pasosLineal} pasos - Encontrado: {(indiceLineal != -1 ? "Sí" : "No")}");
            Console.WriteLine($"Búsqueda binaria: {pasosBinaria} pasos - Encontrado: {(indiceBinaria != -1 ? "Sí" : "No")}");
            
            if (indiceLineal != -1)
            {
                MostrarDatosPaciente(pacientesAdmitidos[indiceLineal]);
            }
        }

        static int BuscarLineal(long dniBuscado, ref int pasos)
        {
            // implementar búsqueda lineal en la lista de pacientes admitidos.
            //para que recorra 1 por uno desde principio a fin, si lo encuentra devuelve su posición
            for (int i = 0; i < pacientesAdmitidos.Count; i++)
            {
                pasos++;
                if (pacientesAdmitidos[i].Dni == dniBuscado)
                {
                    return i; //para que devuelva la posición de donde lo encontro 
                }
            }
            return -1;
        }
        static int BuscarBinariaRecursiva(List<Paciente> listaOrdenada, long dniBuscado, int inicio, int fin, ref int pasos)
        {
            // implementar búsqueda binaria recursiva.
            if (inicio > fin)
            {
                return -1;
            }
            
            pasos++;
            int medio = (inicio + fin) / 2;
            //si el DNI uscado es IGUAL al del medio, lo encontró, si es menor busca en la mitad izquierda o si es mayor en la derecha. Repite hasta encontrarlo o agotar la lista
            if (listaOrdenada[medio].Dni == dniBuscado)
            {
                return medio;
            }
            else if (listaOrdenada[medio].Dni > dniBuscado)
            {
                return BuscarBinariaRecursiva(listaOrdenada, dniBuscado, inicio, medio - 1, ref pasos);
            }
            else
            {
                return BuscarBinariaRecursiva(listaOrdenada, dniBuscado, medio + 1, fin, ref pasos);
            }
        }

        static List<Paciente> CopiarListaPacientes()
        {
            // copiar manualmente la lista de pacientes admitidos.
            List<Paciente> copia = new List<Paciente>();
            foreach (var p in pacientesAdmitidos)
            {
                copia.Add(p); //para que agregue cada paciente a la nueva lsita
            }
            return copia;
        }

        static void OrdenarPacientesPorDni(List<Paciente> lista)
        {
            // ordenar por DNI con burbuja simple.
            for (int i = 0; i < lista.Count - 1; i++) //recorre toda la lista
            {
                for (int j = 0; j < lista.Count - 1 - i; j++) //para que compare elementos 
                {
                    if (lista[j].Dni > lista[j + 1].Dni) //para que si el DNI actual es mayor al siguiente, los intercambia
                    {
                        Paciente temp = lista[j];
                        lista[j] = lista[j + 1];
                        lista[j + 1] = temp;
                    }
                }
            }
        }

        #endregion

        #region RECURSIVIDAD
        //CHEQUEAR LO HIZO FLOR
        static void CalcularPuntajeRiesgo()
        {
        
            Console.WriteLine("\n--- CÁLCULO DE PUNTAJE DE RIESGO RECURSIVO ---");
            int[] puntajes = new int[4];

            // Cargamos puntajes (0-10) basados en la gravedad percibida
            puntajes[0] = LeerEnteroEnRango("Puntaje por Temperatura (0-10): ", 0, 10);
            puntajes[1] = LeerEnteroEnRango("Puntaje por Pulso (0-10): ", 0, 10);
            puntajes[2] = LeerEnteroEnRango("Puntaje por Saturación (0-10): ", 0, 10);
            puntajes[3] = LeerEnteroEnRango("Puntaje por Dolor (0-10): ", 0, 10);

            // Llamada inicial: empezamos desde el índice 0
            int total = SumarPuntajesRecursivo(puntajes, 0);

            Console.WriteLine($"\nEl puntaje de riesgo total es: {total} / 40");

            if (total >= 30) Console.WriteLine("Interpretación: RIESGO CRÍTICO.");
            else if (total >= 15) Console.WriteLine("Interpretación: RIESGO MODERADO.");
            else Console.WriteLine("Interpretación: RIESGO BAJO.");
        }

        static int SumarPuntajesRecursivo(int[] puntajes, int indice)
        {
            // Caso base: si el índice llega al final del arreglo, retornamos 0
            if (indice == puntajes.Length)
            {
                return 0;
            }
            // Caso recursivo: sumamos el valor actual + la suma del resto del arreglo
            return puntajes[indice] + SumarPuntajesRecursivo(puntajes, indice + 1);
        }

      
        #endregion

        //CHEQUEAR LO HIZO FLOR

        #region ESTADÍSTICAS

     
        static void MostrarEstadisticas()
        {
            Console.WriteLine("\n--- ESTADÍSTICAS GENERALES ---");
            Console.WriteLine($"Pacientes en espera: {colaEspera.Count}");
            Console.WriteLine($"Pacientes admitidos: {pacientesAdmitidos.Count}");

            if (pacientesAdmitidos.Count > 0)
            {
                int rojos = 0, amarillos = 0, verdes = 0;
                double sumaEdades = 0;

                foreach (var p in pacientesAdmitidos)
                {
                    sumaEdades += p.Edad;
                    if (p.Nivel == NivelUrgencia.Rojo) rojos++;
                    else if (p.Nivel == NivelUrgencia.Amarillo) amarillos++;
                    else verdes++;
                }

                double promedioEdad = sumaEdades / pacientesAdmitidos.Count;
                double porcentajeCriticos = (double)rojos / pacientesAdmitidos.Count * 100;

                Console.WriteLine($"- Distribución por Urgencia: Rojo ({rojos}), Amarillo ({amarillos}), Verde ({verdes})");
                Console.WriteLine($"- Edad promedio: {promedioEdad:F1} años");
                Console.WriteLine($"- Porcentaje de pacientes críticos (Rojo): {porcentajeCriticos:F1}%");
            }
            else
            {
                Console.WriteLine("No hay datos suficientes en admitidos para calcular promedios.");
            }
        }

       
#endregion

        #region FUNCIONES DE LECTURA Y VALIDACIÓN
        // Vamos a crear funciones para que cuando los llamemos más arriba tengamos todo validado.
        static int LeerEntero(string mensaje) 
        {
            int resultado; 
            //Creamos una variable resultado para almacenar el valor resultante de la comprobación de abajo. 
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out resultado)) //si el mensaje no se puede convertir a entero, 
            {
                Console.Write("Entrada inválida. Ingrese un número entero: ");
            }
            return resultado;
        }


        static long LeerLong(string mensaje)
        {
            long resultado;
            Console.Write(mensaje);
            while (!long.TryParse(Console.ReadLine(), out resultado))
            {
                Console.Write("Entrada inválida. Ingrese un número válido: ");
            }
            return resultado;
        }

        static double LeerDouble(string mensaje)
        {
            double resultado;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out resultado))
            {
                Console.Write("Entrada inválida. Ingrese un número decimal: ");
            }
            return resultado;
        }

        static string LeerTextoObligatorio(string mensaje)
        {
            string texto;
            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(texto))
                {
                    Console.WriteLine("Este campo no puede estar vacío.");
                }
            } while (string.IsNullOrEmpty(texto));
            return texto;
        }


        static int LeerEnteroEnRango(string mensaje, int minimo, int maximo)
        {
            int valor;
            do
            {
                valor = LeerEntero(mensaje);
                if (valor < minimo || valor > maximo)
                {
                    Console.WriteLine($"Error: El valor debe estar entre {minimo} y {maximo}.");
                }
            } while (valor < minimo || valor > maximo);
            return valor;
        }


        static double LeerDoubleEnRango(string mensaje, double minimo, double maximo)
        {
            double valor;
            do
            {
                valor = LeerDouble(mensaje);
                if (valor < minimo || valor > maximo)
                {
                    Console.WriteLine($"Error: El valor debe estar entre {minimo} y {maximo}.");
                }
            } while (valor < minimo || valor > maximo);
            return valor;
        }



        static long LeerDniOCancelar(string mensaje)
        {
            long dni;
            do
            {
                dni = LeerLong(mensaje);
                if (dni <= 0 && dni != -1)
                {
                    Console.WriteLine("El DNI debe ser positivo (o ingrese -1 para cancelar).");
                }
            } while (dni <= 0 && dni != -1);
            return dni;
        }



        static int LeerEnteroEnRangoOCancelar(string mensaje, int minimo, int maximo)
        {
            int valor;
            do
            {
                valor = LeerEntero(mensaje);
                if ((valor < minimo || valor > maximo) && valor != -1)
                {
                    Console.WriteLine($"Error: El valor debe estar entre {minimo} y {maximo} (o -1 para cancelar).");
                }
            } while ((valor < minimo || valor > maximo) && valor != -1);
            return valor;
        }



        static MotivoConsulta LeerMotivoConsulta()
        {
            Console.WriteLine("Motivos de consulta:");
            for (int i = 1; i <= 8; i++)// TODO: Humanizar esto separar en lineas
            {
                Console.WriteLine($"{i}. {(MotivoConsulta)i}");
            }
            int opcion = LeerEnteroEnRango("Seleccione el motivo de consulta (1-8): ", 1, 8);
            return (MotivoConsulta)opcion;
        }

        static NivelUrgencia LeerNivelUrgencia()
        {
            Console.WriteLine("Niveles de Urgencia:");
            Console.WriteLine("1. Verde");
            Console.WriteLine("2. Amarillo");
            Console.WriteLine("3. Rojo");

            int opcion = LeerEnteroEnRangoOCancelar("Seleccione nivel (1-3) o -1 para cancelar: ", 1, 3);
            if (opcion == -1) return NivelUrgencia.SinEvaluar; // TODO: Humanizar 2.0
            return (NivelUrgencia)opcion;
        }
        #endregion
    }
}
