using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMVVM.Controls
{
    public class MyEmpryDataPiker : DatePicker
    {
        public MyDatePieker MyDatePieker { get; private set; }
        public MyEmpryDataPiker(MyDatePieker myDatePieker)
        {
            MyDatePieker = myDatePieker;
        }
    }
}
