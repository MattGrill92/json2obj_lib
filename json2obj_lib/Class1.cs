using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json2obj_lib
{
    public enum enum_obj_array
    {
        obj,
        array,
        str
    }

    public enum enum_key_value {
        key_begin,
        key_end,
        value_begin,
        value_end
    }

    public enum enum_datatype { 
        _string,
        dunno
    }

}
