﻿namespace PopulationsProtocols
{

    #region Usings

    using System;

    using System.Collections.Generic;
    
    #endregion

    class MyMatrix
    {
        #region Private Fields

        private List<double> _data = new List<double>();
        private int _rows;
        private int _cols;

        #endregion

        #region Constructors and Destructors

        public MyMatrix(int rows, int cols)
        {
            _cols = cols;
            _rows = rows;
        }

        public MyMatrix(MyMatrix m)
        {
            _cols = m.Cols;
            _rows = m.Rows;
            for (int i = 0; i < _rows * _cols; i++) _data.Add(m[0, i]);           
        }

        #endregion

        #region Public Methods and Operators

        public List<double> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public int Cols
        {
            get { return _cols; }
            set { _cols = value; }
        }

        public void Copy(MyMatrix m)
        {
            _rows = m.Rows;
            _cols = m.Cols;
            for (int i = 0; i < _rows * _cols; i++) this[0,i] = m[0, i];
        }
        public void addElement(double element)
        {
            _data.Add(element);
        }

        public double this[int row, int col]
        {
            get { return _data[_cols * row + col]; }
            set { _data[_cols * row + col] = value; }
        }

        public List<double> Gauss()
        {
            var temp = new MyMatrix(this);
            //for (int i = 0; i < _rows * _cols; i++) Console.WriteLine(temp[0, i] == this[0, i]);
            double div = 0;
            int hlp = 0;
            for(int n = 0; n < _rows; n++)
            {
                hlp = 0;
                for (int i = n; i < _rows; i++)
                    if (Math.Abs(this[i, n]) > Math.Abs(this[hlp, n])) hlp = i;
                if (hlp != 0)
                {
                    for(int i = 0; i < _cols; i++)
                    {
                        this[n, i] = temp[hlp, i];
                        this[hlp, i] = temp[n, i];
                    }

                    temp.Copy(this);
                }
                for(int i = n+1; i < _rows; i++)
                {
                    div = temp[i, n] / temp[n, n];
                    for(int j = n; j < _cols; j++)
                    {
                        this[i, j] -= div * temp[n, j];
                    }
                }

                temp.Copy(this);
            }
            var x = new List<double>();
            double value = 0;
            for(int i = 1; i <= _rows; i++)
            {
                value = this[_rows - i, _cols - 1];
                for (int j = 2; j <= i; j++) value -= this[_rows - i, _cols - j] * x[j - 2];
                value /= this[_rows - i, _cols - (i + 1)];
                x.Add(value);
            }
            x.Reverse();
            return x;
        }

        public List<double> Jacobi()
        {
            List<double> xvalues = new List<double>();
            List<double> temp_xvalues = new List<double>();
            for (int i = _rows; i > 0; i--)
            {
                xvalues.Add(0);
                temp_xvalues.Add(0);
            }
            for (int iter = 0; iter < 2; iter++)
            {
                for (int i = 0; i < _rows; i++)
                {
                    xvalues[i] = this[i, _rows];
                    for (int j = 0; j < _rows; j++)
                    {
                        if (i == j) continue;
                        else
                        {
                            xvalues[i] -= this[i, j] * temp_xvalues[j];
                        }
                    }
                    xvalues[i] /= this[i, i];
                }
                for (int i = 0; i < _rows; i++) temp_xvalues[i] = xvalues[i];
            }
            return xvalues;
        }
        public List<double> Gauss_Seidel()
        {
            List<double> xvalues = new List<double>();
            for (int i = _rows; i > 0; i--)
            {
                xvalues.Add(0);
            }
            for (int iter = 0; iter < 2; iter++)
            {
                for (int i = 0; i < _rows; i++)
                {
                    xvalues[i] = this[i, _rows];
                    for (int j = 0; j < _rows; j++)
                    {
                        if (i == j) continue;
                        else
                        {
                            xvalues[i] -= this[i, j] * xvalues[j];
                        }
                    }
                    xvalues[i] /= this[i, i];
                }
            }
            return xvalues;
        }

        #endregion
    }
}
