using System;

namespace Sieve.Tests

{
	[TestClass]
	public class UnitTest1
	{
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
			//Assert.AreEqual(2_038_074_751, sieve.NthPrime(100_000_000)); not required, just a fun challenge
		}

		[TestMethod]
		public void TestEstimation_NLessThanSix_Returns15()
		{
			Assert.AreEqual(15, SieveImplementation.EstimateNthPrime(5));
			Assert.AreEqual(15, SieveImplementation.EstimateNthPrime(4));
			Assert.AreEqual(15, SieveImplementation.EstimateNthPrime(3));
			Assert.AreEqual(15, SieveImplementation.EstimateNthPrime(2));
			Assert.AreEqual(15, SieveImplementation.EstimateNthPrime(1));
			Assert.AreEqual(15, SieveImplementation.EstimateNthPrime(1));
		}

		[TestMethod]
		public void TestEstimation()
		{
			Assert.AreEqual(30, SieveImplementation.EstimateNthPrime(10));
			Assert.AreEqual(17, SieveImplementation.EstimateNthPrime(6));
		}

		[TestMethod]
		public void FindNthPrime_ThrowsArgumentOutOfRangeException_NegativeN()
		{
			ISieve sieve = new SieveImplementation();
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => sieve.NthPrime(-1));
		}

	}
}
