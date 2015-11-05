using FannWrapperFloat;
using FannWrapperFixed;
using FannWrapperDouble;
using FannWrapper;

namespace FANNCSharp
{
    public class FannFile
    {
        public FannFile(SWIGTYPE_p_FILE file)
        {
            InternalFile = file;
        }

        internal SWIGTYPE_p_FILE InternalFile { get; set; }
    }
}
