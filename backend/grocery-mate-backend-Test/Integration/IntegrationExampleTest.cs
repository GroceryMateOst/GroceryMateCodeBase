using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using grocery_mate_backend.Sandbox;
using Moq;
using Xunit;
using Assert = Xunit.Assert;


public class IntegrationExampleTest
{
    [Xunit.Theory]
    [InlineData("6'8\"", true, 80)]
    [InlineData("6\"8'", false, 0)]
    [InlineData("six'eight\"", false, 0)]
    public void ConvertHeightTextToInches_VariousOptions(
        string heightText,
        bool expectedIsValid,
        double expectedHeightInInches)
    {
        IntegrationExample processor = new IntegrationExample(null);

        var actual = processor.ConvertHeightTextToInches(heightText);

        
        Assert.Equal(expectedIsValid, actual.isValid);
        Assert.Equal(expectedHeightInInches, actual.heightInInches);
    }

    [Xunit.Theory]
    [InlineData("Tim", "Corey", "6'8\"", 80)]
    [InlineData("Charitry", "Corey", "5'4\"", 64)]
    public void CreatePerson_Successful(string firstName, string lastName, string heightText, double expectedHeight)
    {
        IntegrationExample processor = new IntegrationExample(null);

        PersonModel expected = new PersonModel
        {
            FirstName = firstName,
            LastName = lastName,
            HeightInInches = expectedHeight,
            Id = 0
        };

        var actual = processor.CreatePerson(firstName, lastName, heightText);

        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.FirstName, actual.FirstName);
        Assert.Equal(expected.LastName, actual.LastName);
        Assert.Equal(expected.HeightInInches, actual.HeightInInches);
    }

    [Xunit.Theory]
    [InlineData("Tim#", "Corey", "6'8\"", "firstName")]
    [InlineData("Charitry", "C88ey", "5'4\"", "lastName")]
    [InlineData("Jon", "Corey", "SixTwo", "heightText")]
    [InlineData("", "Corey", "5'11\"", "firstName")]
    public void CreatePerson_ThrowsException(string firstName, string lastName, string heightText,
        string expectedInvalidParameter)
    {
        IntegrationExample processor = new IntegrationExample(null);

        var ex = Record.Exception(() => processor.CreatePerson(firstName, lastName, heightText));

        Assert.NotNull(ex);
        Assert.IsType<ArgumentException>(ex);
        if (ex is ArgumentException argEx)
        {
            Assert.Equal(expectedInvalidParameter, argEx.ParamName);
        }
    }

    [Fact]
    public void LoadPeople_ValidCall()
    {
        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISqliteDataAccess>()
                .Setup(x => x.LoadData<PersonModel>("select * from Person"))
                .Returns(GetSamplePeople());

            var cls = mock.Create<IntegrationExample>();
            var expected = GetSamplePeople();

            var actual = cls.LoadPeople();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].FirstName, actual[i].FirstName);
                Assert.Equal(expected[i].LastName, actual[i].LastName);
            }
        }
    }

    [Fact]
    public void SavePeople_ValidCall()
    {
        using (var mock = AutoMock.GetLoose())
        {
            var person = new PersonModel
            {
                Id = 1,
                FirstName = "Hans",
                LastName = "Peter",
                HeightInInches = 80
            };
            string sql = "insert into Person (FirstName, LastName, HeightInInches) " +
                         "values ('Hans', 'Peter', 80)";

            mock.Mock<ISqliteDataAccess>()
                .Setup(x => x.SaveData(person, sql));

            var cls = mock.Create<IntegrationExample>();

            cls.SavePerson(person);

            mock.Mock<ISqliteDataAccess>()
                .Verify(x => x.SaveData(person, sql), Times.Exactly(1));
        }
    }

    private List<PersonModel> GetSamplePeople()
    {
        List<PersonModel> output = new List<PersonModel>
        {
            new PersonModel
            {
                FirstName = "Tim",
                LastName = "Corey"
            },
            new PersonModel
            {
                FirstName = "Charity",
                LastName = "Corey"
            },
            new PersonModel
            {
                FirstName = "Jon",
                LastName = "Corey"
            },
            new PersonModel
            {
                FirstName = "Chris",
                LastName = "Corey"
            }
        };

        return output;
    }
}