using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSoft.Web.UI.WebControls.TabsControlComponents;
using SSoft.Enum;

namespace SSoft.Web.UI.WebControls 
{
    public class TabsControl : CompositeControl,INamingContainer
    {
        LinkButton[] _linkButtons = new LinkButton[12];
        TabsControlContainer[] _tabsControlContainers = new TabsControlContainer[12];
        Panel[] _tabTitlePanels = new Panel[12];

        [Browsable(false)]
        public Panel[] TabTitlePanels
        {
            get { return _tabTitlePanels; }
            set { _tabTitlePanels = value; }
        }

        public TabsControl()
        {
            this.Init +=new EventHandler(TabsControl_Init); 
        }

        void TabsControl_Init(object sender, EventArgs e)
        {
            //this.ActivePageIndex = 0;
        }

        #region Property
        [Browsable(true)]
        [Description("Gets and sets TabTitleTexts  method")]
        [Category("SSoft")]
        [DefaultValueAttribute("")]
        [TypeConverterAttribute(typeof(System.Web.UI.WebControls.StringArrayConverter))]
        public virtual string[] TabTitleTexts
        {
            get { return ViewState["TabTitleTexts"] != null ? (string[])ViewState["TabTitleTexts"] : new string[0]; }
            set
            {
                if (ViewState["TabTitleTexts"] == null || (string[])ViewState["TabTitleTexts"] != value)
                {
                    ViewState["TabTitleTexts"] = value;
                    //if (Initialized)
                    //    RequiresDataBinding = true;
                }
            }
        }

        
        [Browsable(true)]
        [Description("Gets and sets ActiveIndex  method")]
        [Category("SSoft")]
        public virtual int ActivePageIndex
        {
            get { return ViewState["ActivePageIndex"] != null ? (int)ViewState["ActivePageIndex"] : 0; }
            set
            {
                if (ViewState["ActivePageIndex"] == null || (int)ViewState["ActivePageIndex"] != value)
                {
                    ViewState["ActivePageIndex"] = value;
                    ChangeTabPage(Convert.ToInt32(value));
                }
            }
        }

        EnumTabControlType _tabControlType = EnumTabControlType.None;

        [Browsable(false)]
        public virtual EnumTabControlType TabControlType
        {
            get { return _tabControlType; }
            set { _tabControlType = value;}
        }


        
        [Browsable(true)]
        [Description("Gets and sets TargetMultiView  Control method")]
        [DefaultValue("")]
        [Category("SSoft")]
        [TypeConverter(typeof(System.Web.UI.WebControls.ControlIDConverter))]
        public virtual string  TargetMultiView
        {
            get
            {
                if (ViewState["TargetMultiView"] == null)
                    ViewState["TargetMultiView"] = "";
                return ViewState["TargetMultiView"].ToString();
            }
            set
            {
                ViewState["TargetMultiView"] = value;
            }
        }

        #endregion

        

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }


        #region Events

        public event EventHandler<TabsControlEventArgs> TabsControlClick;
        protected virtual void OnTabsControlClick(TabsControlEventArgs e)
        {
            if (TabsControlClick != null)
                TabsControlClick(this, e);
        }

        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            bool handled = false;

            CommandEventArgs _ce = args as CommandEventArgs;
            if (_ce != null && _ce.CommandName != "")
            {
                int _tabIndex = Convert.ToInt32(_ce.CommandName);

                this.ActivePageIndex = _tabIndex;
               
                handled = true;
            }

            return handled;
        }

        #endregion


        private void ChangeTabPage(int _tabIndex)
        {
            MultiView _mv = (MultiView)this.Parent.FindControl(this.TargetMultiView);
            if (_mv != null)
            {
                _mv.ActiveViewIndex = _tabIndex;
            }

            for (int _i = 0; _i < this._tabTitlePanels.Length; _i++)
            {
                if (this._tabTitlePanels[_i] == null)
                    break;
                if (_tabIndex == _i)
                {
                    _tabTitlePanels[_i].CssClass = "ajax-tab-container active";
                }
                else
                {
                    _tabTitlePanels[_i].CssClass = "ajax-tab-container";
                }
            }

            TabsControlEventArgs _me = new TabsControlEventArgs(_tabIndex);
            OnTabsControlClick(_me);
        }

        protected override void CreateChildControls()
        {
            //Controls.Clear();

            if (this.TabTitleTexts.Length > 0)
            {
                for (int _i = 0; _i < this.TabTitleTexts.Length; _i++)
                {
                    _tabTitlePanels[_i] = new Panel();
                    _tabTitlePanels[_i].ID = _i.ToString();
                    if (this.ActivePageIndex == _i)
                    {
                        _tabTitlePanels[_i].CssClass = "ajax-tab-container active";
                    }
                    else
                    {
                        _tabTitlePanels[_i].CssClass = "ajax-tab-container";
                    }
                    _tabsControlContainers[_i] = new TabsControlContainer();

                    _linkButtons[_i] = new LinkButton();
                    _linkButtons[_i].Text = this.TabTitleTexts[_i];
                    _linkButtons[_i].CommandName = _i.ToString();
                    _linkButtons[_i].CausesValidation = true;
                    _tabsControlContainers[_i].Controls.Add(_linkButtons[_i]);
                    //_tabsControlContainers[_i].RenderControl(writer);
                    _tabTitlePanels[_i].Controls.Add(_tabsControlContainers[_i]);
                    Controls.Add(_tabTitlePanels[_i]);
                }
            }
            else
            {
                for (int _i = 0; _i < this._linkButtons.Length; _i++)
                {
                    _tabTitlePanels[_i] = new Panel();
                    _tabTitlePanels[_i].ID = _i.ToString();
                    if (this.ActivePageIndex == _i)
                    {
                        _tabTitlePanels[_i].CssClass = "ajax-tab-container active";
                    }
                    else
                    {
                        _tabTitlePanels[_i].CssClass = "ajax-tab-container";
                    }
                    _tabsControlContainers[_i] = new TabsControlContainer();

                    _linkButtons[_i] = new LinkButton();
                    _linkButtons[_i].Text = "Tab" + _i.ToString();
                    _linkButtons[_i].CommandName = _i.ToString();
                    _linkButtons[_i].CausesValidation = true;
                    _tabsControlContainers[_i].Controls.Add(_linkButtons[_i]);
                    //_tabsControlContainers[_i].RenderControl(writer);
                    _tabTitlePanels[_i].Controls.Add(_tabsControlContainers[_i]);
                    Controls.Add(_tabTitlePanels[_i]);
                }
            }





            ChildControlsCreated = true;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            //ApplyContainerStyles();
            //writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            ////RenderContainer(this._queryStringTextBoxContainer, writer);
            //RenderContainer(this._queryButtonContainer, writer);
            //RenderContainer(this._addButtonContainer, writer);

            //writer.RenderEndTag();
            
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "ajax-tabs");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            
            for (int _i = 0; _i < this._tabTitlePanels.Length; _i++)
            {
                
                if (_tabTitlePanels[_i] != null)
                {
                    
                    _tabTitlePanels[_i].RenderControl(writer);
                }
            }

            writer.RenderEndTag();
        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //    if (this.TabControlType == EnumTabControlType.Master)
        //    {
        //        if (((SSoft.Web.UI.Interface.IPageMaintain)this.Page).IsPermissionQuery == SSoft.Enum.EnumPermission.Disable)
        //        {
        //            this.ActivePageIndex = 1;
        //            this.TabTitlePanels[0].Enabled = false;
        //        }
        //    }

        //}
    }
}
