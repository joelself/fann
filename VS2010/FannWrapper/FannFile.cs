using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FannWrap;
namespace FannWrapper
{
    class FannFile : IDisposable
    {
        public FannFile(SWIGTYPE_p_FILE file)
        {
            InternalFile = file;
        }

        internal SWIGTYPE_p_FILE InternalFile { get; set; }
    }
}
