using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMVVM.Controls
{
    public class ModalPageWithBackBut:NavigationPage
    {
        ModalPageWithBackBut(ContentPage page)
        {
            ModalPageWithBackBut
            PushAsync(page);
        }
    }
}
