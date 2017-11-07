using ProtoBuf;
using System;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model
{
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Entity, "TestData")]
    public class User: ShareEntity
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string Username { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public string Password { get; set; }
    }
}
