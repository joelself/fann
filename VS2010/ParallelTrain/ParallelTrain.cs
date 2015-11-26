using System;
using FANNCSharp;
#if FANN_FIXED
using FANNCSharp.Fixed;
using DataType = System.Int32;
#elif FANN_DOUBLE
using FANNCSharp.Double;
using DataType = System.Double;
#else
using FANNCSharp.Float;
using DataType = System.Single;
#endif
namespace Example
{
    class ParallelTrain
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
            using (data = new TrainingData("..\\..\\..\\datasets\\mushroom.train"))
            using (net = new NeuralNet(NetworkType.LAYER, 3, data.InputCount, 32, data.OutputCount))
            {
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
