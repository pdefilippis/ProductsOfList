ALTER TABLE Articulo ALTER COLUMN UsuarioAdjudicado INT NULL;

GO
ALTER TABLE dbo.Articulo ADD CONSTRAINT
	FK_Articulo_Usuario FOREIGN KEY
	(
	UsuarioAdjudicado
	) REFERENCES dbo.Usuario
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	