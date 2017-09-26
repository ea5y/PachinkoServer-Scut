using GameServer.CsScript.CommunicationDataStruct;
using GameServer.Script.CsScript.Cast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Service;

//namespace GameServer.CsScript.Action
namespace Game.Script.CsScript.Action
{
    public class Action1004 : BaseStruct
    {
        //PositionData _PosData;
        public Action1004(ActionGetter actionGetter) : base(ActionID.DealLevel, actionGetter)
        {
        }

        public override bool GetUrlElement()
        {
            /*
            string str = string.Empty;
            if(httpGet.GetString("data", ref str))
            {
                _PosData = JsonConvert.DeserializeObject<PositionData>(str);
            }
            */
            return true;
        }

        public override bool TakeAction()
        {
            /*
            var syncData = new SyncPositionData() { UserId = Current.UserId, PosX = _PosData.PosX, PosY = _PosData.PosY, PosZ = _PosData.PosZ, DirX = _PosData.DirX, DirZ = _PosData.DirZ };
            DispatchCast.Send(new CastSyncPlayerPosition(Current, syncData));
            */
            return true;
        }
    }
}
