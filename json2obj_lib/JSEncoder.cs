using System;
using System.Collections.Generic;
using json2obj_lib;

namespace json2obj_console_test
{
    public class JSEncoder
    {
        public string Encode(JSGenericNK obj, int depth = 0)
        {
            string res = string.Empty;
            if (obj is JSNull) res = EncodeNull((JSNull)obj, depth);
            else if (obj is JSObject) res = EncodeObject((JSObject)obj, depth);
            else if (obj is JSArray) res = EncodeArray((JSArray)obj, depth);
            else if (obj is JSString) res = EncodeString((JSString)obj, depth);
            else if (obj is JSDouble) res = EncodeDouble((JSDouble)obj, depth);
            else if (obj is JSBool) res = EncodeBool((JSBool)obj, depth);
            else if (obj is JSNullNK) res = EncodeNullNK((JSNullNK)obj, depth);
            else if (obj is JSObjectNK) res = EncodeObjectNK((JSObjectNK)obj, depth);
            else if (obj is JSArrayNK) res = EncodeArrayNK((JSArrayNK)obj, depth);
            else if (obj is JSStringNK) res = EncodeStringNK((JSStringNK)obj, depth);
            else if (obj is JSDoubleNK) res = EncodeDoubleNK((JSDoubleNK)obj, depth);
            else if (obj is JSBoolNK) res = EncodeBoolNK((JSBoolNK)obj, depth);
            return res;
        }

        private string EncodeNull(JSNull obj, int depth) => $"{new string('\t', depth)}{obj.Key}: null";

        private string EncodeObject(JSObject obj, int depth = 0)
        {
            string res = string.Empty;
            res += new string('\t', depth);
            res += $"\"{obj.Key}\": ";
            res += "{\r\n";
            for (int i = 0; i < obj.Value.Count; i++)
            {
                res += Encode(obj.Value[i], depth + 1);
                if (res != string.Empty && i < obj.Value.Count - 1) res += ",";
                res += "\r\n";
            }
            res += $"{new string('\t', depth)}}}";
            return res;
        }
        private string EncodeArray(JSArray obj, int depth = 0)
        {
            string res = string.Empty;
            res += new string('\t', depth);
            res += $"\"{obj.Key}\": ";
            res += "[\r\n";
            for (int i = 0; i < obj.Value.Count; i++)
            {
                res += Encode(obj.Value[i], depth + 1);
                if (res != string.Empty && i < obj.Value.Count - 1) res += ",";
                res += "\r\n";
            }
            res += $"{new string('\t', depth)}]";
            return res;
        }

        private string EncodeString(JSString obj, int depth = 0) => $"{new string('\t', depth)}\"{obj.Key}\": \"" + obj.Value.Replace("\"","\\\"") + "\"";
        private string EncodeDouble(JSDouble obj, int depth = 0) => $"{new string('\t', depth)}\"{obj.Key}\": {obj.Value}";
        private string EncodeBool(JSBool obj, int depth = 0) => $"{new string('\t', depth)}\"{obj.Key}\": {obj.Value.ToString().ToLower()}";


        //NK
        private string EncodeNullNK(JSNullNK obj, int depth) => $"{new string('\t', depth)}null";
        private string EncodeObjectNK(JSObjectNK obj, int depth = 0)
        {
            string res = string.Empty;
            res += new string('\t', depth);
            res += "{\r\n";
            for (int i = 0; i < obj.Value.Count; i++)
            {
                res += Encode(obj.Value[i], depth + 1);
                if (res != string.Empty && i < obj.Value.Count - 1) res += ",";
                res += "\r\n";
            }
            res += $"{new string('\t', depth)}}}";
            return res;
        }
        private string EncodeArrayNK(JSArrayNK obj, int depth = 0)
        {
            string res = string.Empty;
            res += new string('\t', depth);
            res += "[\r\n";
            for (int i = 0; i < obj.Value.Count; i++)
            {
                res += Encode(obj.Value[i], depth + 1);
                if (res != string.Empty && i < obj.Value.Count - 1) res += ",";
                res += "\r\n";
            }
            res += $"{new string('\t', depth)}]";
            return res;
        }
        private string EncodeStringNK(JSStringNK obj, int depth = 0) => $"{new string('\t', depth)}\"" + obj.Value.Replace("\"","\\\"") + "\"";
        private string EncodeDoubleNK(JSDoubleNK obj, int depth = 0) => $"{new string('\t', depth)}{obj.Value}";
        private string EncodeBoolNK(JSBoolNK obj, int depth = 0) => $"{new string('\t', depth)}{obj.Value.ToString().ToLower()}";
    }
}