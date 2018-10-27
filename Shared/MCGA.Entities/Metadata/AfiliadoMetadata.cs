﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGA.Entities
{
	[MetadataType(typeof(AfiliadoMetadata))]
	public partial class Afiliado
	{
		public class AfiliadoMetadata
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

			[DisplayName("Sexo")]
			[Required(ErrorMessage = "Obligatorio")]
			public int TipoSexoId { get; set; }

			[DisplayName("Fecha de nacimiento")]
			[Required(ErrorMessage = "Obligatorio")]
			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
			public System.DateTime FechaNacimiento { get; set; }

			[DisplayName("Estado civil")]
			[Required(ErrorMessage = "Obligatorio")]
			public int EstadoCivilId { get; set; }

			[DisplayName("Nº de afiliado")]
			[Required(ErrorMessage = "Obligatorio")]
			public string NumeroAfiliado { get; set; }

			[DisplayName("Plan")]
			[Required(ErrorMessage = "Obligatorio")]
			public int PlanId { get; set; }

			[DisplayName("Habilitado")]
			[Required(ErrorMessage = "Obligatorio")]
			public bool Habilitado { get; set; }
		}
	}
}