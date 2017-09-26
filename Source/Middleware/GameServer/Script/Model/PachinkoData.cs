using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model
{
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Entity, "TestData")]
    public class PachinkoData : ShareEntity
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public int Id { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public int Times { get; set; }
        [ProtoMember(3)]
        [EntityField]
        public int Sum { get; set; }
        [ProtoMember(4)]
        [EntityField]
        public int PbChange { get; set; }
        [ProtoMember(5)]
        [EntityField]
        public int Award { get; set; }
    }
}
