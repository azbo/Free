using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Fas.Sql
{
    public class Input : DbParameter
    {
        private void SetDbType(string type)
        {
            string boolType = "boolean,bool,box,bytea";
            string int16Type = "tinyint,int2,smallint,int16";
            string int32Type = "int,integer,int32,integer32,number,int4,mediumint,year";
            string longType = "int8,bigint,int64,long,integer64";
            string floatType = "float4";
            string doubleType = "real,float,double,float8";
            string decimalType = "decimal,numeric,interval,lseg,macaddr,money,path,point,polygon";
            string dateTimeType = "datetime,time,timetz,timestamp,smalldatetime";
            if (boolType.Contains(type)) DbType = DbType.Boolean;
            else if (int16Type.Contains(type)) DbType = DbType.Int16;
            else if (int32Type.Contains(type)) DbType = DbType.Int32;
            else if (longType.Contains(type)) DbType = DbType.Int64;
            else if (floatType.Contains(type)) DbType = DbType.Single;
            else if (doubleType.Contains(type)) DbType = DbType.Double;
            else if (decimalType.Contains(type)) DbType = DbType.Decimal;
            else if (type == "date") DbType = DbType.Date;
            else if (type == "datetime2") DbType = DbType.DateTime2;
            else if (type == "datetimeoffset") DbType = DbType.DateTimeOffset;
            else if (dateTimeType.Contains(type)) DbType = DbType.DateTime;
            else DbType = DbType.String;
        }
        public Input(string name, string value, string type = "")
        {
            ParameterName = name;
            Value = value;

            SetDbType(type);
        }
        public override DbType DbType { get; set; }

        [DefaultValue(ParameterDirection.Input)]

        public override ParameterDirection Direction { get; set; }

        public override bool IsNullable { get; set; }

        [DefaultValue("")]
        public override string ParameterName { get; set; }

        [DefaultValue("4000")]
        public override int Size { get; set; }

        public override string SourceColumn { get; set; }

        public override bool SourceColumnNullMapping { get; set; }

        public override object Value { get; set; }

        public override void ResetDbType()
        {
            DbType = DbType.String;
        }
    }
}
