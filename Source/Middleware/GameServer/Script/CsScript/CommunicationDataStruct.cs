using ProtoBuf;
using ScutGameServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.CsScript.CommunicationDataStruct
{
    //======================Req & Res==========================
    public class LoginData
    {
        public string Username;
        public string Password;
    }

    public class LoginDataRes
    {
        public string SessionId;

        public UserData UserData;
    }

    public class GetPachinkosRes
    {
        public PachinkoDataSet PachinkoDataSet;
    }

    //========================Cast=============================
    [ProtoContract]
    public class PacageCastHead
    {
        [ProtoMember(1)]
        public int ActionId;
    }

    [ProtoContract]
    public class CharacterSyncDataSet
    {
        [ProtoMember(1)]
        public List<CharacterSyncData> CharaSyncDataList;
    }

    [ProtoContract]
    public class CharacterSyncData
    {
        [ProtoMember(1)]
        public int UserId;
        [ProtoMember(2)]
        public double PosX;
        [ProtoMember(3)]
        public double PosY;
        [ProtoMember(4)]
        public double PosZ;
    }

    [ProtoContract]
    public class SyncPositionDataSet
    {
        [ProtoMember(1)]
        public List<SyncPositionData> SyncPositionDataList;
    }

    [ProtoContract]
    public class SyncPositionData
    {
        [ProtoMember(1)]
        public int UserId;
        [ProtoMember(2)]
        public double PosX;
        [ProtoMember(3)]
        public double PosY;
        [ProtoMember(4)]
        public double PosZ;

        [ProtoMember(5)]
        public double DirX;
        [ProtoMember(6)]
        public double DirZ;
    }

    public class PositionData
    {
        public double PosX;
        public double PosY;
        public double PosZ;

        public double DirX;
        public double DirZ;
    }

    [ProtoContract]
    public class SyncStateDataSet
    {
        [ProtoMember(1)]
        public List<SyncStateData> SyncStateDataList;
    }

    [ProtoContract]
    public class SyncStateData
    {
        [ProtoMember(1)]
        public int UserId;
        [ProtoMember(2)]
        public string State;
    }

    public class StateData
    {
        public string State;
    }

    //===================Use for httpReq with httpserver
    public class BaseRes
    {
        public string Result;
    }

    public class DIORes : BaseRes
    {

    }

    public class PachinkoRes : BaseRes
    {
        public string data;
    }

    public class InputRes : BaseRes
    {
        public byte[] InputData;
    }

    //===================Pachinko====================
    public enum PachinkoStateType
    {
        Unoccupied,
        Occupied,
        Owned,
        Maintain,
        Reset,
        LostConnection
    }

    public class SyncPachinkoStateData
    {
        public int Id;
        public PachinkoStateType StateType;
    }

    public class SyncPachinkoStateDataSet
    {
        public List<SyncPachinkoStateData> SyncPachinkoStateDataList = new List<SyncPachinkoStateData>();
    }

    public class PachinkoData
    {
        public int Id;

        public PachinkoStateType StateType;
        public int Times;
        public int Sum;
        public int PbChange;
        public int Award;
    }

    public class PachinkoDataSet
    {
        public List<PachinkoData> PachinkoDataSetList = new List<PachinkoData>();
    }
}
