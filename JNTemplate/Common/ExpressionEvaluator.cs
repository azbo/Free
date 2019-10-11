using System;
using System.Collections.Generic;
using JinianNet.JNTemplate.Exception;

namespace JinianNet.JNTemplate.Common
{
    // Token: 0x02000049 RID: 73
    public class ExpressionEvaluator
    {
        // Token: 0x06000190 RID: 400 RVA: 0x00006120 File Offset: 0x00004320
        private static bool IsOperator(string value)
        {
            switch (value)
            {
                case "||":
                case "|":
                case "&":
                case "&&":
                case ">":
                case ">=":
                case "<":
                case "<=":
                case "==":
                case "!=":
                case "+":
                case "-":
                case "*":
                case "/":
                case "(":
                case ")":
                case "%":
                    return true;
                default:
                    return false;
            }
        }

        // Token: 0x06000191 RID: 401 RVA: 0x0000637C File Offset: 0x0000457C
        private static int GetPriority(string c)
        {
            switch (c)
            {
                case "||":
                case "|":
                case "&":
                case "&&":
                case "Or":
                case "LogicalOr":
                case "LogicAnd":
                case "And":
                    return 5;
                case ">":
                case ">=":
                case "<":
                case "<=":
                case "==":
                case "!=":

                case "GreaterThan":
                case "GreaterThanOrEqual":
                case "LessThan":
                case "LessThanOrEqual":
                case "Equal":
                case "NotEqual":
                    return 6;
                case "+":
                case "-":

                case "Plus":
                case "Minus":
                    return 7;
                case "%":
                case "*":
                case "Percent":
                case "Times":
                case "/":
                case "Divided":
                    return 8;
                default:
                    return 9;
            }
        }

        // Token: 0x06000192 RID: 402 RVA: 0x000067D8 File Offset: 0x000049D8
        private static bool IsNumber(string fullName)
        {
            switch (fullName)
            {
                case "System.Double":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Single":
                case "System.Decimal":
                    return true;
                default:
                    return false;
            }
        }

        // Token: 0x06000193 RID: 403 RVA: 0x000068F4 File Offset: 0x00004AF4
        public static Stack<object> ProcessExpression(string value)
        {
            value = value.Replace("  ", string.Empty);
            List<object> result = new List<object>();
            int i = 0;
            int j;
            for (j = 0; j < value.Length; j++)
            {
                switch (value[j])
                {
                    case '%':
                    case '(':
                    case ')':
                    case '*':
                    case '+':
                    case '-':
                    case '/':
                        if (i < j)
                        {
                            if (value.Substring(i, j - i).IndexOf('.') == -1)
                            {
                                result.Add(int.Parse(value.Substring(i, j - i)));
                            }
                            else
                            {
                                result.Add(double.Parse(value.Substring(i, j - i)));
                            }
                            i = j;
                        }
                        result.Add(OperatorConvert.Parse(value[j].ToString()));
                        i++;
                        break;
                }
            }
            if (i < j)
            {
                result.Add(double.Parse(value.Substring(i, j - i)));
            }
            return ExpressionEvaluator.ProcessExpression(result.ToArray());
        }

