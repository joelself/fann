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
    class Robot
    {
        static void Main()
        {
            const uint num_layers = 3;
            const uint num_neurons_hidden = 96;
            const float desired_error = 0.001F;

            Console.WriteLine("Creating network.");

            using (TrainingData data = new TrainingData())
            using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, data.InputCount, num_neurons_hidden, data.OutputCount))
            using (TrainingData testData = new TrainingData())
            {
                data.ReadTrainFromFile("..\\..\\datasets\\robot.train");

                Console.WriteLine("Training network.");

                net.TrainingAlgorithm = TrainingAlgorithm.TRAIN_INCREMENTAL;
                net.LearningMomentum = 0.4F;

                net.TrainOnData(data, 3000, 10, desired_error);

                Console.WriteLine("Testing network.");
                testData.ReadTrainFromFile("..\\..\\datasets\\robot.test");
                try
                {
                    net.ResetMSE();
                    for (int i = 0; i < testData.TrainDataLength; i++)
                    {
                        net.Test(testData.Input[i], testData.Output[i]);
                    }
                    Console.WriteLine("MSE error on test data: {0}", net.MSE);

                    Console.WriteLine("Saving network.");

                    net.Save("..\\..\\datasets\\robot_float.net");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e.Message);
                }
                Console.ReadKey();
            }

        }
        static float FannAbs(float value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }
}
