using System;

namespace Sieve.Tests

{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		[DataRow(99, 541)]
		public void TestSpecificNthPrime(int n, int result)
		{
			ISieve sieve = new SieveImplementation();
			Assert.AreEqual(result, sieve.NthPrime(n));
		}

		[TestMethod]
		public void TestNthPrime()
		{
			ISieve sieve = new SieveImplementation();
			Assert.AreEqual(2, sieve.NthPrime(0));
			Assert.AreEqual(71, sieve.NthPrime(19));
			Assert.AreEqual(541, sieve.NthPrime(99));
			Assert.AreEqual(3_581, sieve.NthPrime(500));
			Assert.AreEqual(7_793, sieve.NthPrime(986));
			Assert.AreEqual(17_393, sieve.NthPrime(2_000));
			Assert.AreEqual(15_485_867, sieve.NthPrime(1_000_000));

			Assert.AreEqual(179_424_691, sieve.NthPrime(10_000_000));
			Assert.AreEqual(2_038_074_751, sieve.NthPrime(100_000_000)); // not required, just a fun challenge
		}

		[TestMethod]
		[DataRow(5)]
		[DataRow(4)]
		[DataRow(3)]
		[DataRow(2)]
		[DataRow(1)]
		public void TestEstimation_NLessThanSix_Returns15(int n)
		{
			Assert.AreEqual(15, SieveImplementation.EstimateNthPrime(n));
		}

		[TestMethod]
		public void TestEstimation()
		{
			Assert.AreEqual(38, SieveImplementation.EstimateNthPrime(10));
			Assert.AreEqual(18, SieveImplementation.EstimateNthPrime(6));
		}

		[TestMethod]
		public void FindNthPrime_ThrowsArgumentOutOfRangeException_NegativeN()
		{
			ISieve sieve = new SieveImplementation();
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => sieve.NthPrime(-1));
		}

	}
}
