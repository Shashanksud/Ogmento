
namespace OgmentoAPI.Domain.Common.Abstractions.DataContext
{
    public class PictureBinary
    {
		public int Id { get; set; }
		public int PictureId { get; set; }
		public byte[] BinaryData { get; set; }
		public string? Hash	{ get; set; }
    }
}
