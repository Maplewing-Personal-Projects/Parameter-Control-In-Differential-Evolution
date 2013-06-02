using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaheuristicsHW3
{
    public class DifferentialEvolution
    {
        int _populationSize;
        int _n;
        int _evaluationTimes;
        int _maxFFE;

        double _maxValue;
        double _minValue;
        double[][] _solutions;
        
        double[] _best;

        double _cr, _f;

        Random _random;
        double[][] _mutantVectors;
        double[][] _trialVectors;

        public Func<IEnumerable<double>, double> Evaluation;

        public DifferentialEvolution(int populationSize, int n, double minValue,
            double maxValue, Func<IEnumerable<double>, double> evalutaion, int maxFFE, double cr, double f)
        {
            _populationSize = populationSize;
            _n = n;
            _solutions = new double[_populationSize][];
            _mutantVectors = new double[_populationSize][];
            _trialVectors = new double[_populationSize][];
            for (int i = 0; i < _solutions.Length; ++i)
            {
                _solutions[i] = new double[_n];
                _mutantVectors[i] = new double[_n];
                _trialVectors[i] = new double[_n];
            }
            _best = new double[_n];
            _minValue = minValue;
            _maxValue = maxValue;

            _random = new Random();

            _maxFFE = maxFFE;

            _cr = cr;
            _f = f;

            Evaluation = evalutaion;
        }

        public double[] Run()
        {
            CreateInitialPopulation();
            while (StopCriteria())
            {
                Mutation();
                Crossover();
                Selection();
            }
            BestUpdate();
            return _best;
        }

        public void BestUpdate()
        {
            double bestValue = Evaluation(_best);
            //++_evaluationTimes;

            for (int i = 0; i < _solutions.Length; ++i)
            {
                double value = Evaluation(_solutions[i]);
                //++_evaluationTimes;

                if (value < bestValue)
                {
                    bestValue = value;
                    _best = (double[])_solutions[i].Clone();
                }
            }
        }


        public void Mutation()
        {
            for (int i = 0; i < _mutantVectors.Length; ++i)
            {
                int r1 = _random.Next(0, _solutions.Length),
                    r2 = _random.Next(0, _solutions.Length),
                    r3 = _random.Next(0, _solutions.Length);
                for (int j = 0; j < _mutantVectors[i].Length; ++j)
                {
                    _mutantVectors[i][j] = _solutions[r1][j] +
                        _f * (_solutions[r2][j] - _solutions[r3][j]);
                    if (_mutantVectors[i][j] > _maxValue) _mutantVectors[i][j] = _maxValue;
                    if (_mutantVectors[i][j] < _minValue) _mutantVectors[i][j] = _minValue;
                }
            }
        }

        public void Crossover()
        {
            for (int i = 0; i < _trialVectors.Length; ++i)
            {
                int rnbr = _random.Next(0, _trialVectors[i].Length);
                for (int j = 0; j < _mutantVectors[i].Length; ++j)
                {
                    if (_random.NextDouble() <= _cr ||
                        j == rnbr)
                    {
                        _trialVectors[i][j] = _mutantVectors[i][j];
                    }
                    else
                    {
                        _trialVectors[i][j] = _solutions[i][j];
                    }
                }   
            }
        }

        public void Selection()
        {
            for (int i = 0; i < _solutions.Length; ++i)
            {
                if (Evaluation(_trialVectors[i]) < Evaluation(_solutions[i]))
                {
                    _solutions[i] = (double[])_trialVectors[i].Clone();
                }
                ++_evaluationTimes;
                ++_evaluationTimes;
            }
        }

        public void CreateInitialPopulation()
        {
            for (int i = 0; i < _solutions.Length; ++i)
            {
                for (int j = 0; j < _solutions[i].Length; ++j)
                {
                    _solutions[i][j] = _random.NextDouble() * (_maxValue - _minValue) + _minValue;
                }
            }
            _best = (double[])_solutions[0].Clone();
        }

        public bool StopCriteria()
        {
            return _evaluationTimes < _maxFFE;
        }
    }
}
