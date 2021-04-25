using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DataBros.MVP.UserInterface
{
    public interface IAssesmentRecordView
    {
        event Action<int> ActualValueChanged;
        void SetVarianceCategoryColor(Color color);
        void HandleValueUpdated(int variance);

    }
}
