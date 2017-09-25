using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class Action100 : BaseStruct
    {
        private string content;


        public Action100(HttpGet httpGet) : base(100, httpGet)
        {

        }

        public override void BuildPacket()
        {
            this.PushIntoStack(content);

        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            content = "Hello World for C#!";
            return true;
        }
    }

}