        // Token: 0x06000194 RID: 404 RVA: 0x00006A10 File Offset: 0x00004C10
        public static Stack<object> ProcessExpression(object[] value)
        {
            Stack<object> post = new Stack<object>();
            Stack<object> stack = new Stack<object>();
            for (int i = 0; i < value.Length; i++)
            {
                string fullName;
                if (value[i] != null)
                {
                    fullName = value[i].GetType().FullName;
                }
                else
                {
                    fullName = "System.Object";
                    value[i] = null;
                }
                if (fullName != "JinianNet.JNTemplate.Operator")
                {
                    post.Push(value[i]);
                    continue;
                }
                switch (value[i].ToString())
                {
                    case "(":
                    case "LeftParentheses":
                        stack.Push("(");
                        break;
                    case ")":
                    case "RightParentheses":
                        while (stack.Count > 0)
                        {
                            object op;
                            if ((op = stack.Pop()).ToString() == "(")
                            {
                                break;
                            }
                            else
                            {
                                post.Push(op);
                            }
                        }
                        break;
                    case "+":
                    case "-":
                    case "*":
                    case "%":
                    case "/":
                    case "||":
                    case "|":
                    case "&&":
                    case "&":
                    case ">":
                    case ">=":
                    case "<":
                    case "<=":
                    case "==":
                    case "!=":
                    case "Plus":
                    case "Minus":
                    case "Times":
                    case "Percent":
                    case "Divided":
                    case "LogicalOr":
                    case "Or":
                    case "LogicAnd":
                    case "And":
                    case "GreaterThan":
                    case "GreaterThanOrEqual":
                    case "LessThan":
                    case "LessThanOrEqual":
                    case "Equal":
                    case "NotEqual":
                        if (stack.Count == 0)
                        {
                            stack.Push(value[i]);
                        }
                        else
                        {
                            object eX = stack.Peek();
                            object eY = value[i];
                            if (GetPriority(eY.ToString()) > GetPriority(eX.ToString()))
                            {
                                stack.Push(eY);
                            }
                            else
                            {

                                if (eX.ToString() != "(")
                                {
                                    post.Push(stack.Pop());
                                }
                                stack.Push(eY);
                            }
                        }
                        break;
                    default:
                        post.Push(value[i]);
                        break;
                }
            }
            while (stack.Count > 0)
            {
                post.Push(stack.Pop());
            }
            return post;
        }

        // Token: 0x06000195 RID: 405 RVA: 0x000070B0 File Offset: 0x000052B0
        private static Type GetType(object value)
        {
            if (value == null)
            {
                return typeof(object);
            }
            return value.GetType();
        }

