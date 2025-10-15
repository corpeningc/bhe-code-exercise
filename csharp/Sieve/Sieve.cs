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
		var primes = Sieve(limit);

		while (primes.Count < n + 1)
		{
			limit = (long)(limit * 1.2);
			primes = Sieve(limit);
		}

		return (long)primes[(int)n];
	}

	public List<double> Sieve(double limit)
	{
		List<double> primes = new List<double>();
		int intLimit = (int)limit;

		if (intLimit < 2)
			return primes;

		bool[] isComposite = new bool[intLimit + 1];

		isComposite[0] = true;
		isComposite[1] = true;

		int sqrtLimit = (int)Math.Sqrt(intLimit);

		for (int i = 2; i <= sqrtLimit; i++)
		{
			if (!isComposite[i])
			{
				for (int j = i * i; j <= intLimit; j += i)
				{
					// Mark composites
					isComposite[j] = true;
				}
			}
		}


		// Collect primes
		for (int i = 2; i <= intLimit; i++)
		{
			if (!isComposite[i])
				primes.Add(i);
		}

		return primes;
	}

	public static long EstimateNthPrime(long n)
	{
		if (n < 6)
		{
			return 15;
		}

		double logN = Math.Log(n);
		double logLogN = Math.Log(logN);

		var estimate = n * (logN + logLogN);
		return (long)Math.Ceiling(estimate * 1.2);
	}
}
