

/*	SACAR LAS EXISTENCIAS, FECHA DE CADUCIDAD Y TIPO DE MEDICAMENTO DE TODAS LAS AMPICILINAS QUE NO SEAN INYECTABLES */

SELECT EXISTENCIAS, FECHACADUCIDAD, TIPO
FROM NOMMEDICAMENTO INNER JOIN (MEDICAMENTO INNER JOIN TIPOMED ON TIPOMED.CLVTIPO=MEDICAMENTO.CLVTIPO)
ON MEDICAMENTO.CLVNOMMEDICAMENTO=NOMMEDICAMENTO.CLVNOMMEDICAMENTO
WHERE NOMMEDICAMENTO='AMPICILINA' AND NOT(TIPO='INYECCI�N')


/*SACAR EL NOMBRE, TIPO Y CONTENIDO DE TODOS LOS MEDICAMENTOS QUE TENGAN M�S DE 5 EXISTENCIAS Y QUE SE CADUQUEN ANTES DEL 2023*/

SELECT NOMMEDICAMENTO, TIPO, CONTENIDO
FROM NOMMEDICAMENTO INNER JOIN (MEDICAMENTO INNER JOIN TIPOMED ON TIPOMED.CLVTIPO=MEDICAMENTO.CLVTIPO)
ON MEDICAMENTO.CLVNOMMEDICAMENTO=NOMMEDICAMENTO.CLVNOMMEDICAMENTO
WHERE EXISTENCIAS>5 AND FECHACADUCIDAD<'01/01/2023'

/*SACAR EL NOMBRE DE TODOS LOS PACIENTES DE LA DOCTORA 'CRISTINA FLORES PAYAN' QUE PROVENGAN DE JIQUILPAN, 
AS� COMO LAS DESCRIPCIONES DE LOS S�NTOMAS DE LOS RESPECTIVOS PACIENTES */

SELECT NOMPACIENTE, DESCPACIENTE
FROM DOCTOR INNER JOIN (PACIENTEDOCTOR INNER JOIN (PACIENTE INNER JOIN DESCRIPPACIENTE ON DESCRIPPACIENTE.CLVPACIENTE=PACIENTE.CLVPACIENTE)
ON PACIENTE.CLVPACIENTE=PACIENTEDOCTOR.CLVPACIENTE) ON PACIENTEDOCTOR.CLVDOCTOR=DOCTOR.CLVDOCTOR
WHERE NOMDOCTOR='CRISTINA FLORES PAYAN' AND PROCEDENCIA='JIQUILPAN'

/*SACAR EL NOMBRE DEL DOCTOR Y LOS NOMBRES DE LOS PACIENTES QUE HA ATENDIDO, PERO S�LO DE AQUELLOS QUE TRABAJEN DE D�A O DE NOCHE*/
SELECT NOMDOCTOR, NOMPACIENTE
FROM TURNODOC INNER JOIN (DOCTOR INNER JOIN (PACIENTEDOCTOR INNER JOIN PACIENTE ON PACIENTE.CLVPACIENTE=PACIENTEDOCTOR.CLVPACIENTE)
ON PACIENTEDOCTOR.CLVDOCTOR=DOCTOR.CLVDOCTOR) ON DOCTOR.CLVTURNO=TURNODOC.CLVTURNO
WHERE TURNO='MATUTINO' OR TURNO='NOCTURNO'


/*SACAR EL NOMBRE DEL DOCTOR Y LA FECHA DE CU�NDO FUE CREADA LA RECETA M�DICA, AS� COMO LOS NOMBRES DE LOS MEDICAMENTOS QUE RECET� 
DE LAS RECETAS M�DICAS CREADAS DESPU�S DE OCTUBRE Y QUE NO SE HAYA RECETADO NING�N PARACETAMOL*/

SELECT NOMDOCTOR, FECHAELAB, NOMMEDICAMENTO
FROM DOCTOR INNER JOIN (RECETA INNER JOIN (MEDICRECETAS INNER JOIN (MEDICAMENTO INNER JOIN NOMMEDICAMENTO
ON NOMMEDICAMENTO.CLVNOMMEDICAMENTO=MEDICAMENTO.CLVNOMMEDICAMENTO) ON MEDICAMENTO.CLVMEDICAMENTO=MEDICRECETAS.CLVMEDICAMENTO)
ON MEDICRECETAS.CLVRECETA=RECETA.CLVRECETA) ON RECETA.CLVDOCTOR=DOCTOR.CLVDOCTOR
WHERE FECHAELAB>'10/31/2020' AND NOT(NOMMEDICAMENTO='PARACETAMOL')


/*SACAR EL NOMBRE DEL PACIENTE, CON SU DESCRIPCI�N DE S�NTOMAS, LA FECHA DE ELABORACI�N DE LA RECETA M�DICA 
Y EL NOMBRE DEL MEDICAMENTO A CONSUMIR CON SU DESCRIPCI�N DE RECETA Y EL CONTENIDO DEL MEDICAMENTO
DE TODOS LOS PACIENTES QUE PROVENGAN DE JIQUILPAN Y QUE EN LA DESCRIPCI�N DE LA RECETA SE ESPEC�FIQUE 'TOMAR UNA CAPSULA'*/

