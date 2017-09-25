using GameServer.CsScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;

namespace GameServer.Script.CsScript.Cast
{
    public class CastRecyclePlayer : ICast
    {
        private GameSession _session;
        public CastRecyclePlayer(GameSession session)
        {
            _session = session;
        }

        public void Send()
        {
            CharacterManager.Recycle(_session);
        }
    }
}
