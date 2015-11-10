using System;
using FannWrapperFixed;
using FannWrapper;
using System.Collections.Generic;

namespace FANNCSharp
{
    /// <summary> A neural net fixed. </summary>
    ///
    /// <remarks> Joel Self, 11/10/2015. </remarks>

    public class NeuralNetFixed : IDisposable
    {
        neural_net net = null;

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="other"> The other. </param>

        public NeuralNetFixed(NeuralNetFixed other)
        {
           net = new neural_net(other.InternalFixedNet);
        }

        /// <summary> Gets decimal point. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <returns> The decimal point. </returns>

        public uint GetDecimalPoint()
        {
            return net.get_decimal_point();
        }

        /// <summary> Gets the multiplier. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <returns> The multiplier. </returns>

        public uint GetMultiplier()
        {
            return net.get_multiplier();
        }

        /// <summary> Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void Dispose()
        {
            net.destroy();
        }


        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="netType">    Type of the net. </param>
        /// <param name="numLayers">  Number of layers. </param>
        /// <param name="uint[]args"> A variable-length parameters list containing arguments. </param>

        public NeuralNetFixed(NetworkType netType, uint numLayers, params uint[]args)
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

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="netType"> Type of the net. </param>
        /// <param name="layers">  The layers. </param>

        public NeuralNetFixed(NetworkType netType, ICollection<uint> layers)
        {
            using (uintArray newLayers = new uintArray(layers.Count))
            {
                IEnumerator<uint> enumerator = layers.GetEnumerator();
                int i = 0;
                do
                {
                    newLayers.setitem(i, enumerator.Current);
                    i++;
                } while (enumerator.MoveNext());
                Outputs = newLayers.getitem(layers.Count - 1);
                net = new neural_net(netType, (uint)layers.Count, newLayers.cast());
            }
        }

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="connectionRate"> The connection rate. </param>
        /// <param name="numLayers">      Number of layers. </param>
        /// <param name="args">           A variable-length parameters list containing arguments. </param>

        public NeuralNetFixed(float connectionRate, uint numLayers, params uint[] args)
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

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="connectionRate"> The connection rate. </param>
        /// <param name="layers">         The layers. </param>

        public NeuralNetFixed(float connectionRate, ICollection<uint> layers)
        {
            using (uintArray newLayers = new uintArray(layers.Count))
            {
                IEnumerator<uint> enumerator = layers.GetEnumerator();
                int i = 0;
                do
                {
                    newLayers.setitem(i, enumerator.Current);
                    i++;
                } while (enumerator.MoveNext());
                Outputs = newLayers.getitem(layers.Count - 1);
                net = new neural_net(connectionRate, (uint)layers.Count, newLayers.cast());
            }
        }

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="filename"> Filename of the file. </param>

        public NeuralNetFixed(string filename)
        {
            net = new neural_net(filename);
        }

        /// <summary> Runs the given input. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="input"> The input. </param>
        ///
        /// <returns> A int[]. </returns>

        public int[] Run(int[] input)
        {
            using (intArray ints = new intArray(input.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    ints.setitem(i, input[i]);
                }
                using (intArray outputs = intArray.frompointer(net.run(ints.cast())))
                {
                    int[] result = new int[Outputs];
                    for (int i = 0; i < Outputs; i++)
                    {
                        result[i] = outputs.getitem(i);
                    }
                    return result;
                }
            }
        }

        /// <summary> Randomize weights. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="minWeight"> The minimum weight. </param>
        /// <param name="maxWeight"> The maximum weight. </param>

        public void RandomizeWeights(int minWeight, int maxWeight)
        {
           net.randomize_weights(minWeight, maxWeight);
        }

        /// <summary> Initialises the weights. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>

        public void InitWeights(TrainingDataFixed data)
        {
           net.init_weights(data.InternalData);
        }

        /// <summary> Print connections. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void PrintConnections()
        {
           net.print_connections();
        }

        /// <summary> Saves the given file. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="file"> The file. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>

        public bool Save(string file)
        {
            return net.save(file);
        }

        /// <summary> Saves to fixed. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="file"> The file. </param>
        ///
        /// <returns> An int. </returns>

        public int SaveToFixed(string file)
        {
            return net.save_to_fixed(file);
        }

        /// <summary> Tests. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="input">         The input. </param>
        /// <param name="desiredOutput"> The desired output. </param>
        ///
        /// <returns> A int[]. </returns>

        public int[] Test(int[] input, int[] desiredOutput)
        {
            using (intArray intsIn = new intArray(input.Length))
            using (intArray intsOut = new intArray(desiredOutput.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    intsIn.setitem(i, input[i]);
                }
                for (int i = 0; i < desiredOutput.Length; i++)
                {
                    intsOut.setitem(i, desiredOutput[i]);
                }
                intArray result = intArray.frompointer(net.test(intsIn.cast(), intsOut.cast()));
                int[] arrayResult = new int[Outputs];
                for (int i = 0; i < Outputs; i++)
                {
                    arrayResult[i] = result.getitem(i);
                }
                return arrayResult;
            }
        }

