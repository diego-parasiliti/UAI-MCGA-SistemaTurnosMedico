using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCGA.WebSite.Models
{
	public class ControlDatoAdicionalAfiliadoViewModel
	{
		public int ControlId { get; set; }
		public string TipoControl { get; set; }
		public string LabelControl { get; set; }
		public string JsonData { get; set; }
	}
}