using System.Collections.Generic;
using System.Text;

using System.Web.Script.Serialization;

namespace AMPSExcel
{
    public interface MessageType
    {
        void PopulateDictionary(byte[] data, int position, int length, IDictionary<string, object> output);
    }
    public class MessageTypeFactory
    {
        public static MessageType forName(string messageType)
        {
            switch (messageType)
            {
                case "fix":
                case "nvfix":
                    return new FIXMessageType();
                case "json":
                    return new JSONMessageType();
                default:
                    return new BinaryMessageType();
            }
        }
    }
    public class FIXMessageType : MessageType
    {
        enum State { Tag, Value };
        internal static byte FieldSeparator = 0x01;
        internal static byte TagValueSeparator = (byte)'=';
        public void PopulateDictionary(byte[] data, int position, int length, IDictionary<string,object> output)
        {
            // We need a simple state machine so we know how to handle
            // embedded '=' in the value.
            State state = State.Tag;
            int tagBegin = position;
            int tagLength = 0;
            int valueBegin = 0;
            for (int index = position; index < position + length; ++index )
            {
                if(data[index]==FieldSeparator && tagLength > 0)
                {
                    string tag = Encoding.ASCII.GetString(data, tagBegin, tagLength);
                    tagLength = 0;
                    string value = Encoding.UTF8.GetString(data, valueBegin, index - valueBegin);
                    output[tag] = value;
                    tagBegin = index+1;
                    tagLength = 0;
                    state = State.Tag;
                }
                else if(data[index]==TagValueSeparator && state == State.Tag)
                {
                    tagLength = index - tagBegin;
                    state = State.Value;
                    valueBegin = index + 1;
                }
            }
        }
    }
    public class JSONMessageType : MessageType
    {
        JavaScriptSerializer _serializer = new JavaScriptSerializer();
        public void PopulateDictionary(byte[] data, int position, int length, IDictionary<string, object> output)
        {
            string jsonText = Encoding.UTF8.GetString(data, position, length);
            //JsonConvert.PopulateObject(jsonText, output);
            foreach (var pair in _serializer.Deserialize<Dictionary<string, object>>(jsonText))
            {
                if (pair.Value is IDictionary<string, object> || pair.Value is System.Collections.ArrayList)
                {
                    output.Add(pair.Key, _serializer.Serialize(pair.Value));
                }
                else
                {
                    output.Add(pair.Key, pair.Value);
                }
            }
        }
    }
    public class BinaryMessageType : MessageType
    {
        public void PopulateDictionary(byte[] data, int position, int length, IDictionary<string, object> output)
        {
            output["Data"] = Encoding.UTF8.GetString(data, position, length);
        }
    }
}