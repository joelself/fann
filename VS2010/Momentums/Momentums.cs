using System;
using System.Collections.Generic;
using System.IO;
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
    class Momentums
    {
        static void Main()
        {
            const uint num_layers = 3;
	        const uint num_neurons_hidden = 96;
	        const float desired_error = 0.001F;


            using (TrainingData trainData = new TrainingData("..\\..\\..\\datasets\\robot.train"))
            using (TrainingData testData = new TrainingData("..\\..\\..\\datasets\\robot.test"))
            {
                for (float momentum = 0.0F; momentum < 0.7F; momentum += 0.1F)
                {
                    Console.WriteLine("============= momentum = {0} =============\n", momentum);
                    using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, trainData.InputCount, num_neurons_hidden, trainData.OutputCount))
                    {                        
                        net.TrainingAlgorithm = TrainingAlgorithm.TRAIN_INCREMENTAL;

                        net.LearningMomentum = momentum;

                        net.TrainOnData(trainData, 20000, 5000, desired_error);

                        Console.WriteLine("MSE error on train data: {0}", net.TestData(trainData));
                        Console.WriteLine("MSE error on test data: {0}", net.TestData(testData));
                    }

                }
            }
            Console.ReadKey();
        }
    }
}
