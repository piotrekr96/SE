using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project.Tests
{
    public class DeterministicRandom : Random {

        int index = 0;
        int numToReturn;
        List<int> pseudoNumbers;


        public DeterministicRandom(List<int> numbers) : base()
        {
            this.pseudoNumbers = numbers;
        }
        

        // ignore params
        public override int Next(int x, int y)
        {
            numToReturn = pseudoNumbers[index];
            index++;
            return numToReturn;
        }

    }

}
