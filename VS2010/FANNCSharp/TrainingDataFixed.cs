using System;
using FannWrapperFixed;

namespace FANNCSharp
{
    public class TrainingDataFixed : IDisposable
    {
        public TrainingDataFixed()
        {
            InternalData = new FannWrapperFixed.training_data();
        }
        public TrainingDataFixed(FannWrapperFixed.training_data data) {
            InternalData = new FannWrapperFixed.training_data(data);
        }

        public bool ReadTrainFromFile(string filename)
        {
            return InternalData.read_train_from_file(filename);
        }

        public int[][] GetOutput()
        {
            using (intArrayArray output = intArrayArray.frompointer(InternalData.get_output()))
            {
                int length = (int)InternalData.length_train_data();
                int count = (int)InternalData.num_output_train_data();
                int[][] result = new int[length][];
                for (int i = 0; i < length; i++)
                {
                    result[i] = new int[count];
                    using (intArray inputArray = intArray.frompointer(output.getitem(i)))
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
        public int[][] GetInput()
        {
            using (intArrayArray input = intArrayArray.frompointer(InternalData.get_input()))
            {
                int length = (int)InternalData.length_train_data();
                int count = (int)InternalData.num_input_train_data();
                int[][] result = new int[length][];
                for (int i = 0; i < length; i++)
                {
                    result[i] = new int[count];
                    using (intArray inputArray = intArray.frompointer(input.getitem(i)))
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
        public void Dispose()
        {
            InternalData.Dispose();
        }
        internal FannWrapperFixed.training_data InternalData { get; set; }
    }
}
