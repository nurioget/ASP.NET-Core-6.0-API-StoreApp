using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public abstract class RequestParametres
    {
		const int maxPageSize = 50;
		//Auto-iplemented
        public int PageNumber { get; set; }

		//Full-property
		private int _pageSize;

		public int pageSize
		{
			get { return _pageSize; }
			set { _pageSize = value > maxPageSize ? maxPageSize : value; }
		}

		 


	}
}
