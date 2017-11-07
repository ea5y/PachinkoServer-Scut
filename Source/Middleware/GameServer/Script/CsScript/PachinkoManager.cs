using GameServer.CsScript.CommunicationDataStruct;
using GameServer.Script.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;

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
                PachinkoData pData = new PachinkoData();
                pData.Id = data.Id;
                pData.Times = data.Times;
                pData.Sum = data.Sum;
                pData.PbChange = data.PbChange;
                pData.Award = data.Award;
                pData.StateType = PachinkoStateType.Unoccupied;

                _pachinkoDataSet.PachinkoDataSetList.Add(pData);
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
    }
}
