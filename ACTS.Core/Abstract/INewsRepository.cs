using ACTS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Abstract
{
	public interface INewsRepository
	{
		// Uncos -- новости в множине
		IQueryable<News> Uncos { get; }     
		void UpdateNews(News news);
		void CreateNews(News news);
		News GetNewsById(int newsId);
		News DeleteNews(int newsId);
	}
}
