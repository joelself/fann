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
        public TrainingData()
        {
            InternalData = new training_data();
        }
        public TrainingData(training_data data) {
            InternalData = new training_data(data);
        }

        public bool ReadTrainFromFile(string filename)
        {
            return InternalData.read_train_from_file(filename);
        }

        public float[][] GetOutput()
        {
            using (floatArrayArray output = floatArrayArray.frompointer(InternalData.get_output()))
            {
                int length = (int)InternalData.length_train_data();
                int count = (int)InternalData.num_output_train_data();
                float[][] result = new float[length][];
                for (int i = 0; i < length; i++)
                {
                    result[i] = new float[count];
                    using (floatArray inputArray = floatArray.frompointer(output.getitem(i)))
                    {
                        for (int j = 0; j < count; j++)
                        {
                            result[i][j] = inputArray.getitem(j);
                        }
                    }
                }
                return result;
            }
        }
        public float[][] GetInput()
        {
            using (floatArrayArray input = floatArrayArray.frompointer(InternalData.get_input()))
            {
                int length = (int)InternalData.length_train_data();
                int count = (int)InternalData.num_input_train_data();
                float[][] result = new float[length][];
                for (int i = 0; i < length; i++)
                {
                    result[i] = new float[count];
                    using (floatArray inputArray = floatArray.frompointer(input.getitem(i)))
                    {
                        for (int j = 0; j < count; j++)
                        {
                            result[i][j] = inputArray.getitem(j);
                        }
                    }
                }
                return result;
            }
        }

        public uint LengthTrainData()
        {
            return InternalData.length_train_data();
        }
        public void Dispose()
        {
            InternalData.Dispose();
        }
        internal training_data InternalData { get; set; }
    }
}
