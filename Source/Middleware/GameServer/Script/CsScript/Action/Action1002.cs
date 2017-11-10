using System;
using System.Text;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using GameServer.Script.Model;
using Newtonsoft.Json;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;
using GameServer.CsScript.ProtocStruct;

//namespace GameServer.CsScript.Action
namespace GameServer.Script.CsScript.Action
{
    public class Action1002 : BaseStruct
    {
        private LoginDataReq _loginData;

        private UserData _userData;
        private LoginDataRes _loginDataRes;
        public Action1002(ActionGetter actionGetter) : base(ActionID.Login, actionGetter)
        {
        }

        public override bool TakeAction()
        {
            //Current = GameSession.CreateNew(Guid.NewGuid());
            //Current.SetExpired();
			//Test();
            User user;
            ShareCacheStruct<User> userCache;
            if(this.FindUser(out user, out userCache))
            {
                _userData = this.GetUserData(user);
            }
            else
            {
                user = this.CreateUser(userCache);
                _userData = this.GetUserData(user);
            }

            if(_userData != null)
            {
                var sessionUser = new SessionUser(_userData);
                var session = GameSession.Get(Current.SessionId);
                if(session != null)
                {
                    session.Bind(sessionUser);
                }
            }

            _loginDataRes = new LoginDataRes() { SessionId = Current.SessionId, UserData = _userData };
            //CharacterManager.AddCharacter(Current.UserId, new CharacterSyncData() { UserId = Current.UserId });
            return true;
        }

        private bool FindUser(out User user, out ShareCacheStruct<User> userCache)
        {
            userCache = new ShareCacheStruct<User>();
            user = userCache.Find(u => u.Username == _loginData.Username );

            //@TODO
            //Authentication

            return user != null;
        }

        private User CreateUser(ShareCacheStruct<User> userCache)
        {
            var userData = new UserData() { UserId = (int)userCache.GetNextNo(), NickName = _loginData.Username, BallsNum = 1000 };
            _userData = userData;
            new PersonalCacheStruct<UserData>().Add(userData);

            var user = new User();
            user.UserId = userData.UserId;
            user.Username = _loginData.Username;
            user.Password = _loginData.Password;
            userCache.Add(user);

            return user;
        }

        private UserData GetUserData(User user)
        {
            var userDataCache = new PersonalCacheStruct<UserData>();
            var userData = userDataCache.Find(user.UserId.ToString(), ud => user.UserId == user.UserId);
            return userData;
        }

        public override bool GetUrlElement()
        {
            string str = string.Empty;
            if(httpGet.GetString("data", ref str))
            {
                _loginData = JsonConvert.DeserializeObject<LoginDataReq>(str);
            }
            return true;
        }

        public override void BuildPacket()
        {
            PushIntoStack("response");
            PushIntoStack("origin");
            //PushIntoStack("hotfix");
            PushIntoStack(JsonConvert.SerializeObject(_loginDataRes));
        }


		private void Test()
		{
			byte[] data = Encoding.UTF8.GetBytes("This is sent to the client data.");
			Current.SendAsync(OpCode.Text, data, 0, data.Length, asyncResult => 
					{
						Console.WriteLine("The results of data send:{0}", asyncResult.Result == ResultCode.Success ? "ok" : "fail");
					}).Wait();
		}
    }
}
