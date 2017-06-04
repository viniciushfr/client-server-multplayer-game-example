using MsgPack.Serialization;
using System.IO;

internal class JavaScriptSerializer
{
    public JavaScriptSerializer()
    {

    }

    public static byte[] Serialize<T>(T thisObj)
    {
        var serializer = MessagePackSerializer.Get<T>();

        using (var byteStream = new MemoryStream())
        {
            serializer.Pack(byteStream, thisObj);
            return byteStream.ToArray();
        }
    }

    public static T Deserialize<T>(byte[] bytes)
    {
        var serializer = MessagePackSerializer.Get<T>();
        using (var byteStream = new MemoryStream(bytes))
        {
           return serializer.Unpack(byteStream);
        }
    }
}