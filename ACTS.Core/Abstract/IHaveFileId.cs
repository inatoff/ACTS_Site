using System;

namespace ACTS.Core.Abstract
{
	public interface IHaveFileId
	{
		Guid? FileId { get; set; }
	}
}