using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiStandard.WebApiCommon
{
	public interface IErrorResponseContent
	{
		string ToStringReport();

		string ToHtmlReport();
	}
}
