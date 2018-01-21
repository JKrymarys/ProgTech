using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsOnline.Sources.TCPConnector
{
    public interface WriterReader
    {
        String getMessage();
        void sendMessage(String message);
    }
}
