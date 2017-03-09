using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace SSoft.Web.UI.WebControls
{
    public class GridViewMaintain : GridViewBase
    {

        public int RowCount
        {
            get 
            {
                if (this.ViewState["RowCount"] == null)
                    return 0;
                return Convert.ToInt32(this.ViewState["RowCount"]);
            }
            set { this.ViewState["RowCount"] = value; }
        }

        public GridViewMaintain()
        {
            //this.SkinID = "GridViewMaintain";
            if (!this.DesignMode)
            {
                System.Web.UI.WebControls.TemplateField _templateField = new System.Web.UI.WebControls.TemplateField();
                _templateField.HeaderText = "";
                this.Columns.Add(_templateField);
            }
            this.AllowPaging = true;
            this.AllowSorting = true;
            this.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            this.RowStyle.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            this.RowStyle.Height = new System.Web.UI.WebControls.Unit("33px");
        }

        protected override void OnInit(EventArgs e)
        {
            
            base.OnInit(e);
        }


        protected override void OnLoad(EventArgs e)
        {
            
            
            base.OnLoad(e);
        }
        
        protected override void OnRowDataBound(System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

            if (!this.DesignMode)
            {
                if (e.Row.RowIndex != -1)
                {
                    e.Row.Cells[0].Text = Convert.ToString(this.PageIndex * this.PageSize + e.Row.RowIndex + 1);
                }
            }
            base.OnRowDataBound(e);
        }

        DropDownList _pageDDL;
        public PagedDataSource PagedDataSource;
        Button _buttonGO;
        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            this.PagedDataSource = pagedDataSource;
            this.RowCount = pagedDataSource.DataSourceCount;
                System.Web.UI.WebControls.GridViewRow bottonPagerRow = row;
                if (bottonPagerRow != null)
                {
                    
                    _pageDDL = new DropDownList();
                    _pageDDL.AutoPostBack = true;
                    _pageDDL.ID = "PageDDL";
                    _pageDDL.SelectedIndexChanged += new EventHandler(_pageDDL_SelectedIndexChanged);
                    _pageDDL.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Middle");
                    _buttonGO = new Button();
                    _buttonGO.ID = "ButtonGO";
                    _buttonGO.CommandName = "GO";

                    _buttonGO.Text = "GO";
                    _buttonGO.Click += new EventHandler(_buttonGO_Click);

                    TableCell cell = new TableCell();
                    cell.ColumnSpan = columnSpan;


                    bottonPagerRow.Cells.Add(cell);
                    bottonPagerRow.Cells[0].ID = "DDD";
                    bottonPagerRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    bottonPagerRow.Cells[0].VerticalAlign = VerticalAlign.Middle;

                    Label bottonPagerNo = new Label();


                    bottonPagerNo.Text = string.Format("共 {0} 筆 {1}頁  ", pagedDataSource.DataSourceCount, this.PageCount);
                    bottonPagerRow.Cells[0].Controls.Add(bottonPagerNo);

                    //ImageButton _buttonFirst = new ImageButton();
                    //_buttonFirst.ImageUrl = "~/images/button/Enabled/最初頁.gif";
                    //ImageButton _buttonPre = new ImageButton();
                    //_buttonPre.ImageUrl = "~/images/button/Enabled/上一頁.gif";
                    //ImageButton _buttonNext = new ImageButton();
                    //_buttonNext.ImageUrl = "~/images/button/Enabled/下一頁.gif";
                    //ImageButton _buttonLast = new ImageButton();
                    //_buttonLast.ImageUrl = "~/images/button/Enabled/最末頁.gif";
                    //bottonPagerRow.Cells[0].Controls.Add(_buttonFirst);
                    //bottonPagerRow.Cells[0].Controls.Add(_buttonPre);
                    //bottonPagerRow.Cells[0].Controls.Add(_buttonNext);
                    //bottonPagerRow.Cells[0].Controls.Add(_buttonLast);

                    Button _buttonFirst = new Button();
                    _buttonFirst.CausesValidation = false;
                    _buttonFirst.ID = "ButtonFirst";
                    _buttonFirst.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Middle");
                    _buttonFirst.Click += new EventHandler(_buttonFirst_Click);
                    _buttonFirst.Text = "最初頁";

                    Button _buttonPre = new Button();
                    _buttonPre.CausesValidation = false;
                    _buttonPre.ID = "ButtonPre";
                    _buttonPre.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Middle");
                    _buttonPre.Click += new EventHandler(_buttonPre_Click);
                    _buttonPre.Text = "上一頁";

                    Button _buttonNext = new Button();
                    _buttonNext.CausesValidation = false;
                    _buttonNext.ID = "ButtonNext";
                    _buttonNext.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Middle");
                    _buttonNext.Click += new EventHandler(_buttonNext_Click);
                    _buttonNext.Text = "下一頁";

                    Button _buttonLast = new Button();
                    _buttonLast.CausesValidation = false;
                    _buttonLast.ID = "ButtonLast";
                    _buttonLast.Style.Add(HtmlTextWriterStyle.VerticalAlign, "Middle");
                    _buttonLast.Click += new EventHandler(_buttonLast_Click);
                    _buttonLast.Text = "最末頁";
                    if (this.PageIndex == 0)
                    {
                        _buttonFirst.Enabled = false;
                        _buttonPre.Enabled = false;
                    }
                    if (this.PageIndex == this.PageCount - 1)
                    {
                        _buttonLast.Enabled = false;
                        _buttonNext.Enabled = false;
                    }

                    bottonPagerRow.Cells[0].Controls.Add(_buttonFirst);
                    bottonPagerRow.Cells[0].Controls.Add(_buttonPre);
                    bottonPagerRow.Cells[0].Controls.Add(_buttonNext);
                    bottonPagerRow.Cells[0].Controls.Add(_buttonLast);

                    Label bottonPagerNo2 = new Label();
                    bottonPagerNo2.Text = "  至第 ";
                    bottonPagerRow.Cells[0].Controls.Add(bottonPagerNo2);


                    for (int _i = 1; _i <= this.PageCount; _i++)
                    {
                        _pageDDL.Items.Add(new ListItem(_i.ToString(), _i.ToString()));
                    }

                    if (this.PageIndex >= 0)
                    {
                        _pageDDL.Text = (this.PageIndex + 1).ToString();
                    }
                    bottonPagerRow.Cells[0].Controls.Add(_pageDDL);
                    Label bottonPagerNo3 = new Label();
                    bottonPagerNo3.Text = "  頁";
                    bottonPagerRow.Cells[0].Controls.Add(bottonPagerNo3);
                    //bottonPagerRow.Cells[0].Controls.Add(_buttonGO);
                    return;
                }


        }


        #region Event

        public event EventHandler<System.EventArgs> PageChanged;
        protected virtual void OnPageChanged(System.EventArgs e)
        {
            if (PageChanged != null)
                PageChanged(this, e);
        }
        #endregion


        void _pageDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PageIndex = Convert.ToInt32(_pageDDL.SelectedValue) - 1;
            System.EventArgs _e = new EventArgs();
            OnPageChanged(_e);
        }



        void _buttonLast_Click(object sender, EventArgs e)
        {
            this.PageIndex = this.PageCount - 1;
            System.EventArgs _e = new EventArgs();
            OnPageChanged(_e);
        }

        void _buttonNext_Click(object sender, EventArgs e)
        {
            this.PageIndex = this.PageIndex + 1;
            System.EventArgs _e = new EventArgs();
            OnPageChanged(_e);
        }

        void _buttonPre_Click(object sender, EventArgs e)
        {
            this.PageIndex = this.PageIndex - 1;
            System.EventArgs _e = new EventArgs();
            OnPageChanged(_e);
        }

        void _buttonFirst_Click(object sender, EventArgs e)
        {
            this.PageIndex = 0;
            System.EventArgs _e = new EventArgs();
            OnPageChanged(_e);
        }

        void _buttonGO_Click(object sender, EventArgs e)
        {
            this.PageIndex = Convert.ToInt32(_pageDDL.SelectedValue) - 1;
            System.EventArgs _e = new EventArgs();
            OnPageChanged(_e);
        }

    }
}
