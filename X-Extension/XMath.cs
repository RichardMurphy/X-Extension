using System.IO;
using System.Reflection;
using Microsoft.SmallBasic.Library;

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
        /// <returns>CSV values of all primes within the range.
        /// 
        /// Throws an exception if 'to &lt; from' or 'to &lt; 2'.
        /// 
        /// Will be slower for very large ranges. Less than 2 seconds for 0 to 1,000,000 when written to file.
        /// </returns>        
        /// <example>XMath.PrimeNumbers(0, 1000000)</example>
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
    }    
}
