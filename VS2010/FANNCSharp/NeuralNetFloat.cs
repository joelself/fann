using System;
using FannWrapperFloat;
using FannWrapper;
using System.Collections.Generic;

namespace FANNCSharp
{
    public class NeuralNetFloat : IDisposable
    {
        neural_net net = null;

        public NeuralNetFloat(NeuralNetFloat other)
        {
            net = new neural_net(other.InternalFloatNet);
        }

        public void Dispose()
        {
            net.destroy();
        }

        public NeuralNetFloat(network_type_enum netType, uint numLayers, params uint[] args)
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

        public NeuralNetFloat(network_type_enum netType, ICollection<uint> layers)
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

        public NeuralNetFloat(float connectionRate, uint numLayers, params uint[] args)
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

        public NeuralNetFloat(float connectionRate, ICollection<uint> layers)
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
        public NeuralNetFloat(string filename)
        {
            net = new neural_net(filename);
        }

        public float[] Run(float[] input)
        {
            using (floatArray floats = new floatArray(input.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    floats.setitem(i, input[i]);
                }
                using (floatArray outputs = floatArray.frompointer(net.run(floats.cast())))
                {
                    float[] result = new float[Outputs];
                    for (int i = 0; i < Outputs; i++)
                    {
                        result[i] = outputs.getitem(i);
                    }
                    return result;
                }
            }
        }

        public void RandomizeWeights(float minWeight, float maxWeight)
        {
            net.randomize_weights(minWeight, maxWeight);
        }
        public void InitWeights(TrainingDataFloat data)
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

        public void Train(float[] input, float[] desiredOutput)
        {
            using (floatArray floatsIn = new floatArray(input.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    floatsIn.setitem(i, input[i]);
                }
                floatArray floatsOut = new floatArray(desiredOutput.Length);
                for (int i = 0; i < input.Length; i++)
                {
                    floatsOut.setitem(i, input[i]);
                }
                net.train(floatsIn.cast(), floatsOut.cast());
            }
        }

        public float TrainEpoch(TrainingDataFloat data)
        {
            return net.train_epoch(data.InternalData);
        }

        public void TrainOnData(TrainingDataFloat data, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
            net.train_on_data(data.InternalData, maxEpochs, epochsBetweenReports, desiredError);
        }

        public void TrainOnFile(string filename, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
            net.train_on_file(filename, maxEpochs, epochsBetweenReports, desiredError);
        }

