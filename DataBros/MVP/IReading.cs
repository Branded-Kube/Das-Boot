using System;

namespace DataBros.MVP
{
    public interface IReading
    {

        event Action<int> OnActualUpdate;

        int Actual { get; set; }


        int GetVariant();


        VariantGategory GetVariantCategory();
        
    }
}