using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json2obj_lib
{
    public class JsonString
    {
        char []json;
        public string currentChar { get; set; }
        public string previousChar { get; set; }
        public string notevChar { get; set; }
        int index;

        public JsonString(string text) {
            this.json = text.ToCharArray();
            this.previousChar = "";
            this.notevChar = "";
            this.index = 0;
        }

        public bool readNext() {
            if (json.Length > index) {
                currentChar = json[index].ToString();
                
                if (index != 0) {
                    previousChar = json[index-1].ToString();

                    if (!(previousChar.Contains(" ") || 
                        previousChar.Contains("\n") || 
                        previousChar.Contains("\t") || 
                        previousChar.Contains("\r"))) 
                    {
                        notevChar = previousChar;
                    }
                }
                
                this.index++;
                return true;
            }
            return false;
        }
    }
}
