namespace Sieve;

public interface ISieve
{
	long NthPrime(long n);
}

public class SieveImplementation : ISieve
{
	// For the last test case in TestNthPrime, I did some research and found that this could be achieved through a segmented sieve.
	// Essentially we would be processing primes in chunks to avoid large memory allocation.
	//
	// Pseudocode for segmented sieve:
	// 1. Use simple sieve to find all primes up to sqrt(limit)
	//
	// 2. Divide range [sqrt(limit), limit] into segments of size ~1MB
	//
	// 3. For each segment [low, high]:
	//    - Create bool array of size (high - low + 1)
	//    - Use small primes to mark composites in this segment
	//    - Mark multiples starting from first multiple of prime >= low
	//    - Collect primes from segment and add to result
	//
	// 4. This keeps memory bounded to ~1MB per segment vs 2GB+ for full array

	public long NthPrime(long n)
	{

		if (n < 0)
			throw new ArgumentOutOfRangeException(nameof(n), "Cannot have a negative n");

		if (n == 0)
			return 2;

		var limit = EstimateNthPrime(n);
		var primes = Sieve(limit);
		long previousLimit = limit;

		while (primes.Count < n + 1)
		{
			limit = (long)(limit * 1.2);

			// Only sieve the new range to avoid redundant calculations
			var newPrimes = SieveRange(previousLimit + 1, limit, primes);
			primes.AddRange(newPrimes);

			previousLimit = limit;
		}

		return (long)primes[(int)n];
	}

	public static List<double> SieveRange(long low, long high, List<double> existingPrimes)
	{
		List<double> newPrimes = new List<double>();
		int rangeSize = (int)(high - low + 1);

		if (rangeSize <= 0)
			return newPrimes;

		bool[] isComposite = new bool[rangeSize];
		int sqrtHigh = (int)Math.Sqrt(high);

		foreach (var prime in existingPrimes)
		{
			int p = (int)prime;
			if (p > sqrtHigh)
				break;

			long firstMultiple = ((low + p - 1) / p) * p;
			if (firstMultiple < p * p)
				firstMultiple = p * p;

			for (long j = firstMultiple; j <= high; j += p)
			{
				isComposite[j - low] = true;
			}
		}

		for (int i = 0; i < rangeSize; i++)
		{
			if (!isComposite[i])
			{
				long candidate = low + i;
				if (candidate > 1)
					newPrimes.Add(candidate);
			}
		}

		return newPrimes;
	}

	public static List<double> Sieve(double limit)
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
			return 15;

		double logN = Math.Log(n);
		double logLogN = Math.Log(logN);

		var estimate = n * (logN + logLogN);

		// Multiplying the estimate by 1.2 tries to give us a buffer so that our estimation is not too small.
		// If our estimation is too small we will have to expand the limit and sieve more
		return (long)Math.Ceiling(estimate * 1.2);
	}
}
