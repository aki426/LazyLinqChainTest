using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLinqChain.Logic
{
	public class LazyLinq
	{
		public static List<(int num1, List<int>)> LinqChain(List<int> list1, List<int> list2)
		{
			IEnumerable<LongTimeConstructor> enumerable =
				list2.Select(num => new LongTimeConstructor(num));

			List<(int num1, List<int>)> result = list1.Select(num1 => (
				num1,
				enumerable.Where(num2 => num2.Num == num1).Select(num2 => num2.Num * num1).ToList()
			)).ToList();

			return result;
		}

		public static List<(int num1, List<int>)> LinqChainFast(List<int> list1, List<int> list2)
		{
			List<LongTimeConstructor> enumerable =
				list2.Select(num => new LongTimeConstructor(num)).ToList();

			List<(int num1, List<int>)> result = list1.Select(num1 => (
				num1,
				enumerable.Where(num2 => num2.Num == num1).Select(num2 => num2.Num * num1).ToList()
			)).ToList();

			return result;
		}
	}

	public class LongTimeConstructor
	{
		public int Num { get; set; }

		public LongTimeConstructor(int num)
		{
			// await Task.Delay(1000); はコンストラクタ内では使えない？
			System.Threading.Thread.Sleep(100);
			Num = num;
		}
	}
}
