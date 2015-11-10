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
namespace Example
{
    class ScalingTrain
    {
        static void Main()
        {
            const uint num_input = 3;
            const uint num_output = 1;
            const uint num_layers = 4;
            const uint num_neurons_hidden = 5;
            const float desired_error = 0.0001F;
            const uint max_epochs = 5000;
            const uint epochs_between_reports = 1000;
            using(NeuralNet net = new NeuralNet(network_type_enum.LAYER, num_layers, num_input, num_neurons_hidden, num_neurons_hidden, num_output))
            {
                net.ActivationFunctionHidden = activation_function_enum.SIGMOID_SYMMETRIC;
                net.ActivationFunctionOutput = activation_function_enum.LINEAR;
                net.TrainingAlgorithm = training_algorithm_enum.TRAIN_RPROP;
                using(TrainingData data = new TrainingData())
                {
                    data.ReadTrainFromFile("..\\..\\datasets\\scaling.data");
                    net.SetScalingParams(data, -1, 1, -1, 1);
                    net.ScaleTrain(data);

                    net.TrainOnData(data, max_epochs, epochs_between_reports, desired_error);
                    net.Save("..\\..\\datasets\\scaling.net");

                    Console.ReadKey();
                }
            }
        }
        
    }
}
