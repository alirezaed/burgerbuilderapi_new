using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq.Expressions;
using System.Web.Http.Controllers;

namespace BurgerBuilderApi.Tools
{
	public static class Helper
	{
		public static int ToInt(this string input)
		{
			int i = 0;
			int.TryParse(input, out i);
			return i;
		}
		public static int GetInt(object obj)
		{
			if (obj == null)
				return 0;

			if (obj is int)
				return (int)obj;

			int ret;

			if (int.TryParse(obj.ToString(), out ret))
				return ret;
			else
				return 0;
		}
		public static bool IsValidCardNumber(string cardNumber)
		{
			cardNumber = cardNumber.Replace(" ", "");

			//FIRST STEP: Double each digit starting from the right
			int[] doubledDigits = new int[cardNumber.Length / 2];
			int k = 0;
			for (int i = cardNumber.Length - 2; i >= 0; i -= 2)
			{
				int digit = int.Parse(cardNumber[i].ToString());
				doubledDigits[k] = digit * 2;
				k++;
			}

			//SECOND STEP: Add up separate digits
			int total = 0;
			foreach (int i in doubledDigits)
			{
				string number = i.ToString();
				for (int j = 0; j < number.Length; j++)
				{
					total += int.Parse(number[j].ToString());
				}
			}

			//THIRD STEP: Add up other digits
			int total2 = 0;
			for (int i = cardNumber.Length - 1; i >= 0; i -= 2)
			{
				int digit = int.Parse(cardNumber[i].ToString());
				total2 += digit;
			}

			//FOURTH STEP: Total
			int final = total + total2;

			return final % 10 == 0; //Well formed will divide evenly by 10
		}
		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Expression<Func<T, object>> keySelector, bool ascending)
		{
			var selectorBody = keySelector.Body;
			// Strip the Convert expression
			if (selectorBody.NodeType == ExpressionType.Convert)
				selectorBody = ((UnaryExpression)selectorBody).Operand;
			// Create dynamic lambda expression
			var selector = Expression.Lambda(selectorBody, keySelector.Parameters);
			// Generate the corresponding Queryable method call
			var queryBody = Expression.Call(typeof(Queryable),
				ascending ? "OrderBy" : "OrderByDescending",
				new Type[] { typeof(T), selectorBody.Type },
				source.Expression, Expression.Quote(selector));
			return source.Provider.CreateQuery<T>(queryBody);
		}
	}
}
