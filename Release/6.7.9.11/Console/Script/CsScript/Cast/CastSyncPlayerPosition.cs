using GameServer.CsScript;
using GameServer.CsScript.CommunicationDataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;

namespace GameServer.Script.CsScript.Cast
{
    public class CastSyncPlayerPosition : ICast
    {
        private GameSession _session;
        private SyncPositionData _syncPosData;
        public CastSyncPlayerPosition(GameSession session, SyncPositionData data)
        {
            _session = session;
            _syncPosData = data;
        }

        public void Send()
        {
            CharacterManager.SyncPosition(_session, _syncPosData);
        }
    }
}
