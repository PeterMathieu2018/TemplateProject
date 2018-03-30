using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DonorPortal.IntegrationTest.Fakes
{
    public class FakeHttpSession :HttpSessionStateBase
    {
        Dictionary<string,object> _sessionDictionary = new Dictionary<string, object>();

        public override object this[string name]
        {
            get { return _sessionDictionary[name]; }
            set { _sessionDictionary[name] = value;}
        }
    }
}
