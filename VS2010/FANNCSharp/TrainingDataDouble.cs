using System;
using FannWrapperDouble;
using System.Runtime.InteropServices;

namespace FANNCSharp
{

    /* Section: FANN C# Training Data double
    */

    /* Class: TrainingDataDouble

    <TrainingDataDouble> is used to create and manipulate training data used by the <NeuralNetDouble>

    Encapsulation of a training_data class <training_data at http://libfann.github.io/fann/docs/files/fann_training_data_cpp-h.html#training_data> and
    associated C++ API functions.
    */
    public class TrainingDataDouble : IDisposable
    {
        /* Constructor: TrainingDataDouble

            Default constructor creates an empty training data.
            Use <ReadTrainFromFile>, <SetTrainData> or <CreateTrainFromCallback> to initialize.
        */
        public TrainingDataDouble()
        {
            InternalData = new FannWrapperDouble.training_data();
        }
        internal TrainingDataDouble(training_data other)
        {
            InternalData = other;
        }


        /* Constructor: TrainingData

            Copy constructor constructs a copy of the training data.
            Corresponds to the C API <fann_duplicate_train_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_duplicate_train_data> function.
        */
        public TrainingDataDouble(TrainingDataDouble data)
        {
            InternalData = new FannWrapperDouble.training_data(data.InternalData);
        }
        /* Method: Dispose

            Disposes of the training data.
        */
        public void Dispose()
        {
            InternalData.Dispose();
        }

        /* Method: ReadTrainFromFile
           Reads a file that stores training data.

           The file must be formatted like:
           >TrainDataLength InputCount OutputCount
           >inputdata seperated by space
           >outputdata seperated by space
           >
           >.
           >.
           >.
           >
           >inputdata seperated by space
           >outputdata seperated by space

           See also:
   	        <NeuralNetDouble.TrainOnData>, <SaveTrain>, <fann_read_train_from_file at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_read_train_from_file>

            This function appears in FANN >= 1.0.0
        */
        public bool ReadTrainFromFile(string filename)
        {
            return InternalData.read_train_from_file(filename);
        }

        /* Method: SaveTrain

           Save the training structure to a file, with the format as specified in <ReadTrainFromFile>

           Return:
           The function returns true on success and false on failure.

           See also:
   	        <ReadTrainFromFile>, <SaveTrainToFixed>, <fann_save_train at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_save_train> 

           This function appears in FANN >= 1.0.0.
         */
        public bool SaveTrain(string filename)
        {
            return InternalData.save_train(filename);
        }

        /* Method: SaveTrainToFixed

           Saves the training structure to a fixed point data file.

           This function is very useful for testing the quality of a fixed point network.

           Return:
           The function returns true on success and false on failure.

           See also:
   	        <SaveTrain>, <fann_save_train_to_fixed at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_save_train_to_fixed>

           This function appears in FANN >= 1.0.0.
         */
        public bool SaveTrainToFixed(string filename, uint decimalPoint)
        {
            return InternalData.save_train_to_fixed(filename, decimalPoint);
        }

        /* Method: ShuffleTrainData

           Shuffles training data, randomizing the order.
           This is recommended for incremental training, while it have no influence during batch training.

           This function appears in FANN >= 1.1.0.
         */
        public void ShuffleTrainData()
        {
            InternalData.shuffle_train_data();
        }

        /* Method: MergeTrainData

           Merges the data into the data contained in the <TrainingDataDouble>.

           This function appears in FANN >= 1.1.0.
         */
        public void MergeTrainData(TrainingDataDouble data)
        {
            InternalData.merge_train_data(data.InternalData);
        }

        /* Property: TrainDataLength

           Returns the number of training patterns in the <TrainingDataDouble>.

           See also:
           <InputCount>, <OutputCount>, <fann_length_train_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_length_train_data>

           This function appears in FANN >= 2.0.0.
         */
        public uint TrainDataLength
        {
            get
            {
                return InternalData.length_train_data();
            }
        }

        /* Property: InputCount

           Returns the number of inputs in each of the training patterns in the <TrainingDataDouble>.

           See also:
           <OutputCount>, <TrainDataLength>, <fann_num_input_train_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_num_input_train_data>

           This function appears in FANN >= 2.0.0.
         */
        public uint InputCount
        {
            get
            {
                return InternalData.num_input_train_data();
            }
        }

        /* Property: OutputCount

           Returns the number of outputs in each of the training patterns in the <TrainingDataDouble>.

           See also:
           <InputCount>, <TrainDataLength>, <fann_num_output_train_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_num_output_train_data>

           This function appears in FANN >= 2.0.0.
         */
        public uint OutputCount
        {
            get
            {
                return InternalData.num_output_train_data();
            }
        }

        /* Property: Input
            Grant access to the encapsulated data since many situations
            and applications creates the data from sources other than files
            or uses the training data for testing and related functions
         
            Returns:
                A array of arrays of input training data

            See also:
                <Output>, <GetTrainInput>, <SetTrainData>

           This function appears in FANN >= 2.0.0.
        */
        public double[][] Input
        {
            get
            {
                return InternalData.get_input();
            }
        }

        /* Property: Output

            Grant access to the encapsulated data since many situations
            and applications creates the data from sources other than files
            or uses the training data for testing and related functions
            
            Use of this property, when you don't need all of the output data,
            is discouraged because it creates a copy of all of the output
            data every time it is called, thus taking extra time and
            duplicating data in the managed layer.
          
            Instead, if you need a particular output call the <GetTrainOutput>
            with the index of the output you want.
         
            Returns:
                A arrray of arrays of output training data

            See also:
                <Input>, <GetTrainOutput>, <SetTrainData>

           This function appears in FANN >= 2.0.0.
        */
        public double[][] Output
        {
            get
            {
                return InternalData.get_output();
            }
        }

        /* Method: GetTrainInput
            Gets the training input data at the given position

            Returns:
                An array of input training data at the given position

            See also:
                <GetTrainOutput>, <Input>, <SetTrainData>

           This function appears in FANN >= 2.3.0.
        */
        public double[] GetTrainInput(uint position)
        {
            return InternalData.get_train_input(position);
        }

        /* Method: GetTrainOutput
            Gets the training output data at the given position.

            Returns:
                An array of output training data at the given position

            See also:
                <GetTrainInput>, <Output>, <SetTrainData>

           This function appears in FANN >= 2.3.0.
        */
        public double[] GetTrainOutput(uint position)
        {
            return InternalData.get_train_input(position);
        }

        /* Method: SetTrainData

            Set the training data to the input and output data provided.

            A copy of the data is made so there are no restrictions on the
            allocation of the input/output data.

           Parameters:
             input      - The set of inputs (an array of arrays of double data)
             output     - The set of desired outputs (an array of arrays of double data)

            See also:
                <Input>, <Output>
        */
        public void SetTrainData(double[][] input, double[][] output)
        {
            InternalData.set_train_data((uint)input.Length, (uint)input[0].Length, input, (uint)output[0].Length, output);
        }

        /* Method: SetTrainData

            Set the training data to the input and output data provided.

            A copy of the data is made so there are no restrictions on the
            allocation of the input/output data..

           Parameters:
             dataLength      - The number of training data
             input      - The set of inputs (an array with the dimension dataLength*inputCount)
             output     - The set of desired outputs (an array with the dimension dataLength*inputCount)

            See also:
                <Input>, <Output>
        */
        public void SetTrainData(uint dataLength, double[] input, double[] output)
        {
            uint numInput = (uint)input.Length / dataLength;
            uint numOutput = (uint)output.Length / dataLength;
            InternalData.set_train_data(dataLength, numInput, input, numOutput, output);
        }
        /*********************************************************************/

        /* Method: CreateTrainFromCallback
           Creates the training data from a user supplied function.
           As the training data are numerable (data 1, data 2...), the user must write
           a function that receives the number of the training data set (input,output)
           and returns the set.

           Parameters:
             dataCount      - The number of training data
             inputCount     - The number of inputs per training data
             outputCount    - The number of ouputs per training data
             callback       - The user suplied delegate
          
           Parameters for the user delegate:
             number      - The number of the training data set
             inputCount  - The number of inputs per training data
             outputCount - The number of ouputs per training data
             input       - The set of inputs
             output      - The set of desired outputs

           See also:
             <ReadTrainFromFile>, <NeuralNet.TrainOnData>,
             <fann_create_train_from_callback at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_create_train_from_callback>

            This function appears in FANN >= 2.1.0
        */
        public void CreateTrainFromCallback(uint dataCount, uint inputCount, uint outputCount, DataCreateCallback callback)
        {
            InternalData = new FannWrapperDouble.training_data();
            Callback = callback;
            UnmanagedCallback = new data_create_callback(InternalCallback);
            fanndoublePINVOKE.training_data_create_train_from_callback(training_data.getCPtr(this.InternalData), dataCount, inputCount, outputCount, Marshal.GetFunctionPointerForDelegate(UnmanagedCallback));
        }

        /* Property: MinInput

           Get the minimum value of all in the input data

           This function appears in FANN >= 2.3.0
        */
        public double MinInput
        {
            get
            {
                return InternalData.get_min_input();
            }
        }

        /* Property: MaxInput

           Get the maximum value of all in the input data

           This function appears in FANN >= 2.3.0
        */
        public double MaxInput
        {
            get
            {
                return InternalData.get_max_input();
            }
        }

        /* Property: MinOutput

           Get the minimum value of all in the output data

           This function appears in FANN >= 2.3.0
        */
        public double MinOutput
        {
            get
            {
                return InternalData.get_min_output();
            }
        }

        /* Property: MaxOutput

           Get the maximum value of all in the output data

           This function appears in FANN >= 2.3.0
        */
        public double MaxOutput
        {
            get
            {
                return InternalData.get_max_output();
            }
        }

        /* Method: ScaleInputTrainData

           Scales the inputs in the training data to the specified range.

           A simplified scaling method, which is mostly useful in examples where it's known that all the
           data will be in one range and it should be transformed to another range.

           It is not recommended to use this on subsets of data as the complete input range might not be
           available in that subset.

           For more powerful scaling, please consider <NeuralNetDouble.ScaleTrain>

           See also:
   	        <ScaleOutputTrainData>, <ScaleTrainData>, <fann_scale_input_train_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_scale_input_train_data>

           This function appears in FANN >= 2.0.0.
         */
        public void ScaleInputTrainData(double new_min, double new_max)
        {
            InternalData.scale_input_train_data(new_min, new_max);
        }

        /* Method: ScaleOutputTrainData

           Scales the outputs in the training data to the specified range.

           A simplified scaling method, which is mostly useful in examples where it's known that all the
           data will be in one range and it should be transformed to another range.

           It is not recommended to use this on subsets of data as the complete input range might not be
           available in that subset.

           For more powerful scaling, please consider <NeuralNetDouble.ScaleTrain>

           See also:
   	        <ScaleInputTrainData>, <ScaleTrainData>, <fann_scale_output_train_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_scale_output_train_data>

           This function appears in FANN >= 2.0.0.
         */
        public void ScaleOutputTrainData(double new_min, double new_max)
        {
            InternalData.scale_output_train_data(new_min, new_max);
        }

        /* Method: ScaleTrainData

           Scales the inputs and outputs in the training data to the specified range.

           A simplified scaling method, which is mostly useful in examples where it's known that all the
           data will be in one range and it should be transformed to another range.

           It is not recommended to use this on subsets of data as the complete input range might not be
           available in that subset.

           For more powerful scaling, please consider <NeuralNetDouble.ScaleTrain>

           See also:
   	        <ScaleOutputTrainData>, <ScaleInputTrainData>, <fann_scale_train_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_scale_train_data>

           This function appears in FANN >= 2.0.0.
         */
        public void ScaleTrainData(double new_min, double new_max)
        {
            InternalData.scale_train_data(new_min, new_max);
        }

        /* Method: SubsetTrainData

           Changes the training data to a subset, starting at position *pos*
           and *length* elements forward. Use the copy constructor to work
           on a new copy of the training data.

            >TrainingDataDouble fullDataSet = new TrainingDataDouble();
            >fullDataSet.ReadTrainFromFile("somefile.train");
            >TrainingDataDouble smallDataSet = new TrainingDataDouble(fullDataSet);
            >smallDataSet->SubsetTrainData(0, 2); // Only use first two
            >// Use smallDataSet ...
            >small_data_set.Dispose();

           See also:
   	        <fann_subset_train_data http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_subset_train_data>

           This function appears in FANN >= 2.0.0.
         */
        public void SubsetTrainData(uint pos, uint length)
        {
            InternalData.subset_train_data(pos, length);
        }

        internal SWIGTYPE_p_fann_train_data ToFannTrainData()
        {
            return InternalData.to_fann_train_data();
        }

        internal FannWrapperDouble.training_data InternalData
        {
            get;
            set;
        }

        private void InternalCallback(uint number, uint inputCount, uint outputCount, global::System.IntPtr inputs, global::System.IntPtr outputs)
        {
            double[] callbackInput = new double[inputCount];
            double[] callbackOutput = new double[outputCount];

            Callback(number, inputCount, outputCount, callbackInput, callbackOutput);

            using (doubleArray inputArray = new doubleArray(inputs, false))
            using (doubleArray outputArray = new doubleArray(outputs, false))
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

        /* Delegate: DataCreateCallback
           Called for each trianing data input/output pair to create the entire training data set.
          
             Parameters for the user function:
             number      - The number of the training data set
             inputCount  - The number of inputs per training data
             outputCount - The number of ouputs per training data
             input       - The set of inputs
             output      - The set of desired outputs
         
           See also:
             <CreateTrainFromCallback>, <fann_create_train_from_callback at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_create_train_from_callback>
        */
        public delegate void DataCreateCallback(uint number, uint inputCount, uint outputCount, double[] input, double[] output);
        private DataCreateCallback Callback { get; set; }
        private data_create_callback UnmanagedCallback { get; set; }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void data_create_callback(uint number, uint inputCount, uint outputCount, global::System.IntPtr inputs, global::System.IntPtr outputs);
    }
}
