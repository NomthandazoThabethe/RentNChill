using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class LuhnAlgorithm
    {
        public bool validateCardNumber(string Input)
        {

            //Convert Input to int
            int[] cardInt = new int[Input.Length];

            for (int i = 0; i < Input.Length; i++)
            {
                cardInt[i] = (int)(Input[i] - '0');
            }
            //Starting from the right, double each other digit, if greater than 9, mod 10 and + 1 to remainder
            for (int i = cardInt.Length - 2; i >= 0; i = i - 2)
            {
                int tempValue = cardInt[i];
                tempValue = tempValue * 2;
                if (tempValue > 9)
                {
                    tempValue = tempValue % 10 + 1;
                }
                cardInt[i] = tempValue;
            }
            //Add up all digits
            int total = 0;
            for (int i = 0; i < cardInt.Length; i++)
            {
                total += cardInt[i];
            }

            //If number is multiple of 10 ,it is valid
            if (total % 10 == 0)
            {
                return true;
            }
            return false;
        }
    }
}
