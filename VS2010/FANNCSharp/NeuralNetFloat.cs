using System;
using FannWrapperFloat;
using System.Collections.Generic;
using System.Runtime.InteropServices;
/*
 *  Fast Artificial Neural Network (fann) C# Wrapper
 *  Copyright (C) 2015 created by joelself (at) gmail dot com
 *
 *  This wrapper is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *
 *  This wrapper is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 */

/*
 *  Title: FANN C# NeuralNet Float
 *
 *  Overview:
 *
 *  The Fann Wrapper for C# provides Six main classes: <FANNCSharp.Float::NeuralNet>,
 *  <FANNCSharp.Double::NeuralNet>, <FANNCSharp.Fixed::NeuralNet>,
 *  <FANNCSharp.Float::TrainingData>, <FANNCSharp.Double::TrainingData>,
 *  <FANNCSharp.Fixed::TrainingData>. To use the wrapper add FANNCSharp.dll,
 *  FANNCSharp.Float.dll, FANNCSharp.Double.dll, FANNCSharp.Fixed.dll as a reference
 *  to your project.
 *
 *  To get started see XorSample project.
 *  The license is LGPL. Copyright (C) 2015 created by <joelself@gmail.com>.
 *
 *
 *  Note:  Notes and differences from C++ API
 *
 *  -  Method names are the same as the function names in the C++
 *       API except the snake_case style naming has been replaced with
 *       CamelCase, getters and setters have been turned into properties, and
 *       "num" has been replace with "Count" to be more C#-ish
 *  -  The arguments to the methods are the same as the C++ API
 *       except that the neural_net *net/training_data *data
 *       arguments are encapsulated so they are not present in the
 *       method signatures or are translated into class references.
 *  -  The neural network and training data is automatically cleaned
 *       up in the Dispose methods
 *
 */
namespace FANNCSharp.Float
{
    /* Class: NeuralNet
        <NeuralNet> is the main neural network class used for both training and execution using floats

        Encapsulation of a float neural network <neural_net at http://libfann.github.io/fann/docs/files/fann_cpp-h.html#neural_net> and
        associated C++ API functions.
    */
    public class NeuralNet : IDisposable
    {
        neural_net net = null;

        /* Constructor: NeuralNet

            Creates a copy the other NeuralNet.
        */
        public NeuralNet(NeuralNet other)
        {
            net = new neural_net(other.Net.to_fann());
            Outputs = other.Outputs;
        }

        internal NeuralNet(neural_net other)
        {
            net = other;
            Outputs = other.get_num_output();
        }

