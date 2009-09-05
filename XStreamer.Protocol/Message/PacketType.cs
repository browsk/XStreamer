namespace XStreamer.Protocol.Message
{
    public enum PacketType : byte
    {
        Ok = 1,
        Error = 2,
        Handle = 3,
        FileData = 4,
        FileContents = 5,
        AuthenticationContinue = 6,
        Null = 10,
        SetCwd = 11,
        FileListOpen = 12,
        FileListRead = 13,
        FileInfo = 14,
        FileOpen = 15,
        FileRead = 16,
        FileSeek = 17,
        Close = 18,
        CloseAll = 19,
        SetConfiguationOption = 20,
        AuthenticationInit = 21,
        Authenticate = 22,
        UpCwd = 23,
        ServerDiscoveryQuery = 90,
        ServerDiscoveryReply = 91
    }
}
