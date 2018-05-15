namespace Prism._01.BootstrapperSample.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Prism._01.BootstrapperSample.ViewModels;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// <see cref="Person" /> のテストクラスです。
    /// </summary>
    public class PersonTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public PersonTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public static IEnumerable<object[]> GetNamePropertyTestData()
        {
            yield return new object[]
                             {
                                 "(正常系) 文字列を設定するとエラーにならないこと。",
                                 "Name .",
                                 false,
                             };
            yield return new object[]
                             {
                                 "(異常系) nullを設定するとエラーになること。",
                                 null,
                                 true,
                             };
            yield return new object[]
                             {
                                 "(異常系) string.Emptyを設定するとエラーになること。",
                                 string.Empty,
                                 true,
                             };
            yield return new object[]
                             {
                                 "(異常系) 空白スペースを設定するとエラーになること。",
                                 " ",
                                 true,
                             };
        }

        [Theory]
        [MemberData(nameof(GetNamePropertyTestData))]
        public void Test_Theory_NameProperty(string caseName, string name, bool hasError)
        {
            Output(caseName, Parameter.Factory(nameof(name), name), Parameter.Factory(nameof(hasError), hasError));

            // arrange
            var vm = new Person { Name = DateTime.Now.Millisecond.ToString(), };

            // act
            var ex = Record.Exception(() => vm.Name = name);

            // assert
            Assert.Null(ex);
            Assert.Equal(hasError, vm.HasErrorsProperty(x=> x.Name));
        }

        [Fact]
        public void Test_Success_NamePropertyChanged()
        {
            // arrange
            var vm = new Person();
            var tracker = new PropertyChangeTracker<Person>(vm);
            
            // act
            vm.Name = DateTime.Now.ToString("G");

            // assert
            Assert.True(tracker.IsChangedProperty(x => x.Name));
        }

        private void Output(string caseName, params Parameter[] args)
        {
            var sb = new StringBuilder();
            sb.AppendLine(caseName);
            if (args != null && args.Any())
            {
                sb.AppendLine("[Parameters]");

                foreach (var param in args)
                {
                    sb.AppendLine($" {param.Name} : {param.Value}");
                }

            }
            _testOutputHelper.WriteLine(sb.ToString());
        }
    }

    public class PropertyChangeTracker<T> where T : INotifyPropertyChanged
    {
        private readonly T _targetViewModel;

        private readonly List<string> _changedProperties;
        
        public PropertyChangeTracker(T targetViewModel)
        {
            _changedProperties = new List<string>();
            _targetViewModel = targetViewModel;
            _targetViewModel.PropertyChanged += (sender, e) =>
                                                {
                                                    if (!_changedProperties.Contains(e.PropertyName))
                                                        _changedProperties.Add(e.PropertyName);
                                                };
        }

        public IEnumerable<string> ChangedProperties => _changedProperties;

        public bool IsChangedProperty<TProperty>(Expression<Func<T, TProperty>> propertyNameExpression)
        {
            var member = propertyNameExpression.Body as MemberExpression;
            if (member == null) throw new InvalidOperationException();

            var propertyName = member.Member.Name;
            return ChangedProperties.Contains(propertyName);
        }
    }

    public sealed class Parameter
    {
        private Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public object Value { get; private set; }

        public static Parameter Factory(string name, object value)
        {
            return new Parameter(name, value);
        }
    }
}