using System;
using FannWrapperDouble;

namespace FANNCSharp
{
    public class TrainingDataDouble : IDisposable
    {
        public TrainingDataDouble()
        {
            InternalData = new FannWrapperDouble.training_data();
        }
        public TrainingDataDouble(FannWrapperDouble.training_data data) {
            InternalData = new FannWrapperDouble.training_data(data);
        }

        public bool ReadTrainFromFile(string filename)
        {
            return InternalData.read_train_from_file(filename);
        }

        public double[][] GetOutput()
        {
            using (doubleArrayArray output = doubleArrayArray.frompointer(InternalData.get_output()))
            {
                int length = (int)InternalData.length_train_data();
                int count = (int)InternalData.num_output_train_data();
                double[][] result = new double[length][];
                for (int i = 0; i < length; i++)
                {
                    result[i] = new double[count];
                    using (doubleArray inputArray = doubleArray.frompointer(output.getitem(i)))
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
        public double[][] GetInput()
        {
            using (doubleArrayArray input = doubleArrayArray.frompointer(InternalData.get_input()))
            {
                int length = (int)InternalData.length_train_data();
                int count = (int)InternalData.num_input_train_data();
                double[][] result = new double[length][];
                for (int i = 0; i < length; i++)
                {
                    result[i] = new double[count];
                    using (doubleArray inputArray = doubleArray.frompointer(input.getitem(i)))
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

        public uint NumInput()
        {
            return InternalData.num_input_train_data();
        }

        public uint NumOutput()
        {
            return InternalData.num_output_train_data();
        }

        public bool SaveTrainToFixed(string filename, uint decimalPoint)
        {
            return InternalData.save_train_to_fixed(filename, decimalPoint);
        }

        public uint LengthTrainData()
        {
            return InternalData.length_train_data();
        }

        public void ScaleTrainData(double new_min, double new_max)
        {
            InternalData.scale_train_data(new_min, new_max);
        }

        public void Dispose()
        {
            InternalData.Dispose();
        }
        internal FannWrapperDouble.training_data InternalData { get; set; }
    }
}
