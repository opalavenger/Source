using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSoft.Data.Components
{
    public partial class MenuMT
    {

        private string _sysno;

        private string _alternatename;

        private string _sysname;

        private int _syssr;

        public virtual string SysNo
        {
            get
            {
                return this._sysno;
            }
            set
            {
                this._sysno = value;
            }
        }

        public virtual string AlternateName
        {
            get
            {
                return this._alternatename;
            }
            set
            {
                this._alternatename = value;
            }
        }

        public virtual string SysName
        {
            get
            {
                return this._sysname;
            }
            set
            {
                this._sysname = value;
            }
        }

        public virtual int SysSR
        {
            get
            {
                return this._syssr;
            }
            set
            {
                this._syssr = value;
            }
        }
    }

    public class MenuMTCollection : List<MenuMT>
    {
    }

    public partial class MenuMTs
    {

        public static string ConnectionStringSessionName
        {
            get
            {
                return "SystemConnectionStringSessionName";
            }
        }
    }

    public partial class MenuDLPrgType
    {

        private string _programtype;

        private string _programtypename;

        public virtual string ProgramType
        {
            get
            {
                return this._programtype;
            }
            set
            {
                this._programtype = value;
            }
        }

        public virtual string ProgramTypeName
        {
            get
            {
                return this._programtypename;
            }
            set
            {
                this._programtypename = value;
            }
        }
    }

    public class MenuDLPrgTypeCollection : List<MenuDLPrgType>
    {
    }

    public partial class MenuDLPrgTypes
    {

        public static string ConnectionStringSessionName
        {
            get
            {
                return "SystemConnectionStringSessionName";
            }
        }
    }

    public partial class MenuDLPrgNO
    {

        private string _programno;

        private string _alternatename;

        private string _programname;

        private string _programurl;

        private int _menudlid;

        private string _isextension;

        public virtual string ProgramNo
        {
            get
            {
                return this._programno;
            }
            set
            {
                this._programno = value;
            }
        }

        public virtual string AlternateName
        {
            get
            {
                return this._alternatename;
            }
            set
            {
                this._alternatename = value;
            }
        }

        public virtual string ProgramName
        {
            get
            {
                return this._programname;
            }
            set
            {
                this._programname = value;
            }
        }

        public virtual string ProgramURL
        {
            get
            {
                return this._programurl;
            }
            set
            {
                this._programurl = value;
            }
        }

        public virtual int MenuDLId
        {
            get
            {
                return this._menudlid;
            }
            set
            {
                this._menudlid = value;
            }
        }

        public virtual string IsExtension
        {
            get
            {
                return this._isextension;
            }
            set
            {
                this._isextension = value;
            }
        }
    }

    public class MenuDLPrgNOCollection : List<MenuDLPrgNO>
    {
    }

    public partial class MenuDLPrgNOs
    {

        public static string ConnectionStringSessionName
        {
            get
            {
                return "SystemConnectionStringSessionName";
            }
        }
    }

    public partial class Corporation
    {

        private string _corporationshortname;

        private string _corporationfullname;

        private string _corporationaddress;

        public virtual string CorporationshortName
        {
            get
            {
                return this._corporationshortname;
            }
            set
            {
                this._corporationshortname = value;
            }
        }

        public virtual string CorporationFullName
        {
            get
            {
                return this._corporationfullname;
            }
            set
            {
                this._corporationfullname = value;
            }
        }

        public virtual string CorporationAddress
        {
            get
            {
                return this._corporationaddress;
            }
            set
            {
                this._corporationaddress = value;
            }
        }
    }

    public partial class Corporation
    {

        public static string ConnectionStringSessionName
        {
            get
            {
                return "SystemConnectionStringSessionName";
            }
        }
    }

    public partial class PrgNO
    {

        private string _programno;

        private string _programname;

        private string _systemno;

        private string _systemname;

        private string _programurl;

        private string _programtype;

        private string _programtypename;

        private string _isextension;

        public virtual string ProgramNO
        {
            get
            {
                return this._programno;
            }
            set
            {
                this._programno = value;
            }
        }

        public virtual string ProgramName
        {
            get
            {
                return this._programname;
            }
            set
            {
                this._programname = value;
            }
        }

        public virtual string SystemNO
        {
            get
            {
                return this._systemno;
            }
            set
            {
                this._systemno = value;
            }
        }

        public virtual string SystemName
        {
            get
            {
                return this._systemname;
            }
            set
            {
                this._systemname = value;
            }
        }

        public virtual string ProgramURL
        {
            get
            {
                return this._programurl;
            }
            set
            {
                this._programurl = value;
            }
        }

        public virtual string ProgramType
        {
            get
            {
                return this._programtype;
            }
            set
            {
                this._programtype = value;
            }
        }

        public virtual string ProgramTypeName
        {
            get
            {
                return this._programtypename;
            }
            set
            {
                this._programtypename = value;
            }
        }

        public virtual string IsExtension
        {
            get
            {
                return this._isextension;
            }
            set
            {
                this._isextension = value;
            }
        }
    }

    public class PrgNOCollection : List<PrgNO>
    {
    }

    public partial class PrgNOs
    {

        public static string ConnectionStringSessionName
        {
            get
            {
                return "SystemConnectionStringSessionName";
            }
        }
    }

    public partial class PrgNORelative
    {

        private string _programno;

        private string _alternatename;

        private string _programname;

        private string _programurl;

        private int _menudlid;

        public virtual string ProgramNo
        {
            get
            {
                return this._programno;
            }
            set
            {
                this._programno = value;
            }
        }

        public virtual string AlternateName
        {
            get
            {
                return this._alternatename;
            }
            set
            {
                this._alternatename = value;
            }
        }

        public virtual string ProgramName
        {
            get
            {
                return this._programname;
            }
            set
            {
                this._programname = value;
            }
        }

        public virtual string ProgramURL
        {
            get
            {
                return this._programurl;
            }
            set
            {
                this._programurl = value;
            }
        }

        public virtual int MenuDLId
        {
            get
            {
                return this._menudlid;
            }
            set
            {
                this._menudlid = value;
            }
        }
    }

    public class PrgNORelativeCollection : List<PrgNORelative>
    {
    }

    public partial class PrgNORelatives
    {

        public static string ConnectionStringSessionName
        {
            get
            {
                return "SystemConnectionStringSessionName";
            }
        }
    }

    public partial class PrgNORelativePrgType
    {

        private string _programtype;

        private string _programtypename;

        public virtual string ProgramType
        {
            get
            {
                return this._programtype;
            }
            set
            {
                this._programtype = value;
            }
        }

        public virtual string ProgramTypeName
        {
            get
            {
                return this._programtypename;
            }
            set
            {
                this._programtypename = value;
            }
        }
    }

    public class PrgNORelativePrgTypeCollection : List<PrgNORelativePrgType>
    {
    }

    public partial class PrgNORelativePrgTypes
    {

        public static string ConnectionStringSessionName
        {
            get
            {
                return "SystemConnectionStringSessionName";
            }
        }
    }
}
