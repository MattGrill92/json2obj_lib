using System;
using System.IO;
using json2obj_lib;

namespace json2obj_console_test
{
    internal class EncodeDecodeTest
    {

        internal void start()
        {
            string filePath = @"..\..\..\TestJSon\test1.json";
            var str = File.ReadAllText(filePath);

            JSObjectNK jSObjectNK = new JSObjectNK();
            //jSObjectNK.encode(str);
            JSDecoder decode = new JSDecoder();
            //decode.jsonString = new JsonString(str);
            //JSObjectNK jSArrayNK = (JSObjectNK) decode.recursive_read_fn(enum_obj_array.obj, enum_key_value.value_begin, enum_datatype.dunno);
            
            JSDecoder decode2 = new JSDecoder();
            JSObjectNK jSObjectNK2 = decode2.Decode(str);

            Console.WriteLine(new JSEncoder().Encode(jSObjectNK2));
        }
    }
}