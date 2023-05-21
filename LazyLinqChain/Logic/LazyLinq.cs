using System;
using System.Collections.Generic;
using System.Dynamic;
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

		public static List<(int num1, List<int>)> LinqChainMemoized(List<int> list1, List<int> list2)
		{
			IEnumerable<MemoizedConstructor> enumerable =
				list2.Select(num => MemoizedConstructor.Create(num));

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

	public class MemoizedConstructor
	{
		public static Dictionary<int, MemoizedConstructor> memo =
			new Dictionary<int, MemoizedConstructor>();

		public static MemoizedConstructor Create(int num)
		{
			if (memo.ContainsKey(num))
			{
				return memo[num];
			}
			else
			{
				MemoizedConstructor temp = new MemoizedConstructor(num);
				memo.Add(num, temp);
				return temp;
			}
		}

		public int Num { get; set; }

		private MemoizedConstructor(int num)
		{
			// await Task.Delay(1000); はコンストラクタ内では使えない？
			System.Threading.Thread.Sleep(100);
			Num = num;
		}
	}

}
