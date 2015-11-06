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
    class SimpleTrain
    {
        static void Main()
        {
            const uint num_input = 2;
            const uint num_output = 1;
            const uint num_layers = 3;
            const uint num_neurons_hidden = 3;
            const float desired_error = 0.001F;
            const uint max_epochs = 500000;
            const uint epochs_between_reports = 1000;

            using (NeuralNet net = new NeuralNet())
            {
                net.Create(num_layers, num_input, num_neurons_hidden, num_output);

                net.SetActivationFunctionHidden(activation_function_enum.SIGMOID_SYMMETRIC);
                net.SetActivationFunctionOutput(activation_function_enum.SIGMOID_SYMMETRIC);

                net.TrainOnFile("..\\..\\examples\\xor.data", max_epochs, epochs_between_reports, desired_error);

                net.Save("..\\..\\examples\\xor_float.net");

                Console.ReadKey();
            }
        }
    }
}
