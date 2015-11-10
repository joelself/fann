using System;
using FannWrapperFloat;

namespace FANNCSharp
{
    public class TrainingDataFloat : IDisposable
    {
        public TrainingDataFloat()
        {
            InternalData = new FannWrapperFloat.training_data();
        }
        public TrainingDataFloat(FannWrapperFloat.training_data data) {
            InternalData = new FannWrapperFloat.training_data(data);
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

        public void MergeTrainData(TrainingDataFloat data)
        {
            InternalData.merge_train_data(data.InternalData);
        }

        public float[] GetTrainInput(uint position)
        {
            using (floatArray output = floatArray.frompointer(InternalData.get_train_input(position)))
            {
                float[] result = new float[InputCount];
                for (int i = 0; i < InputCount; i++)
                {
                    result[i] = output.getitem(i);
                }
                return result;
            }
        }

        public float[] GetTrainOutput(uint position)
        {
            using (floatArray output = floatArray.frompointer(InternalData.get_train_input(position)))
            {
                float[] result = new float[OutputCount];
                for (int i = 0; i < OutputCount; i++)
                {
                    result[i] = output.getitem(i);
                }
                return result;
            }
        }

        public void SetTrainData(float[][] input, float[][] output)
        {
            int numData = input.Length;
            int inputSize = input[0].Length;
            int outputSize = output[0].Length;
            using (floatArrayArray inputArray = new floatArrayArray(numData))
            using (floatArrayArray outputArray = new floatArrayArray(numData))
            {
                for (int i = 0; i < numData; i++)
                {
                    floatArray inArray = new floatArray((int)inputSize);
                    floatArray outArray = new floatArray((int)outputSize);
                    inputArray.setitem(i, inArray.cast());
                    outputArray.setitem(i, outArray.cast());
                    for (int j = 0; j < inputSize; j++)
                    {
                        inArray.setitem(j, input[i][j]);
                    }
                    for (int j = 0; j < outputSize; j++)
                    {
                        outArray.setitem(j, output[i][j]);
                    }
                }
                InternalData.set_train_data((uint)numData, (uint)inputSize, inputArray.cast(), (uint)outputSize, outputArray.cast());
            }
        }

        public void set_train_data(uint num_data, float[] input, float[] output)
        {
            uint numInput = (uint)input.Length / num_data;
            uint numOutput = (uint)output.Length / num_data;
            using (floatArray inputArray = new floatArray((int)(numInput * num_data)))
            using (floatArray outputArray = new floatArray((int)(numOutput * num_data)))
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

        internal void CreateTrainFromCallback(uint num_data, uint num_input, uint num_output, SWIGTYPE_p_f_unsigned_int_unsigned_int_unsigned_int_p_float_p_float__void user_function)
        {
            throw new System.NotImplementedException("CreateTrainFromCallback is not implemented yet.");
        }

        public float MinInput
        {
            get
            {
                return InternalData.get_min_input();
            }
        }

        public float MaxInput
        {
            get
            {
                return InternalData.get_max_input();
            }
        }

        public float MinOutput
        {
            get
            {
                return InternalData.get_min_output();
            }
        }

        public float MaxOutput
        {
            get
            {
                return InternalData.get_max_output();
            }
        }

        public void ScaleInputTrainData(float new_min, float new_max)
        {
            InternalData.scale_input_train_data(new_min, new_max);
        }

        public void ScaleOutputTrainData(float new_min, float new_max)
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

        private float[][] cachedOutput = null;
        public float[][] Output
        {
            get
            {
                if (cachedOutput == null)
                {
                    using (floatArrayArray output = floatArrayArray.frompointer(InternalData.get_output()))
                    {
                        int length = (int)InternalData.length_train_data();
                        int count = (int)InternalData.num_output_train_data();
                        cachedOutput = new float[length][];
                        for (int i = 0; i < length; i++)
                        {
                            cachedOutput[i] = new float[count];
                            using (floatArray inputArray = floatArray.frompointer(output.getitem(i)))
                            {
                                for (int j = 0; j < count; j++)
                                {
                                    cachedOutput[i][j] = inputArray.getitem(j);
                                }
                            }
                        }
                    }
                }
                return cachedOutput;
            }
        }

        private float[][] cachedInput = null;
        public float[][] Input
        {
            get
            {
                if (cachedInput == null)
                {
                    using (floatArrayArray input = floatArrayArray.frompointer(InternalData.get_input()))
                    {
                        int length = (int)InternalData.length_train_data();
                        int count = (int)InternalData.num_input_train_data();
                        cachedInput = new float[length][];
                        for (int i = 0; i < length; i++)
                        {
                            cachedInput[i] = new float[count];
                            using (floatArray inputArray = floatArray.frompointer(input.getitem(i)))
                            {
                                for (int j = 0; j < count; j++)
                                {
                                    cachedInput[i][j] = inputArray.getitem(j);
                                }
                            }
                        }
                    }
                }
                return cachedInput;
            }
        }
        
        public uint InputCount
        {
            get
            {
                return InternalData.num_input_train_data();
            }
        }

        public uint OutputCount
        {
            get
            {
                return InternalData.num_output_train_data();
            }
        }

        public bool SaveTrainToFixed(string filename, uint decimalPoint)
        {
            return InternalData.save_train_to_fixed(filename, decimalPoint);
        }

        public uint TrainDataLength
        {
            get
            {
                return InternalData.length_train_data();
            }
        }
        public void ScaleTrainData(float new_min, float new_max)
        {
            InternalData.scale_train_data(new_min, new_max);
        }
        public void Dispose()
        {
            InternalData.Dispose();
        }
        internal FannWrapperFloat.training_data InternalData
        {
            get; set;
        }
    }
}
