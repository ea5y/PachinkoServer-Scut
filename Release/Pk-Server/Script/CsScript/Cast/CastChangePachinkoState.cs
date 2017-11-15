using GameServer.CsScript.ProtocStruct;
using GameServer.Script.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;

namespace GameServer.Script.CsScript.Cast
{
    public class CastChangePachinkoState : ICast
    {
        private GameSession _session;
        private ChangePachinkoStateDataCast _data;
        public CastChangePachinkoState(GameSession session, int id)
        {
            _session = session;
            _data = new ChangePachinkoStateDataCast();
            PachinkoData pData;
            PachinkoManager.Inst.FindPachinkoData(id, out pData);
            _data.Id = pData.Id;
            _data.StateType = pData.StateType;
            _data.Type = pData.Type;
            _data.OwnerUserId = pData.OwnerUserId;
        }

        public void Send()
        {
            PachinkoManager.Inst.CastPachinkoState(_session, _data);
        }
    }
}
