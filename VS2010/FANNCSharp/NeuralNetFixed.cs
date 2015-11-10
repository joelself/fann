using System;
using FannWrapperFixed;
using FannWrapper;
using System.Collections.Generic;

namespace FANNCSharp
{
    public class NeuralNetFixed : IDisposable
    {
        neural_net net = null;

        public NeuralNetFixed(NeuralNetFixed other)
        {
           net = new neural_net(other.InternalFloatNet);
        }

        public uint GetDecimalPoint()
        {
            return net.get_decimal_point();
        }

        public uint GetMultiplier()
        {
            return net.get_multiplier();
        }


        public void Dispose()
        {
            net.destroy();
        }

        public NeuralNetFixed(network_type_enum netType, uint numLayers, params uint[] args)
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

        public NeuralNetFixed(network_type_enum netType, ICollection<uint> layers)
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

        public NeuralNetFixed(string filename)
        {
            net = new neural_net(filename);
        }

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

        public void RandomizeWeights(int minWeight, int maxWeight)
        {
            net.randomize_weights(minWeight, maxWeight);
        }
        public void InitWeights(TrainingDataFixed data)
        {
            net.init_weights(data.InternalData);
        }

        public void PrintConnections()
        {
            net.print_connections();
        }

        public bool Save(string file)
        {
            return net.save(file);
        }

        public int SaveToFixed(string file)
        {
            return net.save_to_fixed(file);
        }

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

        public float TestData(TrainingDataFixed data)
        {
            return net.test_data(data.InternalData);
        }

        public float MSE
        {
            get
            {
                return net.get_MSE();
            }
        }

        public void ResetMSE()
        {
            net.reset_MSE();
        }

        public void PrintParameters()
        {
            net.print_parameters();
        }

        public training_algorithm_enum TrainingAlgorithm
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
        public activation_function_enum GetActivationFunction(int layer, int neuron)
        {
            return net.get_activation_function(layer, neuron);
        }

        public void SetActivationFunction(activation_function_enum function, int layer, int neuron)
        {
            net.set_activation_function(function, layer, neuron);
        }

        public void SetActivationFunctionLayer(activation_function_enum function, int layer)
        {
            net.set_activation_function_layer(function, layer);
        }

        public activation_function_enum ActivationFunctionHidden
        {
            set
            {
                net.set_activation_function_hidden(value);
            }
        }

        public activation_function_enum ActivationFunctionOutput
        {
            set
            {
                net.set_activation_function_output(value);
            }
        }

        public int GetActivationSteepness(int layer, int neuron)
        {
            return net.get_activation_steepness(layer, neuron);
        }

        public void SetActivationSteepness(int steepness, int layer, int neuron)
        {
            net.set_activation_steepness(steepness, layer, neuron);
        }

        public void SetActivationSteepnessLayer(int steepness, int layer)
        {
            net.set_activation_steepness_layer(steepness, layer);
        }

        public void SetActivationSteepnessHidden(int steepness)
        {
            net.set_activation_steepness_hidden(steepness);
        }

        public void SetActivationSteepnessOutput(int steepness)
        {
            net.set_activation_steepness_output(steepness);
        }

        public error_function_enum TrainErrorFunction
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

        public uint InputCount
        {
            get
            {
                return net.get_num_input();
            }
        }
        public uint OutputCount
        {
            get
            {
                return net.get_num_output();
            }
        }
        public uint TotalNeurons
        {
            get
            {
                return net.get_total_neurons();
            }
        }
        public uint TotalConnections
        {
            get
            {
                return net.get_total_connections();
            }
        }
        public network_type_enum NetworkType
        {
            get
            {
                return net.get_network_type();
            }
        }
        public float ConnectionRate
        {
            get
            {
                return net.get_connection_rate();
            }
        }
        public uint LayerCount
        {
            get
            {
                return net.get_num_layers();
            }
        }
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
        public connection[] ConnectionArray
        {
            get
            {
                uint count = net.get_total_connections();
                connection[] connections = new connection[count];
                using (connectionArray output = new connectionArray(connections.Length))
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
        public connection[] WeightArray
        {
            set
            {
                using (connectionArray input = new connectionArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i]);
                    }
                    net.set_weight_array(input.cast(), (uint)value.Length);
                }
            }
        }
        public void SetWeight(uint from_neuron, uint to_neuron, int weight)
        {
            net.set_weight(from_neuron, to_neuron, weight);
        }
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
        public stop_function_enum TrainStopFunction
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
        public uint BitFail
        {
            get
            {
                return net.get_bit_fail();
            }
        }
        public void SetErrorLog(FannFile log_file)
        {
            net.set_error_log(log_file.InternalFile);
        }
        public uint ErrNo
        {
            get
            {
                return net.get_errno();
            }
        }
        public void ResetErrno()
        {
            net.reset_errno();
        }
        public void ResetErrstr()
        {
            net.reset_errstr();
        }
        public string ErrStr
        {
            get
            {
                return net.get_errstr();
            }
        }
        public void PrintError()
        {
            net.print_error();
        }
        public void DisableSeedRand()
        {
            net.disable_seed_rand();
        }
        public void EnableSeedRand()
        {
            net.enable_seed_rand();
        }

        #region Properties
        public neural_net InternalFloatNet
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
