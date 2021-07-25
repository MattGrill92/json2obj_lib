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
            jSObjectNK.encode(str);
        }
    }
}