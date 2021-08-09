using System;
using System.Collections.Generic;
using System.Text;

using ServiceReference1;

namespace Map_exercise
{
    public static class MyWebEngine
    {
        static WcfServiceClient client = new WcfServiceClient();

        //kiem tra co ton tai
        public static bool Exist(string name)
        {
            return client.UserExist(name);
        }

        public static string Create(string name, string pass)
        {
            return client.CreateUser(name, pass);
        }

        //password  -> password user
        //latitude  -> vi do cua user
        //longitude -> kinh do cua user
        public static string Edit(string name, string tag, string newval)
        {
            return client.EditInfo(name, tag, newval);
        }

        public static string Get(string name, string tag)
        {
            return client.GetInfo(name, tag);
        }

        //tra ve 10 thang dau tien ket qua theo name
        public static List<string> Find(string name)
        {
            return client.FindByName(name);
        }
    }
}