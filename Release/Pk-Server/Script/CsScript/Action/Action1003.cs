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
            _resData = new DealSwitchRes();
            if (_reqData.SwitchType == "on")
            {
                _resData.ReturnCode = this.StartPlayGame() ? 0 : -1;
            }

            if (_reqData.SwitchType == "off")
            {
                _resData.ReturnCode = this.EndPlayGame() ? 0 : -1;
            }

            return true;
        }

        private bool StartPlayGame()
        {
            //@TODO http req for deal switch
            return this.ChangePachinko(Model.PachinkoStateType.Occupied);
        }

        private bool EndPlayGame()
        {
            //@TODO http req for deal switch
            return this.ChangePachinko(Model.PachinkoStateType.Unoccupied);
        }

        private bool ChangePachinko(Model.PachinkoStateType stateType)
        {
            if(PachinkoManager.Inst.ChangePachinkoState(Current, _reqData.PachinkoId, stateType))
            {
                DispatchCast.Send(new CastChangePachinkoState(Current, _reqData.PachinkoId));
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void BuildPacket()
        {
            PushIntoStack("response");
            PushIntoStack("origin");
            PushIntoStack(JsonConvert.SerializeObject(_resData));
        }
    }
}
