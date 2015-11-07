using System;
using FANNCSharp;
using FannWrapper;
#if FANN_DOUBLE
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
    class Robot
    {
        static void Main(string[] argv)
        {
	        const uint max_epochs = 1000;
	        uint num_threads = 1;
	        TrainingData data;
	        NeuralNet net;
	        long before;
	        float error;

            if (argv.Length == 2)
                num_threads = UInt32.Parse(argv[1]);
            using (data = new TrainingData())
            using (net = new NeuralNet())
            {
                data.ReadTrainFromFile("..\\..\\datasets\\mushroom.train");
                net.Create(3, data.NumInput(), 32, data.NumOutput());

                net.SetActivationFunctionHidden(activation_function_enum.SIGMOID_SYMMETRIC);
                net.SetActivationFunctionOutput(activation_function_enum.SIGMOID);

                before = Environment.TickCount;
                for (int i = 1; i <= max_epochs; i++)
                {
                    error = num_threads > 1 ? net.TrainEpochIrpropmParallel(data, num_threads) : net.TrainEpoch(data);
                    Console.WriteLine("Epochs     {0}. Current error: {1}", i.ToString("00000000"), error.ToString("0.0000000000"));
                }

                Console.WriteLine("ticks {0}", Environment.TickCount - before);
                Console.ReadKey();
            }
        }
    }
}
