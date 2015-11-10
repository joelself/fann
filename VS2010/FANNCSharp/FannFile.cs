using FannWrapperFloat;
using FannWrapperFixed;
using FannWrapperDouble;
using FannWrapper;

namespace FANNCSharp
{
    public class FannFile
    {
        internal FannFile(SWIGTYPE_p_FILE file)
        {
            InternalFile = file;
        }

        public FannFile(string filename, string mode)
        {
            InternalFile = fanndouble.fopen(filename, mode);
        }

        internal SWIGTYPE_p_FILE InternalFile { get; set; }
    }
}
