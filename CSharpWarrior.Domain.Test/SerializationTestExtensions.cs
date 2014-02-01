using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpWarrior
{
    public static class SerializationTestExtensions
    {
        public static T RoundTrip<T>(this T source)
        {
            T clone;
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                clone = (T) formatter.Deserialize(stream);
            }
            return clone;
        }
    }
}
