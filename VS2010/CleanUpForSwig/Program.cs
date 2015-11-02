using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;

namespace CleanUpForSwig
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("You must specify a swig generated file and generated file path to fix-up");
                Console.Error.WriteLine("Usage: CleanUpForSwig [FILEPATH] [PATH_TO_GENERATED_FILES]");
                return -1;
            }
            Console.WriteLine("Arguments: " + args[0] + ", " + args[1]);
            if (fixCpp(args[0]) < 0)
                return -1;
            if (fixCSharp(args[1]) < 0)
                return -1;
            return 0;
        }

        static int fixCSharp(string folder)
        {
            Console.WriteLine("Fixing text in " + folder + "training_algorithm_enum.cs");
            string text = File.ReadAllText(folder + "training_algorithm_enum.cs");
            text = text.Replace("FANN_TRAIN_INCREMENTAL", "fann_train_enum.FANN_TRAIN_INCREMENTAL");
            File.WriteAllText(folder + "training_algorithm_enum.cs", text);

            Console.WriteLine("Fixing text in " + folder + "stop_function_enum.cs");
            text = File.ReadAllText(folder + "stop_function_enum.cs");
            text = text.Replace("FANN_STOPFUNC_MSE", "fann_stopfunc_enum.FANN_STOPFUNC_MSE");
            File.WriteAllText(folder + "stop_function_enum.cs", text);

            Console.WriteLine("Fixing text in " + folder + "error_function_enum.cs");
            text = File.ReadAllText(folder + "error_function_enum.cs");
            text = text.Replace("FANN_ERRORFUNC_LINEAR", "fann_errorfunc_enum.FANN_ERRORFUNC_LINEAR");
            File.WriteAllText(folder + "error_function_enum.cs", text);

            Console.WriteLine("Fixing text in " + folder + "network_type_enum.cs");
            text = File.ReadAllText(folder + "network_type_enum.cs");
            text = text.Replace("FANN_NETTYPE_LAYER", "fann_nettype_enum.FANN_NETTYPE_LAYER");
            File.WriteAllText(folder + "network_type_enum.cs", text);

            Console.WriteLine("Fixing text in " + folder + "activation_function_enum.cs");
            text = File.ReadAllText(folder + "activation_function_enum.cs");
            text = text.Replace("FANN_LINEAR", "fann_activationfunc_enum.FANN_LINEAR");
            File.WriteAllText(folder + "activation_function_enum.cs", text);

            Console.WriteLine("Fixing text in " + folder + "FannWrapperPINVOKE.cs");
            text = File.ReadAllText(folder + "FannWrapperPINVOKE.cs");
            text = text.Replace("\"FannWrapper\"", "\"SwigFann\"");
            File.WriteAllText(folder + "FannWrapperPINVOKE.cs", text);
            return 0;
        }

        static int fixCpp(string path)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine("CPP File does not exist: " + path);
                return -1;
            }
            string text = File.ReadAllText(path);
            Console.WriteLine("Fixing code in " + path);
            text = text.Replace("fann_error ", "struct fann_error ").Replace(" fann_error()", " struct fann_error()");
            text = text.Replace("void (*arg5)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *) = (void (*)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *)) 0 ;",
                                "void (__stdcall *arg5)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *) = (void (__stdcall *)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *)) 0 ;");
            text = text.Replace("arg5 = (void (*)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *))jarg5;",
                                "arg5 = (void (__stdcall *)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *))jarg5;");
            File.Delete(path);
            File.WriteAllText(path, text);

            string dir = Path.GetDirectoryName(path);
            DirectoryInfo dirInfo = Directory.GetParent(dir);
            Project project = new Microsoft.Build.Evaluation.Project(dirInfo.FullName + "\\SwigFann.vcxproj");
            project.AddItem("Folder", dirInfo.FullName + "\\src\\");
            project.AddItem("Compile", path);
            project.Save();
            return 0;
        }
    }
}
