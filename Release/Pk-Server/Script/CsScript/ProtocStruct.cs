using ProtoBuf;
using GameServer.Script.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.CsScript.ProtocStruct
{
    #region Req && Res

    #region Register && Login
    public class RegisterDataReq
    {
        public string UserName;
        public string Password;
        public string Sid;
    }

    public class RegisterDataRes
    {
        public int ReturnCode;
    }

    public class LoginDataReq
    {
        public string Username;
        public string Password;
    }

    public class LoginDataRes
    {
        public int ReturnCode;
        public string SessionId;
        public UserData UserData;
    }
    #endregion

    #region GetPachinko
    public class GetPachinkosRes
    {
        public int ReturnCode;
        public PachinkoDataSet PachinkoDataSet;
    }

    public class PachinkoDataSet
    {
        public List<PachinkoData> PachinkoDataSetList = new List<PachinkoData>();
    }
    #endregion

    #region DealSwitch
    public class DealSwitchReq
    {
        public int PachinkoId;
        public string SwitchType;
    }

    public class DealSwitchRes
    {
        public int ReturnCode;
    }
    #endregion

    #endregion

    #region Cast
    public class ChangePachinkoStateDataCast
    {
        public int Id;
        public PachinkoStateType StateType;
        public PachinkoType Type;
        public int OwnerUserId;
    }
    #endregion
}