        /* Method: Dispose
        
            Destructs the entire network. Must be called manually.
        */
        public void Dispose()
        {
            net.Dispose();
        }
        /* Constructor: NeuralNet

            Creates a neural network of the desired <NetworkType> net_type.

            Parameters:
                numLayers - The total number of layers including the input and the output layer.
                ... - Integer values determining the number of neurons in each layer starting with the
                    input layer and ending with the output layer.

            Example:
                >unt numLayes = 3;
                >uint numInput = 2;
                >uint numHidden = 3;
                >uint numOutput = 1;
                >
                >NeuralNet net(numLayers, numInput, numHidden, numOutput);

            This function appears in FANN >= 2.3.0.
        */
        public NeuralNet(NetworkType netType, uint numLayers, params uint[] args)
        {
            using (uintArray newLayers = new uintArray((int)numLayers))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    newLayers.setitem(i, args[i]);
                }
                Outputs = args[args.Length - 1];
                net = new neural_net(netType, numLayers, newLayers.cast());
            }
        }

        /* Constructor: NeuralNet

            Creates a neural network of the desired <NetworkType> netType, based on a collection of layers.

            Parameters:
                netType - The desired network type of the neural network
                layers - the collection of layer sizes

            Example:
              >NeuralNet net(NetworkType.LAYER, new uint[] {2, 3, 1});

            This function appears in FANN >= 2.3.0.
         */
        public NeuralNet(NetworkType netType, ICollection<uint> layers)
        {
            using (uintArray newLayers = new uintArray(layers.Count))
            {
                int i = 0;
                foreach (uint count in layers)
                {
                    newLayers.setitem(i, count);
                    i++;
                }
                Outputs = newLayers.getitem(layers.Count - 1);
                net = new neural_net(netType, (uint)layers.Count, newLayers.cast());
            }
        }

        /* Constructor: NeuralNet

            Creates a standard backpropagation neural network, which is sparsely connected, this will default the <NetworkType> to <NetworkType::LAYER>

            Parameters:
                connectionRate - The connection rate controls how many connections there will be in the
                    network. If the connection rate is set to 1, the network will be fully
                    connected, but if it is set to 0.5 only half of the connections will be set.
                    A connection rate of 1 will yield the same result as <fann_create_standard at http://libfann.github.io/fann/docs/files/fann-h.html#fann_create_standard>
                numLayers - The total number of layers including the input and the output layer.
                ... - Integer values determining the number of neurons in each layer starting with the
                    input layer and ending with the output layer.

            This function appears in FANN >= 2.3.0.
        */
        public NeuralNet(float connectionRate, uint numLayers, params uint[] args)
        {
            using (uintArray newLayers = new uintArray((int)numLayers))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    newLayers.setitem(i, args[i]);
                }
                Outputs = args[args.Length - 1];
                net = new neural_net(connectionRate, numLayers, newLayers.cast());
            }
        }

        /* Constructor: NeuralNet

            Creates a standard backpropagation neural network, which is sparsely connected, this will default the <NetworkType> to <NetworkType::LAYER>

            Parameters:
                connectionRate - The connection rate controls how many connections there will be in the
                    network. If the connection rate is set to 1, the network will be fully
                    connected, but if it is set to 0.5 only half of the connections will be set.
                    A connection rate of 1 will yield the same result as <fann_create_standard at http://libfann.github.io/fann/docs/files/fann-h.html#fann_create_standard>
                numLayers - The total number of layers including the input and the output layer.
                layers - Integer values determining the number of neurons in each layer starting with the
                    input layer and ending with the output layer.

            This function appears in FANN >= 2.3.0.
        */
        public NeuralNet(float connectionRate, ICollection<uint> layers)
        {
            using (uintArray newLayers = new uintArray(layers.Count))
            {
                int i = 0;
                foreach (uint count in layers)
                {
                    newLayers.setitem(i, count);
                    i++;
                }
                Outputs = newLayers.getitem(layers.Count - 1);
                net = new neural_net(connectionRate, (uint)layers.Count, newLayers.cast());
            }
        }

        /* Constructor: NeuralNet

           Constructs a backpropagation neural network from a configuration file,
           which have been saved by <Save>.

           See also:
            <Save>, <SaveToFixed>

           This function appears in FANN >= 2.3.0.
         */
        public NeuralNet(string filename)
        {
            net = new neural_net(filename);
            Outputs = net.get_num_output();
        }

        /* Method: Run

            Will run input through the neural network, returning an array of outputs, the number of which being 
            equal to the number of neurons in the output layer.
         
           Parameters:
                input - An array of inputs to run through the neural network

            See also:
                <Test>, <fann_run at http://libfann.github.io/fann/docs/files/fann-h.html#fann_run>

            This function appears in FANN >= 1.0.0.
        */
        public float[] Run(float[] input)
        {
            using (floatArray outputs = floatArray.frompointer(net.run(input)))
            {
                float[] result = new float[Outputs];
                for (int i = 0; i < Outputs; i++)
                {
                    result[i] = outputs.getitem(i);
                }
                return result;
            }
        }

        /* Method: Run

            Will run input through the neural network, returning an array of outputs, the number of which being 
            equal to the number of neurons in the output layer.
         
           Parameters:
                input - A DataAccessor that points to the inputs to run through the neural network

            See also:
                <Test>, <fann_run at http://libfann.github.io/fann/docs/files/fann-h.html#fann_run>

            This function appears in FANN >= 1.0.0.
        */
        public float[] Run(DataAccessor input)
        {
            using (floatArray outputs = floatArray.frompointer(net.run(input.Array)))
            {
                float[] result = new float[Outputs];
                for (int i = 0; i < Outputs; i++)
                {
                    result[i] = outputs.getitem(i);
                }
                return result;
            }
        }

        /* Method: RandomizeWeights

            Give each connection a random weight between *minWeight* and *maxWeight*
           
            From the beginning the weights are random between -0.1 and 0.1.

            See also:
                <InitWeights>, <fann_randomize_weights at http://libfann.github.io/fann/docs/files/fann-h.html#fann_randomize_weights>

            This function appears in FANN >= 1.0.0.
        */
        public void RandomizeWeights(float minWeight, float maxWeight)
        {
            net.randomize_weights(minWeight, maxWeight);
        }

        /* Method: InitWeights

            Initialize the weights using Widrow + Nguyen's algorithm.
        	
            This function behaves similarly to <fann_randomize_weights at http://libfann.github.io/fann/docs/files/fann-h.html#fann_randomize_weights>. It will use the algorithm developed 
            by Derrick Nguyen and Bernard Widrow to set the weights in such a way 
            as to speed up training. This technique is not always successful, and in some cases can be less 
            efficient than a purely random initialization.

            The algorithm requires access to the range of the input data (ie, largest and smallest input), 
            and therefore accepts a second argument, data, which is the training data that will be used to 
            train the network.

            See also:
                <RandomizeWeights>, <TrainingData::ReadTrainFromFile>,
                <fann_init_weights at http://libfann.github.io/fann/docs/files/fann-h.html#fann_init_weights>

            This function appears in FANN >= 1.1.0.
        */
        public void InitWeights(TrainingData data)
        {
            net.init_weights(data.InternalData);
        }

        /* Method: PrintConnections

            Will print the connections of the network in a compact matrix, for easy viewing of the internals 
            of the network.

            The output from <fann_print_connections at http://libfann.github.io/fann/docs/files/fann-h.html#fann_print_connections> on a small (2 2 1) network trained on the xor problem
            >Layer / Neuron 012345
            >L   1 / N    3 BBa...
            >L   1 / N    4 BBA...
            >L   1 / N    5 ......
            >L   2 / N    6 ...BBA
            >L   2 / N    7 ......
        		  
            This network have five real neurons and two bias neurons. This gives a total of seven neurons 
            named from 0 to 6. The connections between these neurons can be seen in the matrix. "." is a 
            place where there is no connection, while a character tells how strong the connection is on a 
            scale from a-z. The two real neurons in the hidden layer (neuron 3 and 4 in layer 1) has 
            connection from the three neurons in the previous layer as is visible in the first two lines. 
            The output neuron (6) has connections form the three neurons in the hidden layer 3 - 5 as is 
            visible in the fourth line.

            To simplify the matrix output neurons is not visible as neurons that connections can come from, 
            and input and bias neurons are not visible as neurons that connections can go to.

            This function appears in FANN >= 1.2.0.
        */
        public void PrintConnections()
        {
            net.print_connections();
        }

        /* Method: Save

           Save the entire network to a configuration file.
           
           The configuration file contains all information about the neural network and enables 
           <NeuralNet(string filename)> to create an exact copy of the neural network and all of the
           parameters associated with the neural network.
           
           These two parameters (<SetCallback>, <ErrorLog>) are *NOT* saved 
           to the file because they cannot safely be ported to a different location. Also temporary
           parameters generated during training like <MSE> is not saved.
           
           Return:
           The function returns true on success and false on failure.
           
           See also:
            <NeuralNet>, <SaveToFixed>, <fann_save at http://libfann.github.io/fann/docs/files/fann_io-h.html#fann_save>

           This function appears in FANN >= 1.0.0.
         */
        public bool Save(string file)
        {
            return net.save(file);
        }

        /* Method: SaveToFixed

           Saves the entire network to a configuration file.
           But it is saved in fixed point format no matter which
           format it is currently in.

           This is useful for training a network in floating points,
           and then later executing it in fixed point.

           The function returns the bit position of the fix point, which
           can be used to find out how accurate the fixed point network will be.
           A high value indicates high precision, and a low value indicates low
           precision.

           A negative value indicates very low precision, and a very
           strong possibility for overflow.
           (the actual fix point will be set to 0, since a negative
           fix point does not make sence).

           Generally, a fix point lower than 6 is bad, and should be avoided.
           The best way to avoid this, is to have less connections to each neuron,
           or just less neurons in each layer.

           The fixed point use of this network is only intended for use on machines that
           have no floating point processor, like an iPAQ. On normal computers the floating
           point version is actually faster.

           See also:
            <NeuralNet>, <Save>, <fann_save_to_fixed at http://libfann.github.io/fann/docs/files/fann_io-h.html#fann_save_to_fixed>

           This function appears in FANN >= 1.0.0.
        */
        public int SaveToFixed(string file)
        {
            return net.save_to_fixed(file);
        }

        /* Method: Train

           Train one iteration with a set of inputs, and a set of desired outputs.
           This training is always incremental training (see <TrainingAlgorithm>),
           since only one pattern is presented.
           
           Parameters:
   	        input - an array of inputs. This array must be exactly <InputCount> long.
   	        desiredOutput - an array of desired outputs. This array must be exactly <OutputCount> long.
           	
   	        See also:
   		        <TrainOnData>, <TrainEpoch>, <fann_train at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_train>
           	
   	        This function appears in FANN >= 1.0.0.
         */
        public void Train(float[] input, float[] desiredOutput)
        {
            net.train(input, desiredOutput);
        }

        /* Method: TrainEpoch
            Train one epoch with a set of training data.
           
            Train one epoch with the training data stored in data. One epoch is where all of 
            the training data is considered exactly once.

	        This function returns the MSE error as it is calculated either before or during 
	        the actual training. This is not the actual MSE after the training epoch, but since 
	        calculating this will require to go through the entire training set once more, it is 
	        more than adequate to use this value during training.

	        The training algorithm used by this function is chosen by the <TrainingAlgorithm> 
	        function.
        	
	        See also:
		        <TrainOnData>, <TestData>, <fann_train_epoch at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_train_epoch>
        		
	        This function appears in FANN >= 1.2.0.
         */
        public float TrainEpoch(TrainingData data)
        {
            return net.train_epoch(data.InternalData);
        }

        /* Method: TrainOnData

           Trains on an entire dataset, for a period of time. 
           
           This training uses the training algorithm chosen by <TrainingAlgorithm>,
           and the parameters set for these training algorithms.
           
           Parameters:
   		        data - The data, which should be used during training
   		        maxEpochs - The maximum number of epochs the training should continue
   		        epochsBetweenReports - The number of epochs between printing a status report to the console.
   			        A value of zero means no reports should be printed.
   		        desiredError - The desired <MSE> or <BitFail>, depending on which stop function
   			        is chosen by <TrainStopFunction>.

	        Instead of printing out reports every epochs_between_reports, a callback function can be called 
	        (see <SetCallback>).
        	
	        See also:
		        <TrainOnFile>, <TrainEpoch>, <fann_train_on_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_train_on_data>

	        This function appears in FANN >= 1.0.0.
        */
        public void TrainOnData(TrainingData data, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
            net.train_on_data(data.InternalData, maxEpochs, epochsBetweenReports, desiredError);
        }

        /* Method: TrainOnFile
           
           Does the same as <TrainOnData>, but reads the training data directly from a file.
           
           See also:
   		        <TrainOnData>, <fann_train_on_file at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_train_on_file>

	        This function appears in FANN >= 1.0.0.
        */
        public void TrainOnFile(string filename, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
            net.train_on_file(filename, maxEpochs, epochsBetweenReports, desiredError);
        }

        /* Method: Test

           Test with a set of inputs, and a set of desired outputs.
           This operation updates the mean square error, but does not
           change the network in any way.
           
           See also:
   		        <TestData>, <Train>, <fann_test at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_test>
           
           This function appears in FANN >= 1.0.0.
        */
        public float[] Test(float[] input, float[] desiredOutput)
        {
            using (floatArray result = floatArray.frompointer(net.test(input, desiredOutput)))
            {
                float[] arrayResult = new float[Outputs];
                for (int i = 0; i < Outputs; i++)
                {
                    arrayResult[i] = result.getitem(i);
                }
                return arrayResult;
            }
        }

        /* Method: TestData
          
           Test a set of training data and calculates the MSE for the training data. 
           
           This function updates the MSE and the bit fail values.
           
           See also:
 	        <Test>, <MSE>, <BitFail>, <fann_test_data at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_test_data>

	        This function appears in FANN >= 1.2.0.
         */
        public float TestData(TrainingData data)
        {
            return net.test_data(data.InternalData);
        }

        /* Property: MSE
           Reads the mean square error from the network.
           
           Reads the mean square error from the network. This value is calculated during 
           training or testing, and can therefore sometimes be a bit off if the weights 
           have been changed since the last calculation of the value.
           
           See also:
   	        <TestData>, <fann_get_MSE at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_MSE>

	        This function appears in FANN >= 1.1.0.
         */
        public float MSE
        {
            get
            {
                return net.get_MSE();
            }
        }

        /* Method: ResetMSE

           Resets the mean square error from the network.
   
           This function also resets the number of bits that fail.
           
           See also:
            <MSE>, <BitFailLimit>, <fann_reset_MSE at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_reset_MSE>
           
            This function appears in FANN >= 1.1.0
         */
        public void ResetMSE()
        {
            net.reset_MSE();
        }

        /* Method: SetCallback
           
           Sets the callback function for use during training. The user_data is passed to
           the callback. It can point to arbitrary data that the callback might require and
           can be NULL if it is not used.
         	
           See <FANN::callback_type at http://libfann.github.io/fann/docs/files/fann_data_cpp-h.html#callback_type> for more information about the callback function.
           
           The default callback function simply prints out some status information.

           This function appears in FANN >= 2.0.0.
         */
        public void SetCallback(TrainingCallback callback, Object userData)
        {
            Callback = callback;
            UserData = userData;
            GCHandle handle = GCHandle.Alloc(userData);
            UnmanagedCallback = new training_callback(InternalCallback);
            fannfloatPINVOKE.neural_net_set_callback(neural_net.getCPtr(this.net), Marshal.GetFunctionPointerForDelegate(UnmanagedCallback), (IntPtr)handle);
        }

        /* Method: PrintParameters

  	        Prints all of the parameters and options of the neural network

            See also:
                <fann_print_parameters at http://libfann.github.io/fann/docs/files/fann-h.html#fann_print_parameters>

	        This function appears in FANN >= 1.2.0.
        */
        public void PrintParameters()
        {
            net.print_parameters();
        }

        /* Property: TrainingAlgorithm

           Return or set the training algorithm as described by <TrainingAlgorithm>.
           This training algorithm is used by <TrainOnData> and associated functions.
           
           Note that this algorithm is also used during <CascadetrainOnData>, although only
           TrainingAlgorithm.TRAIN_RPROP and TrainingAlgorithm.:TRAIN_QUICKPROP is allowed during cascade training.
           
           The default training algorithm is TrainingAlgorithm.TRAIN_RPROP.
           
           See also:
            <TrainingAlgorithm>,
            <fann_get_training_algorithm at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_training_algorithm>,
            <fann_set_training_algorithm at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_training_algorithm>

           This function appears in FANN >= 1.0.0.   	
         */
        public TrainingAlgorithm TrainingAlgorithm
        {
            get
            {
                return net.get_training_algorithm();
            }
            set
            {
                net.set_training_algorithm(value);
            }
        }

        /* Property: LearningRate

           Return or set the learning rate.
           
           The learning rate is used to determine how aggressive training should be for some of the
           training algorithms (TrainingAlgorithm.TRAIN_INCREMENTAL, TrainingAlgorithm.TRAIN_BATCH, TrainingAlgorithm.TRAIN_QUICKPROP).
           Do however note that it is not used in TrainingAlgorithm.TRAIN_RPROP.
           
           The default learning rate is 0.7.
           
           See also:
   	        <TrainingAlgorithm>,
            <fann_get_learning_rate at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_learning_rate>,
            <fann_set_learning_rate at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_learning_rate>
           
           This function appears in FANN >= 1.0.0.   	
         */
        public float LearningRate
        {
            get
            {
                return net.get_learning_rate();
            }
            set
            {
                net.set_learning_rate(value);
            }
        }

        /*************************************************************************************************************/

        /* Method: GetActivationFunction

           Get the activation function for neuron number *neuron* in layer number *layer*, 
           counting the input layer as layer 0. 
           
           It is not possible to get activation functions for the neurons in the input layer.
           
           Information about the individual activation functions is available at <ActivationFunction>.

           Returns:
            The activation function for the neuron or -1 if the neuron is not defined in the neural network.
           
           See also:
   	        <SetActivationFunctionLayer>, <ActivationFunctionHidden>,
   	        <ActivationFunctionOutput>, <SetActivationSteepness>,
            <SetActivationFunction>, <fann_get_activation_function at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_activation_function>

           This function appears in FANN >= 2.1.0
         */
        public ActivationFunction GetActivationFunction(int layer, int neuron)
        {
            return net.get_activation_function(layer, neuron);
        }

        /* Method:  SetActivationFunction

           Set the activation function for neuron number *neuron* in layer number *layer*, 
           counting the input layer as layer 0. 
           
           It is not possible to set activation functions for the neurons in the input layer.
           
           When choosing an activation function it is important to note that the activation 
           functions have different range. FANN::SIGMOID is e.g. in the 0 - 1 range while 
           FANN::SIGMOID_SYMMETRIC is in the -1 - 1 range and FANN::LINEAR is unbounded.
           
           Information about the individual activation functions is available at <ActivationFunction>.
           
           The default activation function is FANN::SIGMOID_STEPWISE.
           
           See also:
   	        <SetActivationFunctionLayer>, <ActivationFunctionHidden>,
   	        <ActivationFunctionOutput>, <SetActivationSteepness>,
            <GetActivationFunction>, <fann_get_activation_function at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_activation_function>

           This function appears in FANN >= 2.0.0.
         */
        public void SetActivationFunction(ActivationFunction function, int layer, int neuron)
        {
            net.set_activation_function(function, layer, neuron);
        }

        /* Method: SetActivationFunctionLayer

           Set the activation function for all the neurons in the layer number *layer*, 
           counting the input layer as layer 0. 
           
           It is not possible to set activation functions for the neurons in the input layer.

           See also:
   	        <SetActivationFunction>, <ActivationFunctionHidden>,
   	        <ActivationFunctionOutput>, <SetActivationSteepnessLayer>,
            <fann_set_activation_function_layer at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_activation_function_layer>

           This function appears in FANN >= 2.0.0.
         */
        public void SetActivationFunctionLayer(ActivationFunction function, int layer)
        {
            net.set_activation_function_layer(function, layer);
        }

        /* Property: ActivationFunctionHidden

           Set the activation function for all of the hidden layers.

           See also:
   	        <SetActivationFunction>, <SetActivationFunctionLayer>,
   	        <ActivationFunctionOutput>, <ActivationSteepnessHidden>,
            <fann_set_activation_function_hidden at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_activation_function_hidden>

           This function appears in FANN >= 1.0.0.
         */
        public ActivationFunction ActivationFunctionHidden
        {
            set
            {
                net.set_activation_function_hidden(value);
            }
        }

        /* Property: ActivationFunctionOutput

           Set the activation function for the output layer.

           See also:
   	        <SetActivationFunction>, <SetActivationFunctionLayer>,
   	        <ActivationFunctionHidden>, <ActivationSteepnessOutput>,
            <fann_set_activation_function_output at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_activation_function_output>

           This function appears in FANN >= 1.0.0.
         */
        public ActivationFunction ActivationFunctionOutput
        {
            set
            {
                net.set_activation_function_output(value);
            }
        }

        /* Method: GetActivationSteepness

           Get the activation steepness for neuron number *neuron* in layer number *layer*, 
           counting the input layer as layer 0. 
           
           It is not possible to get activation steepness for the neurons in the input layer.
           
           The steepness of an activation function says something about how fast the activation function 
           goes from the minimum to the maximum. A high value for the activation function will also
           give a more aggressive training.
           
           When training neural networks where the output values should be at the extremes (usually 0 and 1, 
           depending on the activation function), a steep activation function can be used (e.g. 1.0).
           
           The default activation steepness is 0.5.
           
           Returns:
            The activation steepness for the neuron or -1 if the neuron is not defined in the neural network.
           
           See also:
   	        <SetActivationSteepnessLayer>, <ActivationSteepnessHidden>,
   	        <ActivationSteepnessOutput>, <SetActivationFunction>,
            <SetActivationSteepness>, <fann_get_activation_steepness at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_activation_steepness>

           This function appears in FANN >= 2.1.0
         */
        public float GetActivationSteepness(int layer, int neuron)
        {
            return net.get_activation_steepness(layer, neuron);
        }

        /* Method: SetActivationSteepness

           Set the activation steepness for neuron number *neuron* in layer number *layer*, 
           counting the input layer as layer 0. 
           
           It is not possible to set activation steepness for the neurons in the input layer.
           
           The steepness of an activation function says something about how fast the activation function 
           goes from the minimum to the maximum. A high value for the activation function will also
           give a more aggressive training.
           
           When training neural networks where the output values should be at the extremes (usually 0 and 1, 
           depending on the activation function), a steep activation function can be used (e.g. 1.0).
           
           The default activation steepness is 0.5.
           
           See also:
   	        <SetActivationSteepnessLayer>, <ActivationSteepnessHidden>,
   	        <ActivationSteepnessOutput>, <SetActivationFunction>,
            <GetActivationSteepness>, <fann_set_activation_steepness at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_activation_steepness>

           This function appears in FANN >= 2.0.0.
         */
        public void SetActivationSteepness(float steepness, int layer, int neuron)
        {
            net.set_activation_steepness(steepness, layer, neuron);
        }

        /* Method: SetActivationSteepnessLayer

           Set the activation steepness all of the neurons in layer number *layer*, 
           counting the input layer as layer 0. 
           
           It is not possible to set activation steepness for the neurons in the input layer.
           
           See also:
   	        <SetActivationSteepness>, <ActivationSteepnessHidden>,
   	        <ActivationSteepnessOutput>, <SetActivationFunctionLayer>,
            <fann_set_activation_steepness_layer at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_activation_steepness_layer>

           This function appears in FANN >= 2.0.0.
         */
        public void SetActivationSteepnessLayer(float steepness, int layer)
        {
            net.set_activation_steepness_layer(steepness, layer);
        }

        /* Property: ActivationSteepnessHidden

           Set the steepness of the activation steepness in all of the hidden layers.

           See also:
   	        <SetActivationSteepness>, <SetActivationSteepnessLayer>,
   	        <ActivationSteepnessOutput>, <ActivationFunctionHidden>,
            <fann_set_activation_steepness_hidden at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_activation_steepness_hidden>

           This function appears in FANN >= 1.2.0.
         */
        public float ActivationSteepnessHidden
        {
            set
            {
                net.set_activation_steepness_hidden(value);
            }
        }

        /* Property: ActivationSteepnessOutput

           Set the steepness of the activation steepness in the output layer.

           See also:
   	        <SetActivationSteepness>, <SetActivationSteepnessLayer>,
   	        <ActivationFunctionHidden>, <ActivationSteepnessOutput>,
            <fann_set_activation_steepness_output at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_activation_steepness_output>

           This function appears in FANN >= 1.2.0.
         */
        public float ActivationSteepnessOutput
        {
            set
            {
                net.set_activation_steepness_output(value);
            }
        }

        /*************************************************************************************************************/

        /* Property: TrainErrorFunction

           Sets or returns the error function used during training.

           The error functions is described further in <ErrorFunction>
           
           The default error function is ErrorFunction.ERRORFUNC_TANH
           
           See also:
   	       <fann_get_train_error_function at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_train_error_function>,
   	       <fann_set_train_error_function at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_train_error_function>
              
           This function appears in FANN >= 1.2.0.
          */
        public ErrorFunction TrainErrorFunction
        {
            get
            {
                return net.get_train_error_function();
            }
            set
            {
                net.set_train_error_function(value);
            }
        }

        /* Property: QuickpropDecay

           Gets or sets the quickprop decay factor.
          
           The decay is a small negative valued number which is the factor that the weights 
           should become smaller in each iteration during quickprop training. This is used 
           to make sure that the weights do not become too high during training.
           
           The default decay is -0.0001.
           
           See also:
   	       <fann_get_quickprop_decay at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_quickprop_decay>,
   	       <fann_set_quickprop_decay at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_quickprop_decay>

           This function appears in FANN >= 1.2.0.
         */
        public float QuickpropDecay
        {
            get
            {
                return net.get_quickprop_decay();
            }
            set
            {
                net.set_quickprop_decay(value);
            }
        }

        /* Property: QuickpropMu
         
           Get or sets the quickprop mu factor.
         
           The mu factor is used to increase and decrease the step-size during quickprop training. 
           The mu factor should always be above 1, since it would otherwise decrease the step-size 
           when it was suppose to increase it.
           
           The default mu factor is 1.75. 
           
           See also:
   	       <fann_get_quickprop_mu at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_quickprop_mu>,
   	       <fann_set_quickprop_mu at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_quickprop_mu>

           This function appears in FANN >= 1.2.0.
        */
        public float QuickpropMu
        {
            get
            {
                return net.get_quickprop_mu();
            }
            set
            {
                net.set_quickprop_mu(value);
            }
        }

        /* Property: RpropIncreaseFactor
         
           Gets or sets the increase factor used during RPROP training.
         
           The increase factor is a value larger than 1, which is used to 
           increase the step-size during RPROP training.

           The default increase factor is 1.2.
           
           See also:
   	       <fann_get_rprop_increase_factor at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_rprop_increase_factor>,
   	       <fann_set_rprop_increase_factor at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_rprop_increase_factor>

           This function appears in FANN >= 1.2.0.
        */
        public float RpropIncreaseFactor
        {
            get
            {
                return net.get_rprop_increase_factor();
            }
            set
            {
                net.set_rprop_increase_factor(value);
            }
        }

        /* Property: RpropDecreaseFactor
         
           Gets or sets the rprop decrease factor.

           The decrease factor is a value smaller than 1, which is used to decrease the step-size during RPROP training.

           The default decrease factor is 0.5.

           See also:
           <fann_get_rprop_decrease_factor at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_rprop_decrease_factor>,
           <fann_set_rprop_decrease_factor at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_rprop_decrease_factor>

           This function appears in FANN >= 1.2.0.
        */
        public float RpropDecreaseFactor
        {
            get
            {
                return net.get_rprop_decrease_factor();
            }
            set
            {
                net.set_rprop_decrease_factor(value);
            }
        }

        /* Property: RpropDeltaZero
          
           Gets or sets the rprop delta zero.

           The initial step-size is a small positive number determining how small the initial step-size may be.

           The default value delta zero is 0.1.

           See also:
   	       <fann_get_rprop_delta_zero at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_rprop_delta_zero>,
   	       <fann_set_rprop_delta_zero at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_rprop_delta_zero>
           	
           This function appears in FANN >= 2.1.0.
        */
        public float RpropDeltaZero
        {
            get
            {
                return net.get_rprop_delta_zero();
            }
            set
            {
                net.set_rprop_delta_zero(value);
            }
        }

        /* Property: RpropDeltaMin
          
           Gets or sets the rprop delta min.

           The minimum step-size is a small positive number determining how small the minimum step-size may be.

           The default value delta min is 0.0.

           See also:
   	       <fann_get_rprop_delta_min at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_rprop_delta_min>,
   	       <fann_set_rprop_delta_min at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_rprop_delta_min>
           	
           This function appears in FANN >= 1.2.0.
        */
        public float RpropDeltaMin
        {
            get
            {
                return net.get_rprop_delta_min();
            }
            set
            {
                net.set_rprop_delta_min(value);
            }
        }

        /* Property: RpropDeltaMax
          
           Gets or set the rprop delta max.

           The maximum step-size is a positive number determining how large the maximum step-size may be.

           The default delta max is 50.0.

           See also:
   	        <RpropDeltaMin>, <fann_get_rprop_delta_max at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_rprop_delta_max>,
            <fann_set_rprop_delta_max at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_rprop_delta_max>

           This function appears in FANN >= 1.2.0.
        */
        public float RpropDeltaMax
        {
            get
            {
                return net.get_rprop_delta_max();
            }
            set
            {
                net.set_rprop_delta_max(value);
            }
        }

        /* Property: SarpropWeightDecayShift

           Gets or sets the sarprop weight decay shift.

           The default delta max is -6.644.

           See also:
   	       <fann_get_sarprop_weight_decay_shift at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_sarprop_weight_decay_shift>,
   	       <fann_set_sarprop_weight_decay_shift at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_sarprop_weight_decay_shift>

           This function appears in FANN >= 2.1.0.
        */
        public float SarpropWeightDecayShift
        {
            get
            {
                return net.get_sarprop_weight_decay_shift();
            }
            set
            {
                net.set_sarprop_weight_decay_shift(value);
            }
        }

        /* Property: SarpropStepErrorThresholdFactor

           Gets or sets the sarprop step error threshold factor.

           The default delta max is 0.1.

           See also:
   	       <fann_get_sarprop_step_error_threshold_factor at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_sarprop_step_error_threshold_factor>,
   	       <fann_set_sarprop_step_error_threshold_factor at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_sarprop_step_error_threshold_factor>

           This function appears in FANN >= 2.1.0.
        */
        public float SarpropStepErrorThresholdFactor
        {
            get
            {
                return net.get_sarprop_step_error_threshold_factor();
            }
            set
            {
                net.set_sarprop_step_error_threshold_factor(value);
            }
        }


        /* Property: SarpropStepErrorShift

           Gets or sets the sarprop step error shift.

           The default delta max is 1.385.

           See also:
   	       <fann_get_sarprop_step_error_shift at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_sarprop_step_error_shift>,
   	       <fann_set_sarprop_step_error_shift at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_sarprop_step_error_shift>

           This function appears in FANN >= 2.1.0.
        */
        public float SarpropStepErrorShift
        {
            get
            {
                return net.get_sarprop_step_error_shift();
            }
            set
            {
                net.set_sarprop_step_error_shift(value);
            }
        }

        /* Property: SarpropTemperature

               Gets or set the sarprop weight decay shift.

               The default delta max is 0.015.

               See also:
               <fann_get_sarprop_temperature at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_get_sarprop_temperature>,
               <fann_set_sarprop_temperature at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_sarprop_temperature>

               This function appears in FANN >= 2.1.0.
        */
        public float SarpropTemperature
        {
            get
            {
                return net.get_sarprop_temperature();
            }
            set
            {
                net.set_sarprop_temperature(value);
            }
        }

        /* Property: InputCount

           Get the number of input neurons.

	        This function appears in FANN >= 1.0.0.
        */
        public uint InputCount
        {
            get
            {
                return net.get_num_input();
            }
        }

        /* Property: OutputCount

           Get the number of output neurons.

	        This function appears in FANN >= 1.0.0.
        */
        public uint OutputCount
        {
            get
            {
                return net.get_num_output();
            }
        }

        /* Property: TotalNeurons

           Get the total number of neurons in the entire network. This number does also include the 
	        bias neurons, so a 2-4-2 network has 2+4+2 +2(bias) = 10 neurons.

	        This function appears in FANN >= 1.0.0.
        */
        public uint TotalNeurons
        {
            get
            {
                return net.get_total_neurons();
            }
        }

        /* Property: TotalConnections

           Get the total number of connections in the entire network.

	        This function appears in FANN >= 1.0.0.
        */
        public uint TotalConnections
        {
            get
            {
                return net.get_total_connections();
            }
        }

        /*********************************************************************/

        /* Property: NetworkType

            Get the type of neural network it was created as.

	        Returns:
                The neural network type from enum <NetworkType>

            See Also:
                <fann_get_network_type at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_network_type>

           This function appears in FANN >= 2.1.0
        */
        public NetworkType NetworkType
        {
            get
            {
                return net.get_network_type();
            }
        }

        /* Property: ConnectionRate

            Get the connection rate used when the network was created

	        Returns:
                The connection rate

            See also:
                <fann_get_connection_rate at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_connection_rate>

           This function appears in FANN >= 2.1.0
        */
        public float ConnectionRate
        {
            get
            {
                return net.get_connection_rate();
            }
        }

        /* Property: LayerCount

            Get the number of layers in the network

	        Returns:
		        The number of layers in the neural network

            See also:
                <fann_get_num_layers at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_num_layers>

           This function appears in FANN >= 2.1.0
        */
        public uint LayerCount
        {
            get
            {
                return net.get_num_layers();
            }
        }

        /* Property: LayerArray

            Get the number of neurons in each layer in the network.

            Bias is not included so the layers match the create methods.

            See also:
                <fann_get_layer_array at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_layer_array>

           This function appears in FANN >= 2.1.0
        */
        public uint[] LayerArray
        {
            get
            {
                uint[] layers = new uint[net.get_num_layers()];
                using (uintArray array = new uintArray(layers.Length))
                {
                    net.get_layer_array(array.cast());
                    for (int i = 0; i < layers.Length; i++)
                    {
                        layers[i] = array.getitem(i);
                    }
                }
                return layers;
            }
        }

        /* Property: BiasArray

            Get the number of bias in each layer in the network.

            See also:
                <fann_get_bias_array at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_bias_array>

            This function appears in FANN >= 2.1.0
        */
        public uint[] BiasArray
        {
            get
            {
                uint[] bias = new uint[net.get_num_layers()];
                using (uintArray array = new uintArray(bias.Length))
                {
                    net.get_layer_array(array.cast());
                    for (int i = 0; i < bias.Length; i++)
                    {
                        bias[i] = array.getitem(i);
                    }
                }
                return bias;
            }
        }

        /* Property: ConnectionArray

            Get the connections in the network.

            See also:
                <fann_get_connection_array at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_connection_array>

           This function appears in FANN >= 2.1.0
        */
        public Connection[] ConnectionArray
        {
            get
            {
                uint count = net.get_total_connections();
                Connection[] connections = new Connection[count];
                using (ConnectionArray output = new ConnectionArray(connections.Length))
                {
                    net.get_connection_array(output.cast());
                    for (uint i = 0; i < count; i++)
                    {
                        connections[i] = new Connection(output.getitem((int)i));
                    }
                }
                return connections;
            }
        }

        /* Property: WeightArray

            Set connections in the network.

            Only the weights can be changed, connections and weights are ignored
            if they do not already exist in the network.

            See also:
                <fann_set_weight_array at http://libfann.github.io/fann/docs/files/fann-h.html#fann_set_weight_array>

           This function appears in FANN >= 2.1.0
        */
        public Connection[] WeightArray
        {
            set
            {
                using (ConnectionArray input = new ConnectionArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i].connection);
                    }
                    net.set_weight_array(input.cast(), (uint)value.Length);
                }
            }
        }

        /* Method: SetWeight

            Set a connection in the network.

            Only the weights can be changed. The connection/weight is
            ignored if it does not already exist in the network.

            See also:
                <fann_set_weight at http://libfann.github.io/fann/docs/files/fann-h.html#fann_set_weight>

           This function appears in FANN >= 2.1.0
        */
        public void SetWeight(uint fromNeuron, uint toNeuron, float weight)
        {
            net.set_weight(fromNeuron, toNeuron, weight);
        }

        /*********************************************************************/

        /* Property: LearningMomentum

           Get or set the learning momentum.
           
           The learning momentum can be used to speed up TrainingAlgorithm.TRAIN_INCREMENTAL training.
           A too high momentum will however not benefit training. Setting momentum to 0 will
           be the same as not using the momentum parameter. The recommended value of this parameter
           is between 0.0 and 1.0.

           The default momentum is 0.
           
           See also:
           <TrainingAlgorithm>

           This function appears in FANN >= 2.0.0.   	
         */
        public float LearningMomentum
        {
            get
            {
                return net.get_learning_momentum();
            }
            set
            {
                net.set_learning_momentum(value);
            }
        }

        /* Property: TrainStopFunction

           Gets or sets the stop function used during training.
           
           The stop function is described further in <StopFunction>
           
           The default stop function is StopFunction.STOPFUNC_MSE
           
           See also:
   	       <BitFailLimit>
              
           This function appears in FANN >= 2.0.0.
         */
        public StopFunction TrainStopFunction
        {
            get
            {
                return net.get_train_stop_function();
            }
            set
            {
                net.set_train_stop_function(value);
            }
        }

        /* Property: BitFailLimit

           Gets or sets the bit fail limit used during training.
           
           The bit fail limit is used during training when the <StopFunction> is set to StopFunction.FANN_STOPFUNC_BIT.

           The limit is the maximum accepted difference between the desired output and the actual output during
           training. Each output that diverges more than this limit is counted as an error bit.
           This difference is divided by two when dealing with symmetric activation functions,
           so that symmetric and not symmetric activation functions can use the same limit.
           
           The default bit fail limit is 0.35.
           
           This function appears in FANN >= 2.0.0.
         */
        public float BitFailLimit
        {
            get
            {
                return net.get_bit_fail_limit();
            }
            set
            {
                net.set_bit_fail_limit(value);
            }
        }

        /* Property: BitFail
        	
            Gets or set the number of fail bits. Means the number of output neurons which differ more 
	        than the bit fail limit (see <BitFailLimit>). 
	        The bits are counted in all of the training data, so this number can be higher than
	        the number of training data.
        	
	        This value is reset by <ResetMSE> and updated by all the same functions which also
	        updates the MSE value (e.g. <TestData>, <TrainEpoch>)
        	
	        See also:
		        <StopFunction>, <MSE>

	        This function appears in FANN >= 2.0.0
        */
        public uint BitFail
        {
            get
            {
                return net.get_bit_fail();
            }
        }

        /*********************************************************************/

        /* Method: CascadetrainOnData

           Trains on an entire dataset, for a period of time using the Cascade2 training algorithm.
           This algorithm adds neurons to the neural network while training, which means that it
           needs to start with an ANN without any hidden layers. The neural network should also use
           shortcut connections, so NeuralNet(NetworkType.SHORTCUT, ...) should be used to create the NeuralNetwork like this:
           >NeuralNet net(NetworkType.SHORTCUT, ...);
           
           This training uses the parameters set using the Cascade..., but it also uses another
           training algorithm as it's internal training algorithm. This algorithm can be set to either
           TrainingAlgorithm.TRAIN_RPROP or TrainingAlgorithm.TRAIN_QUICKPROP by <TrainingAlgorithm>, and the parameters 
           set for these training algorithms will also affect the cascade training.
           
           Parameters:
   		        data - The data, which should be used during training
   		        maxNeurons - The maximum number of neurons to be added to neural network
   		        neuronsBetweenReports - The number of neurons between printing a status report to the console.
   			        A value of zero means no reports should be printed.
   		        desiredError - The desired <MSE> or <BitFail>, depending on which stop function
   			        is chosen by <TrainStopFunction>.

	        Instead of printing out reports every neuronsBetweenReports, a callback function can be called 
	        (see <SetCallback>).
        	
	        See also:
		        <TrainOnData>, <CascadetrainOnFile>, <fann_cascadetrain_on_data at http://libfann.github.io/fann/docs/files/fann_cpp-h.html#neural_net.cascadetrain_on_data>

	        This function appears in FANN >= 2.0.0. 
        */
        public void CascadetrainOnData(TrainingData data, uint maxNeurons, uint neuronsBetweenReports, float desiredError)
        {
            net.cascadetrain_on_data(data.InternalData, maxNeurons, neuronsBetweenReports, desiredError);
        }

        /* Method: CascadetrainOnFile
           
           Does the same as <CascadetrainOnData>, but reads the training data directly from a file.
           
           See also:
   		        <fann_cascadetrain_on_data at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_cascadetrain_on_data>,
                <fann_cascadetrain_on_file at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_cascadetrain_on_file>

	        This function appears in FANN >= 2.0.0.
        */
        public void CascadetrainOnFile(string filename, uint maxNeurons, uint neuronsBetweenReports, float desiredError)
        {
            net.cascadetrain_on_file(filename, maxNeurons, neuronsBetweenReports, desiredError);
        }

        /* Property: CascadeOutputChangeFraction

           The cascade output change fraction is a number between 0 and 1 determining how large a fraction
           the <MSE> value should change within <CascadeOutputStagnationEpochs> during
           training of the output connections, in order for the training not to stagnate. If the training 
           stagnates, the training of the output connections will be ended and new candidates will be prepared.
           
           This means:
           If the MSE does not change by a fraction of <CascadeOutputStagnationEpochs> during a 
           period of <CascadeOutputStagnationEpochs>, the training of the output connections
           is stopped because the training has stagnated.

           If the cascade output change fraction is low, the output connections will be trained more and if the
           fraction is high they will be trained less.
           
           The default cascade output change fraction is 0.01, which is equalent to a 1% change in MSE.

           See also:
   		        <MSE>, <CascadeOutputStagnationEpochs>,
                <fann_get_cascade_output_change_fraction at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_output_change_fraction>,
                <fann_set_cascade_output_change_fraction at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_output_change_fraction>

	        This function appears in FANN >= 2.0.0.
         */
        public float CascadeOutputChangeFraction
        {
            get
            {
                return net.get_cascade_output_change_fraction();
            }
            set
            {
                net.set_cascade_output_change_fraction(value);
            }
        }

        /* Property: CascadeOutputStagnationEpochs

           The number of cascade output stagnation epochs determines the number of epochs training is allowed to
           continue without changing the MSE by a fraction of <CascadeOutputChangeFraction>.
           
           See more info about this parameter in <CascadeOutputChangeFraction>.
           
           The default number of cascade output stagnation epochs is 12.

           See also:
   		        <CascadeOutputChangeFraction>,
                <fann_get_cascade_output_stagnation_epochs at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_output_stagnation_epochs>,
                <fann_set_cascade_output_stagnation_epochs at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_output_stagnation_epochs>

	        This function appears in FANN >= 2.0.0.
         */
        public uint CascadeOutputStagnationEpochs
        {
            get
            {
                return net.get_cascade_output_stagnation_epochs();
            }
            set
            {
                net.set_cascade_output_stagnation_epochs(value);
            }
        }

        /* Property: CascadeCandidateChangeFraction

           The cascade candidate change fraction is a number between 0 and 1 determining how large a fraction
           the <MSE> value should change within <CascadeCandidateStagnationEpochs> during
           training of the candidate neurons, in order for the training not to stagnate. If the training 
           stagnates, the training of the candidate neurons will be ended and the best candidate will be selected.
           
           This means:
           If the MSE does not change by a fraction of <CascadeCandidateChangeFraction> during a 
           period of <CascadeCandidateStagnationEpochs>, the training of the candidate neurons
           is stopped because the training has stagnated.

           If the cascade candidate change fraction is low, the candidate neurons will be trained more and if the
           fraction is high they will be trained less.
           
           The default cascade candidate change fraction is 0.01, which is equalent to a 1% change in MSE.

           See also:
   		        <MSE>, <CascadeCandidateStagnationEpochs>,
                <fann_get_cascade_candidate_change_fraction at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_candidate_change_fraction>,
                <fann_set_cascade_candidate_change_fraction at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_candidate_change_fraction>

	        This function appears in FANN >= 2.0.0.
         */
        public float CascadeCandidateChangeFraction
        {
            get
            {
                return net.get_cascade_candidate_change_fraction();
            }
            set
            {
                net.set_cascade_output_change_fraction(value);
            }
        }

        /* Property: CascadeCandidateStagnationEpochs

           The number of cascade candidate stagnation epochs determines the number of epochs training is allowed to
           continue without changing the MSE by a fraction of <CascadeCandidateChangeFraction>.
           
           See more info about this parameter in <CascadeCandidateChangeFraction>.

           The default number of cascade candidate stagnation epochs is 12.

           See also:
   		        <CascadeCandidateChangeFraction>,
                <fann_get_cascade_candidate_stagnation_epochs at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_candidate_stagnation_epochs>,
                <fann_set_cascade_candidate_stagnation_epochs at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_candidate_stagnation_epochs>

	        This function appears in FANN >= 2.0.0.
         */
        public uint CascadeCandidateStagnationEpochs
        {
            get
            {
                return net.get_cascade_candidate_stagnation_epochs();
            }
            set
            {
                net.set_cascade_candidate_stagnation_epochs(value);
            }
        }

        /* Property: CascadeWeightMultiplier

           The weight multiplier is a parameter which is used to multiply the weights from the candidate neuron
           before adding the neuron to the neural network. This parameter is usually between 0 and 1, and is used
           to make the training a bit less aggressive.

           The default weight multiplier is 0.4

           See also:
   		        <fann_get_cascade_weight_multiplier at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_weight_multiplier>,
   		        <fann_set_cascade_weight_multiplier at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_weight_multiplier>

	        This function appears in FANN >= 2.0.0.
         */
        public float CascadeWeightMultiplier
        {
            get
            {
                return net.get_cascade_weight_multiplier();
            }
            set
            {
                net.set_cascade_weight_multiplier(value);
            }
        }

        /* Property: CascadeCandidateLimit

           The candidate limit is a limit for how much the candidate neuron may be trained.
           The limit is a limit on the proportion between the MSE and candidate score.
           
           Set this to a lower value to avoid overfitting and to a higher if overfitting is
           not a problem.
           
           The default candidate limit is 1000.0

           See also:
   		        <fann_get_cascade_candidate_limit at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_candidate_limit>,
   		        <fann_set_cascade_candidate_limit at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_candidate_limit>

	        This function appears in FANN >= 2.0.0.
         */
        public float CascadeCandidateLimit
        {
            get
            {
                return net.get_cascade_candidate_limit();
            }
            set
            {
                net.set_cascade_candidate_limit(value);
            }
        }

        /* Property: CascadeMaxOutEpochs

           The maximum out epochs determines the maximum number of epochs the output connections
           may be trained after adding a new candidate neuron.
           
           The default max out epochs is 150

           See also:
   		        <fann_get_cascade_max_out_epochs at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_max_out_epochs>,
   		        <fann_set_cascade_max_out_epochs at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_max_out_epochs>

	        This function appears in FANN >= 2.0.0.
         */
        public uint CascadeMaxOutEpochs
        {
            get
            {
                return net.get_cascade_max_out_epochs();
            }
            set
            {
                net.set_cascade_max_out_epochs(value);
            }
        }

        /* Property: CascadeMaxCandEpochs

           The maximum candidate epochs determines the maximum number of epochs the input 
           connections to the candidates may be trained before adding a new candidate neuron.
           
           The default max candidate epochs is 150

           See also:
   		        <fann_get_cascade_max_cand_epochs at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_max_cand_epochs>,
   		        <fann_set_cascade_max_cand_epochs at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_max_cand_epochs>

	        This function appears in FANN >= 2.0.0.
         */
        public uint CascadeMaxCandEpochs
        {
            get
            {
                return net.get_cascade_max_cand_epochs();
            }
            set
            {
                net.set_cascade_max_cand_epochs(value);
            }
        }

        /* Method: CascadeCandidatesCount

           The number of candidates used during training (calculated by multiplying <CascadeActivationFunctionsCount>,
           <CascadeActivationSteepnessesCount> and <CascadeCandidateGroupsCount>). 

           The actual candidates is defined by the <CascadeActivationFunctions> and 
           <CascadeActivationSteepnesses> arrays. These arrays define the activation functions 
           and activation steepnesses used for the candidate neurons. If there are 2 activation functions
           in the activation function array and 3 steepnesses in the steepness array, then there will be 
           2x3=6 different candidates which will be trained. These 6 different candidates can be copied into
           several candidate groups, where the only difference between these groups is the initial weights.
           If the number of groups is set to 2, then the number of candidate neurons will be 2x3x2=12. The 
           number of candidate groups is defined by <CascadeCandidateGroupsCount>.

           The default number of candidates is 6x4x2 = 48

           See also:
   		        <CascadeActivationFunctions>, <CascadeActivationFunctionsCount>, 
   		        <CascadeActivationSteepnesses>, <CascadeActivationSteepnessesCount>,
   		        <CascadeCandidateGroupsCount>,
                <fann_get_cascade_num_candidates at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_num_candidates>

	        This function appears in FANN >= 2.0.0.
         */
        public uint CascadeCandidatesCount
        {
            get
            {
                return net.get_cascade_num_candidates();
            }
        }

        /* Property: CascadeActivationFunctionsCount

           The number of activation functions in the <CascadeActivationFunctions> array.

           The default number of activation functions is 10.

           See also:
   		        <CascadeActivationFunctions>,
                <fann_get_cascade_activation_functions_count at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_activation_functions_count>

	        This function appears in FANN >= 2.0.0.
         */
        public uint CascadeActivationFunctionsCount
        {
            get
            {
                return net.get_cascade_activation_functions_count();
            }
        }

        /* Property: CascadeActivationFunctions

           The cascade activation functions array is an array of the different activation functions used by
           the candidates. 
           
           See <CascadeCandidatesCount> for a description of which candidate neurons will be 
           generated by this array.
           
           See also:
   		        <CascadeActivationFunctionsCount>, <ActivationFunction>

	        This function appears in FANN >= 2.0.0.
         */
        public ActivationFunction[] CascadeActivationFunctions
        {
            get
            {
                int count = (int)net.get_cascade_activation_functions_count();
                using (ActivationFunctionArray result = ActivationFunctionArray.frompointer(net.get_cascade_activation_functions()))
                {
                    ActivationFunction[] arrayResult = new ActivationFunction[net.get_cascade_activation_functions_count()];
                    for (int i = 0; i < count; i++)
                    {
                        arrayResult[i] = result.getitem(i);
                    }
                    return arrayResult;
                }
            }
            set
            {
                using (ActivationFunctionArray input = new ActivationFunctionArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i]);
                    }
                    net.set_cascade_activation_functions(input.cast(), (uint)value.Length);
                }
            }
        }

        /* Property: CascadeActivationSteepnessesCount

           The number of activation steepnesses in the <CascadeActivationFunctions> array.

           The default number of activation steepnesses is 4.

           See also:
   		        <CascadeActivationSteepnesses>, <CascadeActivationFunctions>,
                <fann_get_cascade_activation_steepnesses_count at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_activation_steepnesses_count>

	        This function appears in FANN >= 2.0.0.
         */
        public uint CascadeActivationSteepnessesCount
        {
            get
            {
                return net.get_cascade_activation_steepnesses_count();
            }
        }

        /* Property: CascadeActivationSteepnesses

           The cascade activation steepnesses array is an array of the different activation functions used by
           the candidates.

           See <CascadeCandidatesCount> for a description of which candidate neurons will be 
           generated by this array.

           The default activation steepnesses is {0.25, 0.50, 0.75, 1.00}

           See also:
   		        <CascadeActivationSteepnessesCount>,
                <fann_get_cascade_activation_steepnesses at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_activation_steepnesses>,
                <fann_set_cascade_activation_steepnesses at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_activation_steepnesses>

	        This function appears in FANN >= 2.0.0.
         */
        public float[] CascadeActivationSteepnesses
        {
            get
            {
                using (floatArray result = floatArray.frompointer(net.get_cascade_activation_steepnesses()))
                {
                    uint count = net.get_cascade_activation_steepnesses_count();
                    float[] resultArray = new float[net.get_cascade_activation_steepnesses_count()];
                    for (int i = 0; i < count; i++)
                    {
                        resultArray[i] = result.getitem(i);
                    }
                    return resultArray;
                }
            }
            set
            {
                net.set_cascade_activation_steepnesses(value, (uint)value.Length);
            }
        }

        /* Property: CascadeCandidateGroupsCount

           The number of candidate groups is the number of groups of identical candidates which will be used
           during training.
           
           This number can be used to have more candidates without having to define new parameters for the candidates.
           
           See <CascadeCandidatesCount> for a description of which candidate neurons will be 
           generated by this parameter.
           
           The default number of candidate groups is 2

           See also:
   		        <fann_get_cascade_num_candidate_groups at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_get_cascade_num_candidate_groups>,
   		        <fann_set_cascade_num_candidate_groups at http://libfann.github.io/fann/docs/files/fann_cascade-h.html#fann_set_cascade_num_candidate_groups>

	        This function appears in FANN >= 2.0.0.
         */
        public uint CascadeCandidateGroupsCount
        {
            get
            {
                return net.get_cascade_num_candidate_groups();
            }
            set
            {
                net.set_cascade_num_candidate_groups(value);
            }
        }

        /*********************************************************************/


        /* Method: ScaleTrain

           Scale input and output data based on previously calculated parameters.

           See also:
   		        <DescaleTrain>, <fann_scale_train at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_scale_train>

	        This function appears in FANN >= 2.1.0.
         */
        public void ScaleTrain(TrainingData data)
        {
            net.scale_train(data.InternalData);
        }

        /* Method: DescaleTrain

           Descale input and output data based on previously calculated parameters.

           See also:
   		        <ScaleTrain>, <fann_descale_train at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_descale_train>

	        This function appears in FANN >= 2.1.0.
         */
        public void DescaleTrain(TrainingData data)
        {
            net.descale_train(data.InternalData);
        }

        /* Method: SetInputScalingParams

           Calculate scaling parameters for future use based on training data.

           See also:
   		        <SetOutputScalingParams>,
                <fann_set_input_scaling_params at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_input_scaling_params>

	        This function appears in FANN >= 2.1.0.
         */
        public bool SetInputScalingParams(TrainingData data, float newInputMin, float newInputMax)
        {
            return net.set_input_scaling_params(data.InternalData, newInputMin, newInputMax);
        }

        /* Method: SetOutputScalingParams

           Calculate scaling parameters for future use based on training data.

           See also:
   		        <SetInputScalingParams>,
                <fann_set_output_scaling_params at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_output_scaling_params>

	        This function appears in FANN >= 2.1.0.
         */
        public bool SetOutputScalingParams(TrainingData data, float newOutputMin, float newOutputMax)
        {
            return net.set_output_scaling_params(data.InternalData, newOutputMin, newOutputMax);
        }

        /* Method: SetScalingParams

           Calculate scaling parameters for future use based on training data.

           See also:
   		        <ClearScalingParams>,
                <fann_set_scaling_params at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_set_scaling_params>

	        This function appears in FANN >= 2.1.0.
         */
        public bool SetScalingParams(TrainingData data, float newInputMin, float newInputMax, float newOutputMin, float newOutputMax)
        {
            return net.set_scaling_params(data.InternalData, newInputMin, newInputMax, newOutputMin, newOutputMax);
        }

        /* Method: ClearScalingParams

           Clears scaling parameters.

           See also:
   		        <SetScalingParams>,
                <fann_clear_scaling_params at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_clear_scaling_params>

	        This function appears in FANN >= 2.1.0.
         */
        public bool ClearScalingParams()
        {
            return net.clear_scaling_params();
        }

        /* Method: ScaleInput

           Scale data in input vector before feed it to ann based on previously calculated parameters.

           See also:
   		        <DescaleInput>, <ScaleOutput>,
                <fann_scale_input at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_scale_input>

	        This function appears in FANN >= 2.1.0.
         */
        public void ScaleInput(DataAccessor input)
        {
            net.scale_input(input.Cast());
        }

        /* Method: ScaleOutput

           Scale data in output vector before feed it to ann based on previously calculated parameters.

           See also:
   		        <DescaleOutput>, <ScaleInput>,
                <fann_scale_output at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_scale_output>

	        This function appears in FANN >= 2.1.0.
         */
        public void ScaleOutput(DataAccessor output)
        {
            net.scale_output(output.Cast());
        }

        /* Method: DescaleInput

           Scale data in input vector after get it from ann based on previously calculated parameters.

           See also:
   		        <ScaleInput>, <DescaleOutput>,
                <fann_descale_input at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_descale_input>

	        This function appears in FANN >= 2.1.0.
         */
        public void DescaleInput(float[] input)
        {
            net.descale_input_(input);
        }

        /* Method: DescaleOutput

           Scale data in output vector after get it from ann based on previously calculated parameters.

           See also:
   		        <ScaleOutput>, <DescaleInput>,
                <fann_descale_output at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_descale_output>

	        This function appears in FANN >= 2.1.0.
         */
        public void DescaleOutput(float[] output)
        {
            net.descale_output_(output);
        }

        /*********************************************************************/

        /* Property: ErrorLog

           Change where errors are logged to.
           
           If the value is NULL, no errors will be printed.
           
           If NeuralNet is empty the default log will be set.
           The default log is the log used when creating a NeuralNet.
           This default log will also be the default for all new NeuralNet
           that are created.
           
           The default behavior is to log them to Console.Error.
           
           See also:
                <struct fann_error at http://libfann.github.io/fann/docs/files/fann_data-h.html#struct_fann_error>,
                <fann_set_error_log at http://libfann.github.io/fann/docs/files/fann_error-h.html#fann_set_error_log>
           
           This function appears in FANN >= 1.1.0.   
         */
        public FannFile ErrorLog
        {
            set
            {
                net.set_error_log(value.InternalFile);
            }
        }

        /* Property: ErrNo

           Returns the last error number.
           
           See also:
            <fann_errno_enum at http://libfann.github.io/fann/docs/files/fann_error-h.html#fann_errno_enum>,
            <fann_reset_errno at http://libfann.github.io/fann/docs/files/fann_error-h.html#fann_reset_errno>,
            <fann_get_errno at http://libfann.github.io/fann/docs/files/fann_error-h.html#fann_get_errno>
            
           This function appears in FANN >= 1.1.0.   
         */
        public uint ErrNo
        {
            get
            {
                return net.get_errno();
            }
        }

        /* Method:  ResetErrno

           Resets the last error number.
           
           This function appears in FANN >= 1.1.0.   
         */
        public void ResetErrno()
        {
            net.reset_errno();
        }

        /* Method:  ResetErrstr

           Resets the last error string.

           This function appears in FANN >= 1.1.0.   
         */
        public void ResetErrstr()
        {
            net.reset_errstr();
        }

        /* Property: ErrStr

           Returns the last errstr.
          
           This function calls <fann_reset_errno at http://libfann.github.io/fann/docs/files/fann_error-h.html#fann_reset_errno> and <fann_reset_errstr at http://libfann.github.io/fann/docs/files/fann_error-h.html#fann_reset_errstr>

           This function appears in FANN >= 1.1.0.   
         */
        public string ErrStr
        {
            get
            {
                return net.get_errstr();
            }
        }

        /* Method:  PrintError

           Prints the last error to Console.Error.

           This function appears in FANN >= 1.1.0.   
         */
        public void PrintError()
        {
            net.print_error();
        }

        /* Method: DisableSeedRand

           Disables the automatic random generator seeding that happens in FANN.

           Per default FANN will always seed the random generator when creating a new network,
           unless FANN_NO_SEED is defined during compilation of the library. This method can
           disable this at runtime.

           This function appears in FANN >= 2.3.0
        */
        public void DisableSeedRand()
        {
            net.disable_seed_rand();
        }

        /* Method: EnableSeedRand

           Enables the automatic random generator seeding that happens in FANN.

           Per default FANN will always seed the random generator when creating a new network,
           unless FANN_NO_SEED is defined during compilation of the library. This method can
           disable this at runtime.

           This function appears in FANN >= 2.3.0
        */
        public void EnableSeedRand()
        {
            net.enable_seed_rand();
        }

        /* Method: TrainEpochBatchParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
           
        */
        public float TrainEpochBatchParallel(TrainingData data, uint threadNumb)
        {
            return fannfloat.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb);
        }

        /* Method: TrainEpochIrpropmParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
           
        */
        public float TrainEpochIrpropmParallel(TrainingData data, uint threadNumb)
        {
            return fannfloat.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb);
        }

        /* Method: TrainEpochQuickpropParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
           
        */
        public float TrainEpochQuickpropParallel(TrainingData data, uint threadNumb)
        {
            return fannfloat.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb);
        }

        /* Method: TrainEpochSarpropParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
           
        */
        public float TrainEpochSarpropParallel(TrainingData data, uint threadNumb)
        {
            return fannfloat.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb);
        }

        /* Method: TrainEpochIncrementalMod
           
            Parameters:
                data - the data to train on
           
        */
        public float TrainEpochIncrementalMod(TrainingData data)
        {
            return fannfloat.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData());
        }

        /* Method: TrainEpochBatchParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
                predictedOutputs - the predicted outputs
           
        */
        public float TrainEpochBatchParallel(TrainingData data, uint threadNumb, List<List<float>> predictedOutputs)
        {
            using (floatVectorVector predicted_out = new floatVectorVector(predictedOutputs.Count))
            {
                for (int i = 0; i < predictedOutputs.Count; i++)
                {
                    predicted_out[i] = new floatVector(predictedOutputs[i].Count);
                }

                float result = fannfloat.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb, predicted_out);

                predictedOutputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predictedOutputs.Add(list);
                }
                return result;
            }
        }

        /* Method: TrainEpochIrpropmParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
                predictedOutputs - the predicted outputs
           
        */
        public float TrainEpochIrpropmParallel(TrainingData data, uint threadNumb, List<List<float>> predictedOutputs)
        {
            using (floatVectorVector predicted_out = new floatVectorVector(predictedOutputs.Count))
            {
                for (int i = 0; i < predictedOutputs.Count; i++)
                {
                    predicted_out[i] = new floatVector(predictedOutputs[i].Count);
                }
                float result = fannfloat.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb, predicted_out);

                predictedOutputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predictedOutputs.Add(list);
                }
                return result;
            }
        }

        /* Method: TrainEpochQuickpropParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
                predictedOutputs - the predicted outputs
           
        */
        public float TrainEpochQuickpropParallel(TrainingData data, uint threadNumb, List<List<float>> predictedOutputs)
        {
            using (floatVectorVector predicted_out = new floatVectorVector(predictedOutputs.Count))
            {
                for (int i = 0; i < predictedOutputs.Count; i++)
                {
                    predicted_out[i] = new floatVector(predictedOutputs[i].Count);
                }
                float result = fannfloat.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb, predicted_out);

                predictedOutputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predictedOutputs.Add(list);
                }
                return result;
            }
        }

        /* Method: TrainEpochSarpropParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
                predictedOutputs - the predicted outputs
           
        */
        public float TrainEpochSarpropParallel(TrainingData data, uint threadNumb, List<List<float>> predictedOutputs)
        {
            using (floatVectorVector predicted_out = new floatVectorVector(predictedOutputs.Count))
            {
                for (int i = 0; i < predictedOutputs.Count; i++)
                {
                    predicted_out[i] = new floatVector(predictedOutputs[i].Count);
                }
                float result = fannfloat.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb, predicted_out);

                predictedOutputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predictedOutputs.Add(list);
                }
                return result;
            }
        }

        /* Method: TrainEpochIncrementalMod
           
            Parameters:
                data - the data to train on
                predictedOutputs - the predicted outputs
           
        */
        public float TrainEpochIncrementalMod(TrainingData data, List<List<float>> predictedOutputs)
        {
            using (floatVectorVector predicted_out = new floatVectorVector(predictedOutputs.Count))
            {
                for (int i = 0; i < predictedOutputs.Count; i++)
                {
                    predicted_out[i] = new floatVector(predictedOutputs[i].Count);
                }
                float result = fannfloat.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData(), predicted_out);

                predictedOutputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predictedOutputs.Add(list);
                }
                return result;
            }
        }

        /* Method: TestDataParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
           
        */
        public float TestDataParallel(TrainingData data, uint threadNumb)
        {
            return fannfloat.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb);
        }

        /* Method: TestDataParallel
           
            Parameters:
                data - the data to train on
                threadNumb - the thread to do training on
                predictedOutputs - the predicted outputs
           
        */
        public float TestDataParallel(TrainingData data, uint threadNumb, List<List<float>> predictedOutputs)
        {
            using (floatVectorVector predicted_out = new floatVectorVector(predictedOutputs.Count))
            {
                for (int i = 0; i < predictedOutputs.Count; i++)
                {
                    predicted_out[i] = new floatVector(predictedOutputs[i].Count);
                }
                float result = fannfloat.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadNumb, predicted_out);

                predictedOutputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predictedOutputs.Add(list);
                }
                return result;
            }
        }

        private int InternalCallback(global::System.IntPtr netPtr, global::System.IntPtr dataPtr, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, global::System.IntPtr user_data)
        {
            NeuralNet callbackNet = new NeuralNet(new neural_net(netPtr, false));
            TrainingData callbackData = new TrainingData(new training_data(dataPtr, false));
            GCHandle handle = (GCHandle)user_data;
            return Callback(callbackNet, callbackData, max_epochs, epochs_between_reports, desired_error, epochs, handle.Target as Object);
        }


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate int training_callback(global::System.IntPtr net, global::System.IntPtr data, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, global::System.IntPtr user_data);


        /* Delegate: TrainingCallback
           This callback function can be called during training when using <TrainOnData>,
           <TrainOnFile> or <CascadetrainOnData>

            The callback can be set by using <SetCallback> and is very useful for doing custom
            things during training. It is recommended to use this function when implementing custom
            training procedures, or when visualizing the training in a GUI etc. The parameters which the
            callback function takes is the parameters given to the <TrainOnData>, plus an epochs
            parameter which tells how many epochs the training have taken so far.

            The callback function should return an integer, if the callback function returns -1, the training
            will terminate.

            Example of a callback function that prints information to the Console:
                >int PrintCallback(NeuralNet net, TrainingData data,
                >    uint maxEpochs, uint epochsBetweenReports,
                >    float desiredError, uint epochs, Object userData)
                >{
                >    Console.WriteLine("Epochs     {0}. Current Error: {1}", epochs.ToString("00000000"), net.MSE.ToString().PadRight(8));
                >}

            See also:
                <SetCallback>, <fann_callback_type at http://libfann.github.io/fann/docs/files/fann_data-h.html#fann_callback_type>
         */
        public delegate int TrainingCallback(NeuralNet net, TrainingData data, uint maxEpochs, uint epochsBetweenReports, float desiredError, uint epochs, Object userData);

        #region Properties
        internal neural_net Net
        {
            get
            {
                return net;
            }
        }
        private TrainingCallback Callback { get; set; }
        private training_callback UnmanagedCallback { get; set; }
        private Object UserData { get; set; }

        private uint Outputs { get; set; }
        #endregion Properties
    }
}
