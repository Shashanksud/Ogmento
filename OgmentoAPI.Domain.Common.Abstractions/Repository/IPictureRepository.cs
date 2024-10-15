using OgmentoAPI.Domain.Common.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Common.Abstractions.Repository
{
	public interface IPictureRepository
	{
		public Task<int?> GetPictureIdFromHash(string hash);
		public List<PictureModel> GetImagesByPictureIds(List<int> pictureIds);
	}
}
