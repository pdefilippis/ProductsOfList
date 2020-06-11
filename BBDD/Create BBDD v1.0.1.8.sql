CREATE TABLE [dbo].[RecuperarClave](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Stamp] [datetime] NULL,
	[Token] [varchar](6) NOT NULL,
 CONSTRAINT [PK_RecuperarClave] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RecuperarClave] ADD  CONSTRAINT [DF_RecuperarClave_Stamp]  DEFAULT (getdate()) FOR [Stamp]
GO

ALTER TABLE [dbo].[RecuperarClave]  WITH CHECK ADD  CONSTRAINT [FK_RecuperarClave_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO