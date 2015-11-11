using System;
using FannWrapperDouble;
using FannWrapper;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FANNCSharp
{
    /// <summary> A neural net double. </summary>
    ///
    /// <remarks> Joel Self, 11/10/2015. </remarks>

    public class NeuralNetDouble : IDisposable
    {
        neural_net net = null;

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="other"> The other. </param>

        public NeuralNetDouble(NeuralNetDouble other)
        {
           net = new neural_net(other.InternalDoubleNet);
        }

        internal NeuralNetDouble(neural_net other)
        {
            net = other;
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

        public NeuralNetDouble(NetworkType netType, uint numLayers, params uint[]args)
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

        public NeuralNetDouble(NetworkType netType, ICollection<uint> layers)
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

        public NeuralNetDouble(float connectionRate, uint numLayers, params uint[] args)
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

        public NeuralNetDouble(float connectionRate, ICollection<uint> layers)
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

        public NeuralNetDouble(string filename)
        {
            net = new neural_net(filename);
        }

        /// <summary> Runs the given input. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="input"> The input. </param>
        ///
        /// <returns> A double[]. </returns>

        public double[] Run(double[] input)
        {
            using (doubleArray doubles = new doubleArray(input.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    doubles.setitem(i, input[i]);
                }
                using (doubleArray outputs = doubleArray.frompointer(net.run(doubles.cast())))
                {
                    double[] result = new double[Outputs];
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

        public void RandomizeWeights(double minWeight, double maxWeight)
        {
           net.randomize_weights(minWeight, maxWeight);
        }

        /// <summary> Initialises the weights. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>

        public void InitWeights(TrainingDataDouble data)
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

        /// <summary> Trains. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="input">         The input. </param>
        /// <param name="desiredOutput"> The desired output. </param>

        public void Train(double[] input, double[] desiredOutput)
        {
            using (doubleArray doublesIn = new doubleArray(input.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    doublesIn.setitem(i, input[i]);
                }
                doubleArray doublesOut = new doubleArray(desiredOutput.Length);
                for (int i = 0; i < input.Length; i++)
                {
                    doublesOut.setitem(i, input[i]);
                }
               net.train(doublesIn.cast(), doublesOut.cast());
            }
        }

        /// <summary> Train epoch. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpoch(TrainingDataDouble data)
        {
            return net.train_epoch(data.InternalData);
        }

        /// <summary> Train on data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">                 The data. </param>
        /// <param name="maxEpochs">            The maximum epochs. </param>
        /// <param name="epochsBetweenReports"> The epochs between reports. </param>
        /// <param name="desiredError">         The desired error. </param>

        public void TrainOnData(TrainingDataDouble data, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
           net.train_on_data(data.InternalData, maxEpochs, epochsBetweenReports, desiredError);
        }

        /// <summary> Train on file. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="filename">             Filename of the file. </param>
        /// <param name="maxEpochs">            The maximum epochs. </param>
        /// <param name="epochsBetweenReports"> The epochs between reports. </param>
        /// <param name="desiredError">         The desired error. </param>

        public void TrainOnFile(string filename, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
           net.train_on_file(filename, maxEpochs, epochsBetweenReports, desiredError);
        }

        /// <summary> Tests. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="input">         The input. </param>
        /// <param name="desiredOutput"> The desired output. </param>
        ///
        /// <returns> A double[]. </returns>

        public double[] Test(double[] input, double[] desiredOutput)
        {
            using (doubleArray doublesIn = new doubleArray(input.Length))
            using (doubleArray doublesOut = new doubleArray(desiredOutput.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    doublesIn.setitem(i, input[i]);
                }
                for (int i = 0; i < desiredOutput.Length; i++)
                {
                    doublesOut.setitem(i, desiredOutput[i]);
                }
                doubleArray result = doubleArray.frompointer(net.test(doublesIn.cast(), doublesOut.cast()));
                double[] arrayResult = new double[Outputs];
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

        public float TestData(TrainingDataDouble data)
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

        public double GetActivationSteepness(int layer, int neuron)
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

        public void SetActivationSteepness(double steepness, int layer, int neuron)
        {
           net.set_activation_steepness(steepness, layer, neuron);
        }

        /// <summary> Sets activation steepness layer. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="steepness"> The steepness. </param>
        /// <param name="layer">     The layer. </param>

        public void SetActivationSteepnessLayer(double steepness, int layer)
        {
           net.set_activation_steepness_layer(steepness, layer);
        }

        /// <summary> Sets activation steepness hidden. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="steepness"> The steepness. </param>

        public void SetActivationSteepnessHidden(double steepness)
        {
           net.set_activation_steepness_hidden(steepness);
        }

        /// <summary> Sets activation steepness output. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="steepness"> The steepness. </param>

        public void SetActivationSteepnessOutput(double steepness)
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

        public void SetWeight(uint from_neuron, uint to_neuron, double weight)
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

        public double BitFailLimit
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

        /// <summary> Cascadetrain on data. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">                    The data. </param>
        /// <param name="max_neurons">             The maximum neurons. </param>
        /// <param name="neurons_between_reports"> The neurons between reports. </param>
        /// <param name="desired_error">           The desired error. </param>

        public void CascadetrainOnData(TrainingDataDouble data, uint max_neurons, uint neurons_between_reports, float desired_error)
        {
           net.cascadetrain_on_data(data.InternalData, max_neurons, neurons_between_reports, desired_error);
        }

        /// <summary> Cascadetrain on file. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="filename">                Filename of the file. </param>
        /// <param name="max_neurons">             The maximum neurons. </param>
        /// <param name="neurons_between_reports"> The neurons between reports. </param>
        /// <param name="desired_error">           The desired error. </param>

        public void CascadetrainOnFile(string filename, uint max_neurons, uint neurons_between_reports, float desired_error)
        {
           net.cascadetrain_on_file(filename, max_neurons, neurons_between_reports, desired_error);
        }

        /// <summary> Gets or sets the cascade output change fraction. </summary>
        ///
        /// <value> The cascade output change fraction. </value>

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

        /// <summary> Gets or sets the cascade output stagnation epochs. </summary>
        ///
        /// <value> The cascade output stagnation epochs. </value>

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

        /// <summary> Gets or sets the cascade candidate change fraction. </summary>
        ///
        /// <value> The cascade candidate change fraction. </value>

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

        /// <summary> Gets or sets the cascade candidate stagnation epochs. </summary>
        ///
        /// <value> The cascade candidate stagnation epochs. </value>

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

        /// <summary> Gets or sets the cascade weight multiplier. </summary>
        ///
        /// <value> The cascade weight multiplier. </value>

        public double CascadeWeightMultiplier
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

        /// <summary> Gets or sets the cascade candidate limit. </summary>
        ///
        /// <value> The cascade candidate limit. </value>

        public double CascadeCandidateLimit
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

        /// <summary> Gets or sets the cascade maximum out epochs. </summary>
        ///
        /// <value> The cascade maximum out epochs. </value>

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

        /// <summary> Gets or sets the cascade maximum cand epochs. </summary>
        ///
        /// <value> The cascade maximum cand epochs. </value>

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

        /// <summary> Gets the number of cascade candidates. </summary>
        ///
        /// <value> The number of cascade candidates. </value>

        public uint CascadeCandidatesCount
        {
            get
            {
                return net.get_cascade_num_candidates();
            }
        }

        /// <summary> Gets the number of cascade activation functions. </summary>
        ///
        /// <value> The number of cascade activation functions. </value>

        public uint CascadeActivationFunctionsCount
        {
            get
            {
                return net.get_cascade_activation_functions_count();
            }
        }

        /// <summary> Gets or sets the cascade activation functions. </summary>
        ///
        /// <value> The cascade activation functions. </value>

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

        /// <summary> Gets the number of cascade activation steepnesses. </summary>
        ///
        /// <value> The number of cascade activation steepnesses. </value>

        public uint CascadeActivationSteepnessesCount
        {
            get
            {
                return net.get_cascade_activation_steepnesses_count();
            }
        }

        /// <summary> Gets or sets the cascade activation steepnesses. </summary>
        ///
        /// <value> The cascade activation steepnesses. </value>

        public double[] CascadeActivationSteepnesses
        {
            get
            {
                using (doubleArray result = doubleArray.frompointer(net.get_cascade_activation_steepnesses()))
                {
                    uint count = net.get_cascade_activation_steepnesses_count();
                    double[] resultArray = new double[net.get_cascade_activation_steepnesses_count()];
                    for (int i = 0; i < count; i++)
                    {
                        resultArray[i] = result.getitem(i);
                    }
                    return resultArray;
                }
            }
            set
            {
                using (doubleArray input = new doubleArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i]);
                    }
                    net.set_cascade_activation_steepnesses(input.cast(), (uint)value.Length);
                    for (int i = 0; i < value.Length; i++)
                    {
                        value[i] = input.getitem(i);
                    }
                }
            }
        }

        /// <summary> Gets or sets the number of cascade candidate groups. </summary>
        ///
        /// <value> The number of cascade candidate groups. </value>

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

        /// <summary> Scale train. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>

        public void ScaleTrain(TrainingDataDouble data)
        {
           net.scale_train(data.InternalData);
        }

        /// <summary> Descale train. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>

        public void DescaleTrain(TrainingDataDouble data)
        {
           net.descale_train(data.InternalData);
        }

        /// <summary> Sets input scaling parameters. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">          The data. </param>
        /// <param name="new_input_min"> The new input minimum. </param>
        /// <param name="new_input_max"> The new input maximum. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>

        public bool SetInputScalingParams(TrainingDataDouble data, float new_input_min, float new_input_max)
        {
            return net.set_input_scaling_params(data.InternalData, new_input_min, new_input_max);
        }

        /// <summary> Sets output scaling parameters. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">           The data. </param>
        /// <param name="new_output_min"> The new output minimum. </param>
        /// <param name="new_output_max"> The new output maximum. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>

        public bool SetOutputScalingParams(TrainingDataDouble data, float new_output_min, float new_output_max)
        {
            return net.set_output_scaling_params(data.InternalData, new_output_min, new_output_max);
        }

        /// <summary> Sets scaling parameters. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">           The data. </param>
        /// <param name="new_input_min">  The new input minimum. </param>
        /// <param name="new_input_max">  The new input maximum. </param>
        /// <param name="new_output_min"> The new output minimum. </param>
        /// <param name="new_output_max"> The new output maximum. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>

        public bool SetScalingParams(TrainingDataDouble data, float new_input_min, float new_input_max, float new_output_min, float new_output_max)
        {
            return net.set_scaling_params(data.InternalData, new_input_min, new_input_max, new_output_min, new_output_max);
        }

        /// <summary> Clears the scaling parameters. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>

        public bool ClearScalingParams()
        {
            return net.clear_scaling_params();
        }

        /// <summary> Scale input. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="input_vector"> The input vector. </param>

        public void ScaleInput(double[] input_vector)
        {
            using (doubleArray inputs = new doubleArray(input_vector.Length))
            {
                for (int i = 0; i < input_vector.Length; i++)
                {
                    inputs.setitem(i, input_vector[i]);
                }
               net.scale_input(inputs.cast());
            }
        }

        /// <summary> Scale output. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="output_vector"> The output vector. </param>

        public void ScaleOutput(double[] output_vector)
        {
            using (doubleArray inputs = new doubleArray(output_vector.Length))
            {
                for (int i = 0; i < output_vector.Length; i++)
                {
                    inputs.setitem(i, output_vector[i]);
                }
               net.scale_output(inputs.cast());
            }
        }

        /// <summary> Descale input. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="input_vector"> The input vector. </param>

        public void DescaleInput(double[] input_vector)
        {
            using (doubleArray inputs = new doubleArray(input_vector.Length))
            {
                for (int i = 0; i < input_vector.Length; i++)
                {
                    inputs.setitem(i, input_vector[i]);
                }
               net.descale_input(inputs.cast());
            }
        }

        /// <summary> Descale output. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="output_vector"> The output vector. </param>

        public void DescaleOutput(double[] output_vector)
        {
            using (doubleArray inputs = new doubleArray(output_vector.Length))
            {
                for (int i = 0; i < output_vector.Length; i++)
                {
                    inputs.setitem(i, output_vector[i]);
                }
               net.descale_output(inputs.cast());
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

        /// <summary> Train epoch batch parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">       The data. </param>
        /// <param name="threadnumb"> The threadnumb. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochBatchParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        /// <summary> Train epoch irpropm parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">       The data. </param>
        /// <param name="threadnumb"> The threadnumb. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochIrpropmParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        /// <summary> Train epoch quickprop parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">       The data. </param>
        /// <param name="threadnumb"> The threadnumb. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochQuickpropParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        /// <summary> Train epoch sarprop parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">       The data. </param>
        /// <param name="threadnumb"> The threadnumb. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochSarpropParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        /// <summary> Train epoch incremental modifier. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data"> The data. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochIncrementalMod(TrainingDataDouble data)
        {
            return fanndouble.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData());
        }

        /// <summary> Train epoch batch parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">              The data. </param>
        /// <param name="threadnumb">        The threadnumb. </param>
        /// <param name="predicted_outputs"> The predicted outputs. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochBatchParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }

                float result = fanndouble.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        /// <summary> Train epoch irpropm parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">              The data. </param>
        /// <param name="threadnumb">        The threadnumb. </param>
        /// <param name="predicted_outputs"> The predicted outputs. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochIrpropmParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        /// <summary> Train epoch quickprop parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">              The data. </param>
        /// <param name="threadnumb">        The threadnumb. </param>
        /// <param name="predicted_outputs"> The predicted outputs. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochQuickpropParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                
                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        /// <summary> Train epoch sarprop parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">              The data. </param>
        /// <param name="threadnumb">        The threadnumb. </param>
        /// <param name="predicted_outputs"> The predicted outputs. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochSarpropParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                 
                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        /// <summary> Train epoch incremental modifier. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">              The data. </param>
        /// <param name="predicted_outputs"> The predicted outputs. </param>
        ///
        /// <returns> A float. </returns>

        public float TrainEpochIncrementalMod(TrainingDataDouble data, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData(), predicted_out);
                
                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        /// <summary> Tests data parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">       The data. </param>
        /// <param name="threadnumb"> The threadnumb. </param>
        ///
        /// <returns> A float. </returns>

        public float TestDataParallel(TrainingDataDouble data, uint threadnumb)
        {
            return fanndouble.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        /// <summary> Tests data parallel. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="data">              The data. </param>
        /// <param name="threadnumb">        The threadnumb. </param>
        /// <param name="predicted_outputs"> The predicted outputs. </param>
        ///
        /// <returns> A float. </returns>

        public float TestDataParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (doubleVectorVector predicted_out = new doubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new doubleVector(predicted_outputs[i].Count);
                }
                float result = fanndouble.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                
                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<double> list = new List<double>();
                    for(int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        /// <summary> Callback, called when the set. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="callback"> The callback. </param>
        /// <param name="userData"> Information describing the user. </param>

        public void SetCallback(TrainingCallbackDouble callback, Object userData)
        {
            Callback = callback;
            UserData = userData;
            training_callback back = (net, data, max_epochs, epochs_between_reports, desired_error, epochs, user_data) =>
            {
                NeuralNetDouble callbackNet = new NeuralNetDouble(new neural_net(fanndoublePINVOKE.new_neural_net__SWIG_6(net), true));
                TrainingDataDouble callbackData = new TrainingDataDouble(new training_data(fanndoublePINVOKE.new_training_data__SWIG_1(data), true));
                return Callback(callbackNet, callbackData, max_epochs, epochs_between_reports, desired_error, epochs, user_data);
            };
            neural_net_set_callback(neural_net.getCPtr(this.net), back, UserData);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate int training_callback(global::System.Runtime.InteropServices.HandleRef net, global::System.Runtime.InteropServices.HandleRef data, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, Object user_data);
        [global::System.Runtime.InteropServices.DllImport("fanndouble", EntryPoint = "CSharp_neural_net_set_callback")]
        internal static extern void neural_net_set_callback(global::System.Runtime.InteropServices.HandleRef net, [MarshalAs(UnmanagedType.FunctionPtr)] training_callback callback, Object userData);

#region Properties
        public neural_net InternalDoubleNet
        {
            get
            {
                return net;
            }
        }
        private TrainingCallbackDouble Callback { get; set; }
        private Object UserData { get; set; }

        private uint Outputs { get; set; }
#endregion Properties
    }

    /// <summary> Training callback double. </summary>
    ///
    /// <remarks> Joel Self, 11/10/2015. </remarks>
    ///
    /// <param name="net">                  The net. </param>
    /// <param name="data">                 The data. </param>
    /// <param name="maxEpochs">            The maximum epochs. </param>
    /// <param name="epochsBetweenReports"> The epochs between reports. </param>
    /// <param name="desiredError">         The desired error. </param>
    /// <param name="epochs">               The epochs. </param>
    /// <param name="userData">             Information describing the user. </param>
    ///
    /// <returns> An int. </returns>

    public delegate int TrainingCallbackDouble(NeuralNetDouble net, TrainingDataDouble data, uint maxEpochs, uint epochsBetweenReports, float desiredError, uint epochs, Object userData);
}
