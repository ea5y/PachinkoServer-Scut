using GameServer.CsScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;

namespace GameServer.Script.CsScript.Cast
{
    public class CastSpawnPlayer : ICast
    {
        public void Send()
        {
            CharacterManager.Spawn(GameSession.GetOnlineAll());
        }
    }
}
