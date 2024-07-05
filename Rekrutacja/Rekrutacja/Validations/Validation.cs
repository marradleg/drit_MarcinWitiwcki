﻿using RabbitMQ.Client.Impl;
using Rekrutacja.Enums;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Core.Extensions;
using Soneta.Generator.Schema;
using Soneta.Tools;
using Soneta.Types;
using System;
using static Soneta.ETHECR.Connection.SerialPort;


namespace Rekrutacja.Validations
{
    internal class Validation
    {
        private Soneta.Business.Session _sesion;
        private IMessageBoxService _messageBoxService;
        public Validation(Soneta.Business.Session session)
        {
            _sesion = session;
            _sesion.GetService(out _messageBoxService, false);

        }
        public bool ValidOperation(string operation)
        {
            if ("+-*/".Contains(operation) && !string.IsNullOrEmpty(operation))
            {
                return true;
            }
            else
            {
                _messageBoxService.Show("Nieprawidłowy znak operacji dostępne są + - * /");
                return false;
            }
        }

        public bool ValidDate(DateTime dateTime)
        {

            if (dateTime == null || dateTime == DateTime.MinValue)
            {
                _messageBoxService.Show("Wprowadzona data jest niepoprawna");
                return false;
            }

            return true;


        }

        public bool ValidDivisionVariable(string operation, double variableY)
        {
            if ((operation == "/") && (variableY == 0))
            {
                _messageBoxService.Show("Nie można dzielić przez 0");
                return false;
            }
            return true;
        }

        public bool ValidVariableThanZero(eFigure figure, uint variableX, uint variableY)
        {
            if (figure == eFigure.koło)
            {
                if (variableX > 0)
                {
                    return true;
                }
                _messageBoxService.Show($"Do obliczenia pola powierzchni {figure.ToString()} wartość zmiennej A musi być większa od 0");
                return false;
            }

            if ((variableX > 0) && (variableY > 0))
                return true;

                _messageBoxService.Show($"Do obliczenia pola powierzchni {figure.ToString()} wartość zmiennej A i B musi być większa od 0");
                return false;
        }
    }
}
