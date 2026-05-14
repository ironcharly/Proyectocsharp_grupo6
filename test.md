# Flujo de Testeo - NeuroHealth

## 1. Registrar paciente
- Selecciona opción 1
- Ingresa DNI (ej: 12345678)
- Ingresa nombre y apellido (ej: Juan García)
- Ingresa edad (ej: 35)
- Ingresa motivo de consulta (ej: Dolor de cabeza)
- Ingresa pulso (ej: 75)
- Ingresa temperatura (ej: 36.5)
- Ingresa presión arterial (ej: 120/80)
- Ingresa saturación de oxígeno (ej: 98)
- Ingresa nivel de dolor (1-10, ej: 5)
- Paciente agregado a la cola de espera

## 2. Mostrar cola de espera
- Selecciona opción 2
- Verás la lista de pacientes en espera con sus datos básicos
- Presiona Enter para volver al menú

## 3. Evaluar paciente (triaje automático)
- Selecciona opción 3
- El sistema evalúa automáticamente al primer paciente de la cola
- Asigna nivel de urgencia (Verde/Amarillo/Rojo) según signos vitales
- Paciente pasa a admitidos
- Verás el resultado del triaje

## 4. Registrar observación médica
- Selecciona opción 4
- Ingresa DNI del paciente admitido (ej: 30111222)
- Ingresa una observación (ej: "Paciente responde bien")
- Observación registrada exitosamente
- Ingresa -1 para cancelar y volver al menú

## 5. Mostrar observaciones de un paciente
- Selecciona opción 5
- Ingresa DNI del paciente (ej: 30111222)
- Verás las observaciones de ese paciente (de más reciente a más antigua)
- Ingresa -1 para cancelar y volver al menú

## 6. Buscar paciente por DNI
- Selecciona opción 6
- Ingresa DNI a buscar (ej: 30111222)
- Verás resultados de búsqueda lineal y binaria con cantidad de pasos
- Si se encuentra, se mostrarán los datos completos del paciente
- Ingresa -1 para cancelar y volver al menú

## 7. Listar pacientes admitidos
- Selecciona opción 8
- Verás tabla con DNI, nombre, edad, motivo y nivel de urgencia de todos los pacientes admitidos
- Si no hay pacientes, mostrará mensaje correspondiente

## 8. Filtrar por nivel de urgencia
- Selecciona opción 9
- Ingresa nivel de urgencia (1=Verde, 2=Amarillo, 3=Rojo)
- Verás pacientes admitidos con ese nivel
- Ingresa -1 para cancelar y volver al menú

## 9. Calcular puntaje de riesgo recursivo
- Selecciona opción 7
- Ingresa puntaje por Temperatura (0-10, ej: 8)
- Ingresa puntaje por Pulso (0-10, ej: 6)
- Ingresa puntaje por Saturación (0-10, ej: 3)
- Ingresa puntaje por Dolor (0-10, ej: 7)
- Verás el puntaje total (0-40) y la interpretación (RIESGO BAJO/MODERADO/CRÍTICO)

## 10. Mostrar estadísticas generales
- Selecciona opción 10
- Verás cantidad de pacientes en espera y admitidos
- Verás distribución por nivel de urgencia (Rojo/Amarillo/Verde)
- Verás edad promedio de pacientes admitidos
- Verás porcentaje de pacientes críticos (Rojo)
- Si no hay pacientes admitidos, mostrará mensaje correspondiente

## Casos especiales a probar:
- DNI inválido en búsqueda de paciente
- DNI sin observaciones en mostrar observaciones
- Nivel de urgencia sin pacientes en filtro
- Cancelación con -1 en todas las funciones que lo permitan
- Calcular puntaje con valores extremos (0 o 10 en todos los campos)
- Mostrar estadísticas sin pacientes admitidos