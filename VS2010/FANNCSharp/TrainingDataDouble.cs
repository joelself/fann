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
        public TrainingDataDouble(TrainingDataDouble data) {
            InternalData = new FannWrapperDouble.training_data(data.InternalData);
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

        public void MergeTrainData(TrainingDataDouble data)
        {
            InternalData.merge_train_data(data.InternalData);
        }

        public double[] GetTrainInput(uint position)
        {
            using (doubleArray output = doubleArray.frompointer(InternalData.get_train_input(position)))
            {
                double[] result = new double[NumInput()];
                for (int i = 0; i < NumInput(); i++)
                {
                    result[i] = output.getitem(i);
                }
                return result;
            }
        }

        public double[] GetTrainOutput(uint position)
        {
            using (doubleArray output = doubleArray.frompointer(InternalData.get_train_input(position)))
            {
                double[] result = new double[NumOutput()];
                for (int i = 0; i < NumOutput(); i++)
                {
                    result[i] = output.getitem(i);
                }
                return result;
            }
        }

        public void SetTrainData(double[,]input, double[,] output)
        {
            int numData = input.GetLength(0);
            int inputSize = input.GetLength(1);
            int outputSize = output.GetLength(1);
            using(doubleArrayArray inputArray = new doubleArrayArray(numData))
            using (doubleArrayArray outputArray = new doubleArrayArray(numData))
            {
                for (int i = 0; i < numData; i++)
                {
                    doubleArray inArray = new doubleArray((int)inputSize);
                    doubleArray outArray = new doubleArray((int)outputSize);
                    inputArray.setitem(i, inArray.cast());
                    outputArray.setitem(i, outArray.cast());
                    for (int j = 0; j < inputSize; j++)
                    {
                        inArray.setitem(j, input[i,j]);
                    }
                    for (int j = 0; j < outputSize; j++)
                    {
                        outArray.setitem(j, output[i,j]);
                    }
                }
                InternalData.set_train_data((uint)numData, (uint)inputSize, inputArray.cast(), (uint)outputSize, outputArray.cast());
            }

        }

        public void set_train_data(uint num_data, double[] input, double[] output)
        {
            uint numInput = (uint)input.Length / num_data;
            uint numOutput = (uint)output.Length / num_data;
            using(doubleArray inputArray = new doubleArray((int)(numInput * num_data)))
            using(doubleArray outputArray = new doubleArray((int)(numOutput * num_data)))
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

        public void CreateTrainFromCallback(uint num_data, uint num_input, uint num_output, SWIGTYPE_p_f_unsigned_int_unsigned_int_unsigned_int_p_double_p_double__void user_function)
        {
            throw new System.NotImplementedException("CreateTrainFromCallback is not implemented yet.");
        }

        public double GetMinInput()
        {
            return InternalData.get_min_input();
        }

        public double get_max_input()
        {
            return InternalData.get_max_input();
        }

        public double get_min_output()
        {
            return InternalData.get_min_output();
        }

        public double get_max_output()
        {
            return InternalData.get_max_output();
        }

        public void ScaleInputTrainData(double new_min, double new_max)
        {
            InternalData.scale_input_train_data(new_min, new_max);
        }

        public void ScaleOutputTrainData(double new_min, double new_max)
        {
            InternalData.scale_output_train_data(new_min, new_max);
        }

        public void SubsetTrainData(uint pos, uint length)
        {
            InternalData.subset_train_data(pos, length);
        }

        internal fann_train_data ToFannTrainData()
        {
            return InternalData.to_fann_train_data();
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
        internal FannWrapperDouble.training_data InternalData
        { 
            get;
            protected set;
        }
    }
}
