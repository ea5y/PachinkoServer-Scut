using GameServer.CsScript.ProtocStruct;
using GameServer.Script.CsScript.Cast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

//namespace GameServer.CsScript.Action
namespace GameServer.Script.CsScript.Action
{
    public class Action1003 : BaseStruct
    {
        private int _id;
        private DealSwitchReq _reqData;
        private DealSwitchRes _resData;


        public Action1003(ActionGetter actionGetter) : base(ActionID.DealSwitch, actionGetter)
        {
        }

        public override bool GetUrlElement()
        {
            string str = string.Empty;
            if(httpGet.GetString("data", ref str))
            {
                _reqData = JsonConvert.DeserializeObject<DealSwitchReq>(str);
            }
            return true;
        }

        public override bool TakeAction()
        {
            //@TODO http req for deal switch
            _resData = new DealSwitchRes();
            if(PachinkoManager.Inst.ChangePachinkoState(_reqData.PachinkoId, Model.PachinkoStateType.Occupied))
            {
                DispatchCast.Send(new CastChangePachinkoState(Current, _reqData.PachinkoId));
                _resData.ReturnCode = 0;
            }
            else
            {
                _resData.ReturnCode = -1;
            }
            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack("response");
            PushIntoStack("origin");
            PushIntoStack(JsonConvert.SerializeObject(_resData));
        }
    }
}
