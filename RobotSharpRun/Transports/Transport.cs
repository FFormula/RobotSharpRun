using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSharpRun.Transports
{
    interface Transport
    {
        string GetNextRunkey();
        void GetWorkFiles(string runkey, string toFolder);
        void PutDoneFiles(string runkey, string fromFolder);
    }
}
