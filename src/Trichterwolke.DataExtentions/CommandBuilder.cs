namespace Trichterwolke.DataExtentions
{
    using System;
    using System.Text;
    using System.Data;
    using System.Linq;
    using System.Collections.Generic;


    /// <summary>
    /// Ein Komposition eines StringBuilder- und SqlCommand-Objekts um mittels eines 
    /// Methodenaufrufs einen optionalen SQL-Ausdruck sowie den dazugehörigen parameter hinzuzufügen.
    /// </summary>
    public class CommandBuilder : IDisposable, IDbCommand
    {
        private readonly IDbCommand command;
        private readonly StringBuilder builder = new StringBuilder();

        /// <summary>
        /// Erzeigt ein neues Objekt
        /// </summary>
        /// <param name="connection">SQL-Verbindungsobjekt</param>
        public CommandBuilder(IDbConnection connection)
        {
            this.command = connection.CreateCommand();
            this.command.Connection = connection;
        }

        /// <summary>
        /// Fügt einen Parameter hinzu
        /// </summary>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        public void AddParameter<T>(string parameterName, T parameterValue)
        {
            var parameter = this.command.CreateParameter();
            parameter.Value = parameterValue;
            parameter.ParameterName = parameterName;
            this.command.Parameters.Add(parameter);
        }

        /// <summary>
        /// Fügt den übergebenen Text am Ende des Commands an.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird./param>
        public void Append(string text)
        {
            builder.Append(text);
        }

        /// <summary>
        /// Fügt den übergebenen Text am Ende des Commands an.
        /// </summary>
        /// <param name="append">Gibt an, ob der Text angehängt wird oder nicht.</param>
        /// <param name="text">Der Text den Angefügt wird./param>
        public void Append(string text, bool append)
        {
            if (append)
            {
                builder.Append(text);
            }
        }

        /// <summary>
        /// Wenn der übergebene Parameter nicht null ist, dann wird der übergebene Text angehängt 
        /// und der Parameter der SqlConnection hinzugefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        public void AppendWithParameterWhenNotNull(string text, string parameterName, object parameterValue)
        {
            AppendWithParameter(text, parameterName, parameterValue, parameterValue != null);
        }

        /// <summary>
        /// Wenn der übergebene Parameter nicht null ist, dann wird der übergebene Text angehängt 
        /// und der Parameter als kommaseparierte Liste eingefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        public void AppendWithParameterWhenNotNull(string text, string parameterName, IEnumerable<byte> parameterValue)
        {
            AppendWithParameter<byte>(text, parameterName, parameterValue, parameterValue != null);
        }

        /// <summary>
        /// Wenn der übergebene Parameter nicht null ist, dann wird der übergebene Text angehängt 
        /// und der Parameter als kommaseparierte Liste eingefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        public void AppendWithParameterWhenNotNull(string text, string parameterName, IEnumerable<int> parameterValue)
        {
            AppendWithParameter<int>(text, parameterName, parameterValue, parameterValue != null);
        }

        /// <summary>
        /// Wenn der übergebene Parameter nicht null ist, dann wird der übergebene Text angehängt 
        /// und der Parameter als kommaseparierte Liste eingefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        public void AppendWithParameterWhenNotNull(string text, string parameterName, IEnumerable<short> parameterValue)
        {
            AppendWithParameter<short>(text, parameterName, parameterValue, parameterValue != null);
        }

        /// <summary>
        /// Wenn der übergebene Parameter nicht null ist, dann wird der übergebene Text angehängt 
        /// und der Parameter als kommaseparierte Liste eingefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        public void AppendWithParameterWhenNotNull(string text, string parameterName, IEnumerable<long> parameterValue)
        {
            AppendWithParameter<long>(text, parameterName, parameterValue, parameterValue != null);
        }

        /// <summary>        
        /// Wenn append true ist, dann wird der übergebene Text angehängt 
        /// und der Parameter der SqlParemterConnection hinzugefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        /// <param name="append">Gibt an, ob der Parameter und der Text angehängt wird oder nicht.</param>
        public void AppendWithParameter(string text, string parameterName, object parameterValue, bool append)
        {
            if (append)
            {
                this.builder.Append(text);
                var parameter = this.command.CreateParameter();
                parameter.Value = parameterValue;
                parameter.ParameterName = parameterName;
                this.command.Parameters.Add(parameter);
            }
        }

        /// <summary>        
        /// Wenn append true ist, dann wird der übergebene Text angehängt und der übergebene Parameter
        /// als kommaseparierte Liste eingefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        /// <param name="append">Gibt an, ob der Parameter und der Text angehängt wird oder nicht.</param>
        private void AppendWithParameter(string text, string parameterName, IEnumerable<byte> parameterValue, bool append)
        {
            AppendWithParameter<byte>(text, parameterName, parameterValue, append);
        }

        /// <summary>        
        /// Wenn append true ist, dann wird der übergebene Text angehängt und der übergebene Parameter
        /// als kommaseparierte Liste eingefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        /// <param name="append">Gibt an, ob der Parameter und der Text angehängt wird oder nicht.</param>
        private void AppendWithParameter(string text, string parameterName, IEnumerable<short> parameterValue, bool append)
        {
            AppendWithParameter<short>(text, parameterName, parameterValue, append);
        }

        /// <summary>        
        /// Wenn append true ist, dann wird der übergebene Text angehängt und der übergebene Parameter
        /// als kommaseparierte Liste eingefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        /// <param name="append">Gibt an, ob der Parameter und der Text angehängt wird oder nicht.</param>
        private void AppendWithParameter(string text, string parameterName, IEnumerable<int> parameterValue, bool append)
        {
            AppendWithParameter<int>(text, parameterName, parameterValue, append);
        }

        /// <summary>        
        /// Wenn append true ist, dann wird der übergebene Text angehängt und der übergebene Parameter
        /// als kommaseparierte Liste eingefügt.
        /// </summary>
        /// <param name="text">Der Text den Angefügt wird.</param>
        /// <param name="parameterName">Name des Parameters</param>
        /// <param name="parameterValue">Parameterwert</param>
        /// <param name="append">Gibt an, ob der Parameter und der Text angehängt wird oder nicht.</param>
        private void AppendWithParameter(string text, string parameterName, IEnumerable<long> parameterValue, bool append)
        {
            AppendWithParameter<long>(text, parameterName, parameterValue, append);
        }


        private void AppendWithParameter<T>(string text, string parameterName, IEnumerable<T> parameterValue, bool append)
        {
            if (append && text != null && parameterValue != null)
            {
                this.builder.Append(text.Replace(parameterName, string.Join(", ", parameterValue)));
                var parameter = this.command.CreateParameter();
                parameter.Value = parameterValue;
                parameter.ParameterName = parameterName;
                this.command.Parameters.Add(parameter);
            }
        }

        private void SetCommandText()
        {
            this.command.CommandText = this.builder.ToString();
        }

        public IDataReader ExecuteReader()
        {
            SetCommandText();
            return this.command.ExecuteReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            SetCommandText();
            return this.command.ExecuteReader(behavior);
        }

        public object ExecuteScalar()
        {
            SetCommandText();
            return this.command.ExecuteScalar();
        }

        public int ExecuteNonQuery()
        {
            SetCommandText();
            return this.command.ExecuteNonQuery();
        }

        public IDataParameterCollection Parameters
        {
            get { return this.command.Parameters; }
        }

        public IDbConnection Connection
        {
            get { return this.command.Connection; }
            set { this.command.Connection = value; }
        }

        public IDbTransaction Transaction
        {
            get { return this.command.Transaction; }
            set { this.command.Transaction = value; }
        }

        public string CommandText
        {
            get { return this.builder.ToString(); }
            set
            {
                this.builder.Clear();
                this.builder.Append(value);
            }
        }

        public int CommandTimeout
        {
            get { return this.command.CommandTimeout; }
            set { this.command.CommandTimeout = value; }
        }

        public CommandType CommandType
        {
            get { return this.command.CommandType; }
            set { this.command.CommandType = value; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get { return this.command.UpdatedRowSource; }
            set { this.command.UpdatedRowSource = value; }
        }

        /// <summary>
        /// Gibt die Resouces des SqlCommand wieder frei.
        /// </summary>
        public void Dispose()
        {
            if (command != null)
            {
                command.Dispose();
            }
        }

        public void Prepare()
        {
            this.command.Prepare();
        }

        public void Cancel()
        {
            this.command.Cancel();
        }

        public IDbDataParameter CreateParameter()
        {
            return this.command.CreateParameter();
        }
    }
}
