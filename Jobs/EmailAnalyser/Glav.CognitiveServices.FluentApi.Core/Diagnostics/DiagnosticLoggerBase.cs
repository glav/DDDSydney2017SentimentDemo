﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Glav.CognitiveServices.FluentApi.Core.Diagnostics
{
    public abstract class DiagnosticLoggerBase : IDiagnosticLogger
    {
        const string MessageType_Error = "Error";
        const string MessageType_Warning = "Wrning";
        const string MessageType_Informational = "Informational";

        public void LogInfo(string message, string topic = null)
        {
            var msg = FormLogMessage(topic, message, MessageType_Informational);
            WriteToOutput(msg);
        }

        public void LogWarning(string message, string topic = null)
        {
            var msg = FormLogMessage(topic, message, MessageType_Warning);
            WriteToOutput(msg);
        }


        public void LogError(string message, string topic = null)
        {
            var msg = FormLogMessage(topic, message, MessageType_Error);
            WriteToOutput(msg);
        }

        public void LogError(Exception ex, string topic = null)
        {
            var exceptionDetails = ExtractDetailsFromException(ex);
            var msg = FormLogMessage(topic, exceptionDetails, MessageType_Error);
            WriteToOutput(msg);
        }

        public abstract void WriteToOutput(string msg);


        protected virtual string ExtractDetailsFromException(Exception ex)
        {
            var builder = new StringBuilder();
            var aggregateEx = ex as AggregateException;
            if (aggregateEx != null)
            {
                if (aggregateEx.InnerExceptions != null)
                {
                    foreach (var inEx in aggregateEx.InnerExceptions)
                    {
                        builder.AppendFormat(" {0} - {1} - StackTrace: {2}", inEx.Source, inEx.Message, inEx.StackTrace);
                    }
                }
                else if (aggregateEx.InnerException != null)
                {
                    builder.AppendFormat(" {0} - {1} - StackTrace: {2}", aggregateEx.InnerException.Source, 
                                aggregateEx.InnerException.Message, 
                                aggregateEx.InnerException.StackTrace);
                }
            }
            else
            {
                var baseEx = ex.GetBaseException();
                builder.AppendFormat(" {0} - {1} - StackTrace: {2}", baseEx.Source, baseEx.Message, baseEx.StackTrace);
            }
            return builder.ToString();
        }

        protected virtual string FormLogMessage(string topic, string message, string messageType)
        {
            return $"{DateTimeDescriptor()},<{messageType}>,#{topic}#, {message}";
        }

        private string DateTimeDescriptor()
        {
            return $"[{DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss.ffff")} UTC]";
        }

    }
}
