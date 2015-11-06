using System;
using FannWrapperFixed;
using FannWrapper;

namespace FANNCSharp
{
    public class NeuralNetFixed : IDisposable
    {
        neural_net net = null;
        public NeuralNetFixed()
        {
           net = new neural_net();
        }

        public NeuralNetFixed(NeuralNetFixed other)
        {
           net = new neural_net(other.InternalFloatNet);
        }

        public NeuralNetFixed(fann other)
        {
           net = new neural_net(other);
        }

        public void CopyFromFann(fann other)
        {
            net.copy_from_struct_fann(other);
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

        public bool Create(uint numLayers, params uint[]args)
        {
            using (uintArray newLayers = new uintArray((int)numLayers))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    newLayers.setitem(i, args[i]);
                }
                Outputs = args[args.Length - 1];
                return net.create_standard_array(numLayers, newLayers.cast());
            }
        }

        public bool Create(uint[] layers)
        {
            using (uintArray newLayers = new uintArray(layers.Length))
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    newLayers.setitem(i, layers[i]);
                }
                Outputs = layers[layers.Length - 1];
                return net.create_standard_array((uint)layers.Length, newLayers.cast());
            }
        }

        public bool Create(float connectionRate, uint numLayers, params uint[] args)
        {
            using (uintArray newLayers = new uintArray((int)numLayers))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    newLayers.setitem(i, args[i]);
                }
                Outputs = args[args.Length - 1];
                return net.create_sparse_array(connectionRate, numLayers, newLayers.cast());
            }
        }

        public bool Create(float connectionRate, uint[] layers)
        {
            using (uintArray newLayers = new uintArray(layers.Length))
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    newLayers.setitem(i, layers[i]);
                }
                Outputs = layers[layers.Length - 1];
                return net.create_sparse_array(connectionRate, (uint)layers.Length, newLayers.cast());
            }
        }

        public bool CreateShortcut(uint numLayers, params uint[] args)
        {
            using (uintArray newLayers = new uintArray((int)numLayers))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    newLayers.setitem(i, args[i]);
                }
                Outputs = args[args.Length - 1];
                return net.create_shortcut_array(numLayers, newLayers.cast());
            }
        }

        public bool CreateShortcut(uint[] layers)
        {
            using (uintArray newLayers = new uintArray(layers.Length))
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    newLayers.setitem(i, layers[i]);
                }
                Outputs = layers[layers.Length - 1];
                return net.create_shortcut_array((uint)layers.Length, newLayers.cast());
            }
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

        public bool CreateFromFile(string file)
        {
            bool result = net.create_from_file(file);
            Outputs = net.get_num_output();
            return result;
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

        public float GetMSE()
        {
            return net.get_MSE();
        }

        public void ResetMSE()
        {
           net.reset_MSE();
        }

        public void PrintParameters()
        {
           net.print_parameters();
        }

        public training_algorithm_enum GetTrainingAlgorithm()
        {
            return net.get_training_algorithm();
        }

        public void SetTrainingAlgorithm(training_algorithm_enum algorithm)
        {
           net.set_training_algorithm(algorithm);
        }

        public float GetLearningRate()
        {
            return net.get_learning_rate();
        }

        public void SetLearningRate(float rate)
        {
           net.set_learning_rate(rate);
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

        public void SetActivationFunctionHidden(activation_function_enum function)
        {
           net.set_activation_function_hidden(function);
        }

        public void SetActivationFunctionOutput(activation_function_enum function)
        {
           net.set_activation_function_output(function);
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

        public error_function_enum GetTrainErrorFunction()
        {
            return net.get_train_error_function();
        }

        public void SetTrainErrorFunction(error_function_enum function)
        {
           net.set_train_error_function(function);
        }

        public float GetQuickpropDecay()
        {
            return net.get_quickprop_decay();
        }

        public void SetQuickpropDecay(float decay)
        {
           net.set_quickprop_decay(decay);
        }

        public float GetQuickpropMu()
        {
            return net.get_quickprop_mu();
        }
        public void SetQuickpropMu(float quickprop_mu)
        {
           net.set_quickprop_mu(quickprop_mu);
        }
        public float GetRpropIncreaseFactor()
        {
            return net.get_rprop_increase_factor();
        }
        public void SetRpropIncreaseFactor(float rprop_increase_factor)
        {
           net.set_rprop_increase_factor(rprop_increase_factor);
        }
        public float GetRpropDecreaseFactor()
        {
            return net.get_rprop_decrease_factor();
        }
        public void SetRpropDecreaseFactor(float rprop_decrease_factor)
        {
           net.set_rprop_decrease_factor(rprop_decrease_factor);
        }
        public float GetRpropDeltaZero()
        {
            return net.get_rprop_delta_zero();
        }
        public void SetRpropDeltaZero(float rprop_delta_zero)
        {
           net.set_rprop_delta_zero(rprop_delta_zero);
        }
        public float GetRpropDeltaMin()
        {
            return net.get_rprop_delta_min();
        }
        public void SetRpropDeltaMin(float rprop_delta_min)
        {
           net.set_rprop_delta_min(rprop_delta_min);
        }
        public float GetRpropDeltaMax()
        {
            return net.get_rprop_delta_max();
        }
        public void SetRpropDeltaMax(float rprop_delta_max)
        {
           net.set_rprop_delta_max(rprop_delta_max);
        }
        public float GetSarpropWeightDecayShift()
        {
            return net.get_sarprop_weight_decay_shift();
        }
        public void SetSarpropWeightDecayShift(float sarprop_weight_decay_shift)
        {
           net.set_sarprop_weight_decay_shift(sarprop_weight_decay_shift);
        }
        public float GetSarpropStepErrorThresholdFactor()
        {
            return net.get_sarprop_step_error_threshold_factor();
        }
        public void SetSarpropStepErrorThresholdFactor(float sarprop_step_error_threshold_factor)
        {
           net.set_sarprop_step_error_threshold_factor(sarprop_step_error_threshold_factor);
        }
        public float GetSarpropStepErrorShift()
        {
            return net.get_sarprop_step_error_shift();
        }
        public void SetSarpropStepErrorShift(float sarprop_step_error_shift)
        {
           net.set_sarprop_step_error_shift(sarprop_step_error_shift);
        }
        public float GetSarpropTemperature()
        {
            return net.get_sarprop_temperature();
        }

        public void SetSarpropTemperature(float sarprop_temperature)
        {
           net.set_sarprop_temperature(sarprop_temperature);
        }
        public uint GetNumInput()
        {
            return net.get_num_input();
        }
        public uint GetNumOutput()
        {
            return net.get_num_output();
        }
        public uint GetTotalNeurons()
        {
            return net.get_total_neurons();
        }
        public uint GetTotalConnections()
        {
            return net.get_total_connections();
        }
        public network_type_enum GetNetworkType()
        {
            return net.get_network_type();
        }
        public float GetConnectionRate()
        {
            return net.get_connection_rate();
        }
        public uint GetNumLayers()
        {
            return net.get_num_layers();
        }
        public void GetLayerArray(out uint[] layers)
        {
            layers = new uint[net.get_num_layers()];
            using (uintArray array = new uintArray(layers.Length))
            {
               net.get_layer_array(array.cast());
                for (int i = 0; i < layers.Length; i++)
                {
                    layers[i] = array.getitem(i);
                }
            }
        }
        public void GetBiasArray(out uint[] bias)
        {
            bias = new uint[net.get_num_layers()];
            using (uintArray array = new uintArray(bias.Length))
            {
               net.get_layer_array(array.cast());
                for (int i = 0; i < bias.Length; i++)
                {
                    bias[i] = array.getitem(i);
                }
            }
        }
        public void GetConnectionArray(out fann_connection[] connections)
        {
            uint count = net.get_total_connections();
            connections = new fann_connection[count];
            using (connectionArray output = new connectionArray(connections.Length))
            {
               net.get_connection_array(output.cast());
                for (uint i = 0; i < count; i++)
                {
                    connections[i] = output.getitem((int)i);
                }
            }
        }
        public void SetWeightArray(fann_connection[] connections)
        {
            using (connectionArray input = new connectionArray(connections.Length))
            {
                for (int i = 0; i < connections.Length; i++)
                {
                    input.setitem(i, connections[i]);
                }
               net.set_weight_array(input.cast(), (uint)connections.Length);
            }
        }
        public void SetWeight(uint from_neuron, uint to_neuron, int weight)
        {
           net.set_weight(from_neuron, to_neuron, weight);
        }
        public float GetLearningMomentum()
        {
            return net.get_learning_momentum();
        }
        public void SetLearningMomentum(float learning_momentum)
        {
           net.set_learning_momentum(learning_momentum);
        }
        public stop_function_enum GetTrainStopFunction()
        {
            return net.get_train_stop_function();
        }
        public void SetTrainStopFunction(stop_function_enum train_stop_function)
        {
           net.set_train_stop_function(train_stop_function);
        }
        public int GetBitFailLimit()
        {
            return net.get_bit_fail_limit();
        }
        public void SetBitFailLimit(int bit_fail_limit)
        {
           net.set_bit_fail_limit(bit_fail_limit);
        }
        public uint GetBitFail()
        {
            return net.get_bit_fail();
        }

        public void SetErrorLog(FannFile log_file)
        {
           net.set_error_log(log_file.InternalFile);
        }
        public uint GetErrno()
        {
            return net.get_errno();
        }
        public void ResetErrno()
        {
           net.reset_errno();
        }
        public void ResetErrstr()
        {
           net.reset_errstr();
        }
        public string GetErrstr()
        {
            return net.get_errstr();
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
        public FannFile OpenFile(string filename, string mode)
        {
            SWIGTYPE_p_FILE file = SwigFannFixed.fopen(filename, mode);
            FannFile result = new FannFile(file);
            return result;
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
