ALTER TABLE Lote ADD Creacion DATETIME NOT NULL DEFAULT GETDATE();

ALTER TABLE Lote ADD Actualizacion DATETIME NOT NULL DEFAULT GETDATE();

ALTER TABLE Usuario ADD Creacion DATETIME NOT NULL DEFAULT GETDATE();

ALTER TABLE Usuario ADD UltimoIngreso DATETIME NOT NULL DEFAULT GETDATE();

ALTER TABLE Usuario ADD Mail VARCHAR(100) NULL;

Lote: fechadecreacion - fechadeactualizacion - estado cerrado para cuando se cierre el lote
Articulo: Marca - public virtual ICollection<UserArticle> UserArticles { get; set; } => para la cantidad de usuario interesados.
Usuario: Mail - fechadecreacion - fechaUltimoIngreso