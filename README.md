# Proyectocsharp_grupo6
Proyecto del grupo 6 - Sistema de Triaje NeuroHealth

## Contexto
La clínica NeuroHealth necesita desarrollar un sistema de consola que permita gestionar el ingreso de pacientes en guardia, clasificarlos según la gravedad de su estado (triaje), registrar observaciones médicas y consultar información relevante. El sistema deberá simular un flujo real de atención en emergencias, donde los pacientes no se atienden por orden de llegada, sino por nivel de urgencia.

## Objetivo
Desarrollar una aplicación de consola en C# que permita:
- Registrar pacientes
- Evaluarlos mediante triaje automatizado
- Almacenar observaciones médicas
- Realizar búsquedas
- Calcular indicadores
- Mostrar información organizada

## Requisitos generales
- La aplicación debe desarrollarse en un único archivo .cs
- El código debe organizarse mediante #region
- Se deberán utilizar únicamente estructuras vistas en clase
- No se requiere el uso de estructuras no vistas
- El programa debe incluir validaciones de entrada
- El código debe estar comentado y explicado

## Datos a registrar por paciente
Cada paciente deberá incluir como mínimo:
- DNI (debe ser un valor numérico positivo y no debe repetirse dentro del sistema)
- Apellido y nombre
- Edad
- Motivo de consulta
- Pulso (latidos por minuto)
- Temperatura corporal (°C)
- Presión arterial (texto)
- Saturación de oxígeno (%)
- Nivel de dolor (0 a 10)
- Fecha y hora de ingreso
- Nivel de urgencia (calculado por el sistema)

## Valores permitidos y rangos válidos

### Motivo de consulta
El motivo de consulta deberá seleccionarse exclusivamente entre las siguientes opciones:
1. Dolor torácico
2. Dificultad respiratoria
3. Fiebre
4. Dolor abdominal
5. Traumatismo
6. Pérdida de conocimiento
7. Cefalea
8. Control general

No se permite ingreso libre de texto.

### Nivel de dolor
- Entero entre 0 y 10
- 0 = sin dolor
- 10 = dolor muy intenso

### Pulso
- Entre 30 y 200 lpm

### Temperatura
- Entre 34.0 °C y 42.0 °C

### Saturación de oxígeno
- Entre 70 y 100 %

Todos los valores deben validarse. En caso de error, se debe solicitar nuevamente el ingreso.

## Estructuras del sistema
El sistema deberá trabajar con:
- Cola de espera (orden de llegada)
- Lista de pacientes admitidos
- Pila de observaciones médicas

## Reglas de Triaje
El sistema clasificará automáticamente a cada paciente según:

### Nivel Rojo (Crítico)
- Saturación < 90
- Pulso > 120
- Temperatura ≥ 39.0
- Dolor ≥ 9

### Nivel Amarillo (Solo si no fue Rojo)
- Saturación 90–94
- Pulso 100–120
- Temperatura 38–38.9
- Dolor 6–8

### Nivel Verde
Si no cumple condiciones anteriores.

## Funcionalidades obligatorias
1. Registrar paciente
2. Mostrar cola de espera
3. Evaluar paciente (triaje automático)
4. Registrar observación médica (Solo se podrán registrar observaciones para pacientes previamente evaluados - presentes en la lista de pacientes admitidos)
5. Mostrar observaciones de un paciente (indicando DNI), desde la más reciente a la más antigua
6. Calcular riesgo
7. Buscar paciente por DNI dentro de la lista de pacientes admitidos
8. Listar pacientes admitidos mostrando: DNI, nombre, edad y nivel de urgencia
9. Filtrar por nivel de urgencia
10. Mostrar estadísticas generales:
   - Cantidad total de pacientes atendidos
   - Cantidad por nivel de urgencia
   - Edad promedio
   - Cantidad de pacientes en espera

## Observaciones médicas (uso de pila)
Las observaciones deben almacenarse de forma que:
- La última observación cargada sea la primera en mostrarse

