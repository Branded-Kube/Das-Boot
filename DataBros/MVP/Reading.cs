using System;
using System.Collections.Generic;
using System.Text;

namespace DataBros.MVP
{
    public class Reading : IReading
    {
        public event Action<int> OnActualUpdate;

        private int actual;

        public int Actual
        {
            get
            {
                return actual;
            }
            set
            {
                actual = value;
                PostActualUpdated();
            }
        }

        private void PostActualUpdated()
        {
            OnActualUpdate?.Invoke(GetVariant());
        }

        public int GetVariant()
        {
            if (Actual % 2 == 0)
            {
                return 0;
            }
            else
                return 1;
        }

        public VariantGategory GetVariantCategory()
        {
            if (GetVariant() == 0)
            {
                return VariantGategory.Normal;
            }
            else
                return VariantGategory.Served;
        }

    }
}
