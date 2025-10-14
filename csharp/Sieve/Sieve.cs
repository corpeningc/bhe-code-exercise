namespace Sieve;

public interface ISieve
{
	long NthPrime(long n);
}

public class SieveImplementation : ISieve
{
	public long NthPrime(long n)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(n);

		if (n == 0)
			return 2;

		var limit = EstimateNthPrime(n);

		bool[] isComposite = new bool[limit + 1];
		Array.Fill(isComposite, true);

		var primes = new List<int>();

		return 0;
	}

	public List<double> Sieve(double limit)
	{
		List<double> primes = new List<double>();
		return primes;
	}

	public static long EstimateNthPrime(long n)
	{
		if (n < 6)
		{
			return 15;
		}

		double logN = Math.Log(n);

		var estimate = n * (logN * Math.Log(logN));

		return (int)Math.Ceiling(estimate + 10);
	}
}
