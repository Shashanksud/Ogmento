﻿
namespace OgmentoAPI.Domain.Common.Abstractions.Dto
{
	public class PictureDto
	{
		public string FileName { get; set; }
		public string MimeType { get; set; }
		public byte[] BinaryData { get; set; }
		public string? Hash { get; set; }
	}
}