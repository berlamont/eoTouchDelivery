using System;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace eoTouchDelivery.ViewModels.Base
{
	public abstract class ExtendedBindableObject : BindableObject
	{
		public void RaisePropertyChanged<T>(Expression<Func<T>> property)
		{
			var name = GetMemberInfo(property).Name;
			OnPropertyChanged(name);
		}

		static MemberInfo GetMemberInfo(Expression expression)
		{
			MemberExpression operand;
			var lambdaExpression = (LambdaExpression) expression;
			var expressionBody = lambdaExpression.Body as UnaryExpression;
			if (expressionBody != null)
			{
				var body = expressionBody;
				operand = (MemberExpression) body.Operand;
			}
			else
			{
				operand = (MemberExpression) lambdaExpression.Body;
			}
			return operand.Member;
		}
	}
}