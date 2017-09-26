using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Script.CsScript.Config
{
    public class PachinkoConfig
    {
        public int Id;
        public string URL;
    }

    public class PachinkoConfigSet
    {
        public List<PachinkoConfig> PachinkoConfigList = new List<PachinkoConfig>() { new PachinkoConfig() { Id = 1 } };
    }
}