        // Token: 0x06000196 RID: 406 RVA: 0x000070C8 File Offset: 0x000052C8
        public static object Calculate(object x, object y, string value)
        {
            if (value == "||")
            {
                return ExpressionEvaluator.CalculateOr(x, y, value);
            }
            if (value == "&&")
            {
                return ExpressionEvaluator.CalculateAnd(x, y, value);
            }
            Type tX = ExpressionEvaluator.GetType(x);
            Type tY = ExpressionEvaluator.GetType(y);
            if (ExpressionEvaluator.IsNumber(tX.FullName) && ExpressionEvaluator.IsNumber(tY.FullName))
            {
                Type t;
                if (tX == tY)
                {
                    t = tX;
                }
                else
                {
                    int i;
                    int j;
                    if (tX.Name[0] == 'U' && tY.Name[0] == 'U')
                    {
                        i = Array.IndexOf<string>(ExpressionEvaluator._uintWeights, tX.FullName);
                        j = Array.IndexOf<string>(ExpressionEvaluator._uintWeights, tY.FullName);
                    }
                    else
                    {
                        if (tX.Name[0] == 'U')
                        {
                            tX = Type.GetType("System." + tX.Name.Remove(0, 1));
                        }
                        if (tY.Name[0] == 'U')
                        {
                            tY = Type.GetType("System." + tY.Name.Remove(0, 1));
                        }
                        i = Array.IndexOf<string>(ExpressionEvaluator._numberWeights, tX.FullName);
                        j = Array.IndexOf<string>(ExpressionEvaluator._numberWeights, tY.FullName);
                    }
                    if (i > j)
                    {
                        t = tX;
                    }
                    else
                    {
                        t = tY;
                    }
                }
                string fullName = t.FullName;
                if (fullName == "System.Double")
                {
                    return ExpressionEvaluator.Calculate(Convert.ToDouble(x.ToString()), Convert.ToDouble(y.ToString()), value);
                }
                if (fullName == "System.Int16")
                {
                    return ExpressionEvaluator.Calculate(Convert.ToInt16(x.ToString()), Convert.ToInt16(y.ToString()), value);
                }
                if (fullName == "System.Int32")
                {
                    return ExpressionEvaluator.Calculate(Convert.ToInt32(x.ToString()), Convert.ToInt32(y.ToString()), value);
                }
                if (fullName == "System.Int64")
                {
                    return ExpressionEvaluator.Calculate(Convert.ToInt64(x.ToString()), Convert.ToInt64(y.ToString()), value);
                }
                if (fullName == "System.Single")
                {
                    return ExpressionEvaluator.Calculate(Convert.ToSingle(x.ToString()), Convert.ToSingle(y.ToString()), value);
                }
                if (!(fullName == "System.Decimal"))
                {
                    return null;
                }
                return ExpressionEvaluator.Calculate(Convert.ToDecimal(x.ToString()), Convert.ToDecimal(y.ToString()), value);
            }
            else
            {
                if (tX.FullName == "System.Boolean" && tY.FullName == "System.Boolean")
                {
                    return ExpressionEvaluator.Calculate((bool)x, (bool)y, value);
                }
                if (tX.FullName == "System.String" && tY.FullName == "System.String")
                {
                    return ExpressionEvaluator.Calculate(x.ToString(), y.ToString(), value);
                }
                if (tX.FullName == "System.DateTime" && tY.FullName == "System.DateTime")
                {
                    return ExpressionEvaluator.Calculate((DateTime)x, (DateTime)y, value);
                }
                switch (value)
                {
                    case "==":
                        return Equals(x, y, tX, tY);
                    case "!=":
                        return !Equals(x, y, tX, tY);
                    case "+":
                        return string.Concat(x.ToString(), y.ToString());
                    case ">=":
                    case ">":
                    case "<=":
                    case "<":
                        if (x != null && y != null)
                        {
                            string strX, strY;
                            if (!string.IsNullOrEmpty(strX = x.ToString())
                                && !string.IsNullOrEmpty(strY = y.ToString())
                                && float.TryParse(strX, out float fx)
                                && float.TryParse(strY, out float fy))
                            {
                                return Calculate(fx, fy, value);
                            }
                        }
                        if (value.Length > 1)
                        {
                            return Equals(x, y, tX, tY);
                        }
                        return false;
                    default:
                        throw new TemplateException(string.Concat("Operator \"", value, "\" can not be applied operand \"object\" and \"object\""));
                }
            }
        }

        // Token: 0x06000197 RID: 407 RVA: 0x0000755D File Offset: 0x0000575D
        private static object CalculateAnd(object x, object y, string value)
        {
            if (!ExpressionEvaluator.CalculateBoolean(x))
            {
                return false;
            }
            if (!ExpressionEvaluator.CalculateBoolean(y))
            {
                return false;
            }
            return true;
        }

        // Token: 0x06000198 RID: 408 RVA: 0x00007583 File Offset: 0x00005783
        private static object CalculateOr(object x, object y, string value)
        {
            if (ExpressionEvaluator.CalculateBoolean(x))
            {
                return true;
            }
            if (ExpressionEvaluator.CalculateBoolean(y))
            {
                return true;
            }
            return false;
        }

        // Token: 0x06000199 RID: 409 RVA: 0x000075AC File Offset: 0x000057AC
        internal static bool CalculateBoolean(object value)
        {
            if (value == null)
            {
                return false;
            }
            switch (value.GetType().FullName)
            {
                case "System.Boolean":
                    return (bool)value;
                case "System.String":
                    return !string.IsNullOrEmpty(value.ToString());
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    return value.ToString() != "0";
                case "System.Decimal":
                    return (decimal)value != 0;
                case "System.Double":
                    return (double)value != 0;
                case "System.Single":
                    return (float)value != 0;
                default:
                    return value != null;
            }
        }

        // Token: 0x0600019A RID: 410 RVA: 0x0000779B File Offset: 0x0000599B
        private static bool Equals(object x, object y, Type tX, Type tY)
        {
            if (x == null || x == null)
            {
                return x == null && y == null;
            }
            return tX.FullName == tX.FullName && x == y;
        }

