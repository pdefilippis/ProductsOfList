CREATE TABLE dbo.Solicitud
	(
	Id int NOT NULL IDENTITY (1, 1),
	IdUsuario int NOT NULL,
	IdArticulo int NOT NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.Solicitud ADD CONSTRAINT
	PK_Solicitud PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.Solicitud ADD CONSTRAINT
	FK_Solicitud_Articulo FOREIGN KEY
	(
	IdArticulo
	) REFERENCES dbo.Articulo
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE dbo.Solicitud ADD CONSTRAINT
	FK_Solicitud_Usuario FOREIGN KEY
	(
	IdUsuario
	) REFERENCES dbo.Usuario
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO