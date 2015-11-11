using System;
using FannWrapperFloat;
using FannWrapper;

namespace FANNCSharp
{
    /// <summary> A training data float. </summary>
    ///
    /// <remarks> Joel Self, 11/10/2015. </remarks>

    public class TrainingDataFloat : IDisposable
    {
        /// <summary> Default constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public TrainingDataFloat()
        {
            InternalData = new FannWrapperFloat.training_data();
        }

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>

        public TrainingDataFloat(TrainingDataFloat data) {
            InternalData = new FannWrapperFloat.training_data(data.InternalData);
        }

        internal TrainingDataFloat(training_data data)
        {
            InternalData = data;
        }

        /// <summary> Reads train from file. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="filename"> Filename of the file. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>

        public bool ReadTrainFromFile(string filename)
        {
            return InternalData.read_train_from_file(filename);
        }

        /// <summary> Saves the given file. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="filename"> Filename of the file. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>

        public bool Save(string filename)
        {
            return InternalData.save_train(filename);
        }

        /// <summary> Shuffle train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void ShuffleTrainData()
        {
            InternalData.shuffle_train_data();
        }

        /// <summary> Merge train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>

        public void MergeTrainData(TrainingDataFloat data)
        {
            InternalData.merge_train_data(data.InternalData);
        }

        /// <summary> Gets train input. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="position"> The position. </param>
        ///
        /// <returns> An array of float. </returns>

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

        /// <summary> Gets train output. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="position"> The position. </param>
        ///
        /// <returns> An array of float. </returns>

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

        /// <summary> Sets train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="parameter1"> The input. </param>
        /// <param name="output">     The output. </param>

        public void SetTrainData(float[][]input, float[][] output)
        {
            int numData = input.Length;
            int inputSize = input[0].Length;
            int outputSize = output[0].Length;
            using(floatArrayArray inputArray = new floatArrayArray(numData))
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

        /// <summary> Sets train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="num_data"> Number of data. </param>
        /// <param name="input">    The input. </param>
        /// <param name="output">   The output. </param>

        public void SetTrainData(uint num_data, float[] input, float[] output)
        {
            uint numInput = (uint)input.Length / num_data;
            uint numOutput = (uint)output.Length / num_data;
            using(floatArray inputArray = new floatArray((int)(numInput * num_data)))
            using(floatArray outputArray = new floatArray((int)(numOutput * num_data)))
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

        /// <summary> Gets the minimum input. </summary>
        ///
        /// <value> The minimum input. </value>

        public float MinInput
        {
            get
            {
                return InternalData.get_min_input();
            }
        }

        /// <summary> Gets the maximum input. </summary>
        ///
        /// <value> The maximum input. </value>

        public float MaxInput
        {
            get
            {
                return InternalData.get_max_input();
            }
        }

        /// <summary> Gets the minimum output. </summary>
        ///
        /// <value> The minimum output. </value>

        public float MinOutput
        {
            get
            {
                return InternalData.get_min_output();
            }
        }

        /// <summary> Gets the maximum output. </summary>
        ///
        /// <value> The maximum output. </value>

        public float MaxOutput
        {
            get
            {
                return InternalData.get_max_output();
            }
        }

        /// <summary> Scale input train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="new_min"> The new minimum. </param>
        /// <param name="new_max"> The new maximum. </param>

        public void ScaleInputTrainData(float new_min, float new_max)
        {
            InternalData.scale_input_train_data(new_min, new_max);
        }

        /// <summary> Scale output train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="new_min"> The new minimum. </param>
        /// <param name="new_max"> The new maximum. </param>

        public void ScaleOutputTrainData(float new_min, float new_max)
        {
            InternalData.scale_output_train_data(new_min, new_max);
        }

        /// <summary> Subset train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="pos">    The position. </param>
        /// <param name="length"> The length. </param>

        public void SubsetTrainData(uint pos, uint length)
        {
            InternalData.subset_train_data(pos, length);
        }

        internal SWIGTYPE_p_fann_train_data ToFannTrainData()
        {
            return InternalData.to_fann_train_data();
        }

        private float [][] cachedOutput = null;

        /// <summary> Gets the output. </summary>
        ///
        /// <value> The output. </value>

        public float[][] Output
        {
            get {
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

        /// <summary> Gets the input. </summary>
        ///
        /// <value> The input. </value>

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

        /// <summary> Gets the number of inputs. </summary>
        ///
        /// <value> The number of inputs. </value>

        public uint InputCount
        {
            get
            {
                return InternalData.num_input_train_data();
            }
        }

        /// <summary> Gets the number of outputs. </summary>
        ///
        /// <value> The number of outputs. </value>

        public uint OutputCount
        {
            get
            {
                return InternalData.num_output_train_data();
            }
        }

        /// <summary> Saves a train to fixed. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="filename">     Filename of the file. </param>
        /// <param name="decimalPoint"> The decimal point. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>

        public bool SaveTrainToFixed(string filename, uint decimalPoint)
        {
            return InternalData.save_train_to_fixed(filename, decimalPoint);
        }

        /// <summary> Gets the length of the train data. </summary>
        ///
        /// <value> The length of the train data. </value>

        public uint TrainDataLength
        {
            get
            {
                return InternalData.length_train_data();
            }
        }

        /// <summary> Scale train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="new_min"> The new minimum. </param>
        /// <param name="new_max"> The new maximum. </param>

        public void ScaleTrainData(float new_min, float new_max)
        {
            InternalData.scale_train_data(new_min, new_max);
        }

        /// <summary> Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

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