Ejemplo:
1. "Paciente ingresa con dolor"
2. "Se registra fiebre"
3. "Se administra medicación"

Salida esperada:
- Se administra medicación
- Se registra fiebre
- Paciente ingresa con dolor

Organización de datos: Cada paciente puede tener múltiples observaciones. No se aceptarán observaciones sin un paciente asociado (por DNI). El grupo deberá decidir cómo representar esta relación y justificar su diseño.

## Cálculo de puntaje de riesgo
Además de clasificar al paciente mediante triaje, el sistema deberá permitir calcular un puntaje de riesgo a partir de distintos indicadores clínicos. Este puntaje no reemplaza el nivel de urgencia asignado por el triaje, sino que funciona como un valor numérico complementario para practicar el uso de arreglos y recursividad.

Para calcularlo, el sistema deberá utilizar un arreglo de enteros con 4 posiciones:
1. Puntaje por temperatura
2. Puntaje por pulso
3. Puntaje por saturación de oxígeno
4. Puntaje por nivel de dolor

Cada puntaje deberá ser un número entero entre 0 y 10. La suma total de estos valores deberá calcularse mediante una función recursiva.

Por ejemplo, si el arreglo contiene:
- Temperatura = 7
- Pulso = 8
- Saturación = 9
- Dolor = 10

Entonces el arreglo será: [7, 8, 9, 10]
Y el puntaje total de riesgo será: 7 + 8 + 9 + 10 = 34

La función recursiva deberá recorrer el arreglo y devolver la suma total.

### Interpretación del puntaje
El sistema deberá mostrar una interpretación simple:
- 0 a 14 = Riesgo bajo
- 15 a 29 = Riesgo medio
- 30 a 40 = Riesgo alto

### Requisitos obligatorios
- El arreglo debe tener 4 posiciones
- Cada valor debe validarse entre 0 y 10
- La suma debe realizarse con una función recursiva
- No se permite resolver esta suma únicamente con for, foreach o métodos automáticos
- El sistema debe mostrar el puntaje total y su interpretación

## Flujo esperado del sistema
1. Se registra un paciente
2. Se agrega a la cola
3. Se evalúa triaje
4. Pasa a admitidos
5. Se agregan observaciones
6. Se consultan observaciones
7. Se realizan búsquedas
8. Se generan reportes

## Estado de implementación

### FUNCIONALIDADES IMPLEMENTADAS:
1. Registrar paciente - Línea 263-298 - COMPLETO
2. Mostrar cola de espera - Línea 315-330 - COMPLETO
3. Evaluar paciente (triaje automático) - Línea 332-365 - COMPLETO
4. Registrar observación médica - Línea 368-418 - COMPLETO
5. Mostrar observaciones de un paciente - Línea 416-443 - COMPLETO
6. Buscar paciente por DNI - Línea 518-551 - COMPLETO
7. Listar pacientes admitidos - Línea 449-465 - COMPLETO
8. Filtrar por nivel de urgencia - Línea 484-512 - COMPLETO
9. Funciones de validación de entrada - Línea 485-620 - COMPLETO
10. Carga de datos de prueba - Línea 201-257 - COMPLETO

### FUNCIONALIDADES FALTANTES:
7. Calcular puntaje de riesgo recursivo - Línea 613-627 - COMPLETO
10. Mostrar estadísticas generales - Línea 629-636 - COMPLETO

### FUNCIONES AUXILIARES COMPLETADAS:
- MostrarDatosPaciente - Línea 467-482 - COMPLETO
- BuscarLineal - Línea 554-566 - COMPLETO
- BuscarBinariaRecursiva - Línea 568-583 - COMPLETO
- CopiarListaPacientes - Línea 585-594 - COMPLETO
- OrdenarPacientesPorDni - Línea 596-611 - COMPLETO
- SumarPuntajesRecursivo -  COMPLETO

