using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FannWrap;
using System.IO;
using System.Text.RegularExpressions;
namespace FannWrapper
{
    public class NeuralNet : IDisposable
    {
        neural_net net = null;
        public NeuralNet()
        {
            net = new neural_net();
        }

        public NeuralNet(NeuralNet other)
        {
            net = new neural_net(other.InternalNet);
        }

        public NeuralNet(fann other)
        {
            net = new neural_net(other);
        }

        public void CopyFromFann(fann other)
        {
            net = new neural_net(other);
        }

        public void Dispose()
        {
            net.destroy();
        }

        public bool Create(uint numLayers, params uint[]args)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                SwigFann.uint_array_setitem(newLayers, i, args[i]);
            }
            Outputs = args[args.Length - 1];
            return net.create_standard_array(numLayers, newLayers);
        }

        public bool Create(uint[] layers)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(layers.Length);
            for(int i = 0; i < layers.Length; i++) {
                SwigFann.uint_array_setitem(newLayers, i, layers[i]);
            }
            Outputs = layers[layers.Length - 1];
            return net.create_standard_array((uint)layers.Length, newLayers);
        }

        public bool Create(float connectionRate, uint numLayers, params uint[] args)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                SwigFann.uint_array_setitem(newLayers, i, args[i]);
            }
            Outputs = args[args.Length - 1];
            return net.create_sparse_array(connectionRate, numLayers, newLayers);
        }

        public bool Create(float connectionRate, uint[] layers)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(layers.Length);
            for (int i = 0; i < layers.Length; i++)
            {
                SwigFann.uint_array_setitem(newLayers, i, layers[i]);
            }
            Outputs = layers[layers.Length - 1];
            return net.create_sparse_array(connectionRate, (uint)layers.Length, newLayers);
        }

        public bool CreateShortcut(uint numLayers, params uint[] args)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                SwigFann.uint_array_setitem(newLayers, i, args[i]);
            }
            Outputs = args[args.Length - 1];
            return net.create_shortcut_array(numLayers, newLayers);
        }

        public bool CreateShortcut(uint[] layers)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(layers.Length);
            for (int i = 0; i < layers.Length; i++)
            {
                SwigFann.uint_array_setitem(newLayers, i, layers[i]);
            }
            Outputs = layers[layers.Length - 1];
            return net.create_shortcut_array((uint)layers.Length, newLayers);
        }

        float[] Run(float[] input)
        {
            floatArray floats = new floatArray(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                floats.setitem(i, input[i]);
            }
            SWIGTYPE_p_float outputs = net.run(floats.cast());
            floats.Dispose();
            float[] result = new float[Outputs];
            for (int i = 0; i < Outputs; i++)
            {
                result[i] = SwigFann.float_array_getitem(outputs, i);
            }
            return result;
        }
        public void RandomizeWeights(float minWeight, float maxWeight)
        {
            net.randomize_weights(minWeight, maxWeight);
        }
        public void InitWeights(TrainingData data)
        {
            net.init_weights(data.InternalData);
        }

        public void PrintConnections()
        {
            net.print_connections();
        }

        public bool CreateFromFile(string file)
        {
            string[] lines = File.ReadAllLines(file);
            Regex regex = new Regex("^layer_sizes=([0-9]+(?: )?)*");
            foreach (string line in lines)
            {
                MatchCollection matches = regex.Matches(line);
                if (matches.Count > 0)
                {
                    Outputs = UInt32.Parse(matches[0].Groups[0].Value);
                    break;
                }
            }
            return net.create_from_file(file);
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
            floatArray floatsIn = new floatArray(input.Length);
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

        public float TrainEpoch(TrainingData data)
        {
            return net.train_epoch(data.InternalData);
        }

        public void TrainOnData(TrainingData data, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
            net.train_on_data(data.InternalData, maxEpochs, epochsBetweenReports, desiredError);
        }

        public void TrainOnFile(string filename, uint maxEpochs, uint epochsBetweenReports, float desiredError)
        {
            net.train_on_file(filename, maxEpochs, epochsBetweenReports, desiredError);
        }

        float[] Test(float[] input, float[] desiredOutput)
        {
            floatArray floatsIn = new floatArray(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                floatsIn.setitem(i, input[i]);
            }
            floatArray floatsOut = new floatArray(desiredOutput.Length);
            for (int i = 0; i < input.Length; i++)
            {
                floatsOut.setitem(i, input[i]);
            }
            floatArray result = floatArray.frompointer(net.test(floatsIn.cast(), floatsOut.cast()));
            float[] arrayResult = new float[Outputs];
            for (int i = 0; i < Outputs; i++)
            {
                arrayResult[i] = result.getitem(i);
            }
            return arrayResult;
        }

        public float TestData(TrainingData data)
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

#region Properties
        public neural_net InternalNet
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
