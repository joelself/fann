using FannWrapperFloat;
using FannWrapperFixed;
using FannWrapperDouble;
using FannWrapper;

namespace FANNCSharp
{
    /// <summary> A fann file. </summary>
    ///
    /// <remarks> Joel Self, 11/10/2015. </remarks>

    public class FannFile
    {
        internal FannFile(SWIGTYPE_p_FILE file)
        {
            InternalFile = file;
        }

        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Joel Self, 11/10/2015. </remarks>
        ///
        /// <param name="filename"> Filename of the file. </param>
        /// <param name="mode">     The mode. </param>

        public FannFile(string filename, string mode)
        {
            InternalFile = fanndouble.fopen(filename, mode);
        }

        internal SWIGTYPE_p_FILE InternalFile { get; set; }
    }
}
