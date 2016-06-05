using ACTS.Core.Abstract;
using ACTS.Core.Concrete;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.IoC
{
	public class RepositoriesModule : NinjectModule
	{
		public override void Load()
		{
			this.Bind<IEventRepository>().To<EFEventRepository>();
			this.Bind<IEmployeeRepository>().To<EFEmployeeRepository>();
			this.Bind<ITeacherRepository>().To<EFTeacherRepository>();
			this.Bind<INewsRepository>().To<EFNewsRepository>();
			this.Bind<IBlogRepository>().To<EFBlogRepository>();
			this.Bind<IFileRepository>().To<FileRepository>();
		}
	}
}
