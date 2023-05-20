using Microsoft.VisualStudio.TestTools.UnitTesting;
using LazyLinqChain.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace LazyLinqChain.Logic.Tests
{
	[TestClass()]
	public class LazyLinqTests
	{
		[TestMethod()]
		public void LinqChainTest()
		{
			List<int> lis1 = Enumerable.Range(0, 10).ToList();
			List<int> lis2 = Enumerable.Range(0, 10).ToList();

			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			LazyLinq.LinqChain(lis1, lis2);
			stopWatch.Stop();

			// Green
			Assert.IsTrue(stopWatch.Elapsed.TotalSeconds < 12.0);
			// Red
			Assert.IsTrue(stopWatch.Elapsed.TotalSeconds < 2.0);
		}

		[TestMethod()]
		public void LinqChainFastTest()
		{
			List<int> lis1 = Enumerable.Range(0, 10).ToList();
			List<int> lis2 = Enumerable.Range(0, 10).ToList();

			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			LazyLinq.LinqChainFast(lis1, lis2);
			stopWatch.Stop();

			// Green
			Assert.IsTrue(stopWatch.Elapsed.TotalSeconds < 12.0);
			// Green
			Assert.IsTrue(stopWatch.Elapsed.TotalSeconds < 2.0);
		}
	}
}
