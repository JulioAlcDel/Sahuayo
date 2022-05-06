USE master;
GO
ALTER DATABASE [SahuayoPrueba] 
SET SINGLE_USER 
WITH ROLLBACK IMMEDIATE;
GO
DROP DATABASE [SahuayoPrueba];
GO
CREATE DATABASE [SahuayoPrueba]

GO
 USE SahuayoPrueba
GO
  CREATE TABLE Persona (
      IdPersona INT IDENTITY(1,1),
	  Nombre  VARCHAR(500),
	  ApellidoMaterno VARCHAR(500),
	  ApellidoPaterno VARCHAR(500),
	  TieneEnfermedad BIT,
	  Descripcion     VARCHAR(900),
	  Sueldo           VARCHAR(300),
	  FechaRegistro DATE,
	  Activo BIT

  )
       ALTER TABLE Persona
         ADD CONSTRAINT [PK_Tb_Personas]
       PRIMARY KEY (IdPersona);

	ALTER TABLE Persona
     ADD CONSTRAINT [DF_Tb_Personas_FechaRegistro]
     DEFAULT (GETDATE()) FOR FechaRegistro;

     ALTER TABLE Persona
       ADD CONSTRAINT [DF_Personas_Activo]
     DEFAULT (1) FOR Activo;
	 GO
	 CREATE PROCEDURE sp_AgregarPersona
	 (
	          @IdPersona INT = 0,
	          @Nombre  VARCHAR(500),
	          @ApellidoMaterno VARCHAR(500),
	          @ApellidoPaterno VARCHAR(500),
	          @TieneEnfermedad BIT,
	          @Descripcion     VARCHAR(900),
	          @Sueldo           VARCHAR(300)
			  
	 )AS
	    IF NOT EXISTS(SELECT IdPersona FROM  Persona WHERE IdPersona = @IdPersona ) BEGIN

		BEGIN TRY
		  INSERT INTO Persona (Nombre,ApellidoMaterno,ApellidoPaterno,TieneEnfermedad,Descripcion,Sueldo) VALUES(@Nombre,@ApellidoMaterno,@ApellidoPaterno,@TieneEnfermedad,@Descripcion,@Sueldo)
		  
		END TRY
		BEGIN CATCH
		
		THROW
		END CATCH
	 END
	 ELSE BEGIN
	   BEGIN TRY
	    UPDATE Persona
		SET Nombre =@Nombre,
		    ApellidoMaterno = @ApellidoMaterno,
		    ApellidoPaterno = @ApellidoPaterno,
			TieneEnfermedad = @TieneEnfermedad,
			Descripcion     =  @Descripcion,
			Sueldo          =  @Sueldo,
			FechaRegistro   =  GETDATE()
	     WHERE  IdPersona = @IdPersona
	   END TRY
	   BEGIN CATCH
		ROLLBACK TRANSACTION
		THROW
		END CATCH
	 END
	 
	 GO
	 CREATE PROCEDURE sp_EliminarPersona(
	          @IdPersona INT
	 )
	 AS 
	 IF  EXISTS(SELECT IdPersona FROM  Persona WHERE IdPersona = @IdPersona ) BEGIN
	 BEGIN TRY
	 DELETE Persona WHERE IdPersona = @IdPersona

     END TRY
	 BEGIN CATCH
		THROW
	END CATCH
	 END
	 GO
	 CREATE PROCEDURE sp_ConsultarPersona
	 (
	       @IdPersona INT =0
	 )AS
	 IF @IdPersona = 0 BEGIN
	    SELECT IdPersona,Nombre,ApellidoPaterno,ApellidoMaterno,TieneEnfermedad,Descripcion,Sueldo,Activo,FechaRegistro FROM Persona
	 END
	 ELSE BEGIN
	    SELECT IdPersona,Nombre,ApellidoPaterno,ApellidoMaterno,TieneEnfermedad,Descripcion,Sueldo,Activo,FechaRegistro FROM Persona WHERE IdPersona = @IdPersona
	 END
GO