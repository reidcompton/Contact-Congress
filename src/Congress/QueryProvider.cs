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

namespace Congress
{
    public class QueryProvider : IQueryProvider
    {
        private string _apiKey;
        public QueryProvider(string apiKey)
        {
            _apiKey = apiKey;
        }
        public string GetQueryText(Expression expression)
        {
            return this.Translate(expression);
        }
        private string Translate(Expression expression)
        {
            return new QueryTranslator().Translate(expression);
        }
        public object Execute (Expression expression)
        {
            throw new NotImplementedException();
        }
        public TResult Execute<TResult>(Expression expression)
        {
            using (WebClient client = new WebClient())
            {
                string operation = Translate(expression);
                string response = string.Empty;
                MethodCallExpression methodCall = expression as MethodCallExpression;
                //string operation = ExpressionConverter<MethodCallExpression>(methodCall

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
    }

    internal class InnermostWhereFinder : ExpressionVisitor
    {
        private MethodCallExpression innermostWhereExpression;

        public MethodCallExpression GetInnermostWhere(Expression expression)
        {
            Visit(expression);
            return innermostWhereExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where")
                innermostWhereExpression = expression;

            Visit(expression.Arguments[0]);

            return expression;
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
        StringBuilder sb;

        internal QueryTranslator()
        {
        }

        internal string Translate(Expression expression)
        {
            this.sb = new StringBuilder();
            this.Visit(expression);
            return this.sb.ToString();
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                this.Visit(m.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                this.Visit(lambda.Body);
                return m;
            }

            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Select")
            {
                throw new NotImplementedException();
            }
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Convert:
                    //if (u.Operand.Type == typeof(DateTime))
                    //{
                    //    System.Collections.ObjectModel.ReadOnlyCollection<Expression> n = (u.Operand as NewExpression).Arguments;
                    //    DateTime date = (DateTime)Activator.CreateInstance(typeof(DateTime), new object[] { n[0], n[1], n[2] });
                    //}
                    this.Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            }
            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.AndAlso:
                    sb.Append("&");
                    break;
                case ExpressionType.Equal:
                    sb.Append("=");
                    break;
                case ExpressionType.NotEqual:
                    sb.Append("__not=");
                    break;
                case ExpressionType.LessThan:
                    sb.Append("__lt=");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sb.Append("__lte=");
                    break;
                case ExpressionType.GreaterThan:
                    sb.Append("__gt=");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sb.Append("__gte=");
                    break;
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
            }
            this.Visit(b.Right);
            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c.Value == null)
            {
                sb.Append("NULL");
            }
            else { 
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        sb.Append((((bool)c.Value) ? true : false).ToString().ToLower());
                        break;
                    case TypeCode.String:
                        if (Regex.IsMatch(c.Value.ToString(), @"^\d+$"))
                            sb.Append(string.Format("%22{0}%22", c.Value));
                        else
                            sb.Append(c.Value);
                        break;
                    case TypeCode.DateTime:
                        sb.Append((c.Value as DateTime?).Value.ToString("yyyy-MM-dd"));
                        break;
                    case TypeCode.Object:
                        break;
                    default:
                        sb.Append(c.Value);
                        break;
                }
            }
            return c;
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null)
            {
                if (m.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    this.Visit(m.Expression);
                    sb.Append(".");
                }
                var attr = m.Member.GetCustomAttributes(true).ToDictionary(a => a.GetType().Name, a => a);
                if (attr.Keys.Contains("JsonPropertyAttribute"))
                {
                    JsonPropertyAttribute attribute = attr["JsonPropertyAttribute"] as JsonPropertyAttribute;
                    sb.Append(attribute.PropertyName);
                }
                return m;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }
        private static void ParseBinary(BinaryExpression b, List<Tuple<MemberExpression, Expression, BinaryExpression>> membersandUnarysAndBinarys, List<BinaryExpression> binarys)
        {
            BinaryExpression left = b.Left as BinaryExpression;
            binarys.Add(b.Right as BinaryExpression);
            if (left.NodeType == ExpressionType.AndAlso || left.NodeType == ExpressionType.OrElse)
                ParseBinary(left, membersandUnarysAndBinarys, binarys);
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
                if (unary.Type.FullName.Contains("DateTime"))
                    return Expression.Lambda<Func<DateTime?>>(unary).Compile().Invoke();
                else if (unary.Type.FullName.Contains("Int"))
                    return Expression.Lambda<Func<int?>>(unary).Compile().Invoke();
                else if (unary.Type.FullName.Contains("Bool"))
                    return Expression.Lambda<Func<bool?>>(unary).Compile().Invoke();
            }
            else if (type == "String[]")
                return Expression.Lambda<Func<string[]>>(unary).Compile().Invoke();
            else if (type == "String")
                return Expression.Lambda<Func<string>>(unary).Compile().Invoke();

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
                return string.Format("%22{0}%22", newValue);
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
                    ParseBinary(b, membersAndUnarysAndBinarys, binarys);
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

                    operation.AppendFormat("={0}{1}", FormatRightValueAsString(rightValue, type), item == membersAndUnarysAndBinarys.Last() ? "" : "&");
                }
            }
            return operation.ToString();
        }
    }
}