        /// <summary> Tests data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>
        ///
        /// <returns> A float. </returns>

        public float TestData(TrainingDataFixed data)
        {
            return net.test_data(data.InternalData);
        }

        /// <summary> Gets the mse. </summary>
        ///
        /// <value> The mse. </value>

        public float MSE
        {
            get
            {
                return net.get_MSE();
            }
        }

        /// <summary> Resets the mse. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void ResetMSE()
        {
           net.reset_MSE();
        }

        /// <summary> Print parameters. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void PrintParameters()
        {
           net.print_parameters();
        }

        /// <summary> Gets or sets the training algorithm. </summary>
        ///
        /// <value> The training algorithm. </value>

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

        /// <summary> Gets or sets the learning rate. </summary>
        ///
        /// <value> The learning rate. </value>

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

        /// <summary> Gets activation function. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="layer">  The layer. </param>
        /// <param name="neuron"> The neuron. </param>
        ///
        /// <returns> The activation function. </returns>

        public ActivationFunction GetActivationFunction(int layer, int neuron)
        {
            return net.get_activation_function(layer, neuron);
        }

        /// <summary> Sets activation function. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="function"> The function. </param>
        /// <param name="layer">    The layer. </param>
        /// <param name="neuron">   The neuron. </param>

        public void SetActivationFunction(ActivationFunction function, int layer, int neuron)
        {
           net.set_activation_function(function, layer, neuron);
        }

        /// <summary> Sets activation function layer. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="function"> The function. </param>
        /// <param name="layer">    The layer. </param>

        public void SetActivationFunctionLayer(ActivationFunction function, int layer)
        {
           net.set_activation_function_layer(function, layer);
        }

        /// <summary> Sets the activation function hidden. </summary>
        ///
        /// <value> The activation function hidden. </value>

        public ActivationFunction ActivationFunctionHidden
        {
            set
            {
                net.set_activation_function_hidden(value);
            }
        }

        /// <summary> Sets the activation function output. </summary>
        ///
        /// <value> The activation function output. </value>

        public ActivationFunction ActivationFunctionOutput
        {
            set
            {
                net.set_activation_function_output(value);
            }
        }

        /// <summary> Gets activation steepness. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="layer">  The layer. </param>
        /// <param name="neuron"> The neuron. </param>
        ///
        /// <returns> The activation steepness. </returns>

        public int GetActivationSteepness(int layer, int neuron)
        {
            return net.get_activation_steepness(layer, neuron);
        }

        /// <summary> Sets activation steepness. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="steepness"> The steepness. </param>
        /// <param name="layer">     The layer. </param>
        /// <param name="neuron">    The neuron. </param>

        public void SetActivationSteepness(int steepness, int layer, int neuron)
        {
           net.set_activation_steepness(steepness, layer, neuron);
        }

        /// <summary> Sets activation steepness layer. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="steepness"> The steepness. </param>
        /// <param name="layer">     The layer. </param>

        public void SetActivationSteepnessLayer(int steepness, int layer)
        {
           net.set_activation_steepness_layer(steepness, layer);
        }

        /// <summary> Sets activation steepness hidden. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="steepness"> The steepness. </param>

        public void SetActivationSteepnessHidden(int steepness)
        {
           net.set_activation_steepness_hidden(steepness);
        }

        /// <summary> Sets activation steepness output. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="steepness"> The steepness. </param>

        public void SetActivationSteepnessOutput(int steepness)
        {
           net.set_activation_steepness_output(steepness);
        }

        /// <summary> Gets or sets the train error function. </summary>
        ///
        /// <value> The train error function. </value>

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

        /// <summary> Gets or sets the quickprop decay. </summary>
        ///
        /// <value> The quickprop decay. </value>

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

        /// <summary> Gets or sets the quickprop mu. </summary>
        ///
        /// <value> The quickprop mu. </value>

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

        /// <summary> Gets or sets the rprop increase factor. </summary>
        ///
        /// <value> The rprop increase factor. </value>

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

        /// <summary> Gets or sets the rprop decrease factor. </summary>
        ///
        /// <value> The rprop decrease factor. </value>

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

        /// <summary> Gets or sets the rprop delta zero. </summary>
        ///
        /// <value> The rprop delta zero. </value>

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

        /// <summary> Gets or sets the rprop delta minimum. </summary>
        ///
        /// <value> The rprop delta minimum. </value>

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

        /// <summary> Gets or sets the rprop delta maximum. </summary>
        ///
        /// <value> The rprop delta maximum. </value>

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

        /// <summary> Gets or sets the sarprop weight decay shift. </summary>
        ///
        /// <value> The sarprop weight decay shift. </value>

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

        /// <summary> Gets or sets the sarprop step error threshold factor. </summary>
        ///
        /// <value> The sarprop step error threshold factor. </value>

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

