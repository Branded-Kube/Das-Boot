using DataBros.MVP;
using DataBros.MVP.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBros
{
    //public static class Program
    //{
    //    [STAThread]
    //    static void Main()
    //    {
    //        using (var game = new GameWorld())
    //            game.Run();
    //    }
    //}
   public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //Form1 view1 = new Form1();
            //Form1 view2 = new Form1();

            //IReading model = new Reading();
            //IAssementRecordController controller = new AssementRecordController(view1, model);
            //controller = new AssementRecordController(view2, model);


            //((IAssesmentRecordView)view1).ActualValueChanged += controller.HandleValueUpdated;
            //model.OnActualUpdate += ((IAssesmentRecordView)view1).HandleValueUpdated;

            //((IAssesmentRecordView)view2).ActualValueChanged += controller.HandleValueUpdated;
            //model.OnActualUpdate += ((IAssesmentRecordView)view2).HandleValueUpdated;

            //Application.Run(view1);
            //Application.Run(view2);


            using (var game = GameWorld.Instance)
                game.Run();
        }
    }
}
