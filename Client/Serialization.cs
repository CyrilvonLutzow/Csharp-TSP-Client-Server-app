using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    public static class Serialization
    {
        public static byte[] Serialize(object ob)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, ob);
            return stream.ToArray();
        }
    }
}