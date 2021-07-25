using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json2obj_lib
{

    #region DefaultGeneric

    public abstract class JSGeneric//con la chiave
    {
        public string Key { get; set; }
        public JSGeneric() { }
    }
    public class JSNull : JSGeneric { }
    public class JSObject : JSGeneric
    {
        public List<JSGeneric> Value { get; set; }
        public JSObject() => Value = new List<JSGeneric>();
        public JSObject(List<JSGeneric> lst) => Value = lst;
    }
    public class JSArray : JSGeneric
    {
        public List<JSGenericNK> Value { get; set; }
        public JSArray() => Value = new List<JSGenericNK>();
        public JSArray(List<JSGenericNK> lst) => Value = lst;
    }
    public class JSString : JSGeneric
    {
        public string Value { get; set; }
    }
      public class JSDouble : JSGeneric
    {
        public double Value { get; set; }
    }
    public class JSBool : JSGeneric
    {
        public bool Value { get; set; }
    }

    #endregion

    #region GenericNoKey
    public abstract class JSGenericNK//senza la chiave
    {
        public JSGenericNK() { }
    }
    public class JSNullNK : JSGenericNK { }
    public class JSObjectNK : JSGenericNK
    {
        public List<JSGeneric> Value { get; set; }
        public JSObjectNK() => Value = new List<JSGeneric>();
        public JSObjectNK(List<JSGeneric> lst) => Value = lst;
    }
    public class JSArrayNK : JSGenericNK
    {
        public List<JSGenericNK> Value { get; set; }
        public JSArrayNK() => Value = new List<JSGenericNK>();
        public JSArrayNK(List<JSGenericNK> lst) => Value = lst;
    }
    public class JSStringNK : JSGenericNK
    {
        public string Value { get; set; }
    }
    public class JSIntNK : JSGenericNK
    {
        public int Value { get; set; }
    }
    public class JSDoubleNK : JSGenericNK
    {
        public double Value { get; set; }
    }
    public class JSBoolNK : JSGenericNK
    {
        public bool Value { get; set; }
    }

    #endregion
}