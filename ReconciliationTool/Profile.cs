using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconciliationTool
{
    public class Profile
    {
        private int inID = 0;
        private String szName = String.Empty;
        private String szDBServer = String.Empty;
        private String szDBCatalog = String.Empty;
        private String szDBUsername = String.Empty;
        private String szDBPassword = String.Empty;
        private String szDBSchema = String.Empty;
        private String szVCVersion = String.Empty;

        private String szDefaultModel = String.Empty;
        private String szDefaultApplication = String.Empty;
        private String szDefaultCluster = String.Empty;
        private String szDefaultTemplate = String.Empty;

        private List<Parameter> listParamConfig = new List<Parameter>();

        public Profile()
        {

        }

        public void vdSetID(int inID)
        {
            this.inID = inID;
        }

        public void vdSetName(String szName)
        {
            this.szName = szName;
        }

        public void vdSetDBServer(String szDBServer)
        {
            this.szDBServer = szDBServer;
        }

        public void vdSetDBCatalog(String szDBCatalog)
        {
            this.szDBCatalog = szDBCatalog;
        }

        public void vdSetDBUsername(String szDBUsername)
        {
            this.szDBUsername = szDBUsername;
        }

        public void vdSetDBPassword(String szDBPassword)
        {
            this.szDBPassword = szDBPassword;
        }

        public void vdSetDBSchema(String szDBSchema)
        {
            this.szDBSchema = szDBSchema;
        }

        public void vdSetVCVersion(String szVCVersion)
        {
            this.szVCVersion = szVCVersion;
        }

        public void vdSetDefaultModel(String szDefaultModel)
        {
            this.szDefaultModel = szDefaultModel;
        }

        public void vdSetDefaultApplication(String szDefaultApplication)
        {
            this.szDefaultApplication = szDefaultApplication;
        }

        public void vdSetDefaultCluster(String szDefaultCluster)
        {
            this.szDefaultCluster = szDefaultCluster;
        }

        public void vdSetDefaultTemplate(String szDefaultTemplate)
        {
            this.szDefaultTemplate = szDefaultTemplate;
        }

        public String szGetDBServer()
        {
            return szDBServer;
        }

        public String szGetDBCatalog()
        {
            return szDBCatalog;
        }

        public String szGetDBUsername()
        {
            return szDBUsername;
        }

        public String szGetDBPassword()
        {
            return szDBPassword;
        }

        public String szGetDBSchema()
        {
            return szDBSchema;
        }

        public String szGetVCVersion()
        {
            return szVCVersion;
        }

        public int inGetID()
        {
            return inID;
        }

        public String szGetName()
        {
            return szName;
        }

        public String szGetDefaultModel()
        {
            return szDefaultModel;
        }

        public String szGetDefaultApplication()
        {
            return szDefaultApplication;
        }

        public String szGetDefaultCluster()
        {
            return szDefaultCluster;
        }

        public String szGetDefaultTemplate()
        {
            return szDefaultTemplate;
        }

        public List<Parameter> listGetParamConfig()
        {
            return listParamConfig;
        }

        ~Profile()
        {

        }
    }

    public class Terminal
    {
        private int inID = 0;
        private String szSN = String.Empty;
        private String szApplication = String.Empty;
        private String szCluster = String.Empty;
        private String szModel = String.Empty;
        private String szTemplate = String.Empty;
        private int inLength = 0;

        private List<Parameter> listParamTerm = new List<Parameter>();

        public Terminal()
        {

        }

        public void vdSetID(int inID)
        {
            this.inID = inID;
        }

        public void vdSetSN(String szSN)
        {
            this.szSN = szSN;
        }

        public void vdSetApplication(String szApplication)
        {
            this.szApplication = szApplication;
        }

        public void vdSetCluster(String szCluster)
        {
            this.szCluster = szCluster;
        }

        public void vdSetModel(String szModel)
        {
            this.szModel = szModel;
        }

        public void vdSetTemplate(String szTemplate)
        {
            this.szTemplate = szTemplate;
        }

        public void vdSetLength(int inLength)
        {
            this.inLength = inLength;
        }

        public int inGetID()
        {
            return inID;
        }

        public String szGetSN()
        {
            return szSN;
        }

        public String szGetApplication()
        {
            return szApplication;
        }

        public String szGetCluster()
        {
            return szCluster;
        }

        public String szGetTemplate()
        {
            return szTemplate;
        }

        public String szGetModel()
        {
            return szModel;
        }

        public int inGetLength()
        {
            return inLength;
        }

        public List<Parameter> listGetParamTerm()
        {
            return listParamTerm;
        }
    }

    public class Parameter
    {
        private int inID = 0;
        private String szName = String.Empty;
        private String szLabel = String.Empty;
        private String szValue = String.Empty;
        private String szLength = String.Empty;
        private int inEncrypt = 0;
        private String szOLDValue = String.Empty;
        private Char chPadChar = 'N';       //No pad
        private Char chPadPos = 'L';        //Default Left

        private String szDesc = String.Empty;

        public Parameter()
        {

        }

        public void vdSetID(int inID)
        {
            this.inID = inID;
        }

        public void vdSetName(String szName)
        {
            this.szName = szName;
        }

        public void vdSetLabel(String szLabel)
        {
            this.szLabel = szLabel;
        }

        public void vdSetValue(String szValue)
        {
            this.szValue = szValue;
        }

        public void vdSetOLDValue(String szOLDValue)
        {
            this.szOLDValue = szOLDValue;
        }

        public void vdSetPadChar(Char chPadChar)
        {
            this.chPadChar = chPadChar;
        }

        public void vdSetPadPos(Char chPadPos)
        {
            this.chPadPos = chPadPos;
        }

        public void vdSetLength(String szLength)
        {
            this.szLength = szLength;
        }

        public void vdSetEncrypt(int inEncrypt)
        {
            this.inEncrypt = inEncrypt;
        }
        public void vdSetDesc(String szDesc)
        {
            this.szDesc = szDesc;
        }


        public int inGetID()
        {
            return inID;
        }

        public String szGetName()
        {
            return szName;
        }

        public String szGetLabel()
        {
            return szLabel;
        }

        public String szGetValue()
        {
            return szValue;
        }

        public String szGetOLDValue()
        {
            return szOLDValue;
        }

        public Char chGetPadChar()
        {
            return chPadChar;
        }

        public Char chGetPadPos()
        {
            return chPadPos;
        }

        public String szGetLength()
        {
            return szLength;
        }

        public String szGetDesc()
        {
            return szDesc;
        }

        public int inGetEncrypt()
        {
            return inEncrypt;
        }
    }
}
