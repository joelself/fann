using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FannWrapperFloat
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int training_callback(neural_net net, training_data data, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, Object user_data);
}

namespace FannWrapperDouble
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int training_callback(global::System.Runtime.InteropServices.HandleRef net, global::System.Runtime.InteropServices.HandleRef data, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, Object user_data);
}

namespace FannWrapperFixed
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int training_callback(global::System.Runtime.InteropServices.HandleRef net, global::System.Runtime.InteropServices.HandleRef data, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, Object user_data);
}
