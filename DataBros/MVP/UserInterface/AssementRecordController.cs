using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DataBros.MVP.UserInterface
{
   
        public class AssementRecordController : IAssementRecordController
        {
            private readonly IReading model;
            private readonly IAssesmentRecordView view;

            public AssementRecordController(IAssesmentRecordView view, IReading model)
            {
                this.view = view;
                this.model = model;
            }

            /// <summary>
            /// Subscribes to view events
            /// </summary>
            public void HandleValueUpdated(int value)
            {
                model.Actual = value;

                var variance = model.GetVariantCategory();
                if (variance == VariantGategory.Normal)
                {
                    view.SetVarianceCategoryColor(Color.DarkGreen);
                }
                else
                {
                    view.SetVarianceCategoryColor(Color.IndianRed);
                }
            }

        }
    }
