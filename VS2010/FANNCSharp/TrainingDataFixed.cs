using System;
using FannWrapperFixed;
using FannWrapper;
using System.Runtime.InteropServices;

namespace FANNCSharp
{
    /// <summary> A training data fixed. </summary>
    ///
    /// <remarks> Joel Self, 11/10/2015. </remarks>

    public class TrainingDataFixed : IDisposable
    {
        /// <summary> Default constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public TrainingDataFixed()
        {
            InternalData = new FannWrapperFixed.training_data();
        }

        internal TrainingDataFixed(training_data other)
        {
            InternalData = other;
        }

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>

        public TrainingDataFixed(TrainingDataFixed data)
        {
            InternalData = new FannWrapperFixed.training_data(data.InternalData);
        }

        public TrainingDataFixed(uint dataCount, uint inputCount, uint outputCount, DataCreateCallback callback)
        {
            InternalData = new FannWrapperFixed.training_data();
            Callback = callback;
            RawCallback = new data_create_callback(InternalCallback);
            fannfixedPINVOKE.training_data_create_train_from_callback(training_data.getCPtr(this.InternalData), dataCount, inputCount, outputCount, Marshal.GetFunctionPointerForDelegate(RawCallback));
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

        public void MergeTrainData(TrainingDataFixed data)
        {
            InternalData.merge_train_data(data.InternalData);
        }

        /// <summary> Gets train input. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="position"> The position. </param>
        ///
        /// <returns> An array of int. </returns>

        public int[] GetTrainInput(uint position)
        {
            using (intArray output = intArray.frompointer(InternalData.get_train_input(position)))
            {
                int[] result = new int[InputCount];
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
        /// <returns> An array of int. </returns>

        public int[] GetTrainOutput(uint position)
        {
            using (intArray output = intArray.frompointer(InternalData.get_train_input(position)))
            {
                int[] result = new int[OutputCount];
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

        public void SetTrainData(int[][]input, int[][] output)
        {
            int numData = input.Length;
            int inputSize = input[0].Length;
            int outputSize = output[0].Length;
            using(intArrayArray inputArray = new intArrayArray(numData))
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

        public void SetTrainData(uint num_data, int[] input, int[] output)
        {
            uint numInput = (uint)input.Length / num_data;
            uint numOutput = (uint)output.Length / num_data;
            using(intArray inputArray = new intArray((int)(numInput * num_data)))
            using(intArray outputArray = new intArray((int)(numOutput * num_data)))
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

        /// <summary> Scale input train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="new_min"> The new minimum. </param>
        /// <param name="new_max"> The new maximum. </param>

        public void ScaleInputTrainData(int new_min, int new_max)
        {
            InternalData.scale_input_train_data(new_min, new_max);
        }

        /// <summary> Scale output train data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="new_min"> The new minimum. </param>
        /// <param name="new_max"> The new maximum. </param>

        public void ScaleOutputTrainData(int new_min, int new_max)
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

        private int [][] cachedOutput = null;

        /// <summary> Gets the output. </summary>
        ///
        /// <value> The output. </value>

        public int[][] Output
        {
            get {
                if (cachedOutput == null)
                {
                    using (intArrayArray output = intArrayArray.frompointer(InternalData.get_output()))
                    {
                        int length = (int)InternalData.length_train_data();
                        int count = (int)InternalData.num_output_train_data();
                        cachedOutput = new int[length][];
                        for (int i = 0; i < length; i++)
                        {
                            cachedOutput[i] = new int[count];
                            using (intArray inputArray = intArray.frompointer(output.getitem(i)))
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

        private int[][] cachedInput = null;

        /// <summary> Gets the input. </summary>
        ///
        /// <value> The input. </value>

        public int[][] Input
        {
            get
            {
                if (cachedInput == null)
                {
                    using (intArrayArray input = intArrayArray.frompointer(InternalData.get_input()))
                    {
                        int length = (int)InternalData.length_train_data();
                        int count = (int)InternalData.num_input_train_data();
                        cachedInput = new int[length][];
                        for (int i = 0; i < length; i++)
                        {
                            cachedInput[i] = new int[count];
                            using (intArray inputArray = intArray.frompointer(input.getitem(i)))
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

        public void ScaleTrainData(int new_min, int new_max)
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
        internal FannWrapperFixed.training_data InternalData
        { 
            get; set;
        }
        private void InternalCallback(uint number, uint inputCount, uint outputCount, global::System.IntPtr inputs, global::System.IntPtr outputs)
        {
            int[] callbackInput = new int[inputCount];
            int[] callbackOutput = new int[outputCount];

            Callback(number, inputCount, outputCount, callbackInput, callbackOutput);

            using (intArray inputArray = new intArray(inputs, false))
            using (intArray outputArray = new intArray(outputs, false))
            {
                for (int i = 0; i < inputCount; i++)
                {
                    inputArray.setitem(i, callbackInput[i]);
                }
                for (int i = 0; i < outputCount; i++)
                {
                    outputArray.setitem(i, callbackOutput[i]);
                }
            }
        }

        /// <summary> Callback, called when the data create. </summary>
        ///
        /// <remarks> Joel Self, 11/12/2015. </remarks>
        ///
        /// <param name="number">      Number of. </param>
        /// <param name="inputCount">  Number of inputs. </param>
        /// <param name="outputCount"> Number of outputs. </param>
        /// <param name="inputs">      The inputs. </param>
        /// <param name="outputs">     The outputs. </param>

        public delegate void DataCreateCallback(uint number, uint inputCount, uint outputCount, int[] inputs, int[] outputs);
        private DataCreateCallback Callback { get; set; }
        private data_create_callback RawCallback { get; set; }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void data_create_callback(uint number, uint inputCount, uint outputCount, global::System.IntPtr inputs, global::System.IntPtr outputs);
    }

}
