using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FannWrap;

namespace CSharpXorTest
{
    class XorTest
    {
        static int Main(string[] args)
        {
            neural_net net = new neural_net();
            if (!net.create_from_file("..\\..\\examples\\xor_float.net"))
            {
                Console.WriteLine("Error creating ann --- ABORTING.\n");
                return -1;
            }

            net.print_connections();
            net.print_parameters();

            Console.WriteLine("Testing network.");

            training_data data = new training_data();
            if(!data.read_train_from_file("..\\..\\examples\\xor.data"))
            {
                Console.WriteLine("Error reading training data --- ABORTING.\n");
                return -1;
            }
            SWIGTYPE_p_p_float inputs = data.get_input();
            SWIGTYPE_p_p_float outputs = data.get_output();
            for (int i = 0; i < data.length_train_data(); i++)
            {
                net.reset_MSE();
                SWIGTYPE_p_float calc_out = net.test(FannWrapper.float_p_array_getitem(inputs, i), FannWrapper.float_p_array_getitem(inputs, i));

                Console.WriteLine("XOR test ({0}, {1}) -> {2}, should be {3}, difference={4}",
                    FannWrapper.float_array_getitem(FannWrapper.float_p_array_getitem(inputs, i), 0),
                    FannWrapper.float_array_getitem(FannWrapper.float_p_array_getitem(inputs, i), 1),
                    FannWrapper.float_array_getitem(calc_out, 0), 
                    FannWrapper.float_array_getitem(FannWrapper.float_p_array_getitem(outputs, i), 0),
                    FannWrapper.float_array_getitem(calc_out, 0) - FannWrapper.float_array_getitem(FannWrapper.float_p_array_getitem(outputs, i), 0));
            }

            Console.WriteLine("Cleaning up.");

            data.Dispose();
            net.Dispose();
            Console.ReadKey();
            return 0;
        }
        static float fann_abs(float value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }

}
