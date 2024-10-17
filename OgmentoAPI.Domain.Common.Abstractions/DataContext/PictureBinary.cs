
namespace OgmentoAPI.Domain.Common.Abstractions.DataContext
{
    public class PictureBinary
    {
		public int PictureBinaryID { get; set; }
		public int PictureId { get; set; }
		public byte[] BinaryData { get; set; }
		public string? Hash	{ get; set; }
    }
}
