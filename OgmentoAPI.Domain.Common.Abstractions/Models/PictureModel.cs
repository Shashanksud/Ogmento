using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Common.Abstractions.Models
{
	public class PictureModel
	{
		public int Id { get; set; }
		public string FileName { get; set; }
		public string MimeType { get; set; }
		public byte[] BinaryData { get; set; }
		public string? Hash { get; set; }
		public bool ToBeDeleted { get; set; } = false;
		public bool IsNew { get; set; } = false;
	}
}
