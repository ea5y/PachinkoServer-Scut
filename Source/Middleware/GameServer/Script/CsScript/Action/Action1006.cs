using GameServer.CsScript.CommunicationDataStruct;
using GameServer.Script.CsScript.Cast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class Action1006 : BaseStruct
    {
        //StateData _stateData;
        public Action1006(ActionGetter actionGetter) : base(ActionID.GetGoods, actionGetter)
        {
        }

        public override bool GetUrlElement()
        {
            /*
            string str = string.Empty;
            if(httpGet.GetString("data", ref str))
            {
                _stateData = JsonConvert.DeserializeObject<StateData>(str);
            }
            */
            return true;
        }

        public override bool TakeAction()
        {
            /*
            var syncData = new SyncStateData() { UserId = Current.UserId, State = _stateData.State };
            DispatchCast.Send(new CastSyncPlayerState(Current, syncData));
            */
            return true;
        }
    }
}
