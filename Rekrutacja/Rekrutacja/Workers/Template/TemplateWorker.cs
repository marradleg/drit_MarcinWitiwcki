using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soneta.Kadry;
using Soneta.KadryPlace;
using Soneta.Types;
using Rekrutacja.Workers.Template;
using Rekrutacja.Operations;
using Soneta.Kasa.Extensions;
using Soneta.Business.UI;
using static Soneta.ETHECR.Connection.SerialPort;
using Soneta.Tools;
using Soneta.Core.Extensions;
using Mono.CSharp;
using Rekrutacja.Validations;
using Rekrutacja.Enums;
using Syncfusion.XPS;
using Rekrutacja.AreaFigures;

//Rejetracja Workera - Pierwszy TypeOf określa jakiego typu ma być wyświetlany Worker, Drugi parametr wskazuje na jakim Typie obiektów będzie wyświetlany Worker
[assembly: Worker(typeof(TemplateWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.Template
{
    public class TemplateWorker
    {
        //Aby parametry działały prawidłowo dziedziczymy po klasie ContextBase
        public class TemplateWorkerParametry : ContextBase
        {
            [Caption("A")]
            public uint VariableX { get; set; }

            [Caption("B")]
            public uint VariableY { get; set; }

            [Caption("Operacja")]
            public eFigure Figure { get; set; }

            public TemplateWorkerParametry(Context context) : base(context)
            {
                this.VariableX = 0;
                this.VariableY = 0;
                this.Figure = eFigure.trókąt;
            }
        }
        //Obiekt Context jest to pudełko które przechowuje Typy danych, aktualnie załadowane w aplikacji
        //Atrybut Context pobiera z "Contextu" obiekty które aktualnie widzimy na ekranie
        [Context]
        public Context Cx { get; set; }
        //Pobieramy z Contextu parametry, jeżeli nie ma w Context Parametrów mechanizm sam utworzy nowy obiekt oraz wyświetli jego formatkę
        [Context]
        public TemplateWorkerParametry Parametry { get; set; }
        //Atrybut Action - Wywołuje nam metodę która znajduje się poniżej
        [Action("Kalkulator",
           Description = "Prosty kalkulator ",
           Priority = 10,
           Mode = ActionMode.ReadOnlySession,
           Icon = ActionIcon.Accept,
           Target = ActionTarget.ToolbarWithText)]
        public void WykonajAkcje()
        {
            //Włączenie Debug, aby działał należy wygenerować DLL w trybie DEBUG
            DebuggerSession.MarkLineAsBreakPoint();
            CalculateAreaFigure calculateFigure = new CalculateAreaFigure();

            Soneta.Kadry.Pracownik[] employees = null;
            if (this.Cx.Contains(typeof(Pracownik[])))
            {
                employees = (Pracownik[])this.Cx[typeof(Pracownik[])];
            }

            foreach (var employee in employees)
            {
                //Modyfikacja danych
                //Aby modyfikować dane musimy mieć otwartą sesję, któa nie jest read only
                using (Session nowaSesja = this.Cx.Login.CreateSession(false, false, "ModyfikacjaPracownika"))
                {
                    //Validation Entered Data
                    Validation validation = new Validation(nowaSesja);
                    if (!validation.ValidVariableThanZero(this.Parametry.Figure, this.Parametry.VariableX, this.Parametry.VariableY))
                        break;

                    //Otwieramy Transaction aby można było edytować obiekt z sesji
                    using (ITransaction trans = nowaSesja.Logout(true))
                    {
                        //Pobieramy obiekt z Nowo utworzonej sesji
                        var pracownikZSesja = nowaSesja.Get(employee);

                        if (this.Parametry.Figure == eFigure.koło || this.Parametry.Figure == eFigure.kwadrat)
                            pracownikZSesja.Features["Wynik"] = calculateFigure.CalculateArea(this.Parametry.Figure, new uint[] { this.Parametry.VariableX });
                        else
                            pracownikZSesja.Features["Wynik"] = calculateFigure.CalculateArea(this.Parametry.Figure, new uint[] { this.Parametry.VariableX, this.Parametry.VariableY });


                        //Zatwierdzamy zmiany wykonane w sesji
                        trans.CommitUI();
                        }
                            //Zapisujemy zmiany
                            nowaSesja.Save();

                
                    }
                }
        }
    }
}