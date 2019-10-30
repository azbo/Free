using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Net.Http;
using System.Xml;

namespace Fas.Util
{
    public static class Extend
    {
        public static long ToTimeStamp(this DateTime time) => StringUtil.ToTimeStamp(time);

        public static DateTime ToDateTime(this long timeStamp) => StringUtil.ToDateTime(timeStamp);

        public static DateTime ToDateTime(this string str) => StringUtil.ToDateTime(str, DateTime.MinValue);

        public static DateTime ToDateTime(this string str, DateTime time) => StringUtil.ToDateTime(str, time);
        public static double ToDouble(this string str, double def = 0) => StringUtil.ToDouble(str, def);

        public static Hashtable ToHashtable(this XmlNode node) => XmlUtil.ToHashtable(node);

        public static (string, string) ToKV(this string data, string join) => StringUtil.ToKV(data, join);

        public static List<(string, string)> ToKVList(this string data, string join, string link) => StringUtil.ToKVList(data, join, link);

        public static string Join(this Hashtable data, bool k = true, string join = "=", bool v = true, string link = "&") => StringUtil.Join(data, k, join, v, link);

        public static HttpClient SetAccept(this HttpClient client, string str = "") => StringUtil.SetAccept(client, str);

        public static HttpContent SetHeader(this HttpContent content, params string[] str) => StringUtil.SetHeader(content, str);

        public static DbType ToDbType(this string type)
        {
            string boolType = "boolean,bool,box,bytea";
            string int16Type = "tinyint,int2,smallint,int16";
            string int32Type = "int,integer,int32,integer32,number,int4,mediumint,year";
            string longType = "int8,bigint,int64,long,integer64";
            string floatType = "float4";
            string doubleType = "real,float,double,float8";
            string decimalType = "decimal,numeric,interval,lseg,macaddr,money,path,point,polygon";
            string dateTimeType = "datetime,time,timetz,timestamp,smalldatetime";
            if (boolType.Contains(type)) return DbType.Boolean;
            if (int16Type.Contains(type)) return DbType.Int16;
            if (int32Type.Contains(type)) return DbType.Int32;
            if (longType.Contains(type)) return DbType.Int64;
            if (floatType.Contains(type)) return DbType.Single;
            if (doubleType.Contains(type)) return DbType.Double;
            if (decimalType.Contains(type)) return DbType.Decimal;
            if (type == "date") return DbType.Date;
            if (type == "datetime2") return DbType.DateTime2;
            if (type == "datetimeoffset") return DbType.DateTimeOffset;
            if (dateTimeType.Contains(type)) return DbType.DateTime;
            return DbType.String;
        }
    }
}
