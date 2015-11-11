using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using System.Threading;

namespace CleanUpForSwig
{
    class Program
    {
        private static string [] filesToCopy = { "ActivationFunction.cs",
                                                 "ErrorFunction.cs",
                                                 "NetworkType.cs",
                                                 "StopFunction.cs",
                                                 "TrainingAlgorithm.cs",
                                                 "Connection.cs",
                                                 "ConnectionArray.cs",
                                                 "ActivationFunctionArray.cs",
                                                 "uintArray.cs",
                                                 "SWIGTYPE_p_FANN__activation_function_enum.cs",
                                                 "SWIGTYPE_p_FILE.cs",
                                                 "SWIGTYPE_p_unsigned_int.cs",
                                                 "SWIGTYPE_p_fann_train_data.cs",
                                                 "SWIGTYPE_p_fann.cs"};
        static int Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.Error.WriteLine("You must specify a swig generated file and generated file path to fix-up");
                Console.Error.WriteLine("Usage: CleanUpForSwig [FILEPATH] [PATH_TO_GENERATED_FILES] [delete|copy] [float|double|int]");
                return -1;
            }
            Console.WriteLine("Arguments: " + args[0] + ", " + args[1]);
            if (fixCpp(args[0]) < 0)
                return -1;
            //if (fixCSharp(args[1], args[3]) < 0)
            //    return -1;
            if (args[2] == "copy" && copyFiles(args[1]) < 0)
                return -1;
            if (deleteFiles(args[1]) < 0)
                return -1;
            if (addNamespace(args[1]) < 0)
                return -1;
            if (args[2] == "copy" && (addNamespaceFile(args[1], "uintArray.cs") < 0 ||
                addNamespaceFile(args[1], "activationFunctionArray.cs") < 0 ||
                addNamespaceFile(args[1], "Connection.cs") < 0 ||
                addNamespaceFile(args[1], "ConnectionArray.cs") < 0))
                return -1;
            //if (addInheritance(args[1], args[3]) < 0)
            //    return -1;
            return 0;
        }

        static int addNamespaceFile(string folder, string file)
        {
            DirectoryInfo info = new DirectoryInfo(folder).Parent;
            string text = File.ReadAllText(info.FullName + "\\FannWrapper\\" + file);
            Match match = Regex.Match(text, "^.*?namespace.*?FannWrap[a-zA-z0-9]*.*?{.*$", RegexOptions.Multiline);
            text = text.Insert(match.Index, "using FannWrapperFloat;\n");
            File.WriteAllText(info.FullName + "\\FannWrapper\\" + file, text);
            return 0;
        }

        static int addNamespace(string folder)
        {
            DirectoryInfo info = new DirectoryInfo(folder);
            foreach(FileInfo file in info.EnumerateFiles("*.cs"))
            {
                string text = File.ReadAllText(file.FullName);
                Match match = Regex.Match(text, "^.*?namespace.*?FannWrap[a-zA-z0-9]*.*?{.*$", RegexOptions.Multiline);
                text = text.Insert(match.Index, "using FannWrapper;\n");
                try
                {
                    File.WriteAllText(file.FullName, text);
                } catch(System.IO.IOException)
                {
                    Thread.Sleep(500);
                    File.WriteAllText(file.FullName, text);
                }
            }
            return 0;
        }

        static int deleteFiles(string folder)
        {
            foreach (string file in filesToCopy)
            {
                File.Delete(folder + file);
            }
            return 0;
        }

        static int copyFiles(string fromFolder)
        {
            DirectoryInfo info = Directory.GetParent(fromFolder).Parent;
            Regex regex = new Regex("(.*?namespace.*?FannWrapper)([a-zA-z0-9]*)(.*?{.*)");
            string toFolder = info.FullName + "\\FannWrapper\\";
            string[] lines = null;
            foreach (string file in filesToCopy)
            {
                Console.WriteLine("Copying \"" + fromFolder + file + "\" to \"" + toFolder + "\"");
                lines = File.ReadAllLines(fromFolder + file);
                for(int i = 0; i < lines.Length; i++)
                {
                    MatchCollection matches = regex.Matches(lines[i]);
                    if(matches.Count > 0)
                    {
                        lines[i] = matches[0].Groups[1].Value + matches[0].Groups[3].Value;
                        break;
                    }
                }
                File.WriteAllLines(toFolder + file, lines);
            }
            return 0;
        }

        static int fixCSharp(string folder, string type)
        {
            string text;
            string fileName = string.Empty;
            string toReplace = string.Empty;
            string replaceWith = string.Empty;
            if(type == "float")
            {
                fileName = "SwigFannFloatPINVOKE.cs";
                toReplace = "\"SwigFannFloat\"";
                replaceWith = "\"fannfloat\"";
            }
            else if (type == "double")
            {
                fileName = "SwigFannDoublePINVOKE.cs";
                toReplace = "\"SwigFannDouble\"";
                replaceWith = "\"fanndouble\"";
            }
            else
            {
                fileName = "SwigFannFixedPINVOKE.cs";
                toReplace = "\"SwigFannFixed\"";
                replaceWith = "\"fannfixed\"";
            }
            Console.WriteLine("Fixing text in " + folder + fileName);
            text = File.ReadAllText(folder + fileName);
            text = text.Replace(toReplace, replaceWith);
            File.WriteAllText(folder + fileName, text);

            return 0;
        }

        static int fixCpp(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("CPP File does not exist: " + path);
                return -1;
            }
            string text = File.ReadAllText(path);
            Console.WriteLine("Fixing code in " + path);
            //Regex callbackRegex = new Regex(@"([a-zA-Z0-9\-_:]*?((callback_type)|(training_algorithm_enum)|(ActivationFunction)|(ErrorFunction)|(NetworkType)|(stop_function_enum)|(connection)))( |\()");
            //MatchCollection matches = callbackRegex.Matches(text);
            //int offset = 0;
            //foreach (Match match in matches)
            //{
            //    if (match.Groups[1].Value == "callback_type" || match.Groups[1].Value == "training_algorithm_enum" ||
            //        match.Groups[1].Value == "ActivationFunction" || match.Groups[1].Value == "ErrorFunction" ||
            //        match.Groups[1].Value == "NetworkType" || match.Groups[1].Value == "stop_function_enum" ||
            //        match.Groups[1].Value == "connection")
            //    {
            //        text = text.Insert(match.Groups[1].Index + offset, "FANN::");
            //        offset += 6;
            //    }
            //}
            //text = text.Replace("fann_error ", "struct fann_error ").Replace(" fann_error()", " struct fann_error()");
            //text = text.Replace("FANN::struct", "");
            //Regex trainingDataRegex = new Regex(@"((FANN::)|(CSharp_.*?))?training_data(_cpp)?");
            //matches = trainingDataRegex.Matches(text);
            //offset = 0;
            //foreach (Match match in matches)
            //{
            //    if (match.Value == "training_data")
            //    {
            //        text = text.Insert(match.Index + offset, "FANN::");
            //        offset += 6;
            //    }
            //}
            text = text.Replace("void (*arg5)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *) = (void (*)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *)) 0 ;",
                                "void (__stdcall *arg5)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *) = (void (__stdcall *)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *)) 0 ;");
            text = text.Replace("arg5 = (void (*)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *))jarg5;",
                                "arg5 = (void (__stdcall *)(unsigned int,unsigned int,unsigned int,fann_type *,fann_type *))jarg5;");
            File.Delete(path);
            File.WriteAllText(path, text);
            return 0;
        }
    }
}
