using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XStreamer.Protocol.Message
{
    public class PacketData
    {
        public const int LengthOffset = 0;
        public const int TypeOffset = 4;
        public const int IdOffset = 5;
        public const int PayloadOffset = 9;
        public const int HeaderSize = 9;

        public static byte[] CreateData(PacketType type, int id, int payloadLength)
        {
            // total size is payload + the header
            byte[] data = new byte[payloadLength + HeaderSize];

            // set the message length - doesn't include the first four bytes containing the 
            // length
            Array.Copy(
                BitConverter.GetBytes(data.Length - 4),
                0, data, LengthOffset, 4);

            // set the packet type
            data[IdOffset] = (byte)type;

            // set the id
            Array.Copy(BitConverter.GetBytes(id), 0, data, IdOffset, 4);

            return data;
        }
    }
}
