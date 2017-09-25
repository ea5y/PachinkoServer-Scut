using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;

namespace ScutGameServer.Model
{
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Dictionary, "TestData")]
    public class UserData : BaseUser
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string NickName { get; set; }


        [ProtoMember(3)]
        [EntityField]
        public int Hp { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public float PosX { get; set; }

        [ProtoMember(5)]
        [EntityField]
        public float PosY { get; set; }

        [ProtoMember(6)]
        [EntityField]
        public float PosZ { get; set; }

        protected override int GetIdentityId()
        {
            return UserId;
        }

        public override bool IsLock
        {
            get
            {
                return false;
            }
        }

        public override int GetUserId()
        {
            return UserId;
        }

        public override string GetNickName()
        {
            return NickName;
        }

        public override string GetPassportId()
        {
            return "";
        }

        public override string GetRetailId()
        {
            return "";
        }
    }
}
