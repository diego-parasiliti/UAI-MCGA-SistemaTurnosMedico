using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGA.Entities
{
	[MetadataType(typeof(EspecialidadMetadata))]
	public partial class Especialidad
	{
		public class EspecialidadMetadata
		{
			[DisplayName("Descripción")]
			[Required(ErrorMessage = "Obligatorio")]
			public string descripcion { get; set; }

			[DisplayName("Frecuencia")]
			[Required(ErrorMessage = "Obligatorio")]
			public int frecuencia { get; set; }

			[DisplayName("Tipo de especialidad")]
			[Required(ErrorMessage = "Obligatorio")]
			public int TipoEspecialidadId { get; set; }

		}
	}
}
