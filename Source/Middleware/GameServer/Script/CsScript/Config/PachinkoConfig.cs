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
        public List<PachinkoConfig> PachinkoConfigList = new List<PachinkoConfig>()
        {
            new PachinkoConfig() { Id = 1 },
            new PachinkoConfig() { Id = 2 },
            new PachinkoConfig() { Id = 3 },
            new PachinkoConfig() { Id = 4 },
            new PachinkoConfig() { Id = 5 },
        };
    }
}
