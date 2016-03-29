using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace SunlightCongress
{
    public class QueryProvider : IQueryProvider
    {
        private string _apiKey;
        public QueryProvider(string apiKey)
        {
            _apiKey = apiKey;
        }
        //public string GetQueryText(Expression expression)
        //{
        //    return this.Translate(expression);
        //}
        //private string Translate(Expression expression)
        //{
        //    return new QueryTranslator().Translate(expression);
        //}
        public object Execute (Expression expression)
        {
            throw new NotImplementedException();
        }
        public TResult Execute<TResult>(Expression expression)
        {
            using (WebClient client = new WebClient())
            {
                //string operation = Translate(expression);
                string response = string.Empty;
                MethodCallExpression methodCall = expression as MethodCallExpression;
                string operation = ExpressionConverter<MethodCallExpression>(methodCall);

                var constant = (methodCall != null) ?
                                (methodCall.Arguments.FirstOrDefault(x => x.NodeType == ExpressionType.Constant) as ConstantExpression).Value.GetType() :
                                expression.Type;
                string path = string.Empty;
                if (constant == typeof(Amendments))
                    path = Settings.AmendmentsUrl;
                else if (constant == typeof(Bills) && operation.Contains("query"))
                    path = Settings.BillsSearchUrl;
                else if (constant == typeof(Bills))
                    path = Settings.BillsUrl;
                else if (constant == typeof(Committees))
                    path = Settings.CommitteesUrl;
                else if (constant == typeof(CongressionalDocuments))
                    path = Settings.CongressionalDocumentsSearchUrl;
                else if (constant == typeof(Districts))
                    path = Settings.DistrictsLocateUrl;
                else if (constant == typeof(Documents))
                    path = Settings.DocumentsSearchUrl;
                else if (constant == typeof(FloorUpdates))
                    path = Settings.FloorUpdatesUrl;
                else if (constant == typeof(Hearings))
                    path = Settings.HearingsUrl;
                else if (constant == typeof(Legislators) && (operation.Contains("zip") || operation.Contains("longitude") || operation.Contains("latitude")))
                    path = Settings.LegislatorsLocateUrl;
                else if (constant == typeof(Legislators))
                    path = Settings.LegislatorsUrl;
                else if (constant == typeof(Nominations))
                    path = Settings.NominationsUrl;
                else if (constant == typeof(UpcomingBills))
                    path = Settings.UpcomingBillsUrl;
                else if (constant == typeof(Votes))
                    path = Settings.VotesUrl;
                client.BaseAddress = string.Format("{0}?apikey={1}&{2}", path, _apiKey, operation);
                response = client.DownloadString(client.BaseAddress);
                var typedResponse = JsonConvert.DeserializeObject<SunlightResponse<TResult>>(response);
                return typedResponse.Results;
            }
        }


        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {       
                return (IQueryable)Activator.CreateInstance(typeof(SunlightData<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            // maybe intercept and swap filter and return types here?
            //return new Amendments()
            return new SunlightData<TElement>(_apiKey, expression);
        }

        private static void VisitMethodCall(MethodCallExpression m, StringBuilder operation)
        {
            ExtractJsonProperties(operation, m.Arguments[0] as MemberExpression);
            if (m.Method.Name == "Contains")
                operation.AppendFormat("__in={0}&", string.Join("|", m.Arguments.Skip(1)));
            else
                throw new Exception(string.Format("Unable to perform {0} action", m.Method.Name));
        }

        private static void ParseBinary(BinaryExpression b, List<Tuple<MemberExpression, Expression, BinaryExpression>> membersandUnarysAndBinarys, List<BinaryExpression> binarys, StringBuilder operation)
        {
            BinaryExpression left = b.Left as BinaryExpression;
            if (b.Right.NodeType.ToString() == "Call")
                VisitMethodCall(b.Right as MethodCallExpression, operation);
            else
                binarys.Add(b.Right as BinaryExpression);
            if (left.NodeType == ExpressionType.AndAlso || left.NodeType == ExpressionType.OrElse)
                ParseBinary(left, membersandUnarysAndBinarys, binarys, operation);
            else
            {
                binarys.Add(b.Left as BinaryExpression);
                foreach (BinaryExpression binary in binarys)
                {
                    membersandUnarysAndBinarys.Add(new Tuple<MemberExpression, Expression, BinaryExpression>(binary.Left as MemberExpression, binary.Right as Expression, binary));
                }
            }

        }

        internal static object GetRightValue(Expression unary, string type)
        {
            if (type == "Nullable`1")
            {
                if (unary.Type.FullName.ToLower().Contains("datetime"))
                    return Expression.Lambda<Func<DateTime?>>(unary).Compile().Invoke();
                else if (unary.Type.FullName.ToLower().Contains("int"))
                    return Expression.Lambda<Func<int?>>(unary).Compile().Invoke();
                else if (unary.Type.FullName.ToLower().Contains("bool"))
                    return Expression.Lambda<Func<bool?>>(unary).Compile().Invoke();
                else if (unary.Type.FullName.ToLower().Contains("double"))
                    return Expression.Lambda<Func<double?>>(unary).Compile().Invoke();
            }
            else if (type.ToLower() == "string[]")
                return Expression.Lambda<Func<string[]>>(unary).Compile().Invoke();
            else if (type.ToLower() == "string")
                return Expression.Lambda<Func<string>>(unary).Compile().Invoke();
            else if (type.ToLower() == "object")
                return null;

            throw new Exception("not a base type");
        }

        internal static string FormatRightValueAsString(object rightValue, string type)
        {
            if (type == "Nullable`1")
            {
                if (rightValue as DateTime? != null)
                {
                    DateTime? newValue = rightValue as DateTime?;
                    return newValue.Value.ToString("yyyy-MM-dd");
                }
                else if (rightValue as int? != null)
                {
                    int? newValue = rightValue as int?;
                    return newValue.Value.ToString();
                }
                else if (rightValue as bool? != null)
                {
                    bool? newValue = rightValue as bool?;
                    return newValue.Value.ToString().ToLower();
                }
            }
            else if (type == "String")
            {
                string newValue = rightValue as string;
                if (Regex.IsMatch(newValue, @"^\d+$")) // check if it's a number, and wrap in quotes (bug in Sunlight)
                    return string.Format("%22{0}%22", newValue);
                else
                    return newValue;
            }
            else if (type == "String[]")
            {
                string[] newValue = rightValue as string[];
                return string.Join("|", newValue);
            }
            return rightValue.ToString().ToLower();
        }

        private static StringBuilder ExtractJsonProperties(StringBuilder operation, MemberExpression item1 = null)
        {
            if (item1.Expression.GetType().Name == "PropertyExpression")
            {
                MemberExpression expression = item1.Expression as MemberExpression;
                ExtractJsonProperties(operation, expression);
                operation.Append(".");
            }
            var attr = item1.Member.GetCustomAttributes(true).ToDictionary(a => a.GetType().Name, a => a);
            if (attr.Keys.Contains("JsonPropertyAttribute"))
            {
                JsonPropertyAttribute attribute = attr["JsonPropertyAttribute"] as JsonPropertyAttribute;
                operation.Append(attribute.PropertyName);
            }
            else
                operation.Append(item1.Member.Name);
            return operation;
        }

        private static string ExpressionConverter<T>(MethodCallExpression methodCall)
        {
            if (methodCall == null)
                return string.Empty;
            //Convert expressions to query strings.
            UnaryExpression unary = methodCall.Arguments.FirstOrDefault(x => x.NodeType == ExpressionType.Quote) as UnaryExpression;

            StringBuilder operation = new StringBuilder();
            if (unary.Operand.NodeType == ExpressionType.Lambda)
            {
                LambdaExpression l = unary.Operand as LambdaExpression;
                BinaryExpression b = l.Body as BinaryExpression;
                List<Tuple<MemberExpression, Expression, BinaryExpression>> membersAndUnarysAndBinarys = new List<Tuple<MemberExpression, Expression, BinaryExpression>>();
                if (b.NodeType == ExpressionType.AndAlso || b.NodeType == ExpressionType.OrElse)
                {
                    List<BinaryExpression> binarys = new List<BinaryExpression>();
                    ParseBinary(b, membersAndUnarysAndBinarys, binarys, operation);
                }
                else
                    membersAndUnarysAndBinarys.Add(new Tuple<MemberExpression, Expression, BinaryExpression>(b.Left as MemberExpression, b.Right as Expression, b));

                foreach (Tuple<MemberExpression, Expression, BinaryExpression> item in membersAndUnarysAndBinarys)
                {
                    string type = item.Item2.Type.Name;
                    var rightValue = GetRightValue(item.Item2, type);

                    //Pull the JsonProperty off of the class and use that for serialization.
                    ExtractJsonProperties(operation, item.Item1);

                    //TODO: Add in, nin, all exists, !exists
                    //https://sunlightlabs.github.io/congress/#operators
                    if (item.Item3.NodeType == ExpressionType.GreaterThan)
                        operation.Append("__gt");
                    if (item.Item3.NodeType == ExpressionType.GreaterThanOrEqual)
                        operation.Append("__gte");
                    if (item.Item3.NodeType == ExpressionType.LessThan)
                        operation.Append("__lt");
                    if (item.Item3.NodeType == ExpressionType.LessThanOrEqual)
                        operation.Append("__lte");
                    if (item.Item3.NodeType == ExpressionType.NotEqual)
                        operation.Append("__not");
                    if (item.Item3.NodeType == ExpressionType.NotEqual && item.Item3.Right.Type.ToString() == "String[]")
                        operation.Append("__nin");
                    if (item.Item3.NodeType == ExpressionType.NotEqual && item.Item3.Right == null)
                        operation.Append("__exists=true");
                    if (item.Item3.NodeType == ExpressionType.Equal && item.Item3.Right.Type.ToString() == "String[]")
                        operation.Append("__all");
                    if (item.Item3.NodeType == ExpressionType.Equal && item.Item3.Right == null)
                        operation.Append("__exists=false");

                    if (item.Item3.Right.ToString() != "null")
                        operation.AppendFormat("={0}{1}", FormatRightValueAsString(rightValue, type), item == membersAndUnarysAndBinarys.Last() ? "" : "&");
                    else
                        operation.Append(item == membersAndUnarysAndBinarys.Last() ? "" : "&");
                }
            }
            return operation.ToString();
        }
    }

    public class SunlightData<T> : IQueryable<T>, IOrderedQueryable<T>
    {
        protected string _apiKey;
        protected Expression _expression;

        public SunlightData(string apiKey, Expression expression)
        {
            _apiKey = apiKey;
            _expression = expression;
        }

        public SunlightData(string apiKey)
        {
            _apiKey = apiKey;
            _expression = Expression.Constant(this);
        }

        public Type ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        public Expression Expression
        {
            get
            {
                return _expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return new QueryProvider(_apiKey);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<System.Collections.IEnumerable>(Expression)).GetEnumerator();
        }
    }

    internal static class TypeSystem
    {
        internal static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }
        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }
            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }
            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }
            return null;
        }
    }

    internal class QueryTranslator : ExpressionVisitor
    {
        //StringBuilder sb;

        //internal QueryTranslator()
        //{
        //}

        //internal string Translate(Expression expression)
        //{
        //    this.sb = new StringBuilder();
        //    this.Visit(expression);
        //    return this.sb.ToString();
        //}

        //private static Expression StripQuotes(Expression e)
        //{
        //    while (e.NodeType == ExpressionType.Quote)
        //    {
        //        e = ((UnaryExpression)e).Operand;
        //    }
        //    return e;
        //}

        //protected override Expression VisitMethodCall(MethodCallExpression m)
        //{
        //    if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
        //    {
        //        this.Visit(m.Arguments[0]);
        //        LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
        //        this.Visit(lambda.Body);
        //        return m;
        //    }
        //    throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
        //}

        //protected override Expression VisitUnary(UnaryExpression u)
        //{
        //    switch (u.NodeType)
        //    {
        //        case ExpressionType.Convert:
        //            //if (u.Operand.Type == typeof(DateTime))
        //            //{
        //            //    System.Collections.ObjectModel.ReadOnlyCollection<Expression> n = (u.Operand as NewExpression).Arguments;
        //            //    DateTime date = (DateTime)Activator.CreateInstance(typeof(DateTime), new object[] { n[0], n[1], n[2] });
        //            //}
        //            this.Visit(u.Operand);
        //            break;
        //        default:
        //            throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
        //    }
        //    return u;
        //}

        //protected override Expression VisitBinary(BinaryExpression b)
        //{
        //    this.Visit(b.Left);
        //    switch (b.NodeType)
        //    {
        //        case ExpressionType.AndAlso:
        //            sb.Append("&");
        //            break;
        //        case ExpressionType.Equal:
        //            if (b.Right.Type.Name.Contains("[]"))
        //                sb.Append("__all=");
        //            else
        //                sb.Append("=");
        //            break;
        //        case ExpressionType.NotEqual:
        //            sb.Append("__not=");
        //            break;
        //        case ExpressionType.LessThan:
        //            sb.Append("__lt=");
        //            break;
        //        case ExpressionType.LessThanOrEqual:
        //            sb.Append("__lte=");
        //            break;
        //        case ExpressionType.GreaterThan:
        //            sb.Append("__gt=");
        //            break;
        //        case ExpressionType.GreaterThanOrEqual:
        //            sb.Append("__gte=");
        //            break;
        //        default:
        //            throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
        //    }
        //    this.Visit(b.Right);
        //    return b;
        //}

        //protected override Expression VisitConstant(ConstantExpression c)
        //{
        //    if (c.Value == null)
        //    {
        //        sb.Append("NULL");
        //    }
        //    else { 
        //        switch (Type.GetTypeCode(c.Value.GetType()))
        //        {
        //            case TypeCode.Boolean:
        //                sb.Append((((bool)c.Value) ? true : false).ToString().ToLower());
        //                break;
        //            case TypeCode.String:
        //                if (Regex.IsMatch(c.Value.ToString(), @"^\d+$"))
        //                    sb.Append(string.Format("%22{0}%22", c.Value));
        //                else
        //                    sb.Append(c.Value);
        //                break;
        //            case TypeCode.DateTime:
        //                sb.Append((c.Value as DateTime?).Value.ToString("yyyy-MM-dd"));
        //                break;
        //            case TypeCode.Object:
        //                break;
        //            default:
        //                sb.Append(c.Value);
        //                break;
        //        }
        //    }
        //    return c;
        //}

        //protected override Expression VisitMember(MemberExpression m)
        //{
        //    if (m.Expression != null)
        //    {
        //        if (m.Expression.NodeType == ExpressionType.MemberAccess)
        //        {
        //            this.Visit(m.Expression);
        //            sb.Append(".");
        //        }
        //        var attr = m.Member.GetCustomAttributes(true).ToDictionary(a => a.GetType().Name, a => a);
        //        if (attr.Keys.Contains("JsonPropertyAttribute"))
        //        {
        //            JsonPropertyAttribute attribute = attr["JsonPropertyAttribute"] as JsonPropertyAttribute;
        //            sb.Append(attribute.PropertyName);
        //        }
        //        return m;
        //    }
        //    throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        //}
        
    }

    //public abstract class ExpressionVisitor
    //{
    //    protected ExpressionVisitor()
    //    {
    //    }

    //    protected virtual Expression Visit(Expression exp)
    //    {
    //        if (exp == null)
    //            return exp;
    //        switch (exp.NodeType)
    //        {
    //            case ExpressionType.Negate:
    //            case ExpressionType.NegateChecked:
    //            case ExpressionType.Not:
    //            case ExpressionType.Convert:
    //            case ExpressionType.ConvertChecked:
    //            case ExpressionType.ArrayLength:
    //            case ExpressionType.Quote:
    //            case ExpressionType.TypeAs:
    //                return this.VisitUnary((UnaryExpression)exp);
    //            case ExpressionType.Add:
    //            case ExpressionType.AddChecked:
    //            case ExpressionType.Subtract:
    //            case ExpressionType.SubtractChecked:
    //            case ExpressionType.Multiply:
    //            case ExpressionType.MultiplyChecked:
    //            case ExpressionType.Divide:
    //            case ExpressionType.Modulo:
    //            case ExpressionType.And:
    //            case ExpressionType.AndAlso:
    //            case ExpressionType.Or:
    //            case ExpressionType.OrElse:
    //            case ExpressionType.LessThan:
    //            case ExpressionType.LessThanOrEqual:
    //            case ExpressionType.GreaterThan:
    //            case ExpressionType.GreaterThanOrEqual:
    //            case ExpressionType.Equal:
    //            case ExpressionType.NotEqual:
    //            case ExpressionType.Coalesce:
    //            case ExpressionType.ArrayIndex:
    //            case ExpressionType.RightShift:
    //            case ExpressionType.LeftShift:
    //            case ExpressionType.ExclusiveOr:
    //                return this.VisitBinary((BinaryExpression)exp);
    //            case ExpressionType.TypeIs:
    //                return this.VisitTypeIs((TypeBinaryExpression)exp);
    //            case ExpressionType.Conditional:
    //                return this.VisitConditional((ConditionalExpression)exp);
    //            case ExpressionType.Constant:
    //                return this.VisitConstant((ConstantExpression)exp);
    //            case ExpressionType.Parameter:
    //                return this.VisitParameter((ParameterExpression)exp);
    //            case ExpressionType.MemberAccess:
    //                return this.VisitMemberAccess((MemberExpression)exp);
    //            case ExpressionType.Call:
    //                return this.VisitMethodCall((MethodCallExpression)exp);
    //            case ExpressionType.Lambda:
    //                return this.VisitLambda((LambdaExpression)exp);
    //            case ExpressionType.New:
    //                return this.VisitNew((NewExpression)exp);
    //            case ExpressionType.NewArrayInit:
    //            case ExpressionType.NewArrayBounds:
    //                return this.VisitNewArray((NewArrayExpression)exp);
    //            case ExpressionType.Invoke:
    //                return this.VisitInvocation((InvocationExpression)exp);
    //            case ExpressionType.MemberInit:
    //                return this.VisitMemberInit((MemberInitExpression)exp);
    //            case ExpressionType.ListInit:
    //                return this.VisitListInit((ListInitExpression)exp);
    //            default:
    //                throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
    //        }
    //    }

    //    protected virtual MemberBinding VisitBinding(MemberBinding binding)
    //    {
    //        switch (binding.BindingType)
    //        {
    //            case MemberBindingType.Assignment:
    //                return this.VisitMemberAssignment((MemberAssignment)binding);
    //            case MemberBindingType.MemberBinding:
    //                return this.VisitMemberMemberBinding((MemberMemberBinding)binding);
    //            case MemberBindingType.ListBinding:
    //                return this.VisitMemberListBinding((MemberListBinding)binding);
    //            default:
    //                throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
    //        }
    //    }

    //    protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
    //    {
    //        ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments);
    //        if (arguments != initializer.Arguments)
    //        {
    //            return Expression.ElementInit(initializer.AddMethod, arguments);
    //        }
    //        return initializer;
    //    }

    //    protected virtual Expression VisitUnary(UnaryExpression u)
    //    {
    //        Expression operand = this.Visit(u.Operand);
    //        if (operand != u.Operand)
    //        {
    //            return Expression.MakeUnary(u.NodeType, operand, u.Type, u.Method);
    //        }
    //        return u;
    //    }

    //    protected virtual Expression VisitBinary(BinaryExpression b)
    //    {
    //        Expression left = this.Visit(b.Left);
    //        Expression right = this.Visit(b.Right);
    //        Expression conversion = this.Visit(b.Conversion);
    //        if (left != b.Left || right != b.Right || conversion != b.Conversion)
    //        {
    //            if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
    //                return Expression.Coalesce(left, right, conversion as LambdaExpression);
    //            else
    //                return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
    //        }
    //        return b;
    //    }

    //    protected virtual Expression VisitTypeIs(TypeBinaryExpression b)
    //    {
    //        Expression expr = this.Visit(b.Expression);
    //        if (expr != b.Expression)
    //        {
    //            return Expression.TypeIs(expr, b.TypeOperand);
    //        }
    //        return b;
    //    }

    //    protected virtual Expression VisitConstant(ConstantExpression c)
    //    {
    //        return c;
    //    }

    //    protected virtual Expression VisitConditional(ConditionalExpression c)
    //    {
    //        Expression test = this.Visit(c.Test);
    //        Expression ifTrue = this.Visit(c.IfTrue);
    //        Expression ifFalse = this.Visit(c.IfFalse);
    //        if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
    //        {
    //            return Expression.Condition(test, ifTrue, ifFalse);
    //        }
    //        return c;
    //    }

    //    protected virtual Expression VisitParameter(ParameterExpression p)
    //    {
    //        return p;
    //    }

    //    protected virtual Expression VisitMemberAccess(MemberExpression m)
    //    {
    //        Expression exp = this.Visit(m.Expression);
    //        if (exp != m.Expression)
    //        {
    //            return Expression.MakeMemberAccess(exp, m.Member);
    //        }
    //        return m;
    //    }

    //    protected virtual Expression VisitMethodCall(MethodCallExpression m)
    //    {
    //        Expression obj = this.Visit(m.Object);
    //        IEnumerable<Expression> args = this.VisitExpressionList(m.Arguments);
    //        if (obj != m.Object || args != m.Arguments)
    //        {
    //            return Expression.Call(obj, m.Method, args);
    //        }
    //        return m;
    //    }

    //    protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
    //    {
    //        List<Expression> list = null;
    //        for (int i = 0, n = original.Count; i < n; i++)
    //        {
    //            Expression p = this.Visit(original[i]);
    //            if (list != null)
    //            {
    //                list.Add(p);
    //            }
    //            else if (p != original[i])
    //            {
    //                list = new List<Expression>(n);
    //                for (int j = 0; j < i; j++)
    //                {
    //                    list.Add(original[j]);
    //                }
    //                list.Add(p);
    //            }
    //        }
    //        if (list != null)
    //        {
    //            return list.AsReadOnly();
    //        }
    //        return original;
    //    }

    //    protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
    //    {
    //        Expression e = this.Visit(assignment.Expression);
    //        if (e != assignment.Expression)
    //        {
    //            return Expression.Bind(assignment.Member, e);
    //        }
    //        return assignment;
    //    }

    //    protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
    //    {
    //        IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings);
    //        if (bindings != binding.Bindings)
    //        {
    //            return Expression.MemberBind(binding.Member, bindings);
    //        }
    //        return binding;
    //    }

    //    protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
    //    {
    //        IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers);
    //        if (initializers != binding.Initializers)
    //        {
    //            return Expression.ListBind(binding.Member, initializers);
    //        }
    //        return binding;
    //    }

    //    protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
    //    {
    //        List<MemberBinding> list = null;
    //        for (int i = 0, n = original.Count; i < n; i++)
    //        {
    //            MemberBinding b = this.VisitBinding(original[i]);
    //            if (list != null)
    //            {
    //                list.Add(b);
    //            }
    //            else if (b != original[i])
    //            {
    //                list = new List<MemberBinding>(n);
    //                for (int j = 0; j < i; j++)
    //                {
    //                    list.Add(original[j]);
    //                }
    //                list.Add(b);
    //            }
    //        }
    //        if (list != null)
    //            return list;
    //        return original;
    //    }

    //    protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
    //    {
    //        List<ElementInit> list = null;
    //        for (int i = 0, n = original.Count; i < n; i++)
    //        {
    //            ElementInit init = this.VisitElementInitializer(original[i]);
    //            if (list != null)
    //            {
    //                list.Add(init);
    //            }
    //            else if (init != original[i])
    //            {
    //                list = new List<ElementInit>(n);
    //                for (int j = 0; j < i; j++)
    //                {
    //                    list.Add(original[j]);
    //                }
    //                list.Add(init);
    //            }
    //        }
    //        if (list != null)
    //            return list;
    //        return original;
    //    }

    //    protected virtual Expression VisitLambda(LambdaExpression lambda)
    //    {
    //        Expression body = this.Visit(lambda.Body);
    //        if (body != lambda.Body)
    //        {
    //            return Expression.Lambda(lambda.Type, body, lambda.Parameters);
    //        }
    //        return lambda;
    //    }

    //    protected virtual NewExpression VisitNew(NewExpression nex)
    //    {
    //        IEnumerable<Expression> args = this.VisitExpressionList(nex.Arguments);
    //        if (args != nex.Arguments)
    //        {
    //            if (nex.Members != null)
    //                return Expression.New(nex.Constructor, args, nex.Members);
    //            else
    //                return Expression.New(nex.Constructor, args);
    //        }
    //        return nex;
    //    }

    //    protected virtual Expression VisitMemberInit(MemberInitExpression init)
    //    {
    //        NewExpression n = this.VisitNew(init.NewExpression);
    //        IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings);
    //        if (n != init.NewExpression || bindings != init.Bindings)
    //        {
    //            return Expression.MemberInit(n, bindings);
    //        }
    //        return init;
    //    }

    //    protected virtual Expression VisitListInit(ListInitExpression init)
    //    {
    //        NewExpression n = this.VisitNew(init.NewExpression);
    //        IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers);
    //        if (n != init.NewExpression || initializers != init.Initializers)
    //        {
    //            return Expression.ListInit(n, initializers);
    //        }
    //        return init;
    //    }

    //    protected virtual Expression VisitNewArray(NewArrayExpression na)
    //    {
    //        IEnumerable<Expression> exprs = this.VisitExpressionList(na.Expressions);
    //        if (exprs != na.Expressions)
    //        {
    //            if (na.NodeType == ExpressionType.NewArrayInit)
    //            {
    //                return Expression.NewArrayInit(na.Type.GetElementType(), exprs);
    //            }
    //            else {
    //                return Expression.NewArrayBounds(na.Type.GetElementType(), exprs);
    //            }
    //        }
    //        return na;
    //    }

    //    protected virtual Expression VisitInvocation(InvocationExpression iv)
    //    {
    //        IEnumerable<Expression> args = this.VisitExpressionList(iv.Arguments);
    //        Expression expr = this.Visit(iv.Expression);
    //        if (args != iv.Arguments || expr != iv.Expression)
    //        {
    //            return Expression.Invoke(expr, args);
    //        }
    //        return iv;
    //    }
    //}
}