using System;
using FannWrapperFixed;
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
 *  Title: FANN C# NeuralNet Fixed
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
namespace FANNCSharp.Fixed
{
    /* Class: NeuralNet
        <NeuralNet> is the main neural network class used for both training and execution using ints

        Encapsulation of a int neural network <neural_net at http://libfann.github.io/fann/docs/files/fann_cpp-h.html#neural_net> and
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
            Outputs = net.get_num_output();
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
                foreach(uint count in layers)
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
        public int[] Run(int[] input)
        {
            using (intArray outputs = intArray.frompointer(net.run(input)))
            {
                int[] result = new int[Outputs];
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
        public int[] Run(DataAccessor input)
        {
            using (intArray outputs = intArray.frompointer(net.run(input.Array)))
            {
                int[] result = new int[Outputs];
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
        public void RandomizeWeights(int minWeight, int maxWeight)
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

        /* Method: Test

           Test with a set of inputs, and a set of desired outputs.
           This operation updates the mean square error, but does not
           change the network in any way.
           
           See also:
   		        <TestData>, <Train>, <fann_test at http://libfann.github.io/fann/docs/files/fann_train-h.html#fann_test>
           
           This function appears in FANN >= 1.0.0.
        */
        public int[] Test(int[] input, int[] desiredOutput)
        {
            using (intArray result = intArray.frompointer(net.test(input, desiredOutput)))
            {
                int[] arrayResult = new int[Outputs];
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
        public int GetActivationSteepness(int layer, int neuron)
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
        public void SetActivationSteepness(int steepness, int layer, int neuron)
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
        public void SetActivationSteepnessLayer(int steepness, int layer)
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
        public int ActivationSteepnessHidden
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
        public int ActivationSteepnessOutput
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
        /* Property: DecimalPoint

            Returns the position of the decimal point in the ann.

            This function is only available when the ANN is in fixed point mode.

            The decimal point is described in greater detail in the tutorial <Fixed Point Usage>.

            See also:
                <Fixed Point Usage at http://leenissen.dk/fann/html/files2/fixedpointusage-txt.html>, <Multiplier>, <SaveToFixed>,
                <TrainingData::SaveTrainToFixed>, <fann_get_decimal_point at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_decimal_point>

            This function appears in FANN >= 1.0.0.
        */
        public uint DecimalPoint
        {
            get
            {
                return net.get_decimal_point();
            }
        }

        /* Property: Multiplier

            Returns the multiplier that fix point data is multiplied with.

            This function is only available when the ANN is in fixed point mode.

            The multiplier is the used to convert between floating point and fixed point notation.
            A floating point number is multiplied with the multiplier in order to get the fixed point
            number and visa versa.

            The multiplier is described in greater detail in the tutorial <Fixed Point Usage>.

            See also:
                <Fixed Point Usage at http://leenissen.dk/fann/html/files2/fixedpointusage-txt.html>, <DecimalPoint>, <SaveToFixed>,
                <TrainingData::SaveTrainToFixed>, <fann_get_multiplier at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_multiplier>

            This function appears in FANN >= 1.0.0.
        */
        public uint Multiplier
        {
            get
            {
                return net.get_multiplier();
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

        /* Property: Layers

            Get the number of neurons in each layer in the network.

            Bias is not included so the layers match the create methods.

            See also:
                <fann_get_layer_array at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_layer_array>

           This function appears in FANN >= 2.1.0
        */
        public uint[] Layers
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

        /* Property: Biases

            Get the number of bias in each layer in the network.

            See also:
                <fann_get_bias_array at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_bias_array>

            This function appears in FANN >= 2.1.0
        */
        public uint[] Biases
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

        /* Property: Connections

            Get the connections in the network.

            See also:
                <fann_get_connection_array at http://libfann.github.io/fann/docs/files/fann-h.html#fann_get_connection_array>

           This function appears in FANN >= 2.1.0
        */
        public Connection[] Connections
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

        /* Property: Weights

            Set connections in the network.

            Only the weights can be changed, connections and weights are ignored
            if they do not already exist in the network.

            See also:
                <fann_set_weight_array at http://libfann.github.io/fann/docs/files/fann-h.html#fann_set_weight_array>

           This function appears in FANN >= 2.1.0
        */
        public Connection[] Weights
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
        public void SetWeight(uint fromNeuron, uint toNeuron, int weight)
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
        public int BitFailLimit
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

        #region Properties
        internal neural_net Net
        {
            get
            {
                return net;
            }
        }
        private Object UserData { get; set; }

        private uint Outputs { get; set; }
        #endregion Properties
    }
}
