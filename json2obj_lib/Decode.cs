using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json2obj_lib
{
    public class Decode2
    {
		public Decode2() { }

		public JsonString jsonString;

		string read_string = "";
		public object recursive_read_fn(enum_obj_array inside_enum_obj_array, enum_key_value expect_enum_key_value, 
			enum_datatype expected_enum_datatype)
		{
			JSObjectNK jSObjectNK = new JSObjectNK();
			JSArrayNK jSArrayNK = new JSArrayNK();

			JSObjectNK jSObjectNK2 = new JSObjectNK();
			JSArrayNK jSArrayNK2 = new JSArrayNK();

			string temp_key = "";
			string temp_value = "";
			
			while (true)
			{
				//temp_string = read_next();
				if (jsonString.readNext()) {
					this.read_string = this.jsonString.currentChar;
				}
				else {
					switch (inside_enum_obj_array)
					{
						case (enum_obj_array.obj):
							return jSObjectNK;
						case (enum_obj_array.array):
							return jSArrayNK;
					}
				}

				switch (read_string)
				{
					case ("{"):
						if (has_write_check(expect_enum_key_value, expected_enum_datatype, ref temp_key, ref temp_value))
						{
							jSObjectNK2 = (JSObjectNK)recursive_read_fn(enum_obj_array.obj, enum_key_value.key_begin, enum_datatype.dunno);
							//*** "string"
							//todo 4 "value"
							key_value_list_insert(inside_enum_obj_array, ref expect_enum_key_value, enum_obj_array.obj,
								ref jSObjectNK, ref jSArrayNK, ref temp_key, ref jSObjectNK2, ref jSArrayNK2, ref temp_value);
						}
						break;
					case ("}"):
						if (has_write_check(expect_enum_key_value, expected_enum_datatype, ref temp_key, ref temp_value))
						{
							//inserire l' oggetto* nella lista (che sia list_obj o list_array) e
							//setObject(ref obj,temp_key, temp_value);//dentro l'"add" verrà determinato il datatype di "valore"

							//todo da valutare
							if (!(jsonString.notevChar.Contains("]") || jsonString.notevChar.Contains("}"))) 
							{
								key_value_list_insert(inside_enum_obj_array, ref expect_enum_key_value, enum_obj_array.str,
								ref jSObjectNK, ref jSArrayNK, ref temp_key, ref jSObjectNK2, ref jSArrayNK2, ref temp_value);
							}

							return jSObjectNK; //terminazione funzione
						}
						break;
					case ("\""):
						if (has_write_check(expect_enum_key_value, expected_enum_datatype, ref temp_key, ref temp_value))
						{
							switch (expect_enum_key_value)
							{
								case (enum_key_value.key_begin):
									expect_enum_key_value = enum_key_value.key_end;
									break;
								case (enum_key_value.key_end):
									//mi aspetto di trovare subito dopo ":" e un datatype qualsiasi (numeric,string,boolean,obj,array,null)
									expect_enum_key_value = enum_key_value.value_begin;//potrei dire che mi aspetto il ":"
									break;
								case (enum_key_value.value_begin)://forse
																  //mi aspetto di trovare un datatype string
									if (expected_enum_datatype == enum_datatype.dunno)
									{
										expected_enum_datatype = enum_datatype._string;//si
									}
									
									temp_value = "";//azzeramento_value
									expect_enum_key_value = enum_key_value.value_end;
									break;
								case (enum_key_value.value_end):
									//che sia un obj o un array mi aspetto di trovare una "," o "}" o "]"
									
									switch (inside_enum_obj_array)
									{
										case (enum_obj_array.obj):
											expect_enum_key_value = enum_key_value.key_begin;
											break;
										case (enum_obj_array.array):
											expect_enum_key_value = enum_key_value.value_begin;
											break;
									}
									break;
							}
							//do_nothing()
						}
						break;
					case (":"):
						if (has_write_check(expect_enum_key_value, expected_enum_datatype, ref temp_key, ref temp_value))
						{
							//todo:
							//bisogna valutare l' eventualità che il "value" non sia una stringa (ma per esempio un double,null,true/false,obj,array)
							// quindi leggere in ogni caso "value" dopo i ":"
							//nel caso abbiamo letto e incomincia la "\"" che determina l' inizio di una stringa
							//basta cancellare tutto quello memorizzato in temp value nel caso non si sapeva che
							//expect_enum_key_value.value e expected_enum_datatype.string
							//quindi basta valorizzare expected_enum_datatype = ?//simbolo provvisiorio per simboleggiare che non conosco il datatype

							expect_enum_key_value = enum_key_value.value_begin;
							expected_enum_datatype = enum_datatype.dunno;
						}
						break;
					case (","):
						if (has_write_check(expect_enum_key_value, expected_enum_datatype, ref temp_key, ref temp_value))
						{
							if (!(jsonString.notevChar.Contains("]") || jsonString.notevChar.Contains("}")))
							{
								key_value_list_insert(inside_enum_obj_array, ref expect_enum_key_value, enum_obj_array.str,
								ref jSObjectNK, ref jSArrayNK, ref temp_key, ref jSObjectNK2, ref jSArrayNK2, ref temp_value);
							}
							expected_enum_datatype = enum_datatype.dunno;
							//inizio nuovi key e value
						}
						break;
					case ("["):
						if (has_write_check(expect_enum_key_value, expected_enum_datatype, ref temp_key, ref temp_value))
						{
						//ArrayList_ arrayList;
						jSArrayNK2 = (JSArrayNK)recursive_read_fn(enum_obj_array.array, enum_key_value.value_begin, enum_datatype.dunno);
							key_value_list_insert(inside_enum_obj_array, ref expect_enum_key_value, enum_obj_array.array,
								ref jSObjectNK, ref jSArrayNK, ref temp_key, ref jSObjectNK2, ref jSArrayNK2, ref temp_value);
						}
						break;
					case ("]"):
						if (has_write_check(expect_enum_key_value, expected_enum_datatype, ref temp_key, ref temp_value))
						{
							//inserire l' oggetto* nella lista (che sia list_obj o list_array) e
							//setArray(ref arrayList,temp_value); //dentro l'"add" verrà determinato il datatype di "valore"

							//todo da valutare
							if (!(jsonString.notevChar.Contains("]") || jsonString.notevChar.Contains("}")))
							{
								key_value_list_insert(inside_enum_obj_array, ref expect_enum_key_value, enum_obj_array.str,
								ref jSObjectNK, ref jSArrayNK, ref temp_key, ref jSObjectNK2, ref jSArrayNK2, ref temp_value);
							}
							return jSArrayNK; //terminazione funzione
						}
						break;
					default:
						//tutte le altre lettere (a,b,c,...)
						has_write_check(expect_enum_key_value, expected_enum_datatype, ref temp_key, ref temp_value);
						break;
				}
			}
		}

		bool has_write_check(enum_key_value expect_enum_key_value, enum_datatype expected_enum_datatype, 
			ref string temp_key, ref string temp_value)
		{
			if ((enum_key_value.key_begin == expect_enum_key_value && read_string.Contains("\"")) ||
				(enum_key_value.value_begin == expect_enum_key_value && read_string.Contains("\""))  ||
				(expect_enum_key_value == enum_key_value.key_end && read_string.Contains("\"") && !jsonString.previousChar.Contains("\\")) ||
				(expect_enum_key_value == enum_key_value.value_end &&  read_string.Contains("\"") && !jsonString.previousChar.Contains("\\")) )
			{
				return true;
			}


			//expected_enum_datatype = enum_datatype.dunno;
			if ((enum_key_value.value_begin == expect_enum_key_value || enum_key_value.key_begin == expect_enum_key_value)
			&& enum_datatype.dunno == expected_enum_datatype) 
            {
				if (read_string.Contains("{") ||
					read_string.Contains("}") ||
					read_string.Contains("[") ||
					read_string.Contains("]") ||
					read_string.Contains(":") ||
					read_string.Contains(",")
					) { return true; }
				temp_value = temp_value + read_string;
				return true;
			}
			//composizione key_value
			switch (expect_enum_key_value)
			{
				case (enum_key_value.key_end):
					if (read_string.Contains("\"") && jsonString.previousChar.Contains("\\"))
					{
						temp_key.Replace("\\", "");
					}
					temp_key = temp_key + read_string;
					return false;
				case (enum_key_value.value_end):
					if (read_string.Contains("\"") && jsonString.previousChar.Contains("\\"))
					{
						temp_value.Replace("\\", "");
					}
					temp_value = temp_value + read_string;
					return false;
				default:
					return true;
			}
		}

		void key_value_list_insert(enum_obj_array inside_enum_obj_array,ref enum_key_value expect_enum_key_value,
			enum_obj_array insert_type, ref JSObjectNK jSObject, ref JSArrayNK jSArrayNK,
			ref string temp_key, ref JSObjectNK jSObject2, ref JSArrayNK jSArrayNK2, ref string temp_value )
		{
			switch (inside_enum_obj_array)
			{
				case (enum_obj_array.obj):
					//inserire l' oggetto* nella lista (che sia list_obj o list_array) e
					if (temp_key == "")
					{
						//non inserisco niente, eccezione . tagSmart
					}

					switch (insert_type)
					{
						case (enum_obj_array.obj):
							//setObject(ref jSObject, temp_key, jSObject2);//dentro l'"add" verrà determinato il datatype di "valore"
							JSObject tempObj = new JSObject();
							tempObj.Key = temp_key;
							tempObj.Value = jSObject2.Value;
							jSObject.Value.Add(tempObj);
							break;
						case (enum_obj_array.array):
							//setObject(ref jSObject, temp_key, jSArrayNK2);//dentro l'"add" verrà determinato il datatype di "valore"
							JSArray tempArr = new JSArray();
							tempArr.Key = temp_key;
							tempArr.Value = jSArrayNK2.Value;
							jSObject.Value.Add(tempArr);
							break;
						case (enum_obj_array.str):
							//setObject(ref jSObject, temp_key, temp_value);//dentro l'"add" verrà determinato il datatype di "valore"
							
							jSObject.Value.Add(ParseGeneric(temp_key,temp_value));
							break;
					}
					
					//aspettarsi nuovo oggetto chiave valore
					expect_enum_key_value = enum_key_value.key_begin;
					break;
				case (enum_obj_array.array):
					//inserire l' oggetto* nella lista (che sia list_obj o list_array) e
					switch (insert_type)
					{
						case (enum_obj_array.obj):
							//setArray(ref jSArrayNK, jSObject2);//dentro l'"add" verrà determinato il datatype di "valore"
							jSArrayNK.Value.Add(jSObject2);

							break;
						case (enum_obj_array.array):
							//setArray(ref jSArrayNK, jSArrayNK2);//dentro l'"add" verrà determinato il datatype di "valore"
							jSArrayNK.Value.Add(jSArrayNK2);
							break;
						case (enum_obj_array.str):
							
							//setArray(ref jSArrayNK, temp_value);//dentro l'"add" verrà determinato il datatype di "valore"
							jSArrayNK.Value.Add(ParseGeneric(temp_value));
							break;
					}
					//aspettarsi nuovo oggetto valore
					expect_enum_key_value = enum_key_value.value_begin;
					break;
			}//fine switch
			temp_key = ""; //azzeramento_key
			temp_value = ""; //azzeramento_value
		}

		private JSGeneric ParseGeneric(string key, string value) {
			JSGenericNK jSGenericNK = ParseGeneric(value);
			if (jSGenericNK is JSBoolNK) {
				JSBoolNK jSBoolNK = (JSBoolNK) jSGenericNK;
				JSBool jSBool = new JSBool();
				jSBool.Key = key;
				jSBool.Value = jSBoolNK.Value;
				return jSBool;
			}
			if (jSGenericNK is JSDoubleNK)
			{
				JSDoubleNK jSDoubleNK = (JSDoubleNK)jSGenericNK;
				JSDouble jSDouble = new JSDouble();
				jSDouble.Key = key;
				jSDouble.Value = jSDoubleNK.Value;
				return jSDouble;
			}
			if (jSGenericNK is JSNullNK)
			{
				JSNullNK JSNullNK = (JSNullNK)jSGenericNK;
				JSNull jSNull = new JSNull();
				jSNull.Key = key;
				//jSNull.Value = JSNullNK.Value;
				return jSNull;
			}
			JSStringNK jSStringNK = (JSStringNK)jSGenericNK;
			JSString jSString = new JSString();
			jSString.Key = key;
			jSString.Value = jSStringNK.Value;
			return jSString;

		}

		private JSGenericNK ParseGeneric(string temp_value)
        {
			string temp_string = temp_value.Trim();
			if (temp_string == "true") {
				JSBoolNK jSBoolNK = new JSBoolNK();
				jSBoolNK.Value = true;
				return jSBoolNK;
			}
			if (temp_string == "false") {
				JSBoolNK jSBoolNK = new JSBoolNK();
				jSBoolNK.Value = false;
				return jSBoolNK;
			}
			if (temp_string == "null") {
				return new JSNullNK();
			}
			//considero le stringhe se iniziano e finiscono con le virgolette""
			if (temp_string.StartsWith("\"") && temp_string.EndsWith("\""))
			{
				JSStringNK jSString = new JSStringNK();
				jSString.Value = temp_string;
				return jSString;
			}
			//provo a trasforamre una stringa senza "" in un double, se non ci riesco
			//la trasformo in stringa accettabile
			string tempString2 = temp_string.Replace(".",",");
			double tempDouble;
			if (double.TryParse(tempString2, out tempDouble))
			{
				JSDoubleNK jSDoubleNK = new JSDoubleNK();
				jSDoubleNK.Value = tempDouble;
				return jSDoubleNK;
			}
			else {
				JSStringNK jSString2 = new JSStringNK();
				jSString2.Value = temp_string;
				return jSString2;
			}
		}

	}
}
