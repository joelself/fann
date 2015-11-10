using System;
using FANNCSharp;
using FannWrapper;
#if FANN_FIXED
using NeuralNet = FANNCSharp.NeuralNetFixed;
using TrainingData = FANNCSharp.TrainingDataFixed;
using DataType = System.Int32;
#elif FANN_DOUBLE
using NeuralNet = FANNCSharp.NeuralNetDouble;
using TrainingData = FANNCSharp.TrainingDataDouble;
using DataType = System.Double;
#else
using NeuralNet = FANNCSharp.NeuralNetFloat;
using TrainingData = FANNCSharp.TrainingDataFloat;
using DataType = System.Single;
#endif
namespace XorTrain
{
    class XorTrain
    {
        static void Main(string[] args)
        {
            DataType[] calc_out;
            const uint num_input = 2;
            const uint num_output = 1;
            const uint num_layers = 3;
            const uint num_neurons_hidden = 3;
            const float desired_error =  0;
            const uint max_epochs = 1000;
            const uint epochs_between_reports = 10;

            int decimal_point;

            Console.WriteLine("Creating network.");
            using (NeuralNet net = new NeuralNet(network_type_enum.LAYER, num_layers, num_input, num_neurons_hidden, num_output))
            {
                using (TrainingData data = new TrainingData())
                {
                    data.ReadTrainFromFile("..\\..\\examples\\xor.data");

                    net.ActivationFunctionHidden = activation_function_enum.SIGMOID_SYMMETRIC;
                    net.ActivationFunctionOutput = activation_function_enum.SIGMOID_SYMMETRIC;

                    net.TrainStopFunction = stop_function_enum.STOPFUNC_BIT;
                    net.BitFailLimit = 0.01F;

                    net.TrainingAlgorithm = training_algorithm_enum.TRAIN_RPROP;

                    net.InitWeights(data);

                    Console.WriteLine("Training network.");
                    net.TrainOnData(data, max_epochs, epochs_between_reports, desired_error);

                    Console.WriteLine("Testing network");

                    for (int i = 0; i < data.TrainDataLength; i++)
                    {
                        calc_out = net.Run(data.Input[i]);
                        Console.WriteLine("XOR test ({0},{1}) -> {2}, should be {3}, difference={4}",
                                            data.Input[i][0], data.Input[i][1], calc_out[0], data.Output[i][0],
                                            FannAbs(calc_out[0] - data.Output[i][0]));
                    }

                    Console.WriteLine("Saving network.\n");

                    net.Save("..\\..\\examples\\xor_float.net");

                    decimal_point = net.SaveToFixed("..\\..\\examples\\xor_fixed.net");
                    data.SaveTrainToFixed("..\\..\\examples\\xor_fixed.data", (uint)decimal_point);

                    Console.ReadKey();
                }
            }
        }

        static float FannAbs(float value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }
}
