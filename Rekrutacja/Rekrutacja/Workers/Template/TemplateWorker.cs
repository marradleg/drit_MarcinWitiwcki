﻿using Soneta.Business;
using System;
using Soneta.Kadry;
using Soneta.Types;
using Rekrutacja.Workers.Template;
using Rekrutacja.Operations;
using Rekrutacja.Validations;
using Rekrutacja.Converter;

//Rejetracja Workera - Pierwszy TypeOf określa jakiego typu ma być wyświetlany Worker, Drugi parametr wskazuje na jakim Typie obiektów będzie wyświetlany Worker
[assembly: Worker(typeof(TemplateWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.Template
{
    public class TemplateWorker
    {
        //Aby parametry działały prawidłowo dziedziczymy po klasie ContextBase
        public class TemplateWorkerParametry : ContextBase
        {
            [Caption("Data obliczeń")]
            public Date DateOfCalculation { get; set; }

            [Caption("A")]
            public string VariableX { get; set; }

            [Caption("B")]
            public string VariableY { get; set; }
            [Caption("Operacja")]
            public string Operation{ get; set; }

            public TemplateWorkerParametry(Context context) : base(context)
            {
                this.DateOfCalculation = Date.Today;
                this.VariableX = "0";
                this.VariableY = "0";
                this.Operation = "+";
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
            Calculator calculator = new Calculator();

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

                    if(!validation.ValidStringToInt(this.Parametry.VariableX) || !validation.ValidStringToInt(this.Parametry.VariableY))
                        break;

                    if (!validation.ValidOperation(this.Parametry.Operation) ||
                        !validation.ValidDate(this.Parametry.DateOfCalculation) ||
                        !validation.ValidDivisionVariable(this.Parametry.Operation, this.Parametry.VariableY.ConvertToInt()))
                            break;

                    //Otwieramy Transaction aby można było edytować obiekt z sesji
                    using (ITransaction trans = nowaSesja.Logout(true))
                        {
                            //Pobieramy obiekt z Nowo utworzonej sesji
                            var pracownikZSesja = nowaSesja.Get(employee);
                            //Features - są to pola rozszerzające obiekty w bazie danych, dzięki czemu nie jestesmy ogarniczeni to kolumn jakie zostały utworzone przez producenta
                            pracownikZSesja.Features["DataObliczen"] = this.Parametry.DateOfCalculation;

                            pracownikZSesja.Features["Wynik"] = calculator.Calculate(this.Parametry.VariableX.ConvertToInt(), this.Parametry.VariableY.ConvertToInt(), this.Parametry.Operation);
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