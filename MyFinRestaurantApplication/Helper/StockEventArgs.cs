using System;

namespace ManagerApplication.Model
{
    public class StockEventArgs : EventArgs
    {
        public Stock NewStock { get; set; }
    }
}
