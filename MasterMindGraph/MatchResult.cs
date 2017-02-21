using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    public class MatchResult
    {
        private int _numCorrect;
        private int _numSemiCorrect;

        public MatchResult(int numCorrect, int numSemiCorrect)
        {
            _numCorrect = numCorrect;
            _numSemiCorrect = numSemiCorrect;
        }

        public int NumCorrect
        {
            get { return _numCorrect; }
        }

        public int NumSemiCorrect
        {
            get { return _numSemiCorrect; }
        }

        public static bool operator == (MatchResult mr1, MatchResult mr2)
        {
            return (mr1.NumCorrect == mr2.NumCorrect && mr1.NumSemiCorrect == mr2.NumSemiCorrect);
        }

        public static bool operator != (MatchResult mr1, MatchResult mr2)
        {
            return !(mr1 == mr2);
        }
    }
}
