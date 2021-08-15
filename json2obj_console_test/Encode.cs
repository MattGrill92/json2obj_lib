using System;
using System.IO;
using json2obj_lib;

namespace json2obj_console_test
{
    internal class Encode
    {

        internal void start()
        {
            string filePath = @"..\..\..\TestJSon\test1.json";
            var str = File.ReadAllText(filePath);

            JSObjectNK jSObjectNK = new JSObjectNK();
            //jSObjectNK.encode(str);
            Decode2 decode = new Decode2();

            decode.jsonString = new JsonString(str);

            JSObjectNK jSArrayNK = (JSObjectNK) decode.recursive_read_fn(enum_obj_array.obj, enum_key_value.value_begin, enum_datatype.dunno);

            Console.WriteLine(new JSEncoder().Encode(jSArrayNK));
        }
    }
}