using GameServer.CsScript.Action;
using GameServer.Script.CsScript.Cast;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.RPC.Sockets;
using GameServer.CsScript.CommunicationDataStruct;

namespace GameServer.CsScript
{
    public class CharacterManager
    {
        private static Dictionary<int, CharacterSyncData> _charaDic = new Dictionary<int, CharacterSyncData>();

        private static Dictionary<GameSession, SyncPositionData> _syncPosDic = new Dictionary<GameSession, SyncPositionData>();
        
        public static void AddCharacter(int key, CharacterSyncData value)
        {
            _charaDic.Add(key, value);
            Console.WriteLine("Add Character UserId: {0}.\nCurrent character count: {1}", key, _charaDic.Count);
        }

        public static CharacterSyncData RemoveCharacter(int key)
        {
            Console.WriteLine("Remove character");
            var result = _charaDic[key];
            _charaDic.Remove(key);
            Console.WriteLine("Remove Character UserId: {0}.\nCurrent character count: {1}", key, _charaDic.Count);
            return result;
        }

        public static CharacterSyncDataSet GetCharaSyncDataSet()
        {
            var result = new CharacterSyncDataSet();
            result.CharaSyncDataList = new List<CharacterSyncData>();
            foreach(var chara in _charaDic)
            {
                result.CharaSyncDataList.Add(_charaDic[chara.Key]);
            }

            return result;
        }

        public static void Spawn(IEnumerable<GameSession> sessions)
        {
            Console.WriteLine("GetSession count: {0}", sessions.Count<GameSession>());
            var charaSet = GetCharaSyncDataSet();
            var buffer = PackCastPackage<CharacterSyncDataSet>(CastID.SpawnPlayer, charaSet);
            foreach(var session in sessions)
            {
                var task = session.SendAsync(OpCode.Text, buffer, 0, buffer.Length, asyncResult =>
                        {
                            Console.WriteLine("The results of data send:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");
                        });
            }
        }

        public static void Recycle(GameSession session)
        {
            var sessions = GameSession.GetOnlineAll();
            var data = RemoveCharacter(session.UserId);
            var buffer = PackCastPackage(CastID.RecyclePlayer, data);
            foreach(var s in sessions)
            {
                if (s.UserId == session.UserId)
                    continue;

                var task = s.SendAsync(OpCode.Text, buffer, 0, buffer.Length, asyncResult =>
                        {
                            Console.WriteLine("The results of data send:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");
                        });
            }

        }

        public static SyncPositionDataSet GetSyncPositionDataSet(GameSession session)
        {
            var result = new SyncPositionDataSet();
            result.SyncPositionDataList = new List<SyncPositionData>();
            foreach(var pos in _syncPosDic)
            {
                //if (session == pos.Key)
                //    continue;
                //pos.Value.UserId = session.UserId;
                result.SyncPositionDataList.Add(pos.Value);
            }

            return result;
        }

        public static void SyncPosition(GameSession session, SyncPositionData data)
        {
            SyncPositionData pData;
            if(_syncPosDic.TryGetValue(session, out pData))
            {
                //pData = data;
                _syncPosDic[session] = data;
            }
            else
            {
                _syncPosDic.Add(session, data);
            }

            var syncPosDataSet = GetSyncPositionDataSet(session);
            var buffer = PackCastPackage(CastID.SyncPlayerPosition, syncPosDataSet);

            foreach(var sync in _syncPosDic)
            {
                if (sync.Key == session)
                    continue;
                SendCast(sync.Key, buffer);
            }
        }

        public static void SendCast(GameSession session, byte[] buffer)
        {
            var task = session.SendAsync(OpCode.Text, buffer, 0, buffer.Length, asyncResult =>
                    {
                        Console.WriteLine("The results of data send:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");
                    });
        }

        public static byte[] PackCastPackage<T>(int actionId, T obj)
        {
            //Console.WriteLine("Cast to UserId: {0}", session.UserId);
            byte[] head = ProtoBufUtils.Serialize(new PacageCastHead() { ActionId = actionId });
            byte[] body = ProtoBufUtils.Serialize(obj);

            byte[] headLen = BitConverter.GetBytes(head.Length);
            byte[] bodyLen = BitConverter.GetBytes(body.Length);

            byte[] buffer = new byte[headLen.Length + head.Length + bodyLen.Length + body.Length];

            Buffer.BlockCopy(headLen, 0, buffer, 0, headLen.Length);
            Buffer.BlockCopy(head, 0, buffer, headLen.Length, head.Length);
            Buffer.BlockCopy(bodyLen, 0, buffer, headLen.Length + head.Length, bodyLen.Length);
            Buffer.BlockCopy(body, 0, buffer, headLen.Length + head.Length + bodyLen.Length, body.Length);

            return buffer;
        }
    }
}
