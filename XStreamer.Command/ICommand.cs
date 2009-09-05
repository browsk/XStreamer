using System.IO;

namespace XStreamer.Command
{
    public interface ICommand
    {
        void Execute(Stream stream);
    }
}
