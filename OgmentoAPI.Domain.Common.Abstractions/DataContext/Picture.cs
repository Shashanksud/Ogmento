
using System.ComponentModel.DataAnnotations;

namespace OgmentoAPI.Domain.Common.Abstractions.DataContext
{
    public class Picture
    {
		[Key]
		public int Id { get; set; }
		public string FileName { get; set; }
		public string MimeType { get; set; }
		public string AltAttribute {  get; set; }
		public string TitleAttribute {  get; set; }
    }
}
