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
        public bool Save(string filename)
        {
            return InternalData.save_train(filename);
        }

        public void ShuffleTrainData()
        {
            InternalData.shuffle_train_data();
        }

        public void MergeTrainData(TrainingDataFixed data)
        {
            InternalData.merge_train_data(data.InternalData);
        }

        public int[] GetTrainInput(uint position)
        {
            using (intArray output = intArray.frompointer(InternalData.get_train_input(position)))
            {
                int[] result = new int[NumInput()];
                for (int i = 0; i < NumInput(); i++)
                {
                    result[i] = output.getitem(i);
                }
                return result;
            }
        }

        public int[] GetTrainOutput(uint position)
        {
            using (intArray output = intArray.frompointer(InternalData.get_train_input(position)))
            {
                int[] result = new int[NumOutput()];
                for (int i = 0; i < NumOutput(); i++)
                {
                    result[i] = output.getitem(i);
                }
                return result;
            }
        }

        public void SetTrainData(int[,] input, int[,] output)
        {
            int numData = input.GetLength(0);
            int inputSize = input.GetLength(1);
            int outputSize = output.GetLength(1);
            using (intArrayArray inputArray = new intArrayArray(numData))
            using (intArrayArray outputArray = new intArrayArray(numData))
            {
                for (int i = 0; i < numData; i++)
                {
                    intArray inArray = new intArray((int)inputSize);
                    intArray outArray = new intArray((int)outputSize);
                    inputArray.setitem(i, inArray.cast());
                    outputArray.setitem(i, outArray.cast());
                    for (int j = 0; j < inputSize; j++)
                    {
                        inArray.setitem(j, input[i, j]);
                    }
                    for (int j = 0; j < outputSize; j++)
                    {
                        outArray.setitem(j, output[i, j]);
                    }
                }
                InternalData.set_train_data((uint)numData, (uint)inputSize, inputArray.cast(), (uint)outputSize, outputArray.cast());
            }

        }

        public void set_train_data(uint num_data, int[] input, int[] output)
        {
            uint numInput = (uint)input.Length / num_data;
            uint numOutput = (uint)output.Length / num_data;
            using (intArray inputArray = new intArray((int)(numInput * num_data)))
            using (intArray outputArray = new intArray((int)(numOutput * num_data)))
            {
                for (int i = 0; i < numInput * num_data; i++)
                {
                    inputArray.setitem(i, input[i]);
                }
                for (int i = 0; i < numOutput * num_data; i++)
                {
                    outputArray.setitem(i, output[i]);
                }

                InternalData.set_train_data(num_data, numInput, inputArray.cast(), numOutput, outputArray.cast());
            }
        }

        public void CreateTrainFromCallback(uint num_data, uint num_input, uint num_output, SWIGTYPE_p_f_unsigned_int_unsigned_int_unsigned_int_p_int_p_int__void user_function)
        {
            throw new System.NotImplementedException("CreateTrainFromCallback is not implemented yet.");
        }

        public void ScaleInputTrainData(int new_min, int new_max)
        {
            InternalData.scale_input_train_data(new_min, new_max);
        }

        public void ScaleOutputTrainData(int new_min, int new_max)
        {
            InternalData.scale_output_train_data(new_min, new_max);
        }

        public void SubsetTrainData(uint pos, uint length)
        {
            InternalData.subset_train_data(pos, length);
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
        public void ScaleTrainData(int new_min, int new_max)
        {
            InternalData.scale_train_data(new_min, new_max);
        }
        public void Dispose()
        {
            InternalData.Dispose();
        }
        internal FannWrapperFixed.training_data InternalData
        {
            get;
            protected set;
        }
    }
}
