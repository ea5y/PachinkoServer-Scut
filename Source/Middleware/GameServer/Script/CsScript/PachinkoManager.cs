using GameServer.CsScript.ProtocStruct;
using GameServer.Script.CsScript.Cast;
using GameServer.Script.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.Script.CsScript
{
    public class PachinkoManager
    {
        private static PachinkoManager _inst;
        public static PachinkoManager Inst
        {
            get
            {
                if (_inst == null)
                    _inst = new PachinkoManager();
                return _inst;
            }
        }

        public PachinkoManager()
        {

        }

        private PachinkoDataSet _pachinkoDataSet;
        private Config.PachinkoConfigSet _pachinkoConfigSet;
        private Dictionary<int, PachinkoData> _pachinkoDic = new Dictionary<int, PachinkoData>();

        #region Init
        public void Init()
        {
            Console.WriteLine("===>PachinkoManager init");
            //read config
            Console.WriteLine("===>PachinkoManager read config");
            _pachinkoConfigSet = IOHelper.ReadFromJson<Config.PachinkoConfigSet>(".");
            //open ydu
            //@TODO

            Console.WriteLine("===>PachinkoManager check pachinko in cache");
            this.CheckPachinkoInCache();
            Console.WriteLine("===>PachinkoManager init set");
            this.InitPachinkoDataSet();
            Console.WriteLine("===>PachinkoManager init dic");
            this.InitPachinkoDic();
            Console.WriteLine("PachinkoManager init complete!");
        }

        private void InitPachinkoDic()
        {
            foreach(var p in _pachinkoDataSet.PachinkoDataSetList)
            {
                _pachinkoDic[p.Id] = p;
            }
        }

        private void InitPachinkoDataSet()
        {
            var pachinkoCache = new ShareCacheStruct<PachinkoData>();
            var datas = pachinkoCache.FindAll();
            _pachinkoDataSet = new PachinkoDataSet();
            foreach(var data in datas)
            {
                /*
                PachinkoData pData = new PachinkoData();
                pData.Id = data.Id;
                pData.Times = data.Times;
                pData.Sum = data.Sum;
                pData.PbChange = data.PbChange;
                pData.Award = data.Award;
                pData.StateType = PachinkoStateType.Unoccupied;
                */

                _pachinkoDataSet.PachinkoDataSetList.Add(data);
            }
        }

        private void CheckPachinkoInCache()
        {
            foreach(var conf in _pachinkoConfigSet.PachinkoConfigList)
            {
                PachinkoData pachinko;
                ShareCacheStruct<PachinkoData> pachinkoCache;
                if (!this.FindPachinko(out pachinko, out pachinkoCache, conf.Id))
                {
                    //Create pachinko
                    this.CreatePchinko(pachinkoCache, conf.Id);
                }
            }
        }

        private bool FindPachinko(out PachinkoData pachinko, out ShareCacheStruct<PachinkoData> pachinkoCache, int id)
        {
            pachinko = null;
            pachinkoCache = null;

            pachinkoCache = new ShareCacheStruct<PachinkoData>();
            pachinko = pachinkoCache.Find(p => p.Id == id);
            return pachinko != null;
        }

        private void CreatePchinko(ShareCacheStruct<PachinkoData> pachinkoCache, int id)
        {
            var pachinkoData = new PachinkoData() { Id = id };
            pachinkoCache.Add(pachinkoData);
        }
        #endregion

        public PachinkoDataSet GetPachinkoDataSet()
        {
            return _pachinkoDataSet;
        }

        public bool ChangePachinkoState(int pachinkoId, PachinkoStateType stateType)
        {
            PachinkoData pachinko;
            if(this.FindPachinko(pachinkoId, out pachinko))
            {
                pachinko.StateType = stateType;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FindPachinko(int pachinkoId, out PachinkoData pachinkoData)
        {
            pachinkoData = _pachinkoDic[pachinkoId];
            return pachinkoData != null;
        }

        #region Brodcast
        public class NetPackage
        {
            public int MsgId;
            public int ProtocId;
            public string Sid;
            public string Uid;
            public string Type;
            public string Version;
            public string Data;
        }

        private void SendCast(GameSession session, int castId, byte[] buffer)
        {
            var task = session.SendAsync(OpCode.Text, buffer, 0, buffer.Length, asyncResult => {
                Console.WriteLine("The result of cast:{0}\nsend:{1}", castId, asyncResult.Result == ResultCode.Success ? "ok" : "fail");
            });
        }

        public void CastPachinkoState(GameSession session, ChangePachinkoStateDataCast dataCast)
        {
            var package = new NetPackage();
            package.ProtocId = CastID.ChangePachinkoState;
            package.Type = "brodcast";
            package.Version = "origin";
            package.Data = JsonConvert.SerializeObject(dataCast);
            var bytes = this.Pack(package);
            var sessions = GameSession.GetOnlineAll();
            foreach(var s in sessions)
            {
                /*
                if (s == session)
                    continue;
                    */
                SendCast(s, CastID.ChangePachinkoState, bytes);
            }
        }

        public byte[] Pack(NetPackage package)
        {
            this.WriteToPackageBytes(0);
            this.WriteToPackageBytes(package.MsgId);
            this.WriteToPackageBytes(0);
            this.WriteToPackageBytes(package.ProtocId);
            this.WriteToPackageBytes(0);
            this.WriteToPackageBytes(package.Type);
            this.WriteToPackageBytes(package.Version);
            this.WriteToPackageBytes(package.Data);

            var bytes = packageBytes;
            var bytesLen = BitConverter.GetBytes(bytes.Length + 4);

            bytes = this.CombineBytes(bytesLen, bytes);
            this.ClearPackageBytes();
            return bytes;
        }
        #endregion

        private byte[] packageBytes = new byte[0];

        private byte[] CombineBytes(byte[] src1, byte[] src2)
        {
            byte[] bytes = new byte[src1.Length + src2.Length];
            Buffer.BlockCopy(src1, 0, bytes, 0, src1.Length);
            Buffer.BlockCopy(src2, 0, bytes, src1.Length, src2.Length);
            return bytes;
        }

        private void WriteToPackageBytes(object value)
        {
            var type = value.GetType();
            if(type == typeof(string))
            {
                this.WriteString(Convert.ToString(value));
            }
            else if(type == typeof(Int32))
            {
                this.WriteInt(Convert.ToInt32(value));
            }
        }

        private void ClearPackageBytes()
        {
            packageBytes = new byte[0];
        }

        private void WriteString(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            byte[] bytesLen = BitConverter.GetBytes(bytes.Length);

            var tmp = this.CombineBytes(bytesLen, bytes);
            packageBytes = this.CombineBytes(packageBytes, tmp);
        }

        private void WriteInt(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            packageBytes = this.CombineBytes(packageBytes, bytes);
        }
    }
}
