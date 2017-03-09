using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace SSoft.Data.Components
{
    /// <summary>
    /// MenuMT 的摘要描述
    /// </summary>
    public partial class MenuMT
    {
        public MenuMT()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
    }

    public partial class MenuMTs
    {
        public static MenuMTCollection GetMenuMTs()
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter("SELECT DISTINCT MenuMT.Menu_No AS SysNo, ISNULL(MenuMT.Menu_Name, '') AS AlternateName, ISNULL(sysno.sys_nm, '') AS SysName,MenuMT.Menu_Sr AS SysSR FROM         sysno RIGHT OUTER JOIN      prgno INNER JOIN MenuDL INNER JOIN MenuMT ON MenuDL.Menu_No = MenuMT.Menu_No ON prgno.prg_no = MenuDL.Prg_No ON sysno.sys_no = MenuMT.Menu_No ORDER BY SysSR", SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            MenuMTCollection _menuMTCollection = new MenuMTCollection();
            foreach (DataRow row in _dt.Rows)
            {
                MenuMT _menuMT = new MenuMT();
                _menuMT.SysNo = (string)row[0];
                _menuMT.AlternateName = (string)row[1];
                _menuMT.SysName = (string)row[2];
                _menuMTCollection.Add(_menuMT);
            }

            return _menuMTCollection;
        }
    }

    public partial class MenuDLPrgTypes
    {
        public static MenuDLPrgTypeCollection GetMenuDLPrgTypes()
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter("select GNLATTRSYSDL.attr_no as ProgramType ,isNull(GNLATTRSYSDL.attr_nm,'') as ProgramType from GNLATTRSYSDL where GNLATTRSYSDL.Clas_No = 'PRG_TYPE' and GNLATTRSYSDL.attr_no in ( select  distinct MenuDL.Prg_Ty from MenuDL) order by GNLATTRSYSDL.attr_sr", SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            MenuDLPrgTypeCollection _menuDLPrgTypeCollection = new MenuDLPrgTypeCollection();
            foreach (DataRow row in _dt.Rows)
            {
                MenuDLPrgType _menuDLPrgType = new MenuDLPrgType();
                _menuDLPrgType.ProgramType = (string)row[0];
                _menuDLPrgType.ProgramTypeName = (string)row[1];
                _menuDLPrgTypeCollection.Add(_menuDLPrgType);
            }

            return _menuDLPrgTypeCollection;
        }
        public static MenuDLPrgTypeCollection GetMenuDLPrgTypes(string _sysNo)
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter(string.Format("select GNLATTRSYSDL.attr_no as ProgramType ,isNull(GNLATTRSYSDL.attr_nm,'') as ProgramType from GNLATTRSYSDL where GNLATTRSYSDL.Clas_No = 'PRG_TYPE' and GNLATTRSYSDL.attr_no in ( select  distinct MenuDL.Prg_Ty from MenuDL where MenuDL.Menu_No = '{0}') order by GNLATTRSYSDL.attr_sr", _sysNo), SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            MenuDLPrgTypeCollection _menuDLPrgTypeCollection = new MenuDLPrgTypeCollection();
            foreach (DataRow row in _dt.Rows)
            {
                MenuDLPrgType _menuDLPrgType = new MenuDLPrgType();
                _menuDLPrgType.ProgramType = (string)row[0];
                _menuDLPrgType.ProgramTypeName = (string)row[1];
                _menuDLPrgTypeCollection.Add(_menuDLPrgType);
            }

            return _menuDLPrgTypeCollection;
        }
    }

    public partial class MenuDLPrgNOs
    {
        public static MenuDLPrgNOCollection GetMenuDLPrgNOs()
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter("select menudl.prg_no AS ProgramNo ,isnull(menudl.AlternateName,'') AS AlternateName ,isNull(prgno.prg_nm,'') AS ProgramName,isNull(prgno.prg_url,'') AS ProgramURL, menudl.id AS MenuDLId,Isnull(prgno.c_ext,'N') as IsExtension from prgno,menudl where menudl.prg_no = prgno.prg_no  order by Menudl.prg_Sr", SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            MenuDLPrgNOCollection _collection = new MenuDLPrgNOCollection();
            foreach (DataRow row in _dt.Rows)
            {
                MenuDLPrgNO _item = new MenuDLPrgNO();
                _item.ProgramNo = (string)row[0];
                _item.AlternateName = (string)row[1];
                _item.ProgramName = (string)row[2];
                _item.ProgramURL = (string)row[3];
                _item.MenuDLId = Convert.ToInt32(row[4]);
                _item.IsExtension = (string)row[5];
                _collection.Add(_item);
            }

            return _collection;
        }
        public static MenuDLPrgNOCollection GetMenuDLPrgNOs(string _sysNo, string _prgType)
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter(string.Format("select menudl.prg_no AS ProgramNo ,isnull(menudl.AlternateName,'') AS AlternateName ,isNull(prgno.prg_nm,'') AS ProgramName,isNull(prgno.prg_url,'') AS ProgramURL , menudl.id AS MenuDLId,Isnull(prgno.c_ext,'N') as IsExtension from prgno,menudl where menudl.prg_no = prgno.prg_no and menudl.menu_no = '{0}' and menudl.prg_ty = '{1}' order by Menudl.prg_Sr", _sysNo, _prgType), SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            MenuDLPrgNOCollection _collection = new MenuDLPrgNOCollection();
            foreach (DataRow row in _dt.Rows)
            {
                MenuDLPrgNO _item = new MenuDLPrgNO();
                _item.ProgramNo = (string)row[0];
                _item.AlternateName = (string)row[1];
                _item.ProgramName = (string)row[2];
                _item.ProgramURL = (string)row[3];
                _item.MenuDLId = Convert.ToInt32(row[4]);
                _item.IsExtension = (string)row[5];
                _collection.Add(_item);
            }

            return _collection;
        }
        public static MenuDLPrgNOCollection GetMenuDLPrgNOs(string _menudl_id)
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter(string.Format("select menudl.prg_no AS ProgramNo ,isnull(menudl.AlternateName,'') AS AlternateName ,isNull(prgno.prg_nm,'') AS ProgramName,isNull(prgno.prg_url,'') AS ProgramURL , menudl.id AS MenuDLId from prgno,menudl where menudl.prg_no = prgno.prg_no and menudl.id={0}", _menudl_id), SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            MenuDLPrgNOCollection _collection = new MenuDLPrgNOCollection();
            foreach (DataRow row in _dt.Rows)
            {
                MenuDLPrgNO _item = new MenuDLPrgNO();
                _item.ProgramNo = (string)row[0];
                _item.AlternateName = (string)row[1];
                _item.ProgramName = (string)row[2];
                _item.ProgramURL = (string)row[3];
                _item.MenuDLId = Convert.ToInt32(row[4]);

                _collection.Add(_item);
            }

            return _collection;
        }
    }

    public partial class Corporation
    {
        public static Corporation GetCorporation()
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter("SELECT  isnull(corp_nm,'') AS CorporationshortName, isNull(corp_fn,'') AS CorporationFullName, isNull(addr,'') AS CorporationAddress FROM corpcfg  WHERE   (id = 1)", SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            if ((_dt.Rows.Count) == 0) return null;

            DataRow row = _dt.Rows[0];

            Corporation _item = new Corporation();
            _item.CorporationshortName = (string)row[0];
            _item.CorporationFullName = (string)row[1];
            _item.CorporationAddress = (string)row[2];

            return _item;
        }
    }
    public partial class PrgNO
    {
        public static PrgNO GetPrgNO(string _prg_no)
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter(string.Format("SELECT  prgno.prg_no AS ProgramNO, isNull(prgno.prg_nm,'') AS ProgramName, isNull(prgno.sys_no,'') AS SystemNO, isNull(sysno.sys_nm,'') AS SystemName, isnull(prgno.prg_url,'') AS ProgramURL, prgno.prg_ty AS ProgramType, GNLATTRSYSDL.attr_nm AS ProgramTypeName,isnull(prgno.c_ext,'N') AS IsExtension FROM prgno LEFT OUTER JOIN  GNLATTRSYSDL ON prgno.prg_ty = GNLATTRSYSDL.attr_no LEFT OUTER JOIN  sysno ON prgno.sys_no = sysno.sys_no WHERE (GNLATTRSYSDL.CLAS_NO = N'prg_type') and prgno.prg_no='{0}' ", _prg_no), SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            if ((_dt.Rows.Count) == 0) return null;

            DataRow row = _dt.Rows[0];

            PrgNO _item = new PrgNO();
            _item.ProgramNO = (string)row[0];
            _item.ProgramName = (string)row[1];
            _item.SystemNO = (string)row[2];
            _item.SystemName = (string)row[3];
            _item.ProgramURL = (string)row[4];
            _item.ProgramType = (string)row[5];
            _item.ProgramTypeName = (string)row[6];
            _item.IsExtension = (string)row[7];

            return _item;
        }
    }

    public partial class PrgNORelatives
    {
        public static PrgNORelativeCollection GetPrgNORelatives()
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter("select prgnorelative.PrgRelative_No AS ProgramNo ,isnull(prgnorelative.AlternateName,'') AS AlternateName ,isNull(prgno.prg_nm,'') AS ProgramName,isnull(prgno.prg_url,'') AS ProgramURL , prgnorelative.id AS MenuDLId from prgno,prgnorelative where prgnorelative.PrgRelative_No = prgno.prg_no order by prgnorelative.prg_Sr", SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            PrgNORelativeCollection _collection = new PrgNORelativeCollection();
            foreach (DataRow row in _dt.Rows)
            {
                PrgNORelative _item = new PrgNORelative();
                _item.ProgramNo = (string)row[0];
                _item.AlternateName = (string)row[1];
                _item.ProgramName = (string)row[2];
                _item.ProgramURL = (string)row[3];
                _item.MenuDLId = Convert.ToInt32(row[4]);
                _collection.Add(_item);
            }

            return _collection;
        }
        public static PrgNORelativeCollection GetPrgNORelatives(string _prgNo, string _prgType)
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter(string.Format("select prgnorelative.PrgRelative_No AS ProgramNo ,isnull(prgnorelative.AlternateName,'') AS AlternateName ,isNull(prgno.prg_nm,'') AS ProgramName,isnull(prgno.prg_url,'') AS ProgramURL , prgnorelative.id AS MenuDLId from prgno,prgnorelative where prgnorelative.PrgRelative_No = prgno.prg_no  and prgnorelative.prg_no = '{0}' and prgnorelative.prg_ty = '{1}' order by prgnorelative.prg_Sr ", _prgNo, _prgType), SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            PrgNORelativeCollection _collection = new PrgNORelativeCollection();
            foreach (DataRow row in _dt.Rows)
            {
                PrgNORelative _item = new PrgNORelative();
                _item.ProgramNo = (string)row[0];
                _item.AlternateName = (string)row[1];
                _item.ProgramName = (string)row[2];
                _item.ProgramURL = (string)row[3];
                _item.MenuDLId = Convert.ToInt32(row[4]);
                _collection.Add(_item);
            }

            return _collection;
        }

    }

    public partial class PrgNORelative
    {
        public static PrgNORelative GetPrgNORelative(string _prgnorelative_id)
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter(string.Format("select prgnorelative.PrgRelative_No AS ProgramNo ,isnull(prgnorelative.AlternateName,'') AS AlternateName ,isNull(prgno.prg_nm,'') AS ProgramName,isnull(prgno.prg_url,'') AS ProgramURL , prgnorelative.id AS MenuDLId from prgno,prgnorelative where prgnorelative.PrgRelative_No = prgno.prg_no  and prgnorelative.id={0}", _prgnorelative_id), SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            if ((_dt.Rows.Count) == 0) return null;

            DataRow row = _dt.Rows[0];

            PrgNORelative _item = new PrgNORelative();

            _item.ProgramNo = (string)row[0];
            _item.AlternateName = (string)row[1];
            _item.ProgramName = (string)row[2];
            _item.ProgramURL = (string)row[3];
            _item.MenuDLId = Convert.ToInt32(row[4]);

            return _item;
        }
    }

    public partial class PrgNORelativePrgTypes
    {
        public static PrgNORelativePrgTypeCollection GetPrgNORelativePrgTypes()
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter("select GNLATTRSYSDL.attr_no as ProgramType ,isNull(GNLATTRSYSDL.attr_nm,'') as ProgramTypeName from GNLATTRSYSDL where GNLATTRSYSDL.Clas_No = 'PRG_TYPE' and GNLATTRSYSDL.attr_no in ( select  distinct PRGNORelative.Prg_Ty from PRGNORelative) order by GNLATTRSYSDL.attr_sr", SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            PrgNORelativePrgTypeCollection _collection = new PrgNORelativePrgTypeCollection();
            foreach (DataRow row in _dt.Rows)
            {
                PrgNORelativePrgType _item = new PrgNORelativePrgType();
                _item.ProgramType = (string)row[0];
                _item.ProgramTypeName = (string)row[1];
                _collection.Add(_item);
            }

            return _collection;
        }
        public static PrgNORelativePrgTypeCollection GetPrgNORelativePrgTypes(string _prgNo)
        {
            SqlDataAdapter _sqldr = new SqlDataAdapter(string.Format("select GNLATTRSYSDL.attr_no as ProgramType ,isNull(GNLATTRSYSDL.attr_nm,'') as ProgramTypeName from GNLATTRSYSDL where GNLATTRSYSDL.Clas_No = 'PRG_TYPE' and GNLATTRSYSDL.attr_no in ( select  distinct PRGNORelative.Prg_Ty from PRGNORelative where PRGNORelative.prg_no = '{0}' ) order by GNLATTRSYSDL.attr_sr", _prgNo), SSoft.Data.MainDatabase.ConnectString);
            DataTable _dt = new DataTable();
            _sqldr.Fill(_dt);

            PrgNORelativePrgTypeCollection _collection = new PrgNORelativePrgTypeCollection();
            foreach (DataRow row in _dt.Rows)
            {
                PrgNORelativePrgType _item = new PrgNORelativePrgType();
                _item.ProgramType = (string)row[0];
                _item.ProgramTypeName = (string)row[1];
                _collection.Add(_item);
            }

            return _collection;
        }
    }

}
