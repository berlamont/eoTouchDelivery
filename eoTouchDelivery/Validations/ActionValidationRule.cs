using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoTouchDelivery.Core.Validations
{
	public class ActionValidationRule<T> : IValidationRule<T>
	{
		private readonly Func<T, bool> _predicate;

		public string ValidationMessage { get; set; }

		public ActionValidationRule(Func<T, bool> predicate, string validationMessage)
		{
			_predicate = predicate;
			ValidationMessage = validationMessage;
		}

		public bool Check(T value)
		{
			return _predicate.Invoke(value);
		}
	}
}
