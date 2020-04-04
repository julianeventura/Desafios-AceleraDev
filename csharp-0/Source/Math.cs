using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Math
    {
        public List<int> Fibonacci()
        {
            List<int> fibonacciNum = new List<int> { 0, 1 };

            int num1 = fibonacciNum[0];
            int num2 = fibonacciNum[1];

            for (int result = 0; result < 350; result += 0)
            {
                result = num1 + num2;
                num1 = num2;
                num2 = result;

                if (result < 350)
                {
                    fibonacciNum.Add(result);
                }

            }

            return fibonacciNum;

        }

        public bool IsFibonacci(int numberToTest)
        {
            List<int> fibonacciNum = Fibonacci();
            
            if (fibonacciNum.IndexOf(numberToTest) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
