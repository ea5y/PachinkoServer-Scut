using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model
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
        public int BallsNum{ get; set; }

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
