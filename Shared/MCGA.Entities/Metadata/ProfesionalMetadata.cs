﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGA.Entities

{
	[MetadataType(typeof(ProfesionalMetadata))]
	public partial class Profesional
	{
		public class ProfesionalMetadata
		{
			[DisplayName("Nombre")]
			[Required(ErrorMessage = "Obligatorio")]
			public string Nombre { get; set; }

			[DisplayName("Apellido")]
			[Required(ErrorMessage = "Obligatorio")]
			public string Apellido { get; set; }

			[DisplayName("Tipo de documento")]
			[Required(ErrorMessage = "Obligatorio")]
			public int TipoDocumentoId { get; set; }

			[DisplayName("Nº de documento")]
			[Required(ErrorMessage = "Obligatorio")]
			public string Numero { get; set; }

			[DisplayName("Dirección")]
			[Required(ErrorMessage = "Obligatorio")]
			public string Direccion { get; set; }

			[DisplayName("Teléfono")]
			[Required(ErrorMessage = "Obligatorio")]
			public string Telefono { get; set; }

			[DisplayName("Email")]
			[Required(ErrorMessage = "Obligatorio")]
			[EmailAddress(ErrorMessage = "El formato es incorrecto.")]
			public string Email { get; set; }

			[DisplayName("Fecha de nacimiento")]
			[Required(ErrorMessage = "Obligatorio")]
			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
			public System.DateTime FechaNacimiento { get; set; }

			[DisplayName("Matrícula")]
			[Required(ErrorMessage = "Obligatorio")]
			public string Matricula { get; set; }
			
		}
	}
}