        // Token: 0x0600019B RID: 411 RVA: 0x000077C8 File Offset: 0x000059C8
        public static object Calculate(Stack<object> value)
        {
            Stack<object> post = new Stack<object>();
            while (value.Count > 0)
            {
                post.Push(value.Pop());
            }
            Stack<object> stack = new Stack<object>();
            while (post.Count > 0)
            {
                object obj = post.Pop();
                if (obj != null && obj.GetType().FullName == "JinianNet.JNTemplate.Operator")
                {
                    object y = stack.Pop();
                    object x = stack.Pop();
                    stack.Push(ExpressionEvaluator.Calculate(x, y, OperatorConvert.ToString((Operator)obj)));
                }
                else
                {
                    stack.Push(obj);
                }
            }
            return stack.Pop();
        }

        // Token: 0x0600019C RID: 412 RVA: 0x0000785B File Offset: 0x00005A5B
        public static object Calculate(object[] value)
        {
            return ExpressionEvaluator.Calculate(ExpressionEvaluator.ProcessExpression(value));
        }

        // Token: 0x0600019D RID: 413 RVA: 0x00007868 File Offset: 0x00005A68
        public static object Calculate(string value)
        {
            return ExpressionEvaluator.Calculate(ExpressionEvaluator.ProcessExpression(value));
        }

        // Token: 0x0600019E RID: 414 RVA: 0x00007878 File Offset: 0x00005A78
        public static object Calculate(bool x, bool y, string value)
        {
            if (value == "==")
            {
                return x == y;
            }
            if (value == "!=")
            {
                return x != y;
            }
            if (value == "||")
            {
                return x || y;
            }
            if (!(value == "&&"))
            {
                throw new TemplateException("Operator \"" + value + "\" can not be applied operand \"Boolean\" and \"Boolean\"");
            }
            return x && y;
        }

        // Token: 0x0600019F RID: 415 RVA: 0x000078FC File Offset: 0x00005AFC
        public static object Calculate(string x, string y, string value)
        {
            if (value == "==")
            {
                return x.Equals(y, Engine.ComparisonIgnoreCase);
            }
            if (value == "!=")
            {
                return !x.Equals(y, Engine.ComparisonIgnoreCase);
            }
            if (!(value == "+"))
            {
                throw new TemplateException("Operator \"" + value + "\" can not be applied operand \"String\" and \"String\"");
            }
            return x + y;
        }

        // Token: 0x060001A0 RID: 416 RVA: 0x00007978 File Offset: 0x00005B78
        public static object Calculate(DateTime x, DateTime y, string value)
        {
            if (value == "==")
            {
                return x == y;
            }
            if (value == "!=")
            {
                return x != y;
            }
            if (value == ">")
            {
                return x > y;
            }
            if (value == ">=")
            {
                return x >= y;
            }
            if (value == "<")
            {
                return x < y;
            }
            if (!(value == "<="))
            {
                throw new TemplateException("Operator \"" + value + "\" can not be applied operand \"DateTime\" and \"DateTime\"");
            }
            return x <= y;
        }

        // Token: 0x060001A1 RID: 417 RVA: 0x00007A38 File Offset: 0x00005C38
        public static object Calculate(double x, double y, string value)
        {
            switch (value)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "%":
                    return x % y;
                case ">=":
                    return x >= y;
                case "<=":
                    return x <= y;
                case "<":
                    return x < y;
                case ">":
                    return x > y;
                case "==":
                    return x == y;
                case "!=":
                    return x != y;
                default:
                    throw new Exception.TemplateException(string.Concat("Operator \"", value, "\" can not be applied operand \"Double\" and \"Double\""));
            }
        }

