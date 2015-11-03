using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FannWrap;

namespace FannWrapper
{
    public class TrainingData : IDisposable
    {
        public TrainingData(training_data data) {
            InternalData = data;
        }

        internal  training_data InternalData { get; set; }

        public void Dispose()
        {
            InternalData.Dispose();
        }
    }
}
