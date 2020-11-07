using System;
using System.Collections.Generic;
using System.Text;

namespace Kartrider.API.Tests
{
    class KartAPISingleton
    {
        private static KartAPI _kartAPI;
        public static KartAPI KartAPI
        {
            get
            {
                return _kartAPI;
            }
        }
        static KartAPISingleton()
        {
            _kartAPI = new KartAPI(Define.API_KEY, true);
        }
    }
}
