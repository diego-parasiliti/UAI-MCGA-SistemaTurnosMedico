CREATE TABLE TipoControl
(
	Id INT IDENTITY(1,1) NOT NULL,
	Descripcion NVARCHAR(200) NOT NULL,
	NombreInterno NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_TIPOCONTROL PRIMARY KEY (ID)
);
GO
INSERT INTO TipoControl (Descripcion, NombreInterno) VALUES ('Campo de texto', 'TextBox');
GO
INSERT INTO TipoControl (Descripcion, NombreInterno) VALUES ('Casilla de verificación', 'CheckBox');
GO
INSERT INTO TipoControl (Descripcion, NombreInterno) VALUES ('Lista de opciones', 'DropDownList');
GO
INSERT INTO TipoControl (Descripcion, NombreInterno) VALUES ('Campo de fecha', 'Fecha');
GO
CREATE TABLE TipoKey
(
	Id INT IDENTITY(1,1) NOT NULL,
	Descripcion NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_TIPOKEY PRIMARY KEY (ID)
);
GO
INSERT INTO TipoKey (Descripcion) VALUES ('Estudios');
GO
CREATE TABLE DetalleTipoKey
(
	Id INT IDENTITY(1,1) NOT NULL,
	TipoKeyId INT NOT NULL,
	TipoControlId INT NOT NULL,
	Nombre NVARCHAR(200) NOT NULL,
	JsonData NVARCHAR(MAX) NULL,
	CONSTRAINT PK_DETALLETIPOKEY PRIMARY KEY (ID),
	CONSTRAINT FK_DETALLETIPOKEY_TIPOKEY FOREIGN KEY (TipoKeyId) REFERENCES TipoKey(ID),
	CONSTRAINT FK_DETALLETIPOKEY_TIPOCONTROL FOREIGN KEY (TipoControlId) REFERENCES TipoControl(ID)
);
GO
INSERT INTO DetalleTipoKey (TipoKeyId, TipoControlId, Nombre) VALUES (1, 1, 'Nombre');
GO
INSERT INTO DetalleTipoKey (TipoKeyId, TipoControlId, Nombre) VALUES (1, 1, 'Resultado');
GO
CREATE TABLE DatoAdicionalAfiliado
(
	Id INT IDENTITY(1,1) NOT NULL,
	Fecha DATETIME NOT NULL,
	AfiliadoId INT NOT NULL,
	TipoKeyId INT NOT NULL,
	JsonData NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_DATOADICIONALAFILIADO PRIMARY KEY (ID),
	CONSTRAINT FK_DATOADICIONALAFILIADO_AFILIADO FOREIGN KEY (AfiliadoId) REFERENCES Afiliado(ID),
	CONSTRAINT FK_DATOADICIONALAFILIADO_TIPOKEY FOREIGN KEY (TipoKeyId) REFERENCES TipoKey(ID)
);



TipoKeyId
JsonData