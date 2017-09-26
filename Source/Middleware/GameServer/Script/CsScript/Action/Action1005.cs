using GameServer.CsScript.CommunicationDataStruct;
using GameServer.Script.CsScript;
using GameServer.Script.CsScript.Cast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Service;

namespace Game.Script.CsScript.Action
{
    public class Action1005 : BaseStruct
    {
        private GetPachinkosRes _res;
        public Action1005(ActionGetter actionGetter) : base(ActionID.GetPachinkos, actionGetter)
        {
        }

        public override bool TakeAction()
        {
            var set = PachinkoManager.Inst.GetPachinkoDataSet();
            _res = new GetPachinkosRes();
            _res.PachinkoDataSet = set;

            return true;
        }

        public override void BuildPacket()
        {
             PushIntoStack(JsonConvert.SerializeObject(_res));
        }
    }
}
