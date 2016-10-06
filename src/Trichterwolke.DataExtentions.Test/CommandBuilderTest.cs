namespace Trichterwolke.DataExtentions.Test
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Xunit;

    public class CommandBuilderTest
    {
        [Fact]
        public void Append()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa"); ;

            var expected = @"SELECT elephant FROM africa";
            var actual = target.CommandText;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, "SELECT elephant FROM africa")]
        [InlineData(true, "SELECT elephant FROM africa WHERE sex = 'female'")]
        public void Append_conditioanl(bool append, string expected)
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa");
            target.Append(@" WHERE sex = 'female'", append);
            
            var actual = target.CommandText;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AppendParameterPhraseWhenNotNull_check_command_text()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa ");
            target.Append(@"WHERE sex = 'male' ");

            target.AppendWithParameterWhenNotNull("AND born > @date", "@date", new DateTime(2016, 10, 03));

            var expected = @"SELECT elephant FROM africa WHERE sex = 'male' AND born > @date";
            var actual = target.CommandText;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AppendParameterPhraseWhenNotNull_check_command_text_parameter_null()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa ");
            target.Append(@"WHERE sex = 'male' ");

            target.AppendWithParameterWhenNotNull("AND born > @date", "@date", (object)null);

            var expected = @"SELECT elephant FROM africa WHERE sex = 'male' ";
            var actual = target.CommandText;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AppendParameterPhraseWhenNotNull_check_parameter_datetime()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa ");
            target.Append(@"WHERE sex = 'male' ");

            target.AppendWithParameterWhenNotNull("AND born > @date", "@date", new DateTime(2016, 10, 03));

            var actual = (SqlParameter)target.Parameters[0];

            Assert.Equal("@date", actual.ParameterName);
            Assert.Equal(new DateTime(2016, 10, 03), actual.Value);
        }

        [Fact]
        public void AppendParameterPhraseWhenNotNull_check_parameter_string()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa ");
            target.Append(@"WHERE sex = 'male' ");

            target.AppendWithParameterWhenNotNull("AND name like @name", "@name", "Jumbo");

            var actual = (SqlParameter)target.Parameters[0];

            Assert.Equal("@name", actual.ParameterName);
            Assert.Equal("Jumbo", actual.Value);
        }

        [Fact]
        public void AppendParameterPhraseWhenNotNull_check_parameter_null()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa ");
            target.Append(@"WHERE sex = 'male' ");

            target.AppendWithParameterWhenNotNull("AND born > @date", "@date", (object)null);

            var actual = target.Parameters;

            Assert.Empty(actual);
        }

        [Fact]
        public void AddParameter()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa");
            target.Append(@" WHERE sex = @sex");

            target.AddParameter("@sex", "male");

            var actual = (SqlParameter)target.Parameters[0];

            Assert.Equal("@sex", actual.ParameterName);
            Assert.Equal("male", actual.Value);
        }

        [Fact]
        public void AppendParameterPhraseWhenNotNull_list_check_command_text()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa ");
            target.Append(@"WHERE sex = 'male' ");

            target.AppendWithParameterWhenNotNull("AND id in (@idlist)", "@idlist", new long[] { 1, 3, 5 });

            var expected = @"SELECT elephant FROM africa WHERE sex = 'male' AND id in (1, 3, 5)";
            var actual = target.CommandText;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void AppendParameterPhraseWhenNotNull_list_check_parameter_null()
        {
            var target = new CommandBuilder(new SqlConnection());
            target.Append(@"SELECT elephant ");
            target.Append(@"FROM africa ");
            target.Append(@"WHERE sex = 'male' ");

            target.AppendWithParameterWhenNotNull("AND id in (@idlist)", "@idlist", (long[])null);

            var actual = target.Parameters;

            Assert.Empty(actual);
        }
    }
}