        /// <summary> Gets or sets the sarprop step error shift. </summary>
        ///
        /// <value> The sarprop step error shift. </value>

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

        /// <summary> Gets or sets the sarprop temperature. </summary>
        ///
        /// <value> The sarprop temperature. </value>

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

        /// <summary> Gets the number of inputs. </summary>
        ///
        /// <value> The number of inputs. </value>

        public uint InputCount
        {
            get
            {
                return net.get_num_input();
            }
        }

        /// <summary> Gets the number of outputs. </summary>
        ///
        /// <value> The number of outputs. </value>

        public uint OutputCount
        {
            get
            {
                return net.get_num_output();
            }
        }

        /// <summary> Gets the total number of neurons. </summary>
        ///
        /// <value> The total number of neurons. </value>

        public uint TotalNeurons
        {
            get
            {
                return net.get_total_neurons();
            }
        }

        /// <summary> Gets the total number of connections. </summary>
        ///
        /// <value> The total number of connections. </value>

        public uint TotalConnections
        {
            get
            {
                return net.get_total_connections();
            }
        }

        /// <summary> Gets the type of the network. </summary>
        ///
        /// <value> The type of the network. </value>

        public NetworkType NetworkType
        {
            get
            {
                return net.get_network_type();
            }
        }

        /// <summary> Gets the connection rate. </summary>
        ///
        /// <value> The connection rate. </value>

        public float ConnectionRate
        {
            get
            {
                return net.get_connection_rate();
            }
        }

        /// <summary> Gets the number of layers. </summary>
        ///
        /// <value> The number of layers. </value>

        public uint LayerCount
        {
            get
            {
                return net.get_num_layers();
            }
        }

        /// <summary> Gets an array of layers. </summary>
        ///
        /// <value> An Array of layers. </value>

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

        /// <summary> Gets an array of bias. </summary>
        ///
        /// <value> An Array of bias. </value>

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

        /// <summary> Gets an array of connections. </summary>
        ///
        /// <value> An Array of connections. </value>

        public Connection[] ConnectionArray
        {
            get {
                uint count = net.get_total_connections();
                Connection[] connections = new Connection[count];
                using (ConnectionArray output = new ConnectionArray(connections.Length))
                {
                   net.get_connection_array(output.cast());
                    for (uint i = 0; i < count; i++)
                    {
                        connections[i] = output.getitem((int)i);
                    }
                }
                return connections;
            }
        }

        /// <summary> Sets an array of weights. </summary>
        ///
        /// <value> An Array of weights. </value>

        public Connection[] WeightArray
        {
            set
            {
                using (ConnectionArray input = new ConnectionArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i]);
                    }
                    net.set_weight_array(input.cast(), (uint)value.Length);
                }
            }
        }

        /// <summary> Sets a weight. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="from_neuron"> from neuron. </param>
        /// <param name="to_neuron">   to neuron. </param>
        /// <param name="weight">      The weight. </param>

        public void SetWeight(uint from_neuron, uint to_neuron, int weight)
        {
           net.set_weight(from_neuron, to_neuron, weight);
        }

        /// <summary> Gets or sets the learning momentum. </summary>
        ///
        /// <value> The learning momentum. </value>

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

        /// <summary> Gets or sets the train stop function. </summary>
        ///
        /// <value> The train stop function. </value>

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

        /// <summary> Gets or sets the bit fail limit. </summary>
        ///
        /// <value> The bit fail limit. </value>

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

        /// <summary> Gets the bit fail. </summary>
        ///
        /// <value> The bit fail. </value>

        public uint BitFail
        {
            get
            {
                return net.get_bit_fail();
            }
        }

        /// <summary> Sets error log. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="log_file"> The log file. </param>

        public void SetErrorLog(FannFile log_file)
        {
           net.set_error_log(log_file.InternalFile);
        }

        /// <summary> Gets the error no. </summary>
        ///
        /// <value> The error no. </value>

        public uint ErrNo
        {
            get
            {
                return net.get_errno();
            }
        }

        /// <summary> Resets the errno. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void ResetErrno()
        {
           net.reset_errno();
        }

        /// <summary> Resets the errstr. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void ResetErrstr()
        {
           net.reset_errstr();
        }

        /// <summary> Gets the error string. </summary>
        ///
        /// <value> The error string. </value>

        public string ErrStr
        {
            get
            {
                return net.get_errstr();
            }
        }

        /// <summary> Print error. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void PrintError()
        {
           net.print_error();
        }

        /// <summary> Disables the seed random. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void DisableSeedRand()
        {
           net.disable_seed_rand();
        }

        /// <summary> Enables the seed random. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>

        public void EnableSeedRand()
        {
           net.enable_seed_rand();
        }
        #region Properties
        public neural_net InternalFixedNet
        {
            get
            {
                return net;
            }
        }

        private uint Outputs { get; set; }
        #endregion Properties
    }
}
