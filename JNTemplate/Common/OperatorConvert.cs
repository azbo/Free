using System;

namespace JinianNet.JNTemplate.Common
{
	// Token: 0x0200004A RID: 74
	public class OperatorConvert
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00008894 File Offset: 0x00006A94
		public static string ToString(Operator value)
		{
			switch (value)
			{
			case Operator.Plus:
				return "+";
			case Operator.Minus:
				return "-";
			case Operator.Times:
				return "*";
			case Operator.Percent:
				return "%";
			case Operator.Divided:
				return "/";
			case Operator.LogicalOr:
				return "|";
			case Operator.Or:
				return "||";
			case Operator.LogicAnd:
				return "&";
			case Operator.And:
				return "&&";
			case Operator.GreaterThan:
				return ">";
			case Operator.GreaterThanOrEqual:
				return ">=";
			case Operator.LessThan:
				return "<";
			case Operator.LessThanOrEqual:
				return "<=";
			case Operator.Equal:
				return "==";
			case Operator.NotEqual:
				return "!=";
			case Operator.LeftParentheses:
				return "(";
			case Operator.RightParentheses:
				return ")";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000895C File Offset: 0x00006B5C
		public static Operator Parse(string value)
		{
            switch (value)
            {
                case "+":
                    return Operator.Plus;
                case "-":
                    return Operator.Minus;
                case "*":
                    return Operator.Times;
                case "%":
                    return Operator.Percent;
                case "/":
                    return Operator.Divided;
                case "|":
                    return Operator.LogicalOr;
                case "||":
                    return Operator.Or;
                case "&":
                    return Operator.LogicAnd;
                case "&&":
                    return Operator.And;
                case ">":
                    return Operator.GreaterThan;
                case ">=":
                    return Operator.GreaterThanOrEqual;
                case "<":
                    return Operator.LessThan;
                case "<=":
                    return Operator.LessThanOrEqual;
                case "==":
                    return Operator.Equal;
                case "!=":
                    return Operator.NotEqual;
                case "(":
                    return Operator.LeftParentheses;
                case ")":
                    return Operator.RightParentheses;
                default:
                    return Operator.None;
            }
        }
	}
}
