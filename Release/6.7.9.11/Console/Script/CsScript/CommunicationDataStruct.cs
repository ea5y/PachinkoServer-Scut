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
    }

    public class PositionData
    {
        public double PosX;
        public double PosY;
        public double PosZ;
    }
}
