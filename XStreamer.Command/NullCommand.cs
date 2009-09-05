using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XStreamer.Command
{
    class NullCommand : ICommand
    {
        public void Execute(Stream stream)
        {
            //OkMessage message = new OkMessage();
        }
    }
}