        // Token: 0x060001A2 RID: 418 RVA: 0x00007C48 File Offset: 0x00005E48
        public static object Calculate(float x, float y, string value)
        {
            switch (value)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "%":
                    return x % y;
                case ">=":
                    return x >= y;
                case "<=":
                    return x <= y;
                case "<":
                    return x < y;
                case ">":
                    return x > y;
                case "==":
                    return x == y;
                case "!=":
                    return x != y;
                default:
                    throw new Exception.TemplateException(string.Concat("Operator \"", value, "\" can not be applied operand \"Single\" and \"Single\""));
            }
        }

        // Token: 0x060001A3 RID: 419 RVA: 0x00007E58 File Offset: 0x00006058
        public static object Calculate(decimal x, decimal y, string value)
        {
            switch (value)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "%":
                    return x % y;
                case ">=":
                    return x >= y;
                case "<=":
                    return x <= y;
                case "<":
                    return x < y;
                case ">":
                    return x > y;
                case "==":
                    return x == y;
                case "!=":
                    return x != y;
                default:
                    throw new Exception.TemplateException(string.Concat("Operator \"", value, "\" can not be applied operand \"Decimal\" and \"Decimal\""));
            }
        }

        // Token: 0x060001A4 RID: 420 RVA: 0x00008094 File Offset: 0x00006294
        public static object Calculate(int x, int y, string value)
        {
            switch (value)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "%":
                    return x % y;
                case ">=":
                    return x >= y;
                case "<=":
                    return x <= y;
                case "<":
                    return x < y;
                case ">":
                    return x > y;
                case "==":
                    return x == y;
                case "!=":
                    return x != y;
                case "|":
                    return x | y;
                case "&":
                    return x & y;

                default:
                    throw new Exception.TemplateException(string.Concat("Operator \"", value, "\" can not be applied operand \"int\" and \"int\""));
            }
        }

        // Token: 0x060001A5 RID: 421 RVA: 0x00008318 File Offset: 0x00006518
        public static object Calculate(long x, long y, string value)
        {
            switch (value)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "%":
                    return x % y;
                case ">=":
                    return x >= y;
                case "<=":
                    return x <= y;
                case "<":
                    return x < y;
                case ">":
                    return x > y;
                case "==":
                    return x == y;
                case "!=":
                    return x != y;
                case "|":
                    return x | y;
                case "&":
                    return x & y;

                default:
                    throw new Exception.TemplateException(string.Concat("Operator \"", value, "\" can not be applied operand \"Int64\" and \"Int64\""));
            }
        }

        // Token: 0x060001A6 RID: 422 RVA: 0x0000859C File Offset: 0x0000679C
        public static object Calculate(short x, short y, string value)
        {
            switch (value)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "%":
                    return x % y;
                case ">=":
                    return x >= y;
                case "<=":
                    return x <= y;
                case "<":
                    return x < y;
                case ">":
                    return x > y;
                case "==":
                    return x == y;
                case "!=":
                    return x != y;
                case "|":
                    return x | y;
                case "&":
                    return x & y;

                default:
                    throw new Exception.TemplateException(string.Concat("Operator \"", value, "\" can not be applied operand \"Int16\" and \"Int16\""));
            }
        }

        // Token: 0x04000094 RID: 148
        private static readonly string[] _numberWeights = new string[]
        {
            "System.Int16",
            "System.Int32",
            "System.Int64",
            "System.Single",
            "System.Double",
            "System.Decimal"
        };

        // Token: 0x04000095 RID: 149
        private static readonly string[] _uintWeights = new string[]
        {
            "System.UInt16",
            "System.UInt32",
            "System.UInt64"
        };

        // Token: 0x02000050 RID: 80
        public enum LetterType
        {
            // Token: 0x0400009C RID: 156
            None,
            // Token: 0x0400009D RID: 157
            Operator,
            // Token: 0x0400009E RID: 158
            LeftParentheses,
            // Token: 0x0400009F RID: 159
            RightParentheses,
            // Token: 0x040000A0 RID: 160
            Number,
            // Token: 0x040000A1 RID: 161
            Other
        }
    }
}
