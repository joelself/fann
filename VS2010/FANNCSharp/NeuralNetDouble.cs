using System;
using FannWrapperDouble;
using FannWrapper;
using System.Collections.Generic;

namespace FANNCSharp
{
    public class NeuralNetDouble : IDisposable
    {
        neural_net net = null;
        public NeuralNetDouble()
        {
           net = new neural_net();
        }

        public NeuralNetDouble(NeuralNetDouble other)
        {
           net = new neural_net(other.InternalFloatNet);
        }

        public NeuralNetDouble(fann other)
        {
            net = new neural_net(other);
        }

        public void CopyFromFann(fann other)
        {
            net.copy_from_struct_fann(other);
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

        public void RandomizeWeights(double minWeight, double maxWeight)
        {
           net.randomize_weights(minWeight, maxWeight);
        }
        public void InitWeights(TrainingDataDouble data)
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

        public float TrainEpoch(TrainingDataDouble data)
        {
            return net.train_epoch(data.InternalData);
        }

        public void TrainOnData(TrainingDataDouble data, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
           net.train_on_data(data.InternalData, maxEpochs, epochsBetweenReports, desiredError);
        }

        public void TrainOnFile(string filename, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
           net.train_on_file(filename, maxEpochs, epochsBetweenReports, desiredError);
        }

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

        public float TestData(TrainingDataDouble data)
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

        public double GetActivationSteepness(int layer, int neuron)
        {
            return net.get_activation_steepness(layer, neuron);
        }

        public void SetActivationSteepness(double steepness, int layer, int neuron)
        {
           net.set_activation_steepness(steepness, layer, neuron);
        }

        public void SetActivationSteepnessLayer(double steepness, int layer)
        {
           net.set_activation_steepness_layer(steepness, layer);
        }

        public void SetActivationSteepnessHidden(double steepness)
        {
           net.set_activation_steepness_hidden(steepness);
        }

        public void SetActivationSteepnessOutput(double steepness)
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
        public void SetWeight(uint from_neuron, uint to_neuron, double weight)
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
        public double GetBitFailLimit()
        {
            return net.get_bit_fail_limit();
        }
        public void SetBitFailLimit(double bit_fail_limit)
        {
           net.set_bit_fail_limit(bit_fail_limit);
        }
        public uint GetBitFail()
        {
            return net.get_bit_fail();
        }
        public void CascadetrainOnData(TrainingDataDouble data, uint max_neurons, uint neurons_between_reports, float desired_error)
        {
           net.cascadetrain_on_data(data.InternalData, max_neurons, neurons_between_reports, desired_error);
        }
        public void CascadetrainOnFile(string filename, uint max_neurons, uint neurons_between_reports, float desired_error)
        {
           net.cascadetrain_on_file(filename, max_neurons, neurons_between_reports, desired_error);
        }
        public float GetCascadeOutputChangeFraction()
        {
            return net.get_cascade_output_change_fraction();
        }
        public void SetCascadeOutputChangeFraction(float cascade_output_change_fraction)
        {
           net.set_cascade_output_change_fraction(cascade_output_change_fraction);
        }
        public uint GetCascadeOutputStagnationEpochs()
        {
            return net.get_cascade_output_stagnation_epochs();
        }
        public void SetCascadeOutputStagnationEpochs(uint cascade_output_stagnation_epochs)
        {
           net.set_cascade_output_stagnation_epochs(cascade_output_stagnation_epochs);
        }
        public float GetCascadeCandidateChangeFraction()
        {
            return net.get_cascade_candidate_change_fraction();
        }
        public void SetCascadeCandidateChangeFraction(float cascade_candidate_change_fraction)
        {
           net.set_cascade_output_change_fraction(cascade_candidate_change_fraction);
        }
        public uint GetCascadeCandidateStagnationEpochs()
        {
            return net.get_cascade_candidate_stagnation_epochs();
        }
        public void SetCascadeCandidateStagnationEpochs(uint cascade_candidate_stagnation_epochs)
        {
           net.set_cascade_candidate_stagnation_epochs(cascade_candidate_stagnation_epochs);
        }
        public double GetCascadeWeightMultiplier()
        {
            return net.get_cascade_weight_multiplier();
        }
        public void SetCascadeWeightMultiplier(double cascade_weight_multiplier)
        {
           net.set_cascade_weight_multiplier(cascade_weight_multiplier);
        }
        public double GetCascadeCandidateLimit()
        {
            return net.get_cascade_candidate_limit();
        }
        public void SetCascadeCandidateLimit(double cascade_candidate_limit)
        {
           net.set_cascade_candidate_limit(cascade_candidate_limit);
        }
        public uint GetCascadeMaxOutEpochs()
        {
            return net.get_cascade_max_out_epochs();
        }
        public void SetCascadeMaxOutEpochs(uint cascade_max_out_epochs)
        {
           net.set_cascade_max_out_epochs(cascade_max_out_epochs);
        }
        public uint GetCascadeMaxCandEpochs()
        {
            return net.get_cascade_max_cand_epochs();
        }
        public void SetCascadeMaxCandEpochs(uint cascade_max_cand_epochs)
        {
           net.set_cascade_max_cand_epochs(cascade_max_cand_epochs);
        }
        public uint GetCascadeNumCandidates()
        {
            return net.get_cascade_num_candidates();
        }
        public uint GetCascadeActivationFunctionsCount()
        {
            return net.get_cascade_activation_functions_count();
        }
        public activation_function_enum[] GetCascadeActivationFunctions()
        {
            int count = (int)net.get_cascade_activation_functions_count();
            using (activationFunctionEnumArray result = activationFunctionEnumArray.frompointer(net.get_cascade_activation_functions()))
            {
                activation_function_enum[] arrayResult = new activation_function_enum[net.get_cascade_activation_functions_count()];
                for (int i = 0; i < count; i++)
                {
                    arrayResult[i] = result.getitem(i);
                }
                return arrayResult;
            }
        }
        public void SetCascadeActivationFunctions(activation_function_enum[] cascade_activation_functions)
        {
            using (activationFunctionEnumArray input = new activationFunctionEnumArray(cascade_activation_functions.Length))
            {
                for (int i = 0; i < cascade_activation_functions.Length; i++)
                {
                    input.setitem(i, cascade_activation_functions[i]);
                }
               net.set_cascade_activation_functions(input.cast(), (uint)cascade_activation_functions.Length);
            }
        }
        public uint GetCascadeActivationSteepnessesCount()
        {
            return net.get_cascade_activation_steepnesses_count();
        }
        public double[] GetCascadeActivationSteepnesses()
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
        public void SetCascadeActivationSteepnesses(double[] cascade_activation_steepnesses)
        {
            using (doubleArray input = new doubleArray(cascade_activation_steepnesses.Length))
            {
                for (int i = 0; i < cascade_activation_steepnesses.Length; i++)
                {
                    input.setitem(i, cascade_activation_steepnesses[i]);
                }
               net.set_cascade_activation_steepnesses(input.cast(), (uint)cascade_activation_steepnesses.Length);
               for (int i = 0; i < cascade_activation_steepnesses.Length; i++)
               {
                   cascade_activation_steepnesses[i] = input.getitem(i);
               }
            }
        }
        public uint GetCascadeNumCandidateGroups()
        {
            return net.get_cascade_num_candidate_groups();
        }
        public void SetCascadeNumCandidateGroups(uint cascade_num_candidate_groups)
        {
           net.set_cascade_num_candidate_groups(cascade_num_candidate_groups);
        }
        public void ScaleTrain(TrainingDataDouble data)
        {
           net.scale_train(data.InternalData);
        }
        public void DescaleTrain(TrainingDataDouble data)
        {
           net.descale_train(data.InternalData);
        }
        public bool SetInputScalingParams(TrainingDataDouble data, float new_input_min, float new_input_max)
        {
            return net.set_input_scaling_params(data.InternalData, new_input_min, new_input_max);
        }
        public bool SetOutputScalingParams(TrainingDataDouble data, float new_output_min, float new_output_max)
        {
            return net.set_output_scaling_params(data.InternalData, new_output_min, new_output_max);
        }
        public bool SetScalingParams(TrainingDataDouble data, float new_input_min, float new_input_max, float new_output_min, float new_output_max)
        {
            return net.set_scaling_params(data.InternalData, new_input_min, new_input_max, new_output_min, new_output_max);
        }
        public bool ClearScalingParams()
        {
            return net.clear_scaling_params();
        }
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
            SWIGTYPE_p_FILE file = SwigFannDouble.fopen(filename, mode);
            FannFile result = new FannFile(file);
            return result;
        }
        public float TrainEpochBatchParallel(TrainingDataDouble data, uint threadnumb)
        {
            return SwigFannDouble.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochIrpropmParallel(TrainingDataDouble data, uint threadnumb)
        {
            return SwigFannDouble.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochQuickpropParallel(TrainingDataDouble data, uint threadnumb)
        {
            return SwigFannDouble.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochSarpropParallel(TrainingDataDouble data, uint threadnumb)
        {
            return SwigFannDouble.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochIncrementalMod(TrainingDataDouble data)
        {
            return SwigFannDouble.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData());
        }

        public float TrainEpochBatchParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (DoubleVectorVector predicted_out = new DoubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new DoubleVector(predicted_outputs[i].Count);
                }

                float result = SwigFannDouble.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

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

        public float TrainEpochIrpropmParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (DoubleVectorVector predicted_out = new DoubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new DoubleVector(predicted_outputs[i].Count);
                }
                float result = SwigFannDouble.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

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

        public float TrainEpochQuickpropParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (DoubleVectorVector predicted_out = new DoubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new DoubleVector(predicted_outputs[i].Count);
                }
                float result = SwigFannDouble.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                
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

        public float TrainEpochSarpropParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (DoubleVectorVector predicted_out = new DoubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new DoubleVector(predicted_outputs[i].Count);
                }
                float result = SwigFannDouble.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                 
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

        public float TrainEpochIncrementalMod(TrainingDataDouble data, List<List<double>> predicted_outputs)
        {
            using (DoubleVectorVector predicted_out = new DoubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new DoubleVector(predicted_outputs[i].Count);
                }
                float result = SwigFannDouble.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData(), predicted_out);
                
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

        public float TestDataParallel(TrainingDataDouble data, uint threadnumb)
        {
            return SwigFannDouble.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TestDataParallel(TrainingDataDouble data, uint threadnumb, List<List<double>> predicted_outputs)
        {
            using (DoubleVectorVector predicted_out = new DoubleVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new DoubleVector(predicted_outputs[i].Count);
                }
                float result = SwigFannDouble.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);
                
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
