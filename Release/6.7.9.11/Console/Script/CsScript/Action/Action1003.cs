using GameServer.Script.CsScript.Cast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class Action1003 : BaseStruct
    {
        public Action1003(ActionGetter actionGetter) : base(ActionID.SpawnPlayer, actionGetter)
        {

        }

        public override bool TakeAction()
        {
            DispatchCast.Send(new CastSpawnPlayer());
            return true;
        }
    }
}
