using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
using System.Net;

namespace Congress
{
    internal class QueryProvider : IQueryProvider
    {
        private string _apiKey;
        public QueryProvider(string apiKey)
        {
            _apiKey = apiKey;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            // possibly implement a switch statement here to detect class and swap in return class
            //return new Amendments()
            return new SunlightData<TElement>(_apiKey, expression);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            using (WebClient client = new WebClient())
            {
                string response = string.Empty;
                MethodCallExpression methodCall = expression as MethodCallExpression;
                string operation = ExpressionConverter<MethodCallExpression>(methodCall);

                var constant = methodCall.Arguments.FirstOrDefault(x => x.NodeType == ExpressionType.Constant) as ConstantExpression;
                string path = string.Empty;
                if (constant.Value.GetType() == typeof(Amendments))
                    path = "amendments";
                else if (constant.Value.GetType() == typeof(Bills) && operation.Contains("query"))
                    path = "bills/search";
                else if (constant.Value.GetType() == typeof(Bills))
                    path = "bills";
                else if (constant.Value.GetType() == typeof(Committees))
                    path = "committees";
                else if (constant.Value.GetType() == typeof(FloorUpdates))
                    path = "floor_updates";
                else if (constant.Value.GetType() == typeof(Hearings))
                    path = "hearings";
                else if (constant.Value.GetType() == typeof(Legislators))
                    path = "legislators";
                else if (constant.Value.GetType() == typeof(Nominations))
                    path = "nominations";
                else if (constant.Value.GetType() == typeof(UpcomingBills))
                    path = "upcoming_bills";
                else if (constant.Value.GetType() == typeof(Votes))
                    path = "votes";
                client.BaseAddress = string.Format("https://congress.api.sunlightfoundation.com/{2}?apikey={0}&{1}", _apiKey, operation, path);
                response = client.DownloadString(client.BaseAddress);
                var typedResult = JsonConvert.DeserializeObject<SunlightResponse<TResult>>(response);
                // TODO handle .Any() and other return typed calls
                switch (methodCall.Method.Name)
                {
                    case "Where":
                        return typedResult.Results;
                    case "Single":
                        return typedResult.Results;
                    //return typedResult.Results.ToString().Single();
                    case "Any":
                        return typedResult.Results;
                    //return typedResult.Results.ToString().Count() > 0;
                    default:
                        return typedResult.Results;
                }
                
                
            }

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
                foreach(BinaryExpression binary in binarys)
                {
                    membersandUnarysAndBinarys.Add(new Tuple<MemberExpression, Expression, BinaryExpression>(binary.Left as MemberExpression, binary.Right as Expression, binary));
                }
            }
                
        }

        public static object GetRightValue(Expression unary, string type)
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

        public static string FormatRightValueAsString(object rightValue, string type)
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
                
                foreach(Tuple<MemberExpression, Expression, BinaryExpression> item in membersAndUnarysAndBinarys)
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
            // check for where, select, etc.
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
}