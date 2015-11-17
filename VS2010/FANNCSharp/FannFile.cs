using FannWrapperFloat;
using FannWrapperFixed;
using FannWrapperDouble;
using System;
using System.IO;
/*
 * Title: FANN C# FILE* wrapper
 */
namespace FANNCSharp
{
    /* Class: FannFile

        A C# class wrapper around a C FILE pointer

        See Also:
            <NeuralNetFloat.ErrorLog>

       This structure appears in FANN >= 2.1.0
    */
    public class FannFile : IDisposable
    {
        internal FannFile(SWIGTYPE_p_FILE file)
        {
            InternalFile = file;
        }

        /* Constructor: FannFile
            
           Encapsulates a C FILE pointer
         
           Parameters:
             filename   - The filepath to open
             mode       - The mode to open file in. "r" for read, "w" write, "a" for append,
                            "r+" for read/update, "w+" for write update, "a+" for append/update...
           See also:
             <fopen at http://www.cplusplus.com/reference/cstdio/fopen/>
        */

        public FannFile(string filename, string mode)
        {
            InternalFile = fannfloat.fopen(filename, mode);
        }

        /* Method: Dispose
            
           Closes the file

           See also:
             <fclose at http://www.cplusplus.com/reference/cstdio/fclose/>
        */
        public void Dispose()
        {
            int code = fannfloat.fclose(InternalFile);
            if (code != 0)
            {
                throw new IOException("Error occurred when trying to close the file. Code: {0}", code);
            }
        }

        internal SWIGTYPE_p_FILE InternalFile { get; set; }
    }
}
