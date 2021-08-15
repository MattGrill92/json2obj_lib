using System;
using System.Collections.Generic;
using json2obj_lib;

namespace json2obj_console_test
{
    internal class Decode
    {

        public Decode()
        {
            string str = JSDecode(
                new JSObject() 
                {
                    Key = "root",
                    Value = new List<JSGeneric>()
                    {
                        new JSString() { Key = "stringa", Value = "valore stringa" },
                        new JSObject() { Key = "oggetto", Value =
                        {
                            new JSNull()
                        }}
                    }
                }
            );

            Console.WriteLine(str);
        }

        public string JSDecode(JSGeneric obj, int depth = 0, bool ignoreKey = false)
        {
            string res = string.Empty;
            if (obj is JSNull) res = JSDecodeNull((JSNull)obj, depth);
            else if (obj is JSObject) res = JSDecodeObject((JSObject)obj, depth, ignoreKey);
            //else if (obj is JSArray) JSDecodeArray(obj, depth);
            //else if (obj is JSString) JSDecodeString(obj, depth);
            //else if (obj is JSInt) JSDecodeInt(obj, depth);
            //else if (obj is JSDouble) JSDecodeDouble(obj, depth);
            //else if (obj is JSBool) JSDecodeBool(obj, depth);
            //else if (obj is JSObjectNK) JSDecodeObjectNK(obj);
            //else if (obj is JSArrayNK) JSDecodeArrayNK(obj);
            //else if (obj is JSStringNK) JSDecodeStringNK(obj);
            //else if (obj is JSIntNK) JSDecodeIntNK(obj);
            //else if (obj is JSDoubleNK) JSDecodeDoubleNK(obj);
            return res;
        }

        public string JSDecodeNull(JSNull obj, int depth) => $"{new string('\t', depth)}{obj.Key}: null";

        public string JSDecodeObject(JSObject obj, int depth = 0, bool ignoreKey = false)
        {
            string res = string.Empty;
            res += new string('\t', depth);
            if (!ignoreKey) res += $"{obj.Key}: ";
            res += "{\r\n";
            foreach (var elm in obj.Value)
            {
                if (res != string.Empty) res += ",\r\n";
                res += JSDecode(elm, depth + 1);
            }
            res += $"{new string('\t', depth)}\r\n}}";
            return res;
        }
        //Array
        public string JSDecodeString(JSObject obj, int depth = 0) => $"{new string('\t', depth)}{obj.Key}:{obj.Value}";


    }
}