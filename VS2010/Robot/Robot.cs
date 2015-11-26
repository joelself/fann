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
    class Robot
    {
        static void Main()
        {
            const uint num_layers = 3;
            const uint num_neurons_hidden = 96;
            const float desired_error = 0.001F;

            Console.WriteLine("Creating network.");

            using (TrainingData data = new TrainingData("..\\..\\..\\datasets\\robot.train"))
            using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, data.InputCount, num_neurons_hidden, data.OutputCount))
            using (TrainingData testData = new TrainingData())
            {
                Console.WriteLine("Training network.");

                net.TrainingAlgorithm = TrainingAlgorithm.TRAIN_INCREMENTAL;
                net.LearningMomentum = 0.4F;

                net.TrainOnData(data, 3000, 10, desired_error);

                Console.WriteLine("Testing network.");
                testData.ReadTrainFromFile("..\\..\\..\\datasets\\robot.test");
                try
                {
                    net.ResetMSE();
                    for (int i = 0; i < testData.TrainDataLength; i++)
                    {
                        net.Test(testData.GetTrainInput((uint)i).Array, testData.GetTrainOutput((uint)i).Array);
                    }
                    Console.WriteLine("MSE error on test data: {0}", net.MSE);

                    Console.WriteLine("Saving network.");

                    net.Save("..\\..\\..\\datasets\\robot_float.net");
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