SELECT NOMPACIENTE, DESCPACIENTE, FECHAELAB, NOMMEDICAMENTO, DESCRECETA, CONTENIDO
FROM DESCRIPPACIENTE INNER JOIN (PACIENTE INNER JOIN (RECETA INNER JOIN (MEDICRECETAS INNER JOIN (MEDICAMENTO INNER JOIN NOMMEDICAMENTO
ON NOMMEDICAMENTO.CLVNOMMEDICAMENTO=MEDICAMENTO.CLVNOMMEDICAMENTO) ON MEDICAMENTO.CLVMEDICAMENTO=MEDICRECETAS.CLVMEDICAMENTO)
ON MEDICRECETAS.CLVRECETA=RECETA.CLVRECETA) ON RECETA.CLVPACIENTE=PACIENTE.CLVPACIENTE) ON PACIENTE.CLVPACIENTE=DESCRIPPACIENTE.CLVPACIENTE
WHERE PROCEDENCIA='JIQUILPAN' AND DESCRECETA LIKE'%TOMAR UNA CAPSULA%'

/*SACAR EL NOMBRE DE LOS DOCTORES QUE TRABAJEN EN EL TURNO MATUTINO O VESPERTINO 
Y QUE SU NOMBRE COMIENCE CON 'HU' O QUE EN CUALQUIER PARTE DE SU NOMBRE TENGAN 'PE' */

SELECT NOMDOCTOR
FROM DOCTOR
WHERE NOMDOCTOR LIKE'HU%' OR NOMDOCTOR LIKE'%PE%' AND CLVTURNO IN(SELECT CLVTURNO
																	FROM TURNODOC
																	WHERE TURNO='MATUTINO' OR TURNO='VESPERTINO')



/*SACAR TODOS LOS MEDICAMENTOS QUE NO SEAN PARACETAMOL NI IBUPROFENO QUE TENGAN M�S DE 25 EXISTENCIAS Y QUE SU CONTENIDO SEA DE 500 MG*/
SELECT  NOMMEDICAMENTO
FROM NOMMEDICAMENTO
WHERE NOT(NOMMEDICAMENTO='PARACETAMOL' OR NOMMEDICAMENTO='IBUPROFENO') AND CLVNOMMEDICAMENTO IN(SELECT CLVNOMMEDICAMENTO
																								FROM MEDICAMENTO
																								WHERE EXISTENCIAS>25 AND CONTENIDO='500 mg')

/*SACAR EL NOMBRE DE LOS PACIENTES Y SU PROCEDENCIA DE AQUELLOS QUE PROVENGAN DE SAHUAYO O DE UNA CIUDAD QUE COMIENCE CON 'J'
Y QUE LOS PACIENTES TENGAN GASTRITIS O FIEBRE, 
ADEM�S, SACAR EL D�A DE ELABORACI�N DE SU RECETA M�DICA, QUE SEA DESPU�S DE OCTUBRE PERO ANTES DE DICIEMBRE*/

SELECT NOMPACIENTE, PROCEDENCIA
FROM PACIENTE
WHERE PROCEDENCIA='SAHUAYO' OR PROCEDENCIA LIKE'J%' AND CLVPACIENTE IN(SELECT CLVPACIENTE
																		FROM RECETA
																		WHERE FECHAELAB>='10/01/2020' AND FECHAELAB<'12/01/2020')
													AND CLVPACIENTE IN(SELECT CLVPACIENTE
																		FROM DESCRIPPACIENTE
																		WHERE DESCPACIENTE='FIEBRE' OR DESCPACIENTE='GASTRITIS')


/*SACAR  EL CONTENIDO Y LA FECHA DE CADUCIDAD DE AQUELLOS MEDICAMENTOS QUE SEAN RAMIPIRIL O LANSOPRAZOL, QUE SEAN COMPRIMIDOS O L�QUIDO ORAL, 
QUE, ADEM�S, SUS EXISTENCIAS SEAN MAYOR A 5 Y SE CADUQUEN ANTES DEL 2024*/

SELECT CONTENIDO, FECHACADUCIDAD
FROM MEDICAMENTO
WHERE EXISTENCIAS>5 AND FECHACADUCIDAD<'01/01/2024' AND CLVTIPO IN(SELECT CLVTIPO
																	FROM TIPOMED
																	WHERE TIPO='COMPRIMIDO' OR TIPO='LIQUIDO ORAL')
													AND CLVNOMMEDICAMENTO IN(SELECT CLVNOMMEDICAMENTO
																			FROM NOMMEDICAMENTO
																			WHERE NOMMEDICAMENTO='RAMIPIRIL' OR NOMMEDICAMENTO='LANSOPRAZOL')

