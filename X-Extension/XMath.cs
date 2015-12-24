using System.IO;
using Microsoft.SmallBasic.Library;
using System.Numerics;


namespace Jibba
{
    /// <summary>
    /// The XMath object provides extra mathematical tools
    /// </summary>
    [SmallBasicType]
    public class XMath
    {
        private static ExceptionX ExceptionX = new ExceptionX();

        /// <summary>
        /// Gets all prime numbers for a given range
        /// </summary>
        /// <param name="from">The integer to start from</param>
        /// <param name="to">The integer to finish at</param>        
        /// <returns>CSV of all primes within the range.
        /// 
        /// Throws an exception if 'to &lt; from' or 'to &lt; 2'.
        /// 
        /// Will be slower for very large ranges. Less than 2 seconds for 0 to 1,000,000 when written to file.
        /// </returns>        
        /// <example>File.WriteContents(filePath, XMath.PrimeNumbers(0, 1000000))</example>
        public static Primitive PrimeNumbers(Primitive from, Primitive to)
        {
            string tempFile = Directory.GetCurrentDirectory() + "\\Prime Numbers.tmp";
            StreamWriter sw = new StreamWriter(tempFile);

            int step = 1;
            bool isPrime;

            try
            {
                if (from < 0 || to < from || to < 2 || from % 1 != 0 || to % 1 != 0)
                    ExceptionX.Handler();                

                if (from <= 2 && from >= 0 && to >= from)
                {
                    sw.Write(2 + ",");
                    from = 3;
                }

                for (int candidate = from; candidate < to; candidate += step)
                {
                    if (from % 2 != 0)
                        step = 2;

                    isPrime = true;
                    for (int divisor = 2; divisor <= System.Math.Sqrt(candidate); divisor++)
                    {
                        if (candidate % divisor == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                        sw.Write(candidate + ",");
                }

                sw.Dispose();
                using (StreamReader sr = new StreamReader(tempFile))
                { return sr.ReadToEnd(); }
            }
            catch
            {
                ExceptionX.Handler();
                return "";
            }

            finally
            {
                System.IO.File.Delete(tempFile);
            }
        }

        /// <summary>
        /// Gets the prime factor(s) for a given value
        /// </summary>
        /// <param name="value">Any positive integer &gt; 1 and &lt; 9,223,372,036,854,775,807
        /// MUST be a numerical string.</param>
        /// <returns>The prime factors as CSV
        /// 
        /// Will be slower for some very large numbers. Less than 1 minute for 9,223,372,036,854,775,806.
        /// </returns>
        /// <example>XMath.PrimeFactors("9223372036854775806")
        /// OR
        /// XMath.PrimeFactors(Controls.GetTextBoxText(txtBox))
        /// </example>
        public static Primitive PrimeFactors(Primitive value)
        {   
            //largest int 9223372036854775806 == upto 9,223,372,036,854,775,806  tt = 1 minute
              
            try
            {
                long orginalValue = System.Convert.ToInt64(value.ToString());

                if (orginalValue < 2 || orginalValue % 1 > 0)
                    ExceptionX.Handler();

                long newValue = orginalValue;
                long total = 1, step = 1;
                string factors = "";
                bool isPrime = true;

                for (long divisor = 2; divisor < orginalValue; divisor += step)
                {
                    if (divisor > 2)
                        step = 2;

                    if (newValue % divisor == 0)
                    {
                        //check if divisor is prime
                        isPrime = true;
                        for (long i = 2; i < System.Math.Sqrt(divisor); i++)
                        {
                            if (divisor % i == 0)
                            {
                                isPrime = false;
                            }
                        }
                        if (isPrime)
                        {
                            factors += divisor.ToString() + ",";
                            newValue = newValue / divisor;
                            total *= divisor;
                            divisor = 1;
                        }
                    }
                    if (total == orginalValue)
                        break;
                }
                if (orginalValue == 2)
                    factors = "2";

                if (factors == "")
                    factors = orginalValue.ToString();

                return factors;                
            }
            catch
            {
                ExceptionX.Handler();
                return "";
            }
        }

        /// <summary>
        /// Gets the Greatest Common Divisor for 2 integers
        /// </summary>
        /// <param name="valueOne">Any integer</param>
        /// <param name="valueTwo">Any integer</param>
        /// <returns>The greatest common divisor</returns>
        /// <example>XMath.GreatestCommonDivisor(15, 2565)</example>
        public static Primitive GreatestCommonDivisor(Primitive valueOne, Primitive valueTwo)
        {
           try
            {
                return BigInteger.GreatestCommonDivisor((int)valueOne, (int)valueTwo).ToString();
            } 

            catch
            {
                ExceptionX.Handler();
                return "";
            }

            
        }
    }    
}
