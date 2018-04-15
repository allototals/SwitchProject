using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Switch.Service
{
    public class Utility
    {
    }
    //public static class Utility
    //{
    //    public  static string unknownMessage = "Kindly Contact your service provider. An Unexpected error has occured. We apologies for the inconveniency.";
    //}
    //public class NotEmptyOrNull:ConventionInjection
    //{
    //    protected override bool Match(ConventionInfo c)
    //    {
    //        return c.SourceProp.Name == c.TargetProp.Name && c.SourceProp.Value != null;
    //        bool check = ((c.SourceProp.Value != null || !string.IsNullOrEmpty(c.SourceProp.Value.ToString()))  && c.SourceProp.Name == c.TargetProp.Name );
    //     return check;
    //       // throw new NotImplementedException();  
    //    }
    //    protected override object SetValue(ConventionInfo c)
    //    {
    //        //for value types and string just return the value as is
    //        if (c.SourceProp.Type.IsValueType || c.SourceProp.Type == typeof(string)
    //            || c.TargetProp.Type.IsValueType || c.TargetProp.Type == typeof(string))
    //            return c.SourceProp.Value;

    //        //handle arrays
    //        if (c.SourceProp.Type.IsArray)
    //        {
    //            var arr = c.SourceProp.Value as Array;
    //            var clone = Activator.CreateInstance(c.TargetProp.Type, arr.Length) as Array;

    //            for (int index = 0; index < arr.Length; index++)
    //            {
    //                var a = arr.GetValue(index);
    //                if (a.GetType().IsValueType || a.GetType() == typeof(string)) continue;
    //                clone.SetValue(Activator.CreateInstance(c.TargetProp.Type.GetElementType()).InjectFrom<NotEmptyOrNull>(a), index);
    //            }
    //            return clone;
    //        }


    //        if (c.SourceProp.Type.IsGenericType)
    //        {
    //            //handle IEnumerable<> also ICollection<> IList<> List<>
    //            if (c.SourceProp.Type.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IEnumerable)))
    //            {
    //                var t = c.TargetProp.Type.GetGenericArguments()[0];
    //                if (t.IsValueType || t == typeof(string)) return c.SourceProp.Value;

    //                var tlist = typeof(List<>).MakeGenericType(t);
    //                var list = Activator.CreateInstance(tlist);

    //                var addMethod = tlist.GetMethod("Add");
    //                foreach (var o in c.SourceProp.Value as IEnumerable)
    //                {
    //                    var e = Activator.CreateInstance(t).InjectFrom<NotEmptyOrNull>(o);
    //                    addMethod.Invoke(list, new[] { e }); // in 4.0 you can use dynamic and just do list.Add(e);
    //                }
    //                return list;
    //            }

    //            //unhandled generic type, you could also return null or throw
    //            return c.SourceProp.Value;
    //        }

    //        //for simple object types create a new instace and apply the clone injection on it
    //        return Activator.CreateInstance(c.TargetProp.Type)
    //            .InjectFrom<NotEmptyOrNull>(c.SourceProp.Value);
    //    }
    //}

    public class SwapVisitor : ExpressionVisitor
    {
        private readonly Expression from, to;
        public SwapVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
        public static Expression<Func<T, bool>> MergeWithAnd<T>(Expression<Func<T, bool>> firstClause, Expression<Func<T, bool>> secondClause)
        {

            // rewrite e1, using the parameter from e2; "&&"
            var lambda1 = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(
                new SwapVisitor(firstClause.Parameters[0], secondClause.Parameters[0]).Visit(firstClause.Body),
                secondClause.Body), secondClause.Parameters);

            return lambda1;
        }

        public static Expression<Func<T, bool>> MergeWithOr<T>(Expression<Func<T, bool>> firstClause, Expression<Func<T, bool>> secondClause)
        {
            // rewrite e1, using the parameter from e2; "||"
            var lambda2 = Expression.Lambda<Func<T, bool>>(Expression.OrElse(
                new SwapVisitor(firstClause.Parameters[0], secondClause.Parameters[0]).Visit(firstClause.Body),
                secondClause.Body), secondClause.Parameters);

            return lambda2;
        }
    }
}
