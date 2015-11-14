using System;
using FANNCSharp;
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
            using (net = new NeuralNet(NetworkType.LAYER, 3, data.InputCount, 32, data.OutputCount))
            {
                data.ReadTrainFromFile("..\\..\\datasets\\mushroom.train");

                net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
                net.ActivationFunctionOutput = ActivationFunction.SIGMOID;

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
