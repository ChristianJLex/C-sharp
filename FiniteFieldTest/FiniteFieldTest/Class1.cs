using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteField
{

     class Element
    {
        //A simple modulo function. Gives the remainder after division of two integers. Use % instead. 
        public static int Modulo(int a, int b)
        {
            if (a < 0)
            {
                while (a < 0)
                {
                    a = a + b;
                }
            }
            if (a >= 0)
            {
                while (a >= b)
                {
                    a = a - b;
                }
            }
            return a;
        }
        public int size_of_field;
        public int val;

        //Constructor of the Element class:
        public Element(int a_val, int a_size_of_field)
        {
            size_of_field = a_size_of_field;
            val = Modulo(a_val, size_of_field);
        }

        //Addition of finite field elements:
        public static Element operator+ (Element a, Element b)
        {
            if( a.size_of_field != b.size_of_field)
            {
                throw new ArgumentException("The two elements must be of fields of equal size");
            }

            Element sum = new Element(0, a.size_of_field);
            sum.val = Modulo(a.val + b.val, a.size_of_field);
            return sum;
        }

        //Subtraction of finite field elements:
        public static Element operator- (Element a, Element b)
        {
            if (a.size_of_field != b.size_of_field)
            {
                throw new ArgumentException("The two elements must be of fields of equal size");
            }

            Element difference = new Element(0, a.size_of_field);
            difference.val = Modulo(a.val - b.val, a.size_of_field);
            return difference;
        }

        //Multiplication of finite field elements:
        public static Element operator* (Element a, Element b)
        {
            if (a.size_of_field != b.size_of_field)
            {
                throw new ArgumentException("The two elements must be of fields of equal size");
            }
            Element product = new Element(0, a.size_of_field);
            product.val = Modulo(a.val * b.val, a.size_of_field);
            return product;
        }

        // IsPrime checks whether an integer is prime. It is miserably slow - use the Miller-Rabin test or a look-up table 
        // for any application which contains large primes. 
        public static bool IsPrime(int num)
        {
            if (num <= 1) return false;
            if (num == 2) return true; 
            if (num % 2 == 0) return false;
            for(int i = 2; i < Math.Sqrt(num)+1; i++)
            {  
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        
        //GCD gives the greatest common divisor of two integers. This is essential when doing division (Mod n). 
        public static int GCD(int a, int b)
        {
            int A = Math.Max(a, b);
            int B = Math.Min(a, b);
            int remainder = A % B;
            while (remainder > 0)
            {
                A = B;
                B = remainder;
                remainder = A % B;
            }
            return B;
        }

        // EEA (Extended Euclidean Algorithm) gives Bezout coefficients s,t such that gcd(a,b) = as + bt. This is essential for determining
        // multiplicative inverses in finite fields. 
        public static int[] EEA(int a, int b)
        {
            static int EuclideanDivision(int a, int b)
            {
                return (a - (a % b)) / b;
            }
            int tempmax = Math.Max(a, b);
            int tempmin = Math.Min(a, b);  
            a = tempmax;
            b = tempmin;

            int r_old = a;
            int r_new = b;
            int s_old = 1;
            int s_new = 0;
            int t_old = 0;
            int t_new = 1;
            while( r_new != 0)
            {
                int quotient = EuclideanDivision(r_old, r_new);
                int aux_r = r_new;
                r_new = r_old - quotient * aux_r;
                r_old = aux_r;

                int aux_s = s_new;
                s_new = s_old - quotient * aux_s;
                s_old = aux_s;

                int aux_t= t_new;
                t_new = t_old - quotient * aux_t;
                t_old = aux_t;
            }
            int[] BezoutCoefficients = {s_old, t_old };
            return BezoutCoefficients;
        }


        // Inverse gives the multiplicative inverse of an integer a (mod b). Make sure that gcd(a,b) = 1.
        public static Element Inverse(Element a)
        {
            if (GCD(a.val, a.size_of_field) != 1)
            {
                throw new ArgumentException("This element does not have a multiplicative inverse");
            }
            int[] bezout = EEA(a.val, a.size_of_field);
            return new Element(bezout[1], a.size_of_field);
        }


        //The following function checks the number of digits in the base-10 representation of an integer.
        //It is used to properly space the multiplication and addition tables.
        public static int NumOfDigits(int a)
        {
            int remainderRemoved = a - (a % 10);
            int digits = 1;
            while (remainderRemoved > 1)
            {
                digits++;
                remainderRemoved = remainderRemoved / 10;
                remainderRemoved = remainderRemoved - (remainderRemoved % 10);
            }
            return digits;
        }

        //DigitSpace takes an integer and returns a string of the same size of spaces.  
        public static string DigitSpace(int a)
        {
            string space = "";
            for(int i = 0; i < NumOfDigits(a); i++)
            {
                space += " ";
            }
            return space;
        }
        
        //EmptySpace takes an integer a and returns a string of spaces of length a. 
        public static string EmptySpace(int a)
        {
            string space = "";
            for (int i = 0; i < a; i++)
            {
                space += " ";
            }
            return space;
        }

        //MultiplicationTable gives a multiplication table of multiplication (mod a).
        public static void MultiplicationTable(int a)
        {
            if(a < 2)
            {
                throw new ArgumentException("Field size should be larger than 1");
            }
            string max_space_string = DigitSpace(a);
            int max_space_int = NumOfDigits(a);

            Console.Write(max_space_string + "*" + max_space_string);
            for(int i = 0; i < a; i++)
            {
                string space = EmptySpace(max_space_int - NumOfDigits(i + 1) + 1);
                Console.Write(i + space);
            }
            Console.WriteLine();
            for(int i = 0; i < a; i++)
            {
                string ispace = EmptySpace(max_space_int - NumOfDigits(i) + 1);
                Console.Write(ispace + i + max_space_string);
                for(int j = 0; j < a; j++)
                {
                    string jspace = EmptySpace(max_space_int - NumOfDigits((i * (j+1)) % a) + 1 );
                    Console.Write((i * j) % a + jspace);
                }
                Console.WriteLine();
            }
        }

        //AdditionTable gives an addition table of addition (mod a).
        public static void AdditionTable(int a)
        {
            if (a < 2)
            {
                throw new ArgumentException("Field size should be larger than 1");
            }
            string max_space_string = DigitSpace(a);
            int max_space_int = NumOfDigits(a);

            Console.Write(max_space_string + "+" + max_space_string);
            for (int i = 0; i < a; i++)
            {
                string space = EmptySpace(max_space_int - NumOfDigits(i + 1) + 1);
                Console.Write(i + space);
            }
            Console.WriteLine();
            for (int i = 0; i < a; i++)
            {
                string ispace_left = EmptySpace(max_space_int - NumOfDigits(i) + 1);
                string ispace_right = EmptySpace(max_space_int -NumOfDigits(i  % a) + 1);
                Console.Write(ispace_left + i + ispace_right);
                for (int j = 0; j < a; j++)
                {
                    string jspace = EmptySpace(max_space_int - NumOfDigits((i + (j + 1)) % a) + 1);
                    Console.Write((i + j) % a + jspace);
                }
                Console.WriteLine();
            }
        }

        public static int[] RSAKeyGen()
        {

        }


    }
    class Matrix
    {

    }


}
