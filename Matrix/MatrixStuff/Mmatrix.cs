using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixStuff
{   
    // Vector class. 
    class Vec
    {
        public int Length;
        public double[] Cont;

        public Vec(double[] aCont)
        {
            Cont = aCont;
            Length = aCont.Length;
        }


        //Sum of vectors://
        public static Vec operator +(Vec u, Vec v)
        {
            if(u.Length != v.Length)
            {
                throw new InvalidOperationException("Length of vectors does not match");
            }
            double[] Cont = new double[u.Length];
            for(int i = 0; i<u.Length; i++)
            {
                Cont[i] = u.Cont[i]+v.Cont[i];
            }
            return new Vec(Cont);
        }


        //Difference of vectors://
        public static Vec operator -(Vec u, Vec v)
        {
            if (u.Length != v.Length)
            {
                throw new InvalidOperationException("Length of vectors does not match");
            }
            double[] Cont = new double[u.Length];
            for (int i = 0; i < u.Length; i++)
            {
                Cont[i] = u.Cont[i] - v.Cont[i];
            }
            return new Vec(Cont);
        }

        //Takes vector of length n and returns size n x 1 matrix//
        public Mmatrix AsMatrix()
        {
            double[][] mCont = new double[Cont.Length][];
            for(int i = 0; i<Cont.Length; i++)
            {
                mCont[i] = new double[1] { Cont[i] };
            }
            Mmatrix asMatrix = new Mmatrix(mCont);
            return asMatrix;
        }

        public void Print()
        {
            Vec.AsMatrix().Print();
        }
    }
    


    //Matrix class. Constructed as jagged arrays 
    class Mmatrix
    {
        public int nRow;
        public int nCol;
        public double[][] Cont;

        public double[][] emptyCont = Array.Empty<double[]>();
        
        

        //Matrix constructor
        public Mmatrix(double[][] aCont)
        {   
            Cont = aCont;
            nRow = Cont.Length;
            nCol = Cont[0].Length; 
        }


        //Returns the transpose of a matrix. 
        public Mmatrix Transpose()
        {
            double[][] transCont = new double[nCol][];
                for(int i = 0; i<nCol; i++)
                {   
                Console.WriteLine(Cont[i]);
                double[] temp = new double[nRow];
                    for (int j = 0; j < nRow; j++)
                    {
                        temp[j] = Cont[j][i];
                    }
                    transCont[i] = temp;
                }
                return new Mmatrix(transCont);
   
        }
        

       
        //Prints a matrix
        public void Print()
        {
            for(int i = 0; i<nRow; i++)
            {
                for(int j = 0; j<nCol; j++)
                {
                    Console.Write( " " +  Cont[i][j] + " ");
                }
                Console.WriteLine();
            }
        }


        //Sum of matrices. 
        public static Mmatrix operator+ (Mmatrix A, Mmatrix B)
        {
            if(A.nRow != B.nRow || A.nCol != B.nCol)
            {
                throw new InvalidOperationException("Dimensions of matrices does not match");
            }
            double[][] cont = new double[A.nRow][];
            for (int i = 0; i<A.nRow; i++)
            {   
                double[] temp = new double[A.nCol];
                for(int j = 0; j<A.nCol; j++)
                {
                    temp[j] = A.Cont[i][j] + B.Cont[i][j];
                }
                cont[i] = temp;
            }
            Mmatrix Res = new Mmatrix(cont);
            return Res;
        }

        //Difference of matrices
        public static Mmatrix operator -(Mmatrix A, Mmatrix B)
        {
            if (A.nRow != B.nRow || A.nCol != B.nCol)
            {
                throw new InvalidOperationException("Dimensions of matrices does not match");
            }
            double[][] cont = new double[A.nRow][];
            for (int i = 0; i < A.nRow; i++)
            {
                double[] temp = new double[A.nCol];
                for (int j = 0; j < A.nCol; j++)
                {
                    temp[j] = A.Cont[i][j] - B.Cont[i][j];
                }
                cont[i] = temp;
            }
            Mmatrix Res = new Mmatrix(cont);
            return Res;
        }


        //Product of Matrices
        public static Mmatrix operator* (Mmatrix A, Mmatrix B)
        {
            if(A.nCol != B.nRow)
            {
                throw new InvalidOperationException("Dimensions of matrices does not match");
            }
            double[][] cont = new double[A.nRow][];
            for(int i = 0; i < A.nRow;i++)
            {
                double[] tempL = new double[B.nCol];
                for(int j = 0; j<B.nCol; j++)
                {
                    double tempN;

                }
            }
        }

        
    }
}