        public float[] Test(float[] input, float[] desiredOutput)
        {
            using (floatArray floatsIn = new floatArray(input.Length))
            using (floatArray floatsOut = new floatArray(desiredOutput.Length))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    floatsIn.setitem(i, input[i]);
                }
                for (int i = 0; i < desiredOutput.Length; i++)
                {
                    floatsOut.setitem(i, desiredOutput[i]);
                }
                floatArray result = floatArray.frompointer(net.test(floatsIn.cast(), floatsOut.cast()));
                float[] arrayResult = new float[Outputs];
                for (int i = 0; i < Outputs; i++)
                {
                    arrayResult[i] = result.getitem(i);
                }
                return arrayResult;
            }
        }

        public float TestData(TrainingDataFloat data)
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

        public float GetActivationSteepness(int layer, int neuron)
        {
            return net.get_activation_steepness(layer, neuron);
        }

        public void SetActivationSteepness(float steepness, int layer, int neuron)
        {
            net.set_activation_steepness(steepness, layer, neuron);
        }

        public void SetActivationSteepnessLayer(float steepness, int layer)
        {
            net.set_activation_steepness_layer(steepness, layer);
        }

        public void SetActivationSteepnessHidden(float steepness)
        {
            net.set_activation_steepness_hidden(steepness);
        }

        public void SetActivationSteepnessOutput(float steepness)
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
        public void SetWeight(uint from_neuron, uint to_neuron, float weight)
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
        public uint BitFail
        {
            get
            {
                return net.get_bit_fail();
            }
        }
        public void CascadetrainOnData(TrainingDataFloat data, uint max_neurons, uint neurons_between_reports, float desired_error)
        {
            net.cascadetrain_on_data(data.InternalData, max_neurons, neurons_between_reports, desired_error);
        }
        public void CascadetrainOnFile(string filename, uint max_neurons, uint neurons_between_reports, float desired_error)
        {
            net.cascadetrain_on_file(filename, max_neurons, neurons_between_reports, desired_error);
        }
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
        public uint CascadeCandidatesCount
        {
            get
            {
                return net.get_cascade_num_candidates();
            }
        }
        public uint CascadeActivationFunctionsCount
        {
            get
            {
                return net.get_cascade_activation_functions_count();
            }
        }
        public activation_function_enum[] CascadeActivationFunctions
        {
            get
            {
                int count = (int)net.get_cascade_activation_functions_count();
                using (activationFunctionArray result = activationFunctionArray.frompointer(net.get_cascade_activation_functions()))
                {
                    activation_function_enum[] arrayResult = new activation_function_enum[net.get_cascade_activation_functions_count()];
                    for (int i = 0; i < count; i++)
                    {
                        arrayResult[i] = result.getitem(i);
                    }
                    return arrayResult;
                }
            }
            set
            {
                using (activationFunctionArray input = new activationFunctionArray(value.Length))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        input.setitem(i, value[i]);
                    }
                    net.set_cascade_activation_functions(input.cast(), (uint)value.Length);
                }
            }
        }
        public uint CascadeActivationSteepnessesCount
        {
            get
            {
                return net.get_cascade_activation_steepnesses_count();
            }
        }
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
                using (floatArray input = new floatArray(value.Length))
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
        public void ScaleTrain(TrainingDataFloat data)
        {
            net.scale_train(data.InternalData);
        }
        public void DescaleTrain(TrainingDataFloat data)
        {
            net.descale_train(data.InternalData);
        }
        public bool SetInputScalingParams(TrainingDataFloat data, float new_input_min, float new_input_max)
        {
            return net.set_input_scaling_params(data.InternalData, new_input_min, new_input_max);
        }
        public bool SetOutputScalingParams(TrainingDataFloat data, float new_output_min, float new_output_max)
        {
            return net.set_output_scaling_params(data.InternalData, new_output_min, new_output_max);
        }
        public bool SetScalingParams(TrainingDataFloat data, float new_input_min, float new_input_max, float new_output_min, float new_output_max)
        {
            return net.set_scaling_params(data.InternalData, new_input_min, new_input_max, new_output_min, new_output_max);
        }
        public bool ClearScalingParams()
        {
            return net.clear_scaling_params();
        }
        public void ScaleInput(float[] input_vector)
        {
            using (floatArray inputs = new floatArray(input_vector.Length))
            {
                for (int i = 0; i < input_vector.Length; i++)
                {
                    inputs.setitem(i, input_vector[i]);
                }
                net.scale_input(inputs.cast());
            }
        }
        public void ScaleOutput(float[] output_vector)
        {
            using (floatArray inputs = new floatArray(output_vector.Length))
            {
                for (int i = 0; i < output_vector.Length; i++)
                {
                    inputs.setitem(i, output_vector[i]);
                }
                net.scale_output(inputs.cast());
            }
        }
        public void DescaleInput(float[] input_vector)
        {
            using (floatArray inputs = new floatArray(input_vector.Length))
            {
                for (int i = 0; i < input_vector.Length; i++)
                {
                    inputs.setitem(i, input_vector[i]);
                }
                net.descale_input(inputs.cast());
            }
        }
        public void DescaleOutput(float[] output_vector)
        {
            using (floatArray inputs = new floatArray(output_vector.Length))
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

        public float TrainEpochBatchParallel(TrainingDataFloat data, uint threadnumb)
        {
            return fannfloat.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochIrpropmParallel(TrainingDataFloat data, uint threadnumb)
        {
            return fannfloat.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochQuickpropParallel(TrainingDataFloat data, uint threadnumb)
        {
            return fannfloat.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochSarpropParallel(TrainingDataFloat data, uint threadnumb)
        {
            return fannfloat.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TrainEpochIncrementalMod(TrainingDataFloat data)
        {
            return fannfloat.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData());
        }

        public float TrainEpochBatchParallel(TrainingDataFloat data, uint threadnumb, List<List<float>> predicted_outputs)
        {
            using (FloatVectorVector predicted_out = new FloatVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new FloatVector(predicted_outputs[i].Count);
                }

                float result = fannfloat.train_epoch_batch_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TrainEpochIrpropmParallel(TrainingDataFloat data, uint threadnumb, List<List<float>> predicted_outputs)
        {
            using (FloatVectorVector predicted_out = new FloatVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new FloatVector(predicted_outputs[i].Count);
                }
                float result = fannfloat.train_epoch_irpropm_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TrainEpochQuickpropParallel(TrainingDataFloat data, uint threadnumb, List<List<float>> predicted_outputs)
        {
            using (FloatVectorVector predicted_out = new FloatVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new FloatVector(predicted_outputs[i].Count);
                }
                float result = fannfloat.train_epoch_quickprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TrainEpochSarpropParallel(TrainingDataFloat data, uint threadnumb, List<List<float>> predicted_outputs)
        {
            using (FloatVectorVector predicted_out = new FloatVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new FloatVector(predicted_outputs[i].Count);
                }
                float result = fannfloat.train_epoch_sarprop_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TrainEpochIncrementalMod(TrainingDataFloat data, List<List<float>> predicted_outputs)
        {
            using (FloatVectorVector predicted_out = new FloatVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new FloatVector(predicted_outputs[i].Count);
                }
                float result = fannfloat.train_epoch_incremental_mod(net.to_fann(), data.ToFannTrainData(), predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
                    {
                        list.Add(predicted_out[i][j]);
                    }
                    predicted_outputs.Add(list);
                }
                return result;
            }
        }

        public float TestDataParallel(TrainingDataFloat data, uint threadnumb)
        {
            return fannfloat.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb);
        }

        public float TestDataParallel(TrainingDataFloat data, uint threadnumb, List<List<float>> predicted_outputs)
        {
            using (FloatVectorVector predicted_out = new FloatVectorVector(predicted_outputs.Count))
            {
                for (int i = 0; i < predicted_outputs.Count; i++)
                {
                    predicted_out[i] = new FloatVector(predicted_outputs[i].Count);
                }
                float result = fannfloat.test_data_parallel(net.to_fann(), data.ToFannTrainData(), threadnumb, predicted_out);

                predicted_outputs.Clear();
                for (int i = 0; i < predicted_out.Count; i++)
                {
                    List<float> list = new List<float>();
                    for (int j = 0; j < predicted_out[i].Count; j++)
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
