using System;
using System.Collections.Generic;
using System.Text;

namespace SSoft.Web.UI.WebControls.TabsControlComponents
{
    public class TabsControlEventArgs : EventArgs
    {
        private int _pageIndex;
        public int PageIndex
        {
            get { return _pageIndex; }
        }

        public TabsControlEventArgs(int _pageIndex)
        {
            this._pageIndex = _pageIndex;
        }
    }
}
