using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace SSoft.Web.UI.WebControls
{
    public class GridViewBase : System.Web.UI.WebControls.GridView
    {

        

        protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            int numRows = base.CreateChildControls(dataSource, dataBinding);

            //no data rows created, create empty table if enabled
            if (numRows == 0 && ShowWhenEmpty)
            {
                //create table
                Table table = new Table();
                table.ID = this.ID;

                //convert the exisiting columns into an array and initialize
                DataControlField[] fields = new DataControlField[this.Columns.Count];
                this.Columns.CopyTo(fields, 0);

                if (this.ShowHeader)
                {
                    //create a new header row
                    GridViewRow headerRow = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);

                    this.InitializeRow(headerRow, fields);
                    table.Rows.Add(headerRow);
                }

                //create the empty row
                GridViewRow emptyRow = new GridViewRow(-1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);

                TableCell cell = new TableCell();
                cell.ColumnSpan = this.Columns.Count;
                cell.Width = Unit.Percentage(100);
                if (!String.IsNullOrEmpty(EmptyDataText))
                    cell.Controls.Add(new LiteralControl(EmptyDataText));

                if (this.EmptyDataTemplate != null)
                    EmptyDataTemplate.InstantiateIn(cell);

                emptyRow.Cells.Add(cell);
                table.Rows.Add(emptyRow);

                if (this.ShowFooter)
                {
                    //create footer row
                    GridViewRow footerRow = base.CreateRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);

                    this.InitializeRow(footerRow, fields);
                    table.Rows.Add(footerRow);
                }

                this.Controls.Clear();
                this.Controls.Add(table);
            }

            return numRows;


        }

        [Category("Behaviour")]
        [Themeable(true)]
        [Bindable(BindableSupport.No)]
        public bool ShowWhenEmpty
        {
            get
            {
                if (ViewState["ShowWhenEmpty"] == null)
                    ViewState["ShowWhenEmpty"] = true;

                return (bool)ViewState["ShowWhenEmpty"];
            }
            set
            {
                ViewState["ShowWhenEmpty"] = value;
            }
        }
    }
}
